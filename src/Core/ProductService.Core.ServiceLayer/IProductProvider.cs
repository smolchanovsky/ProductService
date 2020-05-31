using System.Threading.Tasks;
using ProductService.Core.Models;

namespace ProductService.Core.ServiceLayer
{
    public interface IProductProvider
    {
        Task<Product?> FindAsync(long id);
    }
}
