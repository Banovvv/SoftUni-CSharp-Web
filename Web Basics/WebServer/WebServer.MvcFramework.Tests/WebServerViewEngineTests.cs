using WebServer.MvcFramework.Tests.Models;
using WebServer.MvcFramework.ViewEngine;

namespace WebServer.MvcFramework.Tests
{
    public class WebServerViewEngineTests
    {
        [Theory]
        [InlineData("CleanHtml")]
        [InlineData("Foreach")]
        [InlineData("IfElseFor")]
        [InlineData("ViewModel")]
        public void TestGetHtml(string fileName)
        {
            var viewModel = new TestViewModel()
            {
                Name = "Doggo",
                Price = 12345.67M,
                DateOfBirth = new DateTime(2018, 6, 18)
            };

            IViewEngine viewEngine = new WebServerViewEngine();

            var view = File.ReadAllText($"ViewTests/{fileName}.html");
            var result = viewEngine.GetHtml(view, viewModel, null);
            var expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");

            Assert.Equal(expectedResult, result);
        }
    }
}