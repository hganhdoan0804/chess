using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class King : Piece
    {
        public override PieaceType Type => PieaceType.King;
        public override Player Color { get; }

        private static readonly Direction[] directions = new Direction[]
        {
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right,
            Direction.UpLeft,
            Direction.UpRight,
            Direction.DownLeft,
            Direction.DownRight
        };

        public King(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = this.HasMoved;
            return copy;
        }

        private IEnumerable<Position> MovePositions(Position fromPosition, Board board)
        {
            foreach (var direction in directions)
            {
                Position toPosition = fromPosition + direction;
                if (!Board.IsInside(toPosition))
                {
                    continue;
                }

                if (board.IsEmty(toPosition) || board[toPosition].Color != Color)
                {
                    yield return toPosition;
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position fromPosition, Board board)
        {
            foreach(Position to in MovePositions(fromPosition, board))
            {
                yield return new NormalMove(fromPosition, to);
            }
        }
    }   
}
