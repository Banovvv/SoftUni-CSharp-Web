﻿using System.Text;

namespace WebServer.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.OK)
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}{HttpConstants.NewLine}");

            foreach(var header in this.Headers)
            {
                sb.Append(header.ToString() + HttpConstants.NewLine);
            }

            sb.Append(HttpConstants.NewLine);

            return sb.ToString();
        }
    }
}
