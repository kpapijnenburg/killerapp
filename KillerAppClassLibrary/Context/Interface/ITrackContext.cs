using System.Collections.Generic;
using KillerAppClassLibrary.Classes;

namespace KillerAppClassLibrary.Context.Interface
{
    public interface ITrackContext
    {
        IEnumerable<Track> GetAllTracks();
        IEnumerable<Track> GetMostDownloadedTracks();
    }
}