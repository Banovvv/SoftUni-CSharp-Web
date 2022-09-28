using MvcFrameworkApp.Controllers;
using WebServer.HTTP;

namespace MvcFrameworkApp
{
    class Program
    {
        static async Task Main()
        {
            IHttpServer server = new HttpServer();

            server.AddRoute("/", new HomeController().Index);
            server.AddRoute("/about", new HomeController().About);
            server.AddRoute("/favicon.ico", new StaticFilesController().Favicon);
            server.AddRoute("/users/login", new UsersController().Login);
            server.AddRoute("/users/register", new UsersController().Register);
            server.AddRoute("/cards/add", new CardsController().Add);
            server.AddRoute("/cards/all", new CardsController().All);
            server.AddRoute("/cards/collection", new CardsController().Collection);

            await server.StartAsync(8585);
        }
    }
}