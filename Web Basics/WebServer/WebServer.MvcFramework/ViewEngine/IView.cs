namespace WebServer.MvcFramework.ViewEngine
{
    public interface IView
    {
        string GetHtml(object viewModel);
    }
}
