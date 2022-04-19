using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PUSL2020.Infrastructure.Identity;

public class UserOnlyStore<TUser, TContext, TKey> : Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserOnlyStore<TUser, TContext, TKey>
    where TUser : IdentityUser<TKey>
    where TContext : DbContext
    where TKey : IEquatable<TKey>
{
    public UserOnlyStore(TContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }


    public override Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken = new())
    {
        ThrowIfDisposed();
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return Task.FromResult((IList<Claim>)new List<Claim>());
    }
}