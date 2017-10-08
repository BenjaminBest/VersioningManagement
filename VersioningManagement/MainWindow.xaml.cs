using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using VersioningManagement.Configuration;
using VersioningManagement.Localization;
using VersioningManagement.Versions;

namespace VersioningManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The localizer registry
        /// </summary>
        private readonly ILocalizerRegistry _localizerRegistry;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        /// <param name="localizerRegistry">The localizer registry.</param>
        /// <param name="configuration">The configuration.</param>
        public MainWindow(
            ILocalizerRegistry localizerRegistry, IConfiguration configuration)
        {
            _localizerRegistry = localizerRegistry;
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


                //Load all projects and till finished
                await Task.Run(() => LoadProjects(path));
            }
        }

        /// <summary>
        /// Loads the projects.
        /// </summary>
        /// <param name="directory">The directory.</param>
        private void LoadProjects(DirectoryInfo directory)
        {
            var solutionLocalizer = _localizerRegistry.CreateLocalizer<SolutionInfo>();
            var nuspecLocalizer = _localizerRegistry.CreateLocalizer<NuspecInfo>();
            var solutions = solutionLocalizer.GetItems(directory);


            //TODO:TESTCODE
            var schema = new ConfigurationManager().GenerateSchema(_configuration);
            var valid = new ConfigurationManager().IsValid();


            Dispatcher.Invoke(() =>
            {
                ClearGrid();

                foreach (var solution in solutions)
                {
                    foreach (var project in solution.Projects)
                    {
                        AddProjectToGrid(project);
                        //TODO:TESTCODE
                        var nuspec = nuspecLocalizer.GetItems(project.File.Directory).FirstOrDefault();

                        if (nuspec != null)
                        {
                            var nuspecVersion = new NuspecVersion(nuspec.File);

                            nuspecVersion.Version = "$version$-pre";
                            nuspecVersion.Write();
                        }

                        var assemblyInfoVersion = new AssemblyInfoVersion(new FileInfo(project.File.Directory + @"\Properties\AssemblyInfo.cs"));
                    }
                }
            });

        }

        private void ClearGrid()
        {
            foreach (var child in ContentPanel.Children)
            {
                if (Grid.GetRow((UIElement)child) > 0)
                    ContentPanel.Children.Remove((UIElement)child);
            }
        }

        private void AddProjectToGrid(ProjectInfo project)
        {
            var row = new RowDefinition { Height = GridLength.Auto };
            ContentPanel.RowDefinitions.Add(row);

            var label = new System.Windows.Controls.Label { Content = project.Name };

            Grid.SetColumn(label, 0);
            Grid.SetRow(label, ContentPanel.RowDefinitions.Count - 1);
            ContentPanel.Children.Add(label);
        }
    }
}
