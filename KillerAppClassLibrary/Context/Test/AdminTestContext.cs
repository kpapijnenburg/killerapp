using System.Collections.Generic;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Test
{
    public class AdminTestContext:IAdminContext
    {
        List<Track> tracks = new List<Track>();
        
        

        public bool AddNewTrack(string artistName, string trackName, string label, int price)
        {
            int oldCount = tracks.Count;
            tracks.Add(new Track(artistName,trackName,label,price));
            int newCount = tracks.Count;

            return oldCount < newCount;
        }

        public bool RemoveTrack(Track track)
        {
            tracks.Add(track);

            int oldCount = tracks.Count;
            tracks.Remove(track);
            int newCount = tracks.Count;

            return newCount < oldCount;
        }

        public bool EditTrack(Track track)
        {
            return true;
        }
    }
}