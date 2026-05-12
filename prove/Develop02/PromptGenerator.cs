public class PromptGenerator
{
    public List<string> prompts = new List<string>()
    {
        "What was the best part of your day?",
        "What made you smile today?",
        "What did you learn today?",
        "What was the nicest thing you did today?",
        "What was your favorite meal today?"
    };

    Random randomGen = new Random();

    public string GetRandomPrompt()
    {
        int randomNumber = randomGen.Next(prompts.Count);
        return prompts[randomNumber];
    }
}

