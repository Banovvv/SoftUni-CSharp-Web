using HttpMethod = WebServer.HTTP.HttpMethod;

namespace WebServer.MvcFramework.Attributes
{
    public class HttpGetAttribute : BaseHttpAttribute
    {
        public HttpGetAttribute()
        {
        }

        public HttpGetAttribute(string url)
        {
            this.Url = url;
        }

        public override HttpMethod Method => HttpMethod.GET;
    }
}
