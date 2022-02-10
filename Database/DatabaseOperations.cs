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

                SqlCommand cmd = new SqlCommand("select [Room Occupancy Percentage] from RoomOccupancy where ID=(select max(ID) from RoomOccupancy)", con);
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

        public int[] GetPeakHours()
        {
            List<int> PeakHoursList = new List<int>();
            int[] PeakHours;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT TOP 4 DATEPART(HOUR, [Timestamp]) FROM [dbo].[RoomOccupancy] WHERE [Room Occupancy Percentage] > 0.60 AND [Timestamp] > (SELECT DATEADD(WEEK, -1, GETDATE())) ORDER BY [Room Occupancy Percentage] DESC;", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PeakHoursList.Add(reader.GetInt32(0));
                    }
                }
            }

            PeakHours = PeakHoursList.ToArray();
            return PeakHours;
        }

        public decimal GetAverageCrowded()
        {
            decimal average = 0;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT AVG([Room Occupancy Count]) FROM [dbo].[RoomOccupancy] WHERE [Timestamp] > (SELECT DATEADD(DAY, -1, GETDATE()));", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        average = reader.GetDecimal(0);
                    }
                }
            }

            return average;
        }

        public string GetOpeningTime()
        {
            string openingTime = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT [OpeningHour] FROM [dbo].[CLIRSchedule]", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        openingTime = reader.GetString(0);
                    }
                }
            }

            return openingTime;
        }

        public string GetClosingTime()
        {
            string closingTime = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT [ClosingHour] FROM [dbo].[CLIRSchedule]", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        closingTime = reader.GetString(0);
                    }
                }
            }

            return closingTime;
        }

        public string GetOpeningDay()
        {
            string openingDay = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT [OpeningDay] FROM [dbo].[CLIRSchedule]", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        openingDay = reader.GetString(0);
                    }
                }
            }

            return openingDay;
        }

        public string GetClosingDay()
        {
            string closingDay = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT [ClosingDay] FROM [dbo].[CLIRSchedule]", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        closingDay = reader.GetString(0);
                    }
                }
            }

            return closingDay;
        }
    }
}