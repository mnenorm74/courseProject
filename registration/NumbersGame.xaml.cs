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
using System.Text.RegularExpressions;
using System.Media;

namespace registration
{
    public partial class NumbersGame : Page
    {
        Regex letters = new Regex(@"[0-9]");
        SoundPlayer player;

        void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match matchFirst = letters.Match(e.Text);
            if ((sender as TextBox).Text.Length > 1 || !matchFirst.Success)
            {
                e.Handled = true;
            }
        }

        public NumbersGame()
        {
            InitializeComponent();
            SheepCount.Focus();

            foreach (var element in NumbersGameGrid.Children)
            {
                if (element.GetType() == typeof(TextBox))
                {
                    ((TextBox)element).PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
                }
            }
        }
        
        private void GoToNextBox(TextBox nextBox, string key, TextBox sender)
        {
            if (key == "Return")
            {
                checkResult();
                return;
            }
            if (key == "Space")
            {
                sender.Text = sender.Text.Trim();
                if (nextBox != null)
                {
                    nextBox.Focus();
                }
                else
                {
                    checkResult();
                }
                return;
            }
            if (sender.Text != "" || key == "Right" || key == "Down")
            {
                if (nextBox != null)
                {
                    nextBox.Focus();
                }
                else
                {
                    repaintBox(BallCount, "3");
                    checkResult();
                }
                return;
            }
        }

        private void SheepCount_KeyUp(object sender, KeyEventArgs e)
        {
            repaintBox(SheepCount, "2");
            GoToNextBox(CarrotCount, e.Key.ToString(), (TextBox)sender);
        }

        private void CarrotCount_KeyUp(object sender, KeyEventArgs e)
        {
            repaintBox(CarrotCount, "4");
            GoToNextBox(BallCount, e.Key.ToString(), (TextBox)sender);
        }

        private void BallCount_KeyUp(object sender, KeyEventArgs e)
        {
            repaintBox(BallCount, "3");
            GoToNextBox(null, e.Key.ToString(), (TextBox)sender);
        }


        private void repaintBox(TextBox box, string rightText)
        {
            if (box.Text != rightText && box.Text != "")
            {
                box.Background = Brushes.Red;
            }
            else
            {
                if (box.Text == rightText)
                {
                    box.Background = Brushes.Green;
                }
                if (box.Text == "")
                {
                    box.Background = Brushes.White;
                }
            }
            firstResult.Text = "";
            secondResult.Text = "";
            thirdResult.Text = "";
        }

        private void checkResult()
        {
            if (SheepCount.Background == Brushes.Green && CarrotCount.Background == Brushes.Green && BallCount.Background == Brushes.Green)
            {
                updateDataBase();
                GoToCongratulation(GameResult.CalculateStars());
            }
            else
            {
                writeResult(SheepCount, firstResult);
                writeResult(CarrotCount, secondResult);
                writeResult(BallCount, thirdResult);
            }
        }

        private void checkResult_Click(object sender, RoutedEventArgs e)
        {
            checkResult();
        }

        private void writeResult(TextBox box, TextBlock block)
        {
            if (box.Background == Brushes.Red)
            {
                block.Text = "Пересчитай";
                player = new SoundPlayer(Audio.GetSoundPath("Recount"));
                player.Play();
                return;
            }
            if (box.Background != Brushes.Green)
            {
                block.Text = "Заполни";
                player = new SoundPlayer(Audio.GetSoundPath("FillAllBoxes1"));
                player.Play();
            }
        }

        private void updateDataBase()
        {
            if (!(bool)Settings.Default["IsRegisteredUser"])
            {
                return;
            }
            var userLogin = Settings.Default["SavedLogin"].ToString();
            var connectionData = "server=localhost;user=root;database=users;password=admin12345;";
            var updatingData = $"update usersdata SET numbersCountSuccesses=numbersCountSuccesses+1 where login ='{userLogin}'";
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

        private void toMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void toNumbers_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Numbers());
        }

        private void check_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkResult();
        }
    }
}
