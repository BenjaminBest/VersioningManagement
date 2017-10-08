using System.IO;

namespace VersioningManagement.Localization
{
    /// <summary>
    /// The NuspecInfo holds information about a nuspec file
    /// </summary>
    public class NuspecInfo
    {
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public FileInfo File { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NuspecInfo"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public NuspecInfo(FileInfo file)
        {
            File = file;
        }
    }
}
