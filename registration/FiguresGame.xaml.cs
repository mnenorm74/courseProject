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
using MySql.Data.MySqlClient;

namespace registration
{
    public partial class FiguresGame : Page
    {
        public FiguresGame()
        {
            InitializeComponent();
        }

        private void toFigures_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Figures());
        }

        private void toFigures_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Figures());
        }

        private void toMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void Element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Image)sender).Visibility = Visibility.Hidden;
            Result.Text = "";
        }

        private void check_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsAllFound())
            {
                Result.Text = "Все найдено";
                updateDataBase();
                GoToCongratulation(GameResult.CalculateStars());
            }
            else
            {
                Result.Text = "Еще не все фигуры найдены";
            }
        }

        private bool IsFound(Image sender)
        {
            return sender.Visibility == Visibility.Hidden;
        }

        private bool IsAllFound()
        {
            return IsFound(Circle) && IsFound(Ellips) && IsFound(Pentagon) && 
                IsFound(Rectangle) && IsFound(Rhombus) && IsFound(Square) && IsFound(Trapezium) && IsFound(Triangle);
        }

        private void updateDataBase()
        {
            if (!(bool)Settings.Default["IsRegisteredUser"])
            {
                return;
            }
            var userLogin = Settings.Default["SavedLogin"].ToString();
            var connectionData = "server=localhost;user=root;database=users;password=admin12345;";
            var updatingData = $"update usersdata SET figuresCountSuccesses=figuresCountSuccesses+1 where login ='{userLogin}'";
            var connection = new MySqlConnection(connectionData);
            var update = new MySqlCommand(updatingData, connection);
            connection.Open();
            update.Prepare();
            update.ExecuteNonQuery();
        }

        private void GoToCongratulation(int starsCount)
        {
            if (starsCount == 0)
            {
                NavigationService.Navigate(new CongratulationWithoutStars());
            }
            if (starsCount == 1)
            {
                NavigationService.Navigate(new CongratulationOneStar());
            }
            if (starsCount == 2)
            {
                NavigationService.Navigate(new CongratulationTwoStars());
            }
            if (starsCount == 3)
            {
                NavigationService.Navigate(new CongratulationThreeStars());
            }
        }
    }
}
