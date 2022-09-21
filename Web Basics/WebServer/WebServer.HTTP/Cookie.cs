namespace WebServer.HTTP
{
    public class Cookie
    {
        public Cookie(string cookieLine)
        {
            var cookieParts = cookieLine
                .Split(new string[] { ": " }, 2, StringSplitOptions.None);

            this.Name = cookieParts[0];
            this.Value = cookieParts[1];
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}