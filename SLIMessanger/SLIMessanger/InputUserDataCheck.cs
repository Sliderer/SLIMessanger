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

namespace SLIMessanger
{
    class InputUserDataCheck : MainWindow
    {

        public bool CheckRegistrationData(ref Database database, string name, string surname, string email, string login, string password)
        {
            if (name == "" || surname == "" || email == "" ||
                login == "" || password == "")
            {
                MessageBox.Show("Заполните все поля!");
                return false;
            }

            if (name.Length < 3)
            {
                MessageBox.Show("Имя должно состоять минимум из 3 символов");
                return false;
            }

            if (surname.Length < 3)
            {
                MessageBox.Show("Фамилия должна состоять минимум из 3 символов");
                return false;
            }

            if (login.Length < 3)
            {
                MessageBox.Show("Логин должн состоять минимум из 3 символов");
                return false;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("Пароль должн состоять минимум из 8 символов");
                return false;
            }

            if (database.UserExist(login))
            {
                MessageBox.Show("Такой пользователь уже существует");
                return false;
            }
            return true;
        }

        public bool CheckLogInData(Database database, string login, string password)
        {

            if (password == "" || login == "")
            {
                MessageBox.Show("Заполните все поля!");
                return false;
            }

            if (!database.UserExist(login))
            {
                MessageBox.Show("Такого пользователя не существует");
                return false;
            }

            

            return true;
        }
    }
}
