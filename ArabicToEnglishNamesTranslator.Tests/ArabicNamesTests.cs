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
        [InlineData("سامر علي", "Samer Ali")]
        [InlineData("كريم نادر", "Karim Nader")]
        [InlineData("ليلى سليم", "Layla Salim")]
        // Multi-Part (3+ words)
        [InlineData("سامي فاضل كمال", "Sami Fadhel Kamal")]
        [InlineData("رامي عدنان سليم حاتم", "Rami Adnan Salim Hatim")]
        // Long names
        [InlineData("سامي نادر فاضل كريم الزهيري", "Sami Nader Fadhel Karim Alzhiri")]
        [InlineData("كريم سامر عدنان حاتم البدراني", "Karim Samer Adnan Hatim Albdrani")]
        [InlineData("نادر سليم كمال فاضل الزهراني", "Nader Salim Kamal Fadhel Alzhrani")]
        [InlineData("صفاء عباس خشاف حمزة المرعبي", "Safaa Abbas Khshaf Hamza Almarabi")]
        // Compound عبد names (with variations)
        [InlineData("عبد نور", "Abd Noor")]
        [InlineData("عبد الهادي", "Abd Al-Hadi")]
        [InlineData("عبد القادر", "Abd Al-Qadir")]
        [InlineData("عبدالنور", "Abdalnor")]
        [InlineData("عبد النور سليم", "Abd Al-Noor Salim")]
        // Abu style (أبو variations)
        [InlineData("أبو نادر", "Abu Nader")]
        [InlineData("ابو نادر", "Abu Nader")]
        [InlineData("أبو سامر كريم", "Abu Samer Karim")]
        // Ibn variations (بن/ابن)
        [InlineData("سليم بن نادر", "Salim Bn Nader")]
        [InlineData("كريم ابن سامر", "Karim Abn Samer")]
        [InlineData("نادر بن كريم فاضل", "Nader Bn Karim Fadhel")]
        // With diacritics (tashkeel marks)
        [InlineData("سَامِر فَاضِل", "Samer Fadhel")]
        [InlineData("عَادِل نَادِر", "Adel Nader")]
        [InlineData("كَرِيم سَلِيم", "Karim Salim")]
        // Alef variations (أ/إ/آ)
        [InlineData("إياد كريم", "Ayad Karim")]
        [InlineData("أدهم سليم", "Adham Salim")]
        [InlineData("آسر نادر", "Asir Nader")]
        // Attached forms (no space)
        [InlineData("عبدالقادر", "Abdul Qader")]
        [InlineData("عبدالهادي", "Abdul Hadi")]
        [InlineData("عبدالله محمود", "Abdullah Mahmoud")]
        [InlineData("عبدالكريم مصلح", "Abdul Karim Muslih")]
        // Longer synthetic names
        [InlineData("سليم نادر كريم فاضل الحاتمي", "Salim Nader Karim Fadhel Alhatmai")]
        [InlineData("رامي سامر عدنان فاضل الزهيري", "Rami Samer Adnan Fadhel Alzhiri")]
        [InlineData("كريم نادر سامر سليم القادري", "Karim Nader Samer Salim Alqadri")]
        // Normalization cases
        [InlineData("عبد النور", "Abd Al-Noor")]
        [InlineData("عَبدُ النور", "Abd Al-Noor")]
        // Spacing issues (multiple spaces)
        [InlineData("  سامر   كريم  ", "Samer Karim")]
        [InlineData("سليم   بن   نادر", "Salim Bn Nader")]
        // Very long
        [InlineData("سامي كريم نادر فاضل سليم الحاتمي", "Sami Karim Nader Fadhel Salim Alhatmai")]
        public void TranslateName(string arabic, string expected)
        {
            var translated = _translator.Translate(arabic);
            _testOutputHelper.WriteLine($"Arabic: {arabic} => English: {translated}");  
            Assert.Equal(expected, translated);
        }
    }
}