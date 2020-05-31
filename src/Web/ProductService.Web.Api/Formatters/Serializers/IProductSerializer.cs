using ProductService.Core.Models;

namespace ProductService.Web.Api.Formatters.Serializers
{
    public interface IProductSerializer
    {
        Product? TryDeserialize(string input);
    }
}
