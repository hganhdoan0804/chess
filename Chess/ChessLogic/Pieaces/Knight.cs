using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Knight : Piece
    {
        public override PieaceType Type => PieaceType.Knight;
        public override Player Color { get; }

        public Knight(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = this.HasMoved;
            return copy;
        }

        private static IEnumerable<Position> PotentialToPositions(Position fromPosition)
        {
            foreach (Direction verticalDirection in new Direction[] { Direction.Up, Direction.Down })
            {
                foreach ( Direction horizontalDirection in new Direction[] { Direction.Left, Direction.Right })
                {
                    yield return fromPosition + 2 * verticalDirection + horizontalDirection;
                    yield return fromPosition + 2 * horizontalDirection + verticalDirection;
                }
            }
        }

        private IEnumerable<Position> MovePositions(Position fromPosition, Board board)
        {
            return PotentialToPositions(fromPosition).Where(x => Board.IsInside(x) && (board.IsEmty(x) || board[x].Color != Color));
        }

        public override IEnumerable<Move> GetMoves(Position fromPosition, Board board)
        {
            return MovePositions(fromPosition, board).Select(x => new NormalMove(fromPosition, x)).ToList();
        }
    }
}
