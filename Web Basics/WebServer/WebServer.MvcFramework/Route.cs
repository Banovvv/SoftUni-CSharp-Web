using WebServer.HTTP;

namespace WebServer.MvcFramework
{
    public class Route
    {
        public string Path { get; set; }
        public Func<HttpRequest, HttpResponse> Action { get; set; }
    }
}
