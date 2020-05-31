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
    public class PostProductTest
    {
        [Test, Explicit]
        public void PostProduct_ValidProduct_Ok()
        {
            var product = new Product { Name = "name", Description = "desc" };
            var client = new RestClient("https://localhost:5001");
            var request = new RestRequest("api/v1/products", DataFormat.Json)
                .AddHeader("Authorization", "ClientId 1")
                .AddJsonBody(product);

            using (var scope = new AssertionScope())
            {
                var response = client
                    .Invoking(x => x.Post<Result>(request))
                    .Should()
                    .NotThrow()
                    .Subject;

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Data.StatusCode.Should().Be(HttpStatusCode.Created);
            }
        }
    }
}
