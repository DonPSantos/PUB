using Microsoft.EntityFrameworkCore;
using PUB.Domain.Entities;

namespace PUB.Data.Context
{
    public class PUBContext : DbContext
    {
        public PUBContext(DbContextOptions<PUBContext> options) : base(options)
        {
        }

        public DbSet<OneDrinkPromo> OneDrinkPromos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Define um valor padrão para a coluna do banco caso não tenha sido mapeada
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PUBContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("InsertDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("InsertDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("LastModified").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}