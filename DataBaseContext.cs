﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace SHOP
{
    public static class DataBaseContext
    {
        private static List<Product_class> _Products { get; set; }
        private static List<Creator_class> _Creators { get; set; }
        private static List<Сategory_class> _Category { get; set; }

        public static List<Product_class> Products
        {
            get
            {
                if (_Products == null)
                    _Products = JsonConvert.DeserializeObject<List<Product_class>>(File.ReadAllText("Products.json"));
                return _Products;
            }
        }
        public static List<Creator_class> Creators
        {
            get
            {
                if (_Creators == null)
                    _Creators = JsonConvert.DeserializeObject<List<Creator_class>>(File.ReadAllText("Creators.json"));
                return _Creators;
            }
        }
        public static List<Сategory_class> Category
        {
            get
            {
                if (_Category == null)
                    _Category = JsonConvert.DeserializeObject<List<Сategory_class>>(File.ReadAllText("Сategory.json"));
                return _Category;
            }
        }




        [ExcludeFromCodeCoverage]
        public static void WriteAllJson(List<Product_class> Prod, List<Creator_class> Creat, List<Сategory_class> Cat)
        {
            File.WriteAllText("Products.json", JsonConvert.SerializeObject(Prod));
            File.WriteAllText("Creators.json", JsonConvert.SerializeObject(Creat));
            File.WriteAllText("Сategory.json", JsonConvert.SerializeObject(Cat));
        }

        [ExcludeFromCodeCoverage]
        public static void WriteUsersJson(List<user> UserList)
        {
            File.WriteAllText("Users.json", JsonConvert.SerializeObject(UserList));
        }


        public static List<user> GetUserList() => JsonConvert.DeserializeObject<List<user>>(File.ReadAllText("Users.json"));



        private static List<user> _UserList { get; set; }
        public static List<user> UserList
        {
            get
            {
                if (_UserList == null)
                    _UserList = JsonConvert.DeserializeObject<List<user>>(File.ReadAllText("Users.json"));
                return _UserList;
            }
        } //=> JsonConvert.DeserializeObject<List<user>>(File.ReadAllText("Users.json"));


        [ExcludeFromCodeCoverage]
        public static List<Product_class> SortedProd(Window1 window)
        {
            List<Product_class> LocalProducts = DataBaseContext.Products;


            if (window.SearchTBox != null && window.SearchTBox.Text.Trim() != "")
                LocalProducts = (from prod in DataBaseContext.Products where prod.Name.ToLower().Contains(window.SearchTBox.Text.Trim().ToLower()) select prod).ToList();
            //сортировка букавками

            if (window.minTextBox != null && int.TryParse(window.minTextBox.Text, out int minInt))
                LocalProducts = (from prod in LocalProducts where (prod.Prise >= minInt) select prod).ToList();
            //сортировка цифрами

            if (window.maxTextBox != null && int.TryParse(window.maxTextBox.Text, out int maxInt))
                LocalProducts = (from prod in LocalProducts where (prod.Prise <= maxInt) select prod).ToList();
            //сортировка цифрами


            switch (window.Sorf_comboBox.SelectedIndex)
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
            if (LocalProducts != null && window.CategoruListView != null)
            {
                foreach (var prod in LocalProducts)
                {
                    string nameOfCategoru = DataBaseContext.Category.First(C => C.ID == prod.Category_ID).Name;
                    string nameOfCreator = DataBaseContext.Creators.First(C => C.ID == prod.Creator_ID).Name;

                    foreach (CheckBox ChechBoxCat in window.CategoruListView.Items)
                    {
                        if (nameOfCategoru == ChechBoxCat.Content.ToString() && ChechBoxCat.IsChecked == true)
                        {
                            //sortTed.Add(prod); //сортировка только категории
                            foreach (CheckBox ChechBoxCreat in window.CreatorsListView.Items)
                            {
                                if (nameOfCreator == ChechBoxCreat.Content.ToString() && ChechBoxCreat.IsChecked == true)
                                {
                                    sortTed.Add(prod);
                                    break;//секретный метод сортировки от Стюковой
                                }
                            }
                        }
                    }
                }
            }
            return sortTed;
        }

        public static bool isValidCredentials_reg(List<user> UserList , string LoginText , string PassText) => (!UserList.Any(p => p.login == LoginText.Trim()) && !LoginText.Trim().Contains(" ") && !PassText.Contains(" ") && PassText.Trim().Length > 3 && LoginText.Trim().Length > 3);

        public static user isValidCredentials_login(string login , string password) => DataBaseContext.UserList.FirstOrDefault(p => p.login == login.Trim() && p.pass == password);


        public static List<Product_class> Basket(ListView Products_ListView)
        {
            List<Product_class> basket = new List<Product_class>();
            foreach (ProdControl item in Products_ListView.Items)
            {

                if (int.TryParse(item.label1.Content.ToString(), out int f) && f != 0)
                {
                    item.ProdClass.Sold = f;
                    basket.Add(item.ProdClass);
                }
            }
            return basket;
        }

        public static void SocketSend(string message = "null")
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect("localhost", 11000);
                socket.Send(Encoding.UTF8.GetBytes(message));
                socket.Close();
                
            }
        }
    }
}
