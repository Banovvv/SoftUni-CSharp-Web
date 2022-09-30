using WebServer.HTTP;

namespace WebServer.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(IMvcApplication application, int port)
        {
            List<Route> routeTable = new List<Route>();

            application.ConfigureServices();
            application.Configure(routeTable);

            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }
    }
}
