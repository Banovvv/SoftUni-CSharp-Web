using System.Text;
using WebServer.HTTP;

namespace WebServer.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View(string viewPath)
        {
            var responseHtml = System.IO.File.ReadAllText
                ($"Views/{this.GetType().Name.Replace("Controller", string.Empty)}/{viewPath}.html");
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
    }
}
