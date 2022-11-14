using BattleCards.Data;
using BattleCards.Data.Models;
using BattleCards.ViewModels.Cards;
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
        public async Task<HttpResponse> AddAsync(CardInputModel inputModel)
        {
            if (inputModel.Name.Length < 5)
            {
                return this.Error("Name must be at least five characters long!");
            }

            this.context.Cards.Add(new Card
            {
                Attack = inputModel.Attack,
                Health = inputModel.Health,
                Description = inputModel.Description,
                Name = inputModel.Name,
                ImageUrl = inputModel.Image,
                Keyword = inputModel.Keyword
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
