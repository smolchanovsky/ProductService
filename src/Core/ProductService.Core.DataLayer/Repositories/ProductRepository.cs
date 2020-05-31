using System.Threading.Tasks;
using ProductService.Core.DataLayer.Entities;
using ProductService.Infrastructure.DataLayer.Repositories;

namespace ProductService.Core.DataLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepository<ProductDb, long> repository;

        public ProductRepository(IRepository<ProductDb, long> repository)
        {
            this.repository = repository;
        }

        public async Task<ProductDb?> FindAsync(long id)
        {
            return await repository.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<ProductDb> GetAsync(long id)
        {
            return await repository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task InsertAsync(ProductDb entity)
        {
            await repository.InsertAsync(entity).ConfigureAwait(false);
        }

        public async Task UpdateAsync(ProductDb entity)
        {
            await repository.UpdateAsync(entity).ConfigureAwait(false);
        }

        public async Task SaveAsync(ProductDb entity)
        {
            await repository.SaveAsync(entity).ConfigureAwait(false);
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}
