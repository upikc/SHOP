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

            string Category_STR = DataBaseContext.Category.First(C => C.Category_ID == Prod.Category_ID).Name;
            string Creator_STR = DataBaseContext.Category.First(C => C.Creator_ID == Prod.Creator_ID).Name;


        }
    }
}
