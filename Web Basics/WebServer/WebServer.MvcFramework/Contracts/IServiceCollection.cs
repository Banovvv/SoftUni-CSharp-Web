namespace WebServer.MvcFramework.Contracts
{
    public interface IServiceCollection
    {
        void Add<TSource, TDestination>();
    }
}
