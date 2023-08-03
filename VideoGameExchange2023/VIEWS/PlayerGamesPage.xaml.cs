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
using VideoGameExchange2023.POCO;

namespace VideoGameExchange2023
{
    /// <summary>
    /// Interaction logic for PlayerGamesPage.xaml
    /// </summary>
    public partial class PlayerGamesPage : Page
    {
        private Player player;
        public PlayerGamesPage(Player player)
        {
            InitializeComponent();
            RefreshGameList();
            this.player = player;
        }

        private void RefreshGameList()
        {
            var games = VideoGame.GetLGames();
            LB_playerGames.ItemsSource = games;
        }

        private void LB_playerGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Btn_provideCopy.Visibility = Visibility.Visible;
            if (LB_playerGames.SelectedItem is VideoGame selectedGame)
            {
                int nbr = selectedGame.CopyAvailable().Count;
                Lbl_nbrCopy.Content = nbr;
                int nbr_av = selectedGame.nbrCopyAvailable();
                Lbl_nbrCopyAvailable.Content = nbr_av;
                if (nbr > 0)
                {
                    if (nbr_av > 0)
                    {
                        Btn_rentGame.Visibility = Visibility.Visible;
                        Btn_bookGame.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        Btn_bookGame.Visibility = Visibility.Visible;
                        Btn_rentGame.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    Btn_rentGame.Visibility = Visibility.Hidden;
                    Btn_bookGame.Visibility = Visibility.Hidden;
                }
            }
            
        }
        
        private void Btn_provideCopy_Click(object sender, RoutedEventArgs e)
        {
            VideoGame selectedGame = (VideoGame)LB_playerGames.SelectedItem;
            Copy cp = new Copy(selectedGame,player);
            bool isNewCopyAdded = cp.AddNewCopy();
            if (isNewCopyAdded)
            {
                MessageBox.Show("This copy now available to be rent by other", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Btn_provideCopy.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Error: You already put this game to rent", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn_rentGame_Click(object sender, RoutedEventArgs e)
        {
            if (LB_playerGames.SelectedItem is VideoGame selectedGame)
            {
                Copy cp = new Copy(selectedGame,player);
                bool IsOwner = cp.IsOwner();
                if (IsOwner)
                {
                    MessageBox.Show("Error: You can't rent the game you lent", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (player.Credit < selectedGame.CreditCost)
                    {
                        MessageBox.Show("Error: your credit balance is not enough", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    DateTime startTime = DateTime.Today;
                    DateTime endTime = startTime.AddDays(7);
                    cp = selectedGame.GetACopyAvailable();
                    Player owner = cp.Owner;
                    Player borower = player;
                    Loan loan = new Loan(startTime, endTime, true, cp, owner, borower);
                    cp.borrow(loan);
                    MessageBox.Show("This copy is now rented to you", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    player.UpdateCredit(selectedGame.CreditCost);
                    return;
                }
            }
        }
    }
}
