using BattleCards.Services;
using BattleCards.Services.Contracts;
using WebServer.HTTP;
using WebServer.MvcFramework;
using WebServer.MvcFramework.Attributes;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService service;

        public UsersController()
        {
            this.service = new UserService();
        }

        public HttpResponse Login()
        {
            return this.View();
        }
        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost("Users/Login")]
        public async Task<HttpResponse> DoLogin()
        {
            var username = this.Request.FormData["username"];
            var password = this.Request.FormData["password"];

            var userId = await this.service.GetUserIdAsync(username, password);

            if (userId == null)
            {
                return this.Error("Invalid username or password!");

            }

            this.SignIn(userId);
            return this.Redirect("/");
        }

        [HttpPost("Users/Register")]
        public HttpResponse DoRegister()
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
