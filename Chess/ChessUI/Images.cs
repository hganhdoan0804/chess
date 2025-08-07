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
            { PieaceType.Pawn, LoadImage("Assets/PawnW.png") },
            { PieaceType.Knight, LoadImage("Assets/KnightW.png") },
            { PieaceType.Bishop, LoadImage("Assets/BishopW.png") },
            { PieaceType.Rook, LoadImage("Assets/RookW.png") },
            { PieaceType.Queen, LoadImage("Assets/QueenW.png") },
            { PieaceType.King, LoadImage("Assets/KingW.png") }
        };

        private static readonly Dictionary<PieaceType, ImageSource> blackSources = new()
        {
            { PieaceType.Pawn, LoadImage("Assets/PawnB.png") },
            { PieaceType.Knight, LoadImage("Assets/KnightB.png") },
            { PieaceType.Bishop, LoadImage("Assets/BishopB.png") },
            { PieaceType.Rook, LoadImage("Assets/RookB.png") },
            { PieaceType.Queen, LoadImage("Assets/QueenB.png") },
            { PieaceType.King, LoadImage("Assets/KingB.png") }
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
