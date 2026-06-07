using System.Diagnostics;


public class BaseActivity{
    protected string _activityName;
    protected string _activityDescription;
    protected int _activityDuration;

    public void DisplayStart()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_activityName} Activity");
        Console.WriteLine();
        Console.WriteLine(_activityDescription);
        Console.WriteLine();

        Console.Write("How long, in seconds, would you like for your session? ");
        string input = Console.ReadLine();

        while (!int.TryParse(input, out _activityDuration))          //Loop that checks that user inputs a number instead of letter
        {
           Console.Write("Invalid Input. Please enter a number: ");
           input = Console.ReadLine(); 
        }

        if (_activityName == "Breathing")
        {
            double number = _activityDuration;
            double roundDown10 = Math.Floor(number / 10.0) * 10;
            _activityDuration = (int)roundDown10; 
            Console.WriteLine("\nAdjusting your session time to fit the breathing cycles.");
            Console.WriteLine($"Your breathing session will be {_activityDuration} seconds\n"); 
            Console.Write("You will begin in: ");
            CountDown(5);
        }

    }

    public void DisplayEnd()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        Spinner(3);

        Console.WriteLine($"You have completed {_activityDuration} seconds of the {_activityName} Activity");
        Spinner(3);
    }

    public void DisplayGetReady()
    {
        Console.Clear();
        Console.WriteLine("Get Ready...");
        Spinner(3);
    }

    public void CountDown(int seconds)
    {
        for (int i=seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    public void LaunchCountDown(int seconds)
    {
        for (int i=seconds; i > 0; i--)
        {
            Console.Write(i);
            for (int d=0; d < 4; d++)
            {
                Thread.Sleep(250);
                Console.Write(".");    
            }
        }
    }

    public void CountDots(int seconds)
    {
        for (int i=seconds * 2; i > 0; i--)
        {
            Console.Write(".");
            Thread.Sleep(500);
        }   
        Console.WriteLine();
    }

    public void Spinner(int seconds)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(spinner[i]);
            Thread.Sleep(250);
            Console.Write("\b");

            i = (i+1) % spinner.Length;                //resets to 0 when it reached the end of the array
        }
        Console.Write(" \b");
    }
}