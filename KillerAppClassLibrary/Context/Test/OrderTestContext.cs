using System.Collections.Generic;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;


namespace KillerAppClassLibrary.Context.Test

{
    public class OrderTestContext:IOrderContext
    {
        List<Order> orders = new List<Order>();

        public bool CreateNewOrder(Order order)
        {
            int oldCount = orders.Count;
            orders.Add(order);
            int newCount = orders.Count;

            return newCount > oldCount;
        }
    }
}