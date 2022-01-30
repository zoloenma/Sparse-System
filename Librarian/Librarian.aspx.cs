using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sparse.Database;

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

            float occupancy = databaseOperations.GetCurrentRoomOccupancy();
            CurrentRoomOccupancyLbl.Text = occupancy.ToString("0.##") + "%";

            string status = databaseOperations.GetCurrentStatus();
            RoomStatusLbl.Text = status;

            string statusColor = databaseOperations.GetCurrentStatusColor();
            RoomStatusLbl.Attributes.Add("class", statusColor + " px-6 py-2 rounded-full ml-4 text-2xl");

            string capacity = databaseOperations.GetEffectiveCapacity().ToString();
            EffectiveCapacityLbl.Text = capacity;

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
    }
}