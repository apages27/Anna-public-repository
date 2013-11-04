using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Data.Orders
{
    class OrderTestRepository: IOrderRepository
    {
      
        public bool FileExistsFor(DateTime date)
        {
            if (date.Day % 2 == 0)
                return true;
            else
            {
                return false;
            }
        }
        
        public List<Order> GetOrders(DateTime date)
        {
            return new List<Order>()
            {
                new Order() {OrderNumber = 1, CustomerName = "Anna", State = "GA", TaxRate = 2.25M, Area = 40, CostPerSquareFoot = 2.00M, LaborCost = 25.00M, LaborCostPerSquareFoot = 0.75M, MaterialCost = 1.99M, ProductType = "Bamboo", Tax = 10.00M, Total = 100.00M}
            };
        }


        public bool OrderExistsFor(int orderNumber, DateTime date)
        {
            return true;
        }

        public bool DeleteOrderFromFile(Order orderToBeDeleted, DateTime date)
        {
            return true;
        }
    }
}
