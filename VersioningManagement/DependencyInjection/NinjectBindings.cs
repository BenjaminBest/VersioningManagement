using Ninject.Modules;
using VersioningManagement.Configuration;
using VersioningManagement.Localization;
using VersioningManagement.ViewModel;
using VersioningManagement.ViewModel.Design;

namespace VersioningManagement.DependencyInjection
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

            //ViewModels
            if (DesignMode.IsEnabled())
                Bind<MainWindowViewModel>().To<DesignMainWindowViewModel>().InSingletonScope();
            else
                Bind<MainWindowViewModel>().To<MainWindowViewModel>().InSingletonScope();
        }
    }
}
