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
    public partial class AlphabetGame : Page
    {
        Regex letters = new Regex(@"[а-я]", RegexOptions.IgnoreCase);
        SoundPlayer player;

        void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match matchFirst = letters.Match(e.Text);
            if ((sender as TextBox).Text.Length > 1 || !matchFirst.Success)
            {
                e.Handled = true;
            }
        }

        public AlphabetGame()
        {
            InitializeComponent();
            А.Focus();

            foreach (var element in controls.Children)
            {
                if (element.GetType() == typeof(TextBox))
                {
                    ((TextBox)element).PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
                }
            }
        }


        private void word_KeyUp(object sender, KeyEventArgs e)
        {
            var symbol = e.Key.ToString();
            var nextBox = GetNextBox((TextBox)sender);
            var previousBox = GetPreviousBox((TextBox)sender);

            if (symbol == "Return")
            {
                checkResult();
                return;
            }
            if (symbol == "Right")
            {
                GoToNextBox(nextBox);
                return;
            }
            if (symbol == "Space")
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
                GoToNextBox(nextBox);
                return;
            }
            if (symbol == "Left")
            {
                GoToPreviousBox(previousBox);
                return;
            }
            if (symbol == "Back" && ((TextBox)sender).Text == "")
            {
                GoToPreviousBox(previousBox);
                RepaintBox(sender);
                return;
            }
            if (((TextBox)sender).Text != "")
            {
                GoToNextBox(nextBox);
                RepaintBox(sender);
            }
        }

        private void GoToNextBox(TextBox nextBox)
        {
            if (nextBox != null)
            {
                nextBox.Focus();
            }
        }

        private void GoToPreviousBox(TextBox previousBox)
        {
            if (previousBox != null)
            {
                previousBox.Focus();
            }
        }

        private void RepaintBox(object sender)
        {
            if (isEqual(((TextBox)sender).Name, ((TextBox)sender).Text))
            {
                ((TextBox)sender).Background = Brushes.Green;
            }
            else
            {
                if (((TextBox)sender).Text != "")
                {
                    ((TextBox)sender).Background = Brushes.Red;
                }
                else
                {
                    ((TextBox)sender).Background = Brushes.White;
                }
            }
            Result.Text = "";
        }

        private bool isEqual(string senderName, string enteredName)
        {
            return senderName.Substring(0, 1) == enteredName.ToLower() || senderName.Substring(0, 1) == enteredName.ToUpper();
        }

        private void toAlphabet_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Alphabet());
        }

        private bool isAllRight(Grid word)
        {
            foreach(var element in word.Children)
            {
                if (((TextBox)element).Background != Brushes.Yellow)
                {
                    return false;
                }
            }
            return true;
        }


        private void checkResult()
        {
            var boxes = controls.Children.OfType<TextBox>();

            foreach (var box in boxes)
            {
                if (box.Background != Brushes.Green && box.Background != Brushes.Red)
                {
                    //MessageBox.Show("Заполни все ячейки");
                    player = new SoundPlayer(Audio.GetSoundPath("FillAllBoxes1"));
                    player.Play();
                    Result.Text = "Заполни все ячейки";
                    return;
                }
            }

            foreach (var box in boxes)
            {
                if (box.Background == Brushes.Red)
                {
                    //MessageBox.Show("Проверь красные ячейки");
                    player = new SoundPlayer(Audio.GetSoundPath("CheckRedBoxes"));
                    player.Play();
                    Result.Text = "Проверь красные ячейки";
                    return;
                }
            }

            updateDataBase();
            GoToCongratulation(GameResult.CalculateStars());
        }

        private TextBox GetNextBox(TextBox currentBox)
        {
            var word = ((Grid)currentBox.Parent).Children.OfType<TextBox>();
            var unfilledPart = word.SkipWhile(x => x != currentBox).Skip(1);
            return unfilledPart.Count() > 0 ? unfilledPart.First() : null;
        }

        private TextBox GetPreviousBox(TextBox currentBox)
        {
            var word = ((Grid)currentBox.Parent).Children.OfType<TextBox>();
            var unfilledPart = word.Reverse().SkipWhile(x => x != currentBox).Skip(1);
            return unfilledPart.Count() > 0 ? unfilledPart.First() : null;
        }


        private void updateDataBase()
        {
            if (!(bool)Settings.Default["IsRegisteredUser"])
            {
                return;
            }
            var userLogin = Settings.Default["SavedLogin"].ToString();
            var connectionData = "server=localhost;user=root;database=users;password=admin12345;";
            var updatingData = $"update usersdata SET alphabetCountSuccesses=alphabetCountSuccesses+1 where login ='{userLogin}'";
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
            if(starsCount == 1)
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

        private void toAlphabet_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Alphabet());
        }

        private void check_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkResult();
        }

       
    }
}
