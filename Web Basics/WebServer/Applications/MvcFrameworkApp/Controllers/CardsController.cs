﻿using BattleCards.Data;
using BattleCards.Data.Models;
using WebServer.HTTP;
using WebServer.MvcFramework;
using WebServer.MvcFramework.Attributes;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        public HttpResponse Add()
        {
            if (this.IsSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost("/Cards/Add")]
        public async Task<HttpResponse> DoAddAsync()
        {
            var context = new ApplicationDataContext();

            if (this.Request.FormData["name"].Length < 5)
            {
                return this.Error("Name must be at least five characters long!");
            }

            context.Cards.Add(new Card
            {
                Attack = int.Parse(this.Request.FormData["attack"]),
                Health = int.Parse(this.Request.FormData["health"]),
                Description = this.Request.FormData["description"],
                Name = this.Request.FormData["name"],
                ImageUrl = this.Request.FormData["image"],
                Keyword = this.Request.FormData["keyword"]
            });

            await context.SaveChangesAsync();

            return this.Redirect("/Cards/All");
        }

        public HttpResponse All()
        {
            if (this.IsSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        public HttpResponse Collection()
        {
            if (this.IsSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }
    }
}
