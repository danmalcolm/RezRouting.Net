using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RezRouting.Utility
{
    internal class TextUtility
    {
        public static string FormatList(IEnumerable<string> items, string separator, string lastSeparator = null)
        {
            if (lastSeparator == null)
            {
                lastSeparator = separator;
            }

            var itemList = items.ToList();
            var list = new StringBuilder();
            int lastSeparatorIndex = itemList.Count - 2;
            for(int i = 0; i < itemList.Count; i++)
            {
                list.Append(itemList[i]);
                if (i <= lastSeparatorIndex)
                {
                    list.Append(i < lastSeparatorIndex ? separator : lastSeparator);
                }
            }
            return list.ToString();
        }
    }
}