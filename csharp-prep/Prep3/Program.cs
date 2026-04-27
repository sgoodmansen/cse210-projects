using System;

class Program
{
    static void Main(string[] args)
    {
        // Console.Write("What is the magic number? ");
        // string strMagicNumber = Console.ReadLine();
        // int intMagicNumber = int.Parse(strMagicNumber);
        
        string playAgain = "y";
        while (playAgain == "y")
        {
            Random randomGenerator = new Random();
            int intMagicNumber = randomGenerator.Next(1, 101);

            int intUserGuesses = 0;
            string again = "yes";
            while (again == "yes")
            {
                Console.Write("Guess a number between 1 and 10050: ");
                string strGuess = Console.ReadLine();
                int intGuess = int.Parse(strGuess);

                string response;
                intUserGuesses ++;

                if (intGuess > intMagicNumber)
                {
                    response = "Lower";
                }
                else if (intGuess < intMagicNumber)
                {
                    response = "Higher";
                }
                else
                {
                    response = "You guessed it!";
                    again = "no";
                }

                Console.WriteLine($"{response}");
            }
            Console.WriteLine($"It took you {intUserGuesses} guesses!");
            Console.Write("Do you want to play again? (Y or N) ");
            playAgain = Console.ReadLine().ToLower();
        }
        Console.WriteLine("Thanks for playing!");        
    }
}