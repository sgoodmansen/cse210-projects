using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        bool running = true;

        int breathingCount = 0;
        int breathingTime = 0;
        int reflectingCount = 0;
        int reflectingTime = 0;
        int listingCount = 0;
        int listingTime = 0;

        while (running)
        {
            int choice = DisplayMenu();

            switch (choice)
            {
                case 1:  //Breathing Activity
                    {
                        Breathing breathing = new Breathing();
                        breathing.Run();

                        breathingCount++;
                        breathingTime += breathing.GetDuration();
                        break;    
                    }
                    
                case 2:  //Reflecting Activity
                    {
                        Reflecting reflecting = new Reflecting();
                        reflecting.Run();

                        reflectingCount++;
                        reflectingTime += reflecting.GetDuration();
                        break;
                    }
                    
                case 3:  //Listing Activity
                    {
                        Listing listing = new Listing();
                        listing.Run();

                        listingCount++;
                        listingTime += listing.GetDuration();
                        break;    
                    }

                case 4:  //Display Statistics
                    {
                        DisplayStatistics(
                            breathingCount,
                            breathingTime,
                            reflectingCount,
                            reflectingTime,
                            listingCount,
                            listingTime);

                        break;
                    }
                    
                case 5:  //Quit
                    {
                        Console.WriteLine("Thanks for using the Mindfulness program\n");
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
        Console.WriteLine("  4. Display statistics");
        Console.WriteLine("  5. Quit");
        Console.Write("Select a choice from the menu: ");
        string userChoice = Console.ReadLine();
        return int.Parse(userChoice);

    }

    static void DisplayStatistics(
        int breathingCount,
        int breathingTime,
        int reflectingCount,
        int reflectingTime,
        int listingCount,
        int listingTime)
    {
        int totalActivities = breathingCount + reflectingCount + listingCount;
        int totalSeconds = breathingTime + reflectingTime + listingTime;

        Console.Clear();

        Console.WriteLine("Statistics for Activities Done");
        Console.WriteLine("------------------------------");
        Console.WriteLine("ACTIVITY      TIMES    SECONDS");
        Console.WriteLine($"Breathing.......{breathingCount}..........{breathingTime}");
        Console.WriteLine($"Reflecting......{reflectingCount}..........{reflectingTime}");
        Console.WriteLine($"Listing.........{listingCount}..........{listingTime}");

        Console.WriteLine($"\nTOTAL ACTIVITIES: {totalActivities}");
        Console.WriteLine($"TOTAL SECONDS: {totalSeconds}");

        Console.WriteLine("\nPress Enter to continue...\n");
        Console.ReadLine();
    }
}