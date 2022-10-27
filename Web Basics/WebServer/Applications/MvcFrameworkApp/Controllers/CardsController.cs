using BattleCards.Data;
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
            return this.View();
        }

        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd()
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

            return this.View();
        }

        public HttpResponse All()
        {
            return this.View();
        }

        public HttpResponse Collection()
        {
            return this.View();
        }
    }
}
