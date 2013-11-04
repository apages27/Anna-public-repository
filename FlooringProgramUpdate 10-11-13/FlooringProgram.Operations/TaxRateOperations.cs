using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.TaxRates;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Operations
{
    public class TaxRateOperations
    {
        public bool IsAllowedState(string state)
        {
            ITaxRateRepository repository = TaxRateRepositoryFactory.GetTaxRateRepository();
            var allTaxRates = repository.GetTaxRates();

            return allTaxRates.Any(t => t.State == state);
        }

        public TaxRate GetTaxRateFor(string state)
        {
            ITaxRateRepository repository = TaxRateRepositoryFactory.GetTaxRateRepository();
            var allTaxRates = repository.GetTaxRates();

            return allTaxRates.FirstOrDefault(t => t.State == state);
        }
    }
}
