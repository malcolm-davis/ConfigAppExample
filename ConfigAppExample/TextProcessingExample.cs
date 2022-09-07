using ConfigurationUtils;
using System;

namespace ExampleConfigApp
{
    public class TextProcessingExample
    {
        public static TextProcessingExample NewProcessor(ApplicationConfiguration configuration)
        {
            return new TextProcessingExample(configuration);
        }
        public void Execute()
        {
            // Print configuration values
            // Any field containing the phrase password is ignored and does not show in the ToString()
            Console.WriteLine(configuration.ToString());
            Console.WriteLine();

            // Configuration values can be accessed multiple ways, via enum or string key
            Console.WriteLine("configuration[enum key]=" + configuration[TextProcessing.SourceFolder]);
            
            Console.WriteLine("configuration.Int(enum key, default)=" +
                configuration.Int(TextProcessing.PauseBetweenTextFiles, 5));
            
            Console.WriteLine("configuration.Int(string key, default)=" +
                configuration.Int("TextProcessing.PauseBetweenTextFiles", 5));

            Console.WriteLine("configuration.ValueSafe(key, default)=" +
                configuration.ValueSafe("TextProcessing.SourceFolder", "a default value"));
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        private TextProcessingExample(ApplicationConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private readonly ApplicationConfiguration configuration;
    }
}
