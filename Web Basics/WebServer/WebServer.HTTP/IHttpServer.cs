namespace WebServer.HTTP
{
    public interface IHttpServer
    {
        Task StartAsync(int port);
    }
}
