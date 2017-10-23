using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace VersioningManagement.Versions
{
    /// <summary>
    /// The class AssemblyInfoVersion contains the version information from a assemblyInfo.cs file
    /// </summary>
    public class AssemblyInfoVersion
    {
        //private static readonly Regex AssemblyInfoRegex = new Regex(@"(\[assembly\:\s*)(Assembly(File)?Version)(\(""\d+\.\d+\.\d+)((\.(\d+|\*))?)(""\)])");
        private static readonly Regex AssemblyInfoRegex = new Regex(@"(\[assembly\:\s*)(Assembly(File)?Version)(\("")(.*)(""\)])");

        /// <summary>
        /// The file
        /// </summary>
        public FileInfo File { get; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInfoVersion"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public AssemblyInfoVersion(FileInfo file)
        {
            File = file;

            Read();
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        public void Read()
        {
            if (!File.Exists)
                return;

            using (var sr = File.OpenText())
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null)
                    {
                        continue;
                    }

                    if (AssemblyInfoRegex.IsMatch(line))
                    {
                        var matches = AssemblyInfoRegex.Matches(line);
                        if (matches.Count == 1)
                        {
                            var groups = matches[0];
                            if (groups.Groups.Count == 7)
                            {
                                Version = groups.Groups[5].Value;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Writes this instance.
        /// </summary>
        public void Write()
        {
            if (!File.Exists)
                return;

            var sb = new StringBuilder();

            using (var sr = File.OpenText())
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null)
                    {
                        continue;
                    }

                    if (AssemblyInfoRegex.IsMatch(line))
                    {
                        line = UpdateVersion(line, Version);
                    }

                    sb.AppendLine(line);
                }
            }

            System.IO.File.WriteAllText(File.FullName, sb.ToString());
        }

        /// <summary>
        /// Updates the version.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="version">The buildnumber.</param>
        /// <returns></returns>
        private static string UpdateVersion(string line, string version)
        {
            var matches = AssemblyInfoRegex.Matches(line);
            if (matches.Count == 1)
            {
                var groups = matches[0];
                if (groups.Groups.Count == 7)
                {
                    return
                        $"{groups.Groups[1].Value}{groups.Groups[2].Value}{groups.Groups[4].Value}{version}{groups.Groups[6].Value}";
                }
            }

            return line;
        }
    }
}
