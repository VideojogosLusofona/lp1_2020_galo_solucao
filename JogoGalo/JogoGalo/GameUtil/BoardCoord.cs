using System;
using System.Collections.Generic;
using System.Text;

namespace JogoGalo.GameUtil
{
    class BoardCoord
    {
        private int line;
        private int col;

        public BoardCoord(int line, int col)
        {
            this.line = line;
            this.col = col;
        }

        public int GetLine()
        {
            return line;
        }

        public int GetCol()
        {
            return col;
        }

        public void PrintCoord()
        {
            Console.WriteLine("( " + line + ", " + col + " )");
        }

        public int[] ConvertCoord()
        {
            int[] cArr = new int[2];
            cArr[0] = line;
            cArr[1] = col;
            return cArr;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(BoardCoord))
            {
                return false;
            }
            else
            {
                BoardCoord b2 = (BoardCoord)obj;
                return this.line == b2.line && this.col == b2.col;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(line, col);
        }
    }
}
