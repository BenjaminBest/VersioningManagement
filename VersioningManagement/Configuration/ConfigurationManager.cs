using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace VersioningManagement.Configuration
{
    /// <summary>
    /// The class ConfigurationManager takes care about loading and storing the configuration
    /// </summary>
    public class ConfigurationManager
    {
        /// <summary>
        /// Reads the configuration from file.
        /// </summary>
        public IConfiguration Read()
        {
            var config = new Configuration();
            if (File.Exists("configuration.json"))
            {
                var json = File.ReadAllText("configuration.json");
                config = JsonConvert.DeserializeObject<Configuration>(json);
            }

            return config;
        }

        /// <summary>
        /// Writes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void Write(IConfiguration configuration)
        {
            var json = JsonConvert.SerializeObject(configuration, Formatting.Indented);

            File.WriteAllText("configuration.json", json);
        }

        /// <summary>
        /// Generates the schema.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public string GenerateSchema(IConfiguration configuration)
        {
            var generator = new JSchemaGenerator();
            var schema = generator.Generate(typeof(Configuration), false);

            schema.Title = typeof(Configuration).Name;

            return schema.ToString();
        }

        /// <summary>
        /// Reads the embedded schema.
        /// </summary>
        /// <returns></returns>
        public JSchema ReadSchema()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "VersioningManagement.Configuration.ConfigurationSchema.json";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var schema = reader.ReadToEnd();
                return JSchema.Parse(schema);
            }
        }

        /// <summary>
        /// Returns true if the configuration is valid based on the schema
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {

            if (File.Exists("configuration.json"))
            {
                var json = File.ReadAllText("configuration.json");
                var configuration = JObject.Parse(json);

                return configuration.IsValid(ReadSchema());
            }

            return false;
        }
    }
}
