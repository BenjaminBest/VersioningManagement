namespace VersioningManagement.ViewModel
{
    /// <summary>
    /// The viewmodel contains the information about one solution
    /// </summary>
    public class SolutionViewModel : ObservableViewModelBase
    {
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>
        /// The name of the project.
        /// </value>
        public string Name { get; set; }
    }
}
