using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using VersioningManagement.Configuration;
using VersioningManagement.Localization;

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
        /// The configuration manager
        /// </summary>
        private readonly IConfigurationManager _configurationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        /// <param name="localizerRegistry">The localizer registry.</param>
        /// <param name="configurationManager">The configuration manager.</param>
        public MainWindow(
            ILocalizerRegistry localizerRegistry,
            IConfigurationManager configurationManager)
        {
            _localizerRegistry = localizerRegistry;
            _configurationManager = configurationManager;


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

                var paths = _configurationManager.Configuration.RecentLocalizedPaths;

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
                _configurationManager.Write();


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
            var searcher = _localizerRegistry.CreateLocalizer<SolutionInfo>();
            var solutions = searcher.GetItems(directory);

            Dispatcher.Invoke(() =>
            {
                ClearGrid();

                foreach (var solution in solutions)
                {
                    foreach (var project in solution.Projects)
                    {
                        AddProjectToGrid(project);
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
