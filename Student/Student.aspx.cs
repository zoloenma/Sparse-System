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
            //gettimedatabase
            DateTime time = new DateTime(2015, 12, 31, 22, 10, 20); //dummy

            if (time.TimeOfDay >= new TimeSpan(7, 00, 00) && time.TimeOfDay <= new TimeSpan(20, 00, 00))
            {
                //getpercentdatabase
                double percent = 20; //dummy
                percentage.Text = percent.ToString() + "%";
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

                string statusColor = "";

                if (percent == 0)
                {
                    RoomStatus.Text = "EMPTY";
                    statusColor = "bg-green";
                }
                else if (percent >= 1 & percent <= 30)
                {
                    RoomStatus.Text = "NOT BUSY";
                    statusColor = "bg-lightgreen";
                }
                else if (percent >= 31 & percent <= 70)
                {
                    RoomStatus.Text = "NORMAL";
                    statusColor = "bg-yellow";
                }
                else if (percent >= 71 & percent <= 99)
                {
                    RoomStatus.Text = "BUSY";
                    statusColor = "bg-orange";
                }
                else if (percent >= 100)
                {
                    RoomStatus.Text = "FULL";
                    statusColor = "bg-red";
                }

                string statusCss = "inline-flex " + statusColor + " text-black rounded-full h-24 w-40 md:text-3xl justify-center items-center";
                RoomStatus.Attributes.Add("class", statusCss);
            }
            else
            {
                RoomStatus.Text = "CLOSED";
                RoomStatus.Attributes.Add("class", "inline-flex bg-darkgray text-black rounded-full h-24 w-40 md:text-3xl justify-center items-center");
                circlePercentage.Style["stroke"] = "darkgray";
            }
        }
    }
}