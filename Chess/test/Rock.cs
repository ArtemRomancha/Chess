using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    class Rock : Figure
    {
        public Rock(ChessColor col):base(col)
        {
            if(col==ChessColor.B)
                Sprite = "Image/RB.gif";
            else 
                Sprite = "Image/RW.gif";
        }
        public override void Hint(int _x, int _y, ref List<int[]> points, ref List<int[]> attack, Board Chess)
        {
            int x = _x, y = _y;
            while (x < 7) 
            {
                x++;
                if (Chess.Empty(x, y))
                    points.Add(new int[] { x, y });
                else
                    if (Chess.Find(x, y).Color != _color)
                    {
                        attack.Add(new int[] { x, y });
                        break;
                    }
                    else
                        break;
            }
            x = _x;
            y = _y;
            while (x > 0)
            {
                x--;
                if (Chess.Empty(x, y))
                    points.Add(new int[] { x, y });
                else
                    if (Chess.Find(x, y).Color != _color)
                    {
                        attack.Add(new int[] { x, y });
                        break;
                    }
                    else
                        break;
            }
            x = _x;
            y = _y;
            while (y < 7)
            {
                y++;
                if (Chess.Empty(x, y))
                    points.Add(new int[] { x, y });
                else
                    if (Chess.Find(x, y).Color != _color)
                    {
                        attack.Add(new int[] { x, y });
                        break;
                    }
                    else
                        break;
            }
            x = _x;
            y = _y;
            while (y > 0)
            {
                y--;
                if (Chess.Empty(x, y))
                    points.Add(new int[] { x, y });
                else
                    if (Chess.Find(x, y).Color != _color)
                    {
                        attack.Add(new int[] { x, y });
                        break;
                    }
                    else
                        break;
            }
        }
    }
}
