
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Data.Impl;

public class ReporterRepository : BaseRepository<Reporter, ReporterId>, IReporterRepository
{
    public ReporterRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }
}