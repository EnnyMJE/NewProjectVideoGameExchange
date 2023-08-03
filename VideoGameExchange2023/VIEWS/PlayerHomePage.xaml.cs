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
    }
}
