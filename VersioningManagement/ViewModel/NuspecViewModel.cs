namespace VersioningManagement.ViewModel
{
    /// <summary>
    /// The class NuspecViewModel contains the viewmodel with version information based on the nuspec
    /// </summary>
    public class NuspecViewModel : ObservableViewModelBase
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
