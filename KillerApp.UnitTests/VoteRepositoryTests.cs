using System;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Sql;
using KillerAppClassLibrary.Context.Test;
using KillerAppClassLibrary.Logic.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KillerApp.UnitTests
{
    [TestClass]
    public class VoteRepositoryTests
    {
        private readonly VoteRepository voteRepository = new VoteRepository(new VoteContextTestContext());

        [TestMethod]
        public void UpdateScore_UserHasAlreadyVotedWithThisScore_ThrowsException()
        {
            Exception e = null;
            var track = new Track("test", "test", "test", 1);
            var user = new User("test", "test", "test");

            try
            {
                voteRepository.UpdateScore(user, track, 1);
            }
            catch (Exception exception)
            {
                e = exception;
            }

            Assert.IsNotNull(e);

        }

        [TestMethod]
        public void UpdateScore_UserHasNotVotedWithThisScore_ReturnsTrue()
        {
            Exception e = null;
            var track = new Track("test", "test", "test", 1);
            var user = new User("test", "test", "test");
                
            bool result = new VoteContextTestContext().UpdateCurrentScore(track, user, -1);

            Assert.IsTrue(result);
        }
    }
}
