using System.IO;
using VersioningManagement.Versions;

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
        /// Gets the assembly information version.
        /// </summary>
        /// <value>
        /// The assembly information version.
        /// </value>
        public AssemblyInfoVersion AssemblyInfoVersion { get; }

        /// <summary>
        /// Gets the nuspec version.
        /// </summary>
        /// <value>
        /// The nuspec version.
        /// </value>
        public NuspecVersion NuspecVersion { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInfo" /> class.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="name">The name.</param>
        /// <param name="assemblyInfoVersion">The assembly information version.</param>
        /// <param name="nuspecVersion">The nuspec version.</param>
        public ProjectInfo(FileInfo file, string name, AssemblyInfoVersion assemblyInfoVersion, NuspecVersion nuspecVersion)
        {
            File = file;
            Name = name;
            AssemblyInfoVersion = assemblyInfoVersion;
            NuspecVersion = nuspecVersion;
        }
    }
}
