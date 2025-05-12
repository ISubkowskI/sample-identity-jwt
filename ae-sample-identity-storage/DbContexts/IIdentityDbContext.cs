using Microsoft.EntityFrameworkCore;
using Ae.Sample.Identity.DbEntities;

namespace Ae.Sample.Identity.DbContexts
{
    public interface IIdentityDbContext
    {
        DbSet<DbAccountIdentity> AccountIdentities { get; set; }

        DbSet<DbRefreshToken> RefreshTokens { get; set; }

        DbSet<DbAppClaim> AppClaims { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
