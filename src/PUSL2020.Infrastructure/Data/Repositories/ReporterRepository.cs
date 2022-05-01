using PUSL2020.Application.Data;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data.Repositories;

public class ReporterRepository : BaseRepository<Reporter, ReporterId>, IReporterRepository
{
    public ReporterRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override Task AddAsync(Reporter entity)
    {
        throw new NotSupportedException("Use UserManager<ReporterUser> instead");
    }

    public override Task DeleteAsync(Reporter entity)
    {
        throw new NotSupportedException("Use UserManager<ReporterUser> instead");
    }

    public override Task UpdateAsync(Reporter entity)
    {
        throw new NotSupportedException("Use UserManager<ReporterUser> instead");
    }
}