// using System.Threading.Tasks;
// using Microsoft.Extensions.Logging;
// using TestContainers.Container.Abstractions;
// using TestContainers.Container.Abstractions.Hosting;
// using Xunit;
//
// namespace PUSL2020.Infrastructure.Tests.Fixtures;
//
// public class MinIoContainerFixture : IAsyncLifetime
// {
//     public GenericContainer Container { get; }
//
//     public MinIoContainerFixture()
//     {
//         Container = new ContainerBuilder<GenericContainer>()
//             .ConfigureDockerImageName("quay.io/minio/minio:RELEASE.2022-04-16T04-26-02Z")
//             .ConfigureContainer((ctx, container) =>
//             {
//                 container.PortBindings.Add(9000, 9000);
//                 container.Command.Add("server /data");
//             })
//             .ConfigureLogging(builder =>
//             {
//                 builder.AddConsole();
//                 builder.SetMinimumLevel(LogLevel.Debug);
//             })
//             .Build();
//         
//     }
//     
//     public Task InitializeAsync()
//     {
//         return Container.StartAsync();
//     }
//
//     public Task DisposeAsync()
//     {
//         return Container.StopAsync();
//     }
// }