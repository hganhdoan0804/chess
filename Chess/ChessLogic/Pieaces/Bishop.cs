using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Bishop : Piece
    {
        public override PieaceType Type => PieaceType.Bishop;
        public override Player Color { get; }
        public Bishop(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = this.HasMoved;
            return copy;
        }
    }
}
