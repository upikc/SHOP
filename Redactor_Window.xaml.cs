using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// <summary>
    /// Логика взаимодействия для Redactor_Window.xaml
    /// </summary>

    public partial class Redactor_Window : Window
    {

        //ObservableCollection<Product_class> Prod = new ObservableCollection<Product_class>();
        //ObservableCollection<Creator_class> Creat = new ObservableCollection<Creator_class>();
        //ObservableCollection<Сategory_class> Cat = new ObservableCollection<Сategory_class>();

        List<Product_class> Prod = new List<Product_class>();
        List<Creator_class> Creat = new List<Creator_class>();
        List<Сategory_class> Catd = new List<Сategory_class>();


        public double MyFsize = 13;

        public Redactor_Window()
        {
            InitializeComponent();

            //Prod = DataBaseContext.Products ;
            //Creat = DataBaseContext.Creators;
            //Cat = DataBaseContext.Category;

            //ProductsDataGrid.ItemsSource = Prod;
            //CreatorDataGrid.ItemsSource = Creat;
            //CategoruDataGrid.ItemsSource = Cat;


            Prod = DataBaseContext.Products;
            ProductsDataGrid.ItemsSource = Prod;

            Creat = DataBaseContext.Creators;
            CreatorDataGrid.ItemsSource = Creat;

            Catd = DataBaseContext.Category;
            CategoruDataGrid.ItemsSource = Catd;


            //ProductsDataGrid.ItemsSource = DataBaseContext.Products;
            //CreatorDataGrid.ItemsSource = DataBaseContext.Creators;
            //CategoruDataGrid.ItemsSource = DataBaseContext.Category;
        }

        private void Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            while (true)
            {
                bool b = true;
                foreach (var item in Prod)
                {
                    if (!(Catd.Any(C => C.ID == item.Category_ID) && Creat.Any(C => C.ID == item.Creator_ID)))//все ли правильно введены ID
                    {
                        Prod.Remove(item);
                        b = false;
                        break;
                    }
                }
                if (b)
                break;//можно изменить на удаление ошибок по клику
            }
            
             new Window1(2).Show();
             this.Close();


        }

        private void ProductsDataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Prod.Remove(ProductsDataGrid.SelectedItem as Product_class);
            ProductsDataGrid.ItemsSource = null;
            ProductsDataGrid.ItemsSource = Prod;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var item in Prod)
            {
                if (!(Catd.Any(C => C.ID == item.Category_ID) && Creat.Any(C => C.ID == item.Creator_ID)))//все ли правильно введены ID
                {
                    MessageBox.Show("проблема с индексом у " + item.Name);
                    return;
                }
            }




            DataBaseContext.WriteAllJson(Prod, Creat, Catd);
        }


        private void ProductsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = " картинка аниме |*.png;*.jpg";
            

            if (fileDialog.ShowDialog() == true)
            {
                var s = ProductsDataGrid.SelectedItem as Product_class;
                s.Images = fileDialog.FileName;
                ProductsDataGrid.ItemsSource = null;
                ProductsDataGrid.ItemsSource = Prod;
            }
        }

        

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double f = this.Width / 80;
            ProductsDataGrid.FontSize = f;
            CategoruDataGrid.FontSize = f;
            CreatorDataGrid.FontSize = f;
        }

        private void CategoruDataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Catd.Remove(CategoruDataGrid.SelectedItem as Сategory_class);
            CategoruDataGrid.ItemsSource = null;
            CategoruDataGrid.ItemsSource = Catd;
        }

        private void CreatorDataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Creat.Remove(CreatorDataGrid.SelectedItem as Creator_class);
            CreatorDataGrid.ItemsSource = null;
            CreatorDataGrid.ItemsSource = Creat;
        }
    }
}
