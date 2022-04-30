using StronglyTypedIds;

namespace PUSL2020.Domain.ValueObjects;

[StronglyTypedId]
public partial struct ReporterId
{
    public static ReporterId FromGuid(string guid)
    {
        return new ReporterId(Guid.Parse(guid));
    }
}