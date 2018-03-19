using System.Collections.Generic;
using KillerAppClassLibrary.Classes;

namespace KillerAppClassLibrary.Context.Interface
{
    public interface IShoppingCart
    {
        bool PutInShoppingCart(Track track, User user);
        IEnumerable<Track> GetShoppingcartItems(User user);
        bool RemoveAllItemsFromShoppingCart(User user);
        bool RemoveItemFromShoppingCart(User user, Track track);
    }
}