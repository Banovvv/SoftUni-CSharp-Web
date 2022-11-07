using BattleCards.Services;
using BattleCards.Services.Contracts;
using System.Text.RegularExpressions;
using WebServer.HTTP;
using WebServer.MvcFramework;
using WebServer.MvcFramework.Attributes;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
             this.service = service; 
        }

        public HttpResponse Login()
        {
            if (this.IsSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }
        public HttpResponse Register()
        {
            if (this.IsSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost("/Users/Login")]
        public async Task<HttpResponse> DoLogin()
        {
            if (this.IsSignedIn())
            {
                return this.Redirect("/");
            }

            var username = this.Request.FormData["Username"];
            var password = this.Request.FormData["Password"];

            var userId = await this.service.GetUserIdAsync(username, password);

            if (userId == null)
            {
                return this.Error("Invalid username or password!");

            }

            this.SignIn(userId);
            return this.Redirect("/");
        }

        [HttpPost("/Users/Register")]
        public async Task<HttpResponse> DoRegister()
        {
            if (this.IsSignedIn())
            {
                return this.Redirect("/");
            }

            var username = this.Request.FormData["Username"];
            var email = this.Request.FormData["Email"];
            var password = this.Request.FormData["Password"];
            var confirmPassword = this.Request.FormData["ConfirmPassword"];

            #region Input Data Checks
            if (username == null || username.Length < 5 || username.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters!");
            }

            if (!Regex.IsMatch(username, "^[a-zA-Z0-9]+$"))
            {
                return this.Error("Username contains forbidden characters!");
            }

            if (!this.service.IsValidEmail(email))
            {
                return this.Error("Invalid email address");
            }

            if (password == null || password.Length < 6 || password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters!");
            }

            if (password != confirmPassword)
            {
                return this.Error("Passwords do not match!");
            }

            if (!await this.service.IsUsernameAvailableAsync(username))
            {
                return this.Error("Username is already taken!");
            }

            if (!await this.service.IsEmailAvailableAsync(email))
            {
                return this.Error("Emails is already taken!");
            }
            #endregion

            await this.service.CreateUserAsync(username, email, password);

            return this.Redirect("/Users/Login");
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
