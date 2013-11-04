using System;
using System.Configuration;

namespace PopQuiz5GuessingGame
{
    class Program
    {
        static void Main()
        {
            bool isGameOver = false;
            int playerGuesses = 0;

            while (!isGameOver)
            {
                int upperBoundPreset = int.Parse(ConfigurationManager.AppSettings["upperBound"]);
                Random r = new Random();
                int computerPick = r.Next(1, upperBoundPreset);
                int userGuess = 0;
                bool isCorrectGuess = false;
                Console.Clear();

                while (!isCorrectGuess)
                {
                    bool isNumber = false;

                    while (!isNumber)
                    {
                        Console.Write("\nPlease enter a guess between 1-{0}: ", upperBoundPreset);

                        isNumber = int.TryParse(Console.ReadLine(), out userGuess);
                        if (!isNumber)
                        {
                            Console.WriteLine("\nThat's not a number!  Try again.");
                        }
                    }


                    if (userGuess > computerPick)
                    {
                        Console.WriteLine("\nLower!");
                        playerGuesses++;
                    }
                    else if (userGuess < computerPick)
                    {
                        Console.WriteLine("\nHigher!");
                        playerGuesses++;
                    }
                    else
                    {
                        playerGuesses++;
                        Console.WriteLine("\nYou got it!");
                        Console.WriteLine("Number of guesses it took: {0}", playerGuesses);
                        isCorrectGuess = true;
                    }
                }
                Console.Write("\nPlay again? Y/N ");
                if (Console.ReadLine().ToUpper() == "N")
                {
                    isGameOver = true;
                }
            }


        }
    }
}
