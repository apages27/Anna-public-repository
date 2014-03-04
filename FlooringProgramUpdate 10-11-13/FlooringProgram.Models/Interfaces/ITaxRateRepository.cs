using System.Collections.Generic;

namespace FlooringProgram.Models.Interfaces
{
    public interface ITaxRateRepository
    {
        List<TaxRate> GetTaxRates();
    }
}
