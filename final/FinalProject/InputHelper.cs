public class InputHelper {
    public static int GetIntegerInRange(string prompt)
    {
        int value;

        while (true)
        {
            Console.Write(prompt);
            
            if (int.TryParse(Console.ReadLine(),out value)&& value > 0)
            {
                return value;
            }

            Console.WriteLine("Please enter a positive number");
        }
    }

    public static int GetIntegerInRange(string prompt, int min, int max)
    {
        int value;

        while (true)
        {
            Console.Write(prompt);

            if(int.TryParse(Console.ReadLine(),out value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Please enter a number between {min} and {max}");
        }
    }

    public static double GetPositiveDouble(string prompt)
    {
        double value;
        
        while (true)
        {
            Console.Write(prompt);

            if (double.TryParse(Console.ReadLine(), out value)  && value > 0)
            {
                return value;
            }

            Console.WriteLine("Please enter a positive number");
        }
    }

    public static string GetRequiredText(string prompt)
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

    public static bool GetBoolEntry(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string answer = Console.ReadLine().ToLower();
            
            if (answer == "y")
            {
                return true;
            } 
            
            if (answer == "n")
            {
                return false;
            }

            Console.WriteLine("Please enter Y or N");
        }
    }
    public static PrinterType GetPrinterType()
    {
        Console.WriteLine("\nSelect Printer Type");
        Console.WriteLine(" 1. Laser");
        Console.WriteLine(" 2. Inkjet");
        Console.WriteLine(" 3. Thermal");
        Console.WriteLine(" 4. Label");
        Console.WriteLine(" 5. Multifunction");

        int choice = GetIntegerInRange("Choice: ", 1, 5);

        switch (choice)
        {
            case 1:
                return PrinterType.Laser;
            case 2:
                return PrinterType.Inkjet;
            case 3:
                return PrinterType.Thermal;
            case 4:
                return PrinterType.Label;
            case 5:
                return PrinterType.Multifunction;
            default:
                return PrinterType.Laser;
        }
    }
    public static EmployeeStatus GetEmployeeStatus()
    {
        Console.WriteLine("\nEmployee Status");
        Console.WriteLine(" 1. Active");
        Console.WriteLine(" 2. Inactive");
        Console.WriteLine(" 3. Terminated");

        int choice = InputHelper.GetIntegerInRange("\nSelect a status: ", 1, 3);

        switch (choice)
        {
            case 1:
                return EmployeeStatus.Active;

            case 2:
                return EmployeeStatus.Inactive;

            case 3:
                return EmployeeStatus.Terminated;

            default:
                return EmployeeStatus.Active;
        }
    }

    public static void DisplayError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void DisplayWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();    
    }

    public static void DisplaySuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();    
    }

    public static void DisplayHeader(string title)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine();
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
        Console.ResetColor();
    }

    public static void Pause()
    {
        Console.Write("\nPress Enter to continue.");
        Console.ReadLine(); 
    }
}