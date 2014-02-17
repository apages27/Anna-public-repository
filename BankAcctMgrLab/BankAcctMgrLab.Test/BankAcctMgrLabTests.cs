using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BankAcctMgrLab.Test
{
    [TestFixture]
    public class BankAcctMgrLabTests
    {
        private Operations oper = new Operations();

        [Test]
        public void ValidIntInputTest()
        {
            string input = "1234";
            oper.ValidateAndReturnPINInput(input);

            //Assert.AreEqual(1234, 1234);
            Assert.Pass();
        }

        [Test]
        public void AddUserInfoTest()
        {
            User user = new User();
            user.UserName = "Anna";
            user.PIN = 1234;

            oper.AddUserToFile(user);
            string[] userFileArray = oper.GetUserFile();
            string newestUser = userFileArray[userFileArray.Length - 1];

            Assert.AreEqual("Anna,1234,0.00,0.00", newestUser);
        }

        [Test]
        public void CorrectPINReturnsTrue()
        {
            bool result = oper.CheckIfCorrectPIN(1234, "Anna");

            Assert.AreEqual(true, result);
        }

        [Test]
        public void IncorrectPINReturnsFalse()
        {
            bool result = oper.CheckIfCorrectPIN(1111, "Anna");

            Assert.AreEqual(false, result);
        }

        [Test]
        public void GetCheckingBalanceTest()
        {
            decimal result = oper.GetCheckingBalance("Anna");

            Assert.AreEqual(0.00, result);
        }

        [Test]
        public void GetSavingsBalanceTest()
        {
            decimal result = oper.GetSavingsBalance("Anna");

            Assert.AreEqual(0.00, result);
        }
    }
}
