using System.Globalization;
using TekDeq.Localization.Core.Options;
using TekDeq.Localization.Core.Providers.Interfaces;

namespace TekDeq.Localization.Core.Providers.Abstract;

public abstract class LocalizationProviderBase : ILocalizationProvider
{
    private CultureInfo? _currentCulture;

    protected LocalizationProviderBase(LocalizationOptions options)
    {
        Options = options;
        ValidateOptions(Options);
        CurrentCulture = Options.CurrentCulture;
    }

    protected LocalizationOptions Options { get; }

    public CultureInfo CurrentCulture
    {
        get => _currentCulture!;
        set => ChangeCulture(value);
    }

    public abstract string GetResourceByKey(string key);

    public IEnumerable<CultureInfo> AvailableCultures => Options.Cultures;

    protected abstract Stream? GetResourceStream(CultureInfo culture);

    protected virtual void ValidateOptions(LocalizationOptions options)
    {
        if (!options.Cultures.Any()) throw new ArgumentException("The culture list is empty.");

        if (options.Cultures.All(x => x.IetfLanguageTag != options.DefaultCulture.IetfLanguageTag))
            throw new ArgumentException(
                $"The default culture {options.DefaultCulture.IetfLanguageTag} is not present in the culture list.");
    }

    protected virtual void ChangeCulture(CultureInfo culture)
    {
        _currentCulture = culture;
    }
}