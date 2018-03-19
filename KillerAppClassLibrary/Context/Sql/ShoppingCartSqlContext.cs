using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Sql
{
    public class ShoppingCartSqlContext : IShoppingCart
    {
        private const string ConnectionString =
            "Server=mssql.fhict.local;Database=dbi388613;User Id=dbi388613;Password=wachtwoord;";

        public bool PutInShoppingCart(Track track, User user)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO KillerApp.Shoppingcart " +
                                              "VALUES(@userId, @trackId)";
                        command.Parameters.AddWithValue("@userId", user.Id);
                        command.Parameters.AddWithValue("@trackId", track.Id);

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

        public IEnumerable<Track> GetShoppingcartItems(User user)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT KillerApp.Track.ID, Artistname, Trackname, Label, Price " +
                                              "FROM KillerApp.Track " +
                                              "INNER JOIN KillerApp.Shoppingcart " +
                                              "ON Shoppingcart.TrackId = KillerApp.Track.ID " +
                                              "WHERE Shoppingcart.UserId = @userId";

                        command.Parameters.AddWithValue("@userId", user.Id);

                        var reader = command.ExecuteReader();

                        var tracks = new List<Track>();

                        while (reader.Read())
                        {
                            var track = new Track
                            (
                                Convert.ToInt32(reader["ID"]),
                                reader["Artistname"].ToString(),
                                reader["Trackname"].ToString(),
                                reader["Label"].ToString(),
                                Convert.ToInt32(reader["Price"])

                            );

                            tracks.Add(track);
                        }

                        return tracks;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        public bool RemoveAllItemsFromShoppingCart(User user)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM KillerApp.Shoppingcart WHERE UserId = @userId";
                        command.Parameters.AddWithValue("@userId", user.Id);

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

        public bool RemoveItemFromShoppingCart(User user, Track track)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText ="DELETE FROM KillerApp.Shoppingcart " +
                                             "WHERE UserId = @userId AND TrackId = @trackId";

                        command.Parameters.AddWithValue("@userId", user.Id);
                        command.Parameters.AddWithValue("@trackId", track.Id);

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