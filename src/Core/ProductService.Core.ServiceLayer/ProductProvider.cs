using System.Threading.Tasks;
using ProductService.Core.DataLayer.Repositories;
using ProductService.Core.Models;
using ProductService.Core.ServiceLayer.Mappers;

namespace ProductService.Core.ServiceLayer
{
    public class ProductProvider : IProductProvider
    {
        private readonly IProductMapper productMapper;
        private readonly IProductRepository productRepository;

        public ProductProvider(
            IProductMapper productMapper,
            IProductRepository productRepository)
        {
            this.productMapper = productMapper;
            this.productRepository = productRepository;
        }

        public async Task<Product?> FindAsync(long id)
        {
            var productDb = await productRepository.FindAsync(id).ConfigureAwait(false);
            if (productDb is null)
                return null;

            return productMapper.Map(productDb);
        }
    }
}
