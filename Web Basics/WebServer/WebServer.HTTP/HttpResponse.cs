namespace WebServer.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode, string contentType, byte[] body)
        {
            this.StatusCode = statusCode;
            this.Body = body;

            this.Headers = new List<Header>()
            {
                new Header( "Content-Type", contentType ),
                new Header( "Content-Length", body.Length.ToString() )
            };
        }

        public HttpStatusCode StatusCode { get; set; }
        public ICollection<Header> Headers { get; set; }
        public byte[] Body { get; set; }
    }
}
