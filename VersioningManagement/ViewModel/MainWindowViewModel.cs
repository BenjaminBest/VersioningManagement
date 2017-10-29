using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using VersioningManagement.Commands;

namespace VersioningManagement.ViewModel
{
    /// <summary>
    /// The viewmodel for the main window contains the main view model and necessary nested viewmodels
    /// </summary>
    public class MainWindowViewModel
    {
        /// <summary>
        /// The lock
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public ObservableCollection<ProjectViewModel> Projects { get; set; }

        /// <summary>
        /// Gets or sets the toggle pre release command.
        /// </summary>
        /// <value>
        /// The toggle pre release command.
        /// </value>
        public ICommand TogglePreReleaseCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            Projects = new ObservableCollection<ProjectViewModel>();

            //Take care about thread affinity
            //Enables a collection to be accessed across multiple threads and specifies the lock object that should be used to synchronize access to the collection.
            BindingOperations.EnableCollectionSynchronization(Projects, _lock);

            //Commands
            TogglePreReleaseCommand = new TogglePreReleaseCommand();
        }
    }
}