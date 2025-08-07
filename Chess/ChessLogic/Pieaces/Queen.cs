using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Queen : Piece
    {
        public override PieaceType Type => PieaceType.Queen;
        public override Player Color { get; }

        public Queen(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = this.HasMoved;
            return copy;
        }
    }
}
