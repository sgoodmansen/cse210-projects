using System;
using System.Collections.Generic;
using System.Runtime;
using System.IO;

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int _score = 0;

    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            int choice = DisplayMenu();
            Console.ResetColor();

            switch (choice)
            {
                case 1:  //Create New Goal
                    {
                        int goaltype = DisplaySubmenu();
                        Console.ResetColor();

                        switch (goaltype)
                        {
                            case 1:  //Simple Goal
                                {
                                    GoalInfo info = GetGoalInfo();
                                    goals.Add(new SimpleGoal(info.Name, info.Description, info.Points));

                                    break;
                                }

                            case 2:  //Eternal Goal
                                {
                                    GoalInfo info = GetGoalInfo();
                                    goals.Add(new EternalGoal(info.Name, info.Description, info.Points));

                                    break;
                                }

                            case 3:  //Checklist Goal
                                {
                                    GoalInfo info = GetGoalInfo();

                                    int bonusTarget = GetPositiveInteger("How many times does this goal need to be accomplished for a bonus? ");
                                    int bonusPoints = GetPositiveInteger("What is the bonus for accomplishing it the many times? ");

                                    goals.Add(new ChecklistGoal(info.Name, info.Description, info.Points, bonusTarget, bonusPoints));
                                    break;
                                }
                            
                            default:
                                {
                                    Console.WriteLine("Invalid Choice");
                                    break;
                                }
                        }

                        break;    
                    }
                    
                case 2:  //List Goals
                    {
                        DisplayGoals(goals);
                        DisplayScore();
                        Pause();
                        break;
                    }
                    
                case 3:  //Save Goals to file
                    {
                        WriteToFile(goals); 
                        Pause();  
                        break;    
                    }
                    
                case 4:  //Load Goals from file
                    {
                        ReadFromFile();
                        Pause();
                        break;    
                    }

                case 5:  //Record Event
                    {
                        RecordGoal();
                        Pause(); 
                        break;
                    }

                case 6: //Delete Goal
                    {
                        DeleteGoal();
                        Pause(); 
                        break;
                    }

                case 7: //Quit Program
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Goals will be lost unless you save them.");
                        string answer = GetRequiredText("Do you need to save goals before quitting? (y/n) ").ToLower();
                        Console.ResetColor();

                        if (answer == "n")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Thanks for using the Goal Program. Good-bye.\n");
                            running = false;
                            Console.ResetColor();
                        }
                        else
                        {
                            running = true;
                        } 
                        
                        break;
                    }
                    
                default:  //Invalid Choice
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Invalid Choice");
                        Console.ResetColor();
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
        Console.WriteLine("  1. Create New Goal");
        Console.WriteLine("  2. List Goals");
        Console.WriteLine("  3. Save Goals");
        Console.WriteLine("  4. Load Goals");
        Console.WriteLine("  5. Record Event");
        Console.WriteLine("  6. Delete Goal");
        Console.WriteLine("  7. Quit");

        Console.ForegroundColor = ConsoleColor.Cyan;
        return GetPositiveInteger("\nSelect a choice from the menu: ", 1, 7);
    }

    static int DisplaySubmenu()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Types of Goals:");
        Console.WriteLine("  1. Simple Goal"); 
        Console.WriteLine("  2. Eternal Goal"); 
        Console.WriteLine("  3. Checklist Goal");

        Console.ForegroundColor = ConsoleColor.Cyan;
        return GetPositiveInteger("\nWhich type of goal would you like to create? ", 1, 3);
    }

    static GoalInfo GetGoalInfo()
    {
        GoalInfo info = new GoalInfo();

        info.Name = GetRequiredText("What is the name of your goal? ");
        info.Description = GetRequiredText("What is a short description of it? ");
        info.Points = GetPositiveInteger("What is the amount of points associated with this goal? ");

        return info;
    }

    static void DisplayGoals(List<Goal> goals)
    {
        if (goals.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("There are no goals. Now would be a good time to write one.");
            Console.ResetColor();
            return;
        }

        Console.Clear();
        Console.WriteLine("The goals are:");
        int i = 1;

        foreach (Goal goal in goals)
        {
            Console.Write ($"  {i}. ");
            Console.WriteLine(goal.GetGoalDetails());
            i++;
        }      

    }

    static void RecordGoal()
    {
        if (goals.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("There are no goals to record.");
            Console.ResetColor();
            return;
        }

        Console.Clear();
        Console.WriteLine("The goals are:");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"  {i+1}. {goals[i].GetGoalName()}");
        } 

        Console.ForegroundColor = ConsoleColor.Cyan;
        int choice = GetPositiveInteger("\nWhich goal did you accomplish? ", 1, goals.Count);
        Console.ResetColor();

        int index = choice -1;

        if (index >= 0 && index < goals.Count)
        {
            int pointsEarned = goals[index].RecordGoal();
            _score += pointsEarned;

            RewardMessage reward = new RewardMessage();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{reward.GetRandomMessage()} {pointsEarned} points!");
            Console.ResetColor();

            DisplayScore();
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
          
        
    }

    static void DeleteGoal()
    {
        if (goals.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("There are no goals to delete.");
            Console.ResetColor();
            return;
        }
        
        Console.Clear();
        Console.WriteLine("The goals are:");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"  {i+1}. {goals[i].GetGoalName()}");
        } 

        int choice = GetPositiveInteger("\nWhich goal would you like to delete? ", 1, goals.Count);

        int index = choice -1;

        if (index >= 0 && index < goals.Count)
        {
            string goalname = goals[index].GetGoalName();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Are you sure you want to delete '{goalname}' (y/n)? ");
            string answer = Console.ReadLine().ToLower();
            Console.ResetColor();

            if (answer == "y")
            {
                goals.RemoveAt(index);
                Console.WriteLine($"'{goalname}' has been deleted.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Delete cancelled.");
                Console.ResetColor();
            }           
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Invalid goal selection.");
            Console.ResetColor();
        }
    }

    static void DisplayScore()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nYou have {_score} points");
        Console.ResetColor();
    }

    static void WriteToFile(List<Goal> goals)
    {
        if (goals.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("There are no goals to save.");
            Console.ResetColor();
            return;
        }

        string filename = GetRequiredText("What is the filename for the goal file? ");

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            // Write the Current Total Score
            outputFile.WriteLine (_score);

            // Write each Goal with following format
                // SimpleGoal | Name | Description | Points | IsComplete
                // EternalGoal | Name | Description | Points 
                // ChecklistGoal | Name | Description | Points | BonusTarget | BonusPoints | BonusCompleted
            foreach (Goal goal in goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }

        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{goals.Count} Goals saved successfully to {filename}");   
        Console.ResetColor(); 
    }

    static void ReadFromFile()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {filename} was not found.");
            Console.ResetColor();
            return;
        }

        string[] lines = System.IO.File.ReadAllLines(filename);

        _score = int.Parse(lines[0]);

        goals.Clear();  //remove any existing goals before loading new goals

        for(int i = 1; i< lines.Length; i++)
        {
            string[] parts = lines[i].Split("|");

            string goaltype = parts[0];
            string goalname = parts[1];
            string goaldesc = parts[2];
            int goalpts = int.Parse(parts[3]);

            switch (goaltype)
            {
                case "SimpleGoal":
                    {
                        bool completed = bool.Parse(parts[4]); 
                        SimpleGoal goal = new SimpleGoal(goalname,goaldesc,goalpts);
                        goal.SetComplete(completed);

                        goals.Add(goal);

                        break;   
                    }

                case "EternalGoal":
                    {
                        EternalGoal goal = new EternalGoal(goalname, goaldesc, goalpts);

                        goals.Add(goal);
                        break; 
                    }

                case "ChecklistGoal":
                    {
                        int bonusTarget = int.Parse(parts[4]);
                        int bonusPoints = int.Parse(parts[5]);
                        int bonusCompleted = int.Parse(parts[6]);
                        
                        ChecklistGoal goal = new ChecklistGoal(goalname, goaldesc, goalpts, bonusTarget, bonusPoints);
                        goal.SetAmountCompleted(bonusCompleted);

                        goals.Add(goal);
                        break; 
                    }
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{goals.Count} Goals successfully loaded from {filename}");
        Console.ResetColor();
        DisplayScore();      
    }

    static void Pause()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\nPress Enter to continue.");
        Console.ReadLine(); 
        Console.ResetColor();
    }

    static int GetPositiveInteger(string prompt)
    {
        int value;

        while (true)
        {
            Console.Write(prompt);

            if (int.TryParse(Console.ReadLine(),out value) && value > 0)
            {
                return value;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please enter a positive whole number");
            Console.ResetColor();
        }
    }

    static int GetPositiveInteger(string prompt, int min, int max)
{
    int value;

    while (true)
    {
        Console.Write(prompt);

        if (int.TryParse(Console.ReadLine(), out value) &&
            value >= min && value <= max)
        {
            return value;
        }

        Console.WriteLine($"Please enter a number between {min} and {max}.");
    }
}

    static string GetRequiredText(string prompt)
    {
        string value;

        do
        {
            Console.Write(prompt);
            value = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(value))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("This field cannot be blank.");
                Console.ResetColor();
            }
        } while (string.IsNullOrEmpty(value));

        return value;
    }
}