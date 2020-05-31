using ProductService.Core.DataLayer.Entities;
using ProductService.Core.Models;

namespace ProductService.Core.ServiceLayer.Mappers
{
    public interface IProductMapper
    {
        ProductDb Map(Product product);
        Product Map(ProductDb productDb);
    }
}
