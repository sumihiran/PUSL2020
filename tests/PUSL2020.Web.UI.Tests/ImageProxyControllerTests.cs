using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PUSL2020.Web.UI.Tests.Fixtures;
using Xunit;

namespace PUSL2020.Web.UI.Tests;

public class ImageProxyControllerTests : IClassFixture<InMemoryWebApplicationFactory>
{
    private readonly InMemoryWebApplicationFactory _factory;
    private readonly MinioClient _minioClient;
    
    private const string Filename = "alex-padurariu-unsplash.jpg";

    public ImageProxyControllerTests(InMemoryWebApplicationFactory factory)
    {
        _factory = factory;
        _minioClient = factory.Services.GetRequiredService<MinioClient>();
    }
    
    [Fact]
    public async Task Proxy_WhenCalled_ReturnsResponseOk()
    {
        await EnsureImageIsPresent();
        
        var client = _factory.CreateClient();
        
        var response = await client.GetAsync($"/Resources/Images/pusl/{Filename}");
        response.EnsureSuccessStatusCode();
    }

    private async Task EnsureImageIsPresent()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets",Filename);
        await using var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None,
            bufferSize: 4096, useAsync: true);

        var size = sourceStream.Length;
        
        var putObjectArgs = new PutObjectArgs()
            .WithBucket("pusl")
            .WithObject(Filename)
            .WithContentType("image/jpeg")
            .WithObjectSize(size)
            .WithStreamData(sourceStream);

        await _minioClient.PutObjectAsync(putObjectArgs);

    }
}