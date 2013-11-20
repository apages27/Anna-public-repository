using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesAndPerimeters
{
    class ShapeFactory
    {
        public IShape GetShape(string input)
        {
            GetUserInput getter = new GetUserInput();

            switch (input)
            {
                case "circle":
                    return getter.GetCircleInput();
                case "square":
                    return getter.GetSquareInput();
                case "triangle":
                    return getter.GetTriangleInput();
                default:
                    return null;
            }
        }
    }
}
