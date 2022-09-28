using WebServer.HTTP;
using WebServer.MvcFramework;

namespace MvcFrameworkApp.Controllers
{
    public class CardsController : Controller
    {
        public HttpResponse Add(HttpRequest request)
        {
            return this.View(nameof(Add));
        }

        public HttpResponse All(HttpRequest request)
        {
            return this.View(nameof(All));
        }

        public HttpResponse Collection(HttpRequest request)
        {
            return this.View(nameof(Collection));
        }
    }
}
