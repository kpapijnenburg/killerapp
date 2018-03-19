using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Sql
{
    public class OrderSqlContext: IOrderContext
    {
        
        private const string ConnectionString =
            "Server=mssql.fhict.local;Database=dbi388613;User Id=dbi388613;Password=wachtwoord;";

        public bool CreateNewOrder(Order order)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO KillerApp.[Order] (UserID, OrderDate, Finished) VALUES(@userId, @orderDate, @finished)";
                        command.Parameters.AddWithValue("@userId", order.User.Id);
                        command.Parameters.AddWithValue("@orderDate", order.OrderDate);
                        command.Parameters.AddWithValue("@finished", 1);

                        bool result = command.ExecuteNonQuery() > 0;

                        connection.Close();

                        InsertTracksIntoOrder(order);

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

        private void InsertTracksIntoOrder(Order order)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        var trackIds = new List<int>();

                        trackIds.AddRange(order.Tracks.Select(track => track.Id));

                        trackIds.ToArray();

                        foreach (int trackId in trackIds)
                        {
                            connection.Close();

                            command.CommandText = "INSERT INTO KillerApp.Order_Track(OrderID, TrackID) VALUES(@OrderId, @trackId)";
                            command.Parameters.AddWithValue("@OrderId", order.Id);
                            command.Parameters.AddWithValue("@trackId", trackId);

                            connection.Open();

                            command.ExecuteNonQuery();

                            command.Parameters.Clear();
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
        }

    }
}