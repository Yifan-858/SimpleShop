using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private double currencySelected = 1;
        private string currencySign = "kr";

        public Shop()
        {
            Inventory = new List<Product> ();
            Customers = CustomerManager.LoadCustomersFromFile();

            if (!Customers.Exists(c => c.Name == "Knatte"))
            Customers.Add(new GoldCustomer("Knatte", "123"));

            if (!Customers.Exists(c => c.Name == "Fnatte"))
            Customers.Add(new SilverCustomer("Fnatte", "321"));

            if (!Customers.Exists(c => c.Name == "Tjatte"))
            Customers.Add(new BronzeCustomer("Tjatte", "213"));
  
        }

        public void AddToInventory(Product p)
        {
            Inventory.Add(p);
        }

        public void DisplayInventory()
        {
            foreach (Product product in Inventory)
            {
                Console.WriteLine(product);
            }
        }

        public void DisplayCustomers()
        {
            foreach (Customer c in Customers)
            {
                Console.WriteLine(c);
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
                    //Register
                    case 0:
                        Register();
                        break;
                    //Login
                    case 1:
                        Customer? currentUser = Login();
                        if(currentUser != null)
                        {
                            ProductMenu(currentUser);
                        }
                        break;
                    //Exit
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

            Random rdn = new Random();
            int number = rdn.Next(0, 3);

            Customer newCustomer = null;


            //Assign a customer level randomly
            switch (number)
            {
                case 0:
                    newCustomer = new BronzeCustomer(userName, password);
                    Customers.Add(newCustomer);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Registered successfully! Welcome Bronze customer {userName}!");
                    break;
                case 1:
                    newCustomer = new SilverCustomer(userName, password);
                    Customers.Add(newCustomer);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Registered successfully! Welcome Silver customer {userName}!");
                    break;
                case 2:
                    newCustomer = new GoldCustomer(userName, password);
                    Customers.Add(newCustomer);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Registered successfully! Welcome Gold customer {userName}!");
                    break;
                default:
                    Console.WriteLine("Invalid value");
                    break;
            }

            CustomerManager.SaveCustomerToFile(Customers);
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

            //refactor with LINQ
            Customer existCustomer = Customers.FirstOrDefault(c => c.Name == userName);

            //Customer existCustomer = null;
            //foreach(Customer c in Customers)
            //{
            //    if(userName == c.Name)
            //    {
            //        existCustomer = c;
            //        break;
            //    }
            //}

            if(existCustomer == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You seem to be a new customer. Do you want to register?");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press any key to proceed.");
                Console.ReadKey();
                return null;
            }
            else if(existCustomer != null && !existCustomer.VerifyPassword(password))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The password is invalid. Please try again.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press any key to proceed.");
                Console.ReadKey();
                return null;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Logged in! Welcome {existCustomer.Level} Customer {existCustomer.Name}!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to proceed.");
            Console.ReadKey();
            return existCustomer;
        }

        public void ProductMenu(Customer currentUser)
        {
            bool inProductMenu = true;

            while (inProductMenu)
            {
                List<string> productMenuTitle = ["^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^", "       Fresh food from Farmer Market", "^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^",$"Dear {currentUser.Level} Customer {currentUser.Name}, what would like to buy today >> "];
                List<string> productOptions = new List<string>();

                //List out the available products
                foreach(Product p in Inventory)
                {
                    double currencyPrice = Math.Round(p.Price * currencySelected, 1);
                    double discountPrice = Math.Round(currencyPrice * (1-currentUser.Discount), 1);
                    double discoutDisplay = currentUser.Discount * 100;
                    string pInfo =$"{p.Name,-6}    Regular Price: {currencyPrice}{currencySign} --{discoutDisplay}% off--      Your Price: {discountPrice}{currencySign}";
                    productOptions.Add(pInfo);
                }

                //add option "Shopping cart" and "Logout" in the end of the menu
                productOptions.Add("View price in sek");
                productOptions.Add("View price in eur");
                productOptions.Add("View price in rmb");
                productOptions.Add("To Shopping Cart");
                productOptions.Add("Check Out");
                productOptions.Add("Logout");

                Menu productMenu = new Menu(productOptions,productMenuTitle);
                
                int userChoice = productMenu.ControlSelect();

                if(userChoice == productOptions.Count - 6)//currency sek
                {
                    currencySelected = ApplyCurrency("sek");
                    currencySign = AddCurrencySign("sek");
                }
                else if(userChoice == productOptions.Count - 5)//currency eur
                {
                    currencySelected = ApplyCurrency("eur");
                    currencySign = AddCurrencySign("eur");
                }
                else if(userChoice == productOptions.Count - 4)//currency rmb
                {
                    currencySelected = ApplyCurrency("rmb");
                    currencySign = AddCurrencySign("rmb");
                }
                else if(userChoice == productOptions.Count - 3)//To shopping cart
                {
                    ShowCart(currentUser);
                    Console.WriteLine("Press any key to go back.");
                    Console.ReadKey();
                }
                else if(userChoice == productOptions.Count - 2)//checout
                {
                    CheckOut(currentUser);
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
                    Console.WriteLine();
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
            Console.WriteLine();
            double totalPrice = 0.0;
            double applyDiscount = 1 - currentUser.Discount;


            //regroup the in-cart products by name, each group object contains one key and one Product object
            var productGroupedByName = currentUser.Cart.GroupBy(product => product.Name);

            //then rearrange them into an new object (Tuple in c#) of name:, count:, unitPrice:, totalPrice:
            List<(string Name, int Count, double UnitPrice, double TotalPrice)> reselectedProductInfo = productGroupedByName
                .Select(groupObject => ( 
                Name: groupObject.Key, 
                Count: groupObject.Count(), 
                UnitPrice: Math.Round(groupObject.First().Price*applyDiscount*currencySelected, 1), 
                TotalPrice: Math.Round(groupObject.Sum(product => product.Price)*applyDiscount*currencySelected , 1)
                )).ToList();

            if(reselectedProductInfo.Count > 0)
            {
                for (int i = 0; i < reselectedProductInfo.Count; i++)
                {
                    Console.WriteLine($"{i+1}. {reselectedProductInfo[i].Name} | Quantity: {reselectedProductInfo[i].Count} | Unit Price:{reselectedProductInfo[i].UnitPrice}{currencySign} | Total Price:{reselectedProductInfo[i].TotalPrice}{currencySign}");
                }

                Console.WriteLine();
                double cartTotalPrice = currentUser.Cart.Sum(product => product.Price)*applyDiscount*currencySelected;
                Console.WriteLine($"TotalPrice: --{currentUser.Discount*100}% off-- {cartTotalPrice}{currencySign}");
            }
            else
            {
                Console.WriteLine("Your shopping cart is empty.");
            }

        }

        public void CheckOut(Customer currentUser)
        {
            if (currentUser.Cart.Count > 0)
            {
                double payment = currentUser.Cart.Sum(product => product.Price)*(1 - currentUser.Discount)*currencySelected;
                currentUser.Cart.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Recieved payment: {payment}{currencySign}.");
                Console.WriteLine("Thank you for ordering! ");
                Console.WriteLine("Your package is being processed!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press any key to continue shopping.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Your shopping cart is empty. Please add more product.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press any key to continue shopping.");
                Console.ReadKey();
            }
        }

        private double ApplyCurrency(string currency)
        {
            switch (currency)
            {
                case "sek":
                    return 1;
                case "eur":
                    return 0.9;
                case "rmb":
                    return 0.75;
                default:
                    return 1;
            }
        }

        private string AddCurrencySign(string currency)
        {
            switch (currency)
            {
                case "sek":
                    return "kr";
                case "eur":
                    return "eur";
                case "rmb":
                    return "¥";
                default:
                    return "kr";
            }
        }



    } 
}
