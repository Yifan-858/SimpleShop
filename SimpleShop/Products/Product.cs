using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Products
{
    public abstract class Product
    {
        public string Name { get; private set;}
        public double Price { get; private set;}

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
           
        }

        public override string ToString()
        {
            return $"{Name,-6}    Price: {Price} kr";
        }

        public abstract void IncrementAmount();
        //public void applydiscount(cutomer,,,level)
    }
}
