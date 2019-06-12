using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using registration.Properties;
using System.Windows.Navigation;

namespace registration
{
    class GameResult
    {
        private static int GetCountSuccess(string name)
        {
            if (!(bool)Settings.Default["IsRegisteredUser"])
            {
                return 0;
            }
            var userLogin = Settings.Default["SavedLogin"].ToString();
            var connectionData = "server=localhost;user=root;database=users;password=admin12345;";
            var alphabetCount = $"select {name} from usersdata where login ='{userLogin}'";
            var connection = new MySqlConnection(connectionData);
            var get = new MySqlCommand(alphabetCount, connection);
            connection.Open();
            get.Prepare();
            get.ExecuteNonQuery();
            var count = (int)get.ExecuteScalar();
            return count;
        }

        private static bool IsStudied(string name)
        {
            return GetCountSuccess(name) > 0 ? true : false;
        }

        public static bool IsAlphabetStudied()
        {
            return IsStudied("alphabetCountSuccesses");
        }

        public static int GetAlphabetCountSuccess()
        {
            return GetCountSuccess("alphabetCountSuccesses");
        }

        public static bool IsFiguresStudied()
        {
            return IsStudied("figuresCountSuccesses");
        }

        public static int GetFiguresCountSuccess()
        {
            return GetCountSuccess("figuresCountSuccesses");
        }

        public static bool IsNumbersStudied()
        {
            return IsStudied("numbersCountSuccesses");
        }

        public static int GetNumbersCountSuccess()
        {
            return GetCountSuccess("numbersCountSuccesses");
        }

        public static int CalculateStars()
        {
            if (!(bool)Settings.Default["IsRegisteredUser"])
            {
                return 0;
            }
            var starsCount = 0;
            if (IsAlphabetStudied())
            {
                starsCount++;
            }
            if (IsFiguresStudied())
            {
                starsCount++;
            }
            if (IsNumbersStudied())
            {
                starsCount++;
            }
            return starsCount;
        }

    }
}
