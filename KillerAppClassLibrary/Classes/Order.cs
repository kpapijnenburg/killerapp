using System;
using System.Collections.Generic;
using System.Text;

namespace KillerAppClassLibrary.Classes
{
    public class Order
    {
        public int Id { get; }
        public DateTime OrderDate { get; }
        public User User { get; }
        public IEnumerable<Track> Tracks { get; }
        public bool Status { get; }

        //Used when creating a new order.
        public Order(DateTime orderDate, IEnumerable<Track> tracks, User user)
        {
            OrderDate = orderDate;
            Tracks = tracks;
            Status = false;
            User = user;
        }

        //Used when getting an order from the database.
        public Order(int id, DateTime orderDate, IEnumerable<Track> tracks, bool status)
        {
            Id = id;
            OrderDate = orderDate;
            Tracks = tracks;
            Status = status;
        }
    }
}
