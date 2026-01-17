using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class StateString
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();
        public StateString(Player currentPlayer, Board board)
        {
            AddPiecePlacement(board);
            stringBuilder.Append(' ');
            AddCurrentPlayer(currentPlayer);
            stringBuilder.Append(' ');
            AddCastlingRights(board);
            stringBuilder.Append(' ');
            AddEnPassent(board, currentPlayer);
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        private static char PieceChar(Piece piece)
        {
            char c = piece.Type switch
            {
                PieceType.Pawn => 'p',
                PieceType.Knight => 'n',
                PieceType.Bishop => 'b',
                PieceType.Rook => 'r',
                PieceType.Queen => 'q',
                PieceType.King => 'k',
                _ => ' '
            };

            if (piece.Color == Player.White)
            {
                return char.ToUpper(c);
            }
            return c;
        }

        private void AddRowData(Board board, int row)
        {
            int emptySquare = 0;
            for (int i = 0; i < row; i++)
            {
                if (board[row, i] == null)
                {
                    emptySquare++;
                    continue;
                }

                if (emptySquare > 0)
                {
                    stringBuilder.Append(emptySquare);
                    emptySquare = 0;
                }

                stringBuilder.Append(PieceChar(board[row, i]));
            }

            if (emptySquare > 0)
            {
                stringBuilder.Append(emptySquare);
            }
        }

        private void AddPiecePlacement(Board board)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append('/');
                }
                AddRowData(board, i);
            }
        }

        private void AddCurrentPlayer(Player currentPlayer)
        {
            if (currentPlayer == Player.White)
            {
                stringBuilder.Append('w');
            }
            else
            {
                stringBuilder.Append('b');
            }
        }

        private void AddCastlingRights(Board board)
        {
            bool castleWhiteKingSide = board.CastleRightKingSide(Player.White);
            bool castleWhiteQueenSide = board.CastleRightQueenSide(Player.White);
            bool castleBlackKingSide = board.CastleRightKingSide(Player.Black);
            bool castleBlackQueenSide = board.CastleRightQueenSide(Player.Black);

            if (!(castleBlackKingSide || castleBlackQueenSide || castleWhiteKingSide || castleWhiteQueenSide))
            {
                stringBuilder.Append('-');
            }
            if (castleWhiteKingSide)
            {
                stringBuilder.Append('K');
            }
            if (castleWhiteQueenSide)
            {
                stringBuilder.Append('Q');
            }
            if (castleBlackKingSide)
            {
                stringBuilder.Append('k');
            }
            if (castleBlackQueenSide)
            {
                stringBuilder.Append('q');
            }
        }

        private void AddEnPassent(Board board, Player currentPlayer)
        {
            if (!board.CanCaptureEnPassant(currentPlayer))
            {
                return;
            }

            Position position = board.GetPawnSkipPosition(currentPlayer.Opposite());
            char file = (char)('a' + position.Column);
            int rank = 8 - position.Row;
            stringBuilder.Append(file);
            stringBuilder.Append(rank);
        }
    }
}
