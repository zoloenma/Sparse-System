using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sparse.Database;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient; //remove - for sql
using System.Configuration;

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
            //DateTime time = new DateTime(2022, 01, 28, 11, 10, 20); //dummy open
            //DateTime time = new DateTime(2022, 01, 28, 22, 10, 20); //dummy closed
            DateTime time = databaseOperations.GetCurrentDateTime();

            if (time.TimeOfDay >= new TimeSpan(7, 00, 00) && time.TimeOfDay <= new TimeSpan(20, 00, 00))
            {
                //Room Occupancy Percentage
                float occupancy = databaseOperations.GetCurrentRoomOccupancy();
                CurrentRoomOccupancyLbl.Text = occupancy.ToString() + "%";

                //Warning alert
                if (IsEffectiveCapacityExceeded(occupancy)) WarningAlert.Visible = true;
            }

            //Room Occupancy Status
            string status = databaseOperations.GetCurrentStatus();
            RoomStatusLbl.Text = status;

            //Room Occupancy Status Color
            string statusColor = databaseOperations.GetCurrentStatusColor();
            RoomStatusLbl.Attributes.Add("class", statusColor + " px-6 py-2 rounded-full ml-4 text-2xl");

            //Effective Capacity Count
            string capacity = databaseOperations.GetEffectiveCapacity().ToString();
            EffectiveCapacityLbl.Text = capacity;

            //Room Occupancy for Past Hour Table
            //HistoryTable.DataSource = databaseOperations.GetHistoryforPastHour();
            //HistoryTable.DataBind();
            GetHistory();

            //Average Room Occupancy Chart
            GetChartData("2");
            monBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");

            //User Email
            //dummy email
            emailLbl.Text = "lib@email.com";
        }

        public bool IsEffectiveCapacityExceeded(float occupancy)
        {
            return occupancy > 100;
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

        // will move to DatabaseOperations later
        public void GetHistory()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString()))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select [Room Occupancy]*100 as [Occupancy], [Timestamp] as [Time] from [RoomOccupancy] where [Timestamp] >= DATEADD(HOUR, -1, (SELECT CONVERT(datetime, SWITCHOFFSET(GETDATE(), DATEPART(TZOFFSET, GETDATE() AT TIME ZONE 'Singapore Standard Time'))))) and [Timestamp] <= (SELECT CONVERT(datetime, SWITCHOFFSET(GETDATE(), DATEPART(TZOFFSET, GETDATE() AT TIME ZONE 'Singapore Standard Time')))) order by [Timestamp] desc", con);
                HistoryTable.DataSource = cmd.ExecuteReader();
                HistoryTable.DataBind();
            }
        }

        // will move to DatabaseOperations later
        private void GetChartData(string day)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString()))
            {
                con.Open();

                string query = "select avg([Room Occupancy]) as Occupancy, cast(datepart(hh,[Timestamp]) as varchar) + ':00' as Time from (select [Room Occupancy]*100 as [Room Occupancy], [Timestamp] from RoomOccupancy where datepart(dw,[Timestamp]) = " + day + ") as data group by datepart(hh,[Timestamp])";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable ChartData = ds.Tables[0];
                
                string[] XPointMember = new string[ChartData.Rows.Count];
                int[] YPointMember = new int[ChartData.Rows.Count];
                for (int count = 0; count < ChartData.Rows.Count; count++)
                {
                    XPointMember[count] = ChartData.Rows[count]["Time"].ToString();
                    YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["Occupancy"]);
                }
                AverageChart.Series[0].Points.DataBindXY(XPointMember, YPointMember);

                AverageChart.Series[0].BorderWidth = 10;
                AverageChart.Series[0].ChartType = SeriesChartType.Column;
                AverageChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                AverageChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                foreach (var item in AverageChart.Series[0].Points)
                {
                    item.Color = System.Drawing.ColorTranslator.FromHtml("#022859");
                }
            }
        }

        protected void monBtn_Click(object sender, EventArgs e)
        {
            monBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            tueBtn.BorderColor = wedBtn.BorderColor = thuBtn.BorderColor = friBtn.BorderColor = satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("2");
        }

        protected void tueBtn_Click(object sender, EventArgs e)
        {
            tueBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            monBtn.BorderColor = wedBtn.BorderColor = thuBtn.BorderColor = friBtn.BorderColor = satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("3");
        }

        protected void wedBtn_Click(object sender, EventArgs e)
        {
            wedBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            monBtn.BorderColor = tueBtn.BorderColor = thuBtn.BorderColor = friBtn.BorderColor = satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("4");
        }

        protected void thuBtn_Click(object sender, EventArgs e)
        {
            thuBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            monBtn.BorderColor = tueBtn.BorderColor = wedBtn.BorderColor = friBtn.BorderColor = satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("5");
        }

        protected void friBtn_Click(object sender, EventArgs e)
        {
            friBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            monBtn.BorderColor = tueBtn.BorderColor = wedBtn.BorderColor = thuBtn.BorderColor = satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("6");
        }

        protected void satBtn_Click(object sender, EventArgs e)
        {
            satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            monBtn.BorderColor = tueBtn.BorderColor = wedBtn.BorderColor = thuBtn.BorderColor = friBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("7");
        }
    }
}