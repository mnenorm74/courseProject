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
using registration.Properties;
using System.Media;

namespace registration
{
    public partial class Greeting : Page
    {
        SoundPlayer player;
        public Greeting()
        {
            InitializeComponent();
            player = new SoundPlayer(Audio.GetSoundPath("Greet"));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            mainGrid.Focus();
        }

        private void toAuthorization_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            NavigationService.Navigate(new Authorization());
        }

        private void mainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            player.Stop();
            NavigationService.Navigate(new Authorization());
        }

        private void PlayRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Play();
        }

        private void StopRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
        }
    }
}
