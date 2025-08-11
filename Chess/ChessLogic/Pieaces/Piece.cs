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
        public abstract IEnumerable<Move> GetMoves(Position position, Board board);

        protected IEnumerable<Position> MovePositions(Position fromPosition, Board board, Direction direction)
        {
            for(Position pos = fromPosition + direction; Board.IsInside(pos); pos += direction)
            {
                if (board.IsEmty(pos))
                {
                    yield return pos;
                    continue;
                }

                Piece piece = board[pos];
                if (piece.Color != Color)
                {
                    yield return pos;
                }
                yield break;
            }
        }

        protected IEnumerable<Position> MovePositions(Position fromPosition, Board board, Direction[] directions)
        {
            return directions.SelectMany(x => MovePositions(fromPosition, board, directions));
        }
    }
}
