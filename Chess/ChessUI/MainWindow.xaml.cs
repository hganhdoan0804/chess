using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLogic;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();
        private GameState gameState;
        private Position selectedPosition = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Image image = new Image();
                    pieceImages[row, col] = image;
                    PieceGrid.Children.Add(image);
                    Rectangle highlight = new Rectangle();
                    highlights[row, col] = highlight;
                    HightlightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece piece = board[row, col];
                    pieceImages[row, col].Source = Images.GetImageSource(piece);
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMenuOnScreen())
            {
                return;
            }
            Point point = e.GetPosition(BoardGrid);
            Position position = ToSquarePosition(point);
            if (selectedPosition == null)
            {
                OnFromPosition(position);
            }
            else
            {
                OnToPosition(position);
            }
        }

        private void OnFromPosition(Position position)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPiece(position);
            if (moves.Any())
            {
                selectedPosition = position;
                CacheMove(moves);
                ShowHighlights();
            }
        }

        private void OnToPosition(Position position)
        {
            selectedPosition = null;
            HideHighlights();
            if(moveCache.TryGetValue(position, out Move move))
            {
                if(move.Type == MoveType.Promotion)
                {
                    HandlePromotion(move.FromPosition, move.ToPosition);
                }
                else
                {
                    HandleMove(move);
                }
                HandleMove(move);
            }
        }

        private void HandlePromotion(Position fromPosition, Position toPosition)
        {
            pieceImages[toPosition.Row, toPosition.Column].Source = Images.GetImageSource(gameState.CurrentPlayer, PieaceType.Pawn);
            pieceImages[fromPosition.Row, fromPosition.Column].Source = null;
            PromotionMenu promotionMenu = new PromotionMenu(gameState.CurrentPlayer);
            MenuContainer.Content = promotionMenu;
            promotionMenu.PieaceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promotionMove = new PawnPromotion(fromPosition, toPosition, type);
                HandleMove(promotionMove);
            };
        }

        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
            if (gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }

        private void CacheMove(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (var move in moves)
            {
                moveCache[move.ToPosition] = move;
            }
        }

        private void ShowHighlights()
        {
            Color hightLightColor = Color.FromArgb(150, 151, 255, 255);
            foreach(Position toPosition in moveCache.Keys)
            {
                highlights[toPosition.Row, toPosition.Column].Fill = new SolidColorBrush(hightLightColor);
            }
        }

        private void HideHighlights()
        {
            foreach (Position toPosition in moveCache.Keys)
            {
                highlights[toPosition.Row, toPosition.Column].Fill = Brushes.Transparent;
            }
        }

        private void SetCursor(Player player)
        {
            if(player == Player.White)
            {
                Cursor = ChessCursors.WhiteCursor;
            }
            else
            {
                Cursor = ChessCursors.BlackCursor;
            }
        }

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        private void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;
            gameOverMenu.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }

        private void ShowPauseMenu()
        {
            PauseMenu pauseMenu = new PauseMenu();
            MenuContainer.Content = pauseMenu;
            pauseMenu.OptionSelected += option =>
            {
                MenuContainer.Content = null;
                if (option == Option.Restart)
                {
                    RestartGame();
                }
            };
        }

        private void RestartGame()
        {
            selectedPosition = null;
            HideHighlights();
            moveCache.Clear();
            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(!IsMenuOnScreen() && e.Key == Key.Space)
            {
                ShowPauseMenu();
            }
        }
    }
}