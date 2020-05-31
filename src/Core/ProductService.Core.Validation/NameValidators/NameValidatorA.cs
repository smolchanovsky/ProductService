using System.Threading.Tasks;

namespace ProductService.Core.Validation.NameValidators
{
    public class NameValidatorA : INameValidator
    {
        public Task<bool> ValidateAsync(string? name)
        {
            if (name is null)
                return Task.FromResult(false);

            if (name.Length > 200)
                return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}
