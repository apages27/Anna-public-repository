using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public class OrdersResult
    {
        public bool Success { get; set; }
        public List<Order> Orders { get; set; }
        public string Message { get; set; }
    }
}
