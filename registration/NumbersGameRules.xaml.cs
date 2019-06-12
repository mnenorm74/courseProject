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
using System.Media;


namespace registration
{
    public partial class NumbersGameRules : Page
    {
        SoundPlayer player;

        public NumbersGameRules()
        {
            InitializeComponent();
            NumbersGameRulesGrid.Focus();
            player = new SoundPlayer(Audio.GetSoundPath("NumbersGameRules"));
        }


        private void PlayRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Play();
        }

        private void StopRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
        }

        private void toNumbersGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            NavigationService.Navigate(new NumbersGame());
        }

        private void toNumbers_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            NavigationService.Navigate(new Numbers());
        }

        private void NumbersGameRulesGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                NavigationService.Navigate(new NumbersGame());
            }
        }
    }
}
