using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    class Bishop : Figure
    {
        public Bishop(ChessColor col):base(col)
        {
            if(col==ChessColor.B)
                Sprite = "Image/BB.gif";
            else 
                Sprite = "Image/BW.gif";
        }
        public override void Hint(int _x, int _y, ref List<int[]> points, ref List<int[]> attack, Board Chess)
        {
            int x=_x, y=_y;
           while (x >0 && y>0)
            {
                x--;
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
            x=_x;
            y=_y;
            while (x < 7 && y > 0)
            {
                x++;
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
            x = _x;
            y = _y;
            while (x > 0 && y < 7) 
            {
                x--;
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
            while (x < 7 && y < 7)
            {
                x++;
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
        }
    }
}
