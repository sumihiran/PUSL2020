using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Minio;
using PUSL2020.Infrastructure.Data;
using PUSL2020.Web.UI.Tests.Fixtures;
using Xunit;

namespace PUSL2020.Web.UI.Tests;

public class ImageProxyControllerTests : IClassFixture<WebApplicationFactory>
{
    private readonly WebApplicationFactory _factory;
    private readonly MinioClient _minioClient;
    
    private const string Filename = "alex-padurariu-unsplash.jpg";

    public ImageProxyControllerTests(WebApplicationFactory factory)
    {
        _factory = factory;
        _minioClient = factory.Services.GetRequiredService<MinioClient>();
    }
    
    [Fact]
    public async Task Proxy_WhenCalled_ReturnsResponseOk()
    {
        await EnsureImageIsUploaded_IsPresent();
        
        var client = _factory.CreateClient();
        
        var response = await client.GetAsync($"/Resources/Images/pusl/{Filename}");
        response.EnsureSuccessStatusCode();
    }

    private async Task EnsureImageIsUploaded_IsPresent()
    {
        var embedded = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
        var imageFile = embedded.GetFileInfo("Assets/" + Filename);

        if (!imageFile.Exists)
        {
            throw new FileNotFoundException($"Location: {imageFile}");
        }

        var stream = imageFile.CreateReadStream();
        
        var size = stream.Length;
        
        var putObjectArgs = new PutObjectArgs()
            .WithBucket("pusl")
            .WithObject(Filename)
            .WithContentType("image/jpeg")
            .WithObjectSize(size)
            .WithStreamData(stream);

        await _minioClient.PutObjectAsync(putObjectArgs);

    }
}