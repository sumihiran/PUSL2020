using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Identity;

public class ReporterUserOnlyStore<TContext> : UserOnlyStore<ReporterUser, TContext, ReporterId>
    where TContext: DbContext
{
    public ReporterUserOnlyStore(TContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }

    public override Task<ReporterUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = new CancellationToken())
    {
        return base.FindByEmailAsync(normalizedUserName, cancellationToken);
    }
}