using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.Core.Models;
using ProductService.Core.Validation.DescriptionValidators;
using ProductService.Core.Validation.NameValidators;

namespace ProductService.Core.Validation
{
    public class ProductValidator : IProductValidator
    {
        private readonly IEnumerable<INameValidator> nameValidators;
        private readonly IEnumerable<IDescriptionValidator> descriptionValidators;

        public ProductValidator(
            IEnumerable<INameValidator> nameValidators,
            IEnumerable<IDescriptionValidator> descriptionValidators)
        {
            this.nameValidators = nameValidators;
            this.descriptionValidators = descriptionValidators;
        }

        public async Task<bool> ValidateAsync(Product product)
        {
            var nameValidationTasks = nameValidators.Select(x => x.ValidateAsync(product.Name));
            var nameValidationResults = await Task.WhenAll(nameValidationTasks).ConfigureAwait(false);
            if (nameValidationResults.Any(x => x == false))
                return false;

            var descriptionValidationTasks = descriptionValidators.Select(x => x.ValidateAsync(product.Description));
            var descriptionValidationResults = await Task.WhenAll(descriptionValidationTasks).ConfigureAwait(false);
            if (descriptionValidationResults.Any(x => x == false))
                return false;

            return true;
        }
    }
}
