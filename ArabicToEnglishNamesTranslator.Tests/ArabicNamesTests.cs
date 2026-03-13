using ArabicToEnglishNamesTranslator.Abstractions;
using ArabicToEnglishNamesTranslator.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace ArabicToEnglishNamesTranslator.Tests
{
    public class ArabicNamesTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IArabicNameTranslator _translator;

        public ArabicNamesTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var services = new ServiceCollection();
            services.AddArabicNameTranslator();
            var provider = services.BuildServiceProvider();
            _translator = provider.UseArabicNameTranslator();
        }

        [Fact(DisplayName = "Translate Arabic name to English")]
        public void TranslateName()
        {
            // add list of name from three name to test
            var names = new List<string>
            {
                // simple
                "سامر علي",
                "كريم نادر",
                "ليلى سليم",

                // multi-part
                "سامي فاضل كمال",
                "رامي عدنان سليم حاتم",

                // long names
                "سامي نادر فاضل كريم الزهيري",
                "كريم سامر عدنان حاتم البدراني",
                "نادر سليم كمال فاضل الزهراني",
                "صفاء عباس خشاف حمزة المرعبي",

                // compound عبد names
                "عبد نور",
                "عبد الهادي",
                "عبد القادر",
                "عبدالنور",
                "عبد النور سليم",

                // abu style
                "أبو نادر",
                "ابو نادر",
                "أبو سامر كريم",

                // ibn variations
                "سليم بن نادر",
                "كريم ابن سامر",
                "نادر بن كريم فاضل",

                // with diacritics
                "سَامِر فَاضِل",
                "عَادِل نَادِر",
                "كَرِيم سَلِيم",

                // alef variations
                "إياد كريم",
                "أدهم سليم",
                "آسر نادر",

                // attached forms
                "عبدالنور",
                "عبدالقادر",
                "عبدالهادي",   
                "عبدالله محمود",
                "عبدالكريم مصلح",

                // longer synthetic names
                "سليم نادر كريم فاضل الحاتمي",
                "رامي سامر عدنان فاضل الزهيري",
                "كريم نادر سامر سليم القادري",

                // normalization cases
                "عبدالنور",
                "عبد النور",
                "عَبدُ النور",
             

                // spacing issues
                "  سامر   كريم  ",
                "سليم   بن   نادر",

                // very long
                "سامي كريم نادر فاضل سليم الحاتمي"
            };

            foreach (var name in names)
            {
                var translated = _translator.Translate(name);
                _testOutputHelper.WriteLine($"Arabic: {name} => English: {translated}");

            }


        }
    }
}