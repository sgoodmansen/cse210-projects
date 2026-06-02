using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Journal Program!");

        Journal myJournal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        bool running = true;

        while (running) 
        {
            int choice = DisplayMenu();

            switch (choice)
            {
                case 1:
                    {
                        string prompt = promptGenerator.GetRandomPrompt();

                        Console.WriteLine(prompt);
                        Console.Write("> ");
                        string response = Console.ReadLine();

                        Entry newEntry = new Entry();
                        newEntry._entryDate = DateTime.Now.ToShortDateString();
                        newEntry._prompt = prompt;
                        newEntry._response = response;

                        myJournal.AddEntry(newEntry);
                        Console.WriteLine ($"Entry Saved. Total Journal Entries: {myJournal.GetCount()}");

                    }
                    break;
                case 2:
                    {
                        myJournal.DisplayEntries();
                        Console.WriteLine($"Total Journal Entries: {myJournal.GetCount()}");
                    }
                    break;
                case 3:
                    {
                        Console.Write("Enter filename: ");
                        string filename = Console.ReadLine();

                        myJournal.LoadFromFile(filename);

                        Console.WriteLine("Journal loaded.");
                    }
                    break;
                case 4:
                    {
                        Console.Write("Enter filename: ");
                        string filename = Console.ReadLine();

                        myJournal.SaveToFile(filename);
                        Console.WriteLine($"Journal saved. You now have {myJournal.GetCount()} entries."); 
                    }
                    break;
                case 5:
                    {
                        Console.WriteLine("Thanks for using the Journal program");
                        running = false;
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Invalid Choice - Please select a number between 1 - 5");
                        break;
                    }
            }

        }
    }

    static int DisplayMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Please select one of the folowing choices:");
        Console.WriteLine("1. Write an Entry");
        Console.WriteLine("2. Display all Entries");
        Console.WriteLine("3. Load all Entries");
        Console.WriteLine("4. Save all Entries");
        Console.WriteLine("5. Quit Journal");
        Console.Write("What would you like to do? ");
        // Console.Write("> ");
        string userChoice = Console.ReadLine();
        return int.Parse(userChoice);
    }

}