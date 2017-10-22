using System.ComponentModel;

namespace VersioningManagement.ViewModel
{
    /// <summary>
    /// The ObservableViewModelBase is a base class for viewmodels that should notify when properties have been altered. The Fody-extension is used so that normal properties 
    /// can be used instead of a custom implmentation of getter and setter.
    /// </summary>
    public class ObservableViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}