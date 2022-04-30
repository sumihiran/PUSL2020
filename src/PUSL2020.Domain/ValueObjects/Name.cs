namespace PUSL2020.Domain.ValueObjects;

public class Name
{
    public string First { get; set; }
    public string? Middle { get; set; }
    public string Last { get; set; }

    public override string ToString()
    {
        return $"{Last}, {First} {Middle}";
    }
}