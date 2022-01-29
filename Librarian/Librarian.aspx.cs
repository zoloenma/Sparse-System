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
            DatabaseOperations databaseOperations = new DatabaseOperations();
            string occupancy = databaseOperations.GetCurrentRoomOccupancy().ToString();
            CurrentRoomOccupancy.Text = occupancy;
        }
    }
}