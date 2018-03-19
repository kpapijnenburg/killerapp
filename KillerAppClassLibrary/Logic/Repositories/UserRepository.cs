using System;
using System.Runtime.CompilerServices;
using KillerAppClassLibrary.Context;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;
using KillerAppClassLibrary.Context.Sql;
using KillerAppClassLibrary.Logic;

namespace KillerAppClassLibrary.Logic.Repositories
{
    public class UserRepository
    {
        private User currentUser;

        private readonly IUser userContext;

        public UserRepository(IUser context)
        {
            userContext = context;
        }

        public bool RegisterUser(User user)
        {
            if (user.Username == string.Empty)
            {
                throw new Exception("No username given.");
            }
            if (user.Email == string.Empty)
            {
                throw new Exception("Email adress is not valid.");
            }
            if (user.Password == string.Empty)
            {
                throw new Exception("Password incorrect.");
            }
            if (userContext.RegisterUser(user) == 0)
            {
                throw new Exception("Email adress has been used already.");
            }

            return true;
        }

        public bool Login(string email, string password)
        {
            if (email == string.Empty)
            {
                throw new Exception($"{email} is not a valid emailadress.");
            }

            if (password == string.Empty)
            {
                throw new Exception("No password given.");
            }

            currentUser = userContext.Login(email, password);

            if (currentUser != null) throw new Exception("You are already logged in.");

            if (currentUser == null)
            {
                throw new Exception("Password and emailadress combination is incorrect.");
            }
            return true;
        }

        public string Logout()
        {

            string username = currentUser.Username;
            currentUser = null;

            return $"User {username} has logged out.";
        }

        public string AddFund(int fund)
        {
            currentUser.Fund += fund;

            userContext.AddFund(currentUser, currentUser.Fund);
            return $"Added €{fund},- to wallet.";
        }

        public User GetCurrentUser()
        {
            return currentUser;
        }

    }
}