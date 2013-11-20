using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ShapesAndPerimeters
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isOver = false;

            Console.WriteLine("Area and Perimeter Calculator for Shapes");
            Console.WriteLine("----------------------------------------");

            while (!isOver)
            {
                bool isValidInput = false;

                while (!isValidInput)
                {
                    Console.Write("\nPlease enter the name of the shape you'd like to calculate for (circle, square or triangle): ");
                    string input = Console.ReadLine().ToLower();

                    if (input == "circle" || input == "square" || input == "triangle")
                    {
                        ShapeFactory factory = new ShapeFactory();
                        IShape shape = factory.GetShape(input);

                        if (shape.Area() == 0 || shape.Perimeter() == 0)
                        {
                            Console.WriteLine("\nThe measurement(s) that you entered is/are not valid.");
                        }
                        else
                        {
                            Console.WriteLine("\nThe area of your {0} is: {1:0.00} square inches", input, shape.Area());
                            Console.WriteLine("\nThe perimeter of your {0} is: {1:0.00} inches", input, shape.Perimeter());
                            isValidInput = true;
                        }

                    }
                    else
                    {
                        Console.WriteLine("\nThat was not a valid response.  Please try again.");
                    }
                }

                Console.Write("\nWould you like to calculate for another shape? Y/N ");
                if (Console.ReadLine().ToUpper() == "N")
                {
                    isOver = true;
                }
            }
        }
    }
}
