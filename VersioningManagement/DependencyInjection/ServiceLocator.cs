using System;
using System.Reflection;
using Ninject;

namespace VersioningManagement.DependencyInjection
{
    /// <summary>
    /// The class ServiceLocator is used as a facade for the Ninject kernel, because WPF doenst handle DI very well.
    /// </summary>
    public class ServiceLocator
    {
        /// <summary>
        /// The kernel
        /// </summary>
        private static readonly StandardKernel Kernel;

        /// <summary>
        /// Initializes the <see cref="ServiceLocator"/> class.
        /// </summary>
        static ServiceLocator()
        {
            Kernel = new StandardKernel();
            Kernel.Load(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Gets a instance of type <typeparam name="T">Type</typeparam>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Gets an instance of the specified service.
        /// </summary>
        /// <param name="service">The service to resolve.</param>
        /// <returns>An instance of the service.</returns>
        public static object Get(Type service)
        {
            return Kernel.Get(service);
        }

    }
}
