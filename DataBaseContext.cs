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
        private static List<Product_class> _Products { get; set; }
        private static List<Creator_class> _Creators { get; set; }
        private static List<Сategory_class> _Category { get; set; }

        public static List<Product_class> Products 
        {   get
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




        public static void WriteAllJson(List<Product_class> Prod , List<Creator_class> Creat , List<Сategory_class> Cat)
        {
            File.WriteAllText("Products.json", JsonConvert.SerializeObject(Prod));
            File.WriteAllText("Creators.json", JsonConvert.SerializeObject(Creat));
            File.WriteAllText("Сategory.json", JsonConvert.SerializeObject(Cat));
        }

        public static void Drop()
        {

        }

    }
}
