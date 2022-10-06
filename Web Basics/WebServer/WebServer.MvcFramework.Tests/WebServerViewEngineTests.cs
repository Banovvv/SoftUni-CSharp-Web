using WebServer.MvcFramework.ViewEngine;

namespace WebServer.MvcFramework.Tests
{
    public class WebServerViewEngineTests
    {
        [Theory]
        [InlineData("CleanHtml")]
        public void TestGetHtml(string fileName)
        {
            var viewModel = new TestViewModel()
            {
                Name = "",
                Price = 123.45M,
                BirthDate = new DateTime(2018, 6, 18)
            };

            IViewEngine viewEngine = new WebServerViewEngine();

            var view = File.ReadAllText($"ViewTests/{fileName}.html");
            var result = viewEngine.GetHtml(view, viewModel);
            var expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");

            Assert.Equal(expectedResult, result);
        }

        public class TestViewModel
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public DateTime BirthDate { get; set; }
        }
    }
}