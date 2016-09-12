using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    class Queen : Figure
    {
        public Queen(ChessColor col):base(col)
        {
            if(col==ChessColor.B)
                Sprite = "Image/QB.gif";
            else 
                Sprite = "Image/QW.gif";
        }
        public override void Hint(int _x, int _y, ref List<int[]> points, ref List<int[]> attack, Board Chess)
        {
            int x=_x, y=_y;
            
            while (y < 7)
            {
                y++;
                if (Chess.Empty(_x, y))
                    points.Add(new int[] { _x, y });
                else
                    if (Chess.Find(_x, y).Color != _color)
                    {
                        attack.Add(new int[] { _x, y });
                        break;
                    }
                    else
                        break;

            }
            y = _y;

            while (x < 7)
            {
                x++;
                if (Chess.Empty(x, _y))
                    points.Add(new int[] { x, _y });
                else
                    if (Chess.Find(x, _y).Color != _color)
                    {
                        attack.Add(new int[] { x, _y });
                        break;
                    }
                    else
                        break;

            }
            x = _x;

            while (y > 0)
            {
                y--;
                if (Chess.Empty(_x, y))
                    points.Add(new int[] { _x, y });
                else
                    if (Chess.Find(_x, y).Color != _color)
                    {
                        attack.Add(new int[] { _x, y });
                        break;
                    }
                    else
                        break;

            }
            y = _y;

            while (x > 0)
            {
                x--;
                if (Chess.Empty(x, _y))
                    points.Add(new int[] { x, _y });
                else
                    if (Chess.Find(x, _y).Color != _color)
                    {
                        attack.Add(new int[] { x, _y });
                        break;
                    }
                    else
                        break;

            }
            x = _x;

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
            x = _x;
            y = _y;

            while (x > 0 && y > 0)
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
            x = _x;
            y = _y;

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
        }
    }
}
