using System.Collections.Generic;

namespace VersioningManagement.Configuration
{
    /// <summary>
    /// The class Configuration contains all available configuration as well as automatically stored values during usage of the program to increase usability
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Gets or sets the recent localized paths for solution files
        /// </summary>
        /// <value>
        /// The recent localized paths.
        /// </value>
        public List<string> RecentLocalizedPaths { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            RecentLocalizedPaths = new List<string>();
        }
    }
}
