using AutoMapper;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Vehicles;

namespace PUSL2020.Application.Mappers;

public class EntityProfile : Profile
{
    public EntityProfile()
    {
        CreateMap<Vehicle, VehicleSnapshot>();
        CreateMap<PersonReporter, VehicleOwner>()
            .ForMember(vo => vo.Address, opt => opt.MapFrom(r => r.Address.Clone()))
            .ForMember(vo => vo.Name, opt => opt.MapFrom(r => r.Name.ToString()))
            .ForMember(vo => vo.Phone, opt => opt.MapFrom(r => r.PhoneNumber));
    }
}