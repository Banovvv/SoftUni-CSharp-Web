using System.Text;
using WebServer.HTTP;
using WebServer.MvcFramework;

namespace MvcFrameworkApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            var responseHtml = File.ReadAllText("Views/Users/Login.html");
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }
        public HttpResponse Register(HttpRequest request)
        {
            var responseHtml = File.ReadAllText("Views/Users/Register.html");
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }
    }
}
