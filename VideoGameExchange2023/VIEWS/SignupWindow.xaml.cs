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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VideoGameExchange2023.POCO;

namespace VideoGameExchange2023
{
    /// <summary>
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        public SignupWindow()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            incorrectinput.Content = "";
            if (string.IsNullOrWhiteSpace(tb_username.Text) ||
                string.IsNullOrWhiteSpace(tb_pseudo.Text) ||
                string.IsNullOrWhiteSpace(tb_password.Password) ||
                dp_birthdate.SelectedDate == null)
            {
                incorrectinput.Content = "PLEASE FILL IN ALL THE FIELDS";
                return; // Stop further execution
            }

            DateTime birthdate = dp_birthdate.SelectedDate.Value;
            int age = DateTime.Today.Year - birthdate.Year;
            if (birthdate > DateTime.Today.AddYears(-age)) // Adjust age if birthdate hasn't occurred yet this year
                age--;
            if (age < 12)
            {
                incorrectinput.Content = "PLAYER MUST BE AT LEAST 12 YEARS OLD";
                return; // Stop further execution
            }
            if (tb_username.Text.Length < 2)
            {
                incorrectinput.Content = "NAME MUST HAVE AT LEAST 3 LETTERS";
                return; // Stop further execution
            }
            if (tb_password.Password.Length < 4 && tb_password.Password.Length > 6)
            {
                incorrectinput.Content = "PASSWORD MUST HAVE AT LEAST 4 LETTERS AND MAX 6 LETTERS";
                return; // Stop further execution
            }
            if (tb_pseudo.Text.Length < 2)
            {
                incorrectinput.Content = "PSEUDO MUST HAVE AT LEAST 2 LETTERS";
                return; // Stop further execution
            }

            incorrectinput.Content = "";

            Admin ad = new Admin(tb_username.Text, tb_password.Password);
            Admin adm = (Admin)ad.Login();
            if (adm == null)
            {
                Player pl = new Player(tb_username.Text, tb_password.Password);
                Player ply = (Player)pl.Login();
                DateTime regdate = DateTime.Today;
                if (ply == null)
                {
                    DateTime bd = dp_birthdate.SelectedDate.Value;
                    Player player = new Player(tb_username.Text, tb_password.Password, tb_pseudo.Text, bd, regdate );
                    bool exist = player.PseudoExisted();
                    if (!exist)
                    {
                        int givenYear = player.YearGivenGift();
                        bool added = player.AddNewPlayer(givenYear);
                        if (added)
                        {
                            if (player.IsBirthday())
                            {
                                lbl_birthdaywish.Content = "Happy birthday, please sign in to get your birthday gift";
                            }
                            ShowWelcomeMessage();
                        }
                    }
                    else
                    {
                        incorrectinput.Content = "Please choose another Pseudo";
                    }
                }
                else
                {
                    incorrectinput.Content = "THIS USER NAME AND PASSWORD ALREADY EXIST";
                }
            }
            else
            {
                incorrectinput.Content = "THIS USER NAME AND PASSWORD ALREADY EXIST";
            }
        }

        private void ShowWelcomeMessage()
        {
            // Fade out the signup form grid
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            fadeOutAnimation.Completed += (s, e) =>
            {
                // Hide the signup form grid after fading out
                SignupFormGrid.Visibility = Visibility.Collapsed;

                // Show the welcome message grid
                WelcomeMessageGrid.Visibility = Visibility.Visible;
            };
            SignupFormGrid.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void BtnGoToSignIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.Visibility = Visibility.Visible;
                mainWindow.Activate();
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.Visibility = Visibility.Visible;
                mainWindow.Activate();
            }
            this.Close();
        }
    }
}
