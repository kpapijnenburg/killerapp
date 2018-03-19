using KillerAppClassLibrary.Classes;

namespace KillerAppClassLibrary.Context.Interface
{
    public interface IVoteContext
    {
        bool CastVote(User user, Track track, int score);
        bool HasVoted(User user, Track track);
        int GetScore(Track track);
        int GetCurrentScore(Track track, User user);
        bool UpdateCurrentScore(Track track, User user, int score);

    }
}