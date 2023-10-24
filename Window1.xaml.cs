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


namespace SHOP
{

    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            DataBaseContext dataBaseContext = new DataBaseContext();
            dataBaseContext.ReadJson();


            foreach( Product_class product in dataBaseContext.Products )
            {
                Products_ListView.Items.Add(new ProdControl(product));
            }
            








        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (ProdControl item in Products_ListView.Items)
                item.Width = GetOptimalItemWidth();
        }

        private double GetOptimalItemWidth()
        {
            if (WindowState == WindowState.Maximized)
                return RenderSize.Width - 60;
            return Width - 60;
        }
    }
}
