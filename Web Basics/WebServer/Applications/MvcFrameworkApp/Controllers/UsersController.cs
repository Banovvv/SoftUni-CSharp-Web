using System.Text;
using WebServer.HTTP;
using WebServer.MvcFramework;

namespace MvcFrameworkApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            var responseHtml =
                @"<div class=""container"">
                  <label for=""uname""><b>Username</b></label>
                  <input type=""text"" placeholder=""Enter Username"" name=""uname"" required>
                  <br/ >
                  <br/ >
                  <label for=""psw""><b>Password</b></label>
                  <input type=""password"" placeholder=""Enter Password"" name=""psw"" required>
                  <br/ >
                  <br/ >            
                  <button type=""submit"">Login</button>
                  <label>
                    <input type=""checkbox"" checked=""checked"" name=""remember""> Remember me
                  </label>
                  </div>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }
        public HttpResponse Register(HttpRequest request)
        {
            var responseHtml = "<h1>Register</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }
    }
}
