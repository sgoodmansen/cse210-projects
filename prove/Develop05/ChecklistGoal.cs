public class ChecklistGoal : Goal
{
    protected int _bonusTarget;         //the number of times the goal must be completed to qualify for a bonus
    protected int _bonusPoints;         //the total points awarded for completing the goal multiple times
    protected int _bonusCompleted;      //the number of times the goal has been completed


    public ChecklistGoal(string name, string description, int points, int bonusTarget, int bonusPoints): base(name, description, points)
    {
        _bonusTarget = bonusTarget;
        _bonusPoints = bonusPoints;
        _bonusCompleted = 0;    
    } 

    public override int RecordGoal()
    {
        _bonusCompleted++;          //increase the times completed by 1

        if (_bonusCompleted >= _bonusTarget)
        {
            _isComplete = true;
            return _points + _bonusPoints;
        }
        
        return _points; 
    }  

    public override string GetGoalDetails()
    {
        return $"{GetStatus()} {_name} ~ {_description} --Currently completed: {_bonusCompleted}/{_bonusTarget} ({_points} pts)";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_name}|{_description}|{_points}|{_bonusTarget}|{_bonusPoints}|{_bonusCompleted}";
    } 

    public void SetAmountCompleted(int bonusCompleted)
    {
        _bonusCompleted = bonusCompleted;
    }
}