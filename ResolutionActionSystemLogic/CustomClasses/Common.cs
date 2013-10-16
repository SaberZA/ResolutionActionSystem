using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ResolutionActionSystemLogic.CustomClasses
{
    public static class Common
    {
        public static ObservableCollection<K> ToObservableCollection<K>(IEnumerable<K> inputItems)
        {
            var items = new ObservableCollection<K>();
            foreach (K item in inputItems)
            {
                items.Add(item);
            }
            return items;
        }
    }
}
