using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Products
{
    public class Honey : Product
    {
        public int TotalCount { get; private set; }
        public Honey(string name, double price) : base(name, price)
        {
            TotalCount++;
        }
    }
}
