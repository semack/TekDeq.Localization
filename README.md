# TekDeq.Localization [![Create and Publish Packages](https://github.com/semack/TekDeq.Localization/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/semack/TekDeq.Localization/actions/workflows/dotnet.yml)

Yet another localization library with its own abstractions gives the possibility to 
create localization infrastructure with examples.  It contains basic Json Localization 
Provider implementation (other types will be added in the future).

## Library
### Installation
Before using the library, install following Nuget packages:

| Name | Description | Version |
|:-|:-|:-|
| [TekDeq.Localization.Core](https://img.shields.io/nuget/v/TekDeq.Localization.Core/) | Core library | ![Nuget](https://badgen.net/nuget/v/TekDeq.Localization.Core) | 
| [TekDeq.Localization.DependencyInjection](https://www.nuget.org/packages/TekDeq.Localization.DependencyInjection/) | Microsoft DI extensions | ![Nuget](https://badgen.net/nuget/v/TekDeq.Localization.DependencyInjection) |
| [TekDeq.Localization.Avalonia](https://www.nuget.org/packages/TekDeq.Localization.Avalonia/) | Avalonia Localization and Extensions | ![Nuget](https://badgen.net/nuget/v/TekDeq.Localization.Avalonia) |

## Using the library
To create additional Localization Providers, please look at 
[ILocalizationProvider](https://github.com/semack/TekDeq.Localization/blob/master/TekDeq.Localization.Core/Providers/Interfaces/ILocalizationProvider.cs)
and 
[LocalizationProviderBase](https://github.com/semack/TekDeq.Localization/blob/master/TekDeq.Localization.Core/Providers/Abstract/LocalizationProviderBase.cs)
abstractions. As an example of usage please look at 
[JsonLocalizationProvider](https://github.com/semack/TekDeq.Localization/blob/master/TekDeq.Localization.Core/Providers/JsonLocalizationProvider.cs) 
and 
[AvaloniaJsonLocalizationProvider](https://github.com/semack/TekDeq.Localization/blob/master/TekDeq.Localization.Avalonia/Providers/AvaloniaJsonLocalizationProvider.cs)
implementation.
If you are using dependency injection in your project, an example of the usage could be found in 
[App.axaml.cs](https://github.com/semack/TekDeq.Localization/blob/master/Samples/TeqDeq.Avalonia.Sample/App.axaml.cs)
of the demo project.

```csharp
    public override void OnFrameworkInitializationCompleted()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.UseMicrosoftDependencyResolver();
                // Initialize Splat
                var resolver = Locator.CurrentMutable;
                resolver.InitializeSplat();
                resolver.InitializeReactiveUI();

                // Register Views and ViewModels
                services.AddTransient<MainWindow>();
                services.AddTransient<MainWindowViewModel>();

                // Register Localization
                services.AddLocalization<AvaloniaJsonLocalizationProvider>(() =>
                {
                    var options = new AvaloniaLocalizationOptions(
                        // cultures support localization
                        new List<CultureInfo>
                        {
                            new("en-US"),
                            new("uk-UA")
                        },
                        // defaultCulture, it uses for setting if currentCulture is not in cultures list
                        // and as fallback culture mor missing localization entries.
                        new CultureInfo("en-US"),
                        // currentCulture sets when infrastructure loads,
                        // could be received from app settings or so.
                        Thread.CurrentThread.CurrentCulture,
                        // path to assets with json files of localization.
                        $"{typeof(App).Namespace}/Assets/i18n");
                    return options;
                });
            }).Build();


        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = host.Services.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
```
For start using the markup localization in Avalonia by 
[Localize](https://github.com/semack/TekDeq.Localization/blob/master/TekDeq.Localization.Avalonia/Extensions/LocalizeExtension.cs) 
markup extension, it needs the namespace to be added to the markup.

```xamlml
<Window xmlns="https://github.com/avaloniaui"
...
        xmlns:i18n="clr-namespace:TekDeq.Localization.Avalonia.Extensions;assembly=TekDeq.Localization.Avalonia"
...
>
```
After this it could be used for localization of UI
```xamlml
    <StackPanel
...
        <TextBlock Text="{i18n:Localize Greeting}" />
...
    </StackPanel>
```

## AvaloniaUI demo project
The [DEMO](https://github.com/semack/TekDeq.Localization/tree/master/Samples/TeqDeq.Avalonia.Sample)
project included to the repository, just open and run it in Visual Studio.

## License
This project is licensed under the terms of the 
[MIT license](https://github.com/semack/TekDeq.Localization/blob/master/LICENSE.md).

## Contribute
Contributions are welcome. Just open an Issue or submit a new [PR](https://github.com/semack/TekDeq.Localization/pulls). 

## Contact
You can reach me via my [email](mailto://semack@gmail.com).

