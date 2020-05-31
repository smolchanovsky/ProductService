using ProductService.Infrastructure.DataLayer.Repositories;

namespace ProductService.Infrastructure.DataLayer.Sql
{
    public interface ISqlRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {

    }
}
