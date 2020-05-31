using System;
using System.Collections.Generic;
using System.IO;
using ProductService.Core.Models;

namespace ProductService.Web.Api.Formatters.Serializers
{
    public class ProductSerializer : IProductSerializer
    {
        public Product? TryDeserialize(string input)
        {
            using var stringReader = new StringReader(input);

            var product = new Product();
            while (stringReader.Peek() != -1)
            {
                var filedName = TryGetFieldName(stringReader);
                if (filedName is null)
                    return null;

                var fieldInitializer = TryGetFiledInitializer(filedName);
                if (fieldInitializer is null)
                    return null;

                var filedValue = GetFieldValue(stringReader);
                fieldInitializer.Invoke(product, filedValue);
            }

            return product;
        }

        private static Action<Product, string>? TryGetFiledInitializer(string fieldName)
        {
            return fieldName switch
            {
                nameof(Product.Name) => (product, value) => product.Name = value,
                nameof(Product.Description) => (product, value) => product.Description = value,
                _ => null
            };
        }

        private static string? TryGetFieldName(TextReader streamReader)
        {
            var result = GetUntil(streamReader, '=');
            return String.IsNullOrEmpty(result) ? null : result;
        }

        private static string GetFieldValue(TextReader streamReader)
        {
            return GetUntil(streamReader, '~', stopCharShouldExist: false)!;
        }

        private static string? GetUntil(TextReader streamReader, char stopChar, bool stopCharShouldExist = true)
        {
            var buffer = new List<char>();

            while (streamReader.Peek() != -1)
            {
                var currentChar = (char) streamReader.Read();
                if (currentChar == stopChar)
                    return new String(buffer.ToArray());

                buffer.Add(currentChar);
            }

            return stopCharShouldExist ? null : new String(buffer.ToArray());
        }
    }
}
