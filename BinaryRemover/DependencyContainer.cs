namespace BinaryRemover
{
    using Microsoft.Extensions.DependencyInjection;

    internal class DependencyContainer
    {
        private static DependencyContainer dependencyContainer;
        private static ServiceProvider container;

        private DependencyContainer()
        {
            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IFileManagerExtension, FileManagerExtensions>();

            container = serviceCollection.BuildServiceProvider();
        }

        static internal T Get<T>()
        {
            if (dependencyContainer == null)
            {
                dependencyContainer = new DependencyContainer();
            }

            var serviceScopeFactory = container.GetRequiredService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                return scope.ServiceProvider.GetService<T>();
            }
        }
    }
}