using System.Runtime.CompilerServices;
using System.Text;
using WebServer.HTTP;
using WebServer.MvcFramework.ViewEngine;

namespace WebServer.MvcFramework
{
    public abstract class Controller
    {
        private readonly WebServerViewEngine viewEngine;

        public Controller()
        {
            this.viewEngine = new WebServerViewEngine();
        }

        public HttpRequest Request { get; set; }

        public HttpResponse View(object viewModel = null, [CallerMemberName] string viewPath = "")
        {
            var layout = System.IO.File.ReadAllText($"Views/Shared/_Layout.cshtml");
            layout = layout.Replace("@RenderBody()", "___VIEW___");
            layout = this.viewEngine.GetHtml(layout, viewModel, "");

            var viewContent = System.IO.File.ReadAllText($"Views/{this.GetType().Name.Replace("Controller", string.Empty)}/{viewPath}.cshtml");
            viewContent = this.viewEngine.GetHtml(viewContent, viewModel, "");

            var responseHtml = layout.Replace("___VIEW___", viewContent);

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }

        public HttpResponse File(string filePath, string fileType)
        {
            var iconBytes = System.IO.File.ReadAllBytes(filePath);

            HttpResponse response = new HttpResponse(fileType, iconBytes);

            return response;
        }

        public HttpResponse Redirect(string url)
        {
            HttpResponse response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("Location", url));

            return response;
        }
    }
}
