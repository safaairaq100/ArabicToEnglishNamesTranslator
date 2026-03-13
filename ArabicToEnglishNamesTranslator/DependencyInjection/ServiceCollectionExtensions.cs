using ArabicToEnglishNamesTranslator.Abstractions;
using ArabicToEnglishNamesTranslator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ArabicToEnglishNamesTranslator.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddArabicNameTranslator(this IServiceCollection services)
        {
            services.AddSingleton<IArabicNameTranslator, ArabicNameTranslator>();

            return services;
        }
        public static IArabicNameTranslator UseArabicNameTranslator(this IServiceProvider provider)
            => provider.GetRequiredService<IArabicNameTranslator>();

    }
}
