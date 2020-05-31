﻿namespace ProductService.Infrastructure.DataLayer.Repositories
{
	public interface IRepository<TEntity, TId> : ICrudRepository<TEntity, TId> where TEntity : class, IEntity<TId>
	{
	}
}