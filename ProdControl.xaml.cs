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


namespace SHOP
{
    public partial class ProdControl : UserControl
    {
        public int count;
        public int IntPrise;

        public Product_class ProdClass;
        public ProdControl(Product_class Prod , int userD)
        {
            InitializeComponent();

            
            ProdClass = Prod;
            count = Prod.Count;
            IntPrise = Prod.Prise;
            DataContext = Prod;

            label1.Content = Prod.Sold;


            Price.Content = Prod.Prise + " Рублей";
            Count.Content = Prod.Count + " Штук";

            CategoryLab.Content = DataBaseContext.Category.First(C => C.ID == Prod.Category_ID).Name;

            CreatorLab.Content = DataBaseContext.Creators.First(C => C.ID == Prod.Creator_ID).Name;

            
            if (userD == 1)
            {
            Button1.Visibility = Visibility.Visible;
            Button2.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            }

        }

        private void my_image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new ImageViev(my_image.Source).Show();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(label1.Content.ToString()) < count)
                label1.Content = int.Parse(label1.Content.ToString())+1;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(label1.Content.ToString()) > 0)
            label1.Content = int.Parse(label1.Content.ToString()) -1;
        }
    }
}
