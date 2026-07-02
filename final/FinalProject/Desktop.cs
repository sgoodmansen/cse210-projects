using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Desktop:Equipment
{
    private string _processor;
    private int _ram;
    private int _storage;

    public Desktop(string assetTag, string brand, string model, string serialNumber, string processor, int ram, int storage): base(assetTag, brand, model, serialNumber)
    {
        _processor = processor;
        _ram = ram;
        _storage = storage;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Processor: {_processor}");        
        Console.WriteLine($"RAM: {_ram} GB");        
        Console.WriteLine($"Storage: {_storage} GB");        
    }

    public override string GetEquipmentType()
    {
        return "Desktop";
    }

    public override string ToFileString()
    {
        return $"{base.ToFileString()}|{_processor}|{_ram}|{_storage}";
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
        Console.WriteLine(" 7. Cancel");

        int choice = InputHelper.GetPositiveInteger("Choice: ", 1, 7);

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
                return false;
            default:
                return false;
        }
    }
}