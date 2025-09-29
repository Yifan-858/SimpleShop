using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Customers
{
    public class GoldCustomer : Customer
    {
        public string Level { get; private set; }
        public GoldCustomer(string name, string password) : base(name, password)
        {
            Level = "Gold";
        }
    }
}
