using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Test;
using KillerAppClassLibrary.Logic.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KillerApp.UnitTests
{
    [TestClass]
    public class OrderRepositoryTest
    {
        private readonly OrderRepository orderRepository = new OrderRepository(new OrderTestContext());

        [TestMethod]
        public void CreateOrder_InsufficientFunds_ExceptionIsNotNull()
        {
            Exception expected = null;
            var tracks = new List<Track> { new Track("test", "test", "test", 1), new Track("test", "test", "test", 1) };
            var user = new User("test", "test", "test");

            try
            {
                orderRepository.CreateOrder(DateTime.Now, tracks, user);
            }
            catch (Exception e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void CreateOrder_OrderSuccesfullyPlaced_ReturnsTrue()
        {
            var tracks = new List<Track> { new Track("test", "test", "test", 1), new Track("test", "test", "test", 1) };
            var user = new User("test", "test", "test") { Fund = 100 };

            bool result = orderRepository.CreateOrder(DateTime.Now, tracks, user);
      
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateOrder_FundsHaveBeenTakenAfterOrder_FundIsEqualTo98()
        {
            var tracks = new List<Track> { new Track("test", "test", "test", 1), new Track("test", "test", "test", 1) };
            var user = new User("test", "test", "test") { Fund = 100 };
            int totalPrice = tracks.Sum(track => track.Price);

            user.Fund -= totalPrice;

            Assert.AreEqual(user.Fund, 98);
        }
    }
}
