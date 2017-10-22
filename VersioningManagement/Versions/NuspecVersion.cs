using System.IO;
using NuGet;

namespace VersioningManagement.Versions
{
    /// <summary>
    /// The class NuspecVersion contains the version information from a nuspec file
    /// </summary>
    public class NuspecVersion
    {
        /// <summary>
        /// The file
        /// </summary>
        public FileInfo File { get; }

        /// <summary>
        /// The manifest
        /// </summary>
        private Manifest _manifest;

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NuspecVersion"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public NuspecVersion(FileInfo file)
        {
            File = file;
            Read();
        }

        /// <summary>
        /// Reads this instance based on a nuspec file
        /// </summary>
        public void Read()
        {
            if (!System.IO.File.Exists(File.FullName))
                return;

            using (Stream stream = System.IO.File.Open(File.FullName, FileMode.Open))
            {
                _manifest = Manifest.ReadFrom(stream, false);
                Version = _manifest.Metadata.Version;
            }
        }

        /// <summary>
        /// Writes the specified file.
        /// </summary>
        public void Write()
        {
            _manifest.Metadata.Version = Version;

            using (var stream = System.IO.File.Open(File.FullName, FileMode.OpenOrCreate))
            {
                _manifest.Save(stream);
            }
        }
    }
}
