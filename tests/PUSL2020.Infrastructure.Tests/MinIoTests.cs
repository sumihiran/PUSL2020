using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PUSL2020.Infrastructure.Tests.Fixtures;
using Xunit;

namespace PUSL2020.Infrastructure.Tests;

public class MinIoTests : IClassFixture<MinIoFixture>
{
    private MinioClient Client { get; }

    public MinIoTests(MinIoFixture fixture)
    {
        Client = fixture.Host.Services.GetRequiredService<MinioClient>();
    }

    [Fact]
    public async Task Upload_BucketExists_ReturnTrue()
    {
        var args = new BucketExistsArgs();
        args.WithBucket("pusl");

        var exists = await Client.BucketExistsAsync(args);
        Assert.True(exists, "Bucket does not exists");
    }

    [Fact]
    public async Task Upload_AddFileToBucket_ReturnOk()
    {
        const string filename = "alex-padurariu-unsplash.jpg";
        
        #region cleanup
        try
        {
            await Client.RemoveObjectAsync(new RemoveObjectArgs().WithBucket("pusl").WithObject(filename));
        }
        catch (FileNotFoundException)
        {
            // Do nothing
        }
        #endregion
        
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "alex-padurariu-unsplash.jpg");
        await using var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None,
                bufferSize: 4096, useAsync: true);

        var size = sourceStream.Length;
        
        var putObjectArgs = new PutObjectArgs()
            .WithBucket("pusl")
            .WithObject(filename)
            .WithContentType("image/jpeg")
            .WithObjectSize(size)
            .WithStreamData(sourceStream);

        await Client.PutObjectAsync(putObjectArgs);
        
        var getArgs = new StatObjectArgs()
            .WithBucket("pusl")
            .WithObject(filename);
        
        var result = await Client.StatObjectAsync(getArgs);
        
        Assert.Equal(filename, result.ObjectName);
        Assert.Equal(size, result.Size);
    }
}