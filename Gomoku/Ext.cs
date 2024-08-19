using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    static class Ext
    {
        public static void Show(this string str) => MessageBox.Show(str);
        public static bool CheckWin(this IEnumerable<Cross> list, int value)
        {
            return list
                    .Select((item, index) => new { item, index })
                    .Any(t => list.Skip(t.index).Take(5).Count(x => x.Value == value) == 5);
        }
    }
}
