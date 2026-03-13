using System.Reflection;
using System.Text.Json;

namespace ArabicToEnglishNamesTranslator.Core;

/// <summary>
/// Loads the embedded Arabic-to-English name dictionary from the assembly resources.
/// </summary>
public static class ArabicNameDictionary
{
    /// <summary>
    /// Loads the Arabic-to-English name dictionary from the embedded JSON resource.
    /// </summary>
    /// <returns>A dictionary mapping normalized Arabic name strings to their English equivalents.</returns>
    public static Dictionary<string, string> Load()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var resource = assembly
            .GetManifestResourceNames()
            .First(x => x.EndsWith("names.json"));

        using var stream = assembly.GetManifestResourceStream(resource)!;

        return JsonSerializer.Deserialize<Dictionary<string, string>>(stream)!
            .GroupBy(x => ArabicNormalizer.Normalize(x.Key))
            .ToDictionary(
                g => g.Key,
                g => g.First().Value
            );
    }
}