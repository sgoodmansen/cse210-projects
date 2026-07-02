using System;
using System.Collections.Generic;
using System.IO;

public class InventoryManager
{
    private List<Equipment> _equipmentList = new List<Equipment>();

    public void AddEquipment(Equipment equipment)
    {
        _equipmentList.Add(equipment);
    }

    public void DisplayAllEquipment()
    {
        Console.Clear();

         if (_equipmentList.Count == 0)
        {
            Console.WriteLine("No equipment has been added yet.");
            return;
        }
            DisplayTableHeader();

        int pageSize = 5;

        for (int i = 0; i < _equipmentList.Count; i++)
        {
            _equipmentList[i].DisplaySummary();

            if ((i+1) % pageSize == 0 && i < _equipmentList.Count - 1)
            {
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine($"Showing {i - pageSize + 2}-{i + 1} of {_equipmentList.Count}");
                Console.WriteLine("Press Enter for next page...");
                Console.ReadLine();

                Console.Clear();
                DisplayTableHeader();
            }
        }

        Console.WriteLine("-----------------------------------------------------------------");

        int startItem = ((_equipmentList.Count - 1) / pageSize) * pageSize + 1;
        Console.WriteLine($"Showing {startItem}-{_equipmentList.Count} of {_equipmentList.Count}");

        Console.WriteLine($"Total Equipment: {_equipmentList.Count}");
    }

    private void DisplayTableHeader()
    {
        //Header Information
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine($"{"Asset Tag",-12} {"Type",-10} {"Brand",-12} {"Model",-18} {"Status",-12}");
        Console.WriteLine("-----------------------------------------------------------------");    
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

    public void CheckOutEquipment(string assetTag)
    {
        Equipment item = FindByAssetTag(assetTag);
        
        if (item == null)
        {
            Console.WriteLine("No equipment found with that asset tag");
            return;
        }

        if (item.IsRetired())       //Check to see if equipment is retired
        {
            Console.WriteLine("Retired equipment cannot be checked out");
            return;
        }

        if (!item.IsAvailable())     //Check to see if equipment is available for checkout
        {
            Console.WriteLine("That equipment is not available for check out.");
            return;
        }

        item.CheckOut();
        Console.WriteLine("Equipment checked out successfully");
    }

    public void CheckInEquipment(string assetTag)
    {
        Equipment item = FindByAssetTag(assetTag);

        if (item == null)
        {
            Console.WriteLine("No equipment found with that asset tag");
            return;
        }

        if (!item.IsCheckedOut())     //Check to see if equipment is checked out
        {
            Console.WriteLine("That equipment is not currently checked out.");
            return;
        }

        item.CheckIn();
        Console.WriteLine("Equipment checked in successfully");
    }

    public void RetireEquipment(string assetTag)
    {
        Equipment item = FindByAssetTag(assetTag);

        if (item == null)
        {
            Console.WriteLine("No equipment found with that asset tag");
            return;
        }

        if (item.IsRetired())
        {
            Console.WriteLine("This equipment has already been retired");
            return;
        }

        item.Retire();
        Console.WriteLine("Equipment has been retired");
    }

    public void DeleteEquipment(string assetTag)
    {
        Equipment item = FindByAssetTag(assetTag);

        if (item == null)
        {
            Console.WriteLine("No equipment found with that asset tag");
            return;    
        }
        
         _equipmentList.Remove(item);
         Console.WriteLine("Equipment has been deleted");
    }

    public void SaveInventory(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Equipment item in _equipmentList)
            {
                outputFile.WriteLine(item.ToFileString());
            }
        }

        Console.WriteLine($"Inventory saved successfully to {filename}");
    }

    public void LoadInventory(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _equipmentList.Clear();

        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split("|");

            string equipmentType = parts[0];
            string assetTag = parts[1];
            string brand = parts[2];
            string model = parts[3];
            string serialNumber = parts[4];
            EquipmentStatus status = Enum.Parse<EquipmentStatus>(parts[5]);

            Equipment equipment = null;

            if (equipmentType == "Desktop")
            {
                string processor = parts[6];
                int ram = int.Parse(parts[7]);
                int storage = int.Parse(parts[8]);

                equipment = new Desktop(assetTag, brand, model, serialNumber, processor, ram, storage);
            }
            else if (equipmentType == "Laptop")
            {
                string processor = parts[6];
                int ram = int.Parse(parts[7]);
                int storage = int.Parse(parts[8]);
                double screenSize = double.Parse(parts[9]);

                equipment = new Laptop(assetTag, brand, model, serialNumber, processor, ram, storage, screenSize);
            }
            else if (equipmentType == "Monitor")
            {
                double screenSize = double.Parse(parts[6]);
                bool vga = bool.Parse(parts[7]);
                bool dp = bool.Parse(parts[8]);
                bool hdmi = bool.Parse(parts[9]);

                equipment = new Monitor(assetTag, brand, model, serialNumber, screenSize, vga, dp, hdmi);
            }
            else if (equipmentType == "Printer")
            {
                PrinterType printerType = Enum.Parse<PrinterType>(parts[6]);
                bool isColor = bool.Parse(parts[7]);

                equipment = new Printer(assetTag, brand, model, serialNumber, printerType, isColor);
            }

            if (equipment != null)
            {
                equipment.SetStatus(status);
                _equipmentList.Add(equipment);
            }
        }

        Console.WriteLine("Inventory loaded successfully.");
    }
}