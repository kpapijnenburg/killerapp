using System;
using System.Collections.Generic;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Test;
using KillerAppClassLibrary.Logic.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KillerApp.UnitTests
{
    [TestClass]
    public class ShoppingCartRepositoryTests
    {
        private readonly ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository(new ShoppingCartTestContext());

        [TestMethod]
        public void PutInshoppingCart_ProductAddedToShoppingCart_ReturnsTrue()
        {
            var track = new Track("test", "test", "test", 1);
            var user = new User("test", "test", "test");


            bool result = shoppingCartRepository.PutInShoppingCart(track, user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetShoppingCartItems_AllShoppingCartItemsReturned_ReturnsNotNull()
        {
            var user = new User("test", "test", "test");

            var tracks = shoppingCartRepository.GetShoppingCartItems(user);

            Assert.IsNotNull(tracks);
        }
    }
}
