using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Test
{
    public partial class ChessForm : Form
    {
        BinaryFormatter formatter;
        int Size_Cage = 50;
        Graphics grBack;
        Graphics BkBack;
        Graphics grFront;
        Game_Rules Rules;
        Board Chess_Board;
        List<int[]> HelpList = new List<int[]>();
        List<int[]> AttackList = new List<int[]>();
        bool Clicked = false;
        string moveresult = null;

        int lx = 0, ly = 0;

        void INIT()
        {
            Rules = new Game_Rules();
            Chess_Board = new Board();
            formatter = new BinaryFormatter();

            Chess_Board.Size_cage = Size_Cage;
            ChessBoard.Height = 8 * Size_Cage + 1;
            ChessBoard.Width = 8 * Size_Cage + 1;
            Chess_BK.Location = new Point(ChessBoard.Location.X - 25, ChessBoard.Location.Y - 25);
            Chess_BK.Width = ChessBoard.Width + 50;
            Chess_BK.Height = ChessBoard.Height + 50;
            GameStatus.Top = Chess_BK.Top + Chess_BK.Height + 10;

            Bitmap bkBack = new Bitmap(Chess_BK.Width, Chess_BK.Height);
            Bitmap btmBack = new Bitmap(8 * Size_Cage + 1, 8 * Size_Cage + 1);     
            Bitmap btmFront = new Bitmap(8 * Size_Cage + 1, 8 * Size_Cage + 1);
            BkBack = Graphics.FromImage(bkBack);
            grBack = Graphics.FromImage(btmBack);
            grFront = Graphics.FromImage(btmFront);
            Chess_BK.BackgroundImage = bkBack;            
            ChessBoard.Image = btmFront;
            ChessBoard.BackgroundImage = btmBack;

            Draw_Board();
            Draw_Field();

            ChessBoard.Refresh();
        }
        void New_Game()
        {     
            Chess_Board.Init();
            Rules.NewGame();
                        
            Draw_Figures();            
            ChessBoard.Refresh();
            GameStatus.Visible = false;        
        }
        public ChessForm()
        {
            InitializeComponent();

            INIT();
            
            New_Game();            
        }
        void Draw_Board()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int f = 0; f < 8; f++)
                {
                    grBack.FillRectangle(new SolidBrush(Color.DarkGoldenrod), i * Size_Cage, f * Size_Cage, Size_Cage, Size_Cage);
                    if ((f % 2 == 0 && i % 2 != 0) || (f % 2 != 0 && i % 2 == 0))
                        grBack.FillRectangle(new SolidBrush(Color.MidnightBlue), i * Size_Cage, f * Size_Cage, Size_Cage, Size_Cage);
                }
            }
        }
        public void Draw_Field()
        {
            string field = "ABCDEFGH";
            Font Font_ = new Font("Times New Roman", 16);
            SolidBrush BBrush = new SolidBrush(Color.Black);
            SolidBrush FBrush = new SolidBrush(Color.White);

            BkBack.FillRectangle(BBrush, 0, 0, Chess_BK.Width, Chess_BK.Width);
            for (int i = 0; i < 8; i++)
            {
                BkBack.DrawString(field[i].ToString(), Font_, FBrush, 23 + (i * Size_Cage) + Size_Cage / 2 - 9, 0);
                BkBack.DrawString((i + 1).ToString(), Font_, FBrush, 4, 25 + (i * Size_Cage) + Size_Cage / 2 - 13);
            }
        }
        public void Draw_Hints()
        {
            Pen Help = new Pen(Color.Yellow, 3);
            grFront.Clear(Color.Empty);
            Draw_Figures();
            foreach (int[] a in HelpList)
            {
                grFront.DrawEllipse(Help, a[0] * Size_Cage, a[1] * Size_Cage, Size_Cage, Size_Cage);
            }
            Pen Attack = new Pen(Color.Red, 3);
            foreach (int[] a in AttackList)
            {
                grFront.DrawEllipse(Attack, a[0] * Size_Cage, a[1] * Size_Cage, Size_Cage, Size_Cage);
            }
            ChessBoard.Refresh();
        }
        public void Draw_Figures()
        {
            grFront.Clear(Color.Empty);
            for (int i = 0; i < 8; i++)
            {
                for (int f = 0; f < 8; f++)
                    if (!Chess_Board.Empty(i, f))
                        grFront.DrawImage(Image.FromFile(Chess_Board.Find(i, f).Sprite), i * Size_Cage, f * Size_Cage);
            }
        }
        private void GameBoard_MouseClick(object sender, MouseEventArgs e)
        {
            int X = Cursor.Position.X - this.DesktopLocation.X - ChessBoard.Location.X - 8;
            int Y = Cursor.Position.Y - this.DesktopLocation.Y - ChessBoard.Location.Y - 30;
            int X1 = X / Size_Cage, Y1 = Y / Size_Cage;
            

            if (!Chess_Board.Empty(X1, Y1))
            {
                if (!Clicked)
                {
                    if (Rules.MoveNow == Chess_Board.Find(X1, Y1).Color)
                    {
                        Clicked = true;
                        Chess_Board.Find(X1, Y1).Hint(X1, Y1, ref HelpList, ref AttackList, Chess_Board);

                        Rules.RemoveDanger(Chess_Board, ref HelpList, ref AttackList, X1, Y1);

                        if (HelpList.Count == 0 && AttackList.Count == 0)
                        {
                            Clicked = false;
                        }
                        Draw_Hints();
                    }
                }
                else
                {
                    if (X1 == lx && Y1 == ly)
                    {
                        Clicked = false;
                        HelpList.Clear();
                        AttackList.Clear();
                        grFront.Clear(Color.Empty);
                        Draw_Figures();
                        ChessBoard.Refresh();
                    }
                    else
                    {
                        if (Rules.NextStep(Chess_Board, HelpList, AttackList, lx, ly, X1, Y1, ref moveresult))
                        {
                            Clicked = false;
                            HelpList.Clear();
                            AttackList.Clear();
                            grFront.Clear(Color.Empty);
                            Draw_Figures();
                            ChessBoard.Refresh();
                        }
                        else
                        {
                            Clicked = false;
                            HelpList.Clear();
                            AttackList.Clear();
                            grFront.Clear(Color.Empty);
                            Draw_Figures();
                            ChessBoard.Refresh();
                        }
                    }
                }
            }
            else
                if (Clicked)
                {
                    if (Rules.NextStep(Chess_Board, HelpList, AttackList, lx, ly, X1, Y1, ref moveresult))
                    {
                        Clicked = false;
                        HelpList.Clear();
                        AttackList.Clear();
                        grFront.Clear(Color.Empty);
                        Draw_Figures();
                        ChessBoard.Refresh();
                    }
                    else
                    {
                        Clicked = false;
                        HelpList.Clear();
                        AttackList.Clear();
                        grFront.Clear(Color.Empty);
                        Draw_Figures();
                        ChessBoard.Refresh();
                    }
                }

            lx = X1;
            ly = Y1;

            GameStatus.Visible = true;
            GameStatus.Text = moveresult;
            

            if (Rules.GameOver)
            {
                File.Delete("./ChessBoard.dat");
                File.Delete("./GameRules.dat");

                string output;
                if (!Rules.StaleMate)
                {
                    output = "Игрок ";
                    if (Rules.MoveNow == ChessColor.W)
                    {
                        output += "\"Черный\" проиграл";
                    }
                    else
                    {
                        output += "\"Белый\" проиграл";
                    }
                }
                else
                {
                    output = "Ничья!\n";
                    output += "Игроку ";
                    if (Rules.MoveNow == ChessColor.W)
                    {
                        output += "\"Черный\" был поставлен ПАТ";
                    }
                    else
                    {
                        output += "\"Белый\" был поставлен ПАТ";
                    }
                }
                DialogResult result = MessageBox.Show(output + "\nХотите ли Вы начать новую игру?", "Конец игры", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Chess_Board.Clear();
                    New_Game();
                }
                else
                {
                    ChessBoard.MouseClick -= GameBoard_MouseClick;
                }

            }
        }               

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chess_Board.Clear();
            New_Game();
            ChessBoard.MouseClick += GameBoard_MouseClick;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Rules.GameOver)
            {
                using (var fStream = new FileStream("./ChessBoard.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fStream, Chess_Board);
                }
                using (var fStream = new FileStream("./GameRules.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fStream, Rules);
                }
            }
            else
                MessageBox.Show("Игра окончена! Начните новую игру!");
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("./ChessBoard.dat") && File.Exists("./GameRules.dat"))
            {
                using (var fStream = File.OpenRead("./ChessBoard.dat"))
                {
                    Chess_Board = (Board)formatter.Deserialize(fStream);
                }
                using (var fStream = File.OpenRead("./GameRules.dat"))
                {
                    Rules = (Game_Rules)formatter.Deserialize(fStream);
                }
                Draw_Figures();
                ChessBoard.Refresh();
            }
            else
            {
                MessageBox.Show("Нет сохранений! Начните новую игру");
            }
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Rules.GameOver)
            {
                DialogResult result = MessageBox.Show("Хотите ли Вы перед выходом сохранить игру?", "Сохранить?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (var fStream = new FileStream("./ChessBoard.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        formatter.Serialize(fStream, Chess_Board);
                    }
                    using (var fStream = new FileStream("./GameRules.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        formatter.Serialize(fStream, Rules);
                    }
                }
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void ChessForm_Shown(object sender, EventArgs e)
        {
            if (File.Exists("./ChessBoard.dat") && File.Exists("./GameRules.dat"))
            {
                DialogResult result = MessageBox.Show("Хотите ли Вы загрузить сохраненную игру?", "Обнаружено сохранение!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (var fStream = File.OpenRead("./ChessBoard.dat"))
                    {
                        Chess_Board = (Board)formatter.Deserialize(fStream);
                    }
                    using (var fStream = File.OpenRead("./GameRules.dat"))
                    {
                        Rules = (Game_Rules)formatter.Deserialize(fStream);
                    }
                    Draw_Figures();
                    ChessBoard.Refresh();
                }
            }
        }            
    }
}
