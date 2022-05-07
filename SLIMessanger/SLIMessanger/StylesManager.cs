using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SLIMessanger
{
    class StylesManager
    {
        public Style userNamesStyle;
        public Style dialogMessagesStyle;
        public Style userNameTableStyle;


        public StylesManager()
        {
            userNamesStyle = new Style();
            dialogMessagesStyle = new Style();
            userNameTableStyle = new Style();

            Color frontUserNameColor = (Color)ColorConverter.ConvertFromString("#191970");
            Color frontDialogMessageColor = (Color)ColorConverter.ConvertFromString("#FFFF");
            Color backColor = (Color)ColorConverter.ConvertFromString("#1E90FF");
            Color backgroundUserTableColor = (Color)ColorConverter.ConvertFromString("#FFFF");

            userNamesStyle.Setters.Add(new Setter { Property = TextBlock.ForegroundProperty, Value = new SolidColorBrush(frontUserNameColor) });
            userNamesStyle.Setters.Add(new Setter { Property = TextBlock.PaddingProperty, Value = new Thickness(10, 10, 10, 10) });
            userNamesStyle.Setters.Add(new Setter { Property = TextBlock.FontSizeProperty, Value = 20.0 });
            userNamesStyle.Setters.Add(new Setter { Property = TextBlock.HorizontalAlignmentProperty, Value = HorizontalAlignment.Left});
            userNamesStyle.Setters.Add(new Setter { Property = TextBlock.VerticalAlignmentProperty, Value = VerticalAlignment.Center });

            dialogMessagesStyle.Setters.Add(new Setter { Property = TextBlock.ForegroundProperty, Value = new SolidColorBrush(frontDialogMessageColor) });
            dialogMessagesStyle.Setters.Add(new Setter { Property = TextBlock.FontSizeProperty, Value = 15.0 });
            dialogMessagesStyle.Setters.Add(new Setter { Property = TextBlock.PaddingProperty, Value = new Thickness(10, 10, 10, 10) });
            dialogMessagesStyle.Setters.Add(new Setter { Property = TextBlock.VerticalAlignmentProperty, Value = VerticalAlignment.Top });
            dialogMessagesStyle.Setters.Add(new Setter { Property = TextBlock.BackgroundProperty, Value =new SolidColorBrush(backColor) });

            userNameTableStyle.Setters.Add(new Setter { Property = Grid.HeightProperty, Value = 40.0 });
            //userNameTableStyle.Setters.Add(new Setter { Property = Grid.BackgroundProperty, Value = new SolidColorBrush(backgroundUserTableColor) });
            userNameTableStyle.Setters.Add(new Setter { Property = Grid.VerticalAlignmentProperty, Value = VerticalAlignment.Top });
        }


    }
}
