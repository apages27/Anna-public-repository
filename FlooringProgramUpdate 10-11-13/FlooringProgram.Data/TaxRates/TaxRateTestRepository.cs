using System.Collections.Generic;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Data.TaxRates
{
    public class TaxRateTestRepository : ITaxRateRepository
    {
        public List<TaxRate> GetTaxRates()
        {
            return new List<TaxRate>()
            {
                new TaxRate() {State = "OH", TaxPercent = 0.10M}
            };
        }
    }
}
