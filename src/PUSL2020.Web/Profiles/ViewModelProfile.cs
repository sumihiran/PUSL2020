using AutoMapper;
using PUSL2020.Web.Areas.Identity.Pages.Account;

namespace PUSL2020.Web.Profiles;

public class ViewModelProfile : Profile
{
    public ViewModelProfile()
    {
        CreateMap<RegisterModel.Address, Domain.ValueObjects.Address>();
    }
}