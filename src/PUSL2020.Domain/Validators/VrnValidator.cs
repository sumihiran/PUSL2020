using System.Text.RegularExpressions;

namespace PUSL2020.Domain.Validators;

public class VrnValidator
{
    // https://stackoverflow.com/questions/28602745/i-need-a-regex-expression-for-validating-sri-lankan-vehicle-number
    private const string Pattern = @"^(?>[a-zA-Z]{1,3}|(?!0*-)[0-9]{1,3})-[0-9]{4}(?<!0{4})";
    
    private static readonly Regex Regex = new (Pattern, RegexOptions.Compiled);
    
    public static bool IsValid(string vrn) => Regex.IsMatch(vrn);
}