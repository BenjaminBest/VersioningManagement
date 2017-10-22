using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using VersioningManagement.Configuration;
using VersioningManagement.ViewModel;

namespace VersioningManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public MainWindow(IConfiguration configuration)
        {
            _configuration = configuration;


            InitializeComponent();
        }

        /// <summary>
        /// Handles the OnClick event of the OpenFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void OpenFolder_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.ShowNewFolderButton = false;
                dialog.Description = "Select a folder which will be searched for packages";

                var paths = _configuration.RecentLocalizedPaths;

                //Restore last path
                if (paths.Any())
                    dialog.SelectedPath = paths.FirstOrDefault();

                var result = dialog.ShowDialog();

                if (result != System.Windows.Forms.DialogResult.OK)
                    return;

                if (string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    return;

                var path = new DirectoryInfo(dialog.SelectedPath);

                //Remember path
                if (paths.Contains(path.FullName))
                {
                    paths.Remove(path.FullName);
                }

                paths.Insert(0, path.FullName);
                _configuration.Write();


                //Load all projects and wait till finished
                await Task.Run(() => ViewModelLocator.Update());
            }
        }
    }
}