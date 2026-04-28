using System;
using System.Globalization;

class Program
{
    
    static void Main(string[] args)
    {
        DisplayWelcome();

        string userName = PromptUserName();
        int userNumber = PromptUserNumber();
        int birthYear = PromptUserYear();
        double square = SquareNumber(userNumber);

        Console.WriteLine($"{userName}, the square of your number is {square}");
        Console.WriteLine($"{userName}, you will turn {2026 - birthYear} years old this year.");



        static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the Program!");
        }

        static string PromptUserName()
        {
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();

            return name;
        }
        
        static int PromptUserNumber()
        {
            Console.Write("Please enter your favorite number: ");
            int number = int.Parse(Console.ReadLine());

            return number;
        }

        static int PromptUserYear()
        {
            Console.Write("Please enter the year you were born: ");
            int year = int.Parse(Console.ReadLine());

            return year;
        }

        static double SquareNumber(int x)
        {
            double num = Math.Pow(x, 2);

            return num;
        }
    }
}