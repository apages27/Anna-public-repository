using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesAndPerimeters
{
    public class GetUserInput
    {
        public IShape GetSquareInput()
        {
            bool lengthIsNumber = false;
            bool widthIsNumber = false;
            Square squareInput = new Square();

            while (!lengthIsNumber)
            {
                Console.Write("\nPlease enter length in inches: ");
                double length = 0;
                lengthIsNumber = double.TryParse(Console.ReadLine(), out length);
                if (lengthIsNumber)
                {
                    squareInput.Length = length; 
                }
                else
                {
                    Console.WriteLine("\nThat was not a valid number.  Please try again.");
                }
            }

            while (!widthIsNumber)
            {
                Console.Write("\nPlease enter width in inches: ");
                double width = 0;
                widthIsNumber = double.TryParse(Console.ReadLine(), out width);
                if (widthIsNumber)
                {
                    squareInput.Width = width; 
                }
                else
                {
                    Console.WriteLine("\nThat was not a valid number.  Please try again.");
                }
            }
            return squareInput;
        }

        public IShape GetTriangleInput()
        {
            bool sideAIsNumber = false;
            bool sideBIsNumber = false;
            bool sideCIsNumber = false;
            Triangle triangleInput = new Triangle();

            while (!sideAIsNumber)
            {
                Console.Write("\nPlease enter length of side A in inches: ");
                double sideA = 0;
                sideAIsNumber = double.TryParse(Console.ReadLine(), out sideA);
                if (sideAIsNumber)
                {
                    triangleInput.SideA = sideA;
                }
                else
                {
                    Console.WriteLine("\nThat was not a valid number.  Please try again.");
                }
            }

            while (!sideBIsNumber)
            {
                Console.Write("\nPlease enter length of side B in inches: ");
                double sideB = 0;
                sideBIsNumber = double.TryParse(Console.ReadLine(), out sideB);
                if (sideBIsNumber)
                {
                    triangleInput.SideB = sideB;
                }
                else
                {
                    Console.WriteLine("\nThat was not a valid number.  Please try again.");
                }
            }

            while (!sideCIsNumber)
            {
                Console.Write("\nPlease enter length of side C in inches: ");
                double sideC = 0;
                sideCIsNumber = double.TryParse(Console.ReadLine(), out sideC);
                if (sideCIsNumber)
                {
                    triangleInput.SideC = sideC;
                }
                else
                {
                    Console.WriteLine("\nThat was not a valid number.  Please try again.");
                }
            }
            return triangleInput;
        }

        public IShape GetCircleInput()
        {
            bool radiusIsNumber = false;
            Circle circleInput = new Circle();

            while (!radiusIsNumber)
            {
                Console.Write("\nPlease enter the radius of your circle in inches: ");
                double radius = 0;
                radiusIsNumber = double.TryParse(Console.ReadLine(), out radius);
                if (radiusIsNumber)
                {
                    circleInput.Radius = radius; 
                }
                else
                {
                    Console.WriteLine("\nThat was not a valid number.  Please try again.");
                }
            }
            return circleInput;
        }
    }
}
