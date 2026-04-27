using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int listNumber = -1;
        while (listNumber != 0)
        {
            Console.Write("Enter number: ");
            string strNumber = Console.ReadLine();
            listNumber = int.Parse(strNumber);
            if (listNumber != 0)
            {
                numbers.Add(listNumber);    
            }
        }
        int sumNumber = numbers.Sum();
        double avgNumber = numbers.Average();
        int maxNumber = numbers.Max();
        int minNumber = numbers.Min();
        int countNumber = numbers.Count();
        
        int smallestPositive = int.MaxValue;
        foreach (int number in numbers)
        {
            if (number < smallestPositive && number > 0)
            {
                smallestPositive = number;
            }
        }

        Console.WriteLine($"The sum is: {sumNumber}");
        Console.WriteLine($"The average is: {avgNumber}");
        Console.WriteLine($"The largest is: {maxNumber}");
        Console.WriteLine($"The smallest is: {minNumber}");
        Console.WriteLine($"The count of items is: {countNumber}");
        Console.WriteLine($"The smallest positive is: {smallestPositive}");

        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }

    }
}