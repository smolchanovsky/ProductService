namespace ProductService.Web.Api.Auth
{
    public interface IAuthorizationService
    {
        bool TryAuthorize(string authHeader);
    }
}
