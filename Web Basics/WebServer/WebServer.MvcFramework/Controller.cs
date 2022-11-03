using System.Runtime.CompilerServices;
using System.Text;
using WebServer.HTTP;
using WebServer.MvcFramework.ViewEngine;

namespace WebServer.MvcFramework
{
    public abstract class Controller
    {
        private readonly WebServerViewEngine viewEngine;
        private const string UserIdSessionName = "UserId";

        public Controller()
        {
            this.viewEngine = new WebServerViewEngine();
        }

        public HttpRequest Request { get; set; }

        protected HttpResponse View(object viewModel = null, [CallerMemberName] string viewPath = "")
        {
            var viewContent = System.IO.File
                .ReadAllText($"Views/{this.GetType().Name.Replace("Controller", string.Empty)}/{viewPath}.cshtml");

            var responseHtml = InsertViewInLayout(viewContent, viewModel);

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }

        protected HttpResponse File(string filePath, string fileType)
        {
            var iconBytes = System.IO.File.ReadAllBytes(filePath);

            HttpResponse response = new HttpResponse(fileType, iconBytes);

            return response;
        }

        protected HttpResponse Redirect(string url)
        {
            HttpResponse response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("Location", url));

            return response;
        }

        protected HttpResponse Error(string errorMessage)
        {
            var viewContent = $"<div class=\"alert alert-danger\" role=\"alert\">{errorMessage}</div>";

            var responseHtml = InsertViewInLayout(viewContent);

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBodyBytes, HttpStatusCode.InternalServerError);

            return response;
        }

        protected void SignIn(string userId)
        {
            this.Request.Session[UserIdSessionName] = userId;
        }

        protected void SignOut()
        {
            this.Request.Session[UserIdSessionName] = null;
        }

        protected bool IsSignedIn()
        {
            return this.Request.Session.ContainsKey(UserIdSessionName);
        }

        protected string GetUserId()
        {
            if (!this.Request.Session.ContainsKey(UserIdSessionName))
            {
                return null;
            }

            return this.Request.Session[UserIdSessionName];
        }

        private string InsertViewInLayout(string viewContent, object viewModel = null)
        {
            var layout = System.IO.File.ReadAllText($"Views/Shared/_Layout.cshtml");
            layout = layout.Replace("@RenderBody()", "___VIEW___");
            layout = this.viewEngine.GetHtml(layout, viewModel, "");
            viewContent = this.viewEngine.GetHtml(viewContent, viewModel, "");
            var responseHtml = layout.Replace("___VIEW___", viewContent);

            return responseHtml;
        }
    }
}
