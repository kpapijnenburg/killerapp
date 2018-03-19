using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Sql;
using KillerAppClassLibrary.Logic;

namespace KillerAppClassLibrary.Logic.Repositories
{
    public class TrackRepository
    {
        private readonly TrackContextSqlContext trackContextContext = new TrackContextSqlContext();
        
        public IEnumerable<Track> GetAllTracks()
        {
            return trackContextContext.GetAllTracks();
        }

        public IEnumerable<Track> GetLatestReleases()
        {
            var tracks = GetAllTracks().ToList();
            return tracks.Skip(Math.Max(0, tracks.Count - 10)).Reverse();
        }

        public IEnumerable<Track> GetMostDownloadedTracks()
        {
            var tracks = trackContextContext.GetMostDownloadedTracks().ToList();
            return tracks.Skip(Math.Max(0, tracks.Count - 10));
        }
    }
}
