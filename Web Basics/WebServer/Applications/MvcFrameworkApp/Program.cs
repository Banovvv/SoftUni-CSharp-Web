using WebServer.MvcFramework;

namespace BattleCards
{
    class Program
    {
        static async Task Main()
        {
            await Host.CreateHostAsync(new StartUp(), 8585);
        }
    }
}