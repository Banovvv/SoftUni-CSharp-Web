using BattleCards.Data;
using BattleCards.Services;
using BattleCards.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using WebServer.HTTP;
using WebServer.MvcFramework.Contracts;

namespace BattleCards
{
    public class StartUp : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Add<IUserService, UserService>();
            services.Add<ICardService, CardService>();
            services.Add<ApplicationDataContext, ApplicationDataContext>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDataContext().Database.Migrate();
        }
    }
}
