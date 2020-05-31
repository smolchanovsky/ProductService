using System.Threading.Tasks;
using ProductService.Core.Models;

namespace ProductService.Core.ServiceLayer
{
    public interface IProductSaver
    {
        Task AddAsync(Product product);
    }
}
