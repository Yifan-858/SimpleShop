using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShop.Products;

namespace SimpleShop.Customers
{
    public class Customer
    {
        public string Name { get; private set; }
        private string Password { get; set; }

        private List<Product> cart;
        public List<Product> Cart { get { return cart; } }

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            cart = new List<Product>();
        }

        public string ToString()
        {
            string productInfo = "";

            if(Cart.Count == 0)
            {
                productInfo = "Your shopping cart is empty.";
            }

            foreach(Product p in Cart)
            {
                productInfo += $"/{p.Name}";
            }

            return $"Name: {Name}, Password: {Password}, Cart: {productInfo}";
        }

        public bool VerifyPassword(string password)
        {
            return password == Password;
        } 

        //ShowCartInfo productname,price,amount
    }
}
