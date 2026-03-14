using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArabicToEnglishNamesTranslator.Core;

/// <summary>
/// Loads the embedded Arabic-to-English name dictionary from the assembly resources.
/// </summary>
public static class ArabicNameDictionary
{
    private sealed class NamesDictionary
    {
        [JsonPropertyName("masculine")]
        public Dictionary<string, string> Masculine { get; set; } = new();

        [JsonPropertyName("feminine")]
        public Dictionary<string, string> Feminine { get; set; } = new();
    }

    /// <summary>
    /// Loads the Arabic-to-English name dictionary from the embedded JSON resource.
    /// The resource contains masculine and feminine sections which are merged into a single lookup.
    /// </summary>
    /// <returns>A dictionary mapping normalized Arabic name strings to their English equivalents.</returns>
    public static Dictionary<string, string> Load()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var resource = assembly
            .GetManifestResourceNames()
            .First(x => x.EndsWith("names.json"));

        using var stream = assembly.GetManifestResourceStream(resource)!;

        var names = JsonSerializer.Deserialize<NamesDictionary>(stream)!;

        return names.Masculine
            .Concat(names.Feminine)
            .GroupBy(x => ArabicNormalizer.Normalize(x.Key))
            .ToDictionary(
                g => g.Key,
                g => g.First().Value
            );
    }
}