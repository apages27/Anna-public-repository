using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;
using System.IO;

namespace FlooringProgram.Data.Products
{
    public class ProductFileRepository: IProductRepository
    {
        public List<Models.Product> GetProducts()
        {
            List<Product> items = new List<Product>();

             string[] data = File.ReadAllLines(Configuration.GetDataDirectory() + "Products.txt");
            for (int i = 1; i < data.Length; i++)
            {
                string[] row = data[i].Split(',');

                Product toAdd = new Product();
                toAdd.ProductType = row[0];
                toAdd.CostPerSquareFoot = decimal.Parse(row[1]);
                toAdd.LaborCostPerSquareFoot = decimal.Parse(row[2]);

                items.Add(toAdd);
            }
            return items;
        }
            
    }
}




