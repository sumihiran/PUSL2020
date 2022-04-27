using System.ComponentModel.DataAnnotations;
using PUSL2020.Domain.Validators;

namespace PUSL2020.Application.DataAnnotations;

public class NicAttribute : ValidationAttribute
{
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var nic = Convert.ToString(value) ?? "";
        string errorMessage;
        
        try
        {
            if (NicValidator.IsValid(nic))
            {
                return ValidationResult.Success;
            }

            errorMessage = "Invalid Nic";
        }
        catch (ValidationException e)
        {
            errorMessage = e.Message;
        }
        
        
        return new ValidationResult(errorMessage);
    }
}