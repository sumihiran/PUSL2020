using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PUSL2020.Domain.Validators;

public class NicValidator
{
    private const string Pattern = @"^(?:19|20)?\d{2}[0-9]{10}|[0-9]{9}[x|X|v|V]$";
    
    private static readonly Regex Regex = new (Pattern, RegexOptions.Compiled);
    
    public static bool IsValid(string nic)
    {
        if (nic.Length is 10 or 12)
        {
            return Regex.IsMatch(nic);
        }
        
        throw new ValidationException("Nic must contain either 10 or 12 characters");
    }
}