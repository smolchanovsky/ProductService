using System.Threading.Tasks;

namespace ProductService.Core.Validation.NameValidators
{
    public class NameValidatorB : INameValidator
    {
        public Task<bool> ValidateAsync(string? name)
        {
            return Task.FromResult(true);
        }
    }
}
