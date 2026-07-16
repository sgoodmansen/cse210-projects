using System;
using System.Collections.Generic;
using System.IO;

public class InventoryManager
{
    private List<Equipment> _equipmentList = new List<Equipment>();
    private List<Employee> _employeeList = new List<Employee>();

    public bool AddEquipment(Equipment equipment)
    {
        if (FindByAssetTag(equipment.GetAssetTag()) != null)
        {
            return false;
        }

        _equipmentList.Add(equipment);
        return true;
    }

    public void DisplayAllEquipment()
    {
        Console.Clear();

         if (_equipmentList.Count == 0)
        {
            InputHelper.DisplayWarning("No equipment has been added yet.");
            return;
        }
            DisplayTableHeader();

        int pageSize = 5;

        for (int i = 0; i < _equipmentList.Count; i++)
        {
            _equipmentList[i].DisplaySummary();

            if ((i+1) % pageSize == 0 && i < _equipmentList.Count - 1)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("----------------------------------------------------------------------------------------------");
                Console.WriteLine($"Showing {i - pageSize + 2}-{i + 1} of {_equipmentList.Count}");
                Console.WriteLine("Press Enter for next page...");
                Console.ReadLine();
                Console.ResetColor(); 

                Console.Clear();
                DisplayTableHeader();
            }
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("----------------------------------------------------------------------------------------------");

        int startItem = ((_equipmentList.Count - 1) / pageSize) * pageSize + 1;
        Console.WriteLine($"Showing {startItem}-{_equipmentList.Count} of {_equipmentList.Count}");

        Console.WriteLine($"Total Equipment: {_equipmentList.Count}");
        Console.ResetColor(); 
    }

    private void DisplayTableHeader()
    {
        //Header Information
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Equipment List");
        Console.WriteLine("----------------------------------------------------------------------------------------------");
        Console.WriteLine($"{"Asset Tag",-12} {"Type",-10} {"Brand",-12} {"Model",-18} {"Status",-12} {"Assigned To",-20}");
        Console.WriteLine("----------------------------------------------------------------------------------------------"); 
        Console.ResetColor();   
    }

    public Equipment FindByAssetTag(string assetTag)
    {
        foreach (Equipment item in _equipmentList)
        {
            if(item.GetAssetTag().ToLower() == assetTag.ToLower())
            {
                return item;
            }
        }

        return null;
    }

    public void CheckOutEquipment(string assetTag, Employee employee)
    {
        Equipment item = FindByAssetTag(assetTag);
        
        if (item == null)
        {
            InputHelper.DisplayError("No equipment found with that asset tag");
            return;
        }

        if (item.IsRetired())       //Check to see if equipment is retired
        {
            InputHelper.DisplayError("Retired equipment cannot be checked out");
            return;
        }

        if (!item.IsAvailable())     //Check to see if equipment is available for checkout
        {
            InputHelper.DisplayError("That equipment is not available for check out.");
            return;
        }

        if (!employee.IsActive())
        {
            InputHelper.DisplayError("Equipment can only be assigned to an active employee.");
            return;
        }

        item.CheckOut(employee);

        InputHelper.DisplaySuccess($"Equipment assigned to {employee.GetFullName()} successfully");
    }

    public void CheckInEquipment(string assetTag)
    {
        Equipment item = FindByAssetTag(assetTag);

        if (item == null)
        {
            InputHelper.DisplayError("No equipment found with that asset tag");
            return;
        }

        if (!item.IsCheckedOut())     //Check to see if equipment is checked out
        {
            InputHelper.DisplayWarning("That equipment is not currently checked out.");
            return;
        }

        item.CheckIn();
        InputHelper.DisplaySuccess("Equipment checked in successfully");
    }

    public void RetireEquipment(string assetTag)
    {
        Equipment item = FindByAssetTag(assetTag);

        if (item == null)
        {
            InputHelper.DisplayError("No equipment found with that asset tag");
            return;
        }

        if (item.IsRetired())
        {
            InputHelper.DisplayWarning("This equipment has already been retired");
            return;
        }

        item.Retire();
        InputHelper.DisplaySuccess("Equipment has been retired");
    }

    public void DeleteEquipment(string assetTag)
    {
        Equipment item = FindByAssetTag(assetTag);

        if (item == null)
        {
            InputHelper.DisplayError("No equipment found with that asset tag");
            return;    
        }
        
         _equipmentList.Remove(item);
         InputHelper.DisplaySuccess("Equipment has been deleted");
    }

    public void SaveInventory(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Employee employee in _employeeList)
            {
                outputFile.WriteLine(employee.ToFileString());
            }

