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

            var constructor = type.GetConstructors()
                .OrderBy(x => x.GetParameters().Count())
                .FirstOrDefault();

            var parameters = constructor?.GetParameters();
            var parameterValues = new List<object>();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    var parameterValue = CreateInstance(parameter.GetType());
                    parameterValues.Add(parameterValue);
                }
            }

            var constructedObject = constructor?.Invoke(parameterValues.ToArray());

            return constructedObject;
        }
    }
}
