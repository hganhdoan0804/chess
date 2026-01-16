using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];
        private readonly Dictionary<Player, Position> pawnSkipPositions = new Dictionary<Player, Position>()
        {
            {Player.White, null },
            { Player.Black, null },
        };

        public Piece this[int row, int col]
        {
            get => pieces[row, col];
            set => pieces[row, col] = value;
        }

        public Piece this[Position pos]
        {
            get => pieces[pos.Row, pos.Column];
            set => pieces[pos.Row, pos.Column] = value;
        }

        public Position GetPawnSkipPosition(Player player)
        {
            return pawnSkipPositions[player];
        }

        public void SetPawnSkipPosition(Player player, Position position)
        {
            pawnSkipPositions[player] = position;
        }

        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;
        }

        private void AddStartPieces()
        {
            // Add Pawns
            for (int col = 0; col < 8; col++)
            {
                pieces[1, col] = new Pawn(Player.Black);
                pieces[6, col] = new Pawn(Player.White);
            }
            // Add Rooks
            pieces[0, 0] = new Rook(Player.Black);
            pieces[0, 7] = new Rook(Player.Black);
            pieces[7, 0] = new Rook(Player.White);
            pieces[7, 7] = new Rook(Player.White);
            // Add Knights
            pieces[0, 1] = new Knight(Player.Black);
            pieces[0, 6] = new Knight(Player.Black);
            pieces[7, 1] = new Knight(Player.White);
            pieces[7, 6] = new Knight(Player.White);
            // Add Bishops
            pieces[0, 2] = new Bishop(Player.Black);
            pieces[0, 5] = new Bishop(Player.Black);
            pieces[7, 2] = new Bishop(Player.White);
            pieces[7, 5] = new Bishop(Player.White);
            // Add Queens
            pieces[0, 3] = new Queen(Player.Black);
            pieces[7, 3] = new Queen(Player.White);
            // Add Kings
            pieces[0, 4] = new King(Player.Black);
            pieces[7, 4] = new King(Player.White);
        }

        public static bool IsInside(Position position)
        {
            return position.Row >= 0 && position.Row < 8 && position.Column >= 0 && position.Column < 8;
        }

        public bool IsEmty(Position position)
        {
            return this[position] == null;
        }

        public IEnumerable<Position> PiecePositions()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Position position = new Position(row, column);
                    if (!IsEmty(position))
                    {
                        yield return position;
                    }
                }
            }

        }

        public IEnumerable<Position> PiecePositionsFor(Player player)
        {
            return PiecePositions().Where(position => this[position].Color == player);
        }

        public bool IsInCheck(Player player)
        {
            return PiecePositionsFor(player.Opposite()).Any(position =>
            {
                Piece piece = this[position];
                return piece.CanCaptureOpponentKing(position, this);
            });
        }

        public Board Copy()
        {
            Board copy = new Board();
            foreach(Position position in PiecePositions())
            {
                copy[position] = this[position].Copy();
            }
            return copy;
        }

        public Counting CountPiece()
        {
            Counting counting = new Counting();
            foreach(Position position in PiecePositions())
            {
                Piece piece = this[position];
                counting.Increment(piece.Color, piece.Type);
            }
            return counting;
        }

        public bool InsufficientMaterial()
        {
            Counting counting = CountPiece();
            return IsKingVKing(counting) || IsKingBishopVKing(counting) || IsKingBishopVKingBishop(counting)
                || IsKingKnightVKing(counting);
        }

        private static bool IsKingVKing(Counting counting)
        {
            return counting.TotalCount == 2;
        }

        private static bool IsKingBishopVKing(Counting counting)
        {
            return counting.TotalCount == 3 && (counting.White(PieaceType.Bishop) == 1 
                || counting.Black(PieaceType.Bishop) == 1);
        }

        private static bool IsKingKnightVKing(Counting counting)
        {
            return counting.TotalCount == 3 && (counting.White(PieaceType.Knight) == 1
                || counting.Black(PieaceType.Knight) == 1);
        }

        private bool IsKingBishopVKingBishop(Counting counting)
        {
            if (counting.TotalCount != 4) 
            { 
                return false;
            }

            if(counting.White(PieaceType.Bishop) != 1 || counting.Black(PieaceType.Bishop) != 1)
            {
                return false;
            }
            Position whiteBishopPosition = FindPiece(Player.White, PieaceType.Bishop);
            Position blackBishopPosition = FindPiece(Player.Black, PieaceType.Bishop);
            return whiteBishopPosition.SquareColor() == blackBishopPosition.SquareColor();
        }

        private Position FindPiece(Player color, PieaceType type )
        {
            return PiecePositionsFor(color).First(position => this[position].Type == type);
        }
    }
}
