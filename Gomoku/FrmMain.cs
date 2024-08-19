using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class FrmMain : Form
    {
        private List<Cross> crossList = new List<Cross>();
        private int currentUser = -1;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            crossList.Clear();
            for (int i = 0; i < Config.BoardSize * Config.BoardSize; i++)
            {
                crossList.Add(new Cross
                {
                    Row = i / Config.BoardSize,
                    Column = i % Config.BoardSize,
                });
            }
        }

        private void pbBoard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
           
            g.Clear(Config.BoardColor);

            //item size
            int h = this.pbBoard.Height / Config.BoardSize;
            int w = this.pbBoard.Width / Config.BoardSize;

            foreach (Cross cross in crossList)
            {
                cross.Height = h;
                cross.Width = w;
                cross.StartX = cross.Row * w;
                cross.StartY = cross.Column * h;
                cross.EndX = cross.StartX + w;
                cross.EndY = cross.StartY + h;
                cross.Draw(g);
            }

            #region Border
            g.FillRectangle(new SolidBrush(Config.BoardColor), -1, -1, w / 2 + 1, this.pbBoard.Height + 1);
            g.FillRectangle(new SolidBrush(Config.BoardColor), this.pbBoard.Width - w / 2, 0, w / 2, this.pbBoard.Height);
            g.FillRectangle(new SolidBrush(Config.BoardColor), -1, -1, this.pbBoard.Width + 1, h / 2 + 1);
            g.FillRectangle(new SolidBrush(Config.BoardColor), 0, this.pbBoard.Height - h / 2, this.pbBoard.Width, h / 2);
            g.DrawRectangle(new Pen(Color.Black, 4), w / 2, h / 2, this.pbBoard.Width - w, this.pbBoard.Height - h);
            #endregion

            foreach (Cross cross in crossList)
            {
                cross.DrawValue(g);
            }
        }

        private void pbBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            var cross = crossList.FirstOrDefault(t =>
                e.X > t.StartX
             && e.X < t.EndX
             && e.Y > t.StartY
             && e.Y < t.EndY
             && t.Value == 0
            );
            if (cross == null) return;
            cross.Value = currentUser;
            currentUser = -currentUser;

            pbBoard.Invalidate();

            if (GuideWin(cross.Row, cross.Column, cross.Value))
            {
                var userName = cross.Value == 1 ? "Black" : "White";
                $"{userName} Win".Show();
                ResetGame();
            }

            if (!crossList.Any(t => t.Value == 0))
            {
                "A dead heat".Show();
                ResetGame();
            }
        }

        private void ResetGame()
        {
            Init();
            pbBoard.Invalidate();
            currentUser = -1;
        }

        public bool GuideWin(int row, int column, int value)
        {
            //guide |
            var columnList = crossList.Where(t => t.Column == column && Math.Abs(t.Row - row) <= 4);
            bool columnWin = columnList.CheckWin(value);


            //guide --
            var rowList = crossList.Where(t => t.Row == row && Math.Abs(t.Column - column) <= 4);
            bool rowWin = rowList.CheckWin(value);

            //guide \
            var ltrList = crossList.Where(t => (t.Row - row) == (t.Column - column) && Math.Abs(t.Row - row) <= 4);
            bool ltrWin = ltrList.CheckWin(value);

            //guide /
            var rtlList = crossList.Where(t => (t.Row - row) == -(t.Column - column) && Math.Abs(t.Row - row) <= 4);
            bool rtlWin = rtlList.CheckWin(value);

            return columnWin || rowWin || ltrWin || rtlWin;
        }
    }
}
