namespace ArabicToEnglishNamesTranslator.Core;

/// <summary>
/// Provides a mapping from Arabic characters to their phonetic English equivalents.
/// </summary>
public static class ArabicLetterMap
{
    /// <summary>
    /// A dictionary mapping each Arabic letter to its phonetic English transliteration.
    /// </summary>
    public static readonly Dictionary<char, string> Map = new()
    {
        ['ا'] = "a",
        ['ب'] = "b",
        ['ت'] = "t",
        ['ث'] = "th",
        ['ج'] = "j",
        ['ح'] = "h",
        ['خ'] = "kh",
        ['د'] = "d",
        ['ذ'] = "dh",
        ['ر'] = "r",
        ['ز'] = "z",
        ['س'] = "s",
        ['ش'] = "sh",
        ['ص'] = "s",
        ['ض'] = "d",
        ['ط'] = "t",
        ['ظ'] = "z",
        ['ع'] = "a",
        ['غ'] = "gh",
        ['ف'] = "f",
        ['ق'] = "q",
        ['ك'] = "k",
        ['ل'] = "l",
        ['م'] = "m",
        ['ن'] = "n",
        ['ه'] = "h",
        ['و'] = "w",
        ['ي'] = "y"
    };
}