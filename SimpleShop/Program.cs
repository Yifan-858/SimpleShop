using SimpleShop.Products;

namespace SimpleShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shop farmerMarket = new Shop();

            Product milk = new Milk("Milk", 30);
            Product honey = new Honey("Honey", 120);
            Product egg = new Egg("Egg",50);

            farmerMarket.AddToInventory(milk);
            farmerMarket.AddToInventory(honey);
            farmerMarket.AddToInventory(egg);

            farmerMarket.WelcomeMenu();
        }
    }
}
