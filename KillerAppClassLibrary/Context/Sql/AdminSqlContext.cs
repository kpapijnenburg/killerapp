using System;
using System.Data.SqlClient;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Sql
{
    public class AdminSqlContext:IAdminContext
    {
        private const string ConnectionString =
            "Server=mssql.fhict.local;Database=dbi388613;User Id=dbi388613;Password=wachtwoord;";

        public bool AddNewTrack(string artistName, string trackName, string label, int price)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO KillerApp.Track " +
                                              "VALUES(@artistName, @trackName, @label, @price)";

                        command.Parameters.AddWithValue("@artistName", artistName);
                        command.Parameters.AddWithValue("@trackName", trackName);
                        command.Parameters.AddWithValue("@label", label);
                        command.Parameters.AddWithValue("@price", price);

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

        public bool RemoveTrack(Track track)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM KillerApp.Track WHERE ID = @id";

                        command.Parameters.AddWithValue("@ID", track.Id);

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

        public bool EditTrack(Track track)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE KillerApp.Track SET " +
                                              "Artistname = @artistName," +
                                              "Trackname = @trackName," +
                                              "Label = @label," +
                                              "Price = @price " +
                                              "WHERE ID = @id";

                        command.Parameters.AddWithValue("@Artistname", track.ArtistName);
                        command.Parameters.AddWithValue("@Trackname", track.TrackName);
                        command.Parameters.AddWithValue("@label", track.Label);
                        command.Parameters.AddWithValue("@price", track.Price);
                        command.Parameters.AddWithValue("@id", track.Id);

                        return command.ExecuteNonQuery() > 0;

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
    }
}