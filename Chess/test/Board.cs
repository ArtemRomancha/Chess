using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public class Board
    {
        private Figure[,] table = new Figure[8, 8];
        public int Size_cage;

        public void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                table[i, 1] = new Pawn(ChessColor.B);
                table[i, 6] = new Pawn(ChessColor.W);
            }
            
            table[0, 0] = new Rock(ChessColor.B);
            table[7, 0] = new Rock(ChessColor.B);
            table[0, 7] = new Rock(ChessColor.W);
            table[7, 7] = new Rock(ChessColor.W);
            
            table[1, 0] = new Knight(ChessColor.B);
            table[6, 0] = new Knight(ChessColor.B);
            table[1, 7] = new Knight(ChessColor.W);
            table[6, 7] = new Knight(ChessColor.W);
            
            table[2, 0] = new Bishop(ChessColor.B);
            table[5, 0] = new Bishop(ChessColor.B);
            table[2, 7] = new Bishop(ChessColor.W);
            table[5, 7] = new Bishop(ChessColor.W);

            table[4, 0] = new King(ChessColor.B);
            table[3, 0] = new Queen(ChessColor.B);

            table[4, 7] = new King(ChessColor.W);
            table[3, 7] = new Queen(ChessColor.W);
        }
        public void Clear()
        {
            for (int i = 0; i < table.GetLength(0); i++)
                for (int f = 0; f < table.GetLength(1); f++)
                    table[i, f] = null;
        }
        public void Move(int x, int y, int x1, int y1)
        {
            table[x1, y1] = table[x, y];
            table[x, y] = null;
        }
        public void KillFigure(int x, int y)
        {
            table[x, y] = null;
        }
        public void RestoreFigure(int x, int y, Figure F)
        {
            table[x, y] = F;
        }
        public bool Empty(int x, int y)
        {
            if (table[x, y] == null)
                return true;
            else
                return false;
        }
        public Figure Find(int x, int y)
        {
            return table[x, y];
        }        
    }
}
