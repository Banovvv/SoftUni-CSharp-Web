using WebServer.MvcFramework.Contracts;

namespace WebServer.MvcFramework
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, Type> dependencyContainer = new Dictionary<Type, Type>();

        public void Add<TSource, TDestination>()
        {
            this.dependencyContainer[typeof(TSource)] = typeof(TDestination);
        }

        public object CreateInstance(Type type)
        {
            if (this.dependencyContainer.ContainsKey(type))
            {
                type = this.dependencyContainer[type];
            }

            return Activator.CreateInstance(type);
        }
    }
}
