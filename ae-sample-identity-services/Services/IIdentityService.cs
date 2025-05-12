using Ae.Sample.Identity.Authentication;

namespace Ae.Sample.Identity.Services
{
    public interface IIdentityService
    {
        Task<ClientCredentialsResult> TryVerifyClientCredentialAsync(string email, string password, CancellationToken ct = default);
    }
}
