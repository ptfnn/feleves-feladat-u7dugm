using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace feleves_feladat
{
    public class Shop
    {
        public enum ShopType
        {
            Hygene,
            Games,
            Food,
            Drinks
        }
        public string Name {  get; set; }
        public ShopType Type { get; set; }
        public int Price { get; set; }
        public Shop(string name, ShopType type, int price)
        {
            Name = name;
            Type = type;
            Price = price;
        }
    }
}
