public class PromptGenerator
{
    public List<string> prompts = new List<string>()
    {
        "What was the best part of your day?",
        "What made you smile today?",
        "What did you learn today?",
        "What was the nicest thing you did today?",
        "What was the best food you ate today?",
        "What is something you are proud of, and why?",
        "What are you feeling right now—and why?",
        "What is your favorite thing about your personality?",
        "What are three goals you want to achieve this year?",
        "What is a skill you want to master in the next 6 months?",
        "What is your favorite thing in your room that brings you joy?"
    };

    Random randomGen = new Random();

    public string GetRandomPrompt()
    {
        int randomNumber = randomGen.Next(prompts.Count);
        return prompts[randomNumber];
    }
}

