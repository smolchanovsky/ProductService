using System;
using System.Linq;
using ProductService.Infrastructure.Helpers;

namespace ProductService.Web.Api.Auth
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool TryAuthorize(string authHeader)
        {
            if (authHeader.IsNullOrEmpty())
                return false;

            var headerItems = authHeader.Split(" ").ToArray();
            if (headerItems.Length > 2)
                return false;

            var (typeId, id) = headerItems;
            if (typeId != "ClientId" || !Int64.TryParse(id, out var clientId))
                return false;

            return clientId > 0 && clientId % 2 != 0;
        }
    }
}
