using WebServer.HTTP;
using WebServer.MvcFramework;
using WebServer.MvcFramework.Attributes;

namespace MvcFrameworkApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
        }
        public HttpResponse Register(HttpRequest request)
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse DoLogin(HttpRequest request)
        {
            // TODO: read data
            // TODO: check user
            // TODO: log user in

            return this.Redirect("/");
        }
    }
}
