namespace WebServer.MvcFramework.Tests
{
    public class ViewEngineTests
    {
        [Theory]
        [InlineData("CleanHtml")]
        public void TestGetHtml(string fileName)
        {

        }

        public class TestViewModel
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public DateTime BirthDate { get; set; }
        }
    }
}