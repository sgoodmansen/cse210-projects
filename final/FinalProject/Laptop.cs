using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Laptop:Equipment
{
    private string _processor;
    private int _ram;
    private int _storage;
    private double _screenSize;

    public Laptop(string assetTag, string brand, string model, string serialNumber, string processor, int ram, int storage, double screenSize): base(assetTag, brand, model, serialNumber)
    {
        _processor = processor;
        _ram = ram;
        _storage = storage;
        _screenSize = screenSize;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Processor: {_processor}");        
        Console.WriteLine($"RAM: {_ram} GB");        
        Console.WriteLine($"Storage: {_storage} GB");
        Console.WriteLine($"Screen Size: {_screenSize} inches");        
    }

    public override string GetEquipmentType()
    {
        return "Laptop";
    }

    public override string ToFileString()
    {
        return $"{base.ToFileString()}|{_processor}|{_ram}|{_storage}|{_screenSize}";
    }

    public override bool EditDetails()
    {
        Console.Clear();
        Console.WriteLine("Current Information:");
        Console.WriteLine("-----------------------------");
        DisplayInfo();

        Console.WriteLine("\nWhat would you like to edit? ");
        Console.WriteLine(" 1. Brand");
        Console.WriteLine(" 2. Model");
        Console.WriteLine(" 3. Serial Number");
        Console.WriteLine(" 4. Processor");
        Console.WriteLine(" 5. RAM");
        Console.WriteLine(" 6. Storage");
        Console.WriteLine(" 7. Screen Size");
        Console.WriteLine(" 8. Cancel");

        int choice = InputHelper.GetPositiveInteger("Choice: ", 1, 8);

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
                _processor = InputHelper.GetRequiredText("New processor: ");
                return true;
            case 5:
                _ram = InputHelper.GetPositiveInteger("New RAM (GB): ");
                return true;
            case 6:
                _storage = InputHelper.GetPositiveInteger("New Storage (GB): ");
                return true;
            case 7:
                _screenSize = InputHelper.GetPositiveDouble("New Screen Size: ");
                return true;
            case 8:
                return false;
            default:
                return false;
        }
    }
}