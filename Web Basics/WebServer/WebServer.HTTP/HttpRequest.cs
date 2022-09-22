using System.Text;

namespace WebServer.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();

            var lines = requestString
                .Split(new string[] { HttpConstants.NewLine }, StringSplitOptions.None);

            var headerLine = lines[0];
            var headerLineParts = headerLine
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerLineParts[0], true);
            this.Path = headerLineParts[1];

            bool isHeader = true;
            StringBuilder requestBody = new StringBuilder();

            for (int currentLine = 1; currentLine < lines.Length; currentLine++)
            {
                var line = lines[currentLine];

                if (string.IsNullOrWhiteSpace(line))
                {
                    isHeader = false;
                }
                else
                {
                    if (isHeader)
                    {
                        this.Headers.Add(new Header(line));
                    }
                    else
                    {
                        requestBody.AppendLine(line);
                    }
                }
            }

            if (this.Headers.Any(x=>x.Name == HttpConstants.RequestCookieHeader))
            {
                var cookieString = this.Headers
                    .First(x => x.Name == HttpConstants.RequestCookieHeader)
                    .Value;

                var cookies = cookieString.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                foreach(var cookie in cookies)
                {
                    this.Cookies.Add(new Cookie(cookie));
                }
            }

            this.Body = requestBody.ToString();
        }

        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public string Body { get; set; }
    }
}
