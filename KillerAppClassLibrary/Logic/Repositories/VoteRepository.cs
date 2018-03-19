using System;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;
using KillerAppClassLibrary.Context.Sql;


namespace KillerAppClassLibrary.Logic.Repositories
{
    public class VoteRepository
    {
        private readonly IVoteContext voteSqlContext;

        public VoteRepository(IVoteContext context)
        {
            voteSqlContext = context;
        }

        public bool CastVote(User user, Track track, int score)
        {
            if (!voteSqlContext.HasVoted(user, track))
            {
                voteSqlContext.CastVote(user, track, score);
                return true;
            }

            UpdateScore(user, track, score);
            return false;
        }

        public int GetScore(Track track)
        {
            return voteSqlContext.GetScore(track);
        }

        public bool UpdateScore(User user, Track track, int score) //todo splitsen en hernoemen.
        {
            int currentScore = voteSqlContext.GetCurrentScore(track, user);
            int newScore = score;

            if (score == currentScore)
            {
                throw new Exception("You already voted on this track.");
            }
            else if (score != currentScore)
            {
                voteSqlContext.UpdateCurrentScore(track, user, newScore);
                return true;
            }
            else
            {
                throw new Exception("Something went wrong.");;
            }
        }

        public int GetCurrentScore(Track track, User user)
        {
            return voteSqlContext.GetCurrentScore(track, user);
        }

        public bool HasVoted(Track track, User user)
        {
            return voteSqlContext.HasVoted(user, track);
        }
    }
}