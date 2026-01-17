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
        private readonly PieceType _newType;

        public PawnPromotion(Position fromPosition, Position toPosition, PieceType newType)
        {
            FromPosition = fromPosition;
            ToPosition = toPosition;
            _newType = newType;
        }

        private Piece CreatePromotionPiece(Player color)
        {
            return _newType switch
            {
                PieceType.Knight => new Knight(color),
                PieceType.Bishop => new Bishop(color),
                PieceType.Rook => new Rook(color),
                PieceType.Queen => new Queen(color),
                _ => new Queen(color)
            };
        }

        public override bool Execute(Board board)
        {
            Piece pawn = board[FromPosition];
            board[FromPosition] = null;
            Piece promotionPiece = CreatePromotionPiece(pawn.Color);
            promotionPiece.HasMoved = true;
            board[ToPosition] = promotionPiece;
            return true;
        }
    }
}
