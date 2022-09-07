using ConfigurationUtils;
using GeneralUtils;

/// <summary>
/// Configuration setup example
/// </summary>
namespace ExampleConfigApp
{
    /// <summary>
    /// The config file keys as an enum:
    /// a.  Strong typing: helps avoid issues associated with the string key appoach
    /// b.  Clarity: Clearifies the options associated with a process
    /// </summary>
    public enum TextProcessing
    {
        SourceFolder,
        TargetFolder,
        TempProcessingFolder,
        ArchiveFolder,
        PauseBetweenTextFiles
    }

    /// <summary>
    /// Builder class for TextProcessingConfigurationBuilder
    /// </summary>
    public class TextProcessingConfigurationBuilder
    {
        public ApplicationConfiguration Build()
        {
            ConfigurationRulesBuilder builder = ConfigurationRulesBuilder.NewBuilder("Text Processing");
            return builder.SetConfigurationFolder(configurationFolder)
                .SetConfigurationName(configurationFilename)
                .AddField(TextProcessing.SourceFolder, Condition.Required, ParamType.Folder)
                .AddField(TextProcessing.TargetFolder, Condition.Required, ParamType.Folder)
                .AddField(TextProcessing.TempProcessingFolder, Condition.Required, ParamType.Folder)
                .AddField(TextProcessing.ArchiveFolder, Condition.Required, ParamType.Folder)
                .AddField(TextProcessing.PauseBetweenTextFiles, Condition.Optional, ParamType.Number)
                .Build();
        }

        /// <summary>
        /// Location of the configuration files.
        /// </summary>
        public TextProcessingConfigurationBuilder SetConfigurationFolder(string configurationFolder)
        {
            MethodUtils.NotNull(configurationFolder, "configurationFolder cannot be null");
            this.configurationFolder = configurationFolder;
            return this;
        }

        /// <summary>
        /// Override the configuration file name.  Default will be System.Diagnostics.Process.GetCurrentProcess().ProcessName
        /// </summary>
        public TextProcessingConfigurationBuilder SetConfigurationName(string configurationFilename)
        {
            MethodUtils.NotNull(configurationFilename, "configurationFilename cannot be null");
            this.configurationFilename = configurationFilename;
            return this;
        }

        private string? configurationFolder;
 
        private string configurationFilename = "";
    }
}