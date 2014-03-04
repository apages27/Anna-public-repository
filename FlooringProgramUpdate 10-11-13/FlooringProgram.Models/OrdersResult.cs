using System.Collections.Generic;

namespace FlooringProgram.Models
{
    public class OrdersResult
    {
        public bool Success { get; set; }
        public List<Order> Orders { get; set; }
        public string Message { get; set; }
    }
}
