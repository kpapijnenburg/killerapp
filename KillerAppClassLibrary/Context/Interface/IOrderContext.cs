using KillerAppClassLibrary.Classes;

namespace KillerAppClassLibrary.Context.Interface
{
    public interface IOrderContext
    {
        bool CreateNewOrder(Order order);
    }
}