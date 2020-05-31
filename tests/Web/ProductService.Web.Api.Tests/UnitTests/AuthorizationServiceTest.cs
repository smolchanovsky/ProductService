using System;
using System.Collections;
using FluentAssertions;
using NUnit.Framework;
using ProductService.Web.Api.Auth;

namespace ProductService.Web.Api.Tests
{
    public class AuthorizationServiceTest
    {
        private IAuthorizationService authorizationService;

        public static IEnumerable ValidCases
        {
            get
            {
                yield return new TestCaseData("ClientId 9")
                    .SetName("Client with an odd one-digit id");
                yield return new TestCaseData("ClientId 1342721")
                    .SetName("Client with an odd id");
                yield return new TestCaseData("ClientId 1")
                    .SetName("Client with min id");
                yield return new TestCaseData($"ClientId {Int64.MaxValue}")
                    .SetName("Client with max id");
            }
        }

        public static IEnumerable InvalidCases
        {
            get
            {
                yield return new TestCaseData("")
                    .SetName("Empty string");
                yield return new TestCaseData("123")
                    .SetName("Only id");
                yield return new TestCaseData("ClientId")
                    .SetName("Client without id");
                yield return new TestCaseData("ClientId -1")
                    .SetName("Client with negative id");
                yield return new TestCaseData("Client 1")
                    .SetName("Client with invalid type id");
                yield return new TestCaseData("Client a1d2")
                    .SetName("Client with non-numeric id");
                yield return new TestCaseData("ClientId 1342722")
                    .SetName("Client with an even digit id");
            }
        }

        [SetUp]
        public void SetUp()
        {
            authorizationService = new AuthorizationService();
        }

        [TestCaseSource(nameof(ValidCases))]
        public void TryAuthorize_ValidCase_SuccessfulAuthorization(string clientId)
        {
            var actualResult = authorizationService.TryAuthorize(clientId);

            actualResult.Should().Be(true);
        }

        [TestCaseSource(nameof(InvalidCases))]
        public void TryAuthorize_InvalidCase_FailureAuthorization(string clientId)
        {
            var actualResult = authorizationService.TryAuthorize(clientId);

            actualResult.Should().Be(false);
        }
    }
}
