using Microsoft.Data.SqlClient;
using Roommates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roomates.Repositories
{
    internal class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionstring) : base(connectionstring) { }

        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT rm.FirstName, rm.RentPortion, r.[Name]
                                        FROM Roommate rm
                                        LEFT JOIN Room r on r.Id = rm.RoomID";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;

                    if (reader.Read())
                    {
                        roommate = new Roommate
                        {
                            Id = id,
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            //Room.Name = reader.GetString(reader.GetOrdinal("Name")),
                        };
                    }
                    reader.Close();
                    
                    return roommate;
                }
            }
        }

    }
}
