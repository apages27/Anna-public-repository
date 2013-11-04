using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachineMVC.Models
{
    public class VendingItem
    {
        public int Choice { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}