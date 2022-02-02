using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Sparse.Database
{
    public class DatabaseOperations
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();

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
            roomOccupancy = (float)Math.Round(roomOccupancy * 100f) / 100f; //round off to 2 decimal places
            return roomOccupancy;
        }

        public string GetCurrentStatus()
        {
            string status = "";

            float occupancy = GetCurrentRoomOccupancy();

            //DateTime time = DateTime.Now;
            //DateTime time = new DateTime(2022, 01, 28, 11, 10, 20); //dummy open
            //DateTime time = new DateTime(2022, 01, 28, 22, 10, 20); //dummy closed
            DateTime time = GetCurrentDateTime();

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
                case "EMPTY": statusColor = "bg-custom-green"; break;
                case "NOT BUSY": statusColor = "bg-custom-lightgreen"; break;
                case "NORMAL": statusColor = "bg-custom-yellow"; break;
                case "BUSY": statusColor = "bg-custom-orange"; break;
                case "FULL": statusColor = "bg-custom-red"; break;
                case "CLOSED": statusColor = "bg-custom-darkgray"; break;

            }

            return statusColor;
        }

        public DateTime GetCurrentDateTime()
        {
            DateTime currentDateTime = DateTime.Now;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT CONVERT(datetime, SWITCHOFFSET(GETDATE(), DATEPART(TZOFFSET, GETDATE() AT TIME ZONE 'Singapore Standard Time')))", con);
                currentDateTime = DateTime.Parse(cmd.ExecuteScalar().ToString());
            }

            return currentDateTime;
        }
    }
}