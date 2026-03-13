using System.Text;
using ArabicToEnglishNamesTranslator.Abstractions;
using ArabicToEnglishNamesTranslator.Core;

namespace ArabicToEnglishNamesTranslator.Services;

/// <summary>
/// Translates Arabic names to their English (romanized) equivalents using a built-in
/// dictionary and phonetic transliteration fallback.
/// </summary>
public class ArabicNameTranslator : IArabicNameTranslator
{
    private readonly Dictionary<string, string> _nameDictionary;
    private readonly Dictionary<char, string> _letters;

    /// <summary>
    /// Initializes a new instance of <see cref="ArabicNameTranslator"/>,
    /// loading the embedded name dictionary and letter map.
    /// </summary>
    public ArabicNameTranslator()
    {
        _nameDictionary = ArabicNameDictionary.Load();
        _letters = ArabicLetterMap.Map;
    }

    /// <inheritdoc />
    public string Translate(string arabicName)
    {
        if (string.IsNullOrWhiteSpace(arabicName))
            return "";

        arabicName = ArabicNormalizer.Normalize(arabicName);

        var words = arabicName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var result = new StringBuilder();

        foreach (var word in words)
        {
            result.Append(TranslateWord(word));
            result.Append(" ");
        }

        return result.ToString().Trim();
    }

    private string TranslateWord(string word)
    {
        if (_nameDictionary.TryGetValue(word, out var exact))
            return exact;

        var sb = new StringBuilder();

        foreach (var c in word)
        {
            if (_letters.TryGetValue(c, out var en))
                sb.Append(en);
        }

        return Capitalize(PostProcess(sb.ToString()));
    }

    private static string Capitalize(string s)
    {
        if (string.IsNullOrEmpty(s))
            return s;

        return char.ToUpper(s[0]) + s.Substring(1);
    }

    private static string PostProcess(string s)
    {
        s = s.Replace("aa", "a");
        s = s.Replace("ee", "i");
        s = s.Replace("oo", "u");

        if (s.StartsWith("abd"))
            s = "Abd" + s.Substring(3);

        return s;
    }
}