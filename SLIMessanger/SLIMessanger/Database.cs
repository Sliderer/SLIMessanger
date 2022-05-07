using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace SLIMessanger
{
    class Database
    {
        private string connectionString = @"Data Source=LAPTOP-10LBF8FV\SLIDERER;Initial Catalog=MessangerDatabase;Integrated Security=True;";
        private SqlConnection sqlConnection = null;

        public Database()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }
        public bool LogIn(string login, string password)
        {
            SqlCommand command = new SqlCommand($"SELECT password FROM Users WHERE login = '{login}'", sqlConnection);
            SqlDataReader dataReader = command.ExecuteReader();
            
            while (dataReader.Read())
            {
                if (password == dataReader["password"].ToString())
                {
                    dataReader.Close();
                    return true;
                }
            }
            dataReader.Close();
            return false;
        }

        public bool UserExist(string login)
        {
    
            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE login = '{login}'", sqlConnection);
            SqlDataReader dataReader = command.ExecuteReader();

            bool result = dataReader.HasRows;
            dataReader.Close();
            return result;
        }

        public User GetUser(string login)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE login = '{login}'", sqlConnection);
            SqlDataReader dataReader = command.ExecuteReader();
            User user = null;
            while (dataReader.Read())
            {
                user = new User(dataReader);
            }
            dataReader.Close();
            return user;
        }

        public void AddNewUser(User user)
        {
            string colums = "id, name, surname, email, login, password";
            string values = $"{user.id}, '{user.name}', '{user.surname}', '{user.email}', '{user.login}', '{user.password}'";
            SqlCommand command = new SqlCommand($"INSERT INTO Users ({colums}) VALUES ({values})", sqlConnection);
            command.ExecuteNonQuery();
        }

        public List<User> GetAllUsers()
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM Users", sqlConnection);
            SqlDataReader dataReader = command.ExecuteReader();
            List<User> users = new List<User>();

            while (dataReader.Read())
            {
                users.Add(new User(dataReader));
            }
            dataReader.Close();
            return users;
        }

        public void AddMessage(Message message)
        {
            SqlCommand command = new SqlCommand($"INSERT INTO Messages (sender, recipient, text, date) VALUES ('{message.sender}', '{message.recipient}', '{message.text}', '{message.date}')", sqlConnection);
            command.ExecuteNonQuery();
        }

        public List<Message> GetDialogMessages(string firstUserId, string secondUserId)
        {
            List<Message> output = new List<Message>();

            SqlCommand command = new SqlCommand($"SELECT * FROM Messages WHERE (sender = '{firstUserId}' AND recipient = '{secondUserId}') OR (sender = '{secondUserId}' AND recipient = '{firstUserId}')", sqlConnection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                output.Add(new Message(dataReader["sender"].ToString(), dataReader["recipient"].ToString(), dataReader["text"].ToString(), Convert.ToDateTime(dataReader["date"]) ));
            }
            dataReader.Close();
            return output;
        }
    }
}
