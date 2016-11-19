using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette
{
    public static class ObservableIntCollectionExtensonMethods
    {
        public static ObservableCollection<int> Sort(this ObservableCollection<int> observableCollection)
        {
            var sortetList = observableCollection.ToList();
            sortetList.Sort();
            return new ObservableCollection<int>(sortetList);
        }
    }
}
