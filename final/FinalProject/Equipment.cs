using System;
using System.Collections.Generic;

public abstract class Equipment
{
    protected string _assetTag;
    protected string _brand;
    protected string _model;
    protected string _serialNumber;
    private EquipmentStatus _status;

    // protected Location _location;
    protected Employee _assignedEmployee;

    

    public Equipment(string assetTag, string brand, string model, string serialNumber)
    {
        _assetTag = assetTag;
        _brand = brand;
        _model = model;
        _serialNumber = serialNumber;
        _status = EquipmentStatus.Available;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Asset Tag: {_assetTag}");
        Console.WriteLine($"Brand: {_brand}");
        Console.WriteLine($"Model: {_model}");
        Console.WriteLine($"Status: {_status}");

        if (_assignedEmployee == null)
        {
            Console.WriteLine("Assigned To: None");
        }
        else
        {
            Console.WriteLine($"Assigned To: {_assignedEmployee.GetFullName()}");
            Console.WriteLine($"Department: {_assignedEmployee.GetDepartment()}");
        }
    }

    public virtual void DisplaySummary()
    {
        Console.WriteLine(
            $"{_assetTag, -12} " +
            $"{GetEquipmentType(),-10} " +
            $"{_brand, -12} " +
            $"{_model, -18} " +
            $"{_status, -12} " +
            $"{GetAssignedEmployeeName(), -20}"
        );
    }

    public virtual string ToFileString()
    {
        string employeeId = _assignedEmployee == null ? "None" : _assignedEmployee.GetEmployeeId();

        return $"EQUIPMENT|{GetEquipmentType()}|{_assetTag}|{_brand}|{_model}|{_serialNumber}|{_status}|{employeeId}";
    }

    public void SetStatus(EquipmentStatus status)
    {
        _status = status;
    }

    public abstract string GetEquipmentType();

    public string GetAssetTag()
    {
        return _assetTag;
    }
    
    public bool IsAvailable()
    {
        return _status == EquipmentStatus.Available;
    }

    public bool IsCheckedOut()
    {
        return _status == EquipmentStatus.CheckedOut;
    }

    public void CheckOut(Employee employee)
    {
        _assignedEmployee = employee;
        _status = EquipmentStatus.CheckedOut;
    }

    public void CheckIn()
    {
        _assignedEmployee = null;
        _status = EquipmentStatus.Available;
    }

    public bool IsRetired()
    {
        return _status == EquipmentStatus.Retired;
    }

    public void Retire()
    {
        _status = EquipmentStatus.Retired;
    }
    public virtual bool EditDetails()
    {
        Console.Clear();
        InputHelper.DisplayHeader("Current Information:");
        DisplayInfo();

        Console.WriteLine("\nWhat would you like to edit? ");
        Console.WriteLine(" 1. Brand");
        Console.WriteLine(" 2. Model");
        Console.WriteLine(" 3. Serial Number");
        Console.WriteLine(" 4. Cancel");

        int choice = InputHelper.GetIntegerInRange("Choice: ", 1, 4);

        switch (choice)
        {
            case 1:
                _brand = InputHelper.GetRequiredText("New brand: ");
                return true;
            case 2:
                _model = InputHelper.GetRequiredText("New model: ");
                return true;
            case 3:
                _serialNumber = InputHelper.GetRequiredText("New serial number: ");
                return true;
            case 4:
                return false;
            default:
                return false;    
        }
    }

    public string GetAssignedEmployeeName()
    {
        if(_assignedEmployee == null)
        {
            return "None";
        }

        return _assignedEmployee.GetFullName();
    }

    public Employee GetAssignedEmployee()
    {
        return _assignedEmployee;
    }

    public void SetAssignedEmployee(Employee employee)
    {
        _assignedEmployee = employee;
    }
}