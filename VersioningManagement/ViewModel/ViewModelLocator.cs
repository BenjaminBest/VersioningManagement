using System.IO;
using System.Linq;
using VersioningManagement.Configuration;
using VersioningManagement.DependencyInjection;
using VersioningManagement.Localization;

namespace VersioningManagement.ViewModel
{
    /// <summary>
    /// The ViewModelLocator is called by the window to retrieve the bound datacontext which is in fact a viewmodel
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Gets or sets the main window view model.
        /// </summary>
        /// <value>
        /// The main window view model.
        /// </value>
        public MainWindowViewModel MainWindowViewModel => ServiceLocator.Get<MainWindowViewModel>();

        /// <summary>
        /// Creates the main view model
        /// </summary>
        /// <returns></returns>
        public static void Update()
        {
            var viewmodel = ServiceLocator.Get<MainWindowViewModel>();
            var configuration = ServiceLocator.Get<IConfiguration>();
            var localizerRegistry = ServiceLocator.Get<ILocalizerRegistry>();

            var path = configuration.RecentLocalizedPaths.Any()
                ? configuration.RecentLocalizedPaths.FirstOrDefault()
                : string.Empty;

            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            var solutionLocalizer = localizerRegistry.CreateLocalizer<SolutionInfo>();
            var solutions = solutionLocalizer.GetItems(new DirectoryInfo(path));

            viewmodel.Projects.Clear();

            foreach (var solution in solutions)
            {
                foreach (var project in solution.Projects)
                {
                    var nuspecViewModel = new NuspecViewModel()
                    {
                        Version = project.NuspecVersion?.Version ?? string.Empty,
                        File = project.NuspecVersion?.File.FullName ?? string.Empty
                    };

                    var assemblyInfoViewModel = new AssemblyInfoViewModel()
                    {
                        Version = project.AssemblyInfoVersion?.Version ?? string.Empty,
                        File = project.AssemblyInfoVersion?.File.FullName ?? string.Empty
                    };

                    viewmodel.Projects.Add(new ProjectViewModel
                    {
                        Name = project.Name,
                        AssemblyInfoVersion = assemblyInfoViewModel,
                        NuspecVersion = nuspecViewModel,
                        Solution = new SolutionViewModel()
                        {
                            Name = solution.Name
                        }
                    });
                }
            }
        }
    }
}