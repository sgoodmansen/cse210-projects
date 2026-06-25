using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            int choice = DisplayMenu();

            switch (choice)
            {
                case 1:  //Breathing Activity
                    {
                        Breathing breathing = new Breathing();
                        breathing.Run();
                        break;    
                    }
                    
                case 2:  //Reflecting Activity
                    {
                        Reflecting reflecting = new Reflecting();
                        reflecting.Run();
                        break;
                    }
                    
                case 3:  //Listing Activity
                    {
                        break;    
                    }
                    
                case 4:  //Quit
                    {
                        Console.WriteLine("Thanks for using the Mindfulness program");
                        running = false;
                        break;    
                    }
                    
                default:  //Invalid Choice
                    {
                        Console.WriteLine("Invalid Choice - Please select a number between 1 - 4");
                        break;    
                    }      
            }
        }
    }

    static int DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Menu Options");
        Console.WriteLine("  1. Start breathing activity");
        Console.WriteLine("  2. Start reflecting activity");
        Console.WriteLine("  3. Start listing activity");
        Console.WriteLine("  4. Quit");
        Console.Write("Select a choice from the menu: ");
        string userChoice = Console.ReadLine();
        return int.Parse(userChoice);

    }
}