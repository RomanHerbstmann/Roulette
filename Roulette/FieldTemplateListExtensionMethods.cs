using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette
{
    public static class FieldTemplateListExtensionMethods
    {
        public static FieldTemplate GetRandomEntry(this FieldTemplateList fieldTemplateList, int minIndex, int maxIndex)
        {
            var generator = new Random();
            var index = generator.Next(minIndex, maxIndex);
            var randomEntry = fieldTemplateList.First(item => item.Number == index);
            return randomEntry;
        }
    }
}
