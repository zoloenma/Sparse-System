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

            GetChartData("MON");
            //monBtn.BackColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            //monBtn.ForeColor = System.Drawing.Color.White;
            monBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");

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

        // will move to DatabaseOperations later
        public void GetHistory()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString()))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select [Room Occupancy] as Occupancy, [Timestamp] as [Time] from RoomOccupancy where [Timestamp] >= DATEADD(HOUR, -1, GETDATE())", con);
                HistoryTable.DataSource = cmd.ExecuteReader();
                HistoryTable.DataBind();
            }
        }

        // will move to DatabaseOperations later
        private void GetChartData(string day)
        {
            //dummy - will change later
            string date = "";
            switch (day)
            {
                case "MON": date = "24"; break;
                case "TUE": date = "25"; break;
                case "WED": date = "26"; break;
                case "THU": date = "27"; break;
                case "FRI": date = "28"; break;
                case "SAT": date = "29"; break;
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString()))
            {
                con.Open();

                string query = "select avg(RoomOccupancy) as Occupancy, cast(datepart(hh,[Timestamp]) as varchar) + ':00' as Time from (select [Room Occupancy]*100 as RoomOccupancy, [Timestamp] from RoomOccupancy where [Timestamp] > '2022-01-" + date + " 00:00:00' and [Timestamp] < '2022-01-" + date + " 23:59:59') as data group by datepart(hh,[Timestamp])";

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

            GetChartData("MON");
        }

        protected void tueBtn_Click(object sender, EventArgs e)
        {
            tueBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            monBtn.BorderColor = wedBtn.BorderColor = thuBtn.BorderColor = friBtn.BorderColor = satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("TUE");
        }

        protected void wedBtn_Click(object sender, EventArgs e)
        {
            wedBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            monBtn.BorderColor = tueBtn.BorderColor = thuBtn.BorderColor = friBtn.BorderColor = satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("WED");
        }

        protected void thuBtn_Click(object sender, EventArgs e)
        {
            thuBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            monBtn.BorderColor = tueBtn.BorderColor = wedBtn.BorderColor = friBtn.BorderColor = satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("THU");
        }

        protected void friBtn_Click(object sender, EventArgs e)
        {
            friBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            monBtn.BorderColor = tueBtn.BorderColor = wedBtn.BorderColor = thuBtn.BorderColor = satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("FRI");
        }

        protected void satBtn_Click(object sender, EventArgs e)
        {
            satBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#657A8C");
            monBtn.BorderColor = tueBtn.BorderColor = wedBtn.BorderColor = thuBtn.BorderColor = friBtn.BorderColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");

            GetChartData("SAT");
        }
    }
}