using KillerAppClassLibrary.Classes;

namespace KillerAppClassLibrary.Context.Interface
{
    public interface IAdminContext
    {
        bool AddNewTrack(string artistName, string trackName, string label, int price);
        bool RemoveTrack(Track track);
        bool EditTrack(Track track);
    }
}