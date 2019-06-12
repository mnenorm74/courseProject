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


namespace registration
{
    public partial class Alphabet : Page
    {
        public Alphabet()
        {
            InitializeComponent();
            AlphabetGrid.Focus();
        }

        private void letter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var name = ((Image)sender).Name;
            Audio.PlaySound(name);
        }

        private void toMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void toAlphabetGameRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AlphabetGameRules());
        }

        private void AlphabetGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                NavigationService.Navigate(new AlphabetGameRules());
            }
        }
    }
}
