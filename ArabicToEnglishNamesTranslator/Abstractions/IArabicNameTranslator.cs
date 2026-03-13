using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicToEnglishNamesTranslator.Abstractions
{
    /// <summary>
    /// Defines a service for translating Arabic names to their English (romanized) equivalents.
    /// </summary>
    public interface IArabicNameTranslator
    {
        /// <summary>
        /// Translates an Arabic name to its English (romanized) equivalent.
        /// </summary>
        /// <param name="arabicName">The Arabic name to translate.</param>
        /// <returns>The English transliteration of the Arabic name, or an empty string if the input is null or whitespace.</returns>
        string Translate(string arabicName);

    }
}
