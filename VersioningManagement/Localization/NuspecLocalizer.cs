using System.Collections.Generic;
using System.IO;
using System.Linq;
using VersioningManagement.Configuration;

namespace VersioningManagement.Localization
{
    /// <summary>
    /// The NuspecLocalizer localizes a nuspec file and returns the information
    /// </summary>
    /// <seealso cref="LocalizerBase" />
    /// <seealso cref="Localization.ILocalizer{NuspecInfo}" />
    public class NuspecLocalizer : LocalizerBase, ILocalizer<NuspecInfo>
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="NuspecLocalizer"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public NuspecLocalizer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns></returns>
        public IEnumerable<NuspecInfo> GetItems(DirectoryInfo folder)
        {
            var files = new List<FileInfo>();
            GetAllFiles(folder, ref files, _configuration.NuspecExtension);

            return !files.Any() ? new List<NuspecInfo>() : new List<NuspecInfo>() { new NuspecInfo(files.FirstOrDefault()) };
        }
    }
}
