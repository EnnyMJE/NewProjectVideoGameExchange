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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            incorectinput.Content = "";
            if (string.IsNullOrWhiteSpace(tb_username.Text) || string.IsNullOrWhiteSpace(tb_password.Password))
            {
                incorectinput.Content = "Username and password are required";
                return;
            }

            Admin ad = new Admin(tb_username.Text, tb_password.Password);
            Admin adm = (Admin)ad.Login();

            if (adm != null) 
            {
                AdminWindow adminWindow = new AdminWindow(adm);
                this.Visibility = Visibility.Hidden;
                adminWindow.Show();
            }
            else
            {
                Player pl = new Player(tb_username.Text, tb_password.Password);
                Player ply = (Player)pl.Login();
                if (ply != null)
                {
                    PlayerWindow playerWindow = new PlayerWindow(ply);
                    this.Visibility=Visibility.Hidden;
                    playerWindow.Show();
                }

                else
                {
                    incorectinput.Content = "Incorrect username or password ";
                }
            }
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            SignupWindow signupWindow = new SignupWindow();
            this.Visibility =Visibility.Hidden;
            signupWindow.Show();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
