Versioning Management
=====================================
This lightweight WPF application can be useful when one or multiple solutions have lots of projects whose version needs to be manually maintained.

Of course one can use automatic versioning (e.g. semantic versioning based on breaking changes detection) as part of the DI pipeline, but that might not be possible for every project.

The tool search for every solution within a folder and then analyzes every solution including projects. It searches for the `AssemblyInfo`-files and [NuGet manifests](https://docs.microsoft.com/en-us/nuget/schema/nuspec).

An easy to use UI is generated which allows easy and fast version changes.

![UI](.png)

Implementation
--------------
### Configuration
The configuration is stored in a `JSON`-file which also is used to remember the last searched folders. Besides this, the configuration contains settings to exclude test-projects, the search pattern for nuspecs and sln files as well as an identifier for pre-releases.

```json
{
  "RecentLocalizedPaths": [
    "F:\\Folder\\SearchedFolder",
    "F:\\Folder\\Folder\\AnotherSearchedFolder"
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

The programmatic generation of the schema is easy as well:

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
The below described approach of reading and writing does only take care about that, but not of actually changing the version. For that a class `VersionChanger` was created.

The first approach was to use the original [.NET System.Version class](https://msdn.microsoft.com/de-de/library/system.version(v=vs.110).aspx), but it can't take an asterisk as the build-part of the version which is needed in the AssemblyInfo when MsBuild should generate a build-number.

Therefore a rudimentary custom class was created. It parses the version string and is able to identify all parts:

```csharp
internal static void ParseFromString(string input, out int major, out int minor, out int revision, out int build)
{
    major = ParseGroup(input, @"^[0-9]+", 0);
    minor = ParseGroup(input, @"^[0-9]+\.([0-9]+|\*)", 1);
    revision = ParseGroup(input, @"^[0-9]+\.[0-9]+\.([0-9]+|\*)", 1);
    build = ParseGroup(input, @"^[0-9]+\.[0-9]+\.[0-9]+\.([0-9]+|\*)", 1);
}

private static int ParseGroup(string input, string regex, int group)
{
    int output;

    var valid = int.TryParse(Regex.Match(input, regex, RegexOptions.IgnoreCase).Groups[group].Value, out output);

    //Hack to parse asterisks
    if (!valid & Regex.Match(input, regex, RegexOptions.IgnoreCase).Groups[group].Value.Equals("*"))
    {
        output = int.MaxValue;
        valid = true;
    }

    return valid ? output : -1;
}
```

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
It was not possible to use [NuGet.Core](https://www.nuget.org/packages/NuGet.Core/) because it left an invalid manifest file behind when it wrote to the file. Somehow when e.g. the version was reduced from `1.0.0.0` to `1.0.0` the XML in the nuspec-file was not completely overwritten. When I removed a character in the version field, one character stayed in the file, so that it ended with e.g. `</package>>` instead of `</package>`.

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

#### ViewModel Locator & Dependency Injection

The locator is registrated in the App.xaml

```csharp
<Application x:Class="VersioningManagement.App"
<Application.Resources>
<viewModel:ViewModelLocator x:Key="Locator" />
</Application.Resources>
</Application>
```
and then used as DataContext.

```csharp
<Window.DataContext>
    <Binding Path="MainWindowViewModel" Source="{StaticResource Locator}"></Binding>
</Window.DataContext>
```

The `viewmodel` is actually not stored in the located but retrieved by a ServiceLocator using ninject to make sure there is only one instance of the viewmodel.

```csharp
public class ViewModelLocator
{
    public MainWindowViewModel MainWindowViewModel => ServiceLocator.Get<MainWindowViewModel>();
}
```

But, so make all that working ninject needs also to be invoked loading the window.

```csharp
public partial class App : Application
{
  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);
    var mainWindow = ServiceLocator.Get<MainWindow>();
    mainWindow.Show();
  }
}
```            

When the viewmodel is updated (e.g. loading the data from the AssemblyInfo and NuSpec files) the UI is automatically updated. The reason for that is that the viewmodels implement `INotifyPropertyChanged`. Here, [Fody](https://github.com/Fody/Fody) and [Fody PropertyChanged](https://github.com/Fody/PropertyChanged) are used.

They allows to use normal properties like
```csharp
public string GivenNames { get; set; }
```

instead of
```csharp
string givenNames;
public string GivenNames
{
    get => givenNames;
    set
    {
        if (value != givenNames)
        {
            givenNames = value;
            OnPropertyChanged("GivenNames");
        }
    }
}
```
because it injects code automatically during build time.

#### Design vs. Runtime viewmodels
Using [DI](https://de.wikipedia.org/wiki/Dependency_Injection) to get the view model is very convenient, because it allows to bind a [mock](https://de.wikipedia.org/wiki/Mock-Objekt) as datacontext. With that the XAML designer shows data and therefore it enables the developer to actually see how it will look like.

In the `NinjectModule`a different model is bound when the application is not running but is in design mode:

```csharp
if (DesignMode.IsEnabled())
    Bind<MainWindowViewModel>().To<DesignMainWindowViewModel>().InSingletonScope();
else
    Bind<MainWindowViewModel>().To<MainWindowViewModel>().InSingletonScope();
```  

#### Commands
All buttons in the UI are firing commands when pressed. Therefore a class is needed which implements `ICommand`. In that class a check for example is done if the command can be applied, if not, the button is deactivated (e.g. in cased when the version could not be parsed).

Then these commands are bound in a `DataGrid` and the bound data context, in the table one project is sent to the command when executed:

```csharp
 <Button Command="{Binding IncreaseMajorVersionCommand}" CommandParameter="{Binding .}">+</Button>
```
