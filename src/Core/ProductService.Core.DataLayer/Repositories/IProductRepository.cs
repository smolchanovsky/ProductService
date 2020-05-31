using ProductService.Core.DataLayer.Entities;
using ProductService.Infrastructure.DataLayer.Repositories;

namespace ProductService.Core.DataLayer.Repositories
{
    public interface IProductRepository : IRepository<ProductDb, long>
    {

    }
}
