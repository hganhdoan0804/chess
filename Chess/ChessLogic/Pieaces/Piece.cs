using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class Piece
    {
        public abstract PieaceType Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; } = false;

        public abstract Piece Copy();
    }
}
