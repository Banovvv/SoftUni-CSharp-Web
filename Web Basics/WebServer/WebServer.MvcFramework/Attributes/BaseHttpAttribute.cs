using HttpMethod = WebServer.HTTP.HttpMethod;

namespace WebServer.MvcFramework.Attributes
{
    public abstract class BaseHttpAttribute
    {
        public string Url { get; }
        public abstract HttpMethod Method { get; }
    }
}
