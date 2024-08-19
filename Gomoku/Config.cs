using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    public class Config
    {
        public static SolidBrush brushA = new SolidBrush(Color.Black);
        public static SolidBrush brushB = new SolidBrush(Color.White);

        // 15 * 15
        public const int BoardSize = 15;
        public static Pen DefaultPen=new Pen(Color.Black, 2);

        public static Color BoardColor=Color.Gainsboro;

       
    }
}
