using Microsoft.EntityFrameworkCore;
using Store.Data.EntityFrameworkCore.Uow;
using Store.Domain.Entity;

namespace Store.Data.EntityFrameworkCore
{
    public class EfCoreApplicationDbContext : DbContext, IDbContext
    {
        public EfCoreApplicationDbContext(DbContextOptions<EfCoreApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductSku> ProductSku { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //config sql field 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfCoreApplicationDbContext).Assembly);

            //hide deleted items
            modelBuilder.ApplyQueryFilter();

            //seed tables
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
