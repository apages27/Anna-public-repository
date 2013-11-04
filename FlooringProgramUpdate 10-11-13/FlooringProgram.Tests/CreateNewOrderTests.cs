using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.Orders;
using FlooringProgram.Models;
using FlooringProgram.Operations;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    public class CreateNewOrderTests
    {
        [Test]
        public void CreateOrderTest()
        {
            Order myOrder = new Order() {CustomerName = "Big Bird", Area = 500M};

            OrderFileRepository repository = new OrderFileRepository();
            repository.CheckAndSaveNewFile(myOrder);

            Assert.AreEqual(500M, myOrder.Area);
            Assert.AreEqual("Big Bird", myOrder.CustomerName);
        }
    }
}
