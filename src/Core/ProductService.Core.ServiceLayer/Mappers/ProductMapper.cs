using AutoMapper;
using ProductService.Core.DataLayer.Entities;
using ProductService.Core.Models;

namespace ProductService.Core.ServiceLayer.Mappers
{
    public class ProductMapper : IProductMapper
    {
        private readonly IMapper mapper;

        public ProductMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDb>();
                cfg.CreateMap<ProductDb, Product>();
            });
            configuration.AssertConfigurationIsValid();

            mapper = configuration.CreateMapper();
        }

        public ProductDb Map(Product product)
        {
            return mapper.Map<ProductDb>(product);
        }

        public Product Map(ProductDb productDb)
        {
            return mapper.Map<Product>(productDb);
        }
    }
}
