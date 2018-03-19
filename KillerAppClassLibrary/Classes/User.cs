using System;
using KillerAppClassLibrary.Context;

namespace KillerAppClassLibrary.Classes
{
    public class User
    {
        public int Id { get; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public int Fund { get; set; }
        public bool IsAdmin { get; }



        //used when creating a new user.
        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
            Fund = 0;
            IsAdmin = false;

        }

        //used when getting the user from the database.
        public User(int id, string username, string email, string password, int fund, bool isAdmin)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Fund = fund;
            IsAdmin = isAdmin;

        }
    }
}
