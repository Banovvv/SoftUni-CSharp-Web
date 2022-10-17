using MvcFrameworkApp.ViewModels;
using WebServer.HTTP;
using WebServer.MvcFramework;

namespace MvcFrameworkApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            var viewModel = new IndexViewModel()
            {
                CurrentYear = DateTime.UtcNow.Year,
                Message = "Welcome to Battle Cards"
            };

            return this.View(viewModel);
        }
    }
}
