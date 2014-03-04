using System.Collections.Generic;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Data.Products
{
    public class ProductTestRepository: IProductRepository
    {
        public List<Models.Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product() {ProductType = "Carpet", CostPerSquareFoot = 2.25M, LaborCostPerSquareFoot = 2.10M}
            };

        }
    }
}



