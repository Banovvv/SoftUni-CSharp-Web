namespace WebServer.MvcFramework.Contracts
{
    public interface IServiceCollection
    {
        object CreateInstance(Type type);
        void Add<TSource, TDestination>();
    }
}