            foreach (Equipment item in _equipmentList)
            {
                outputFile.WriteLine(item.ToFileString());
            }
        }

        InputHelper.DisplaySuccess($"Employees and Inventory saved successfully to {filename}");
    }

    public void LoadInventory(string filename)
    {
        if (!File.Exists(filename))
        {
            InputHelper.DisplayError("File not found.");
            return;
        }

        _employeeList.Clear();
        _equipmentList.Clear();

        string[] lines = File.ReadAllLines(filename);

        //First pass - employees
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split('|');

            if (parts[0]  == "EMPLOYEE")
            {
                string employeeId = parts[1];
                string firstname = parts[2];
                string lastname = parts[3];
                string department = parts[4];

                EmployeeStatus status = Enum.Parse<EmployeeStatus>(parts[5]);

                if (FindEmployeeById(employeeId) != null)
                {
                    InputHelper.DisplayWarning($"Duplicate employee ID: '{employeeId} - {firstname} {lastname}' was skipped.");
                    continue;    
                }

                Employee employee = new Employee(employeeId, firstname, lastname, department, status);
                _employeeList.Add(employee); 
                
            }
        }

        //Second pass - equipment
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split('|');

            if (parts[0] == "EQUIPMENT")
            {
                string equipmentType = parts[1];
                string assetTag = parts[2];
                string brand = parts[3];
                string model = parts[4];
                string serialNumber = parts[5];

                if (FindByAssetTag(assetTag) != null)
                {
                    InputHelper.DisplayWarning($"Duplicate asset tag: '{assetTag} - {brand} {model}' was skipped");
                    continue;
                }

                EquipmentStatus status = Enum.Parse<EquipmentStatus>(parts[6]);
                string employeeID = parts[7];

                Equipment equipment = null;

                if (equipmentType == "Desktop")
                {
                    string processor = parts[8];
                    int ram = int.Parse(parts[9]);
                    int storage = int.Parse(parts[10]);

                    equipment = new Desktop(assetTag, brand, model, serialNumber, processor, ram, storage);
                }
                else if (equipmentType == "Laptop")
                {
                    string processor = parts[8];
                    int ram = int.Parse(parts[9]);
                    int storage = int.Parse(parts[10]);
                    double screenSize = double.Parse(parts[11]);

                    equipment = new Laptop(assetTag, brand, model, serialNumber, processor, ram, storage, screenSize);
                }
                else if (equipmentType == "Monitor")
                {
                    double screenSize = double.Parse(parts[8]);
                    bool vga = bool.Parse(parts[9]);
                    bool dp = bool.Parse(parts[10]);
                    bool hdmi = bool.Parse(parts[11]);

                    equipment = new Monitor(assetTag, brand, model, serialNumber, screenSize, vga, dp, hdmi);
                }
                else if (equipmentType == "Printer")
                {
                    PrinterType printerType = Enum.Parse<PrinterType>(parts[8]);
                    bool isColor = bool.Parse(parts[9]);

                    equipment = new Printer(assetTag, brand, model, serialNumber, printerType, isColor);
                }

                if (equipment != null)
                {
                    equipment.SetStatus(status);
                    if (employeeID != "None")
                    {
                        Employee employee = FindEmployeeById(employeeID);

                        if (employee != null)
                        {
                            equipment.SetAssignedEmployee(employee);
                        }
                        else
                        {
                            InputHelper.DisplayError($"Employee {employeeID} was not found for asset {assetTag}.");
                            equipment.SetStatus(EquipmentStatus.Available);
                        }
                    }

                    _equipmentList.Add(equipment);
                }    
            }
        }
        InputHelper.DisplaySuccess("Employees and Inventory loaded successfully.");
    }

    public Employee FindEmployeeById(string employeeId)
    {
        foreach (Employee employee in _employeeList)
        {
            if (employee.GetEmployeeId().Equals(employeeId, StringComparison.OrdinalIgnoreCase))
            {
                return employee;
            }
        }

        return null;
    }

    public bool AddEmployee(Employee employee)
    {
        if (FindEmployeeById(employee.GetEmployeeId()) != null)
        {
            InputHelper.DisplayError("An employee with that ID already exists");
            return false;
        }

        _employeeList.Add(employee);
        return true;
    }

    public void DisplayAllEmployees()
    {
        Console.Clear();

        if (_employeeList.Count == 0)
        {
            InputHelper.DisplayWarning("No employees have been added");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Employee Listing");
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine($"{"Employee ID",-12} {"Name",-25} {"Department",-18} {"Status",-12}");
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.ResetColor();

        foreach (Employee employee in _employeeList)
        {
            employee.DisplaySummary();
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine($"Total Employees: {_employeeList.Count}");
        Console.ResetColor();
    }
}