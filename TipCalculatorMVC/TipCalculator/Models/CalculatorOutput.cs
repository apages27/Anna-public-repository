using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TipCalculator.Models
{
    public class CalculatorOutput
    {
        public decimal Amount { get; set; }
        public decimal TipAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public CalculatorOutput()
        {
            
        }
        
        public CalculatorOutput(CalculatorInput input)
        {
            Amount = input.Amount.Value;            
            TipAmount = input.Amount.Value*(input.TipPercent.Value/100);
            TotalAmount = TipAmount + Amount;
        }
    }
}