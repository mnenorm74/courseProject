﻿using System;
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
    public partial class CongratulationOneStar : Page
    {

        public CongratulationOneStar()
        {
            InitializeComponent();
        }

        private void CongratuletionGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                NavigationService.Navigate(new Menu());
            }    
        }

        private void ToMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void CongratuletionGrid_Loaded(object sender, RoutedEventArgs e)
        {
            CongratuletionGrid.Focus();
        }
    }
}
