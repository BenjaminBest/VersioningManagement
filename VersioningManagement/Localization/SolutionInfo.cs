using System.Collections.Generic;
using System.IO;

namespace VersioningManagement.Localization
{
    /// <summary>
    /// Defines the available information about a solution
    /// </summary>
    public class SolutionInfo
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
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public IEnumerable<ProjectInfo> Projects { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionInfo" /> class.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="name">The name.</param>
        /// <param name="projects">The projects in the solution.</param>
        public SolutionInfo(FileInfo file, string name, IEnumerable<ProjectInfo> projects)
        {
            File = file;
            Name = name;
            Projects = projects;
        }
    }
}
