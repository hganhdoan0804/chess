using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; } = false;
        public abstract Piece Copy();
        public abstract IEnumerable<Move> GetMoves(Position fromPosition, Board board);

        protected IEnumerable<Position> MovePositionsInDirection(Position fromPosition, Board board, Direction direction)
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
            return directions.SelectMany(direction => MovePositionsInDirection(fromPosition, board, direction));
        }

        public virtual bool CanCaptureOpponentKing(Position from, Board board)
        {
            return GetMoves(from, board).Any(move =>
            {
                Piece piece = board[move.ToPosition];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
