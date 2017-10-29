Versioning Management
=====================================
This lightweight WPF application can be usefull when one or multiple solutions have lots of projects whose version needs to be manually maintained.

Of course one can use automatic versioning (e.g. semantic versioning based on breaking changes detection) as part of the DI pipeline, but that might not be possible for every project.

The tool search for every solution within a folder and then analyzes every solution including projects. It searches for the `AssemblyInfo`-files and [NuGet manifests](https://docs.microsoft.com/en-us/nuget/schema/nuspec).

An easy to use UI is generated which allows easy and fast version changes.

![UI](https://cloud.githubusercontent.com/assets/29073072/26785873/c5aa72a4-4a04-11e7-8e63-412f59c5c51a.png)

Implementation
--------------
### Configuration
The configuration is stored in a `JSON`-file which also is used to remember the last searched folders. Besides this, the configuration contains settings to exclude test-projects, the search pattern for nuspecs and sln files as well as an identifier for pre-releases.

```json
{
  "RecentLocalizedPaths": [
    "F:\\Folder\\SearchedFolder",
    "F:\\Folder\\Folder\\SearchedFolder"
  ],
  "ProjectsRegexFilter": "\\.Tests",
  "SolutionExtension": "*.sln",
  "NuspecExtension": "*.nuspec",
  "NuspecXmlNamespace": "http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd",
  "PreReleaseIdentifier": "-pre"
}
```

The configuration is validated per JSON schema. [Json.NET Schema](https://www.newtonsoft.com/jsonschema) is used here because it is easy to use and fast developed and maintained.

To validate the configuration a few line of code are enough:

```csharp
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
```

The programatic generation of the schema is easy as well:

```csharp
public string GenerateSchema()
{
    var generator = new JSchemaGenerator();
    var schema = generator.Generate(typeof(Configuration), false);

    schema.Title = typeof(Configuration).Name;

    return schema.ToString();
}
```

### Locate project files
With a recursive method all files that matches the pattern `*.sln` are found, then [roslyn](https://github.com/dotnet/roslyn) is used to get the project files:

```csharp
public static async Task<IEnumerable<Project>> GetProjects(string solutionPath)
{
    var workspace = MSBuildWorkspace.Create();
    var solution = await workspace.OpenSolutionAsync(solutionPath);

    return solution.Projects;
}
```

It's important to know what `NuGet` packages are needed to make it work. The above code just returns nothing but will not crash if some packages are missing:
1. Microsoft.CodeAnalysis.Analyzers
2. Microsoft.CodeAnalysis.Common
3. Microsoft.CodeAnalysis.CSharp
4. Microsoft.CodeAnalysis.CSharp.Workspaces
5. Microsoft.CodeAnalysis.Workspaces.Common

### Version
TODO

### Change versions
#### AssemblyInfo
To identify the AssemblyVersion and AssemblyFileVersion attributes
```csharp
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
```
a regex is used

```csharp
@"(\[assembly\:\s*)(Assembly(File)?Version)(\("")(.*)(""\)])"
```

The file is read line by line and the regex is used to either match or update the value.

#### NuSpec
It was not possible to use [NuGet.Core](https://www.nuget.org/packages/NuGet.Core/) because it left an invalid manifest file behind when it wrote to the file. Somehow when e.g. the version was reduced from `1.0.0.0` to `1.0.0` the XML in the nuspec-file was not completely overwritten. When I removed a chanracter in the version field, one character stayed in the file, so that it ended with e.g. `</package>>` instead of `</package>`.

So I used a regular XMLDocument parser instead.

Reading the `version` XML node from nuspec-file:

```csharp
public void Read()
{
    if (!File.Exists)
        return;

    _xmlDocument = new XmlDocument();
    _xmlDocument.Load(File.FullName);

    var nsmgr = new XmlNamespaceManager(_xmlDocument.NameTable);
    nsmgr.AddNamespace("nu", ServiceLocator.Get<IConfiguration>().NuspecXmlNamespace);

    Version = _xmlDocument.SelectSingleNode("//nu:package/nu:metadata/nu:version", nsmgr).IsNotNull(o => o.InnerText);
}
```

Writing the manifest with an updated `version` XML node to the nuspec-file:

```csharp
public void Write()
{
    if (!File.Exists)
        return;

    var nsmgr = new XmlNamespaceManager(_xmlDocument.NameTable);
    nsmgr.AddNamespace("nu", ServiceLocator.Get<IConfiguration>().NuspecXmlNamespace);

    _xmlDocument.SelectSingleNode("//nu:package/nu:metadata/nu:version", nsmgr).IsNotNull(o => o.InnerText = Version);
    _xmlDocument.Save(File.FullName);
}
```


### WPF
The solution only contains one window which get's it's data by a viewmodel-structure. The `viewmodel` is located via a class called `ViewModelLocator`. It's using a ServiceLocator to get the viemodel which is bound in singleton-mode. 

#### Dependency Injection
#### ViewModel Locator
#### Design vs. Runtime viewmodels
#### Commands
