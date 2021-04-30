using System;
using System.Collections.Generic;
using System.Text;

namespace JogoGalo.GameUtil
{

    /// <summary>
    /// Transform into Struct
    /// </summary>
    public struct BoardCoord
    {
        public int line { get; }
        public int col { get; }

        public BoardCoord(int line, int col)
        {
            this.line = line;
            this.col = col;
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

    }
}
