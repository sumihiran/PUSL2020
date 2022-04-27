using AspNetCore.Proxy;
using AspNetCore.Proxy.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio.AspNetCore;

namespace PUSL2020.Web.Controllers;

[Controller]
public class ImageProxyController : Controller
{
    private readonly MinioOptions _minioOptions;
    private readonly HttpProxyOptions _httpOptions = HttpProxyOptionsBuilder.Instance
        .WithShouldAddForwardedHeaders(false)
        .WithBeforeSend(BeforeSend)
        .WithHandleFailure(OnFailure).Build();

    public ImageProxyController(IOptions<MinioOptions> minioOptions)
    {
        _minioOptions = minioOptions.Value;
    }

    private static async Task OnFailure(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.StatusCode = 403;
        await httpContext.Response.WriteAsync("Error occured.");
    }

    private static Task BeforeSend(HttpContext httpContext, HttpRequestMessage httpRequestMessage)
    {
        
        return Task.CompletedTask;
    }

    [HttpGet]
    [Route("/Resources/Images/{**path}")]
    public Task Invoke(string path)
    {
        var endpoint = new UriBuilder($"http://{_minioOptions.Endpoint}")
        {
            Path = path
        };
        return this.HttpProxyAsync(endpoint.ToString(),  _httpOptions);
    }
}