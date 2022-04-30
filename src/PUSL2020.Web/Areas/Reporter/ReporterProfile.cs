using AutoMapper;
using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Web.Areas.Reporter.Pages.Vehicles;

namespace PUSL2020.Web.Areas.Reporter;

public class ReporterProfile : Profile
{
    public ReporterProfile()
    {
        CreateMap<Vehicle, IndexModel.VehicleModel>();
        CreateMap<Vehicle, EditModel.InputModel>()
            .ForMember(m => m.InsurancePolicyId, o => o.MapFrom(x => x.Insurance.PolicyId))
            .ForMember(m => m.InsuranceExpiryAt, o => o.MapFrom(x => x.Insurance.ExpiryAt.ToDateTime(TimeOnly.Parse("00:00 PM"))))
            .ForMember(m => m.InsuranceStartAt, o => o.MapFrom(x => x.Insurance.StartAt.ToDateTime(TimeOnly.Parse("00:00 PM"))));
    }
}