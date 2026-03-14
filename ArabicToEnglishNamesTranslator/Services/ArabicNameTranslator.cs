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
    private readonly Dictionary<string, string> _dictionary;

    /// <summary>
    /// Initializes a new instance of <see cref="ArabicNameTranslator"/>,
    /// loading the embedded name dictionary and letter map.
    /// </summary>
    public ArabicNameTranslator()
    {
        _dictionary = ArabicNameDictionary.Load();
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

    private string TranslateWord(string name)
    {
        if (_dictionary.TryGetValue(name, out var exact))
            return exact;

        var enName = new StringBuilder();

        for (int i = 0; i < name.Length; i++)
        {
            var letter = name[i].ToString();

            if (i == 0 && ArabicRules.FirstLetterRules.TryGetValue(letter, out var first))
                enName.Append(first);

            else if (ArabicRules.CaseRules.TryGetValue(letter, out var baseLetter))
                enName.Append(baseLetter);

            else
                enName.Append(letter);

            if (i < name.Length - 1)
            {
                var nextLetter = name[i + 1].ToString();

                if (ArabicRules.NextLetterRules.TryGetValue(letter, out var nextDict)
                    && nextDict.TryGetValue(nextLetter, out var val))
                {
                    enName.Append(val);
                }

                if (i > 0)
                {
                    var before = name[i - 1].ToString();

                    if (ArabicRules.MiddleLetterRules.TryGetValue(letter, out var beforeDict)
                        && beforeDict.TryGetValue(before, out var afterDict)
                        && afterDict.TryGetValue(nextLetter, out var mid))
                    {
                        enName.Append(mid);
                    }
                }

                if (i < name.Length - 2)
                {
                    var third = name[i + 2].ToString();

                    if (ArabicRules.ThreeLetterRules.TryGetValue(letter, out var secondDict)
                        && secondDict.TryGetValue(nextLetter, out var thirdDict)
                        && thirdDict.TryGetValue(third, out var three))
                    {
                        enName.Append(three);
                    }
                }

                if (ArabicRules.SpecialLetterRules.TryGetValue(letter, out var special)
                    && special.Rules.TryGetValue(nextLetter, out var sp))
                {
                    if (special.Action == "slice" && enName.Length > 0)
                        enName.Length -= 1;

                    enName.Append(sp);
                }
            }
        }

        var lastLetter = name[^1].ToString();

        if (ArabicRules.LastLetterRules.TryGetValue(lastLetter, out var last))
        {
            if (last.Action == "slice" && enName.Length > 0)
                enName.Length -= 1;

            enName.Append(last.Value);
        }

        return Capitalize(enName.ToString());
    }

    private static string Capitalize(string s)
        => string.IsNullOrEmpty(s) ? s : char.ToUpper(s[0]) + s.Substring(1);
}