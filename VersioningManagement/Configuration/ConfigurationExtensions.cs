namespace VersioningManagement.Configuration
{
    /// <summary>
    /// The class ConfigurationExtensions contains extension methods for Configuration 
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// The configuration manager
        /// </summary>
        private static readonly ConfigurationManager ConfigurationManager;

        /// <summary>
        /// Initializes the <see cref="ConfigurationExtensions"/> class.
        /// </summary>
        static ConfigurationExtensions()
        {
            ConfigurationManager = new ConfigurationManager();
        }

        /// <summary>
        /// Persists the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public static void Write(this IConfiguration configuration)
        {
            ConfigurationManager.Write(configuration);
        }

        /// <summary>
        /// Reads from persistence and updates the given <paramref name="configuration"/>
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public static void Read(this IConfiguration configuration)
        {
            ConfigurationManager.Read().CopyTo(configuration);
        }
    }
}
