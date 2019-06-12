using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace registration
{
    public partial class CongratulationThreeStars : Page
    {
        public CongratulationThreeStars()
        {
            InitializeComponent();
            mailBox.Focus();
        }

        private string GetText()
        {
            var alphabetCountSuccess = GameResult.GetAlphabetCountSuccess();
            var figuresCountSuccess = GameResult.GetFiguresCountSuccess();
            var numbersCountSuccess = GameResult.GetNumbersCountSuccess();
            var text = new StringBuilder();
            text.Append("Привет, ")
                .Append(Settings.Default["SavedLogin"])
                .Append("! ")
                .Append("Ты успешно прошел обучающий курс от программы Кот Ученый. ")
                .Append("Мы надеемся, что он тебе понравился, ты узнал много нового и хорошо провел время. ")
                .Append("За все время в игре твои успехи таковы. Ты прошел игру Алфавит ")
                .Append(alphabetCountSuccess)
                .Append(" ")
                .Append(GetCountForm(alphabetCountSuccess))
                .Append(" , игру Фигуры ")
                .Append(figuresCountSuccess)
                .Append(" ")
                .Append(GetCountForm(figuresCountSuccess))
                .Append(" , игру Цифры ")
                .Append(numbersCountSuccess)
                .Append(" ")
                .Append(GetCountForm(numbersCountSuccess))
                .Append(". Спасибо что ты был с нами, дорогой друг! Желаем тебе дальнейшего развития и успехов!");
            return text.ToString();
        }


        private void SendMail(string mailAdress)
        {
            try
            {
                Mail.SendEmail(mailAdress, "Кот Ученый", "mailforcourseproject@gmail.com",
                    "smtp.gmail.com", 587, "admin12345_", "Успешное прохождение курса", GetText(), @"documents/doc.png"); 
            }
            catch (Exception error)
            {
                MessageBox.Show("Что-то пошло не так");
                Mail.SendEmail("mailforcourseproject@gmail.com", "Кот Ученый", "mailforcourseproject@gmail.com",
                    "smtp.gmail.com", 587, "admin12345_", "Ошибка отправки", error.ToString());
            }
        }

        private void TrySendMail()
        {
            var enteredMail = mailBox.Text;
            if (enteredMail == "")
            {
                MessageBox.Show("Поле не может быть пустым");
                return;
            }
            if(IsValid(enteredMail))
            {
                Cursor = Cursors.Wait;
                SendMail(enteredMail);
                Cursor = Cursors.Arrow;
                MessageBox.Show("Письмо отправлено");
                NavigationService.Navigate(new Captions());
            }
            else
            {
                MessageBox.Show("Проверьте правильность почтового адреса");        
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                TrySendMail();
            }
        }

        private bool IsValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        private void ToMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void mailBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                TrySendMail();
            }
        }

        private void Send_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrySendMail();
        }

        private string GetCountForm(int count)
        {
            var number = count > 9 ? count % 10 : count;
            return number == 2 || number == 4 ? "раза" : "раз";
        }
    }
}
