using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TipCalculator.Models
{
    public class CalculatorInput
    {
        [Required(ErrorMessage = "Please enter an amount!")]
        [DisplayName("Purchase Amount")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "Please enter a tip percent!")]
        [DisplayName("Tip Percent")]
        public decimal? TipPercent { get; set; }
    }
}