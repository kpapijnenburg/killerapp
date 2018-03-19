using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Test
{
    public class UserTestContext:IUser
    {
        public int RegisterUser(User user)
        {
            var existingUser = new User("test", "test", "test");

            return existingUser.Email == user.Email ? 0 : 1;
        }

        public User Login(string email, string password)
        {
            return new User("test",email, password);
        }

        public void AddFund(User user, int fund)
        {
            throw new System.NotImplementedException();
        }
    }
}