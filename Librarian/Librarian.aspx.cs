using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sparse.Database;
using System.Data.SqlClient; //remove

namespace Sparse.Librarian
{
    public partial class Librarian : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowData();
        }

        public void ShowData()
        {
            DatabaseOperations databaseOperations = new DatabaseOperations();

            WarningAlert.Visible = false;

            //DateTime time = DateTime.Now;
            DateTime time = new DateTime(2022, 01, 28, 11, 10, 20); //dummy

            if (time.TimeOfDay >= new TimeSpan(7, 00, 00) && time.TimeOfDay <= new TimeSpan(20, 00, 00))
            {
                float occupancy = databaseOperations.GetCurrentRoomOccupancy();
                CurrentRoomOccupancyLbl.Text = occupancy.ToString() + "%";

                if (occupancy > 100) WarningAlert.Visible = true;
            }

            string status = databaseOperations.GetCurrentStatus();
            RoomStatusLbl.Text = status;

            string statusColor = databaseOperations.GetCurrentStatusColor();
            RoomStatusLbl.Attributes.Add("class", statusColor + " px-6 py-2 rounded-full ml-4 text-2xl");

            string capacity = databaseOperations.GetEffectiveCapacity().ToString();
            EffectiveCapacityLbl.Text = capacity;

            //HistoryTable.DataSource = databaseOperations.GetHistoryforPastHour();
            //HistoryTable.DataBind();
            GetHistory();

            //dummy email
            emailLbl.Text = "librarian@mcl.edu.ph";
        }

        protected void ChangeBtn_Click(object sender, EventArgs e)
        {
            int capacity = int.Parse(capacityTB.Text);

            DatabaseOperations databaseOperations = new DatabaseOperations();

            databaseOperations.UpdateEffectiveCapacity(capacity);

            EffectiveCapacityLbl.Text = capacity.ToString();
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Librarian/LogIn.aspx");
        }

        public void GetHistory()
        {
            using (SqlConnection con = new SqlConnection("Server=tcp:***REMOVED***,1433;Initial Catalog=sparse;User ID=***REMOVED***;Password=***REMOVED***"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select [Room Occupancy] as Occupancy, [Timestamp] as [Time] from RoomOccupancy where [Timestamp] >= DATEADD(HOUR, -1, GETDATE())", con);
                HistoryTable.DataSource = cmd.ExecuteReader();
                HistoryTable.DataBind();
            }
        }
    }
}