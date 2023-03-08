using System.ComponentModel;
using System.Globalization;
using TekDeq.Localization.Interfaces;
using TekDeq.Localization.Providers.Interfaces;

namespace TekDeq.Localization;

public sealed class Localizer : ILocalizer, INotifyPropertyChanged
{
    private const string IndexerName = "Item";
    private const string IndexerArrayName = "Item[]";

    private Localizer(ILocalizationProvider provider)
    {
        Provider = provider;
    }

    public static ILocalizer? Instance { get; private set; }

    private ILocalizationProvider Provider { get; }

    public IEnumerable<CultureInfo> AvailableCultures => Provider.AvailableCultures;

    public string this[string key] => Provider.GetResourceByKey(key);

    public CultureInfo CurrentCulture
    {
        get => Provider.CurrentCulture;
        set
        {
            Provider.CurrentCulture = value;
            Invalidate();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Invalidate()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCulture)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerName));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerArrayName));
    }

    public static ILocalizer Initialize(ILocalizationProvider provider)
    {
        Instance = new Localizer(provider);
        return Instance;
    }
}