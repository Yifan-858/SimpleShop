using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Manager
{
    public class Menu
    {
        public List<string> MenuOptions { get; private set; }
        public List<string> MenuTitle { get; private set; }
        public int IndexSelected = 0;

        public Menu(List<string> menuOptions, List<string> menuTitle)
        {
            MenuOptions = menuOptions;
            MenuTitle = menuTitle;
        }

        public void DisplayTitle()
        {
            foreach(string s in MenuTitle)
            {
                Console.WriteLine(s);
            }
        }

        public void DisplayMenu()
        {
            string pointer;

            for (int i = 0; i < MenuOptions.Count; i++)
            {
                if( i == IndexSelected)
                {
                    pointer = ">";
                }
                else
                {
                    pointer = " ";
                }

                Console.WriteLine($"{pointer} {MenuOptions[i]}");
            }
        }

        public int ControlSelect()
        {
            ConsoleKey userInput;

            do
            {
                Console.Clear();
                DisplayTitle();
                Console.WriteLine();
                DisplayMenu();
                userInput = Console.ReadKey(true).Key;

                if(userInput == ConsoleKey.UpArrow)
                {
                    IndexSelected--;

                    if(IndexSelected == -1)
                    {
                        IndexSelected = MenuOptions.Count - 1;
                    }
                }
                else if(userInput == ConsoleKey.DownArrow)
                {
                    IndexSelected++;

                    if(IndexSelected == MenuOptions.Count)
                    {
                        IndexSelected = 0;
                    }
                }

            } while (userInput != ConsoleKey.Enter);

            return IndexSelected;
        }
    }
}
