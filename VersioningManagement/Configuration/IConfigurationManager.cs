namespace VersioningManagement.Configuration
{
    /// <summary>
    /// The interface IConfigurationManager defines a manager to interact with the configuration
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        Configuration Configuration { get; set; }

        /// <summary>
        /// Reads the configuration from file.
        /// </summary>
        void Read();

        /// <summary>
        /// Writes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void Write(Configuration configuration);

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        void Write();
    }
}