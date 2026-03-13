using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicToEnglishNamesTranslator.Core
{
    internal class ArabicNormalizer
    {
        private static readonly HashSet<char> Diacritics =
        [
            '\u064B','\u064C','\u064D','\u064E',
            '\u064F','\u0650','\u0651','\u0652'
        ];

        public static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var sb = new StringBuilder();

            foreach (var c in input)
            {
                if (Diacritics.Contains(c))
                    continue;

                sb.Append(c switch
                {
                    'أ' or 'إ' or 'آ' => 'ا',
                    'ة' => 'ه',
                    'ٱ' => 'ا',
                    _ => c
                });
            }

            return sb.ToString();
        }
    }
}
