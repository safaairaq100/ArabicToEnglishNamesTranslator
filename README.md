# ArabicToEnglishNamesTranslator

A .NET 8 library for translating Arabic names to their English phonetic equivalents. It uses a built-in dictionary of over 2,500 common Arabic names for exact matches and falls back to character-by-character transliteration for names not in the dictionary.

## Features

- Translates Arabic names to English using a dictionary of 2,500+ common names
- Falls back to phonetic transliteration for unknown names
- Normalizes Arabic diacritical marks (tashkeel) and character variations (e.g. أ / إ / آ → ا)
- Handles compound names, multi-word names, and names with prefixes such as عبد (Abd) and أبو (Abu)
- Supports Microsoft Dependency Injection out of the box

## Installation

Install the package from [NuGet](https://www.nuget.org/packages/ArabicToEnglishNamesTranslator):

### .NET CLI

```bash
dotnet add package ArabicToEnglishNamesTranslator
```

### Package Manager Console

```powershell
Install-Package ArabicToEnglishNamesTranslator
```

### PackageReference (in your `.csproj`)

```xml
<PackageReference Include="ArabicToEnglishNamesTranslator" Version="1.0.0" />
```

## Usage

### Without Dependency Injection

Instantiate `ArabicNameTranslator` directly:

```csharp
using ArabicToEnglishNamesTranslator.Services;

var translator = new ArabicNameTranslator();

string english = translator.Translate("سامر علي");
Console.WriteLine(english); // Output: Samar Ali

string compound = translator.Translate("عبد القادر");
Console.WriteLine(compound); // Output: Abd Alqadr
```

### With Dependency Injection (ASP.NET Core / Generic Host)

Register the translator in your service collection using the provided extension method:

```csharp
using ArabicToEnglishNamesTranslator.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register the translator as a singleton
builder.Services.AddArabicNameTranslator();
```

Then inject `IArabicNameTranslator` wherever you need it:

```csharp
using ArabicToEnglishNamesTranslator.Abstractions;

public class MyService
{
    private readonly IArabicNameTranslator _translator;

    public MyService(IArabicNameTranslator translator)
    {
        _translator = translator;
    }

    public string GetEnglishName(string arabicName)
    {
        return _translator.Translate(arabicName);
    }
}
```

You can also resolve the translator directly from an `IServiceProvider`:

```csharp
using ArabicToEnglishNamesTranslator.DependencyInjection;

var translator = serviceProvider.UseArabicNameTranslator();
string result = translator.Translate("أحمد محمد");
```

## API Reference

### `IArabicNameTranslator`

```csharp
namespace ArabicToEnglishNamesTranslator.Abstractions
{
    public interface IArabicNameTranslator
    {
        /// <summary>
        /// Translates an Arabic name to its English phonetic equivalent.
        /// </summary>
        /// <param name="arabicName">The Arabic name to translate.</param>
        /// <returns>The English transliteration of the name.</returns>
        string Translate(string arabicName);
    }
}
```

### `ServiceCollectionExtensions`

| Method | Description |
|--------|-------------|
| `AddArabicNameTranslator(this IServiceCollection)` | Registers `IArabicNameTranslator` as a singleton in the DI container. |
| `UseArabicNameTranslator(this IServiceProvider)` | Resolves and returns the registered `IArabicNameTranslator` instance. |

## Examples

| Arabic Name | English Translation |
|-------------|---------------------|
| سامر علي | Samar Ali |
| عبد القادر | Abd Alqadr |
| أبو نادر | Abo Nadr |
| محمد إبراهيم | Mohamed Ibrahim |
| فاطمة الزهراء | Fatma Alzhraa |

## Requirements

- .NET 8.0 or later

## License

This project is licensed under the terms included in the repository. See the [LICENSE](LICENSE) file for details.
