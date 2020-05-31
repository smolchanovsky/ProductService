using ProductService.Infrastructure.DataLayer;

namespace ProductService.Core.DataLayer.Entities
{
    public class ProductDb : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
