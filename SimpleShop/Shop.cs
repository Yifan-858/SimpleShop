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
        public List<Product> Inventory { get; private set; }
        public List<Customer> Customers { get; private set; }

        public Shop()
        {
            Inventory = new List<Product> ();
            Customers = new List<Customer>
            {
                new Customer("Knatte", "123"),
                new Customer("Fnatte", "321"),
                new Customer("Tjatte", "213")
            };
        }

        public void AddToInventory(Product p)
        {
            Inventory.Add(p);
        }

        public void DisplayInventory()
        {
            foreach (Product product in Inventory)
            {
                Console.WriteLine(product.ToString());
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
                            ProductMenu(currentUser);
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

        public void ProductMenu(Customer currentUser)
        {
            bool inProductMenu = true;

            while (inProductMenu)
            {
                List<string> productMenuTitle = ["^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^", "       Fresh food from Farmer Market", "^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^",$"Dear {currentUser.Name}, what would like to buy today >> "];
                List<string> productOptions = new List<string>();

                //List out the available products
                foreach(Product p in Inventory)
                {
                    string pInfo = p.ToString();
                    productOptions.Add(pInfo);
                }

                //add option"Shopping cart" and "Logout" in the end of the menu
                productOptions.Add("To Shopping Cart");
                productOptions.Add("Logout");

                Menu productMenu = new Menu(productOptions,productMenuTitle);
                
                int userChoice = productMenu.ControlSelect();

                //To shopping cart
                if(userChoice == productOptions.Count - 2)
                {
                    ShowCart(currentUser);
                    Console.WriteLine("Press any key to go back.");
                    Console.ReadKey();
                }
                else if (userChoice == productOptions.Count - 1)//logout
                {
                    inProductMenu = false;
                }
                else
                {
                    Product selectedItem = Inventory[userChoice];
                    currentUser.Cart.Add(selectedItem);// add products in the cart honey egg egg honey milk rearrange!
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{selectedItem.Name} has been added to your cart!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Press any key to continue shopping.");
                    Console.ReadKey();  
                }   
            }

        }

        public void ShowCart(Customer currentUser)
        {
            Console.Clear();
            Console.WriteLine($"=== {currentUser.Name}'s shopping cart ===");
            double totalPrice = 0.0;

            //regroup the in-cart products by name, each group object contains one key and one Product object
            var productGroupedByName = currentUser.Cart.GroupBy(product => product.Name);
            //then rearrange them into an new object (Tuple in c#) of name:, count:, totalPrice:
            List<(string Name, int Count, double TotalPrice)> reselectedProductInfo = productGroupedByName.Select(groupObject => ( Name: groupObject.Key, Count: groupObject.Count(), TotalPrice: groupObject.Sum(product => product.Price))).ToList();

            if(reselectedProductInfo.Count > 0)
            {
                for (int i = 0; i < reselectedProductInfo.Count; i++)
                {
                    Console.WriteLine($"{i+1}. {reselectedProductInfo[i].Name} | Amount: {reselectedProductInfo[i].Count} | Price:{reselectedProductInfo[i].TotalPrice}");
                }
            }
            else
            {
                Console.WriteLine("Your shopping cart is empty.");
            }

        }

    } 
}
