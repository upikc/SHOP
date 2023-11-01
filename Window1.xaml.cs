using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SHOP
{

    public partial class Window1 : Window
    {
        public ObservableCollection<ProdControl> ProdList = new ObservableCollection<ProdControl>();

        private void ReadinProdList(List<Product_class> SortedClasssesProducts)
        {
            ProdList.Clear();
            foreach (Product_class Pclass in SortedClasssesProducts)
            {
                ProdList.Add(new ProdControl(Pclass));//добавление пока что без сортировки
            }

        }

        private void Sorter_Cbox_Search_CatSelect()
        {
            List<Product_class> LocalProducts = DataBaseContext.Products;


            if (SearchTBox != null && SearchTBox.Text.Trim() != "")
                LocalProducts = (from prod in DataBaseContext.Products where prod.Name.ToLower().Contains(SearchTBox.Text.Trim().ToLower()) select prod).ToList();
            //сортировка букавками

            if (minTextBox != null && int.TryParse(minTextBox.Text, out int minInt))
                LocalProducts = (from prod in LocalProducts where (prod.Prise >= minInt) select prod).ToList();
            //сортировка цифрами

            if (maxTextBox != null && int.TryParse(maxTextBox.Text, out int maxInt))
                LocalProducts = (from prod in LocalProducts where (prod.Prise <= maxInt) select prod).ToList();
            //сортировка цифрами


            switch (Sorf_comboBox.SelectedIndex)
            {
                case 0:
                    LocalProducts = LocalProducts.OrderBy(p => p.Prise).ToList();
                    break;
                case 1:
                    LocalProducts = LocalProducts.OrderBy(p => p.Count).ToList();
                    break;
                case 2:
                    LocalProducts = LocalProducts.OrderBy(p => p.Name).ToList();
                    break;
                case 3:
                    LocalProducts = LocalProducts.OrderByDescending(p => p.Prise).ToList();
                    break;
                case 4:
                    LocalProducts = LocalProducts.OrderByDescending(p => p.Count).ToList();
                    break;
                case 5:
                    LocalProducts = LocalProducts.OrderByDescending(p => p.Name).ToList();
                    break;
            }



            List<Product_class> sortTed = new List<Product_class>();
            if (LocalProducts != null && CategoruListView != null)
            foreach (var prod in LocalProducts)
            {
                string nameOfCategoru = DataBaseContext.Category.First(C => C.ID == prod.Category_ID).Name; 

                foreach (CheckBox ChechBoxCat in CategoruListView.Items)
                {
                    if (nameOfCategoru == ChechBoxCat.Content.ToString() && ChechBoxCat.IsChecked == true)
                    {
                            sortTed.Add(prod);
                            break;
                    }
                }
            }

            Window_SizeChanged();
            this.Title = "window " + sortTed.Count.ToString();
            ReadinProdList(sortTed);
        }





        public Window1()
        {
            InitializeComponent();


            Products_ListView.ItemsSource = ProdList;

            foreach(var C_class in DataBaseContext.Category)
            {
                var buff = new CheckBox { Content = C_class.Name , IsChecked = true};
                buff.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(CheckBox_Checked));
                CategoruListView.Items.Add(buff);//Добавляем чекбоксы категорий
            }
            foreach (var Creator in DataBaseContext.Creators)
            {
                var buff = new CheckBox { Content = Creator.Name, IsChecked = true };
                buff.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(CheckBox_Checked));
                CreatorsListView.Items.Add(buff);//Добавляем чекбоксы категорий
            }//ОБЬЕДЕНИТЬ ЭТО В ОДИН МЕТОД

            Sorter_Cbox_Search_CatSelect();//первичная сортировка и вывод
        }

        private void Window_SizeChanged(object sender = null, SizeChangedEventArgs e = null)
        {
            if (Products_ListView != null)
            foreach (ProdControl item in Products_ListView.Items)
                item.Width = GetOptimalItemWidth();
        }

        private double GetOptimalItemWidth()
        {
            if (WindowState == WindowState.Maximized)
                return RenderSize.Width;
            return Width;
        }


        private void SearchTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sorter_Cbox_Search_CatSelect();

            //Products_ListView.Items.Clear(); //ВСЕ **** НЕ РАБОТАЕТ ЭТО БОЛЬШЕ
            //if (SearchTBox.Text.Trim() != "")
            //{
            //    foreach (Product_class product in (from prod in DataBaseContext.Products where prod.Name.ToLower().Contains(SearchTBox.Text.Trim().ToLower()) select prod))
            //    {
            //        Products_ListView.Items.Add(new ProdControl(product));
            //    }
            //}
            //else
            //{
            //    foreach (Product_class product in DataBaseContext.Products)
            //    {
            //        Products_ListView.Items.Add(new ProdControl(product));
            //    }
            //}
            //foreach (ProdControl item in Products_ListView.Items)
            //    Window_SizeChanged();

        }

        private void Sorf_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)//делаем сортировку пузырьком
        {
            Sorter_Cbox_Search_CatSelect();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Sorter_Cbox_Search_CatSelect();
        }
    }
}
