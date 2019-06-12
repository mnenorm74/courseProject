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
using registration.Properties;

namespace registration
{
    public partial class EntryWithoutLogin : Page
    {
        SoundPlayer player;

        public EntryWithoutLogin()
        {
            InitializeComponent();
            EntryWithoutLoginGrid.Focus();
            player = new SoundPlayer(Audio.GetSoundPath("WithoutLogin"));
        }

        private void PlayRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Play();
        }

        private void StopRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
        }

        private void toRegistration_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            NavigationService.Navigate(new Registration());
        }

        private void toMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            ToMenu();
        }

        private void EntryWithoutLoginGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                ToMenu();
            }
        }

        private void ToMenu()
        {
            Settings.Default["IsRegisteredUser"] = false;
            Settings.Default.Save();
            NavigationService.Navigate(new Menu());
        }
    }
}
