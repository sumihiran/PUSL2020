using PUSL2020.Domain.Enums;

namespace PUSL2020.Application.Dtos;

public record VehicleAccidentCountByClassAndYearDto(VehicleClass VehicleClass, int Year, long Quantity);

public record VehicleAccidentPercentageByClassAndYearDto(VehicleClass VehicleClass, int Year, float Percentage);