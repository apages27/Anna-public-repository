using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesAndPerimeters
{
    public class Circle : IShape
    {
        public double Radius { get; set; }

        public double Area()
        {
            return Math.PI*Math.Pow(Radius, 2);
        }

        public double Perimeter()
        {
            return 2*Math.PI*Radius;
        }
    }
}
