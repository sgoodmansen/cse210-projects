using System;

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("John", 3, 16);           //create new Reference object
        Scripture scripture = new Scripture(reference, "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.");           //create new Scripture object
        
        while (true)                                                //create loop to run while scripture still has visible words
        {
            Console.Clear();                                        //clear console screen
            Console.WriteLine(scripture.DisplayScripture());        //display scripture text

            Console.WriteLine();
            if (scripture.AllHidden())
            {
                Console.WriteLine("Hope this helped you memorize the scripture. If not, run the program again.");
                break;
            }
            else
            {
                Console.WriteLine("How many words would you like to hide? (1-3, default is 3)");    //display instruction prompt to user
                Console.Write("Press Enter for default or type 'quit' to end: ");
                string input = Console.ReadLine();

                int numberToHide = 3;

                if (input.ToLower() == "quit")                          //if user types quit - exit program
                {
                    break;
                }
                else
                {
                    if(int.TryParse(input, out int result))
                    {
                        numberToHide = int.Parse(input);
                    }
                }

                scripture.HideRandomWords(numberToHide);                //when user inputs a value or presses enter - hide random words
            }   
        }     
    }
}