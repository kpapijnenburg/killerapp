using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Test
{
    public class VoteContextTestContext:IVoteContext
    {
        public bool CastVote(User user, Track track, int score)
        {
            throw new System.NotImplementedException();
        }

        public bool HasVoted(User user, Track track)
        {
            throw new System.NotImplementedException();
        }

        public int GetScore(Track track)
        {
            return 1;
        }

        public int GetCurrentScore(Track track, User user)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateCurrentScore(Track track, User user, int score)
        {
            var currentScore = 1;

            return currentScore != score;
        }
    }
}