using System.Security.Claims;

namespace Ae.Sample.Identity.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateAccessToken(ClaimsIdentity claimsIdentity);
    }
}
