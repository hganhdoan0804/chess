using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class PawnPromotion : Move
    {
        public override MoveType Type => MoveType.Promotion;
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }
        private readonly PieaceType _newType;

        public PawnPromotion(Position fromPosition, Position toPosition, PieaceType newType)
        {
            FromPosition = fromPosition;
            ToPosition = toPosition;
            _newType = newType;
        }

        private Piece CreatePromotionPiece(Player color)
        {
            return _newType switch
            {
                PieaceType.Knight => new Knight(color),
                PieaceType.Bishop => new Bishop(color),
                PieaceType.Rook => new Rook(color),
                PieaceType.Queen => new Queen(color),
                _ => new Queen(color)
            };
        }

        public override void Execute(Board board)
        {
            Piece pawn = board[FromPosition];
            board[FromPosition] = null;
            Piece promotionPiece = CreatePromotionPiece(pawn.Color);
            promotionPiece.HasMoved = true;
            board[ToPosition] = promotionPiece;
        }
    }
}
