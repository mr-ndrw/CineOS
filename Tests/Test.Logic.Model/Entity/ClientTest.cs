using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using en.AndrewTorski.CineOS.Logic.Model.Entity;
using NUnit.Framework;

namespace en.AndrewTorski.CineOS.Test.Logic.Model.Entity
{
    [TestFixture]
	public class ClientTest
	{
        [Test]
        public void TestExistsMethod_ItExists()
        {
            //  Arrange
            var cred1 = new PersonCredentials() {Email = "best"};
            var cred2 = new PersonCredentials() {Email = "notbest"};
            Person.CreateClient(cred1, "hello");
            Person.CreateClient(cred2, "hello");

            //  Act & Assert
            Assert.IsTrue(0 <= Client.Exists("best", "hello"));
        }

        [Test]
        public void TestExistsMethod_ItDoesntExist()
        {
            //  Arrange
            var cred1 = new PersonCredentials() { Email = "best" };
            var cred2 = new PersonCredentials() { Email = "notbest" };
            Person.CreateClient(cred1, "hello");
            Person.CreateClient(cred2, "hello");

            //  Act & Assert
            var id = Client.Exists("best", "hello1");
            Assert.IsTrue(-1 == id);
        }
	}
}
