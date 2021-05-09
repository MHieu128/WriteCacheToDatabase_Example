using Example1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Example1.DAL
{
    public class DbContext
    {
        private readonly string _sqlconnectstring = @"Data Source=NFLaptop;Initial Catalog=UserManager;Integrated Security=True";

        public SqlConnection ConnectDb(string connectionStr)
        {
            var conn = new SqlConnection(connectionStr);
            conn.Open();

            return conn;
        }

        public List<User> GetInfoUser()
        {
            List<User> users = new List<User>();

            using (var command = new SqlCommand())
            {
                command.Connection = ConnectDb(_sqlconnectstring);
                
                string queryString = "select *from USERS";
                command.CommandText = queryString;
                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = Convert.ToInt32(reader[0]),
                            Name = Convert.ToString(reader[1]),
                            Email = Convert.ToString(reader[2]),
                            Address = Convert.ToString(reader[3]),
                            NumberPhone = Convert.ToInt32(reader[4])
                        });
                    }
                }
            }

            return users;
        }
    }
}
