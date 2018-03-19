using System;
using System.Data.SqlClient;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Sql
{
    public class VoteSqlContext:IVoteContext
    {
        private const string ConnectionString =
            "Server=mssql.fhict.local;Database=dbi388613;User Id=dbi388613;Password=wachtwoord;";

        public bool CastVote(User user, Track track, int score)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO KillerApp.Vote VALUES (@trackId, @userId, @vote)";

                        command.Parameters.AddWithValue("@trackId", track.Id);
                        command.Parameters.AddWithValue("@userId", user.Id);
                        command.Parameters.AddWithValue("@vote", score);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            } 
        }

        public bool HasVoted(User user, Track track)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM KillerApp.Vote WHERE UserId = @userId AND TrackId = @trackId";

                        command.Parameters.AddWithValue("@userId", user.Id);
                        command.Parameters.AddWithValue("@trackId", track.Id);

                        var reader = command.ExecuteReader();

                        bool result = reader.HasRows;

                        connection.Close();

                        return result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int GetScore(Track track)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT COUNT(Vote) as TotalVote, TrackID, UserID " +
                                              "FROM KillerApp.Vote " +
                                              "WHERE TrackId = @trackId " +
                                              "GROUP BY TrackId, UserId";

                        command.Parameters.AddWithValue("@trackId", track.Id);
                        
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader["TotalVote"]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

            return 0;
        }

        public int GetCurrentScore(Track track, User user)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText ="SELECT Vote FROM KillerApp.Vote " +
                                             "WHERE UserId = @userId AND TrackId = @trackId";

                        command.Parameters.AddWithValue("@userId", user.Id);
                        command.Parameters.AddWithValue("@trackId", track.Id);

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader["Vote"]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return -2;
                }
                finally
                {
                    connection.Close();
                }
            }
            return -2;
        }

        public bool UpdateCurrentScore(Track track, User user, int score)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE KillerApp.Vote " +
                                              "SET Vote = @score " +
                                              "WHERE UserId = @userId AND TrackId = @trackId";

                        command.Parameters.AddWithValue("@userId", user.Id);
                        command.Parameters.AddWithValue("@trackId", track.Id);
                        command.Parameters.AddWithValue("@score", score);

                        return command.ExecuteNonQuery() > 0;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }

                finally
                {
                    connection.Close();
                }
            }
        }
    }
}