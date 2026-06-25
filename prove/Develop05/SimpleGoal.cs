public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points): base(name, description, points)
    {
        
    } 

    public override int RecordGoal()
    {
       _isComplete = true;
       return _points; 
    }  

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
    } 

    public void SetComplete(bool complete)
    {
        _isComplete = complete;
    }
}