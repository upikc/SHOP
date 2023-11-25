using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHOP 
{
    public class Product_class
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Category_ID { get; set; }
        public int Creator_ID { get; set; }
        public int Count { get; set; }
        public int Prise { get; set; }
        public string Images { get; set; }

        public int Sold { get; set; }
    }



    public class Сategory_class
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }



    public class Creator_class
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }

}
