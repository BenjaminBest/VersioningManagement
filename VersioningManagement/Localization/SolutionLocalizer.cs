using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using VersioningManagement.Configuration;
using VersioningManagement.Roslyn;
using VersioningManagement.Versions;

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
        /// The localizer registry
        /// </summary>
        private readonly ILocalizerRegistry _localizerRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionLocalizer"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="localizerRegistry"></param>
        public SolutionLocalizer(IConfiguration configuration, ILocalizerRegistry localizerRegistry)
        {
            _configuration = configuration;
            _localizerRegistry = localizerRegistry;
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

                solutions.Add(new SolutionInfo(file, solution.Result.ToString(), solution.Result.Projects
                    .Filter(_configuration.ProjectsRegexFilter)
                    .Select(GetProject)));
            }

            return solutions;
        }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        private ProjectInfo GetProject(Project project)
        {
            var nuspecLocalizer = _localizerRegistry.CreateLocalizer<NuspecInfo>();

            var projectFile = new FileInfo(project.FilePath);

            var nuspec = nuspecLocalizer.GetItems(projectFile.Directory).FirstOrDefault();
            var nuspecVersion = nuspec != null ? new NuspecVersion(nuspec.File) : null;

            var assemblyInfoVersion =
                new AssemblyInfoVersion(new FileInfo(projectFile.Directory + @"\Properties\AssemblyInfo.cs"));

            return new ProjectInfo(projectFile, project.Name, assemblyInfoVersion, nuspecVersion);
        }
    }
}
