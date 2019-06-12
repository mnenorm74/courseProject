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
using System.Windows.Media.Animation;
using System.Media;

namespace registration
{
    public partial class Captions : Page
    {
        SoundPlayer player;

        public Captions()
        {
            InitializeComponent();
            player = new SoundPlayer(Audio.GetSoundPath("Captions"));
            Parting.Visibility = Visibility.Collapsed;
            toMenu.Visibility = Visibility.Collapsed;
            OlyaPicture.Visibility = Visibility.Collapsed;
            VaryaPicture.Visibility = Visibility.Collapsed;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            mainCaption.Visibility = Visibility.Collapsed;
            Parting.Visibility = Visibility.Visible;
            toMenu.Visibility = Visibility.Visible;
            OlyaPicture.Visibility = Visibility.Visible;
            VaryaPicture.Visibility = Visibility.Visible;
        }

        private void toMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            NavigationService.Navigate(new Menu());
        }

        private void StopRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
        }

        private void PlayRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Play();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            player.Play();
        }
    }
}
