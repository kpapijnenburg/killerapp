using System.Collections.Generic;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Test
{
    public class TrackContextTestContext:ITrackContext
    {
        public IEnumerable<Track> GetAllTracks()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Track> GetMostDownloadedTracks()
        {
            throw new System.NotImplementedException();
        }
    }
}