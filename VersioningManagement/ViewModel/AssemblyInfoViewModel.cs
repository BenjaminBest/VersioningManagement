namespace VersioningManagement.ViewModel
{
    /// <summary>
    /// The class AssemblyInfoViewModel contains the viewmodel with version information based on the AssemblyInfo
    /// </summary>
    public class AssemblyInfoViewModel : ObservableViewModelBase
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public string File { get; set; }
    }
}
