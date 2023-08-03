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
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using VideoGameExchange2023.POCO;


namespace VideoGameExchange2023
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        private Player currentPlayer;

        public PlayerWindow(Player player)
        {
            InitializeComponent();
            lblWelcomeMessage.Content = $"Welcome, {player.Username} ({player.Pseudo})! You have {player.Credit} credit(s).";
            currentPlayer = player;
            /*if (currentPlayer != null)
            {
                PlayerFrame.Content= new PlayerHomePage(currentPlayer);
            }*/
        }

        private void BtnSignout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.Visibility = Visibility.Visible;
                mainWindow.Activate();
            }
            this.Close();
        }

        private void Signout_click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.Visibility = Visibility.Visible;
                mainWindow.Activate();
            }
            this.Close();
        }

        private void Home_player_click(object sender, RoutedEventArgs e)
        {
            PlayerFrame.Content = new PlayerHomePage(currentPlayer);
            lblWelcomeMessage.Visibility = Visibility.Collapsed;
        }

        private void Games_player_click(object sender, RoutedEventArgs e)
        {
            lblWelcomeMessage.Visibility = Visibility.Collapsed;
            PlayerFrame.Content = new PlayerGamesPage(currentPlayer);
        }
    }
}
