using Microsoft.Extensions.DependencyInjection;
using TekDeq.Localization.Interfaces;
using TekDeq.Localization.Options;
using TekDeq.Localization.Providers.Abstract;
using TekDeq.Localization.Providers.Interfaces;

namespace TekDeq.Localization.DependencyInjection.Extensions;

public static class LocalizerExtensions
{
    public static IServiceCollection AddLocalization<T>(this IServiceCollection services,
        Func<LocalizationOptions> optionsDelegate)
        where T : LocalizationProviderBase
    {
        var options = optionsDelegate?.Invoke();
        if (Activator.CreateInstance(typeof(T), options) is ILocalizationProvider provider)
        {
            var localizer = Localizer.Initialize(provider);
            services.AddSingleton<ILocalizer>(impl => localizer);
        }
        else
        {
            throw new Exception("Cannot create an instance of the Localizer");
        }

        return services;
    }
}