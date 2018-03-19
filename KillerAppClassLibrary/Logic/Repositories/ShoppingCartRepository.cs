using System;
using System.Collections.Generic;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;
using KillerAppClassLibrary.Context.Sql;
using KillerAppClassLibrary.Logic;

namespace KillerAppClassLibrary.Logic.Repositories
{
    public class ShoppingCartRepository
    {
        private readonly IShoppingCart shoppingCartContext;
        readonly ShoppingCart shoppingCart = new ShoppingCart();

        public ShoppingCartRepository(IShoppingCart context)
        {
            shoppingCartContext = context;
        }

        public bool PutInShoppingCart(Track track, User user)
        {
            shoppingCart.Tracks = (List<Track>) (shoppingCartContext.GetShoppingcartItems(user) ?? new List<Track>());

            if (shoppingCart.Tracks.Count >= 5)
            {
                throw new Exception("Maximum amount of tracks in the shoppingcart has been exceeded.");
            }

            try
            {
                shoppingCart.Tracks.Add(track);
                shoppingCartContext.PutInShoppingCart(track, user);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public IEnumerable<Track> GetShoppingCartItems(User user)
        {
           return shoppingCartContext.GetShoppingcartItems(user);
        }

        public void RemoveAllItemsFromShoppingCart(User user)
        {
            shoppingCartContext.RemoveAllItemsFromShoppingCart(user);
        }

        public void RemoveItemFromShoppingCart(User user, Track track)
        {
            shoppingCartContext.RemoveItemFromShoppingCart(user,track);
        }
    }
}