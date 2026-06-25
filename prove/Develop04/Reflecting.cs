public class Reflecting : BaseActivity
{
    private List<string> _prompts = new List<string>();
    private List<string> _questions= new List<string>();

    private Random _random = new Random();

    private string GetRandomPrompt()
    {
        return _prompts[_random.Next(_prompts.Count)];
    }

    private string GetQuestion(List<string> tempQuestions)
    {
        int index = _random.Next(tempQuestions.Count);
        string question = tempQuestions[index];
        tempQuestions.RemoveAt(index);
        return question;
    }

    private string DisplayPrompt()
    {
        string prompt = GetRandomPrompt();
        return prompt;
    }

    public Reflecting()
    {
        _activityName = "Reflecting";
        _activityDescription = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";

        //List of Prompts
        _prompts.Add("Think of a time when you stood up for someone else.");
        _prompts.Add("Think of a time when you did something really difficult.");
        _prompts.Add("Think of a time when you helped someone in need.");
        _prompts.Add("Think of a time when you did something truly selfless.");

        //List of Follow up Questions
        _questions.Add("Why was this experience meaningful to you?");
        _questions.Add("Have you ever done anything like this before?");
        _questions.Add("How did you get started?");
        _questions.Add("How did you feel when it was complete?");
        _questions.Add("What made this time different than other times when you were not as successful?");
        _questions.Add("What is your favorite thing about this experience?");
        _questions.Add("What could you learn from this experience that applies to other situations?");
        _questions.Add("What did you learn about yourself through this experience?");
        _questions.Add("How can you keep this experience in mind in the future?");
        _questions.Add("What would you change about this experience?");
    }

    public int GetDuration()
    {
        int duration = _activityDuration;
        return duration;
    }

    public void Run()
    {
        List<string> tempQuestions = new List<string>(_questions);

        DisplayStart();

        DisplayGetReady();
        
        // Show a random prompt
        Console.WriteLine();
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine($"---- {DisplayPrompt()} ----");

        // Let the user think about it
        Console.WriteLine();
        Console.WriteLine("When you have something in mind, press Enter to continue.");
        Console.ReadLine();

        //Instructions on what to do with the prompt
        Console.WriteLine("Now ponder the following questions as they relate to this experience");
        Console.Write("You may begin in: ");
        CountDown(5);

        Console.Clear();
        Console.WriteLine($"---- {DisplayPrompt().ToUpper()} ----");
        // Ask reflection questions until time expires
        DateTime endTime = DateTime.Now.AddSeconds(_activityDuration);
        while (DateTime.Now < endTime)
        {
            if (tempQuestions.Count == 0)                           //if duration of activity is longer than question list
            {
                tempQuestions = new List<string>(_questions);       //create a new questions list that is shuffled randomly
            }
            Console.WriteLine(GetQuestion(tempQuestions));
            CountDots(5);     
        }


        DisplayEnd();

    }
}