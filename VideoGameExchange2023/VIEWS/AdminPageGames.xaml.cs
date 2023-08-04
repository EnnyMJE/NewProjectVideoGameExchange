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
using Console = VideoGameExchange2023.POCO.Console;

namespace VideoGameExchange2023.VIEWS
{
    /// <summary>
    /// Interaction logic for PageAdminGames.xaml
    /// </summary>
    public partial class PageAdminGames : Page
    {
        public PageAdminGames()
        {
            InitializeComponent();
            RefreshGameList();
            var consoles = Console.GetLConsoles();
            LB_adminConsoles.ItemsSource = consoles;

        }

        private void RefreshGameList()
        {
            var games = VideoGame.GetLGames();
            LB_adminGames.ItemsSource = games;
        }

        private void LB_adminGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Btn_deleteGame.Visibility = Visibility.Visible;
            Btn_updatecost.Visibility = Visibility.Visible;
        }

        private void Btn_updatecost_Click(object sender, RoutedEventArgs e)
        {
            Btn_updatecost.Visibility=Visibility.Hidden;
            Btn_deleteGame.Visibility = Visibility.Hidden;
            Tb_newCost.Visibility = Visibility.Visible;
            Btn_submitNewCost.Visibility = Visibility.Visible;
            Btn_cancelNewCost.Visibility = Visibility.Visible;

        }

        private void Btn_cancelnewcost_Click(object sender, RoutedEventArgs e)
        {
            Btn_updatecost.Visibility = Visibility.Visible;
            Btn_deleteGame.Visibility = Visibility.Visible;
            Tb_newCost.Visibility = Visibility.Collapsed;
            Tb_newCost.Text = "";
            Btn_submitNewCost.Visibility = Visibility.Collapsed;
            Btn_cancelNewCost.Visibility = Visibility.Collapsed;
        }

        private void Btn_submitnewcost_Click(object sender, RoutedEventArgs e)
        {
            if (LB_adminGames.SelectedItem is VideoGame selectedGame)
            {
                VideoGame videoGame = selectedGame;
                string stringCost = Tb_newCost.Text;
                if (string.IsNullOrEmpty(stringCost))
                {
                    MessageBox.Show("Please enter a valid integer for the new cost.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (int.TryParse(stringCost, out int intCost))
                {
                    if(intCost>0 && intCost <= 5)
                    {
                        bool updated = videoGame.UpdateCost(intCost);
                        if (updated)
                        {
                            MessageBox.Show("Game's cost successfully updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            RefreshGameList();
                            Tb_newCost.Visibility = Visibility.Collapsed;
                            Tb_newCost.Text = "";
                            Btn_cancelNewCost.Visibility = Visibility.Collapsed;
                            Btn_submitNewCost.Visibility = Visibility.Collapsed;
                            Btn_deleteGame.Visibility = Visibility.Collapsed;
                            Btn_updatecost.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            MessageBox.Show("Error: Check the input.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: The cost must be between 1-5 credit", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error: The cost must be number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Btn_addGame_Click(object sender, RoutedEventArgs e)
        {
            Grid_addGame.Visibility = Visibility.Visible;
            Btn_addGame.Visibility = Visibility.Collapsed;
        }

        private void Btn_cancelNewGame_Click(object sender, RoutedEventArgs e)
        {
            Grid_addGame.Visibility= Visibility.Collapsed;
            Btn_addGame.Visibility = Visibility.Visible;
            Tb_newGameCost.Text = "";
            Tb_newGameName.Text = "";
        }

        private void Btn_submitNewGame_Click(object sender, RoutedEventArgs e)
        {
            Console selectedConsole = (Console)LB_adminConsoles.SelectedItem;
            if (string.IsNullOrEmpty(Tb_newGameName.Text) || string.IsNullOrEmpty(Tb_newGameCost.Text) || selectedConsole == null)
            {
                MessageBox.Show("Please choose a console and fill in all the fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(Tb_newGameCost.Text, out int creditCost))
            {
                MessageBox.Show("Invalid credit cost. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }else if (creditCost <= 0 || creditCost > 5)
            {
                MessageBox.Show("Error: The cost must be between 1-5 credit", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return ;
            }

            VideoGame newGame = new VideoGame(Tb_newGameName.Text, creditCost, selectedConsole.ConsoleName);
            bool successfullyAdded = newGame.AddNewGame();
            if (successfullyAdded)
            {
                Grid_addGame.Visibility=Visibility.Collapsed;
                Tb_newGameName.Text = "";
                Tb_newGameCost.Text = "";
                Btn_addGame.Visibility = Visibility.Visible;
                MessageBox.Show("New console successfully added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshGameList();
            }
            else
            {
                MessageBox.Show("Error: This game existed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Tb_newGameName.Text = "";
                Tb_newGameCost.Text = "";
            }
        }

        private void Btn_deleteGame_Click(object sender, RoutedEventArgs e)
        {
            if (LB_adminGames.SelectedItem is VideoGame selectedGame)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this video game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        selectedGame.DeleteGame();
                        RefreshGameList();
                        Btn_deleteGame.Visibility = Visibility.Collapsed;
                        Btn_updatecost.Visibility = Visibility.Collapsed;
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
