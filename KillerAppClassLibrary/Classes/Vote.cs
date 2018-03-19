using System;
using System.Collections.Generic;
using System.Text;

namespace KillerAppClassLibrary.Classes
{
    public class Vote
    {
        private int Id { get; }
        private double Score { get; }

        //Used when casting a new vote.
        public Vote(double score)
        {
            Score = 0;
        }

        //Used when getting a vote from the database.
        public Vote(int id, double score)
        {
            Id = id;
            Score = score;
        }
    }
}
