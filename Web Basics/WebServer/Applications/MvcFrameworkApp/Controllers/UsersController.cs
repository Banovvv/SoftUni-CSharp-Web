using WebServer.HTTP;
using WebServer.MvcFramework;

namespace MvcFrameworkApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View(nameof(Login));
        }
        public HttpResponse Register(HttpRequest request)
        {
            return this.View(nameof(Register));
        }
    }
}
