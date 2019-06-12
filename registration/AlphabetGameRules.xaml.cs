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
    public partial class AlphabetGameRules : Page
    {
        SoundPlayer player;

        public AlphabetGameRules()
        {
            InitializeComponent();
            AlpabetGameRulesGrid.Focus();
            player = new SoundPlayer(Audio.GetSoundPath("AlphabetGameRules"));
        }

        private void PlayRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Play();
        }

        private void StopRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
        }

        private void toALphabet_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            NavigationService.Navigate(new Alphabet());
        }

        private void toAlphabetGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            NavigationService.Navigate(new AlphabetGame());
        }

        private void AlpabetGameRulesGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                NavigationService.Navigate(new AlphabetGame());
            }
        }
    }
}
