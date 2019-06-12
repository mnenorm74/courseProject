using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using registration.Properties;


namespace registration
{
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
            login.Focus();
        }

        private void Registrate()
        {
            Cursor = System.Windows.Input.Cursors.Wait;

            var enteredLogin = login.Text;
            var enteredPassword = password.Password;

            if (enteredLogin == "")
            {
                MessageBox.Show("Заполните все поля");
                login.Focus();
                Cursor = System.Windows.Input.Cursors.Arrow;
                return;
            }
            if (enteredPassword == "")
            {
                MessageBox.Show("Заполните все поля");
                password.Focus();
                Cursor = System.Windows.Input.Cursors.Arrow;
                return;
            }

            var connectionData = "server=localhost;user=root;database=users;password=admin12345;";
            var checkingData = $"select password from usersdata where login ='{enteredLogin}'";
            var connection = new MySqlConnection(connectionData);
            var check = new MySqlCommand(checkingData, connection);

            connection.Open();
            check.Prepare();
            check.ExecuteNonQuery();

            var dataBaseHash = (string)check.ExecuteScalar();

            if (Password.IsEqualPasswords(enteredPassword, dataBaseHash))
            {
                Settings.Default["SavedLogin"] = enteredLogin;
                Settings.Default["IsRegisteredUser"] = true;
                Settings.Default.Save();
                NavigationService.Navigate(new Menu());
            }
            else
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
                MessageBox.Show("Логин или пароль введены неверно. Попробуйте еще раз.");
            }

            connection.Close();

            Cursor = System.Windows.Input.Cursors.Arrow;
        }
       
        private void login_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return" || e.Key.ToString() == "Down")
            {
                password.Focus();
            }
        }

        private void password_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                Registrate();
                return;
            }
            if (e.Key.ToString() == "Up")
            {
                login.Focus();
            }
        }

        private void registrate_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Registrate();
        }
    }
}
