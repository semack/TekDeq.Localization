using Avalonia;
using Avalonia.Platform;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using TekDeq.Localization.Avalonia.Options;
using TekDeq.Localization.Core.Options;
using TekDeq.Localization.Core.Providers;

namespace TekDeq.Localization.Avalonia.Providers;

public class AvaloniaJsonLocalizationProvider : JsonLocalizationProvider
{
    public AvaloniaJsonLocalizationProvider(LocalizationOptions options) : base(options)
    {
        if (Options is not AvaloniaLocalizationOptions)
            throw new ArgumentException("The options is not compatible with requested provider.");
    }

    private new AvaloniaLocalizationOptions Options => (AvaloniaLocalizationOptions)base.Options;

    private Uri GetAssetUri(CultureInfo culture)
    {
        return new Uri($"avares://{Options.AssetsPath}/{culture.IetfLanguageTag}.json");
    }

    protected override void ValidateOptions(LocalizationOptions options)
    {
        base.ValidateOptions(options);

        var assets = AvaloniaLocator.Current.GetRequiredService<IAssetLoader>();
        options.Cultures.ToList().ForEach(culture =>
        {
            var uri = GetAssetUri(culture);
            if (!assets.Exists(uri))
                throw new ValidationException($"Cannot find resource file\n{uri}");
        });
    }

    protected override Stream? GetResourceStream(CultureInfo culture)
    {
        var uri = GetAssetUri(culture);
        var assets = AvaloniaLocator.Current.GetRequiredService<IAssetLoader>();

        if (assets.Exists(uri))
            return assets.Open(uri);

        return null;
    }
}