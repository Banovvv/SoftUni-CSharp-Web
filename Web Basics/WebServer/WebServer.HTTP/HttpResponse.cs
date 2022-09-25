namespace WebServer.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode, byte[] body)
        {
            this.StatusCode = statusCode;
            this.Body = body;

            this.Headers = new List<Header>()
            {
                new Header( "Content-Type", "TODO" ),
                new Header( "Content-Length", "TODO" )
            };
        }

        public HttpStatusCode StatusCode { get; set; }
        public ICollection<Header> Headers { get; set; }
        public byte[] Body { get; set; }
    }
}
