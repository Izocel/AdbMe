using Funq;
using ServiceStack;
using AdbMe.ServiceInterface;

[assembly: HostingStartup(typeof(AdbMe.AppHost))]

namespace AdbMe;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("AdbMe", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig {
        });
    }
}
