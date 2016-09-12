using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    class Knight : Figure
    {
        public Knight(ChessColor col) : base(col)
        {
            if (col == ChessColor.B)
                Sprite = "Image/NB.gif";
            else
                Sprite = "Image/NW.gif";
        }
        public override void Hint(int _x, int _y, ref List<int[]> points, ref List<int[]> attack, Board Chess)
        {
            if (_x - 2 >= 0)
            {
                if (_y - 1 >= 0)
                {
                    if (Chess.Empty(_x - 2, _y - 1))
                        points.Add(new int[] { _x - 2, _y - 1 });
                    else
                        if (Chess.Find(_x - 2, _y - 1).Color != _color)
                            attack.Add(new int[] { _x - 2, _y - 1 });
                }
                if (_y + 1 <= 7)
                {
                    if (Chess.Empty(_x - 2, _y + 1))
                        points.Add(new int[] { _x - 2, _y + 1 });
                    else
                        if (Chess.Find(_x - 2, _y + 1).Color != _color)
                            attack.Add(new int[] { _x - 2, _y + 1 });
                }
            }
            if (_x + 2 <= 7)
            {
                if (_y - 1 >= 0)
                {
                    if (Chess.Empty(_x + 2, _y - 1))
                        points.Add(new int[] { _x + 2, _y - 1 });
                    else
                        if (Chess.Find(_x + 2, _y - 1).Color != _color)
                            attack.Add(new int[] { _x + 2, _y - 1 });
                }
                if (_y + 1 <= 7)
                {
                    if (Chess.Empty(_x + 2, _y + 1))
                        points.Add(new int[] { _x + 2, _y + 1 });
                    else
                        if (Chess.Find(_x + 2, _y + 1).Color != _color)
                            attack.Add(new int[] { _x + 2, _y + 1 });
                }
            }
            if (_y - 2 >= 0)
            {
                if (_x - 1 >= 0)
                {
                    if (Chess.Empty(_x - 1, _y - 2))
                        points.Add(new int[] { _x - 1, _y - 2 });
                    else
                        if (Chess.Find(_x - 1, _y - 2).Color != _color)
                            attack.Add(new int[] { _x - 1, _y - 2 });
                }
                if (_x + 1 <= 7)
                {
                    if (Chess.Empty(_x + 1, _y - 2))
                        points.Add(new int[] { _x + 1, _y - 2 });
                    else
                        if (Chess.Find(_x + 1, _y - 2).Color != _color)
                            attack.Add(new int[] { _x + 1, _y - 2 });
                }
            }
            if (_y + 2 <= 7)
            {
                if (_x - 1 >= 0)
                {
                    if (Chess.Empty(_x - 1, _y + 2))
                        points.Add(new int[] { _x - 1, _y + 2 });
                    else
                        if (Chess.Find(_x - 1, _y + 2).Color != _color)
                            attack.Add(new int[] { _x - 1, _y + 2 });
                }
                if (_x + 1 <= 7)
                {
                    if (Chess.Empty(_x + 1, _y + 2))
                        points.Add(new int[] { _x + 1, _y + 2 });
                    else
                        if (Chess.Find(_x + 1, _y + 2).Color != _color)
                            attack.Add(new int[] { _x + 1, _y + 2 });
                }
            }

        }
    }
}
