# ArabicToEnglishNamesTranslator

A .NET library for translating Arabic names to their English (romanized) equivalents.

## Features

- Built-in dictionary of **2,500+ common Arabic names** for accurate translations
- Phonetic **character-by-character transliteration** fallback for unknown names
- Handles **diacritical marks** (tashkeel) and common letter variants (أ / إ / آ → a)
- Supports **multi-word names** (e.g. full names with عبد, أبو, ابن prefixes)
- Microsoft.Extensions.**DependencyInjection** integration

## Installation

```
dotnet add package ArabicToEnglishNamesTranslator
```

## Usage

### Without dependency injection

```csharp
using ArabicToEnglishNamesTranslator.Services;

var translator = new ArabicNameTranslator();

string english = translator.Translate("محمد");       // "Muhammad"
string full    = translator.Translate("عبد الله");   // "Abdullah"
```

### With Microsoft.Extensions.DependencyInjection

Register the translator in your service collection:

```csharp
using ArabicToEnglishNamesTranslator.DependencyInjection;

services.AddArabicNameTranslator();
```

Resolve and use it:

```csharp
using ArabicToEnglishNamesTranslator.DependencyInjection;

var translator = serviceProvider.UseArabicNameTranslator();
string english = translator.Translate("فاطمة"); // "Fatima"
```

Or inject `IArabicNameTranslator` directly into your classes:

```csharp
using ArabicToEnglishNamesTranslator.Abstractions;

public class MyService(IArabicNameTranslator translator)
{
    public string GetEnglishName(string arabicName) => translator.Translate(arabicName);
}
```

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).
