public class Listing : BaseActivity
{
    private List<string> _prompts = new List<string>();
    private List<string> _responses = new List<string>();

    private Random _random = new Random();

    private string GetRandomPrompt()
    {
        return _prompts[_random.Next(_prompts.Count)];
    }

    private string DisplayPrompt()
    {
        string prompt = GetRandomPrompt();
        return prompt;
    }

    public Listing()
    {
        _activityName = "Listing";
        _activityDescription = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";

        //List of Prompts
        _prompts.Add("Who are people that you appreciate?");
        _prompts.Add("What are personal strengths of yours?");
        _prompts.Add("Who are people that you have helped this week?");
        _prompts.Add("When have you felt the Holy Ghost this month?");
        _prompts.Add("Who are some of your personal heroes?");
        _prompts.Add("What made you smile today?");
        _prompts.Add("What relationships needs attention in my life?");
        _prompts.Add("What are your greatest skills?");
        _prompts.Add("What are you most afraid of losing?");
    }

    public int GetDuration()
    {
        int duration = _activityDuration;
        return duration;
    }

    public void Run()
    {
        DisplayStart();

        DisplayGetReady();

        // Display random prompt
        Console.WriteLine("\nList as many responses you can to the following prompt:");
        Console.WriteLine($"--- {DisplayPrompt().ToUpper()} ---\n");

        //Start Timer
        Console.Write("You may begin in: ");
        CountDown(5);
        Console.WriteLine();

        DateTime endTime = DateTime.Now.AddSeconds(_activityDuration);
        while (DateTime.Now < endTime)
        {
            // Save user's response into list 
            Console.Write("> ");
            string response = Console.ReadLine();
            _responses.Add(response);   
        }
        
        // When time runs out, display number of items in list 
        Console.WriteLine($"You listed {_responses.Count} items!");


        DisplayEnd();
    }
}