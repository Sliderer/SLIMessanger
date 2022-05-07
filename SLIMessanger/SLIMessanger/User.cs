using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SLIMessanger
{
    class User
    {
        public int id;
        public string name;
        public string surname;
        public string email;
        public string login;
        public string password;

        public User(int id, string name, string surname, string email, string login, string password)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.login = login;
            this.password = password;
        }

        public User(SqlDataReader dataReader)
        {
            this.id = int.Parse(dataReader["id"].ToString());
            this.name = dataReader["name"].ToString();
            this.surname = dataReader["surname"].ToString();
            this.email = dataReader["email"].ToString();
            this.login = dataReader["login"].ToString();
            this.password = dataReader["password"].ToString();
        }

        public static bool operator==(User a, User b)
        {
            return a.login == b.login;
        }

        public static bool operator !=(User a, User b)
        {
            return a.login != b.login;
        }

        public Grid GetTextBlock(int topMargin, StylesManager stylesManager, string usersName, MouseButtonEventHandler clickFunction)
        {
            Grid userTable = new Grid();
            userTable.Margin = new Thickness(0, topMargin, 0, 0);
            userTable.Style = stylesManager.userNameTableStyle;

            TextBlock text = new TextBlock();
            text.Text = usersName;
            text.MouseDown += clickFunction;
            text.Style = stylesManager.userNamesStyle;

            userTable.Children.Add(text);

            Separator separator = new Separator();
            separator.VerticalAlignment = VerticalAlignment.Bottom;
            userTable.Children.Add(separator);
            return userTable;
        }
    }
}
