using System.Windows;
using VersioningManagement.DependencyInjection;

namespace VersioningManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>Raises the <see cref="E:System.Windows.Application.Startup" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureNinject();
        }

        /// <summary>
        /// Configures ninject and load alls modules
        /// </summary>
        private void ConfigureNinject()
        {
            var mainWindow = ServiceLocator.Get<MainWindow>();
            mainWindow.Show();
        }
    }
}
