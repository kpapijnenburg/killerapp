using KillerAppClassLibrary.Classes;

namespace KillerAppClassLibrary.Context.Interface
{
    public interface IUser
    {
        int RegisterUser(User user);
        User Login(string email, string password);
        void AddFund(User user, int fund);

    }
}