using System.Text.RegularExpressions;

namespace VersioningManagement.Versions
{
    /// <summary>
    /// The class VersionChanger is used to extract a version from string and change parts of the version. The .NET version class is not capable of using asteriks in the Version class.
    /// </summary>
    public class VersionChanger
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionChanger"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        public VersionChanger(string version)
        {
            ParseFromString(version, out int major, out int minor, out int revision, out int build);
            Version = version;
        }

        /// <summary>
        /// Tries to parse the <param name="version"></param>.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="parsedVersion">The parsed version.</param>
        /// <returns></returns>
        public static bool TryParse(string version, out VersionChanger parsedVersion)
        {
            ParseFromString(version, out int major, out int minor, out int revision, out int build);

            parsedVersion = null;

            if (major == -1)
                return false;

            if (minor == -1 && revision != -1)
                return false;

            if (revision == -1 && build != -1)
                return false;

            if (major == int.MaxValue)
                return false;

            if (((minor == int.MaxValue ? 1 : 0) + (revision == int.MaxValue ? 1 : 0) +
                 (build == int.MaxValue ? 1 : 0)) > 1)
                return false;

            parsedVersion = new VersionChanger(ToString(major, minor, revision, build));

            return true;
        }

        /// <summary>
        /// Increases the version at the given <paramref name="part"/>.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <returns></returns>
        public void IncreaseVersion(VersionPart part)
        {
            ParseFromString(Version, out int major, out int minor, out int revision, out int build);

            switch (part)
            {
                case VersionPart.Major:
                    if (major != int.MaxValue && major != -1)
                        major++;
                    break;
                case VersionPart.Minor:
                    if (minor != int.MaxValue && minor != -1)
                        minor++;
                    break;
                case VersionPart.Revision:
                    if (revision != int.MaxValue && revision != -1)
                        revision++;
                    break;
                case VersionPart.Build:
                    if (build != int.MaxValue && build != -1)
                        build++;
                    break;
            }

            Version = ToString(major, minor, revision, build);
        }

        /// <summary>
        /// Decreases the version at the given <paramref name="part"/>.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <returns></returns>
        public void DecreaseVersion(VersionPart part)
        {
            ParseFromString(Version, out int major, out int minor, out int revision, out int build);

            switch (part)
            {
                case VersionPart.Major:
                    if (major != int.MaxValue && major != 0 && major != -1)
                        major--;
                    break;
                case VersionPart.Minor:
                    if (minor != int.MaxValue && minor != 0 && minor != -1)
                        minor--;
                    break;
                case VersionPart.Revision:
                    if (revision != int.MaxValue && revision != 0 && revision != -1)
                        revision--;
                    break;
                case VersionPart.Build:
                    if (build != int.MaxValue && build != 0 && build != -1)
                        build--;
                    break;
            }

            Version = ToString(major, minor, revision, build);
        }

        /// <summary>
        /// Parses the string and assignes the version parts to the given parameters.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="major">The major.</param>
        /// <param name="minor">The minor.</param>
        /// <param name="revision">The revision.</param>
        /// <param name="build">The build.</param>
        internal static void ParseFromString(string input, out int major, out int minor, out int revision, out int build)
        {
            major = ParseGroup(input, @"^[0-9]+", 0);
            minor = ParseGroup(input, @"^[0-9]+\.([0-9]+|\*)", 1);
            revision = ParseGroup(input, @"^[0-9]+\.[0-9]+\.([0-9]+|\*)", 1);
            build = ParseGroup(input, @"^[0-9]+\.[0-9]+\.[0-9]+\.([0-9]+|\*)", 1);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="major">The major.</param>
        /// <param name="minor">The minor.</param>
        /// <param name="revision">The revision.</param>
        /// <param name="build">The build.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        internal static string ToString(int major, int minor, int revision, int build)
        {
            var value = string.Empty;

            if (major != -1)
            {
                value += $"{major}";
            }
            else
            {
                return value;
            }

            if (minor != -1)
            {
                if (minor == int.MaxValue)
                {
                    value += $".*";
                    return value;
                }
                else
                {
                    value += $".{minor}";
                }
            }
            else
            {
                return value;
            }

            if (revision != -1)
            {
                if (revision == int.MaxValue)
                {
                    value += $".*";
                    return value;
                }
                else
                {
                    value += $".{revision}";
                }
            }
            else
            {
                return value;
            }

            if (build != -1)
            {
                if (build == int.MaxValue)
                {
                    value += $".*";
                }
                else
                {
                    value += $".{build}";
                }
            }

            return value;
        }

        /// <summary>
        /// Parses the regex group.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="regex">The regex.</param>
        /// <param name="group">The group.</param>
        /// <returns>-1 is the group is not valid or was not found, <c>int.MaxValue</c> if the group contains an *</returns>
        private static int ParseGroup(string input, string regex, int group)
        {
            int output;

            var valid = int.TryParse(Regex.Match(input, regex, RegexOptions.IgnoreCase).Groups[group].Value, out output);

            //Hack to parse asteriks
            if (!valid & Regex.Match(input, regex, RegexOptions.IgnoreCase).Groups[group].Value.Equals("*"))
            {
                output = int.MaxValue;
                valid = true;
            }

            return valid ? output : -1;
        }
    }
}
