using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShop.Customers;

namespace SimpleShop.Manager
{
    public static class CustomerManager
    {
        private static string filePath = Path.Combine("Data", "customers.txt");

        public static void SaveCustomerToFile(List<Customer> customers)
        {
            Directory.CreateDirectory("Data");//for bin/Debug/net8.0 ??
            List<string> lines = new List<string>();

            foreach(var c in customers)
            {
                lines.Add($"{c.Name};{c.GetPassword()};{c.Discount};{c.Level}");
            }

            File.WriteAllLines(filePath, lines);
            Console.WriteLine("Customers saved successfully!");
        }

        public static List<Customer> LoadCustomersFromFile()
        {
            List<Customer> customers = new List<Customer>();
            if (!File.Exists(filePath))
            {
                return customers;
            }

            string[] lines = File.ReadAllLines(filePath);
            Customer loadedCustomer = null;

            foreach(string l in lines)
            {
                string[] parts = l.Split(';');
                if(parts.Length >= 4)
                {
                    string name = parts[0];
                    string password = parts[1];
                    string level = parts[3];

                    switch (level)
                    {
                        case "Bronze":
                            loadedCustomer = new BronzeCustomer(name, password);
                            break;
                        case "Silver":
                            loadedCustomer = new SilverCustomer(name, password);
                            break;
                        case "Gold":
                            loadedCustomer = new GoldCustomer(name, password);
                            break;
                    }

                    customers.Add(loadedCustomer);
                }
            }

            return customers;
        }
    }
}
