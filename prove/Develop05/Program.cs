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

            switch (choice)
            {
                case 1:  //Create New Goal
                    {
                        int goaltype = DisplaySubmenu();

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
                                    Console.WriteLine("Invalid Choice - Please select a number between 1 - 3");
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
                        Console.WriteLine($"Goals will be lost unless you saved them.");
                        Console.Write($"Do you need to save goals before quitting? (y/n) ");
                        string answer = Console.ReadLine().ToLower();

                        if (answer == "y")
                        {
                            running = true;
                        }
                        else
                        {
                            Console.WriteLine($"Thanks for using the Goal Program. Good-bye.\n");
                            running = false;
                        } 
                        
                        break;
                    }
                    
                default:  //Invalid Choice
                    {
                        Console.WriteLine("Invalid Choice - Please select a number between 1 - 6");
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
        Console.Write("Select a choice from the menu: ");
        string userChoice = Console.ReadLine();
        return int.Parse(userChoice);

    }

    static int DisplaySubmenu()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Types of Goals:");
        Console.WriteLine("  1. Simple Goal"); 
        Console.WriteLine("  2. Eternal Goal"); 
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string userChoice = Console.ReadLine();
        return int.Parse(userChoice); 
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
            Console.WriteLine("There are no goals. Now would be a good time to write one.");
            return;
        }

        Console.Clear();
        Console.WriteLine("The goals are:");
        int i = 1;

        foreach (Goal goal in goals)
        {
            Console.Write ($"{i}. ");
            Console.WriteLine(goal.GetGoalDetails());
            i++;
        }      

    }

    static void RecordGoal()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("There are no goals to record.");
            return;
        }

        Console.Clear();
        Console.WriteLine("The goals are:");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i+1}. {goals[i].GetGoalName()}");
        } 

        Console.Write("\nWhich goal did you accomplish? ");
        int choice = int.Parse(Console.ReadLine());
        int index = choice -1;

        if (index >= 0 && index < goals.Count)
        {
            int pointsEarned = goals[index].RecordGoal();
            _score += pointsEarned;

            RewardMessage reward = new RewardMessage();
            Console.WriteLine($"{reward.GetRandomMessage()} {pointsEarned} points!");
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
            Console.WriteLine("There are no goals to delete.");
            return;
        }
        
        Console.Clear();
        Console.WriteLine("The goals are:");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i+1}. {goals[i].GetGoalName()}");
        } 

        Console.Write("\nWhich goal would you like to delete? ");
        int choice = int.Parse(Console.ReadLine());

        int index = choice -1;

        if (index >= 0 && index < goals.Count)
        {
            string goalname = goals[index].GetGoalName();

            Console.Write($"Are you sure you want to delete '{goalname}' (y/n)? ");
            string answer = Console.ReadLine().ToLower();

            if (answer == "y")
            {
                goals.RemoveAt(index);
                Console.WriteLine($"'{goalname}' has been deleted.");
            }
            else
            {
                Console.WriteLine("Delete cancelled.");
            }           
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    static void DisplayScore()
    {
        Console.WriteLine($"\nYou have {_score} points");
    }

    static void WriteToFile(List<Goal> goals)
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("There are no goals to save.");
            return;
        }

        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

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

        Console.WriteLine($"{goals.Count} Goals saved successfully to {filename}");    
    }

    static void ReadFromFile()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine($"Error: {filename} was not found.");
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
        Console.WriteLine($"\n{goals.Count} Goals successfully loaded from {filename}");
        DisplayScore();      
    }

    static void Pause()
    {
        Console.Write("\nPress Enter to continue.");
        Console.ReadLine(); 
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

            Console.WriteLine("Please enter a positive whole number");
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
                Console.WriteLine("This field cannot be blank.");
            }
        } while (string.IsNullOrEmpty(value));

        return value;
    }
}