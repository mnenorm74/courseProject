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
using registration.Properties;

namespace registration
{
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
            login.Focus();
        }

        private void Registrate()
        {
            Cursor = Cursors.Wait;

            var enteredLogin = login.Text;
            var enteredPassword = password.Password;
            var enteredCopyPassword = passwordCopy.Password;

            if (enteredLogin == "" || enteredPassword == "" || enteredCopyPassword == "")
            {
                MessageBox.Show("Заполните все поля");
                Cursor = Cursors.Arrow;
                return;
            }

            var connectionData = "server=localhost;user=root;database=users;password=admin12345;";
            var connection = new MySqlConnection(connectionData);
            connection.Open();

            var checkData = $"select login from usersdata where login = '{enteredLogin}'";
            var check = new MySqlCommand(checkData, connection);
            check.Prepare();
            check.ExecuteNonQuery();

            if (check.ExecuteScalar() != null)
            {
                MessageBox.Show("Такой логин занят!");
                Cursor = Cursors.Arrow;
                login.Focus();
                return;
            }

            if (enteredPassword != enteredCopyPassword)
            {
                MessageBox.Show("Введенные пароли не совпадают. Попробуйте еще раз.");
                Cursor = Cursors.Arrow;
                password.Focus();
                password.Clear();
                passwordCopy.Clear();
                return;
            }

            var hash = Password.CalculateMD5Hash(enteredPassword);

            var addingData = $"insert into usersdata set login='{enteredLogin}', password='{hash}'";
            var add = new MySqlCommand(addingData, connection);
            add.Prepare();
            add.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Пользователь зарегистрирован");
            Settings.Default["SavedLogin"] = enteredLogin;
            Settings.Default["IsRegisteredUser"] = true;
            Settings.Default.Save();
            NavigationService.Navigate(new Menu());
            Cursor = Cursors.Arrow;
        }

        private void login_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return" || e.Key.ToString() == "Down")
            {
                password.Focus();
            }
        }

        private void password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return" || e.Key.ToString() == "Down")
            {
                passwordCopy.Focus();
                return;
            }
            if (e.Key.ToString() == "Up")
            {
                login.Focus();
            }
        }

        private void passwordCopy_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                Registrate();
            }
            if (e.Key.ToString() == "Up")
            {
                password.Focus();
            }
        }

        private void registrate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Registrate();
        }
    }
}
