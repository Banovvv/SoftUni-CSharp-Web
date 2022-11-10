using BattleCards.Data;
using BattleCards.Data.Models;
using WebServer.HTTP;
using WebServer.MvcFramework;
using WebServer.MvcFramework.Attributes;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly ApplicationDataContext context;

        public CardsController(ApplicationDataContext context)
        {
            this.context = context;
        }

        public HttpResponse Add()
        {
            if (this.IsSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost("/Cards/Add")]
        public async Task<HttpResponse> DoAddAsync(string name, int attack, int health, string description, string image, string keyword)
        {
            if (name.Length < 5)
            {
                return this.Error("Name must be at least five characters long!");
            }

            this.context.Cards.Add(new Card
            {
                Attack = attack,
                Health = health,
                Description = description,
                Name = name,
                ImageUrl = image,
                Keyword = keyword
            });

            await this.context.SaveChangesAsync();

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
