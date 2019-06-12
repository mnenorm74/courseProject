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
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }
        
        private void Alphabet_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Alphabet());
        }

        private void Figures_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Figures());
        }

        private void Numbers_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Numbers());
        }

        private void Title_MouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = Brushes.Black;
        }

        private void Title_MouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = Brushes.Blue;
        }



        private void Alphabet_MouseEnter(object sender, MouseEventArgs e)
        {
            Alphabet.Source = BitmapFrame.Create(new Uri(@"c:\users\оля\documents\visual studio 2015\Projects\registration\registration\Resources\AlphabetPicture2.png"));
        }


        private void Alphabet_MouseLeave(object sender, MouseEventArgs e)
        {
            Alphabet.Source = BitmapFrame.Create(new Uri(@"c:\users\оля\documents\visual studio 2015\Projects\registration\registration\Resources\AlphabetPicture.png"));
        }

        private void Figures_MouseEnter(object sender, MouseEventArgs e)
        {
            Figures.Source = BitmapFrame.Create(new Uri(@"c:\users\оля\documents\visual studio 2015\Projects\registration\registration\Resources\FiguresPicture2.png"));
        }

        private void Figures_MouseLeave(object sender, MouseEventArgs e)
        {
            Figures.Source = BitmapFrame.Create(new Uri(@"c:\users\оля\documents\visual studio 2015\Projects\registration\registration\Resources\FiguresPicture.png"));
        }

        private void Numbers_MouseEnter(object sender, MouseEventArgs e)
        {
            Numbers.Source = BitmapFrame.Create(new Uri(@"c:\users\оля\documents\visual studio 2015\Projects\registration\registration\Resources\NumbersPicture2.png"));
        }

        private void Numbers_MouseLeave(object sender, MouseEventArgs e)
        {
            Numbers.Source = BitmapFrame.Create(new Uri(@"c:\users\оля\documents\visual studio 2015\Projects\registration\registration\Resources\NumbersPicture.png"));
        }

        private void toAuthorization_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Authorization());
        }
    }
}
