using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Products
{
    public class Egg : Product
    {
        public int TotalCount { get; private set; }
        public Egg(string name, double price) : base(name, price)
        {
            TotalCount++;
        }
    }
}
