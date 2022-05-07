using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Text.RegularExpressions;
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace SLIMessanger
{
    class DialogManager : MainWindow
    {
        private User currnetUser;
        private User secondUser;
        private int topMargin = 0;

        public DialogManager(User currnetUser, User secondUser)
        {
            this.currnetUser = currnetUser;
            this.secondUser = secondUser;
        }

        public List<TextBlock> GetDialogMessages(ref Database database, ref StylesManager stylesManager)
        {
            List<TextBlock> output = new List<TextBlock>();
    
            List<Message> dialogMessages = database.GetDialogMessages(currnetUser.id.ToString(), secondUser.id.ToString());
            foreach (Message i in dialogMessages)
            {
                TextBlock message = GetTextBlockFromMessage(i.text.Split(), i.sender, stylesManager);
                output.Add(message);
            }
            return output;
        }

        public TextBlock GetTextBlockFromMessage(string [] words, string sender, StylesManager stylesManager)
        {
            TextBlock message = new TextBlock();

            string output = "";
            int cur = 0, lines = 0;
            for(int j = 0; j < words.Length; ++j)
            {
                if (words[j].Length + cur > 40)
                {
                    output += $"\n{words[j]} ";
                    lines++;
                    cur = words[j].Length;
                }
                else
                {
                    output += $"{words[j]} ";
                    cur += words[j].Length;
                }
            }

            message.Text = output;
            message.Style = stylesManager.dialogMessagesStyle;
            message.Margin = new Thickness(0, topMargin, 0, 0);


            topMargin += 40 + lines* 18;

            if (sender == currnetUser.id.ToString())
            {
                message.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            }
            else
            {
                message.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            }
            
            return message;
        }

        //public Image GetImageFromImage(Message message)
        //{
     
        //    string[] parts = message.text.Split();
        //    byte[] bytes = new byte[ parts.Length - 2];

        //    for (int i= 1; i < parts.Length-1; ++i)
        //    {
        //        bytes[i - 1] = Convert.ToByte(parts[i]);
        //    }

        //    System.Drawing.Image drawingImage = ImageConvertor.byteArrayToImage(bytes);

        //    string path = "D:\\ww.bmp";
        //    drawingImage.Save(path);

        //    Image image = new Image();
        //    BitmapImage bi3 = new BitmapImage();
        //    bi3.BeginInit();
        //    bi3.UriSource = new Uri(path, UriKind.Relative);
        //    bi3.EndInit();
        //    image.Stretch = Stretch.Fill;
        //    image.Source = bi3;

        //    return image;
        //}

    }
}
