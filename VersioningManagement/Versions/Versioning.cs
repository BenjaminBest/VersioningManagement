namespace VersioningManagement.Versions
{
    /// <summary>
    /// The class Versioning is used to extract a version from string and change parts of the version. The .NET version class is not capable of using asteriks in the Version class.
    /// </summary>
    public class Versioning
    {
        /// <summary>
        /// Gets the major.
        /// </summary>
        /// <value>
        /// The major.
        /// </value>
        public int Major { get; private set; }

        /// <summary>
        /// Gets the minor.
        /// </summary>
        /// <value>
        /// The minor.
        /// </value>
        public int Minor { get; private set; }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        /// <value>
        /// The revision.
        /// </value>
        public int Revision { get; private set; }

        /// <summary>
        /// Gets the build. If set to int.MaxValue an asterik is used
        /// </summary>
        /// <value>
        /// The build.
        /// </value>
        public int Build { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Versioning"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        public Versioning(string version)
        {

        }

        public static bool TryParse(string version, out Versioning parsedVersion)
        {
            //TODO: Just use the old version and take care about *
            parsedVersion = new Versioning("1.0.0");

            return false;
        }

        /// <summary>
        /// Increases the major version if the version is valid. If not, the original string will be returned
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static string IncreaseMajorVersion(string version)
        {
            return version;

            //if (string.IsNullOrEmpty(version))
            //    return version;

            //if (!TryParse(version, out Versioning oldVersion))
            //    return version;

            //var newVersion = string.Empty;

            //if (oldVersion.Major != default(int))
            //    newVersion += $"{oldVersion.Major + 1}";

            //if (oldVersion.Minor != default(int))
            //    newVersion += $"{oldVersion.Minor}";

            //if (oldVersion.Revision != default(int))
            //    newVersion += $"{oldVersion.Revision}";

            //if (oldVersion.Build != default(int))
            //    newVersion += $"{oldVersion.Build}";

            //return newVersion;
        }

        /// <summary>
        /// Increases the minor if the version is valid. If not, the original string will be returned
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static string IncreaseMinorVersion(string version)
        {
            return version;
        }

        /// <summary>
        /// Increases the revision if the version is valid. If not, the original string will be returned
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static string IncreaseRevisionVersion(string version)
        {
            return version;
        }

        /// <summary>
        /// Decreases the major version if the version is valid. If not, the original string will be returned
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static string DecreaseMajorVersion(string version)
        {
            return version;
        }

        /// <summary>
        /// Decreases the minor if the version is valid. If not, the original string will be returned
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static string DecreaseMinorVersion(string version)
        {
            return version;
        }

        /// <summary>
        /// Decreases the revision if the version is valid. If not, the original string will be returned
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static string DecreaseRevisionVersion(string version)
        {
            return version;
        }
    }
}
