using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShop.Customers;
using SimpleShop.Manager;
using SimpleShop.Products;

namespace SimpleShop
{
    public class Shop
    {
        public List<string> Inventory { get; private set; }
        public List<Customer> Customers { get; private set; }

        public Shop()
        {
            Inventory = new List<string> { "Milk", "Honey", "Egg" };
            Customers = new List<Customer>
            {
                new Customer("Knatte", "123"),
                new Customer("Fnatte", "321"),
                new Customer("Tjatte", "213")
            };
        }

        public void DisplayInventory()
        {
            foreach (string product in Inventory)
            {
                Console.WriteLine(product);
            }
        }

        public void DisplayCustomers()
        {
            foreach (Customer c in Customers)
            {
                Console.WriteLine(c.ToString());
            }
        }
        public void WelcomeMenu()
        {
            bool inWelcomMenu = true;
            while (inWelcomMenu)
            {
                List<string> welcomeMenuTitle = ["                          _.-^-._    .--.\r\n                       .-'   _   '-. |__|\r\n                      /     |_|     \\|  |\r\n                     /               \\  |\r\n                    /|     _____     |\\ |\r\n                     |    |==|==|    |  |\r\n |---|---|---|---|---|    |--|--|    |  |\r\n |---|---|---|---|---|    |==|==|    |  |\r\n^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^", "          Welcome To Farmer Market", "^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^"];
                List<string> welcomeOptions = ["Register", "Login", "Exit"];
                Menu welcomeMenu = new Menu(welcomeOptions, welcomeMenuTitle);
                welcomeMenu.DisplayMenu();
                int userChoice = welcomeMenu.ControlSelect();

                switch (userChoice)
                {
                    case 0:
                        Register();
                        break;
                    case 1:
                        Customer? currentUser = Login();
                        if(currentUser != null)
                        {
                            Console.WriteLine("Open shopping menu");
                            Console.ReadKey();
                        }
                        break;
                    case 2:
                        Console.WriteLine();
                        Console.WriteLine("Welcome back again!");
                        inWelcomMenu = false;
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

            }
        }

        public void Register()
        {   
            Console.Clear();
            string userName = "";
            string password = "";

            while (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {   
                
                Console.WriteLine("== Register New Customer ==");
                Console.Write("Your Name: ");
                userName = Console.ReadLine();
                Console.Write("Your Password: ");
                password = Console.ReadLine();

                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Name or Password is invalid. Please enter again.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            Customer newCustomer = new Customer(userName, password);
            Customers.Add(newCustomer);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Welcome {userName}!");
            Console.WriteLine("Your new account has been registered successfully!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to proceed..");
            Console.ReadKey();
        }

        public Customer? Login()
        {
            Console.Clear();
            string userName = "";
            string password = "";

            while(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("== Login ==");
                Console.Write("User Name: ");
                userName = Console.ReadLine();
                Console.Write("Password: ");
                password = Console.ReadLine();

                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Name or Password is invalid. Please enter again.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
          
            foreach(Customer c in Customers)
            {
                if(userName == c.Name && c.VerifyPassword(password))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Logged in! Welcome {c.Name}!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Press any key to proceed.");
                    Console.ReadKey();
                    return c;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Can not find user.");
            Console.WriteLine("Please try again.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to proceed.");
            Console.ReadKey();
            return null;
        }

    } 
}
