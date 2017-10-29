using System.Windows.Input;
using VersioningManagement.Commands;

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

        //Commands
        public ICommand IncreaseMajorVersionCommand { get; set; }
        public ICommand IncreaseMinorVersionCommand { get; set; }
        public ICommand IncreaseRevisionVersionCommand { get; set; }
        public ICommand DecreaseMajorVersionCommand { get; set; }
        public ICommand DecreaseMinorVersionCommand { get; set; }
        public ICommand DecreaseRevisionVersionCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewModel"/> class.
        /// </summary>
        public ProjectViewModel()
        {
            Solution = new SolutionViewModel();
            NuspecVersion = new NuspecViewModel();
            AssemblyInfoVersion = new AssemblyInfoViewModel();

            //Commands
            IncreaseMajorVersionCommand = new IncreaseMajorVersionCommand();
            IncreaseMinorVersionCommand = new IncreaseMinorVersionCommand();
            IncreaseRevisionVersionCommand = new IncreaseRevisionVersionCommand();
            DecreaseMajorVersionCommand = new DecreaseMajorVersionCommand();
            DecreaseMinorVersionCommand = new DecreaseMinorVersionCommand();
            DecreaseRevisionVersionCommand = new DecreaseRevisionVersionCommand();
        }
    }
}
