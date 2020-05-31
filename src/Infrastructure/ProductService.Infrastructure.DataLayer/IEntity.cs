namespace ProductService.Infrastructure.DataLayer
{
	public interface IEntity<T>
	{
		public T Id { get; set; }
	}
}