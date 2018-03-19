using System;
using System.Collections.Generic;
using System.Linq;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context;
using KillerAppClassLibrary.Context.Interface;
using KillerAppClassLibrary.Context.Sql;


namespace KillerAppClassLibrary.Logic.Repositories
{
    public class OrderRepository
    {
        private readonly IOrderContext orderSqlContext;

        public OrderRepository(IOrderContext context)
        {
            orderSqlContext = context;
        }

        public bool CreateOrder(DateTime orderDate, IEnumerable<Track> tracks, User user)
        {
            var order = new Order(orderDate, tracks.ToList(), user);

            int totalPrice = tracks.Sum(track => track.Price);

            if (user.Fund < totalPrice) throw new Exception( "insufficient funds available.");

            try
            {
                if (!orderSqlContext.CreateNewOrder(order)) return false;

                user.Fund -= totalPrice;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}




