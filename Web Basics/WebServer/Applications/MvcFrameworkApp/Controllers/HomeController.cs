using BattleCards.ViewModels;
using WebServer.HTTP;
using WebServer.MvcFramework;
using WebServer.MvcFramework.Attributes;

namespace BattleCards.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
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
