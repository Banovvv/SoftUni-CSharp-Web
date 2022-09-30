using System.Net.Mime;
using System.Text;

namespace WebServer.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.Body = new byte[0];
            this.StatusCode = statusCode;
            this.Headers = new List<Header>();
            this.Cookies = new List<ResponseCookie>();
        }

        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            this.StatusCode = statusCode;
            this.Body = body;

            this.Headers = new List<Header>()
            {
                new Header( "Server", "Ivan's Web Server 1.0" ),
                new Header( "Content-Type", contentType ),
                new Header( "Content-Length", body.Length.ToString() )
            };

            this.Cookies = new List<ResponseCookie>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<ResponseCookie> Cookies { get; set; }
        public byte[] Body { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}{HttpConstants.NewLine}");

            foreach (var header in this.Headers)
            {
                sb.Append(header.ToString() + HttpConstants.NewLine);
            }

            foreach (var cookie in this.Cookies)
            {
                sb.Append($"Set-Cookie: {cookie.ToString()}{HttpConstants.NewLine}");
            }

            sb.Append(HttpConstants.NewLine);

            return sb.ToString();
        }
    }
}
