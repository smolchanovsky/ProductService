using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using ProductService.Web.Api.Formatters.Serializers;

namespace ProductService.Web.Api.Formatters
{
    public class ProductInputFormatter : TextInputFormatter
    {
        private readonly IProductSerializer productSerializer;

        public ProductInputFormatter(IProductSerializer productSerializer)
        {
            this.productSerializer = productSerializer;

            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/product"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            using var streamReader = new StreamReader(context.HttpContext.Request.Body, leaveOpen: true);
            var textBody = await streamReader.ReadToEndAsync().ConfigureAwait(false);

            var product = productSerializer.TryDeserialize(textBody);
            if (product is null)
                InputFormatterResult.Failure();

            return InputFormatterResult.Success(product);
        }
    }
}
