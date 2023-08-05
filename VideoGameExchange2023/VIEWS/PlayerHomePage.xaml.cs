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
    /// Interaction logic for PlayerHomePage.xaml
    /// </summary>
    public partial class PlayerHomePage : Page
    {
        private Player player;
        public PlayerHomePage(Player ply)
        {
            InitializeComponent();
            player = ply;
            if (player != null)
            {
                RefreshCopyList();
                RefreshLoanList();
                RefreshOldLoanList();
                Lbl_pseudo.Content = player.Pseudo;
                Lbl_creditBalance.Content = player.Credit;
                if (player.IsBirthday())
                {
                    lbl_happybday.Content = "Happy birthday";
                }
                else lbl_happybday.Content = "";
                if (player.BirthdayGiftGiven())
                {
                    lbl_bdaygift.Content = "2 credits just added for your (last) birthday gift";
                    player.BirthdayGiftGivenYear = DateTime.Today.Year;
                }
            }
        }

        private void RefreshCopyList()
        {
            var copies = Copy.GetLCopiesByPlayer(player);
            Lb_copyOwn.ItemsSource = copies;
        }

        private void RefreshLoanList()
        {
            var loans = Loan.GetLLoansByBorrower(player);
            foreach (var loan in loans)
            {
                loan.StartTime = loan.StartTime.Date; 
                loan.EndTime = loan.EndTime.Date;     
            }

            Lb_loanBorrower.ItemsSource = loans;
        }

        private void RefreshOldLoanList()
        {
            var loans = Loan.GetLOldLoansByBorrower(player);
            foreach (var loan in loans)
            {
                loan.StartTime = loan.StartTime.Date;
                loan.EndTime = loan.EndTime.Date;
            }

            Lb_loanBorrower_previous.ItemsSource = loans;
        }

        private void Btn_DeleteCopy_Click(object sender, RoutedEventArgs e)
        {
            if(Lb_copyOwn.SelectedItem is Copy selectedCopy)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this copy?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        selectedCopy.DeleteCopy();
                        RefreshCopyList();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Btn_Return_Click(object sender, RoutedEventArgs e)
        {
            if (Lb_loanBorrower.SelectedItem is Loan selectedLoan)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to return this game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        selectedLoan.Copy.ReleaseCopy();
                        RefreshLoanList();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Btn_DeleteOldLoan_Click(object sender, RoutedEventArgs e)
        {
            if (Lb_loanBorrower_previous.SelectedItem is Loan selectedLoan)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        selectedLoan.DeleteLoan();
                        RefreshOldLoanList();
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
