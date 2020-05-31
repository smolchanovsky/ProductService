using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using ProductService.Core.Models;
using ProductService.Web.Api.Models;
using RestSharp;

namespace ProductService.Web.Api.Tests.FunctionalTests
{
    [TestFixture, Explicit]
    public class GetProductTest
    {
        [Test, Explicit]
        public void GetProduct_ExistingId_Product()
        {
            var client = new RestClient("https://localhost:5001");
            var request = new RestRequest("api/v1/products", DataFormat.Json)
                .AddHeader("Authorization", "ClientId 1");

            using (var scope = new AssertionScope())
            {
                var response = client
                    .Invoking(x => x.Get<Result<Product>>(request))
                    .Should()
                    .NotThrow()
                    .Subject;

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Data.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}
