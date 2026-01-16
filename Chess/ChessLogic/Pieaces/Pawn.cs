using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Pawn : Piece
    {
        public override PieaceType Type => PieaceType.Pawn;
        public override Player Color { get; }
        private readonly Direction forward;
        public Pawn(Player color)
        {
            Color = color;
            if (color == Player.White)
            {
                forward = Direction.Up;
            }
            else if (color == Player.Black)
            {
                forward = Direction.Down;
            }
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = this.HasMoved;
            return copy;
        }

        private static bool CanMoveTo(Position position, Board board)
        {
            return Board.IsInside(position) && board.IsEmty(position);
        }

        private bool CanCaptureAt(Position position, Board board)
        {
            if (!Board.IsInside(position) || board.IsEmty(position))
            {
                return false;
            }
            return board[position].Color != Color;
        }

        private static IEnumerable<Move> PromotionsMove(Position fromPosition, Position toPosition)
        {
            yield return new PawnPromotion(fromPosition, toPosition, PieaceType.Knight);
            yield return new PawnPromotion(fromPosition, toPosition, PieaceType.Bishop);
            yield return new PawnPromotion(fromPosition, toPosition, PieaceType.Rook);
            yield return new PawnPromotion(fromPosition, toPosition, PieaceType.Queen);
        }

        private IEnumerable<Move> ForwardMoves(Position fromPosition, Board board)
        {
            Position oneMove = fromPosition + forward;
            if (CanMoveTo(oneMove, board))
            {
                if(oneMove.Row == 0 || oneMove.Row == 7)
                {
                    foreach(Move promotionMove  in PromotionsMove(fromPosition, oneMove))
                    {
                        yield return promotionMove;
                    }
                }
                else
                {
                    yield return new NormalMove(fromPosition, oneMove);
                }

                Position twoMoves = oneMove + forward;
                if(!HasMoved && CanMoveTo(twoMoves, board))
                {
                    yield return new DoublePawn(fromPosition, twoMoves);
                }
            }
        }

        private IEnumerable<Move> CaptureMoves(Position fromPosition, Board board)
        {
            foreach (Direction direction in new Direction[] {Direction.Left, Direction.Right })
            {
                Position toPosition = fromPosition + forward + direction;
                if(toPosition == board.GetPawnSkipPosition(Color.Opposite()))
                {
                    yield return new EnPassant(fromPosition, toPosition);
                }
                else if(CanCaptureAt(toPosition, board))
                {
                    if (toPosition.Row == 0 || toPosition.Row == 7)
                    {
                        foreach (Move promotionMove in PromotionsMove(fromPosition, toPosition))
                        {
                            yield return promotionMove;
                        }
                    }
                    else
                    {
                        yield return new NormalMove(fromPosition, toPosition);
                    }
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position fromPosition, Board board)
        {
            return ForwardMoves(fromPosition, board).Concat(CaptureMoves(fromPosition, board));
        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return CaptureMoves(from, board).Any(move =>
            {
                Piece piece = board[move.ToPosition];
                return piece != null && piece.Type == PieaceType.King;
            });
        }
    }
}
