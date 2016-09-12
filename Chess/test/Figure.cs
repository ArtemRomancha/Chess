using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public enum ChessColor{B, W}
    [Serializable]
    public abstract class Figure
    {
        protected ChessColor _color;
        private string _sprite;     
       
        public ChessColor Color
        {
            get { return _color; }
        }
        public string Sprite
        {
            get {return _sprite;}
            set {_sprite=value;}
        }
        public Figure(ChessColor col)
        {
            _color = col;
        }
        public abstract void Hint(int _x, int _y,ref List<int[]> points, ref List<int[]> attack, Board Chess);
    }
}
