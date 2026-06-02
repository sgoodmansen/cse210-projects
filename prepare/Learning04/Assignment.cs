using System;
using System.IO.Pipelines;

class Assignment
{
    private string _studentName;
    private string _topic;

    public Assignment(string studentname, string topic)
    {
        _studentName = studentname;
        _topic = topic;
    }
    
    public string GetSummary()
    {
       return $"{_studentName} - {_topic}";    
    }

    public string GetStudentName()
    {
        return $"{_studentName}";
    }

}