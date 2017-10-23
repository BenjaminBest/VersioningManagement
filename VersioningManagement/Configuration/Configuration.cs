using System.Collections.Generic;

namespace VersioningManagement.Configuration
{
    /// <summary>
    /// The class Configuration contains all available configuration as well as automatically stored values during usage of the program to increase usability
    /// </summary>
    public class Configuration : IConfiguration
    {
        /// <summary>
        /// Gets or sets the recent localized paths for solution files
        /// </summary>
        /// <value>
        /// The recent localized paths.
        /// </value>
        public List<string> RecentLocalizedPaths { get; set; }

        /// <summary>
        /// Gets or sets the project filter to exclude specific projects based on a regex.
        /// </summary>
        /// <value>
        /// The project filter.
        /// </value>
        public string ProjectsRegexFilter { get; set; }

        /// <summary>
        /// Gets or sets the solution extension to find all solution files
        /// </summary>
        /// <value>
        /// The solution extension.
        /// </value>
        public string SolutionExtension { get; set; }

        /// <summary>
        /// Gets or sets the nuspec extension to find the nuspec files
        /// </summary>
        /// <value>
        /// The nuspec extension.
        /// </value>
        public string NuspecExtension { get; set; }

        /// <summary>
        /// Gets or sets the nuspec namespace.
        /// </summary>
        /// <value>
        /// The nuspec namespace.
        /// </value>
        public string NuspecXmlNamespace { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            RecentLocalizedPaths = new List<string>();
        }

        /// <summary>
        /// Copies this configuration to the given <paramref name="copy"/>
        /// </summary>
        /// <param name="copy">The copy.</param>
        public void CopyTo(IConfiguration copy)
        {
            copy.RecentLocalizedPaths = RecentLocalizedPaths;
            copy.ProjectsRegexFilter = ProjectsRegexFilter;
            copy.SolutionExtension = SolutionExtension;
            copy.NuspecExtension = NuspecExtension;
            copy.NuspecXmlNamespace = NuspecXmlNamespace;
        }
    }
}
