using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sparse.Database;
namespace Sparse.Student
{
    public partial class Student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DatabaseOperations databaseOperations = new DatabaseOperations();

            //DateTime time = DateTime.Now; 
            //DateTime time = new DateTime(2022, 01, 28, 11, 10, 20); //dummy
            DateTime time = databaseOperations.GetCurrentDateTime();

            if (time.TimeOfDay >= new TimeSpan(7, 00, 00) && time.TimeOfDay <= new TimeSpan(20, 00, 00))
            {
                float percent = databaseOperations.GetCurrentRoomOccupancy();
                //float percent = 0; //dummy
                percentage.Text = percent.ToString("n2") + "% ";
                if (percent > 100)
                {
                    circlePercentage.Style["stroke-dasharray"] = "754.285714286";
                    circlePercentage.Style["stroke-dashoffset"] = "0";
                }
                else
                {
                    double circumference = 754.285714286;
                    double circumferencePercentage = circumference - percent / 100 * circumference;
                    circlePercentage.Style["stroke-dasharray"] = circumference.ToString();
                    circlePercentage.Style["stroke-dashoffset"] = circumferencePercentage.ToString();
                }
            }
            else circlePercentage.Attributes.Add("class", "text-custom-darkgray");

            RoomStatus.Text = databaseOperations.GetCurrentStatus();

            string statusColor = databaseOperations.GetCurrentStatusColor();
            string statusCss = "inline-flex " + statusColor + " text-black rounded-full h-24 w-40 md:text-3xl justify-center items-center";
            RoomStatus.Attributes.Add("class", statusCss);

            // ---------- HCI Portion ----------
            if (RoomStatus.Text == "EMPTY" || RoomStatus.Text == "NOT BUSY")
            {
                statusHCI_1.Text = "We have room for everyone!";
                statusHCI_2.Text = "Many seats are available as of the moment, feel free to come in!";
                statusContainer.Style.Add("background-color", "#95e6a0");
            }
            else if (RoomStatus.Text == "NORMAL")
            {
                statusHCI_1.Text = "Some seats are available!";
                statusHCI_2.Text = "The room capacity is normal, come and take a seat!";
                statusContainer.Style.Add("background-color", "#ededa1");
            }
            else if (RoomStatus.Text == "BUSY")
            {
                statusHCI_1.Text = "The room is pretty crowdy!";
                statusHCI_2.Text = "Take precaution, there are only few seats available as of the moment!";
                statusContainer.Style.Add("background-color", "#db8b4d");
            }
            else
            {
                statusHCI_1.Text = "No more seats available!";
                statusHCI_2.Text = "The room is currently packed! Please wait for others to leave the premises.";
                statusContainer.Style.Add("background-color", "#bf5252");
            }
            // ---------- End of HCI Portion ----------
        }
    }
}