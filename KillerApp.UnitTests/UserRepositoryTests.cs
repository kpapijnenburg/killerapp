using System;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Test;
using KillerAppClassLibrary.Logic.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KillerApp.UnitTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private readonly UserRepository userRepository = new UserRepository(new UserTestContext());

        [TestMethod]
        public void RegisterUser_UsernameIsEmpty_ThrowsException()
        {
            Exception e = null;
            var user = new User("", "test", "test");

            try
            {
                userRepository.RegisterUser(user);
            }
            catch (Exception exception)
            {
                e = exception;
            }

            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void RegisterUser_EmailIsEmpty_ThrowsException()
        {
            Exception e = null;
            var user = new User("test", "", "test");

            try
            {
                userRepository.RegisterUser(user);
            }
            catch (Exception exception)
            {
                e = exception;
            }

            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void RegisterUser_PasswordIsEmpty_ThrowsException()
        {
            Exception e = null;
            var user = new User("test", "test", "");

            try
            {
                userRepository.RegisterUser(user);
            }
            catch (Exception exception)
            {
                e = exception;
            }

            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void RegisterUser_EmailIsAlreasyInUse_ThrowsException()
        {
            Exception e = null;
            var user = new User("test", "test", "test");

            try
            {
                userRepository.RegisterUser(user);
            }
            catch (Exception exception)
            {
                e = exception;
            }

            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void RegisterUser_SuccesfullyRegisterd_ReturnsTrue()
        {
            var user = new User("test", "uniqueEmail", "test");

            bool result = userRepository.RegisterUser(user);
            
            Assert.IsTrue(result);
      
        }

        [TestMethod]
        public void Login_EmailIsEmpty_ThrowsException()
        {
            Exception e = null;

            try
            {
                userRepository.Login("", "test");
            }
            catch (Exception exception)
            {
                e = exception;
            }
            
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Login_PasswordIsEmpty_ThrowsException()
        {
            Exception e = null;

            try
            {
                userRepository.Login("test", "");
            }
            catch (Exception exception)
            {
                e = exception;
            }

            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void Login_UserIsAlreadyLoggedIn_ThrowsException()
        {
            Exception e = null;

            try
            {
                userRepository.Login("test", "test");
            }
            catch (Exception exception)
            {
                e = exception;
            }

            Assert.IsNotNull(e);
        }
    }
}
