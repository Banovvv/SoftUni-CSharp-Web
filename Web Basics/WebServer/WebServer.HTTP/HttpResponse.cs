namespace WebServer.HTTP
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public ICollection<Header> Headers { get; set; }
    }
}
