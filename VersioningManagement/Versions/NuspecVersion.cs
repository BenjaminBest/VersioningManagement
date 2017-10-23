using System.IO;
using System.Xml;
using VersioningManagement.Configuration;
using VersioningManagement.DependencyInjection;
using VersioningManagement.Helpers;

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
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// The XML document
        /// </summary>
        private XmlDocument _xmlDocument;

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
            if (!File.Exists)
                return;

            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(File.FullName);

            var nsmgr = new XmlNamespaceManager(_xmlDocument.NameTable);
            nsmgr.AddNamespace("nu", ServiceLocator.Get<IConfiguration>().NuspecXmlNamespace);

            Version = _xmlDocument.SelectSingleNode("//nu:package/nu:metadata/nu:version", nsmgr).IsNotNull(o => o.InnerText);
        }

        /// <summary>
        /// Writes the specified file.
        /// </summary>
        public void Write()
        {
            if (!File.Exists)
                return;

            var nsmgr = new XmlNamespaceManager(_xmlDocument.NameTable);
            nsmgr.AddNamespace("nu", ServiceLocator.Get<IConfiguration>().NuspecXmlNamespace);

            _xmlDocument.SelectSingleNode("//nu:package/nu:metadata/nu:version", nsmgr).IsNotNull(o => o.InnerText = Version);
            _xmlDocument.Save(File.FullName);
        }
    }
}
