using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Infrastructure.DataLayer.Sql
{
	public class SqlRepository<TEntity, TId> : ISqlRepository<TEntity, TId> where TEntity : class, IEntity<TId>
	{
		private readonly DbContext context;

		public SqlRepository(DbContext context)
		{
			this.context = context;
		}

        public async Task<TEntity?> FindAsync(TId id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public Task<TEntity> GetAsync(TId id)
		{
            throw new System.NotImplementedException();
		}

		public async Task InsertAsync(TEntity entity)
		{
			await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
		}

		public Task UpdateAsync(TEntity entity)
		{
			throw new System.NotImplementedException();
		}

		public Task SaveAsync(TEntity entity)
		{
			throw new System.NotImplementedException();
		}

		public Task DeleteAsync(TId id)
		{
			throw new System.NotImplementedException();
		}
	}
}
