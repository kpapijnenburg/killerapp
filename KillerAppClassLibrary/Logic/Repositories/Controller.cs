using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Sql;


namespace KillerAppClassLibrary.Logic.Repositories
{
    public class Controller
    {
        private readonly UserRepository userRepository = new UserRepository(new UserSqlContext());
        private readonly TrackRepository trackRepository = new TrackRepository();
        private readonly AdminRepository adminRepository = new AdminRepository(new AdminSqlContext());
        private readonly ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository(new ShoppingCartSqlContext());
        private readonly VoteRepository voteRepository = new VoteRepository(new VoteSqlContext());
        private readonly OrderRepository orderRepository = new OrderRepository(new OrderSqlContext());
        public Track CurrentTrack { get; set; }

        #region User

        public bool UserIsLoggedIn()
        {
            return userRepository.GetCurrentUser() != null;
        }

        public User GetCurrentUser()
        {
            return userRepository.GetCurrentUser();
        }

        public bool RegisterUser(User user)
        {
            try
            {
                userRepository.RegisterUser(user);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool Login(string email, string password)
        {
            return userRepository.Login(email, password);
        }

        public string AddFund(int fund)
        {
            return UserIsLoggedIn() ? userRepository.AddFund(fund) : "You must be logged in to use this feature.";
        }

        public string Logout()
        {
            return GetCurrentUser() != null ? userRepository.Logout() : "You must be logged in to use this feature.";
        }

        #endregion

        #region Track

        public IEnumerable<Track> GetAllTracks()
        {
            return trackRepository.GetAllTracks();
        }

        public IEnumerable<Track> GetLatestReleases()
        {
            return trackRepository.GetLatestReleases();
        }

        public IEnumerable<Track> GetMostDownloadedTracks()
        {
            return trackRepository.GetMostDownloadedTracks();
        }

        #endregion

        #region Admin

        public Exception AddNewTrack(string artistName, string trackName, string label, int price)
        {
            if (GetCurrentUser().IsAdmin)
            {
                try
                {
                    adminRepository.AddNewTrack(artistName, trackName, label, price);
                    throw new Exception("Succesfully added.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            var ex = new Exception("You must be an administrator to acces this function.");
            throw ex;
        }

        public bool RemoveTrack(Track track)
        {
            if (GetCurrentUser().IsAdmin)
            {
                try
                {
                    adminRepository.RemoveTrack(track);
                    var e = new Exception("You must be an administrator to acces this function.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw e;
                }
            }


            return false;
        }

        public bool EditTrack(Track track)
        {
            if (GetCurrentUser().IsAdmin)
            {
                try
                {
                    adminRepository.EditTrack(track);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
            var ex = new Exception("You must be an administrator to acces this function.");
            throw ex;
        }

        #endregion

        #region Shoppingcart

        public IEnumerable<Track> GetShoppingCartItems(User currentUser)
        {
            return currentUser != null ? shoppingCartRepository.GetShoppingCartItems(currentUser) : null;
        }

        public bool PutInShoppingCart(Track currentTrack, User currentUser)
        {
            if (!UserIsLoggedIn()) throw new Exception("You must be logged in to use this feature.");

            shoppingCartRepository.PutInShoppingCart(currentTrack, currentUser);
            return true;
        }

        public void RemoveItemFromShoppingCart(User user, Track track)
        {
            shoppingCartRepository.RemoveItemFromShoppingCart(user, track);
        }

        #endregion

        #region Vote

        public bool CastVote(User user, Track track, int score)
        {
            if (GetCurrentUser() == null) throw new Exception("You must be logged in to use this feature.");

            try
            {
                voteRepository.CastVote(user, track, score);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int GetScore(Track track)
        {
            return voteRepository.GetScore(track);
        }

        public bool VoteIsPositive(Track track, User user)
        {
            int score = voteRepository.GetCurrentScore(track, user);

            switch (score)
            {
                case 1:
                    return true;
                case -1:
                    return false;
            }

            return false;

        }

        public bool HasVoted(Track track, User user)
        {
            return voteRepository.HasVoted(track, user);
        }

        #endregion

        #region Order

        public bool CreateOrder(IEnumerable<Track> tracks, User user)
        {
            try
            {
                orderRepository.CreateOrder(DateTime.Now, tracks, user);
                shoppingCartRepository.RemoveAllItemsFromShoppingCart(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        #endregion
    }
}