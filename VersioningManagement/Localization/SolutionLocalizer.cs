using System.Collections.Generic;
using System.IO;
using System.Linq;
using VersioningManagement.Configuration;
using VersioningManagement.Roslyn;

namespace VersioningManagement.Localization
{
    /// <summary>
    /// Searches for solution files in the given folder
    /// </summary>
    public class SolutionLocalizer : LocalizerBase, ILocalizer<SolutionInfo>
    {
        /// <summary>
        /// The configuration manager
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionLocalizer"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public SolutionLocalizer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns></returns>
        public IEnumerable<SolutionInfo> GetItems(DirectoryInfo folder)
        {
            var solutions = new List<SolutionInfo>();

            var files = new List<FileInfo>();
            GetAllFiles(folder, ref files, _configuration.SolutionExtension);

            foreach (var file in files)
            {
                var solution = WorkspaceHelper.GetSolution(file.FullName);

                solutions.Add(new SolutionInfo(file, solution.ToString(), solution.Result.Projects.Filter(_configuration.ProjectsRegexFilter)
                    .Select(project => new ProjectInfo(new FileInfo(project.FilePath), project.Name))));
            }

            return solutions;
        }
    }
}
