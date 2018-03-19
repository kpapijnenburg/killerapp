using System.Collections.Generic;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Test
{
    public class ShoppingCartTestContext:IShoppingCart
    {
        private ShoppingCart shoppingCart = new ShoppingCart();
        

        public bool PutInShoppingCart(Track track, User user)
        {
            shoppingCart.Tracks = new List<Track>();

            int oldCount = shoppingCart.Tracks.Count;
            shoppingCart.Tracks.Add(track);
            int newCount = shoppingCart.Tracks.Count;

            return oldCount < newCount;
        }

        public IEnumerable<Track> GetShoppingcartItems(User user)
        {
            shoppingCart.Tracks = new List<Track>();

            return shoppingCart.Tracks;
        }

        public bool RemoveAllItemsFromShoppingCart(User user)
        {
            foreach (Track track in shoppingCart.Tracks)
            {
                shoppingCart.Tracks.Remove(track);
            }

            return shoppingCart.Tracks.Count == 0;
        }

        public bool RemoveItemFromShoppingCart(User user, Track track)
        {
            shoppingCart.Tracks.Add(track);

            return shoppingCart.Tracks.Remove(track);
        }
    }
}