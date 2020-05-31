﻿using System.Threading.Tasks;

 namespace ProductService.Infrastructure.DataLayer.Repositories
{
	public interface IReadOnlyRepository<TEntity, TId> where TEntity : class, IEntity<TId>
	{
        Task<TEntity?> FindAsync(TId id);
		Task<TEntity> GetAsync(TId id);
	}
}
