using System.Reflection;
using System.Text.Json;

namespace ArabicToEnglishNamesTranslator.Core;

public static class ArabicNameDictionary
{
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