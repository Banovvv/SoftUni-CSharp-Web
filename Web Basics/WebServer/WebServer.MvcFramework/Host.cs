using WebServer.HTTP;

namespace WebServer.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(List<Route> routeTable, int port)
        {
            IHttpServer server = new HttpServer();

            foreach (var route in routeTable)
            {
                server.AddRoute(route.Path, route.Action);
            }

            await server.StartAsync(port);
        }
    }
}
