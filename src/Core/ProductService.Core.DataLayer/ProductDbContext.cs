using Microsoft.EntityFrameworkCore;
using ProductService.Core.DataLayer.Entities;

namespace ProductService.Core.DataLayer
{
    public class ProductDbContext : DbContext
    {
        private readonly IProductDbContextConfig productDbContextConfig = null!;

        public virtual DbSet<ProductDb> Products { get; set; } = null!;

        public ProductDbContext(IProductDbContextConfig productDbContextConfig)
        {
            this.productDbContextConfig = productDbContextConfig;
        }

        internal ProductDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(productDbContextConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDb>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(x => x.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Description)
                    .HasMaxLength(500);
            });
        }
    }
}
