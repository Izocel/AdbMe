using Funq;
using ServiceStack;
using Acme.ServiceInterface;

[assembly: HostingStartup(typeof(Acme.AppHost))]

namespace Acme;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("Acme", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig {
        });
    }
}
