using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for PromotionMenu.xaml
    /// </summary>
    public partial class PromotionMenu : UserControl
    {
        public event Action<PieceType> PieaceSelected;
        public PromotionMenu(Player player)
        {
            InitializeComponent();
            QueenImg.Source = Images.GetImageSource(player, PieceType.Queen);
            BishopImg.Source = Images.GetImageSource(player, PieceType.Bishop);
            KnightImg.Source = Images.GetImageSource(player, PieceType.Knight);
            RookImg.Source = Images.GetImageSource(player, PieceType.Rook);
        }

        private void KnightImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieaceSelected?.Invoke(PieceType.Knight);
        }

        private void BishopImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieaceSelected?.Invoke(PieceType.Bishop);
        }

        private void QueenImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieaceSelected?.Invoke(PieceType.Queen);
        }

        private void RookImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieaceSelected?.Invoke(PieceType.Rook);
        }
    }
}
