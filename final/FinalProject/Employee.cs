public class Employee
{
    private string _employeeId;
    private string _firstname;
    private string _lastname;
    private string _department;
    private EmployeeStatus _status;

    public Employee(string employeeId, string firstname, string lastname, string department, EmployeeStatus status)
    {
       _employeeId = employeeId;
       _firstname = firstname;
       _lastname = lastname;
       _department = department;
       _status = status; 
    }

    public string GetEmployeeId()
    {
        return _employeeId;
    }

    public string GetFullName()
    {
        return $"{_firstname} {_lastname}";
    }

    public string GetDepartment()
    {
        return _department;
    }

    public EmployeeStatus GetStatus()
    {
        return _status;
    }

    public bool IsActive()
    {
        return _status == EmployeeStatus.Active;
    }

    public void DisplaySummary()
    {
        Console.WriteLine($"{_employeeId, -12} " + $"{GetFullName(), -25} " + $"{_department, -18} " + $"{_status, -12}");
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"Employee ID: {_employeeId}");
        Console.WriteLine($"Employee Name: {GetFullName()}");
        Console.WriteLine($"Department: {_department}");
        Console.WriteLine($"Status: {_status}");
    }

    public bool EditDetails()
    {
        Console.Clear();
        Console.WriteLine("Current Employee Information");
        Console.WriteLine("----------------------------");
        DisplayDetails();

        Console.WriteLine("\nWhat would you like to edit?");
        Console.WriteLine(" 1. First Name");
        Console.WriteLine(" 2. Last Name");
        Console.WriteLine(" 3. Department");
        Console.WriteLine(" 4. Employee Status");
        Console.WriteLine(" 5. Cancel");

        int choice = InputHelper.GetIntegerInRange("Choice: ", 1, 5);

        switch (choice)
        {
            case 1:
                _firstname = InputHelper.GetRequiredText("New first name: ");
                return true;

            case 2:
                _lastname = InputHelper.GetRequiredText("New last name: ");
                return true;

            case 3:
                _department = InputHelper.GetRequiredText("New department: ");
                return true;

            case 4:
                _status = InputHelper.GetEmployeeStatus();
                return true;

            case 5:
                return false;

            default:
                return false;
        }
    }

    public string ToFileString()
    {
        return $"EMPLOYEE|{_employeeId}|{_firstname}|{_lastname}|{_department}|{_status}";
    }
}