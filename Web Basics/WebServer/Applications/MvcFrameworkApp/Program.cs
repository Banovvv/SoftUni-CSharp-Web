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

        static HttpResponse HomePage(HttpRequest request)
        {
            var responseHtml = "<h1>Welcome</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBodyBytes);
            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
                { HttpOnly = true, MaxAge = 3 * 24 * 60 * 60 });

            return response;
        }

        static HttpResponse About(HttpRequest request)
        {
            var responseHtml = "<h1>About</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }
        static HttpResponse Login(HttpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}