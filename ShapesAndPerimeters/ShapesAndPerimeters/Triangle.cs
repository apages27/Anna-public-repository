using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesAndPerimeters
{
    public class Triangle : IShape
    {
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }

        public double Area()
        {
            double halfPerimeter = Perimeter()/2;

            return Math.Sqrt(halfPerimeter*(halfPerimeter - SideA)*(halfPerimeter - SideB)*(halfPerimeter - SideC));
        }

        public double Perimeter()
        {
            return SideA + SideB + SideC;
        }
    }
}
