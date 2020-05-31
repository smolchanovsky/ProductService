using System.Threading.Tasks;
using ProductService.Core.Models;

namespace ProductService.Core.Validation
{
    public interface IProductValidator
    {
        Task<bool> ValidateAsync(Product product);
    }
}
