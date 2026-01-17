using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class NormalMove : Move
    {
        public override MoveType Type => MoveType.Normal;
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }

        public NormalMove(Position from, Position to)
        {
            FromPosition = from;
            ToPosition = to;
        }

        public override bool Execute(Board board)
        {
            Piece piece = board[FromPosition];
            bool captrue = !board.IsEmty(ToPosition);
            board[ToPosition] = piece;
            board[FromPosition] = null;
            piece.HasMoved = true;
            return captrue || piece.Type == PieceType.Pawn;
        }

    }
}
