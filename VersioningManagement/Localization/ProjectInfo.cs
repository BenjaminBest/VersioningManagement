using System.IO;

namespace VersioningManagement.Localization
{
    /// <summary>
    /// Defines the available information about a project
    /// </summary>
    public class ProjectInfo
    {
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public FileInfo File { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInfo"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="name">The name.</param>
        public ProjectInfo(FileInfo file, string name)
        {
            File = file;
            Name = name;
        }
    }
}
