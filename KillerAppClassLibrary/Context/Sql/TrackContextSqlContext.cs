using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Sql
{
    public class TrackContextSqlContext:ITrackContext
    {
        private const string ConnectionString =
            "Server=mssql.fhict.local;Database=dbi388613;User Id=dbi388613;Password=wachtwoord;";

        public IEnumerable<Track> GetAllTracks()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM KillerApp.Track";

                        var reader = command.ExecuteReader();

                        var tracks = new List<Track>();

                        while (reader.Read())
                        {
                            tracks.Add(new Track
                                (
                                    Convert.ToInt32(reader["ID"]),
                                    reader["Artistname"].ToString(),
                                    reader["Trackname"].ToString(),
                                    reader["Label"].ToString(),
                                    Convert.ToInt32(reader["Price"])
                                ));
                        }

                        connection.Close();

                        return tracks;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    return null;
                }
            }
        }

        public IEnumerable<Track> GetMostDownloadedTracks()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT COUNT(TrackId) as TotalDownloads, Trackid, KillerApp.Track.Artistname, KillerApp.Track.Trackname, KillerApp.Track.Label, KillerApp.Track.Price " +
                                              "FROM KillerApp.Order_Track ot " +
                                              "INNER JOIN KillerApp.Track ON ot.TrackID = KillerApp.Track.ID " +
                                              "GROUP BY Trackid, Artistname, Trackname, Label, Price " +
                                              "ORDER BY TotalDownloads DESC";

                        var reader = command.ExecuteReader();

                        var tracks = new List<Track>();

                        while (reader.Read())
                        {
                            tracks.Add(new Track
                            (
                                Convert.ToInt32(reader["Trackid"]),
                                reader["Artistname"].ToString(),
                                reader["Trackname"].ToString(),
                                reader["Label"].ToString(),
                                Convert.ToInt32(reader["Price"])
                            ));
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
    }
}