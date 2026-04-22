using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the grade percentage: ");
        string strGradePercent = Console.ReadLine();
        int intGradePercent = int.Parse(strGradePercent);
        int intSubGrade = intGradePercent % 10;

        string letterGrade;
        string letterSub;

        if (intGradePercent >= 90)
        {
            letterGrade = "A";
        }
        else if (intGradePercent >= 80)
        {
            letterGrade = "B";
        }
        else if (intGradePercent >= 70)
        {
            letterGrade = "C";
        }
        else if (intGradePercent >= 60)
        {
            letterGrade = "D";
        }
        else
        {
            letterGrade = "F";
        }

        if (intSubGrade >= 7)
        {
            if (letterGrade == "A" || letterGrade == "F")
            {
                letterSub = "";
            }
            else
            {
                letterSub = "+";   
            }
        }
        else if (intSubGrade <= 3)
        {
            if (letterGrade == "F")
            {
                letterSub = "";
            }
            else
            {
                letterSub = "-";
            }
        }
        else
        {
            letterSub = "";
        }

        Console.WriteLine($"You grade is: {letterGrade}{letterSub}");

        if (intGradePercent >= 70)
        {
            Console.WriteLine("You PASSED the class.");
        }
        else
        {
            Console.WriteLine("You did not pass. Try Again.");
        }
    }
}