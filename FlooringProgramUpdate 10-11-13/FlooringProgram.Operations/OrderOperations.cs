using System.Net.Configuration;
using FlooringProgram.Data.Orders;
using FlooringProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Operations
{
    public class OrderOperations
    {
        IOrderRepository repository = OrderRepositoryFactory.GetOrderRepository();
        
        public OrdersResult RetrieveOrdersFor(DateTime date)
        {
            OrdersResult result = new OrdersResult();

            

            if (!repository.FileExistsFor(date))
            {
                result.Success = false;
                result.Message = "Order file does not exist for that date!";
            }
            else
            {
                // ask repository to load orders
                // set success to true
                result.Success = true;
                result.Orders = repository.GetOrders(date);
            }
            return result;
        }

        public bool CheckIfExistingOrder(int orderNumber, DateTime date)
        {
            return repository.OrderExistsFor(orderNumber, date);
        }

        public Order RetrieveOrderByNumber(int orderNumber, DateTime date)
        {
            List<Order> data = RetrieveOrdersFor(date).Orders;
            return data.FirstOrDefault(o => o.OrderNumber == orderNumber);
        }

        public void CreateNewOrder(CustomerInput custIn, DateTime date)
        {
            Order orderData = new Order();
            OrderFileRepository newFile = new OrderFileRepository();

            orderData.CustomerName = custIn.CustName;
            orderData.State = custIn.State;
            orderData.ProductType = custIn.ProductType;
            orderData.Area = custIn.Area;
            orderData.OrderNumber = custIn.OrderNumber;

            string[] stateTaxData = File.ReadAllLines(@"Data\Taxes.txt");

            for (int i = 1; i < stateTaxData.Length; i++)
            {
                if (stateTaxData[i].Contains(custIn.State))
                {
                    string taxRateLine = stateTaxData[i];
                    orderData.TaxRate = decimal.Parse(taxRateLine.Substring(3, taxRateLine.Length - 3));
                }
            }
            string[] productData = File.ReadAllLines(@"Data\Products.txt");

            for (int i = 1; i < productData.Length; i++)
            {
                string[] productDataSplit = productData[i].Split(',');
                
                if (productDataSplit[0].ToUpper() == custIn.ProductType.ToUpper())
                {
                    orderData.CostPerSquareFoot = decimal.Parse(productDataSplit[1]);
                }
                if (productDataSplit[0].ToUpper() == custIn.ProductType.ToUpper())
                {
                    orderData.LaborCostPerSquareFoot = decimal.Parse(productDataSplit[2]);
                }
            }

            orderData.MaterialCost = orderData.Area * orderData.CostPerSquareFoot;

            orderData.LaborCost = orderData.Area * orderData.LaborCostPerSquareFoot;

            orderData.Tax = Math.Round((orderData.TaxRate / 100M) * (orderData.MaterialCost + orderData.LaborCost), 2);

            orderData.Total = orderData.MaterialCost + orderData.LaborCost + orderData.Tax;

            if (date == default(DateTime))
            {
                newFile.CheckAndSaveNewFile(orderData);
            }
            else newFile.CheckAndSaveEditedFile(orderData, date);
        }

        public bool CallDelete(Order orderToBeDeleted, DateTime date)
        {
            return repository.DeleteOrderFromFile(orderToBeDeleted, date);
        }
    }
}
