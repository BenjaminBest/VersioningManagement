using System;
using System.IO;

namespace VersioningManagement.Versions
{
    /// <summary>
    /// The class AssemblyInfoVersion contains the version information from a assemblyInfo.cs file
    /// </summary>
    public class AssemblyInfoVersion
    {
        /// <summary>
        /// The file
        /// </summary>
        private readonly FileInfo _file;

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public Version Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInfoVersion"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public AssemblyInfoVersion(FileInfo file)
        {
            _file = file;

            Read();
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        public void Read()
        {
            // open the file
            var contents = File.ReadAllLines(_file.FullName);

            // find the attribute
            var versionLine = string.Empty;
            var versionLineNumber = 0;
            const string attribute = "AssemblyVersion";

            for (var i = 0; i < contents.Length; i++)
            {
                var content = contents[i];
                if (content.Contains("[assembly: " + attribute + "(") && !content.StartsWith("//"))
                {
                    versionLineNumber = i;
                    versionLine = content;
                    break;
                }
            }

            // extract the version number info from the line
            // assumes the version number info is contained in a quoted string in some brackets (it should be!)
            var version = versionLine.Substring(versionLine.IndexOf("(\"", StringComparison.Ordinal) + 2);
            Version = new Version(version.Substring(0, version.LastIndexOf("\")", StringComparison.Ordinal)));
        }

        /// <summary>
        /// Writes this instance.
        /// </summary>
        public void Write()
        {

        }
    }
}
