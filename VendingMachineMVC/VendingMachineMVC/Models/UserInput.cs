using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendingMachineMVC.Models
{
    public class UserInput
    {
        public List<VendingItem> VendingItems { get; set; }

        [Required(ErrorMessage = "Enter a choice!")]
        [DisplayName("User Choice:")]
        public int? UserChoice { get; set; }

        [Required(ErrorMessage = "Enter an amount!")]
        [DisplayName("Money Deposited:")]
        public decimal? UserPaid { get; set; }

        public string ErrorMessage { get; set; }
    }
}