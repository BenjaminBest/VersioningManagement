using System.Collections.Generic;

namespace VersioningManagement.Configuration
{
    /// <summary>
    /// The interface defines a configuration that contains all available configuration as well as automatically
    /// stored values during usage of the program to increase usability
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets or sets the recent localized paths for solution files
        /// </summary>
        /// <value>
        /// The recent localized paths.
        /// </value>
        List<string> RecentLocalizedPaths { get; set; }

        /// <summary>
        /// Gets or sets the project filter to exclude specific projects based on a regex.
        /// </summary>
        /// <value>
        /// The project filter.
        /// </value>
        string ProjectsRegexFilter { get; set; }

        /// <summary>
        /// Gets or sets the solution extension to find all solution files
        /// </summary>
        /// <value>
        /// The solution extension.
        /// </value>
        string SolutionExtension { get; set; }

        /// <summary>
        /// Gets or sets the nuspec extension to find the nuspec files
        /// </summary>
        /// <value>
        /// The nuspec extension.
        /// </value>
        string NuspecExtension { get; set; }

        /// <summary>
        /// Copies this configuration to the given <paramref name="copy"/>
        /// </summary>
        /// <param name="copy">The copy.</param>
        void CopyTo(IConfiguration copy);
    }
}