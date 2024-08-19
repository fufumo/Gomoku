using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    public class Cross
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; } = 0; //0 NULL 1 A -1 B

        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public void Draw(Graphics g)
        {
            int centerX = Width / 2 + StartX;
            int centerY = Height / 2 + StartY;

            // |
            g.DrawLine(Config.DefaultPen, centerX, StartY, centerX, EndY);
            // --
            g.DrawLine(Config.DefaultPen, StartX, centerY, EndX, centerY);

         
        }
        public void DrawValue(Graphics g)
        {
            if (Value != 0)
            {
                var brush = Value == 1 ? Config.brushA : Config.brushB;

                g.FillEllipse(
                                brush,
                                StartX + (Width / 8),
                                StartY + (Height / 8),
                                (Width / 4) * 3,
                                (Height / 4) * 3
                             );
            }
        }
    }
}
