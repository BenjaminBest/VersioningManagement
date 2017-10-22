using System.Collections.ObjectModel;
using System.Windows.Data;

namespace VersioningManagement.ViewModel
{
    /// <summary>
    /// The viewmodel for the main window contains the main view model and necessary nested viewmodels
    /// </summary>
    public class MainWindowViewModel
    {
        private static object _lock = new object();

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public ObservableCollection<ProjectViewModel> Projects { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            Projects = new ObservableCollection<ProjectViewModel>();

            //Take care about thread affinity
            //Enables a collection to be accessed across multiple threads and specifies the lock object that should be used to synchronize access to the collection.
            BindingOperations.EnableCollectionSynchronization(Projects, _lock);
        }
    }
}
