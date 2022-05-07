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
using System.Drawing;
using System.IO;
using Microsoft.Win32;

namespace SLIMessanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database database;
        private User currentUser;
        private User dialogUser;
        private InputUserDataCheck dataChecker;
        private DialogManager dialog;
        private StylesManager stylesManager;

        public MainWindow()
        {
            InitializeComponent();
            database = new Database();
            stylesManager = new StylesManager();
            LogInGrid.Visibility = Visibility.Visible;
            RegistrationGrid.Visibility = Visibility.Hidden;
            MainMessangerGrid.Visibility = Visibility.Hidden;
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            dataChecker = new InputUserDataCheck();

            if (!dataChecker.CheckLogInData(database, loginTextLogIn.Text, passwordTextLogIn.Text))
            {
                return;
            }

            currentUser = database.GetUser(loginTextLogIn.Text);
            LogIn();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(loginTextRegistration.Text.GetHashCode(), nameTextRegistration.Text, surnameTextRegistration.Text,
                emailTextRegistration.Text, loginTextRegistration.Text, passwordTextRegistration.Text);

            dataChecker = new InputUserDataCheck();

            if (!dataChecker.CheckRegistrationData(ref database, nameTextRegistration.Text, surnameTextRegistration.Text,
                emailTextRegistration.Text, loginTextRegistration.Text, passwordTextRegistration.Text))
            {
                return;
            }

            currentUser = user;
            database.AddNewUser(user);
            LogIn();
        }

        private void SwitchButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            LogInGrid.Visibility = Visibility.Hidden;
            RegistrationGrid.Visibility = Visibility.Visible;
        }

        private void SwitchButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            LogInGrid.Visibility = Visibility.Visible;
            RegistrationGrid.Visibility = Visibility.Hidden;
        }

        private void LogIn()
        {
            LogInGrid.Visibility = Visibility.Hidden;
            RegistrationGrid.Visibility = Visibility.Hidden;
            MainMessangerGrid.Visibility = Visibility.Visible;
            ChatGrid.Visibility = Visibility.Hidden;
            InitMainMessangerGrid();
        }

        private void InitMainMessangerGrid()
        {
            List<User> users = database.GetAllUsers();
            int topMargin = 0;

            foreach(User i in users)
            {
                if (i == currentUser) continue;
                var obj = i.GetTextBlock(topMargin, stylesManager, i.login, UserTextClick);
                topMargin += 40;
                UsersScrollViewer.Children.Add(obj);
            }
        }

        public void UserTextClick(object sender, RoutedEventArgs e)
        {
            TextBlock text = (TextBlock)sender;
            string login = text.Text;
            dialogUser = database.GetUser(login);

            ChatGrid.Visibility = Visibility.Visible;
            ChatScrollView.Children.Clear();

            dialog = new DialogManager(currentUser, dialogUser);

            List<TextBlock> messages = dialog.GetDialogMessages(ref database, ref stylesManager);
            foreach(TextBlock i in messages)
            {
                AddTextBlockToDialog(i);
            }
        }

        private void SendMessage()
        {
            if (MessageText.Text != "")
            {
                Message message = new Message(currentUser.id.ToString(), dialogUser.id.ToString(), MessageText.Text, DateTime.Now);
                AddTextBlockToDialog(dialog.GetTextBlockFromMessage(message.text.Split(), message.sender, stylesManager));
                database.AddMessage(message);
                MessageText.Text = "";
            }
        }

        public void AddTextBlockToDialog(TextBlock message)
        {
            ChatScrollView.Children.Add(message);
        }

        public void AddImageToDialog(System.Windows.Controls.Image image)
        {
            image.Height = 200;
            image.Width = 200;
            ChatScrollView.Children.Add(image);

        }

        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void MessageText_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            string messageText = MessageText.Text;
            if (messageText.Length <= 250)
            {
                MessageText.Height = 50 + 15 * (messageText.Length / 50);
            }
        }

        private void MessageText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendImage_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    System.Drawing.Image image = System.Drawing.Image.FromFile($"{openFileDialog.FileName}");
            //    byte[] bytesArray = ImageConvertor.imageToByteArray(image);
            //    string text = "{IMAGE} ";
            //    for(int i = 0; i < bytesArray.Length; ++i)
            //    {
            //        text += $"{bytesArray[i]} ";
            //    }
            //    Message message = new Message(currentUser.id.ToString(), dialogUser.id.ToString(), text, DateTime.Now);

            //    AddImageToDialog(dialog.GetImageFromImage(message));
            //    database.AddMessage(message);
            //}
 
        }

        private void DataTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if ( ((sender as TextBox).Parent as Grid).Name == "RegistrationGrid")
                {
                    RegistrationButton_Click(sender, e);
                }
                else
                {
                    LogInButton_Click(sender, e);
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
