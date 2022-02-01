using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sparse.Librarian;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        [DataRow("lib@email.com", "asd", true)]
        [DataRow("abc@email.com", "cde", false)]
        [DataRow("zxc@email.com", "qwe", false)]

        public void AuthenticateUserTest(string email, string password, bool expected)
        {
            LogIn user = new LogIn();
            bool result = user.AuthenticateUser(email, password);
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [DataRow(19, false)]
        [DataRow(102, true)]
        [DataRow(99, false)]

        public void EffectiveCapacityExceededTest(float roomOccupancy, bool expected)
        {
            Librarian librarian = new Librarian();
            bool actual = librarian.IsEffectiveCapacityExceeded(roomOccupancy);
            Assert.AreEqual(expected, actual);
        }
    }

}
