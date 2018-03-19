using System;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Test;
using KillerAppClassLibrary.Logic.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KillerApp.UnitTests
{
    [TestClass]
    public class ControllerTests
    {
        private Controller controller = new Controller();

        [TestMethod]
        public void UserIsLoggedIn_NoUserIsLoggedIn_ReturnsFalse()
        {
            var user = controller.UserIsLoggedIn();

            Assert.IsFalse(user);
        }

        [TestMethod]
        public void UserIsLoggedIn_UserIsLoggedIn_ReturnsTrue()
        {
            controller.Login("test", "test");

            var user = controller.UserIsLoggedIn();
            
            Assert.IsTrue(user);
        }

    }
}
