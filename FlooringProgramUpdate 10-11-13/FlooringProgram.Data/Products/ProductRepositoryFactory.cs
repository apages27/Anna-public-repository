﻿using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Data.Products
{
  public  class ProductRepositoryFactory
    {
      public static IProductRepository GetProductRepository()
      {
          string mode = Configuration.GetMode();

          switch (mode)
          {
              case "T":
                  return new ProductTestRepository();
              case "P":
                  return new ProductFileRepository();
          }

          return null;
      }
    }
}
