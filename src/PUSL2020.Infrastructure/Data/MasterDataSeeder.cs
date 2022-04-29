using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PUSL2020.Domain.Entities.Institutions;
using PUSL2020.Domain.Enums;

namespace PUSL2020.Infrastructure.Data;

public class MasterDataSeeder
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<MasterDataSeeder> _logger;
    private readonly IHostEnvironment _env;

    private static CsvConfiguration CsvConfiguration => new(CultureInfo.InvariantCulture)
    {
        NewLine = Environment.NewLine,
        Delimiter = ","
    };

    public MasterDataSeeder(ApplicationDbContext dbContext, ILogger<MasterDataSeeder> logger, IHostEnvironment env)
    {
        _dbContext = dbContext;
        _logger = logger;
        _env = env;
    }

    public async Task SeedPoliceStations()
    {
        var csvFile = Path.Combine(_env.ContentRootPath, "Data", "MasterData", "police_stations.csv");

        if (!File.Exists(csvFile))
        {
            throw new FileNotFoundException($"Location: {csvFile}");
        }

        using var reader = new StreamReader(csvFile);
        using var csv = new CsvReader(reader, CsvConfiguration);
        var records = csv.GetRecordsAsync<PoliceStationRecord>();

        var i = 0;
        await foreach (var record in records)
        {
            _dbContext.PoliceStations.Add(new PoliceStation()
            {
                Area = record.Station,
                Division = record.Division,
                Province = record.Province,
                PhoneNumber = record.Phone
            });
            _logger.LogInformation("[PoliceStation:{i}] Area ={Area}, Division={Division}", ++i, record.Station,
                record.Division);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task SeedInsurances()
    {
        var csvFile = Path.Combine(_env.ContentRootPath, "Data", "MasterData", "insurances.csv");

        if (!File.Exists(csvFile))
        {
            throw new FileNotFoundException($"Location: {csvFile}");
        }

        using var reader = new StreamReader(csvFile);

        using var csv = new CsvReader(reader, CsvConfiguration);
        var records = csv.GetRecordsAsync<InsuranceRecord>();

        var i = 0;
        await foreach (var record in records)
        {
            _dbContext.Insurances.Add(new Insurance()
            {
                Name = record.Name,
                Address = record.Address,
                PhoneNumber = record.Phone
            });
            _logger.LogInformation("[Insurance:{i}] Name ={Name}", ++i, record.Name);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task SeedRdaOffices()
    {
        var districts = Enum.GetNames<District>();
        var i = 0;
        foreach (var district in districts)
        {
            _dbContext.RdaOffices.Add(new RdaOffice()
            {
                District = Enum.Parse<District>(district)
            });
            _logger.LogInformation("[RdaOffice:{i}] District ={District}", ++i, district);
        }
        await _dbContext.SaveChangesAsync();
    }

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