using Microsoft.EntityFrameworkCore;
using PUSL2020.Application;
using PUSL2020.Application.Data;
using PUSL2020.Application.Services;
using PUSL2020.Domain.Entities.Institutions;
using PUSL2020.MasterData;

namespace PUSL2020.Web.Data;

[Order(0)]
public class MasterDataSeeder : IApplicationInitializer
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public MasterDataSeeder(IApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }
    
    public async Task InitialiseAsync()
    {
        await SeedRdaOffices();
        await SeedPoliceStations();
        await SeedInsurances();
        
        await _unitOfWork.CompleteAsync();
    }

    private async Task SeedRdaOffices()
    {
        if (await IsDataSetEmpty<RdaOffice>())
        {
            await _dbContext.RdaOffices.AddRangeAsync(MasterDataProvider.GetRdaOffices());
        }
    }

    private async Task SeedPoliceStations()
    {
        if (await IsDataSetEmpty<PoliceStation>())
        {
            await _dbContext.PoliceStations.AddRangeAsync(await MasterDataProvider.GetPoliceStationsAsync());
        }
    }

    private async Task SeedInsurances()
    {
        if (await IsDataSetEmpty<Insurance>())
        {
            await _dbContext.Insurances.AddRangeAsync(await MasterDataProvider.GetInsurancesAsync());
        }
    }

    private async Task<bool> IsDataSetEmpty<T>() where T: class
    {
        return await _dbContext.Set<T>().AnyAsync() == false;
    }
}