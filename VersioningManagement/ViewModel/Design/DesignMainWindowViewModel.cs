namespace VersioningManagement.ViewModel.Design
{
    /// <summary>
    /// The DesignMainWindowViewModel is a test model for design time
    /// </summary>
    public class DesignMainWindowViewModel : MainWindowViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignMainWindowViewModel"/> class.
        /// </summary>
        public DesignMainWindowViewModel()
        {
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution.Project1",
                NuspecVersion = new NuspecViewModel { Version = "$version$-pre" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "1.50.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 1" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution.Project2",
                NuspecVersion = new NuspecViewModel { Version = "$version$-pre" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "1.50.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 1" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution.Project3",
                NuspecVersion = new NuspecViewModel { Version = "$version$-pre" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "1.50.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 1" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution.Project4",
                NuspecVersion = new NuspecViewModel { Version = "$version$-pre" },
                AssemblyInfoVersion = new AssemblyInfoViewModel() { Version = "1.50.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 1" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution.Project5",
                NuspecVersion = new NuspecViewModel { Version = "$version$-pre" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "1.50.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 1" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution.Project6",
                NuspecVersion = new NuspecViewModel { Version = "$version$-pre" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "1.50.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 1" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution.Project7",
                NuspecVersion = new NuspecViewModel { Version = "$version$-pre" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "1.50.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 1" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution.Project8",
                NuspecVersion = new NuspecViewModel { Version = "$version$-pre" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "1.50.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 1" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution2.Project1",
                NuspecVersion = new NuspecViewModel { Version = "$version$" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "2.0.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 2" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution2.Project2",
                NuspecVersion = new NuspecViewModel { Version = "$version$" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "2.1.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 2" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution2.Project3",
                NuspecVersion = new NuspecViewModel { Version = "$version$" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "2.5.1.x" },
                Solution = new SolutionViewModel { Name = "Solution 2" }
            });
            Projects.Add(new ProjectViewModel
            {
                Name = "Solution2.Project4",
                NuspecVersion = new NuspecViewModel { Version = "$version$" },
                AssemblyInfoVersion = new AssemblyInfoViewModel { Version = "2.9.0.x" },
                Solution = new SolutionViewModel { Name = "Solution 2" }
            });
        }
    }
}
