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

        [Theory(DisplayName = "Translate Arabic name to English")]
        // Simple (2 words)
        [InlineData("سامر علي", "Samar Ali")]
        [InlineData("كريم نادر", "Kariam Nadr")]
        [InlineData("ليلى سليم", "Laila Salim")]
        // Multi-Part (3+ words)
        [InlineData("سامي فاضل كمال", "Samai Fadhl Kamal")]
        [InlineData("رامي عدنان سليم حاتم", "Rami Adnan Salim Hatim")]
        // Long names
        [InlineData("سامي نادر فاضل كريم الزهيري", "Samai Nadr Fadhl Kariam Alzhyry")]
        [InlineData("كريم سامر عدنان حاتم البدراني", "Kariam Samar Adnan Hatim Albdrany")]
        [InlineData("نادر سليم كمال فاضل الزهراني", "Nadr Salim Kamal Fadhl Alzhrany")]
        [InlineData("صفاء عباس خشاف حمزة المرعبي", "Safaa Abbas Khshaf Hamzah Almraby")]
        // Compound عبد names (with variations)
        [InlineData("عبد نور", "Abad Noar")]
        [InlineData("عبد الهادي", "Abad Alhadi")]
        [InlineData("عبد القادر", "Abad Aliqadr")]
        [InlineData("عبدالنور", "Abdalnwr")]
        [InlineData("عبد النور سليم", "Abad Alnoar Salim")]
        // Abu style (أبو variations)
        [InlineData("أبو نادر", "Abo Nadr")]
        [InlineData("ابو نادر", "Abo Nadr")]
        [InlineData("أبو سامر كريم", "Abo Samar Kariam")]
        // Ibn variations (بن/ابن)
        [InlineData("سليم بن نادر", "Salim Bn Nadr")]
        [InlineData("كريم ابن سامر", "Kariam Abn Samar")]
        [InlineData("نادر بن كريم فاضل", "Nadr Bn Kariam Fadhl")]
        // With diacritics (tashkeel marks)
        [InlineData("سَامِر فَاضِل", "Samar Fadhl")]
        [InlineData("عَادِل نَادِر", "Aadl Nadr")]
        [InlineData("كَرِيم سَلِيم", "Kariam Salim")]
        // Alef variations (أ/إ/آ)
        [InlineData("إياد كريم", "Ayad Kariam")]
        [InlineData("أدهم سليم", "Adham Salim")]
        [InlineData("آسر نادر", "Asr Nadr")]
        // Attached forms (no space)
        [InlineData("عبدالقادر", "Abadaliqadr")]
        [InlineData("عبدالهادي", "Abadalhadi")]
        [InlineData("عبدالله محمود", "Abadallah Mahmood")]
        [InlineData("عبدالكريم مصلح", "Abadalkariam Muslh")]
        // Longer synthetic names
        [InlineData("سليم نادر كريم فاضل الحاتمي", "Salim Nadr Kariam Fadhl Alhatmy")]
        [InlineData("رامي سامر عدنان فاضل الزهيري", "Rami Samar Adnan Fadhl Alzhyry")]
        [InlineData("كريم نادر سامر سليم القادري", "Kariam Nadr Samar Salim Alqadry")]
        // Normalization cases
        [InlineData("عبد النور", "Abad Alnoar")]
        [InlineData("عَبدُ النور", "Abad Alnoar")]
        // Spacing issues (multiple spaces)
        [InlineData("  سامر   كريم  ", "Samar Kariam")]
        [InlineData("سليم   بن   نادر", "Salim Bn Nadr")]
        // Very long
        [InlineData("سامي كريم نادر فاضل سليم الحاتمي", "Samai Kariam Nadr Fadhl Salim Alhatmy")]
        public void TranslateName(string arabic, string expected)
        {
            var translated = _translator.Translate(arabic);
            _testOutputHelper.WriteLine($"Arabic: {arabic} => English: {translated}");
            Assert.Equal(expected, translated);
        }
    }
}