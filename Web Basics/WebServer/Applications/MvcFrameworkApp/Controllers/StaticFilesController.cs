using WebServer.HTTP;
using WebServer.MvcFramework;

namespace MvcFrameworkApp.Controllers
{
    public class StaticFilesController : Controller
    {
        public HttpResponse Favicon(HttpRequest request)
        {
            var iconBytes = File.ReadAllBytes("wwwroot/favicon.ico");

            HttpResponse response = new HttpResponse("image/vnd.microsoft.ico", iconBytes);

            return response;
        }
    }
}
