using FluentValidation.Results;
using GDE.Core.Data;
using GDE.Core.Messages;
using GDE.Estoque.Domain;
using Microsoft.EntityFrameworkCore;

namespace GDE.Estoque.Infra.Data
{
    public class EstoqueContext : DbContext, IUnitOfWork
    {
        public EstoqueContext(DbContextOptions<EstoqueContext> options)
            : base(options)
        {
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
            return await base.SaveChangesAsync() > 0;
        }
    }
}
