using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    [Serializable]
    public class Game_Rules
    {
        ChessColor move_now;
        bool gameover = false;
        bool stalemate = false;
        public ChessColor MoveNow
        {
            get { return move_now; }
        }
        public bool GameOver
        {
            get { return gameover; }
        }
        public bool StaleMate
        { get { return stalemate; } }
        public void NewGame()
        {
            move_now = ChessColor.W;
            gameover = false;
        }
        public Game_Rules()
        {            
            move_now = ChessColor.W;
        }        
        public bool NextStep(Board ChessBoard, List<int[]> HelpList, List<int[]> AttackList, int x, int y, int xl, int yl, ref string result)
        {
            if (TryMoveSafely(ChessBoard, HelpList, AttackList, x, y, xl, yl, move_now))
            {
                ChessBoard.Move(x, y, xl, yl);
                if (Control_Check(ChessBoard, move_now))
                {
                    if (!Control_CheckMate(ChessBoard))
                    {
                        
                        result = "ШАХ ";
                        if (move_now == ChessColor.W)
                        {
                            result += "Черным";
                        }
                        else
                        {
                            result += "Белым";
                        }
                        
                        if (move_now == ChessColor.W)
                        {
                            move_now = ChessColor.B;
                        }
                        else
                        {
                            move_now = ChessColor.W;
                        }
                        return true;
                    }
                    else
                    {                        
                        gameover = true;
                        result = "МАТ ";
                        if (move_now == ChessColor.W)
                        {
                            result += "Черным";
                        }
                        else
                        {
                            result += "Белым";
                        }
                    }
                }
                else
                {
                    if (Control_Stalemate(ChessBoard, move_now))
                    {                        
                        gameover = true;
                        stalemate = true;
                        result = "ПАТ ";
                        if (move_now == ChessColor.W)
                        {
                            result += "Черным";
                        }
                        else
                        {
                            result += "Белым";
                        }
                    }
                    else
                    {                        
                        if (move_now == ChessColor.W)
                        {
                            move_now = ChessColor.B;
                        }
                        else
                        {
                            move_now = ChessColor.W;
                        }
                        result = null;
                        return true;
                    }
                }                
            }
            return false;
        }
        public void RemoveDanger(Board ChessBoard, ref List<int[]> HelpList, ref List<int[]> AttackList, int X1, int Y1)
        {
            for (int i = 0; i < HelpList.Count; i++)
            {
                if (!TryMoveSafely(ChessBoard, HelpList, AttackList, X1, Y1, HelpList[i][0], HelpList[i][1], MoveNow))
                {
                    HelpList.Remove(HelpList[i]);
                    --i;
                }
            }
            for (int i = 0; i < AttackList.Count; i++)
            {
                if (!TryMoveSafely(ChessBoard, HelpList, AttackList, X1, Y1, AttackList[i][0], AttackList[i][1], MoveNow))
                {
                    AttackList.Remove(AttackList[i]);
                    --i;
                }
            }            
        }
        public bool TryMoveSafely(Board ChessBoard, List<int[]> HelpList, List<int[]> AttackList, int x, int y, int xl, int yl, ChessColor Color) 
        {            
            if (Color == ChessColor.W)
            {
                Color = ChessColor.B;
            }
            else
            {
                Color = ChessColor.W;
            }
            
            foreach (int[] point in HelpList)
                if (point[0] == xl && point[1] == yl)
                {
                    ChessBoard.Move(x, y, xl, yl);
                    if (!Control_Check(ChessBoard, Color))
                    {
                        ChessBoard.Move(xl, yl, x, y);
                        return true;
                    }
                    else
                    {
                        ChessBoard.Move(xl, yl, x, y);
                        return false;
                    }
                }
            foreach (int[] point in AttackList)
                if (point[0] == xl && point[1] == yl)
                {
                    Figure Temp = ChessBoard.Find(xl, yl);
                    
                    ChessBoard.KillFigure(xl, yl);
                    ChessBoard.Move(x, y, xl, yl);
                    if (!Control_Check(ChessBoard, Color))
                    {
                        ChessBoard.Move(xl, yl, x, y);
                        ChessBoard.RestoreFigure(xl, yl, Temp);
                        return true;
                    }
                    else
                    {
                        ChessBoard.Move(xl, yl, x, y);
                        ChessBoard.RestoreFigure(xl, yl, Temp);
                        return false;
                    }
                }
            return false;
        }
        private bool Control_Check(Board ChessBoard, ChessColor col)
        {            
                int king_X = -1, king_Y = -1;
                for (int i = 0; i < 8; i++)
                {
                    for (int f = 0; f < 8; f++)
                    {
                        if (!ChessBoard.Empty(i, f))
                        {
                            if (ChessBoard.Find(i, f).Color != col)
                            {
                                if (ChessBoard.Find(i, f) is King)
                                {
                                    king_X = i;
                                    king_Y = f;
                                    break;
                                }
                            }
                        }
                    }
                }


                List<int[]> attk = new List<int[]>();
                List<int[]> points = new List<int[]>();

                for (int i = 0; i < 8; i++)
                {
                    for (int f = 0; f < 8; f++)
                    {
                        if (!ChessBoard.Empty(i, f))
                            if (ChessBoard.Find(i, f).Color == col)
                            {
                                ChessBoard.Find(i, f).Hint(i, f, ref points, ref attk, ChessBoard);
                                for (int t = 0; t < attk.Count; t++)
                                {
                                    if (attk[t][0] == king_X && attk[t][1] == king_Y)
                                    {   
                                        return true;
                                    }
                                }
                                attk.Clear();
                                points.Clear();
                            }
                    }
                }                
                             
            
            return false;
        }
        private bool Control_CheckMate(Board ChessBoard)
        {
            ChessColor temp;
            
            if (move_now == ChessColor.W)
            {
                temp = ChessColor.B;
            }
            else
            {
                temp = ChessColor.W;
            }

            List<int[]> attk = new List<int[]>();
            List<int[]> points = new List<int[]>();
            int [] XY =new int [2];

            for (int i = 0; i < 8; i++)
            {
                for (int f = 0; f < 8; f++)
                {
                    if (!ChessBoard.Empty(i, f))
                        if (ChessBoard.Find(i, f).Color == temp)
                        {
                            ChessBoard.Find(i, f).Hint(i, f, ref points, ref attk, ChessBoard);
                            for (int t = 0; t < points.Count; t++)
                            {
                                if (TryMoveSafely(ChessBoard, points, attk, i, f, points[t][0], points[t][1], temp))
                                {
                                    return false;
                                }
                            }
                            for (int t = 0; t < attk.Count; t++)
                            {
                                if (TryMoveSafely(ChessBoard, points, attk, i, f, attk[t][0], attk[t][1], move_now))
                                {
                                    return false;
                                }
                            }
                            attk.Clear();
                            points.Clear();
                        }
                }
            } 
            return true;
        }
        bool Control_Stalemate(Board ChessBoard, ChessColor col)
        {
            if (col == ChessColor.W)
            {
                col = ChessColor.B;
            }
            else
            {
                col = ChessColor.W;
            }            
            
            List<int[]> tempmv = new List<int[]>();
            List<int[]> tempatt = new List<int[]>();


            for (int i = 0; i < 8; i++)
            {
                for (int f = 0; f < 8; f++)
                {
                    if (!ChessBoard.Empty(i, f)) 
                    {
                        if (ChessBoard.Find(i, f).Color == col)
                        {
                            ChessBoard.Find(i, f).Hint(i, f, ref tempmv, ref tempatt, ChessBoard);
                            if(tempmv.Count!=0 || tempatt.Count!=0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }            
            return true;
        }
    }
}
