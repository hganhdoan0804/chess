using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic;

namespace ChessUI
{
    public static class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            { PieceType.Pawn, LoadImage("Assets/whitePawn.png") },
            { PieceType.Knight, LoadImage("Assets/whiteKnight.png") },
            { PieceType.Bishop, LoadImage("Assets/whiteBishop.png") },
            { PieceType.Rook, LoadImage("Assets/whiteRook.png") },
            { PieceType.Queen, LoadImage("Assets/whiteQueen.png") },
            { PieceType.King, LoadImage("Assets/whiteKing.png") }
        };

        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            { PieceType.Pawn, LoadImage("Assets/blackPawn.png") },
            { PieceType.Knight, LoadImage("Assets/blackKnight.png") },
            { PieceType.Bishop, LoadImage("Assets/blackBishop.png") },
            { PieceType.Rook, LoadImage("Assets/blackRook.png") },
            { PieceType.Queen, LoadImage("Assets/blackQueen.png") },
            { PieceType.King, LoadImage("Assets/blackKing.png") }
        };
        private static ImageSource LoadImage (string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource GetImageSource(Player player , PieceType pieaceType)
        {
            return player switch
            {
                Player.White => whiteSources[pieaceType],
                Player.Black => blackSources[pieaceType],
                _ => throw new ArgumentException("Invalid player type")
            };
        }

        public static ImageSource GetImageSource(Piece piece)
        {
            if (piece == null)
            {
                return null;
            }
            return GetImageSource(piece.Color, piece.Type);
        }
    }
}
