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
        private static readonly Direction[] directions = new Direction[]
        {
            Direction.UpLeft,
            Direction.UpRight,
            Direction.DownLeft,
            Direction.DownRight
        };

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

        public override IEnumerable<Move> GetMoves(Position position, Board board)
        {
            return MovePositions(position, board, directions).Select(x => new NormalMove(position, x));
        }
    }
}
