using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Printer : Equipment
{
    private PrinterType _printerType;
    private bool _isColor;

    public Printer(string assetTag, string brand, string model, string serialNumber,PrinterType printerType, bool isColor): base(assetTag, brand, model, serialNumber)
    {
        _printerType = printerType;
        _isColor = isColor;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Printer Type: {_printerType}");        
        Console.WriteLine($"Color: {YesNo(_isColor)}");      
    }

    private static string YesNo(bool value)
    {
        return value ? "Yes" : "No";
    }

    public override string GetEquipmentType()
    {
        return "Printer";
    }

    public override string ToFileString()
    {
        return $"{base.ToFileString()}|{_printerType}|{_isColor}";
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
        Console.WriteLine(" 4. Printer Type...");
        Console.WriteLine(" 5. Color");
        Console.WriteLine(" 6. Cancel");

        int choice = InputHelper.GetPositiveInteger("Choice: ", 1, 6);

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
                _printerType = InputHelper.GetPrinterType();
                return true;
            case 5:
                _isColor = InputHelper.GetBoolEntry("Color (y/n): ");
                return true;
            case 6:
                return false;
            default:
                return false;
        }
    }
}