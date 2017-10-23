using System.IO;
using VersioningManagement.Versions;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInfoViewModel"/> class.
        /// </summary>
        public AssemblyInfoViewModel()
        {
            PropertyChanged += AssemblyInfoViewModel_PropertyChanged;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the AssemblyInfoViewModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void AssemblyInfoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(File))
                return;

            var file = new FileInfo(File);

            if (!file.Exists)
                return;

            var version = new AssemblyInfoVersion(file) { Version = Version };
            version.Write();
        }
    }
}
