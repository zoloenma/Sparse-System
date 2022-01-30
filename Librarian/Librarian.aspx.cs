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

            string occupancy = databaseOperations.GetCurrentRoomOccupancy().ToString();
            CurrentRoomOccupancyLbl.Text = occupancy + "%";

            string status = databaseOperations.GetCurrentStatus();
            RoomStatusLbl.Text = status;

            string statusColor = databaseOperations.GetCurrentStatusColor();
            RoomStatusLbl.Attributes.Add("class", statusColor + " px-6 py-2 rounded-full ml-4 text-2xl");

            string capacity = databaseOperations.GetEffectiveCapacity().ToString();
            EffectiveCapacityLbl.Text = capacity;

            //dummy email
            emailLbl.Text = "librarian@mcl.edu.ph";
        }
    }
}