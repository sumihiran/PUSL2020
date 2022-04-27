using System.ComponentModel.DataAnnotations;
using PUSL2020.Domain.Validators;

namespace PUSL2020.Application.DataAnnotations;

public class VrnAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var vrn = Convert.ToString(value) ?? "";

        return VrnValidator.IsValid(vrn)
            ? ValidationResult.Success
            : new ValidationResult("Invalid vehicle registration number");
    }
}