using System.Threading.Tasks;
using ProductService.Core.DataLayer.Repositories;
using ProductService.Core.Models;
using ProductService.Core.ServiceLayer.Mappers;

namespace ProductService.Core.ServiceLayer
{
    public class ProductSaver : IProductSaver
    {
        private readonly IProductMapper productMapper;
        private readonly IProductRepository productRepository;

        public ProductSaver(
            IProductMapper productMapper,
            IProductRepository productRepository)
        {
            this.productMapper = productMapper;
            this.productRepository = productRepository;
        }

        public async Task AddAsync(Product product)
        {
            var productDb = productMapper.Map(product);
            await productRepository.InsertAsync(productDb).ConfigureAwait(false);
        }
    }
}
