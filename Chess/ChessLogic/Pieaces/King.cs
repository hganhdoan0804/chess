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

        private static bool IsUnmovedRook(Position position, Board board)
        {
            if (board.IsEmty(position))
            {
                return false;
            }
            Piece piece = board[position];
            return piece.Type == PieaceType.Rook && !piece.HasMoved;
        }

        private static bool AllEmpty(IEnumerable<Position> positions, Board board)
        {
            return positions.All(position => board.IsEmty(position));
        }

        private bool CanCastleKingSide(Position fromPosition, Board board)
        {
            if (HasMoved)
            {
                return false;
            }
            Position rookPosition = new Position(fromPosition.Row, 7);
            Position[] betweenPositions = new Position[] { new(fromPosition.Row, 5), new(fromPosition.Row, 6) }; 
            return IsUnmovedRook(rookPosition, board) && AllEmpty(betweenPositions, board);
        }

        private bool CanCastleQueenSide(Position fromPosition, Board board)
        {
            if (HasMoved)
            {
                return false;
            }
            Position rookPosition = new Position(fromPosition.Row, 0);
            Position[] betweenPositions = new Position[] { new(fromPosition.Row, 1), new(fromPosition.Row, 2), new(fromPosition.Row, 3) };
            return IsUnmovedRook(rookPosition, board) && AllEmpty(betweenPositions, board);
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

            if(CanCastleKingSide(fromPosition, board))
            {
                yield return new Castle(MoveType.CastleKingSide, fromPosition);
            }
            if(CanCastleQueenSide(fromPosition, board))
            {
                yield return new Castle(MoveType.CastleQueenSide, fromPosition);
            }
        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return GetMoves(from, board).Any(move =>
            {
                Piece piece = board[move.ToPosition];
                return piece != null && piece.Type == PieaceType.King;
            });
        }
    }   
}
