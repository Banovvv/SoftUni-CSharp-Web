using WebServer.HTTP;

namespace WebServer.MvcFramework.Contracts
{
    public interface IMvcApplication
    {
        void ConfigureServices(IServiceCollection serviceCollection);
        void Configure(List<Route> routeTable);
    }
}
