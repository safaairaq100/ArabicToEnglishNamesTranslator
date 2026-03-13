using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicToEnglishNamesTranslator.Abstractions
{
    public interface IArabicNameTranslator
    {
        string Translate(string arabicName);

    }
}
