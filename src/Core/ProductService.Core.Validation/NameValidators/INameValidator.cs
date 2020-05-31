using System.Threading.Tasks;

namespace ProductService.Core.Validation.NameValidators
{
    public interface INameValidator
    {
        Task<bool> ValidateAsync(string? name);
    }
}
