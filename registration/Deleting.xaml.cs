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

namespace registration
{
    public partial class Deleting : Page
    {
        public Deleting()
        {
            InitializeComponent();
            login.Focus();
        }

        private void Delete()
        {
            Cursor = Cursors.Wait;

            var enteredLogin = login.Text;
            var enteredPassword = password.Password;

            if (enteredLogin == "" || enteredPassword == "")
            {
                Cursor = Cursors.Arrow;
                MessageBox.Show("Заполните все поля");
                login.Focus();
                return;
            }

            var hash = Password.CalculateMD5Hash(enteredPassword);

            var connectionData = "server=localhost;user=root;database=users;password=admin12345;";
            var connection = new MySqlConnection(connectionData);
            connection.Open();

            var checkData = $"select login from usersdata where login = '{enteredLogin}' and password = '{hash}'";
            var check = new MySqlCommand(checkData, connection);
            check.Prepare();
            check.ExecuteNonQuery();

            if (check.ExecuteScalar() == null)
            {
                Cursor = Cursors.Arrow;
                MessageBox.Show("Такого пользователя не существует или неверно введен логин/пароль");
                return;
            }

            var delitingData = $"delete from usersdata where login='{enteredLogin}'";
            var delete = new MySqlCommand(delitingData, connection);
            delete.Prepare();
            delete.ExecuteNonQuery();

            connection.Close();
            MessageBox.Show("Пользователь удален!");
            NavigationService.Navigate(new Authorization());
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
            if (e.Key.ToString() == "Return")
            {
                Delete();
                return;
            }
            if (e.Key.ToString() == "Up")
            {
                login.Focus();
            }
        }

        private void delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Delete();
        }
    }
}
