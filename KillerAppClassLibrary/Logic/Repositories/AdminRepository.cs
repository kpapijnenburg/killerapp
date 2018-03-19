using System;
using System.Net.Http.Headers;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context;
using KillerAppClassLibrary.Context.Interface;
using KillerAppClassLibrary.Context.Sql;


namespace KillerAppClassLibrary.Logic.Repositories
{

    public class AdminRepository
    {
        private readonly IAdminContext adminSqlContext;

        public AdminRepository(IAdminContext context)
        {
            adminSqlContext = context;
        }

        public bool AddNewTrack(string artistName, string trackName, string label, int price)
        {
            try
            {
                if (artistName == string.Empty)
                {
                    throw new Exception("Artistname can't be empty.");
                }
                if (trackName == string.Empty)
                {
                    throw new Exception("Trackname can't be empty.");
                }

                if (label == string.Empty)
                {
                    throw new Exception("Label can't be empty.");
                }

                if (price < 1)
                {
                    throw new Exception("Price must be atleast €1,-");
                }

                adminSqlContext.AddNewTrack(artistName, trackName, label, price);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
                
            }
        }

        public bool RemoveTrack(Track track)
        {
            try
            {
                adminSqlContext.RemoveTrack(track);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }
        }

        public bool EditTrack(Track track)
        {
            if (track.ArtistName == string.Empty)
            {
                var e = new Exception("Artistname can't be empty.");
                throw e;
            }
            if (track.TrackName == string.Empty)
            {
                var e = new Exception("Trackname can't be empty.");
                throw e;
            }
            if (track.Label == string.Empty)
            {
                var e = new Exception("Label can't be empty.");
                throw e;
            }
            if (track.Price < 1)
            {
                var e = new Exception("Price must be atleast €1,-");
                throw e;
            }

            adminSqlContext.EditTrack(track);
            return true;
        }
    }
}