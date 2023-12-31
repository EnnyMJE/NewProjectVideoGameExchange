﻿using System;
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
                Lbl_nbrCopy.Content = $"Number of copy : {nbr}";
                int nbr_av = selectedGame.nbrCopyAvailable();
                Lbl_nbrCopyAvailable.Content = $"Copy availlable : {nbr_av}";
                if (player.Credit <= 0)
                {
                    Btn_provideCopy.Visibility = Visibility.Visible;
                    Btn_rentGame.Visibility = Visibility.Hidden;
                    Btn_bookGame.Visibility = Visibility.Hidden;
                }
                else if (nbr > 0)
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
                MessageBox.Show("Error: You own this game", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn_rentGame_Click(object sender, RoutedEventArgs e)
        {
            if (LB_playerGames.SelectedItem is VideoGame selectedGame)
            {
                bool alreadyRented = selectedGame.AlreadyRented(player);
                if (alreadyRented)
                {
                    MessageBox.Show("Error: You have already rented this game.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Copy cp = new Copy(selectedGame,player);
                bool IsOwner = cp.IsOwner();
                if (IsOwner)
                {
                    MessageBox.Show("Error: You can't rent the game you own", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    
                    return;
                }
            }
        }

        private void Btn_bookGame_Click(object sender, RoutedEventArgs e)
        {
            if (LB_playerGames.SelectedItem is VideoGame selectedGame)
            {
                bool alreadyRented = selectedGame.AlreadyRented(player);
                if (alreadyRented)
                {
                    MessageBox.Show("Error: You can't book a game you're curently renting.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                bool alreadyBooked = Booking.PlayerHasBookedGame(player, selectedGame);
                if (alreadyBooked)
                {
                    MessageBox.Show("Error: You have already booked this game.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                Copy cp = new Copy(selectedGame, player);
                bool IsOwner = cp.IsOwner();
                if (IsOwner)
                {
                    MessageBox.Show("Error: You can't book the game you own", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (player.Credit < selectedGame.CreditCost)
                    {
                        MessageBox.Show("Error: your credit balance is not enough for this game", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    DateTime bookdate = DateTime.Today;
                    Booking book = new Booking(bookdate, player, selectedGame);
                    bool isBooked = book.AddNewBooking();
                    if (isBooked)
                    {
                        MessageBox.Show("This game is booked", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    }
}
