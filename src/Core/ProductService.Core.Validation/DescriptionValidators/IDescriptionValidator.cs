using System.Threading.Tasks;

namespace ProductService.Core.Validation.DescriptionValidators
{
    public interface IDescriptionValidator
    {
        Task<bool> ValidateAsync(string? description);
    }
}
