using System.Net;
using System.Text;

namespace WebServer.HTTP
{
    public class HttpRequest
    {
        public static IDictionary<string, Dictionary<string, string>> Sessions = new Dictionary<string, Dictionary<string, string>>();

        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
            this.FormData = new Dictionary<string, string>();
            this.QueryData = new Dictionary<string, string>();

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

            if (this.Headers.Any(x => x.Name == HttpConstants.RequestCookieHeader))
            {
                var cookieString = this.Headers
                    .First(x => x.Name == HttpConstants.RequestCookieHeader)
                    .Value;

                var cookies = cookieString
                    .Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in cookies)
                {
                    this.Cookies.Add(new Cookie(cookie));
                }
            }

            var sessionCookie = this.Cookies
                .Where(x => x.Name == HttpConstants.SessionCookieName)
                .FirstOrDefault();

            if (sessionCookie == null)
            {
                var sessionId = Guid.NewGuid().ToString();
                this.Session = new Dictionary<string, string>();

                Sessions.Add(sessionId, this.Session);
                this.Cookies.Add(new Cookie(HttpConstants.SessionCookieName, sessionId));
            }
            else if (!Sessions.ContainsKey(sessionCookie.Value))
            {
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionCookie.Value, this.Session);
            }
            else
            {
                this.Session = Sessions[sessionCookie.Value];
            }

            if (this.Path.Contains("?"))
            {
                var pathParts = this.Path.Split(new char[] { '?' }, 2);

                this.Path = pathParts[0];
                this.Query = pathParts[1];
            }
            else
            {
                this.Query = string.Empty;
            }

            this.Body = requestBody.ToString().TrimEnd('\n', '\r');

            SplitParameters(this.Body, this.FormData);
            SplitParameters(this.Query, this.QueryData);
        }

        private static void SplitParameters(string inputParameters, IDictionary<string, string> output)
        {
            var parameters = inputParameters.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var parameter in parameters)
            {
                if (string.IsNullOrEmpty(parameter))
                {
                    break;
                }

                var parameterParts = parameter.Split(new char[] { '=' }, 2);
                var name = parameterParts[0];
                var value = WebUtility.UrlDecode(parameterParts[1]);

                if (!output.ContainsKey(name))
                {
                    output.Add(name, value);
                }
            }
        }

        public string Path { get; set; }
        public string Query { get; set; }
        public HttpMethod Method { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public IDictionary<string, string> FormData { get; set; }
        public IDictionary<string, string> QueryData { get; set; }
        public Dictionary<string, string> Session { get; set; }
        public string Body { get; set; }
    }
}
