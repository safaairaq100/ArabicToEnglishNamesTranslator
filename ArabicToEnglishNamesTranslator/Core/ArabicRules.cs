using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicToEnglishNamesTranslator.Core
{
    internal class ArabicRules
    {
        public static readonly Dictionary<string, string> CaseRules = new()
        {
            ["أ"] = "a",
            ["ب"] = "b",
            ["ج"] = "j",
            ["د"] = "d",
            ["ه"] = "h",
            ["و"] = "o",
            ["ز"] = "z",
            ["ح"] = "h",
            ["ط"] = "t",
            ["ي"] = "i",
            ["ك"] = "k",
            ["ل"] = "l",
            ["م"] = "m",
            ["ن"] = "n",
            ["س"] = "s",
            ["ع"] = "a",
            ["ف"] = "f",
            ["ص"] = "s",
            ["ق"] = "q",
            ["ر"] = "r",
            ["ش"] = "sh",
            ["ت"] = "t",
            ["ث"] = "th",
            ["خ"] = "kh",
            ["ذ"] = "th",
            ["ض"] = "dh",
            ["ظ"] = "z",
            ["غ"] = "gh",
            ["ا"] = "a",
            ["ئ"] = "a",
            ["ى"] = "a",
            ["ؤ"] = "u",
            ["ء"] = "a",
            ["ة"] = "h",
            ["إ"] = "i",
            ["آ"] = "a",
            [" "] = " "
        };

        public static readonly Dictionary<string, string> FirstLetterRules = new()
        {
            ["و"] = "w",
            ["ي"] = "y"
        };

        public static readonly Dictionary<string, Dictionary<string, string>> NextLetterRules = new()
        {
            ["م"] = new()
            {
                ["ح"] = "u",
                ["ث"] = "u",
                ["ص"] = "u",
                ["خ"] = "u",
                ["ر"] = "a",
                ["ه"] = "u",
                ["ق"] = "u",
                ["ع"] = "u",
                ["ف"] = "u",
                ["ن"] = "u",
                ["ك"] = "a",
                ["ي"] = "a"
            },

            ["س"] = new()
            {
                ["ه"] = "u",
                ["ن"] = "a",
                ["و"] = "a",
                ["ي"] = "a",
                ["ل"] = "a",
                ["ع"] = "a",
                ["د"] = "a",
                ["م"] = "a",
                ["ح"] = "a"
            },

            ["ن"] = new()
            {
                ["ب"] = "a",
                ["د"] = "a",
                ["ج"] = "a",
                ["ص"] = "a",
                ["ز"] = "a",
                ["ش"] = "a",
                ["ظ"] = "i",
                ["س"] = "i",
                ["ه"] = "u"
            }
        };

        public static readonly Dictionary<string,
            Dictionary<string, Dictionary<string, string>>> MiddleLetterRules = new()
        {
            ["ب"] = new()
            {
                ["ك"] = new() { ["ر"] = "a" },
                ["ع"] = new() { ["ر"] = "a" },
                ["خ"] = new() { ["ر"] = "a" },
                ["ص"] = new() { ["ا"] = "a" }
            },

            ["ز"] = new()
            {
                ["ا"] = new() { ["ل"] = "a" },
                ["ع"] = new() { ["ت"] = "a" }
            },

            ["م"] = new()
            {
                ["ح"] = new() { ["د"] = "a" }
            }
        };

        public static readonly Dictionary<string,
            Dictionary<string, Dictionary<string, string>>> ThreeLetterRules = new()
        {
            ["م"] = new()
            {
                ["ح"] = new()
                {
                    ["م"] = "u",
                    ["ا"] = "a",
                    ["أ"] = "a",
                    ["ب"] = "a",
                    ["ف"] = "a",
                    ["ج"] = "a"
                }
            },

            ["س"] = new()
            {
                ["ل"] = new() { ["ي"] = "u" },
                ["ف"] = new() { ["ي"] = "u" }
            }
        };

        public static readonly Dictionary<string, SpecialRule> SpecialLetterRules = new()
        {
            ["و"] = new()
            {
                Action = "slice",
                Rules = new()
                {
                    ["ا"] = "w",
                    ["ج"] = "w"
                }
            },

            ["م"] = new()
            {
                Action = "",
                Rules = new()
                {
                    ["ع"] = "u"
                }
            },

            ["ئ"] = new()
            {
                Action = "slice",
                Rules = new()
                {
                    ["ل"] = "e"
                }
            },

            ["ا"] = new()
            {
                Action = "slice",
                Rules = new()
                {
                    ["ش"] = "I"
                }
            }
        };

        public static readonly Dictionary<string, LastLetterRule> LastLetterRules = new()
        {
            ["ه"] = new()
            {
                Action = "slice",
                Value = "ah"
            },

            ["ة"] = new()
            {
                Action = "slice",
                Value = "ah"
            }
        };
    }
    public class SpecialRule
    {
        public string Action { get; set; } = "";
        public Dictionary<string, string> Rules { get; set; } = new();
    }

    public class LastLetterRule
    {
        public string Action { get; set; } = "";
        public string Value { get; set; } = "";
    }
}