using System.Globalization;

namespace TekDeq.Localization.Core.Providers.Interfaces;

public interface ILocalizationProvider
{
    public IEnumerable<CultureInfo> AvailableCultures { get; }
    public CultureInfo CurrentCulture { get; set; }

    public string GetResourceByKey(string key);
}