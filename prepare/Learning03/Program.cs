using System;
using System.Security.Cryptography;

class Program

{
    static void Main(string[] args)
    {
        Fraction Fraction1 = new Fraction();
        Console.WriteLine("Initialize Fraction:");
        Console.WriteLine(Fraction1.GetFractionString());
        Console.WriteLine(Fraction1.GetDecimalValue());

        Fraction Fraction2 = new Fraction(6);
        Console.WriteLine("1 Variable Provided:");
        Console.WriteLine(Fraction2.GetFractionString());
        Console.WriteLine(Fraction2.GetDecimalValue());

        Fraction Fraction3 = new Fraction(6,7);
        Console.WriteLine("2 Variables Provided:");
        Console.WriteLine(Fraction3.GetFractionString());
        Console.WriteLine(Fraction3.GetDecimalValue());

        //Change vaules of Fraction1
        Fraction1.SetTop(3);
        Fraction1.SetBottom(4);

        //Retrieve new fraction
        int top = Fraction1.GetTop();
        int bottom = Fraction1.GetBottom();

        //Display new fraction
        Console.WriteLine("Updated Fraction:");
        Fraction Fraction4 = new Fraction(top,bottom);
        Console.WriteLine(Fraction4.GetFractionString());
        Console.WriteLine(Fraction4.GetDecimalValue());

        Random random = new Random();
        Fraction Fraction5 = new Fraction();
        for (int i=0; i < 20; i++)
        {
            int topValue = random.Next(1,11);
            int bottomValue = random.Next(1,11);
            Fraction5.SetTop(topValue);
            Fraction5.SetBottom(bottomValue);
            Console.WriteLine($"Fraction: {i+1}  String: {Fraction5.GetFractionString()}  Number: {Fraction5.GetDecimalValue()}");
        }
    }
}