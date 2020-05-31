using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductService.Core.Models;
using ProductService.Core.ServiceLayer;
using ProductService.Core.Validation;
using ProductService.Web.Api.Models;

namespace ProductService.Web.Api.Controllers
{
	[ApiController]
    [ApiVersion("1")]
	[Route("api/v{version:apiVersion}/products")]
	public class ProductController : ControllerBase
	{
        private readonly IProductSaver productSaver;
        private readonly IProductProvider productProvider;
        private readonly IProductValidator productValidator;
        private readonly ILogger<ProductController> logger;

		public ProductController(
            IProductSaver productSaver,
            IProductProvider productProvider,
            IProductValidator productValidator,
            ILogger<ProductController> logger)
        {
            this.productSaver = productSaver;
            this.productProvider = productProvider;
            this.productValidator = productValidator;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<Result<Product>> Get([Range(1, long.MaxValue)] long id)
        {
            logger.LogInformation("Request product with Id {id}", id);
            var product = await productProvider.FindAsync(id).ConfigureAwait(false);
            if (product is null)
            {
                logger.LogInformation("Product with id {id} not found", id);
                return Result<Product>.Failure(HttpStatusCode.NotFound, message: "Product not found.");
            }

            logger.LogInformation("Product with id {id} found", id);
            return Result<Product>.Success(HttpStatusCode.OK, product);
        }

        [HttpPost]
        public async Task<Result> Get([Required, FromBody] Product product)
        {
            logger.LogInformation("Request to save new product");
            var productIsValid = await productValidator.ValidateAsync(product).ConfigureAwait(false);
            if (!productIsValid)
            {
                logger.LogInformation("Product not saved: invalid product");
                return Result.Failure(HttpStatusCode.BadRequest, message: "Validation failed.");
            }

            await productSaver.AddAsync(product).ConfigureAwait(false);
            logger.LogInformation("Product saved");
            return Result.Success(HttpStatusCode.Created);
        }
	}
}
