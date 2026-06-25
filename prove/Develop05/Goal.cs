public class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;
    protected bool _isComplete;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
        _isComplete = false;
    }

    public virtual int RecordGoal()
    {
        _isComplete = true;
        return _points;
    }

    public virtual string GetStatus()
    {
        if (_isComplete)
        {
            return "[X]";
        }
        else
        {
            return "[ ]";
        }
    }
    
    public string GetGoalName()
    {
        return _name;
    }

    public virtual string GetGoalDetails()
    {
        return $"{GetStatus()} {_name} ~ {_description} ({_points} pts)";
    }

    public virtual string GetStringRepresentation()
    {
        return $"{_name}|{_description}|{_points}";
    }   
}