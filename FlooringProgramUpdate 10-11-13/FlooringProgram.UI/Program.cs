using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Operations;

namespace FlooringProgram.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInteractions displayer = new UserInteractions();
            displayer.Run();
        }
    }
}
