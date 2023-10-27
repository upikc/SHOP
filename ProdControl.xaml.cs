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

        public ProdControl(Product_class Prod)
        {
            InitializeComponent();
            DataContext = Prod;

            Price.Content = Prod.Prise + " Рублей";
            Count.Content = Prod.Count + " Штук";

            CategoryLab.Content = DataBaseContext.Category.First(C => C.ID == Prod.Category_ID).Name;


            CreatorLab.Content = DataBaseContext.Creators.First(C => C.ID == Prod.Creator_ID).Name;



        }

        private void my_image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new ImageViev(my_image.Source).Show();
        }
    }
}
