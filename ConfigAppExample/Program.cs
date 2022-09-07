// See https://aka.ms/new-console-template for more information
using ConfigurationUtils;
using ExampleConfigApp;

// TextProcessingConfigurationBuilder contains the ConfigurationRulesBuilder
// ConfigurationRulesBuilder makes it easy to setup the configuration keys and expected value types
// The following rule says that TextProcessing.SourceFolder is required, and the type to verify is a folder
// Example rule: AddField(TextProcessing.SourceFolder, Condition.Required, ParamType.Folder)
// Config entry: TextProcessing.SourceFolder=c:\required_field_to_a_valid_path

// The purpose of the ApplicationConfiguration is to make it easy to create computer specific configuration files.
// The example creates a TextProcessing.ini with sample values
// The TextProcessing.ini will be prefixed with the machinename. 
string configFilename = ConfigurationUtils.ApplicationConfiguration.ConfigName("TextProcessing.ini");

// The following creates a default config file if one does not exist
string appPath = Directory.GetCurrentDirectory();
string configFullpath = Path.Combine(appPath, configFilename);
if (!File.Exists(configFullpath))
{
    string[] configurationFile =
    {
        "; comment lines start with ; ",
        "TextProcessing.SourceFolder="+appPath,
        "TextProcessing.TargetFolder="+appPath,
        "TextProcessing.TempProcessingFolder="+appPath,
        "TextProcessing.ArchiveFolder="+appPath,
        "TextProcessing.PauseBetweenTextFiles=1",
        "; Additional configuration for other processes can be included in the same config file ",
        "BatchProcessing.SourceFolder="+appPath,
    };

    // Write the default config file
    File.WriteAllLines(configFullpath, configurationFile);
}

try
{
    // By default, the current process name is part of the configuration filename.
    // SetConfigurationName(filename) allows the developer to override the default.
    ApplicationConfiguration appConfig = new TextProcessingConfigurationBuilder()
        .SetConfigurationFolder(appPath)
        .SetConfigurationName("TextProcessing.ini").Build();

    // If you only want to just verify the config, there is nothing left to do
    if (args.Length > 0 && string.Equals(args[0], "VerifyOnly", StringComparison.CurrentCultureIgnoreCase))
    {
        Console.WriteLine(configFilename + " verified.");
        return 0;
    }

    // The appConfig is passed to the text processing.
    // See the Execute() method on ways to access the config values.
    TextProcessingExample.NewProcessor(appConfig).Execute();
} 
catch(Exception e)
{
    Console.WriteLine("Validation failed for file=" + configFullpath);
    Console.WriteLine(e.Message);
    Console.WriteLine("Config failed verification, please verify and fix values.");
    return 0;
}

return 1;