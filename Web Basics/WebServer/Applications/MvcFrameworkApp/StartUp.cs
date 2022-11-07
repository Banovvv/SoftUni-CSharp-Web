using BattleCards.Data;
using Microsoft.EntityFrameworkCore;
using WebServer.HTTP;
using WebServer.MvcFramework.Contracts;

namespace BattleCards
{
    public class StartUp : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDataContext().Database.Migrate();
        }
    }
}
