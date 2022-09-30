using WebServer.HTTP;

namespace WebServer.MvcFramework
{
    public interface IMvcApplication
    {
        void ConfigureServices();
        void Configure(List<Route> routeTable);
    }
}
