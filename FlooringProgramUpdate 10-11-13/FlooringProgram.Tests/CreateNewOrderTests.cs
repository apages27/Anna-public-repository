using FlooringProgram.Data.Orders;
using FlooringProgram.Models;
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
