using System.Threading.Tasks;

namespace ProductService.Core.Validation.DescriptionValidators
{
    public class DescriptionValidatorA : IDescriptionValidator
    {
        public Task<bool> ValidateAsync(string? description)
        {
            return Task.FromResult(true);
        }
    }
}
