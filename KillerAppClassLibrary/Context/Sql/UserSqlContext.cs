using System;
using System.Data.SqlClient;
using KillerAppClassLibrary.Classes;
using KillerAppClassLibrary.Context.Interface;

namespace KillerAppClassLibrary.Context.Sql
{
    public class UserSqlContext:IUser
    {
        private const string ConnectionString =
            "Server=mssql.fhict.local;Database=dbi388613;User Id=dbi388613;Password=wachtwoord;";

        public int RegisterUser(User user)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText =
                            "INSERT INTO KillerApp.[User](Username, Email, Password, Fund, IsAdministrator) " +
                            "VALUES(@username, @email, @password, @fund, @isAdmin)";

                        command.Parameters.AddWithValue("@username", user.Username);
                        command.Parameters.AddWithValue("@email", user.Email);
                        command.Parameters.AddWithValue("@password", user.Password);
                        command.Parameters.AddWithValue("@fund", user.Fund);
                        command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

                        return command.ExecuteNonQuery();

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return 0;

                }

                finally
                {
                    connection.Close();
                }
            }
        }

        public User Login(string email, string password)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM KillerApp.[User] " +
                                              "WHERE Email LIKE @email " +
                                              "AND Password LIKE @password";

                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);

                        User user = null;

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            user = new User
                                (
                                    Convert.ToInt32(reader["ID"]),
                                    reader["Username"].ToString(),
                                    reader["Email"].ToString(),
                                    reader["Password"].ToString(),
                                    Convert.ToInt32(reader["Fund"]),
                                    Convert.ToBoolean(reader["IsAdministrator"]) //todo koppelen aan een ordernummer.
                                );
                        }

                        connection.Close();

                        return user;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;

                }
            }
        }

        public void AddFund(User user, int fund)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE KillerApp.[User] SET Fund = @fund WHERE ID = @id";

                        command.Parameters.AddWithValue("@fund", fund);
                        command.Parameters.AddWithValue("@id", user.Id);

                        command.ExecuteNonQuery();
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
