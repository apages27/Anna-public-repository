using System;
using System.Collections.Generic;

namespace FlooringProgram.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOrders(DateTime date);

        bool FileExistsFor(DateTime date);

        bool OrderExistsFor(int orderNumber, DateTime date);

        bool DeleteOrderFromFile(Order orderToBeDeleted, DateTime date);
    }
}
