using System.Globalization;
using TekDeq.Localization.Options;

namespace TekDeq.Localization.Avalonia.Options;

public class AvaloniaLocalizationOptions : LocalizationOptions
{
    public AvaloniaLocalizationOptions(IEnumerable<CultureInfo> cultures,
        CultureInfo defaultCulture, CultureInfo currentCulture, string assetsPath)
        : base(cultures, defaultCulture, currentCulture)
    {
        AssetsPath = assetsPath;
    }

    public string AssetsPath { get; }
}