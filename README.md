ConfigAppExample
========

ConfigAppExample demonstrates the ConfigurationUtils feature in GeneralUtilsLib.

## Overview

**TextProcessingConfigurationBuilder.cs** - Shows how a configuration class might be constructed.

**Keys** - 
The configuration file keys are represented as C# enum rather than strings.  Strings can still be used to access config values but are not required.
```
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
```
<br/>

**Rules** - The ConfigurationRulesBuilder standardizes the validation approach by specifying the type of each config value and if the value is optional or required.  
```
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
```
<br/>

**Filename** - Standardized configuration filename helps specify a configuration per-server, which helps with source control management and security.  
The file naming structure is: ``` Environment.MachineName + . + Process.GetCurrentProcess().ProcessName + .ini```
<br/>
Example, ```DESKTOP-V8PLC41.ConfigAppExample.ini```
<br/>
The Process.GetCurrentProcess().ProcessName + .ini can be overwritten in code as demonstrated in the example project.
<br/>

**VerifyOnly** - A VerifyOnly can be passed into the Program.cs.  The VerifyOnly option demonstrates how a program can provide a verification feature to the Admin to confirm the correct configuration on a server before turning on the application.
