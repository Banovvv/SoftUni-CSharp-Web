using HttpMethod = WebServer.HTTP.HttpMethod;

namespace WebServer.MvcFramework.Attributes
{
    public class HttpPostAttribute : BaseHttpAttribute
    {
        public HttpPostAttribute()
        {
        }

        public HttpPostAttribute(string url)
        {
            this.Url = url;
        }

        public override HttpMethod Method => HttpMethod.POST;
    }
}
