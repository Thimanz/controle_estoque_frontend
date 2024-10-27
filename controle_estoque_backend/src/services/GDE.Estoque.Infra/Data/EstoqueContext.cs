using FluentValidation.Results;
using GDE.Core.Data;
using GDE.Core.DomainObjects;
using GDE.Core.Mediator;
using GDE.Core.Messages;
using GDE.Estoque.Domain;
using Microsoft.EntityFrameworkCore;

namespace GDE.Estoque.Infra.Data
{
    public sealed class EstoqueContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public EstoqueContext(DbContextOptions<EstoqueContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Local> Locais { get; set; }
        public DbSet<LocalItem> LocalItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstoqueContext).Assembly);
        }


        public async Task<bool> Commit()
        {
            try
            {
                var sucesso = await base.SaveChangesAsync() > 0;

                if (sucesso) await _mediatorHandler.PublicarEventos(this);

                return sucesso;
            }
            catch { return false; }
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity?.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity?.Notificacoes)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublicarEvento(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
