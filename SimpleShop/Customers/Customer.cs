using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShop.Products;

namespace SimpleShop.Customers
{
    public abstract class Customer
    {
        public string Name { get; private set; }
        private string Password { get; set; }
        public double Discount { get; private set; } 
        public string Level { get; private set; }

        private List<Product> cart;
        public List<Product> Cart { get { return cart; } }

        public Customer(string name, string password, double discount, string level)
        {
            Name = name;
            Password = password;
            Discount = discount;
            Level = level;
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

            return $"Name: {Name}, Password: {Password}, Level: {Level}, Discount: {Discount}, Cart: {productInfo}";
        }

        public bool VerifyPassword(string password)
        {
            return password == Password;
        } 

        public string GetPassword()
        {
            return Password;
        }
    }
}
