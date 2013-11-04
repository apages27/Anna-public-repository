using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ballacp
{
    class GameManager
    {
        static Random randomChoice = new Random();
        private bool isGameOver;
        private string answer;

        public void PlayGame()
        {
            while (!isGameOver)
            {
                GetQuestion();
                Console.Clear();
                Console.WriteLine("The Magic 8 Ball is considering your question...");
                Console.Clear();
                Console.WriteLine("Your answer is:\n");
                GetAnswer();
                Console.Write("\n\nThat was cool, play again? [Y/N]");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    isGameOver = false;
                }
                else isGameOver = true;
            }
        }

        private void GetAnswer()
        {
            int rng = randomChoice.Next(1, 5);
            string[] reader = File.ReadAllLines("TextFile1.txt");
            Console.WriteLine(reader[rng]);
        }

        private void GetQuestion()
        {
            Console.Clear();

            Console.WriteLine("What is your question?");

            Console.ReadLine();
        }
    }
}
