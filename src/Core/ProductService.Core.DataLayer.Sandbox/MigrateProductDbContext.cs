using Microsoft.EntityFrameworkCore;

namespace ProductService.Core.DataLayer.Sandbox
{
    public class MigrateProductDbContext : ProductDbContext
    {
        public MigrateProductDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ProductService;Trusted_Connection=True;");
            }
        }
    }
}
