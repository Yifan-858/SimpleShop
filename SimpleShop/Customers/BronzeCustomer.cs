using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Customers
{
    public class BronzeCustomer : Customer
    {
        public BronzeCustomer(string name, string password) : base(name, password, 0.05, "Bronze")
        {
        }
    }
}
