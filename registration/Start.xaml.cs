using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace registration
{
    public partial class Start : Page
    {
        SoundPlayer player;

        public Start()
        {
            InitializeComponent();
            player = new SoundPlayer(Audio.GetSoundPath("Start"));
            player.Play();
        }

        private void to_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            NavigationService.Navigate(new Greeting());
        }

        private void start_KeyDown(object sender, KeyEventArgs e)
        {
            player.Stop();
            NavigationService.Navigate(new Greeting());
        }

        private void startGrid_Loaded(object sender, RoutedEventArgs e)
        {
            startGrid.Focus();
        }
    }
}
