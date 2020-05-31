using System.Threading.Tasks;

namespace ProductService.Core.Validation.DescriptionValidators
{
    public class DescriptionValidatorB : IDescriptionValidator
    {
        public Task<bool> ValidateAsync(string? description)
        {
            if (description?.Length > 500)
                return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}
