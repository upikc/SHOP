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
        

        public static List<Product_class> Products 
        {   get {return JsonConvert.DeserializeObject<List<Product_class>>(File.ReadAllText("Products.json"));}
        }
        public static List<Creator_class> Creators
        { get { return JsonConvert.DeserializeObject<List<Creator_class>>(File.ReadAllText("Creators.json")); } }
        public static List<Сategory_class> Category
        { get { return JsonConvert.DeserializeObject<List<Сategory_class>>(File.ReadAllText("Сategory.json")); }
}




        public static void WriteAllJson()
        {
            File.WriteAllText("Products.json", JsonConvert.SerializeObject(Products));
            File.WriteAllText("Category.json", JsonConvert.SerializeObject(Creators));
            File.WriteAllText("Category.json", JsonConvert.SerializeObject(Category));
        }

    }
}
