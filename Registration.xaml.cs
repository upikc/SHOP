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
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Media.Animation;

namespace SHOP
{

    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void ButtonEscape_Click(object sender = null, RoutedEventArgs e = null)
        {
            new MainWindow().Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var UserList = DataBaseContext.GetUserList();
            if (!UserList.Any(p => p.login == LoginTBox.Text.Trim()) && !PasswordTBox.Text.Contains(" ") && LoginTBox.Text.Trim().Length > 3 && PasswordTBox.Text.Trim().Length > 3)
            {
                UserList.Add(new user(LoginTBox.Text.Trim(), PasswordTBox.Text, 1));
                DataBaseContext.WriteAllJson(UserList);
                ButtonEscape_Click();
            }
            else
            {
                DoubleAnimation AlertAnimation = new DoubleAnimation();
                AlertAnimation.From = AlertLabel.Opacity;
                AlertAnimation.To = 100;
                AlertAnimation.Duration = TimeSpan.FromMilliseconds(300);
                AlertAnimation.AutoReverse = true;
                AlertLabel.BeginAnimation(Button.OpacityProperty, AlertAnimation);
            }

        }
    }
}
