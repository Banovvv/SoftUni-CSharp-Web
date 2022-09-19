namespace WebServer.HTTP
{
    public interface IHttpServer
    {
        void Start(int port);
        void AddRoute(string path, Func<HttpRequest, HttpResponse> action);
    }
}
