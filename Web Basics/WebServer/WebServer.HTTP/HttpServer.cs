namespace WebServer.HTTP
{
    public class HttpServer : IHttpServer
    {
        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            throw new NotImplementedException();
        }

        public async Task StartAsync(int port)
        {
            throw new NotImplementedException();
        }
    }
}
 