using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class Pawn : Figure
    {
        public Pawn(ChessColor col):base(col)
        {
            if(col==ChessColor.B)
                Sprite = "Image/pB.gif";
            else 
                Sprite = "Image/pW.gif";
        }
        public override void Hint(int _x, int _y, ref List<int[]> points, ref List<int[]> attack, Board Chess)
        {
            if (_color == ChessColor.W)
            {
                if (_y > 0)
                {
                    if (_y > 5)
                    {
                        for (int i = 1; i <= 2; i++)
                        {
                            if (Chess.Empty(_x, _y - i))                            
                                points.Add(new int[] { _x, _y - i });                            
                            else
                                break;
                        }
                    }
                    else
                    {
                        if (Chess.Empty(_x, _y - 1))
                            points.Add(new int[] { _x, _y - 1 });
                    }                
                }
                if (_x < 7 && _y > 0)
                    if (!Chess.Empty(_x + 1, _y - 1) && Chess.Find(_x + 1, _y - 1).Color != ChessColor.W)
                        attack.Add(new int[] { _x + 1, _y - 1 });
                if (_x > 0 && _y > 0)
                    if (!Chess.Empty(_x - 1, _y - 1) && Chess.Find(_x - 1, _y - 1).Color != ChessColor.W)
                        attack.Add(new int[] { _x - 1, _y - 1 });
            }
            else
            {
                if (_y < 7)
                {
                    if (_y < 2)
                    {
                        for (int i = 1; i <= 2; i++)
                        {
                            if (Chess.Empty(_x, _y + i))                            
                                points.Add(new int[] { _x, _y + i });
                            else
                                break;
                        }                        
                    }
                    else
                    {
                        if (Chess.Empty(_x, _y + 1))
                            points.Add(new int[] { _x, _y + 1 });
                    }
                }
                if (_x < 7 && _y < 7)
                    if (!Chess.Empty(_x + 1, _y + 1) && Chess.Find(_x + 1, _y + 1).Color != ChessColor.B)
                        attack.Add(new int[] { _x + 1, _y + 1 });
                if (_x > 0 && _y < 7)
                    if (!Chess.Empty(_x - 1, _y + 1) && Chess.Find(_x - 1, _y + 1).Color != ChessColor.B)
                        attack.Add(new int[] { _x - 1, _y + 1 });
            }
        }
    }
}
