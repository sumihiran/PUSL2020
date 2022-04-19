#pragma warning disable CS8618
namespace PUSL2020.Domain.ValueObjects;

public class Address
{
    public Address()
    {
    }

    public Address(string line1, string line2, string street, string city, string district, int zipCode)
    {
        Line1 = line1;
        Line2 = line2;
        Street = street;
        City = city;
        District = district;
        ZipCode = zipCode;
    }

    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public int ZipCode { get; set; }
}