using System.Text;
using WebServer.HTTP;

namespace MvcFrameworkApp
{
    class Program
    {
        static async Task Main()
        {
            IHttpServer server = new HttpServer();

            server.AddRoute("/", HomePage);
            server.AddRoute("/about", About);
            server.AddRoute("/favicon.ico", Favicon);
            server.AddRoute("/users/login", Login);

            await server.StartAsync(8585);
        }        
        static HttpResponse Login(HttpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}