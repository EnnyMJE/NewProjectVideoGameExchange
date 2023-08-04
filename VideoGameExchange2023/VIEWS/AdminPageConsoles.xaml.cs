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
    /// Interaction logic for AdminPageConsoles.xaml
    /// </summary>
    public partial class AdminPageConsoles : Page
    {
        public AdminPageConsoles()
        {
            InitializeComponent();
            RefreshConsoleList();
        }

        private void RefreshConsoleList()
        {
            var consoles = Console.GetLConsoles();
            LB_adminConsoles.ItemsSource = consoles;
        }


        private void btn_addNewConsole_Click(object sender, RoutedEventArgs e)
        {
            BtnAddNewConsole.Visibility = Visibility.Collapsed;
            Grid_newConsole.Visibility = Visibility.Visible;
        }
        

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Console con = new Console(tb_consoleName.Text);
            bool added = con.AddNewConsole();
            if (added)
            {
                Grid_newConsole.Visibility = Visibility.Collapsed;
                BtnAddNewConsole.Visibility = Visibility.Visible;
                MessageBox.Show("New console successfully added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                tb_consoleName.Text = "";
                RefreshConsoleList();
            }
            else
            {
                MessageBox.Show("Error: Console name might existed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tb_consoleName.Text = "";
            }

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            BtnAddNewConsole.Visibility = Visibility.Visible;
            Grid_newConsole.Visibility = Visibility.Collapsed;
            tb_consoleName.Text = "";

        }

        private void IsConsoleSelected(object sender, SelectionChangedEventArgs e)
        {
            btn_deleteConsole.Visibility = Visibility.Visible;
        }

        private void btn_deleteConsole_Click(object sender, RoutedEventArgs e)
        {
            if (LB_adminConsoles.SelectedItem is Console selectedConsole)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this console?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        selectedConsole.DeleteConsole();
                        RefreshConsoleList();
                        btn_deleteConsole.Visibility = Visibility.Collapsed;
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
