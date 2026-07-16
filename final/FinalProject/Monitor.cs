using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Monitor : Equipment
{
    private double _screenSize;
    private bool _vga;
    private bool _dp;
    private bool _hdmi;

    public Monitor(string assetTag, string brand, string model, string serialNumber,double screenSize, bool vga, bool dp, bool hdmi): base(assetTag, brand, model, serialNumber)
    {
        _screenSize = screenSize;
        _vga = vga;
        _dp = dp;
        _hdmi = hdmi;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Screen Size: {_screenSize} inches");        
        Console.WriteLine($"VGA Port: {YesNo(_vga)}");        
        Console.WriteLine($"Display Port: {YesNo(_dp)}");        
        Console.WriteLine($"HDMI Port: {YesNo(_hdmi)}");        
    }

    private static string YesNo(bool value)
    {
        return value ? "Yes" : "No";
    }

    public override string GetEquipmentType()
    {
        return "Monitor";
    }

    public override string ToFileString()
    {
        return $"{base.ToFileString()}|{_screenSize}|{_vga}|{_dp}|{_hdmi}";
    }

    public override bool EditDetails()
    {
        Console.Clear();
        InputHelper.DisplayHeader("Current Information:");
        DisplayInfo();

        Console.WriteLine("\nWhat would you like to edit? ");
        Console.WriteLine(" 1. Brand");
        Console.WriteLine(" 2. Model");
        Console.WriteLine(" 3. Serial Number");
        Console.WriteLine(" 4. Screen Size");
        Console.WriteLine(" 5. VGA Port");
        Console.WriteLine(" 6. Display Port");
        Console.WriteLine(" 7. HDMI Port");
        Console.WriteLine(" 8. Cancel");

        int choice = InputHelper.GetIntegerInRange("Choice: ", 1, 8);

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
                _screenSize = InputHelper.GetPositiveDouble("New screen size: ");
                return true;
            case 5:
                _vga = InputHelper.GetBoolEntry("VGA (y/n): ");
                return true;
            case 6:
                _dp = InputHelper.GetBoolEntry("Display Port (y/n): ");
                return true;
            case 7:
                _hdmi = InputHelper.GetBoolEntry("HDMI Port (y/n): ");
                return true;
            case 8:
                return false;
            default:
                return false;
        }
    }
}