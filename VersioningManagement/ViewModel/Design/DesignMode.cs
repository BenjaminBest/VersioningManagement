using System.ComponentModel;
using System.Windows;

namespace VersioningManagement.ViewModel.Design
{
    /// <summary>
    /// The DesignMode is used to determine if currently the designmode is enabled
    /// </summary>
    public static class DesignMode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignMode"/> class.
        /// </summary>
        public static bool IsEnabled()
        {
            return ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject))
                .DefaultValue));
        }
    }
}