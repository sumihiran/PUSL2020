using System.ComponentModel.DataAnnotations;
using PUSL2020.Domain.Validators;

namespace PUSL2020.Application.DataAnnotations;

public class PhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var phoneNumber = Convert.ToString(value) ?? "";

        return PhoneNumberValidator.IsValid(phoneNumber)
            ? ValidationResult.Success
            : new ValidationResult("Invalid phone number");
    }
}