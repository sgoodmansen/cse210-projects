using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        bool running = true;
        
        InventoryManager inventory = new InventoryManager();

        while (running)
        {
            Console.Clear();
            int choice = DisplayMenu();

            switch (choice)
            {
                case 1:   //Add Equipment
                    {
                        int equipType = DisplaySubmenu();

                        switch (equipType)
                        {
                            case 1:     //Add Desktop 
                                {
                                    inventory.AddEquipment(CreateDesktop(inventory));
                                    InputHelper.DisplaySuccess("\nDesktop added successfully");
                                    InputHelper.Pause();
                                    break;
                                }
                            case 2:     //Add Laptop 
                                {
                                    inventory.AddEquipment(CreateLaptop(inventory));
                                    InputHelper.DisplaySuccess("\nLaptop added successfully");
                                    InputHelper.Pause();
                                    break;
                                }
                            case 3:     //Add Monitor 
                                {
                                    inventory.AddEquipment(CreateMonitor(inventory));
                                    InputHelper.DisplaySuccess("\nMonitor added successfully");
                                    InputHelper.Pause();
                                    break;
                                }
                            case 4:     //Add Printer 
                                {
                                    inventory.AddEquipment(CreatePrinter(inventory));
                                    InputHelper.DisplaySuccess("\nPrinter added successfully");
                                    InputHelper.Pause();
                                    break;
                                }
                            case 5:    //Cancel
                                {
                                    break;
                                }
                            
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case 2:   //Display All Equipment
                    {
                        inventory.DisplayAllEquipment();
                        InputHelper.Pause();
                        break;
                    }
                case 3:   //Edit Equipment
                    {
                        string assetTag = InputHelper.GetRequiredText("Enter asset tag to edit: ");

                        Equipment item = inventory.FindByAssetTag(assetTag);

                        if (item == null)
                        {
                            InputHelper.DisplayError("No equipment found with that asset tag");
                        }
                        else
                        {
                            bool updated = item.EditDetails();

                            if (updated)
                            {
                                Console.Clear();
                                InputHelper.DisplaySuccess("\nEquipment updated successfully");
                                item.DisplayInfo();
                            }
                            else
                            {
                                InputHelper.DisplayWarning("\nNo changes were made");
                            }
                        }

                        InputHelper.Pause();
                        break;
                    }
                case 4:   //Check Out Equipment
                    {
                        string assetTag = InputHelper.GetRequiredText("Enter asset tag to check out: ");
                        Equipment item = inventory.FindByAssetTag(assetTag);

                        if (item == null)
                        {
                            InputHelper.DisplayError("No equipment found with that asset tag");
                            InputHelper.Pause();
                            break;
                        }

                        string employeeId = InputHelper.GetRequiredText("Enter employee ID: ");

                        Employee employee = inventory.FindEmployeeById(employeeId);

                        if (employee == null)
                        {
                            InputHelper.DisplayError("No employee found with that ID");
                            InputHelper.Pause();
                            break;
                        }

                        Console.WriteLine("\nEquipment:");
                        item.DisplayInfo();

                        Console.WriteLine("\nAssign To:");
                        employee.DisplayDetails();

                        bool confirm = InputHelper.GetBoolEntry("\nConfirm check out? (y/n): ");

                        if (confirm)
                        {
                            inventory.CheckOutEquipment(assetTag, employee);
                        }
                        else
                        {
                            InputHelper.DisplayWarning("No changes were made.");
                        }

                        InputHelper.Pause();
                        break;
                    }
                case 5:   //Check In Equipment
                    {
                        string assetTag = InputHelper.GetRequiredText("Enter asset tag to check in: ");

                        Equipment item = inventory.FindByAssetTag(assetTag);

                        if (item == null)
                        {
                            InputHelper.DisplayError("No equipment found with that asset tag");
                            InputHelper.Pause();
                            break;
                        }

                        item.DisplayInfo();

                        bool confirm = InputHelper.GetBoolEntry("\nAre you sure? (y/n): ");

                        if (item == null)
                        {
                            InputHelper.DisplayError("No equipment found with that asset tag");
                        }
                        else
                        {
                            if (confirm)
                            {
                                inventory.CheckInEquipment(assetTag);
                            }  
                            else
                            {
                                InputHelper.DisplayWarning("No changes were made");    
                            }   
                        }
                        
                        InputHelper.Pause();
                        break;
                    }
                case 6:   //Retire or Delete Equipment
                    {
                        Console.Clear();
                        int choiceDel = DisplayRetireDeleteMenu();

                        if (choiceDel == 3)
                        {
                            InputHelper.DisplayWarning("No changes were made");
                            InputHelper.Pause();
                            break;
                        }

                        string action = choiceDel == 1 ? "retire" : "delete";

                        string assetTag = InputHelper.GetRequiredText($"Enter asset tag to {action}: ");
                        Equipment item = inventory.FindByAssetTag(assetTag);
                        
                        if (item == null)
                        {
                            InputHelper.DisplayError("No equipment found with that asset tag");
                        }
                        else
                        {
                            item.DisplayInfo();
                            bool confirm = InputHelper.GetBoolEntry("\nAre you sure? (y/n): ");

                            if (confirm)
                            {
                                if (choiceDel == 1)
                                {
                                    inventory.RetireEquipment(assetTag);
                                }
                                else if (choiceDel == 2)
                                {
                                    inventory.DeleteEquipment(assetTag);
                                }
                            }
                            else
                            {
                                InputHelper.DisplayWarning("No changes were made");
                            }
                        }

                        InputHelper.Pause();
                        break;
                    }
                case 7:   //Manage Employees
                    {
                        bool managingEmployees = true;

                        while (managingEmployees)
                        {
                            int employeeChoice = DisplayEmployeeMenu();

                            switch (employeeChoice)
                            {
                                case 1:  //Add Employee
                                    {
                                        Employee employee = CreateEmployee(inventory);

                                        bool added = inventory.AddEmployee(employee);

                                        if (added)
                                        {
                                            InputHelper.DisplaySuccess("\nEmployee added successfully");
                                        }

                                        InputHelper.Pause();
                                        break;
                                    }
                                case 2:  //Display Employees
                                    {
                                        inventory.DisplayAllEmployees();
                                        InputHelper.Pause();
                                        break;
                                    }
                                case 3:  //Search Employee
                                    {
                                        string employeeId = InputHelper.GetRequiredText("Enter employee ID: ");

                                        Employee employee = inventory.FindEmployeeById(employeeId);

                                        if (employee == null)
                                        {
                                            InputHelper.DisplayError("No employee found with that ID");
                                        }
                                        else
                                        {
                                            Console.WriteLine();
                                            employee.DisplayDetails();
                                        }

                                        InputHelper.Pause();
                                        break;
                                    }
                                case 4:  //Edit Employee
                                    {
                                        string employeeId = InputHelper.GetRequiredText("Enter employee ID to edit: ");
                                        
                                        Employee employee = inventory.FindEmployeeById(employeeId);

                                        if (employee == null)
                                        {
                                            InputHelper.DisplayError("No employee found with that ID");
                                        }
                                        else
                                        {
                                            bool updated = employee.EditDetails();

                                            if (updated)
                                            {
                                                InputHelper.DisplaySuccess("\nEmployee updated successfully");
                                                Console.WriteLine();
                                                employee.DisplayDetails();
                                            }
                                            else
                                            {
                                                InputHelper.DisplayWarning("\nNo changes were made");
                                            }
                                        }

                                        InputHelper.Pause();
                                        break;
                                    }
                                case 5:  //Return to Main Menu
                                    {
                                        managingEmployees = false;
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                case 8:   //Save Inventory
                    {
                        string filename = GetFileName("Enter filename to save", "inventory.txt");
                        inventory.SaveInventory(filename);
                        InputHelper.Pause();
                        break;
                    }
                case 9:   //Load Inventory
                    {
                        string filename = GetFileName("Enter filename to load", "inventory.txt");
                        inventory.LoadInventory(filename);
                        InputHelper.Pause();
                        break;
                    }
                case 0:  //Quit 
                    {
                        bool saveBeforeQuit = InputHelper.GetBoolEntry("Do you need to save before quitting? (y/n): ");

                        if (saveBeforeQuit)
                        {
                            string filename = GetFileName("Enter filename to save", "inventory.txt");
                            inventory.SaveInventory(filename);
                        }

                        Console.WriteLine("Thanks for using the Equipment Inventory Program. Good-bye.\n");
                        running = false;    
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }

    static int DisplayMenu()
    {
        InputHelper.DisplayHeader("Equipment Inventory");
        Console.WriteLine(" 1. Add Equipment");
        Console.WriteLine(" 2. Display All Equipment");
        Console.WriteLine(" 3. Edit Equipment");
        Console.WriteLine(" 4. Check Out Equipment");
        Console.WriteLine(" 5. Check In Equipment");
        Console.WriteLine(" 6. Retire / Delete Equipment");
        Console.WriteLine(" 7. Manage Employees");
        Console.WriteLine(" 8. Save Employees and Inventory");
        Console.WriteLine(" 9. Load Employees and Inventory");
        Console.WriteLine(" 0. Quit");

        return InputHelper.GetIntegerInRange("\nSelect a choice from the menu: ", 0, 10);
    }

    static int DisplaySubmenu()
    {
        Console.Clear();
        InputHelper.DisplayHeader("Type of Equipment");
        Console.WriteLine("What type of equipment would you like to add?");
        Console.WriteLine(" 1. Desktop");
        Console.WriteLine(" 2. Laptop");
        Console.WriteLine(" 3. Monitor");
        Console.WriteLine(" 4. Printer");
        Console.WriteLine(" 5. Cancel");

        return InputHelper.GetIntegerInRange("\nSelect a choice from the menu: ", 1, 5);
    }

    static int DisplayEmployeeMenu()
    {
        Console.Clear();
        InputHelper.DisplayHeader("Employee Management");
        Console.WriteLine(" 1. Add Employee");
        Console.WriteLine(" 2. Display All Employees");
        Console.WriteLine(" 3. Search Employee");
        Console.WriteLine(" 4. Edit Employee");
        Console.WriteLine(" 5. Return to Main Menu");

        return InputHelper.GetIntegerInRange("\nSelect a choice: ", 1, 5);
    }

    static int DisplayRetireDeleteMenu()
    {
        Console.WriteLine("Retire / Delete Equipment");
        Console.WriteLine(" 1. Retire Equipment");
        Console.WriteLine(" 2. Delete Equipment");
        Console.WriteLine(" 3. Cancel");

        return InputHelper.GetIntegerInRange("\nSelect a choice: ", 1, 3);
    }

    private static Desktop CreateDesktop(InventoryManager inventory)
    {
        EquipmentData data = GetCommonEquipmentData(inventory);
        
        string processor = InputHelper.GetRequiredText("Processor: ");
        int ram = InputHelper.GetIntegerInRange("RAM (GB): ");
        int storage = InputHelper.GetIntegerInRange("Storage (GB): ");

        return new Desktop(data.AssetTag, data.Brand, data.Model, data.SerialNumber, processor, ram, storage);
    }

    private static Laptop CreateLaptop(InventoryManager inventory)
    {
        EquipmentData data = GetCommonEquipmentData(inventory);
        
        string processor = InputHelper.GetRequiredText("Processor: ");
        int ram = InputHelper.GetIntegerInRange("RAM (GB): ");
        int storage = InputHelper.GetIntegerInRange("Storage (GB): ");
        double screenSize = InputHelper.GetPositiveDouble("Screen Size: ");

        return new Laptop(data.AssetTag, data.Brand, data.Model, data.SerialNumber, processor, ram, storage, screenSize);
    }

    private static Monitor CreateMonitor(InventoryManager inventory)
    {
        EquipmentData data = GetCommonEquipmentData(inventory);

        double screenSize = InputHelper.GetPositiveDouble("Screen Size: ");
        bool vga = InputHelper.GetBoolEntry("VGA (y/n): ");
        bool dp = InputHelper.GetBoolEntry("Display Port (y/n): ");
        bool hdmi = InputHelper.GetBoolEntry("HDMI (y/n): ");

        return new Monitor(data.AssetTag, data.Brand, data.Model, data.SerialNumber, screenSize, vga, dp, hdmi);
    }

    private static Printer CreatePrinter(InventoryManager inventory)
    {
        EquipmentData data = GetCommonEquipmentData(inventory);
        PrinterType printerType = InputHelper.GetPrinterType();
        
        bool isColor = InputHelper.GetBoolEntry("Color (y/n): ");  

        return new Printer(data.AssetTag, data.Brand, data.Model, data.SerialNumber, printerType, isColor);
    }

    static string GetFileName(string prompt, string defaultFilename)
    {
        Console.Write($"{prompt} (Press Enter for {defaultFilename}): ");
        string filename = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filename))
        {
            return defaultFilename;
        }

        return filename;
    }

     private static EquipmentData GetCommonEquipmentData(InventoryManager inventory)
    {
        string assetTag;

        while (true)
        {
            assetTag = InputHelper.GetRequiredText("Asset Tag: ");

            if (inventory.FindByAssetTag(assetTag) == null)
            {
                break;
            }

            InputHelper.DisplayWarning("That asset tag is already in use. Please enter a different asset tag.");
        }

        return new EquipmentData
        {
            AssetTag = assetTag,
            Brand = InputHelper.GetRequiredText("Brand: "),
            Model = InputHelper.GetRequiredText("Model: "),
            SerialNumber = InputHelper.GetRequiredText("Serial Number: ")
        };
    }

    private static Employee CreateEmployee(InventoryManager inventory)
    {
        string employeeId;

        while (true)
        {
            employeeId = InputHelper.GetRequiredText("Employee ID: ");

            if (inventory.FindEmployeeById(employeeId) == null)
            {
                break;
            }

            InputHelper.DisplayWarning("That Employee ID is already in use.");
        }

        string firstname = InputHelper.GetRequiredText("First name: ");
        string lastname = InputHelper.GetRequiredText("Last name: ");
        string department = InputHelper.GetRequiredText("Department: ");
        EmployeeStatus status = InputHelper.GetEmployeeStatus();

        return new Employee(employeeId, firstname, lastname, department, status);
    }
}