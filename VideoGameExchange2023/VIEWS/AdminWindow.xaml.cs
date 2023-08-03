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
using VideoGameExchange2023.POCO;
using VideoGameExchange2023.VIEWS;

namespace VideoGameExchange2023
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Admin currentAdmin;
        public AdminWindow(Admin admin)
        {
            InitializeComponent();
            currentAdmin = admin;
            if (currentAdmin != null)
            {
                Lbl_welcomeAdm.Content = $"WELCOME BACK {admin.Username}!";
            }
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

        private void VideoGamesAdmin_click(object sender, RoutedEventArgs e)
        {
            Lbl_welcomeAdm.Visibility= Visibility.Hidden;
            AdminFrame.Content = new PageAdminGames();
        }

        private void ConsolesAdmin_click(object sender, RoutedEventArgs e)
        {
            Lbl_welcomeAdm.Visibility = Visibility.Hidden;
            AdminFrame.Content = new AdminPageConsoles();
        }
    }
}
