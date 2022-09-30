using WebServer.MvcFramework;

namespace MvcFrameworkApp
{
    class Program
    {
        static async Task Main()
        {
            await Host.CreateHostAsync(new StartUp(), 8585);
        }
    }
}