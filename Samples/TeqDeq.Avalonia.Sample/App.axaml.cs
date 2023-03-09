using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using TekDeq.Localization.Avalonia.Options;
using TekDeq.Localization.Avalonia.Providers;
using TekDeq.Localization.DependencyInjection.Extensions;
using TeqDeq.Avalonia.Sample.ViewModels;
using TeqDeq.Avalonia.Sample.Views;

namespace TeqDeq.Avalonia.Sample;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

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
}