public class RewardMessage
{
    private List<string> _messages = new List<string>();
    private Random _random = new Random();

    public RewardMessage()
    {
        _messages.Add("Fantastic! You earned");
        _messages.Add("Great Job! You earned");
        _messages.Add("Keep it up! You earned");
        _messages.Add("Outstanding! You earned");
        _messages.Add("Way to go! You earned");
        _messages.Add("Excellent work! You earned");
        _messages.Add("You are doing great! You earned");
    }

    public string GetRandomMessage()
    {
        int index = _random.Next(_messages.Count);
        return _messages[index];
    }
}