using System;
using System.Net.Http.Headers;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Sql;
using KillerAppClassLibrary.Context.Test;
using KillerAppClassLibrary.Logic.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KillerApp.UnitTests
{
    [TestClass]
    public class AdminRepositoryTests
    {
        private readonly AdminRepository adminRepository = new AdminRepository(new AdminTestContext());

        #region AddNewTrack tests

        [TestMethod]
        public void AddNewTrack_ArtistNameIsEmpty_ExceptionIsNotNull()
        {
            Exception expected = null;

            try
            {
                adminRepository.AddNewTrack(string.Empty, "test", "test", 1);
            }
            catch (Exception e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);

        }

        [TestMethod]
        public void AddNewTrack_TrackNameIsEmpty_TrackNameIsEmpty_ExceptionIsNotNull()
        {
            Exception expected = null;

            try
            {
                adminRepository.AddNewTrack("test", string.Empty, "test", 1);
            }
            catch (Exception e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void AddNewTrack_LabelIsEmpty_ExceptionIsNotNull()
        {
            Exception expected = null;

            try
            {
                adminRepository.AddNewTrack("test", "test", string.Empty, 1);
            }
            catch (Exception e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);

        }

        [TestMethod]
        public void AddNewTrack_PriceIsLowerThan1_ExceptionIsNotNull()
        {
            Exception expected = null;

            try
            {
                adminRepository.AddNewTrack("test", "test", "test", 0);
            }
            catch (Exception e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void AddNewTrack_TrackSuccesfullyAdded_ReturnsTrue()
        {
            bool result = adminRepository.AddNewTrack("test", "test", "test", 1);
            Assert.IsTrue(result);
        }

        #endregion
        
        #region EditTrack tests

        [TestMethod]
        public void EditTrack_ArtistNameIsEmpty_ExceptionIsNotNull()
        {
            Exception expected = null;

            try
            {
                adminRepository.EditTrack(new Track("", "test", "test", 1));
            }
            catch (Exception e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
            
        }

        [TestMethod]
        public void EditTrack_TrackNameIsEmpty_ExceptionIsNotNull()
        {
            Exception expected = null;

            try
            {
                adminRepository.EditTrack(new Track("test", "", "test", 1));
            }
            catch (Exception e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);

        }

        [TestMethod]
        public void EditTrack_LabelEmpty_ExceptionIsNotNull()
        {
            Exception expected = null;

            try
            {

                adminRepository.EditTrack(new Track("test", "test", "", 1));
            }
            catch (Exception e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);

        }

        [TestMethod]
        public void EditTrack_PriceIsLowerThan1_ExceptionIsNotNull()
        {
            Exception expected = null;

            try
            {
                adminRepository.EditTrack(new Track("test", "test", "test", 0));
            }
            catch (Exception e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void EditTrack_SuccesfullyEdited_ReturnsTrue()
        {
            bool result = adminRepository.EditTrack(new Track("test", "test", "test", 1));
            Assert.IsTrue(result);
        }
    }

    #endregion

}
