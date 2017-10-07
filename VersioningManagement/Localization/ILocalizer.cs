using System.Collections.Generic;
using System.IO;

namespace VersioningManagement.Localization
{
    /// <summary>
    /// The interface ILocalizer defines a localizer to search for different types of files
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    public interface ILocalizer<out TType>
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns></returns>
        IEnumerable<TType> GetItems(DirectoryInfo folder);
    }
}