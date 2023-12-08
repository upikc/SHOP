using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SHOP
{
    /// <summary>
    /// Логика взаимодействия для Carz.xaml
    /// </summary>
    public partial class Carz : Window
    {
        public ObservableCollection<ProdControl> bask = new ObservableCollection<ProdControl>();

        public user this_user = null;

        public Carz(List<Product_class> bask_list , user logged_user)
        {
            InitializeComponent();

            this_user = logged_user;

            Products_ListView.ItemsSource = bask;

            foreach (Product_class Pclass in bask_list)
            {
                bask.Add(new ProdControl(Pclass , 1));//добавление 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataBaseContext.SocketSend(JsonConvert.SerializeObject(DataBaseContext.Basket(Products_ListView)) + "{%Yay$}" + JsonConvert.SerializeObject(this_user));
            }
            catch (Exception ex) { }
        }
    }
}
