using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Products
{
    public class Milk : Product
    {
        public static int TotalAmount { get; private set; } = 0;
        public Milk(string name, double price) : base(name, price)
        {
        }
    }
}
