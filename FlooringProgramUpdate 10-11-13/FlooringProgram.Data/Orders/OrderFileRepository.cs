
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;
using System.IO;

namespace FlooringProgram.Data.Orders
{
    public class OrderFileRepository: IOrderRepository
    {
        private const string HeaderRow =
            "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";

        public bool FileExistsFor(DateTime date)
        {
            return File.Exists(GetFilePath(date));
        }

        public bool OrderExistsFor(int orderNumber, DateTime orderDate)
        {
            bool exists = false;
            string[] file = File.ReadAllLines(GetFilePath(orderDate));
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].StartsWith(orderNumber.ToString()))
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        public string GetFilePath(DateTime date)
        {
            string result = @"Data\Orders_" + date.ToString("MMddyyyy") + ".txt";
            return result;
        }

        public List<Order> GetOrders(DateTime orderDate)
        {
            List<Order> orders = new List<Order>();

            string[] data = File.ReadAllLines(GetFilePath(orderDate));

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] != "")
                {
                    string[] row = data[i].Split(',');

                    Order toAdd = new Order();
                    toAdd.OrderNumber = int.Parse(row[0]);
                    toAdd.CustomerName = row[1];
                    toAdd.State = row[2];
                    toAdd.TaxRate = decimal.Parse(row[3]);
                    toAdd.ProductType = row[4];
                    toAdd.Area = decimal.Parse(row[5]);
                    toAdd.CostPerSquareFoot = decimal.Parse(row[6]);
                    toAdd.LaborCostPerSquareFoot = decimal.Parse(row[7]);
                    toAdd.MaterialCost = decimal.Parse(row[8]);
                    toAdd.LaborCost = decimal.Parse(row[9]);
                    toAdd.Tax = decimal.Parse(row[10]);
                    toAdd.Total = decimal.Parse(row[11]);

                    orders.Add(toAdd);
                }
            }
            return orders;
        }

        public void CheckAndSaveNewFile(Order neworder)
        {
            string fileName = GetFilePath(DateTime.Today);

            if (FileExistsFor(DateTime.Today))
            {
                string[] lineCount = File.ReadAllLines(fileName);

                for (int i = 0; i < lineCount.Length; i++)
                {
                    string[] lastRow = lineCount[lineCount.Length - 1].Split(',');
                    neworder.OrderNumber = (int.Parse(lastRow[0])) + 1;
                }
                string orderLine = CreateCSVLine(neworder);

                File.AppendAllText(fileName, Environment.NewLine + orderLine);
            }
            else
            {
                // create file
                var writer = File.AppendText(fileName);
                writer.WriteLine(HeaderRow);
                neworder.OrderNumber = 1;
                writer.Write(CreateCSVLine(neworder));
                writer.Close();
            }
        }

        public void CheckAndSaveEditedFile(Order editedOrder, DateTime date)
        {
            string fileName = GetFilePath(date);

            string orderLine = CreateCSVLine(editedOrder);

            List<string> orders = new List<string>(File.ReadAllLines(fileName));
            int lineIndex = orders.FindIndex(o => o.StartsWith(editedOrder.OrderNumber.ToString()));
            if (lineIndex != -1)
            {
                orders[lineIndex] = orderLine;
                File.WriteAllLines(fileName, orders);
            }
        }

        public bool DeleteOrderFromFile(Order orderToBeDeleted, DateTime date)
        {
            string fileName = GetFilePath(date);
            bool deleted = false;

            List<string> orders = new List<string>(File.ReadAllLines(fileName));
            int lineIndex = orders.FindIndex(o => o.StartsWith(orderToBeDeleted.OrderNumber.ToString()));
            if (lineIndex != -1)
            {
                orders[lineIndex] = null;
                File.WriteAllLines(fileName, orders);
                deleted = true;
            }
            return deleted;
        }

        private string CreateCSVLine(Order order)
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", order.OrderNumber, order.CustomerName, order.State,
                order.TaxRate, order.ProductType, order.Area, order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
        }
    }
}
