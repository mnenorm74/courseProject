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
    public partial class Figures : Page
    {
        public Figures()
        {
            InitializeComponent();
            FiguresGrid.Focus();
        }
       
        private void Figure_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var name = ((Image)sender).Name;
            Audio.PlaySound(name);
        }

        private void toMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void toFiguresGameRules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new FigureGameRules());
        }

        private void FiguresGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                NavigationService.Navigate(new FigureGameRules());
            }
        }
    }
}
