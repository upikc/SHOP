using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SHOP
{
    internal static class DataBaseContext
    {
        public static List<Product_class> Products { get; set; }
        public static List<Product_class> Category { get; set; }

        public static void ReadJson()
        {
            Products = JsonConvert.DeserializeObject<List<Product_class>>(File.ReadAllText("Products.json"));
            Category = JsonConvert.DeserializeObject<List<Product_class>>(File.ReadAllText("Сategory.json"));
        }



        public static void WriteAllJson()
        {
            File.WriteAllText("Products.json", JsonConvert.SerializeObject(Products));
            File.WriteAllText("Category.json", JsonConvert.SerializeObject(Category));
        }

    }
}
