using PUSL2020.Application.Services;
using PUSL2020.Infrastructure.Data;

namespace PUSL2020.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork, IDisposable 
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        // _context.Dispose();
    }
}