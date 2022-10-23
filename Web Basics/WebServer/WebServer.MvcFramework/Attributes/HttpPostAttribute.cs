using HttpMethod = WebServer.HTTP.HttpMethod;

namespace WebServer.MvcFramework.Attributes
{
    public class HttpPostAttribute : BaseHttpAttribute
    {
        public override HttpMethod Method => HttpMethod.Post;
    }
}
