using WebServer.HTTP;
using WebServer.MvcFramework;
using WebServer.MvcFramework.Attributes;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return this.View();
        }
        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse DoLogin()
        {
            // TODO: read data
            // TODO: check user
            // TODO: log user in

            return this.Redirect("/");
        }

        public HttpResponse Logout()
        {
            if (!this.IsSignedIn())
            {
                return this.Error("Only logged in users can logout!");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
