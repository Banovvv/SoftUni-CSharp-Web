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
            server.AddRoute("/users/login", Login);

            await server.StartAsync(8585);
        }

        static HttpResponse HomePage(HttpRequest arg)
        {
            throw new NotImplementedException();
        }

        static HttpResponse About(HttpRequest arg)
        {
            throw new NotImplementedException();
        }

        static HttpResponse Login(HttpRequest arg)
        {
            throw new NotImplementedException();
        }
    }
}