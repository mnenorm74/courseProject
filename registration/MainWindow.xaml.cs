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
using MySql.Data.MySqlClient;
using System.Threading;
using registration.Properties;

namespace registration
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        { 
            Settings.Default["IsRegisteredUser"] = false;
            Settings.Default.Save();
            InitializeComponent();
            frame.Navigate(new Start());  
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Ты уже уходишь?", "Выход", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            } 
        }
    }


}
