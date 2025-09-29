using SimpleShop.Products;

namespace SimpleShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shop farmerMarket = new Shop();

            Product milk = new Product("Milk", 20);
            Product honey = new Product("Honey", 120);
            Product egg = new Product("Egg", 50);

            farmerMarket.AddToInventory(milk);
            farmerMarket.AddToInventory(honey);
            farmerMarket.AddToInventory(egg);

            farmerMarket.WelcomeMenu();
        }
    }
}
