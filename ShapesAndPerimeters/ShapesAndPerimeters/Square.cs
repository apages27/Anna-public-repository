using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesAndPerimeters
{
    public class Square : IShape
    {
        public double Length { get; set; }
        public double Width { get; set; }

        public double Area()
        {
            return Length*Width;
        }

        public double Perimeter()
        {
            return (Length*2) + (Width*2);
        }
    }
}
