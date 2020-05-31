using System.Collections;
using FluentAssertions;
using NUnit.Framework;
using ProductService.Core.Models;
using ProductService.Web.Api.Formatters.Serializers;

namespace ProductService.Web.Api.Tests
{
    [TestFixture]
    public class ProductSerializerTest
    {
        private IProductSerializer productSerializer;

        public static IEnumerable ValidCases
        {
            get
            {
                yield return new TestCaseData("", new Product { Name = null, Description = null })
                    .SetName("Empty string");
                yield return new TestCaseData("Name=", new Product { Name = "", Description = null })
                    .SetName("There is no Description");
                yield return new TestCaseData("Description=", new Product { Name = null, Description = "" })
                    .SetName("There is no Name");
                yield return new TestCaseData("Name=~Description=", new Product { Name = "", Description = "" })
                    .SetName("Name and Description are empty strings");
                yield return new TestCaseData("Name=~Description=desc", new Product { Name = "", Description = "desc" })
                    .SetName("Name is empty string");
                yield return new TestCaseData("Name=name~Description=", new Product { Name = "name", Description = "" })
                    .SetName("Description is empty string");
                yield return new TestCaseData("Name=name~Description=desc", new Product { Name = "name", Description = "desc" })
                    .SetName("There are Name amd Description");
                yield return new TestCaseData("Name=123~Description=123", new Product { Name = "123", Description = "123" })
                    .SetName("Name and Description are numbers");
                yield return new TestCaseData("Name=Description=", new Product { Name = "Description=", Description = null })
                    .SetName("Missing tilde sign");
            }
        }

        public static IEnumerable InvalidCases
        {
            get
            {
                yield return new TestCaseData("name=name~Description=desc")
                    .SetName("Lowercase field name");
                yield return new TestCaseData("NAME=name~Description=desc")
                    .SetName("Uppercase field name");
                yield return new TestCaseData("Field=name~Description=desc")
                    .SetName("Invalid field name for Name");
                yield return new TestCaseData("Name=name~Field=desc")
                    .SetName("Invalid field name for Description");
                yield return new TestCaseData("=~=")
                    .SetName("There are no field names");
                yield return new TestCaseData("Name~Description=")
                    .SetName("Missing equal sign");
                yield return new TestCaseData("Name")
                    .SetName("Single filed name");
            }
        }

        [SetUp]
        public void SetUp()
        {
            productSerializer = new ProductSerializer();
        }

        [TestCaseSource(nameof(ValidCases))]
        public void TryDeserialize_ValidCase_Product(string input, Product expectedResult)
        {
            var actualResult = productSerializer.TryDeserialize(input);

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestCaseSource(nameof(InvalidCases))]
        public void TryDeserialize_InvalidCase_Failure(string input)
        {
            var actualResult = productSerializer.TryDeserialize(input);

            actualResult.Should().BeEquivalentTo<Product>(null);
        }
    }
}
