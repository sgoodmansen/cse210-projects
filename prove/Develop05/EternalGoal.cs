public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points): base(name, description, points)
    {
        
    } 

    public override int RecordGoal()
    {
        return _points; 
    }  

    public override string GetStatus()
    {
            return "[ ]";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{_name}|{_description}|{_points}";
    } 
}