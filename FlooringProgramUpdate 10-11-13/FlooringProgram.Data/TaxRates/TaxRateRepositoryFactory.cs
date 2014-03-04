using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Data.TaxRates
{
    public class TaxRateRepositoryFactory
    {
        public static ITaxRateRepository GetTaxRateRepository()
        {
            string mode = Configuration.GetMode();

            switch (mode)
            {
                case "T":
                    return new TaxRateTestRepository();
                case "P":
                    return new TaxRateFileRepository();
            }

            return null;
        }
    }
}
