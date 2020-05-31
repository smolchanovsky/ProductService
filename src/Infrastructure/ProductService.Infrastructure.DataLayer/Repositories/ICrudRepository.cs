﻿using System.Threading.Tasks;

 namespace ProductService.Infrastructure.DataLayer.Repositories
{
	public interface ICrudRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId>  where TEntity : class, IEntity<TId>
	{
		Task InsertAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task SaveAsync(TEntity entity);
		Task DeleteAsync(TId id);
	}
}