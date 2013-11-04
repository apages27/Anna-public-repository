using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.Products;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Operations
{
    public class ProductOperations
    {
        public bool IsExistingProduct(string product)
        {
            IProductRepository repository = ProductRepositoryFactory.GetProductRepository();
            var allProducts = repository.GetProducts();
            return allProducts.Any(p => p.ProductType.ToUpper() == product.ToUpper());
        }

        public Product GetProductFor(string product)
        {
            IProductRepository repository = ProductRepositoryFactory.GetProductRepository();
            var allProducts = repository.GetProducts();
            return allProducts.FirstOrDefault(p => p.ProductType.ToUpper() == product.ToUpper());
        }

    }
}
