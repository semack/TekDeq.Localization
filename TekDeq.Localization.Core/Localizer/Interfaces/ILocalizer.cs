using System.Globalization;

namespace TekDeq.Localization.Core.Interfaces;

public interface ILocalizer
{
    public string this[string key] { get; }
    public CultureInfo CurrentCulture { get; set; }
    public IEnumerable<CultureInfo> AvailableCultures { get; }
}