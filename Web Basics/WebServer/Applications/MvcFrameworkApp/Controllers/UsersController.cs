﻿using BattleCards.Services;
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

        [HttpPost("users/login")]
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
        public async Task<HttpResponse> DoRegister()
        {
            // TODO: read data
            var username = this.Request.FormData["username"];
            var email = this.Request.FormData["email"];
            var password = this.Request.FormData["password"];
            var confirmPassword = this.Request.FormData["confirmPassword"];

            // TODO: check user
            if (username == null || username.Length < 5 || username.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters!");
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
