using ArabicToEnglishNamesTranslator.Abstractions;
using ArabicToEnglishNamesTranslator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ArabicToEnglishNamesTranslator.DependencyInjection
{
    /// <summary>
    /// Extension methods for registering and resolving the Arabic name translator with Microsoft.Extensions.DependencyInjection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Registers <see cref="IArabicNameTranslator"/> as a singleton in the service collection.
        /// </summary>
        /// <param name="services">The service collection to add the translator to.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance so that calls can be chained.</returns>
        public static IServiceCollection AddArabicNameTranslator(this IServiceCollection services)
        {
            services.AddSingleton<IArabicNameTranslator, ArabicNameTranslator>();

            return services;
        }

        /// <summary>
        /// Resolves the registered <see cref="IArabicNameTranslator"/> from the service provider.
        /// </summary>
        /// <param name="provider">The service provider to resolve the translator from.</param>
        /// <returns>The registered <see cref="IArabicNameTranslator"/> instance.</returns>
        public static IArabicNameTranslator UseArabicNameTranslator(this IServiceProvider provider)
            => provider.GetRequiredService<IArabicNameTranslator>();

    }
}
