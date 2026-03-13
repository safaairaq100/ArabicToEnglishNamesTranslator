using Microsoft.Extensions.DependencyInjection;

namespace ArabicToEnglishNamesTranslator
{
    public static class NamesTranslatorServiceExtensions
    {

        public static IServiceCollection AddArabicCurrencyService(this IServiceCollection services)
        {
            services.AddSingleton<INamesTranslatorService, NamesTranslatorService>();
            return services;
        }
        public static INamesTranslatorService UseArabicTextCurrency(this IServiceProvider provider)
            => provider.GetRequiredService<INamesTranslatorService>();

    }
}
