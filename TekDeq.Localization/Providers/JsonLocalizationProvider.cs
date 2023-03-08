using System.Globalization;
using System.Text;
using System.Text.Json;
using TekDeq.Localization.Options;
using TekDeq.Localization.Providers.Abstract;

namespace TekDeq.Localization.Providers;

public abstract class JsonLocalizationProvider : LocalizationProviderBase
{
    private IDictionary<string, string> _activeDictionary = new Dictionary<string, string>();
    private IDictionary<string, string> _fallbackDictionary = new Dictionary<string, string>();

    protected JsonLocalizationProvider(LocalizationOptions options) : base(options)
    {
    }


    private Dictionary<string, string> GetDictionary(CultureInfo culture)
    {
        // load strings
        if (LoadSingleAsset(culture, out var stream) && stream != null)
        {
            using var streamReader = new StreamReader(stream, Encoding.UTF8);
            var json = streamReader.ReadToEnd();
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json)!;
        }
        else
        {
            throw new InvalidDataException($"Cannot load asset for culture {culture.IetfLanguageTag}");
        }
    }

    protected override void ChangeCulture(CultureInfo culture)
    {
        if (AvailableCultures.All(c => c.IetfLanguageTag != culture.IetfLanguageTag))
            culture = Options.DefaultCulture;
        _activeDictionary = GetDictionary(culture);
        base.ChangeCulture(culture);
    }

    public override string GetResourceByKey(string key)
    {
        var resource = string.Empty;

        // extract data from active dictionary
        if (!_activeDictionary.TryGetValue(key, out resource))
        {
            // try to extract data from default dictionary if active fails
            if (!_fallbackDictionary.Any())
                _fallbackDictionary = GetDictionary(Options.DefaultCulture);
            _fallbackDictionary.TryGetValue(key, out resource);
        }
        
        return !string.IsNullOrEmpty(resource) 
            ? resource.Replace("\\n", "\n") 
            : $"{CurrentCulture.IetfLanguageTag}:{key}"; // Return missing data if fallback fails
    }
}