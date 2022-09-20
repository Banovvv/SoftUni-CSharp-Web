﻿namespace WebServer.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();

            var lines = requestString
                .Split(new string[] {HttpConstants.NewLine }, StringSplitOptions.None);

            var headerLine = lines[0];
            var headerLineParts = headerLine
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            this.Method = headerLineParts[0];
            this.Path = headerLineParts[1];

            bool isHeader = true;

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
                        this.Body += line + HttpConstants.NewLine;
                    }
                }
            }
        }

        public string Path { get; set; }
        public string Method { get; set; }
        public List<Header> Headers { get; set; }
        public List<Cookie> Cookies { get; set; }
        public string Body { get; set; }
    }
}
