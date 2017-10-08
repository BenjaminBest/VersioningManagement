using Ninject.Modules;
using VersioningManagement.Configuration;
using VersioningManagement.Localization;

namespace VersioningManagement
{
    /// <summary>
    /// Module to configure all ninject bindings
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class NinjectBindings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ILocalizer<SolutionInfo>>().To<SolutionLocalizer>();
            Bind<ILocalizer<NuspecInfo>>().To<NuspecLocalizer>();
            Bind<ILocalizerRegistry>().To<LocalizerRegistry>().InSingletonScope();

            Bind<IConfiguration>().ToMethod(context =>
            {
                var configuration = new Configuration.Configuration();
                configuration.Read();

                return configuration;
            }).InSingletonScope();
        }
    }
}
