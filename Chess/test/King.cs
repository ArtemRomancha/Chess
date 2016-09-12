using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    class King : Figure
    {
        List<int[]> helpl=new List<int[]>();
        List<int[]> tmphelp = new List<int[]>();
        public King(ChessColor col) : base(col)
        {
            if (col == ChessColor.B)
                Sprite = "Image/KB.gif";
            else
                Sprite = "Image/KW.gif";
        }
        public override void Hint(int _x, int _y, ref List<int[]> points, ref List<int[]> attack, Board Chess)
        {
            if (_x + 1 <= 7)
            {
                if (_y + 1 <= 7)
                {
                    if (Chess.Empty(_x + 1, _y + 1))
                        points.Add(new int[] { _x + 1, _y + 1 });
                    else
                        if (Chess.Find(_x + 1, _y + 1).Color != _color)
                            attack.Add(new int[] { _x + 1, _y + 1 });
                }
                if (_y - 1 >= 0)
                {
                    if (Chess.Empty(_x + 1, _y - 1))
                        points.Add(new int[] { _x + 1, _y - 1 });
                    else
                        if (Chess.Find(_x + 1, _y - 1).Color != _color)
                            attack.Add(new int[] { _x + 1, _y - 1 });
                }
                if (Chess.Empty(_x + 1, _y))
                    points.Add(new int[] { _x + 1, _y });
                else
                    if (Chess.Find(_x + 1, _y).Color != _color)
                        attack.Add(new int[] { _x + 1, _y });
            }

            if (_x <= 7 && _x >= 0)
            {
                if (_y + 1 <= 7)
                {
                    if (Chess.Empty(_x, _y + 1))
                        points.Add(new int[] { _x, _y + 1 });
                    else
                        if (Chess.Find(_x, _y + 1).Color != _color)
                            attack.Add(new int[] { _x, _y + 1 });
                }
                if (_y - 1 >= 0)
                {
                    if (Chess.Empty(_x, _y - 1))
                        points.Add(new int[] { _x, _y - 1 });
                    else
                        if (Chess.Find(_x, _y - 1).Color != _color)
                            attack.Add(new int[] { _x, _y - 1 });
                }
            }

            if (_x - 1 >= 0)
            {
                if (_y + 1 <= 7)
                {
                    if (Chess.Empty(_x - 1, _y + 1))
                        points.Add(new int[] { _x - 1, _y + 1 });
                    else
                        if (Chess.Find(_x - 1, _y + 1).Color != _color)
                            attack.Add(new int[] { _x - 1, _y + 1 });
                }
                if (_y - 1 >= 0)
                {
                    if (Chess.Empty(_x - 1, _y - 1))
                        points.Add(new int[] { _x - 1, _y - 1 });
                    else
                        if (Chess.Find(_x - 1, _y - 1).Color != _color)
                            attack.Add(new int[] { _x - 1, _y - 1 });
                }
                if (Chess.Empty(_x - 1, _y))
                    points.Add(new int[] { _x - 1, _y });
                else
                    if (Chess.Find(_x - 1, _y).Color != _color)
                        attack.Add(new int[] { _x - 1, _y });
            }
            //Отсечение точек где могут атаковать другие фигуры
            for (int i = 0; i < 8; i++) 
            {
                for (int t = 0; t < 8; t++)
                {
                    if (!Chess.Empty(t, i))
                    {
                        Figure figur = Chess.Find(t, i);
                        if (figur.Color != _color)
                        {
                            if (!(figur is Pawn))
                            {
                                if (!(figur is King))
                                {
                                    figur.Hint(t, i, ref helpl, ref tmphelp, Chess);
                                    for (int v = 0; v < helpl.Count; v++)
                                    {
                                        for (int w = 0; w < points.Count; w++)
                                            if (helpl[v][0] == points[w][0] && helpl[v][1] == points[w][1])
                                                points.Remove(points[w]);
                                    }
                                }
                            }
                            else
                            {                                
                                if(_color==ChessColor.W)
                                {
                                    if (i + 1 <= 7)
                                    {
                                        if (t + 1 <= 7)
                                            for (int w = 0; w < points.Count; w++)
                                                if ((t + 1) == points[w][0] && (i + 1) == points[w][1])
                                                    points.Remove(points[w]);
                                        if (t - 1 >= 0)
                                            for (int w = 0; w < points.Count; w++)
                                                if ((t - 1) == points[w][0] && (i + 1) == points[w][1])
                                                    points.Remove(points[w]);
                                    }
                                }
                                else
                                {
                                    if (t + 1 <= 7)
                                        for (int w = 0; w < points.Count; w++)
                                            if ((t + 1) == points[w][0] && (i - 1) == points[w][1])
                                                points.Remove(points[w]);
                                    if (t - 1 >= 0)
                                        for (int w = 0; w < points.Count; w++)
                                            if ((t - 1) == points[w][0] && (i - 1) == points[w][1])
                                                points.Remove(points[w]);
                                }

                            }
                        }
                    }
                    helpl.Clear();
                    tmphelp.Clear();
                }
            }            
        }     
    }
}
