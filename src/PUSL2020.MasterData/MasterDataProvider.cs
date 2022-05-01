using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.FileProviders;
using PUSL2020.Domain.Entities.Institutions;
using PUSL2020.Domain.Enums;

namespace PUSL2020.MasterData;

public static class MasterDataProvider
{
    private static readonly EmbeddedFileProvider FileProvider = new(Assembly.GetExecutingAssembly());

    private static CsvConfiguration CsvConfiguration => new(CultureInfo.InvariantCulture)
    {
        NewLine = Environment.NewLine,
        Delimiter = ","
    };

    public static async Task<IEnumerable<PoliceStation>> GetPoliceStationsAsync()
    {
        var csvFile = FileProvider.GetFileInfo("Csv/police_stations.csv");
        using var reader = new StreamReader(csvFile.CreateReadStream());
        using var csv = new CsvReader(reader, CsvConfiguration);

        var stations = new List<PoliceStation>();
        await foreach (var record in csv.GetRecordsAsync<PoliceStationRecord>())
        {
            stations.Add(new PoliceStation()
            {
                Area = record.Station,
                Division = record.Division,
                Province = record.Province,
                PhoneNumber = record.Phone
            });
        }

        return stations;
    }

    public static async Task<IEnumerable<Insurance>> GetInsurancesAsync()
    {
        var csvFile = FileProvider.GetFileInfo("Csv/insurances.csv");
        using var reader = new StreamReader(csvFile.CreateReadStream());
        using var csv = new CsvReader(reader, CsvConfiguration);
        
        var insurances = new List<Insurance>();
        await foreach (var record in csv.GetRecordsAsync<InsuranceRecord>())
        {
            insurances.Add(new Insurance()
            {
                Name = record.Name,
                Address = record.Address,
                PhoneNumber = record.Phone
            });
        }

        return insurances;
    }

    public static IEnumerable<RdaOffice> GetRdaOffices()
        => Enum.GetNames<District>()
            .Select(district => new RdaOffice()
            {
                District = Enum.Parse<District>(district)
            })
            .ToList();


    private class PoliceStationRecord
    {
        public string Province { get; set; }
        public string Division { get; set; }
        public string Station { get; set; }
        public string? Phone { get; set; }
    }

    private class InsuranceRecord
    {
        [Name("Insurance")] public string Name { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
    }
}