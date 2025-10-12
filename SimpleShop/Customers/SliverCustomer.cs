using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Customers
{
    public class SilverCustomer : Customer
    {
        public SilverCustomer(string name, string password) : base(name, password,0.1, "Sliver")
        {
        }
    }
}
