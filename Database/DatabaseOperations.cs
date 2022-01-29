using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Sparse.Database
{
    public class DatabaseOperations
    {
        public string ConnectionString = "Server=tcp:***REMOVED***,1433;Initial Catalog=sparse;User ID=***REMOVED***;Password=***REMOVED***";

        public void UpdateEffectiveCapacity(int newEffectiveCapacity)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("update EffectiveCapacity set Capacity = " + newEffectiveCapacity.ToString(), con);
                cmd.ExecuteNonQuery();
            }
        }

        public float GetCurrentRoomOccupancy()
        {
            float roomOccupancy = 0;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select [Room Occupancy] from RoomOccupancy where ID=(select max(ID) from RoomOccupancy)", con);
                roomOccupancy = float.Parse(cmd.ExecuteScalar().ToString());
            }

            roomOccupancy = roomOccupancy * 100;
            return roomOccupancy;
        }
    }
}