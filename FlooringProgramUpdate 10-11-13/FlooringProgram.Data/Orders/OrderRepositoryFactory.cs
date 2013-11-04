using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Data.Orders
{
    public class OrderRepositoryFactory
    {
        public static IOrderRepository GetOrderRepository()
        {
            string mode = Configuration.GetMode();

            switch (mode)
            {
                case "T":
                    return new OrderTestRepository();
                case "P":
                    return new OrderFileRepository();
            }

            return null;
        }
    }
}
