namespace VersioningManagement.ViewModel
{
    /// <summary>
    /// The viewmodel contains the information about one project
    /// </summary>
    public class ProjectViewModel : ObservableViewModelBase
    {
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>
        /// The name of the project.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the nuspec version.
        /// </summary>
        /// <value>
        /// The nuspec version.
        /// </value>
        public NuspecViewModel NuspecVersion { get; set; }

        /// <summary>
        /// Gets or sets the assembly information version.
        /// </summary>
        /// <value>
        /// The assembly information version.
        /// </value>
        public AssemblyInfoViewModel AssemblyInfoVersion { get; set; }

        /// <summary>
        /// Gets or sets the solution.
        /// </summary>
        /// <value>
        /// The solution.
        /// </value>
        public SolutionViewModel Solution { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewModel"/> class.
        /// </summary>
        public ProjectViewModel()
        {
            Solution = new SolutionViewModel();
            NuspecVersion = new NuspecViewModel();
            AssemblyInfoVersion = new AssemblyInfoViewModel();
        }
    }
}
