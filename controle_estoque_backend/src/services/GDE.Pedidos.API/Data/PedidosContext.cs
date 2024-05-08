using FluentValidation.Results;
using GDE.Core.Data;
using GDE.Core.Messages;
using GDE.Pedidos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GDE.Pedidos.API.Data
{
    public class PedidosContext : DbContext, IUnitOfWork
    {
        public PedidosContext(DbContextOptions<PedidosContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<PedidoVenda> PedidosVenda { get; set; }
        public DbSet<PedidoCompra> PedidosCompra { get; set; }
        public DbSet<PedidoTransferencia> PedidosTransferencia { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidosContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
