using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    class Grid
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int CellWidth { get; set; }
        public int CellHeight { get; set; }

        public Grid(int rows, int columns, int cellWidth, int cellHeight)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.CellWidth = cellWidth;
            this.CellHeight = cellHeight;
        }

        public Vector2 GetGridPosition(Point point)
        {
            return new Vector2(point.X * CellWidth, point.Y * CellHeight);
        }
    }
}
