using WebServer.HTTP;
using WebServer.MvcFramework.Attributes;
using WebServer.MvcFramework.Contracts;
using HttpMethod = WebServer.HTTP.HttpMethod;

namespace WebServer.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(IMvcApplication application, int port)
        {
            List<Route> routeTable = new List<Route>();
            IServiceCollection services = new ServiceCollection();

            RegisterStaticFiles(routeTable);

            application.ConfigureServices(services);
            application.Configure(routeTable);

            RegisterControllerRoutes(routeTable, application, services);

            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }

        private static void RegisterControllerRoutes(List<Route> routeTable, IMvcApplication application, IServiceCollection services)
        {
            var controllerTypes = application.GetType().Assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Controller)));

            foreach (var controllerType in controllerTypes)
            {
                var methods = controllerType.GetMethods()
                    .Where(x => x.IsPublic && !x.IsStatic && !x.IsAbstract && !x.IsConstructor
                            && !x.IsSpecialName && x.DeclaringType == controllerType);

                foreach (var method in methods)
                {
                    string url = $"/{controllerType.Name.Replace("Controller", string.Empty)}/{method.Name}";

                    var attribute = method.GetCustomAttributes(false)
                        .Where(x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute)))
                        .FirstOrDefault() as BaseHttpAttribute;

                    var httpMethod = HttpMethod.GET;

                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (!string.IsNullOrEmpty(attribute?.Url))
                    {
                        url = attribute.Url;
                    }

                    routeTable.Add(new Route(url, httpMethod, (request) =>
                    {
                        var instance = services.CreateInstance(controllerType) as Controller;
                        instance.Request = request;
                        var response = method.Invoke(instance, new object[] { }) as HttpResponse;

                        return response;
                    }));
                }
            }
        }

        private static void RegisterStaticFiles(List<Route> routeTable)
        {
            var staticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);

            foreach (var staticFile in staticFiles)
            {
                string url = staticFile
                    .Replace("wwwroot", string.Empty)
                    .Replace("\\", "/");

                routeTable.Add(new Route(url, HttpMethod.GET, (request) =>
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
