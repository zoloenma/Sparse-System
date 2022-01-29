using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Sparse.Database
{
    public class DatabaseOperations
    {
        public int GetEffectiveCapacity()
        {
            int effectiveCapacity = 0;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select Capacity from EffectiveCapacity", con);
                effectiveCapacity = int.Parse(cmd.ExecuteScalar().ToString());
            }

            return effectiveCapacity;
        }

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

        public string GetCurrentStatus()
        {
            string status = "";

            float occupancy = GetCurrentRoomOccupancy();

            //DateTime time = DateTime.Now; 
            DateTime time = new DateTime(2022, 01, 28, 11, 10, 20); //dummy

            if (time.TimeOfDay >= new TimeSpan(7, 00, 00) && time.TimeOfDay <= new TimeSpan(20, 00, 00))
            {
                if (occupancy == 0)
                {
                    status = "EMPTY";
                }
                else if (occupancy >= 1 & occupancy <= 30)
                {
                    status = "NOT BUSY";
                }
                else if (occupancy >= 31 & occupancy <= 70)
                {
                    status = "NORMAL";
                }
                else if (occupancy >= 71 & occupancy <= 99)
                {
                    status = "BUSY";
                }
                else if (occupancy >= 100)
                {
                    status = "FULL";
                }
            }
            else
            {
                status = "CLOSED";
            }

            return status;
        }

        public string GetCurrentStatusColor()
        {
            string status = GetCurrentStatus();

            string statusColor = "";

            switch (status)
            {
                case "EMPTY": statusColor = "custom-green"; break;
                case "NOT BUSY": statusColor = "custom-lightgreen"; break;
                case "NORMAL": statusColor = "custom-yellow"; break;
                case "BUSY": statusColor = "custom-orange"; break;
                case "FULL": statusColor = "custom-red"; break;
                case "CLOSED": statusColor = "custom-darkgray"; break;

            }

            return statusColor;
        }
    }
}