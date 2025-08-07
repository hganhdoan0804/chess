using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8,8];
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

        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;
        }

        public static bool IsInside(Position position)
        {
            return position.Row >= 0 && position.Row < 8 && position.Column >= 0 && position.Column < 8;
        }

        public bool IsEmty(Position position)
        {
            return this[position] == null;
        }
    }
}
