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
                                    inventory.AddEquipment(CreateDesktop());
                                    break;
                                }
                            case 2:     //Add Laptop 
                                {
                                    inventory.AddEquipment(CreateLaptop());
                                    break;
                                }
                            case 3:     //Add Monitor 
                                {
                                    inventory.AddEquipment(CreateMonitor());
                                    break;
                                }
                            case 4:     //Add Printer 
                                {
                                    inventory.AddEquipment(CreatePrinter());
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
                            Console.WriteLine("No equipment found with that asset tag");
                        }
                        else
                        {
                            bool updated = item.EditDetails();

                            if (updated)
                            {
                                Console.Clear();
                                Console.WriteLine("\nEquipment updated successfully");
                                item.DisplayInfo();
                            }
                            else
                            {
                                Console.WriteLine("\nNo changes were made");
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
                            Console.WriteLine("No equipment found with that asset tag");
                        }
                        else
                        {
                            item.DisplayInfo();
                            bool confirm = InputHelper.GetBoolEntry("\nAre you sure? (y/n): ");

                            if (confirm)
                            {
                                inventory.CheckOutEquipment(assetTag);    
                            }
                            else
                            {
                                Console.WriteLine("No changes were made");    
                            }    
                        }

                        InputHelper.Pause();
                        break;
                    }
                case 5:   //Check In Equipment
                    {
                        string assetTag = InputHelper.GetRequiredText("Enter asset tag to check in: ");

                        Equipment item = inventory.FindByAssetTag(assetTag);

                        item.DisplayInfo();
                        bool confirm = InputHelper.GetBoolEntry("\nAre you sure? (y/n): ");

                        if (item == null)
                        {
                            Console.WriteLine("No equipment found with that asset tag");
                        }
                        else
                        {
                            if (confirm)
                            {
                                inventory.CheckInEquipment(assetTag);
                            }  
                            else
                            {
                                Console.WriteLine("No changes were made");    
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
                            Console.WriteLine("No changes were made");
                            InputHelper.Pause();
                            break;
                        }

                        string assetTag = InputHelper.GetRequiredText("Enter asset tag to retire: ");
                        Equipment item = inventory.FindByAssetTag(assetTag);
                        
                        if (item == null)
                        {
                            Console.WriteLine("No equipment found with that asset tag");
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
                        }

                        InputHelper.Pause();
                        break;
                    }
                case 7:   //Save Inventory
                    {
                        string filename = GetFileName("Enter filename to save", "inventory.txt");
                        inventory.SaveInventory(filename);
                        InputHelper.Pause();
                        break;
                    }
                case 8:   //Load Inventory
                    {
                        string filename = GetFileName("Enter filename to load", "inventory.txt");
                        inventory.LoadInventory(filename);
                        InputHelper.Pause();
                        break;
                    }
                case 9:   //Quit 
                    {
                        Console.WriteLine("Inventory data will be lost unless you save the data.");
                        bool confirm = InputHelper.GetBoolEntry("Do you need to save before quitting? (y/n): ");

                        if (!confirm)
                        {
                            Console.WriteLine("Thanks for using the Equipment Inventory Program. Good-bye.\n");
                            running = false;    
                        }
                        else
                        {
                            running = true;
                        }

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
        Console.WriteLine("Equipment Inventory Menu Options");
        Console.WriteLine(" 1. Add Equipment");
        Console.WriteLine(" 2. Display All Equipment");
        Console.WriteLine(" 3. Edit Equipment");
        Console.WriteLine(" 4. Check Out Equipment");
        Console.WriteLine(" 5. Check In Equipment");
        Console.WriteLine(" 6. Retire / Delete Equipment");
        Console.WriteLine(" 7. Save Inventory");
        Console.WriteLine(" 8. Load Inventory");
        Console.WriteLine(" 9. Quit");

        return InputHelper.GetPositiveInteger("\nSelect a choice from the menu: ", 1, 9);
    }

    static int DisplaySubmenu()
    {
        Console.Clear();
        Console.WriteLine("What type of equipment would you like to add?");
        Console.WriteLine(" 1. Desktop");
        Console.WriteLine(" 2. Laptop");
        Console.WriteLine(" 3. Monitor");
        Console.WriteLine(" 4. Printer");

        return InputHelper.GetPositiveInteger("\nSelect a choice from the menu: ", 1, 4);
    }

    static int DisplayRetireDeleteMenu()
    {
        Console.WriteLine("Retire / Delete Equipment");
        Console.WriteLine(" 1. Retire Equipment");
        Console.WriteLine(" 2. Delete Equipment");
        Console.WriteLine(" 3. Cancel");

        return InputHelper.GetPositiveInteger("\nSelect a choice: ", 1, 3);
    }

    private static Desktop CreateDesktop()
    {
        EquipmentData data = GetCommonEquipmentData();
        
        string processor = InputHelper.GetRequiredText("Processor: ");
        int ram = InputHelper.GetPositiveInteger("RAM (GB): ");
        int storage = InputHelper.GetPositiveInteger("Storage (GB): ");

        return new Desktop(data.AssetTag, data.Brand, data.Model, data.SerialNumber, processor, ram, storage);
    }

    private static Laptop CreateLaptop()
    {
        EquipmentData data = GetCommonEquipmentData();
        
        string processor = InputHelper.GetRequiredText("Processor: ");
        int ram = InputHelper.GetPositiveInteger("RAM (GB): ");
        int storage = InputHelper.GetPositiveInteger("Storage (GB): ");
        double screenSize = InputHelper.GetPositiveDouble("Screen Size: ");

        return new Laptop(data.AssetTag, data.Brand, data.Model, data.SerialNumber, processor, ram, storage, screenSize);
    }

    private static Monitor CreateMonitor()
    {
        EquipmentData data = GetCommonEquipmentData();

        double screenSize = InputHelper.GetPositiveDouble("Screen Size: ");
        bool vga = InputHelper.GetBoolEntry("VGA (y/n): ");
        bool dp = InputHelper.GetBoolEntry("Display Port (y/n): ");
        bool hdmi = InputHelper.GetBoolEntry("HDMI (y/n): ");

        return new Monitor(data.AssetTag, data.Brand, data.Model, data.SerialNumber, screenSize, vga, dp, hdmi);
    }

    private static Printer CreatePrinter()
    {
        EquipmentData data = GetCommonEquipmentData();
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

     private static EquipmentData GetCommonEquipmentData()
    {
        return new EquipmentData
        {
            AssetTag = InputHelper.GetRequiredText("Asset Tag: "),
            Brand = InputHelper.GetRequiredText("Brand: "),
            Model = InputHelper.GetRequiredText("Model: "),
            SerialNumber = InputHelper.GetRequiredText("Serial Number: ")
        };
    }

}