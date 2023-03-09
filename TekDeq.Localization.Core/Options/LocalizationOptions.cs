using System.Globalization;

namespace TekDeq.Localization.Core.Options;

public class LocalizationOptions
{
    public LocalizationOptions(IEnumerable<CultureInfo> cultures,
        CultureInfo defaultCulture, CultureInfo currentCulture)
    {
        Cultures = cultures;
        CurrentCulture = currentCulture;
        DefaultCulture = defaultCulture;
    }

    public IEnumerable<CultureInfo> Cultures { get; }
    public CultureInfo CurrentCulture { get; }
    public CultureInfo DefaultCulture { get; }
}