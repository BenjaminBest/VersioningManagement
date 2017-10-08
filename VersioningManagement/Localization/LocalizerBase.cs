using System.Collections.Generic;
using System.IO;

namespace VersioningManagement.Localization
{
    /// <summary>
    /// Base class for all localizers
    /// </summary>
    public abstract class LocalizerBase
    {
        /// <summary>
        /// Gets all files in the given <paramref name="directory" /> recursivly
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="files">The files.</param>
        /// <param name="filter">The filter for the files.</param>
        protected static void GetAllFiles(DirectoryInfo directory, ref List<FileInfo> files, string filter)
        {
            //Top level files
            files.AddRange(directory.GetFiles(filter));

            //Directories
            foreach (var folder in directory.GetDirectories())
            {
                GetAllFiles(folder, ref files, filter);
            }
        }
    }
}