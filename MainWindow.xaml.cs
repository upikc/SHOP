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
using Newtonsoft.Json;
using System.IO;
using System.Windows.Media.Animation;

//grid.row grid.colums = ""

namespace SHOP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Registration().Show();
            Close();
        }

        private void Button_Enter_Press(object sender, RoutedEventArgs e)
        {
            var UserList = JsonConvert.DeserializeObject<List<user>>(File.ReadAllText("Users.json"));
            var user = UserList.FirstOrDefault(p => p.login == LoginTBox.Text.Trim() && p.pass == PasswordTBox.Text);
            if (user != null)
            {
                new Window1(user.role).Show();
                Close();
            }
            DoubleAnimation AlertAnimation = new DoubleAnimation();
            AlertAnimation.From = AlertLabel.Opacity;
            AlertAnimation.To = 100;
            AlertAnimation.Duration = TimeSpan.FromMilliseconds(300);
            AlertAnimation.AutoReverse = true;
            AlertLabel.BeginAnimation(Button.OpacityProperty, AlertAnimation);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Window1(0).Show();
            Close();
        }
    }
}
