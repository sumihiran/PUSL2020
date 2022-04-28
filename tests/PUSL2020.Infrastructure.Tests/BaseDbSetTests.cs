using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PUSL2020.Infrastructure.Data;
using PUSL2020.Infrastructure.Tests.Fixtures;
using Xunit;

namespace PUSL2020.Infrastructure.Tests;

public abstract class BaseDbSetTests :  IClassFixture<InMemoryApplicationDbContextFixture>, IAsyncLifetime
{
    private readonly InMemoryApplicationDbContextFixture _fixture;

    public BaseDbSetTests(InMemoryApplicationDbContextFixture fixture)
    {
        _fixture = fixture;
    }
    
    protected IServiceScope? Scope { get; set; }
    protected IServiceProvider Provider { get; set; }
    
    protected ApplicationDbContext DbContext { get; set; }


    public Task InitializeAsync()
    {
        Scope = _fixture.Host.Services.CreateScope();
        Provider = Scope.ServiceProvider;
        DbContext = Provider.GetRequiredService<ApplicationDbContext>();
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        Scope?.Dispose();
        return Task.CompletedTask;
    }
}