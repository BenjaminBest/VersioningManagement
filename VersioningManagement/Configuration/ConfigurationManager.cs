using System.IO;
using Newtonsoft.Json;

namespace VersioningManagement.Configuration
{
    /// <summary>
    /// The class ConfigurationManager takes care about loading and storing the configuration
    /// </summary>
    public class ConfigurationManager : IConfigurationManager
    {
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public Configuration Configuration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationManager"/> class.
        /// </summary>
        public ConfigurationManager()
        {
            Read();
        }

        /// <summary>
        /// Reads the configuration from file.
        /// </summary>
        public void Read()
        {
            var config = new Configuration();
            if (File.Exists("configuration.json"))
            {
                var json = File.ReadAllText("configuration.json");
                config = JsonConvert.DeserializeObject<Configuration>(json);
            }

            Configuration = config;
        }

        /// <summary>
        /// Writes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void Write(Configuration configuration)
        {
            var json = JsonConvert.SerializeObject(configuration, Formatting.Indented);

            File.WriteAllText("configuration.json", json);

            Read();
        }

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        public void Write()
        {
            Write(Configuration);
        }
    }
}
