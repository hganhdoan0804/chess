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
        private static readonly Dictionary<PieaceType, ImageSource> whiteSources = new()
        {
            { PieaceType.Pawn, LoadImage("Assets/whitePawn.png") },
            { PieaceType.Knight, LoadImage("Assets/whiteKnight.png") },
            { PieaceType.Bishop, LoadImage("Assets/whiteBishop.png") },
            { PieaceType.Rook, LoadImage("Assets/whiteRook.png") },
            { PieaceType.Queen, LoadImage("Assets/whiteQueen.png") },
            { PieaceType.King, LoadImage("Assets/whiteKing.png") }
        };

        private static readonly Dictionary<PieaceType, ImageSource> blackSources = new()
        {
            { PieaceType.Pawn, LoadImage("Assets/blackPawn.png") },
            { PieaceType.Knight, LoadImage("Assets/blackKnight.png") },
            { PieaceType.Bishop, LoadImage("Assets/blackBishop.png") },
            { PieaceType.Rook, LoadImage("Assets/blackRook.png") },
            { PieaceType.Queen, LoadImage("Assets/blackQueen.png") },
            { PieaceType.King, LoadImage("Assets/blackKing.png") }
        };
        private static ImageSource LoadImage (string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource GetImageSource(Player player , PieaceType pieaceType)
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
