using WebServer.HTTP;
using HttpMethod = WebServer.HTTP.HttpMethod;

namespace WebServer.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(IMvcApplication application, int port)
        {
            List<Route> routeTable = new List<Route>();

            RegisterStaticFiles(routeTable);
            RegisterControllerRoutes(routeTable);

            application.ConfigureServices();
            application.Configure(routeTable);

            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }

        private static void RegisterControllerRoutes(List<Route> routeTable)
        {
            throw new NotImplementedException();
        }

        private static void RegisterStaticFiles(List<Route> routeTable)
        {
            var staticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);

            foreach (var staticFile in staticFiles)
            {
                string url = staticFile
                    .Replace("wwwroot", string.Empty)
                    .Replace("\\", "/");

                routeTable.Add(new Route(url, HttpMethod.Get, (request) =>
                {
                    var fileContent = File.ReadAllBytes(staticFile);
                    var fileExtension = new FileInfo(staticFile).Extension;
                    var contentType = string.Empty;

                    switch (fileExtension)
                    {
                        case ".txt": contentType = "text/plain"; break;
                        case ".js": contentType = "text/javascript"; break;
                        case ".css": contentType = "text/css"; break;
                        case ".jpg": contentType = "image/jpeg"; break;
                        case ".jpeg": contentType = "image/jpeg"; break;
                        case ".png": contentType = "text/plain"; break;
                        case ".gif": contentType = "image/gif"; break;
                        case ".ico": contentType = "image/vnd.microsoft.icon"; break;
                        case ".html": contentType = "text/html"; break;
                        default: contentType = "text/plain"; break;
                    }

                    return new HttpResponse(contentType, fileContent, HttpStatusCode.OK);
                }));
            }
        }
    }
}
