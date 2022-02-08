using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class Statistics : Form
    {
        MySqlComponents MySS;
        List<Double> xValues = new List<double>();
        List<Double> yValues = new List<double>();
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkBook;
        private Excel.Worksheet xlWorkSheet;

        public Statistics()
        {
            InitializeComponent();
        }
        private void Statistics_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.Statistics_ToLight(this.tabControl);
                else
                    myTheme.Statistics_ToNight(this.tabControl);

                MySS = new MySqlComponents();

                tabControl.SelectedIndex = 0;
                Project_Status_BindChart("", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region bind to comboBox
        private DataTable Category_bind()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select C_ID,C_Name from category";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.reader = MySS.sc.ExecuteReader();
            MySS.dt = new DataTable();
            MySS.dt.Columns.Add("C_ID", typeof(string));
            MySS.dt.Columns.Add("C_Name", typeof(string));
            MySS.dt.Load(MySS.reader);
            return MySS.dt;
        }
        #endregion 

        #region bind to chart functions and queries
        private void Project_Status_BindChart(string costFrom,string costTo)
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "Select CASE MP_State WHEN 0 THEN N'Rejected' WHEN 1 THEN N'Accepted' WHEN 2 THEN N'Delayed' WHEN 4 THEN N'Financed' WHEN 5 THEN N'Closed' ELSE N'On Hold' End as 'Project State'"+
                    ", count(MP_ID) as numOfProjects FROM `microproject` ";
                string condition = "";

                //      Cost        //
                if (costFrom != "")
                    if (costTo != "")
                        condition += " where (MP_AllPriceNeeded between '" + costFrom + "' and '" + costTo + "') ";
                    else
                        condition += " where (MP_AllPriceNeeded between '" + costFrom + "' and '" + costFrom + "' )";

                string GroupBycondition = "";
                GroupBycondition += " group by MP_State ";

                MySS.query += condition + GroupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;

                //check connection//
                Program.buildConnection();

                //count all
                string sel = "select count(MP_ID) from microproject as count ";
                sel += condition;

                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                AllResultCount_textBox.Text = MySS.sc.ExecuteScalar().ToString();

                foreach (var series in chart1.Series)
                {   //clear chart
                    series.Points.Clear();
                }
                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                {   //fill chart
                    this.chart1.Series["Projects"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                }
                chart1.ChartAreas[0].AxisY.Title = "Projects";
                chart1.ChartAreas[0].AxisX.Title = "Project Status";
                chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart1.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart1.ChartAreas[0].AxisX.Interval = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Project_Status_BindChart(DateTime applyFrom, DateTime applyTo)     //use it only when updating date
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "Select CASE MP_State WHEN 0 THEN N'Rejected' WHEN 1 THEN N'Accepted' WHEN 2 THEN N'Delayed' WHEN 4 THEN N'Financed' WHEN 5 THEN N'Closed' ELSE N'On Hold' End as 'Project State'"+
                    ", count(MP_ID) as numOfProjects FROM `microproject` ";
                string condition = "";

                //     Apply Date        //
                string dateFrom = MP_ApplyDateFrom_dateTimePicker.Value.Year.ToString() + "-" + MP_ApplyDateFrom_dateTimePicker.Value.Month.ToString() + "-" + MP_ApplyDateFrom_dateTimePicker.Value.Day.ToString() + "";
                string dateTo = MP_ApplyDateTo_dateTimePicker.Value.Year.ToString() + "-" + MP_ApplyDateTo_dateTimePicker.Value.Month.ToString() + "-" + MP_ApplyDateTo_dateTimePicker.Value.Day.ToString() + "";
                condition += " where (MP_DateOfRequest between '" + dateFrom + "' and '" + dateTo + "') ";

                string GroupByCondition = "";
                GroupByCondition += " group by MP_State ";

                MySS.query += condition + GroupByCondition;
                
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                //check connection//
                Program.buildConnection();

                //count all
                string sel = "select count(MP_ID) from microproject as count";
                sel += condition;

                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                AllResultCount_textBox.Text = MySS.sc.ExecuteScalar().ToString();

                foreach (var series in chart1.Series)
                {   //clear chart
                    series.Points.Clear();
                }
                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                {   //fill chart
                    this.chart1.Series["Projects"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                }
                chart1.ChartAreas[0].AxisY.Title = "Projects";
                chart1.ChartAreas[0].AxisX.Title = "Project Status";
                chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart1.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart1.ChartAreas[0].AxisX.Interval = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_Category_BindChart(string fundedBy, string parish)
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "select category.C_Name , count(MP_ID) as numOfProjects "
                    + " FROM `microproject` left outer join `category` on microproject.MP_Category_ID = category.C_ID " +
                    " right join person_microproject PMP on PMP.MicroProject_ID = microproject.MP_ID "
                                     + " left outer join person p on p.P_ID = PMP.Person_ID "; 
                string condition = "";
                //Project State
                if (MP_Accepted_radioButton3.Checked)
                    condition += " where MP_State = 1";
                else if (MP_NotFinanced_radioButton3.Checked)
                    condition += " where MP_State = 0";
                else if (MP_Delayed_radioButton3.Checked)
                    condition += " where MP_State = 2";
                else if (MP_Hold_radioButton3.Checked)
                    condition += " where MP_State = 3";
                else if (MP_Financed_radioButton3.Checked)
                    condition += " where MP_State = 4";
                else if (MP_Closed_radioButton3.Checked)
                    condition += " where MP_State = 5";
                else condition += "";
                //funded by
                if (fundedBy != "")
                    if (condition == "")
                        condition += " where MP_ResonOfPlace like N'" + fundedBy + "'";
                    else
                        condition += " and MP_ResonOfPlace like N'" + fundedBy + "'";
                //parish
                if (parish != "")
                    if (condition == "")
                        condition += " where p.P_Parish like N'" + parish + "'";
                    else
                        condition += " and p.P_Parish like N'" + parish + "'";

                
                string  groupBycondition = " group by category.C_Name ";
                MySS.query += condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                //check connection//
                Program.buildConnection();

                //count all
                string sel = "select count(MP_ID) FROM `microproject` left outer join `category` on microproject.MP_Category_ID = category.C_ID " +
                    " right join person_microproject PMP on PMP.MicroProject_ID = microproject.MP_ID "
                                     + " left outer join person p on p.P_ID = PMP.Person_ID ";
                sel += condition;

                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                AllResultCount_textBox3.Text = MySS.sc.ExecuteScalar().ToString();

                foreach (var series in chart3.Series)
                {   //clear chart
                    series.Points.Clear();
                }
                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                {   //fill chart
                    this.chart3.Series["Categories"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                }
                chart3.ChartAreas[0].AxisY.Title = "Projects";
                chart3.ChartAreas[0].AxisX.Title = "Categories";
                chart3.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart3.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart3.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart3.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart3.ChartAreas[0].AxisX.Interval = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Project_Parish_BindChart(string fundedBy, string category)
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "Select p.P_Parish , count(MP_ID) as numOfProjects " +
                    "FROM `Person` p right outer join `person_microproject` pmp on pmp.Person_ID = p.P_ID " +
                    "left outer join `microproject` mp on pmp.MicroProject_ID = mp.MP_ID left outer join `category` C on mp.MP_Category_ID = C.C_ID ";
                string condition = "";
                //Project State
                if (MP_Accepted_radioButton4.Checked)
                    condition += " where MP_State = 1";
                else if (MP_NotFinanced_radioButton4.Checked)
                    condition += " where MP_State = 0";
                else if (MP_Delayed_radioButton4.Checked)
                    condition += " where MP_State = 2";
                else if (MP_Hold_radioButton4.Checked)
                    condition += " where MP_State = 3";
                else if (MP_Financed_radioButton4.Checked)
                    condition += " where MP_State = 4";
                else if (MP_Closed_radioButton4.Checked)
                    condition += " where MP_State = 5";
                else condition += "";

                //funded by
                if (fundedBy != "")
                    if (condition == "")
                        condition += " where MP_ResonOfPlace like N'" + fundedBy + "'";
                    else
                        condition += " and MP_ResonOfPlace like N'" + fundedBy + "'";
                //category
                if (category != "")
                    if (condition == "")
                        condition += " where C.C_Name like N'%" + category + "%'";
                    else
                        condition += " and C.C_Name like N'%" + category + "%'";

                string groupBycondition = " group by p.P_Parish ";
                MySS.query += condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                //check connection//
                Program.buildConnection();

                //count all
                string sel = "select count(MP_ID) as count from microproject left outer join category C on microproject.MP_Category_ID = C.C_ID ";
                sel += condition;

                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                AllResultCount_textBox4.Text = MySS.sc.ExecuteScalar().ToString();

                foreach (var series in chart4.Series)
                {   //clear chart
                    series.Points.Clear();
                }
                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                {   //fill chart
                    this.chart4.Series["Parishes"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                }
                chart4.ChartAreas[0].AxisY.Title = "Projects";
                chart4.ChartAreas[0].AxisX.Title = "Parishes";
                chart4.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart4.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart4.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart4.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart4.ChartAreas[0].AxisX.Interval = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Project_Fund_BindChart(string parish,string category)
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "Select MP_ResonOfPlace , count(MP_ID) as numOfProjects " +
                     "FROM `Person` p right outer join `person_microproject` pmp on pmp.Person_ID = p.P_ID " +
                     "left outer join `microproject` mp on pmp.MicroProject_ID = mp.MP_ID left outer join `category` C on mp.MP_Category_ID = C.C_ID ";
                string condition = "";
                //Project State
                if (MP_Accepted_radioButton5.Checked)
                    condition += " where MP_State = 1";
                else if (MP_NotFinanced_radioButton5.Checked)
                    condition += " where MP_State = 0";
                else if (MP_Delayed_radioButton5.Checked)
                    condition += " where MP_State = 2";
                else if (MP_Hold_radioButton5.Checked)
                    condition += " where MP_State = 3";
                else if (MP_Financed_radioButton5.Checked)
                    condition += " where MP_State = 4";
                else if (MP_Closed_radioButton5.Checked)
                    condition += " where MP_State = 5";
                else condition += "";

                //parish
                if (parish != "")
                    if (condition == "")
                        condition += " where p.P_Parish like N'" + parish + "'";
                    else
                        condition += " and p.P_Parish like N'" + parish + "'";
                //category
                if (category != "")
                    if (condition == "")
                        condition += " where C.C_Name like N'%" + category + "%'";
                    else
                        condition += " and C.C_Name like N'%" + category + "%'";

                string groupBycondition = " group by MP_ResonOfPlace ";
                MySS.query += condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                //check connection//
                Program.buildConnection();

                //count all
                string sel = "select count(MP_ID) as count FROM `microproject` left outer join `category` C on microproject.MP_Category_ID = C.C_ID " +
                    " right join person_microproject PMP on PMP.MicroProject_ID = microproject.MP_ID "
                                     + " left outer join person p on p.P_ID = PMP.Person_ID ";
                sel += condition;

                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                AllResultCount_textBox5.Text = MySS.sc.ExecuteScalar().ToString();


                foreach (var series in chart5.Series)
                {   //clear chart
                    series.Points.Clear();
                }
                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                {   //fill chart
                    this.chart5.Series["Donors"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                }
                chart5.ChartAreas[0].AxisY.Title = "Projects";
                chart5.ChartAreas[0].AxisX.Title = "Donors";
                chart5.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart5.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart5.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart5.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart5.ChartAreas[0].AxisX.Interval = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        
        private void Project_MilitaryServices_BindChart(string parish, string category, string fundedBy) {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "SELECT P_MilitaryService , COUNT(MP_ID) as numOfProjects " +
                                " from person_microproject PMP left outer join person p on p.P_ID = PMP.Person_ID " +
                                " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID " +
                                " left outer join category C on MP.MP_Category_ID = C.C_ID ";
                string condition = "";
                //Project State
                if (MP_Accepted_radioButton6.Checked)
                    condition += " where MP_State = 1";
                else if (MP_NotFinanced_radioButton6.Checked)
                    condition += " where MP_State = 0";
                else if (MP_Delayed_radioButton6.Checked)
                    condition += " where MP_State = 2";
                else if (MP_Hold_radioButton6.Checked)
                    condition += " where MP_State = 3";
                else if (MP_Financed_radioButton6.Checked)
                    condition += " where MP_State = 4";
                else if (MP_Closed_radioButton6.Checked)
                    condition += " where MP_State = 5";
                else condition += "";

                //funded by
                if (fundedBy != "")
                    if (condition == "")
                        condition += " where MP_ResonOfPlace like N'" + fundedBy + "'";
                    else
                        condition += " and MP_ResonOfPlace like N'" + fundedBy + "'";
                //parish
                if (parish != "")
                    if (condition == "")
                        condition += " where P_Parish like N'" + parish + "'";
                    else
                        condition += " and P_Parish like N'" + parish + "'";
                //category
                if (category != "")
                    if (condition == "")
                        condition += " where C.C_Name like N'%" + category + "%'";
                    else
                        condition += " and C.C_Name like N'%" + category + "%'";

                string groupBycondition = " group by P_MilitaryService ";
                MySS.query += condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                //check connection//
                Program.buildConnection();

                //count the results
                string sel = "select count(MP_ID) " +
                                " from person_microproject PMP left outer join person p on p.P_ID = PMP.Person_ID " +
                                " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID " +
                                " left outer join category C on MP.MP_Category_ID = C.C_ID ";
                sel += condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                AllResultCount_textBox6.Text = MySS.sc.ExecuteScalar().ToString();

                foreach (var series in chart6.Series)
                {
                    series.Points.Clear();
                }
                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    this.chart6.Series["Military Service"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                }
                chart6.ChartAreas[0].AxisY.Title = "Beneficiaries";
                chart6.ChartAreas[0].AxisX.Title = "Military Service";
                chart6.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart6.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart6.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart6.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart6.ChartAreas[0].AxisX.Interval = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Project_Gender_BindChart(string parish, string category, string fundedBy) {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "SELECT P_Sex , COUNT(MP_ID) as numOfProjects " +
                                " from person_microproject PMP left outer join person p on p.P_ID = PMP.Person_ID " + 
                                " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID " +
                                " left outer join category C on MP.MP_Category_ID = C.C_ID ";
                string condition = "";
                //Project State
                if (MP_Accepted_radioButton6.Checked)
                    condition += " where MP_State = 1";
                else if (MP_NotFinanced_radioButton6.Checked)
                    condition += " where MP_State = 0";
                else if (MP_Delayed_radioButton6.Checked)
                    condition += " where MP_State = 2";
                else if (MP_Hold_radioButton6.Checked)
                    condition += " where MP_State = 3";
                else if (MP_Financed_radioButton6.Checked)
                    condition += " where MP_State = 4";
                else if (MP_Closed_radioButton6.Checked)
                    condition += " where MP_State = 5";
                else condition += "";

                //funded by
                if (fundedBy != "")
                    if (condition == "")
                        condition += " where MP_ResonOfPlace like N'" + fundedBy + "'";
                    else
                        condition += " and MP_ResonOfPlace like N'" + fundedBy + "'";
                //parish
                if (parish != "")
                    if (condition == "")
                        condition += " where P_Parish like N'" + parish + "'";
                    else
                        condition += " and P_Parish like N'" + parish + "'";
                //category
                if (category != "")
                    if (condition == "")
                        condition += " where C.C_Name like N'%" + category + "%'";
                    else
                        condition += " and C.C_Name like N'%" + category + "%'";

                string groupBycondition = " group by P_Sex ";
                MySS.query += condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                //check connection//
                Program.buildConnection();

                //count the results
                string sel = "select count(MP_ID) " +
                                " from person_microproject PMP left outer join person p on p.P_ID = PMP.Person_ID " +
                                " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID " +
                                " left outer join category C on MP.MP_Category_ID = C.C_ID ";
                sel += condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                AllResultCount_textBox6.Text = MySS.sc.ExecuteScalar().ToString();
                foreach (var series in chart6.Series)
                {
                    series.Points.Clear();
                }
                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                { 
                    this.chart6.Series["Gender"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                }
                chart6.ChartAreas[0].AxisY.Title = "Beneficiaries";
                chart6.ChartAreas[0].AxisX.Title = "Gender";
                chart6.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart6.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart6.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart6.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart6.ChartAreas[0].AxisX.Interval = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Project_MaritalStatus_BindChart(string parish, string category, string fundedBy)
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "SELECT P_MaritalStatus , COUNT(MP_ID) as numOfProjects " +
                                " from person_microproject PMP left outer join person p on p.P_ID = PMP.Person_ID " +
                                " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID " +
                                " left outer join category C on MP.MP_Category_ID = C.C_ID ";
                string condition = "";
                //Project State
                if (MP_Accepted_radioButton6.Checked)
                    condition += " where MP_State = 1";
                else if (MP_NotFinanced_radioButton6.Checked)
                    condition += " where MP_State = 0";
                else if (MP_Delayed_radioButton6.Checked)
                    condition += " where MP_State = 2";
                else if (MP_Hold_radioButton6.Checked)
                    condition += " where MP_State = 3";
                else if (MP_Financed_radioButton6.Checked)
                    condition += " where MP_State = 4";
                else if (MP_Closed_radioButton6.Checked)
                    condition += " where MP_State = 5";
                else condition += "";

                //funded by
                if (fundedBy != "")
                    if (condition == "")
                        condition += " where MP_ResonOfPlace like N'" + fundedBy + "'";
                    else
                        condition += " and MP_ResonOfPlace like N'" + fundedBy + "'";
                //parish
                if (parish != "")
                    if (condition == "")
                        condition += " where P_Parish like N'" + parish + "'";
                    else
                        condition += " and P_Parish like N'" + parish + "'";
                //category
                if (category != "")
                    if (condition == "")
                        condition += " where C.C_Name like N'%" + category + "%'";
                    else
                        condition += " and C.C_Name like N'%" + category + "%'";

                string groupBycondition = " group by P_MaritalStatus ";
                MySS.query += condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;

                //check connection//
                Program.buildConnection();

                //count the results
                string sel = "select count(MP_ID) " +
                                " from person_microproject PMP left outer join person p on p.P_ID = PMP.Person_ID " +
                                " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID " +
                                " left outer join category C on MP.MP_Category_ID = C.C_ID ";
                sel += condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                AllResultCount_textBox6.Text = MySS.sc.ExecuteScalar().ToString();


                foreach (var series in chart6.Series)
                {
                    series.Points.Clear();
                }
                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    this.chart6.Series["Marital Status"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                }
                chart6.ChartAreas[0].AxisY.Title = "Beneficiaries";
                chart6.ChartAreas[0].AxisX.Title = "Marital Status";
                chart6.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart6.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart6.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart6.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart6.ChartAreas[0].AxisX.Interval = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void MoneyFinanced_Parish_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "SELECT P_Parish ,sum(Loan.Loan_Amount) as MoneyFunded "
                                     + "FROM `loan` inner JOIN `microproject` ON loan.MicroProject_ID = microproject.MP_ID "
                                     + " join person_microproject PMP on PMP.MicroProject_ID = microproject.MP_ID "
                                     + " left outer join person p on p.P_ID = PMP.Person_ID "  ;
                string condition = "";
                //Project State
                if (MP_Accepted_radioButton2.Checked)
                    condition += " where MicroProject.MP_State = 1";

                condition += " group by P_Parish ";
                MySS.query += condition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                results_dataGridView.Columns["MoneyFunded"].DefaultCellStyle.Format = "c2";

                foreach (var series in chart2.Series)
                {
                    series.Points.Clear();
                }

                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                {   //fill chart
                    this.chart2.Series["Financed"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToDecimal(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                }
                chart2.ChartAreas[0].AxisY.Title = "Money Financed";
                chart2.ChartAreas[0].AxisX.Title = "Parishes";
                chart2.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart2.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart2.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart2.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart2.ChartAreas[0].AxisX.Interval = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MoneyNeeded_Parish_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();

                //as DECIMAL(9, 2) = real
                MySS.query = "SELECT P_Parish ,sum(cast(MP_AllPriceNeeded as DECIMAL(9, 2))) as MoneyNeeded FROM `microproject` right join person_microproject PMP on PMP.MicroProject_ID = microproject.MP_ID "
                                     + " left outer join person p on p.P_ID = PMP.Person_ID ";
                string condition = " where P_Parish != '' ";
                //Project State
                if (MP_NotFinanced_radioButton2.Checked)
                    condition += " and MP_State = 0";
                else if (MP_Delayed_radioButton2.Checked)
                    condition += " and MP_State = 2";
                else if (MP_Hold_radioButton2.Checked)
                    condition += " and MP_State = 3";
                else if (MP_Financed_radioButton2.Checked)
                    condition += " and MP_State = 4";
                else if (MP_Closed_radioButton2.Checked)
                    condition += " and MP_State = 5";

                condition += " group by P_Parish ";
                MySS.query += condition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);

                results_dataGridView.Columns[1].DefaultCellStyle.Format = "N0";
                results_dataGridView.DataSource = MySS.dt;

                foreach (var series in chart2.Series)
                {
                    series.Points.Clear();
                }

                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                {   //fill chart
                    this.chart2.Series["Needed"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToDecimal(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                }

                chart2.ChartAreas[0].AxisY.Title = "Money Needed";
                chart2.ChartAreas[0].AxisX.Title = "Parishes";
                chart2.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart2.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart2.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart2.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart2.ChartAreas[0].AxisX.Interval = 1;
                chart2.ChartAreas[0].AxisY.LabelStyle.Format = "{#,###,#0}";
                //chart2.SaveImage(@"D:\" + (2).ToString() + ".png", ChartImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MoneyFinancedAndNeeded_Parish_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();
                
                MySS.query = "SELECT P_Parish ,sum(cast(MP_AllPriceNeeded as DECIMAL(9, 2))) as MoneyNeeded ,sum(Loan.Loan_Amount) as MoneyFunded "
                           + "FROM `loan` right outer JOIN `microproject` ON loan.MicroProject_ID = microproject.MP_ID " +
                           " right join person_microproject PMP on PMP.MicroProject_ID = microproject.MP_ID "
                                     + " left outer join person p on p.P_ID = PMP.Person_ID ";

                string condition = " where P_Parish != '' ";
                condition += " group by P_Parish ";
                MySS.query += condition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;

                foreach (var series in chart2.Series)
                {
                    series.Points.Clear();
                }

                for (int i = 0; i < results_dataGridView.Rows.Count; i++)
                {   //fill chart
                    this.chart2.Series["Needed"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToDecimal(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    //check if is there null values
                    string value = results_dataGridView.Rows[i].Cells[2].Value.ToString();
                    if (string.IsNullOrEmpty(value))
                        value = "0";
                    this.chart2.Series["Financed"].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(), Convert.ToDecimal(value));
                    
                }
                chart2.ChartAreas[0].AxisY.Title = "Money(SP)";
                chart2.ChartAreas[0].AxisX.Title = "Parishes";
                chart2.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart2.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
                chart2.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart2.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9.25f, System.Drawing.FontStyle.Regular);
                chart2.ChartAreas[0].AxisX.Interval = 1;
                //chart2.SaveImage(@"D:\" + (3).ToString() + ".png", ChartImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region radio btn checked clicks
        //chart1//
        private void chart1_Click(object sender, EventArgs e)
        {
            Project_Status_BindChart("","");
        }
        private void MP_ApplyDateFrom_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Project_Status_BindChart(MP_ApplyDateFrom_dateTimePicker.Value.Date, MP_ApplyDateTo_dateTimePicker.Value.Date);
        }
        private void MP_CostFrom_textBox_TextChanged(object sender, EventArgs e)
        {
            Project_Status_BindChart(MP_CostFrom_textBox.Text, MP_CostTo_textBox.Text);
        }
        //chart2//
        private void MP_Financed_radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MoneyFinanced_Parish_BindChart();
        }
        private void MP_NotFinanced_radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MoneyNeeded_Parish_BindChart();
        }
        private void MP_All_radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MoneyFinancedAndNeeded_Parish_BindChart();
        }
        //chart3//
        private void MP_Financed_radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Project_Category_BindChart(replaceQuotation(cat_FundedBy_comboBox.Text), cat_Parish_comboBox.Text);
        }
        private void cat_FundedBy_comboBox_TextChanged(object sender, EventArgs e)
        {
            Project_Category_BindChart(replaceQuotation(cat_FundedBy_comboBox.Text), cat_Parish_comboBox.Text);
        }
        //chart4//
        private void MP_All_radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Project_Parish_BindChart(par_FundedBy_comboBox.Text, replaceQuotation(par_Category_comboBox.Text));
        }
        private void par_FundedBy_comboBox_TextChanged(object sender, EventArgs e)
        {

            Project_Parish_BindChart(par_FundedBy_comboBox.Text, replaceQuotation(par_Category_comboBox.Text));
        }
        //chart5//
        private void MP_All_radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Project_Fund_BindChart(fun_Parish_comboBox.Text, replaceQuotation(fun_Category_comboBox.Text));
        }
        private void fun_Category_comboBox_TextChanged(object sender, EventArgs e)
        {
            Project_Fund_BindChart(fun_Parish_comboBox.Text, replaceQuotation(fun_Category_comboBox.Text));
        }
        //chart6//        
        private void MaritalStatus_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(MaritalStatus_radioButton.Checked)
                Project_MaritalStatus_BindChart(ben_Parish_comboBox.Text, replaceQuotation(ben_FundedBy_comboBox.Text), ben_Category_comboBox.Text);
            else if (MilitaryService_radioButton.Checked)
                Project_MilitaryServices_BindChart(ben_Parish_comboBox.Text, replaceQuotation(ben_FundedBy_comboBox.Text), ben_Category_comboBox.Text);
            else
                Project_Gender_BindChart(ben_Parish_comboBox.Text, replaceQuotation(ben_FundedBy_comboBox.Text), ben_Category_comboBox.Text);
        }
        private void MP_All_radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (MaritalStatus_radioButton.Checked)
                Project_MaritalStatus_BindChart(ben_Parish_comboBox.Text, replaceQuotation( ben_FundedBy_comboBox.Text), ben_Category_comboBox.Text);
            else if (MilitaryService_radioButton.Checked)
                Project_MilitaryServices_BindChart(ben_Parish_comboBox.Text, replaceQuotation(ben_FundedBy_comboBox.Text), ben_Category_comboBox.Text);
            else
                Project_Gender_BindChart(ben_Parish_comboBox.Text, replaceQuotation(ben_FundedBy_comboBox.Text), ben_Category_comboBox.Text);
        }
        #endregion

        #region tab and back buttons
        private void Back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void First_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
            First_button.BackColor = Color.Maroon;
            Second_button.BackColor = Third_button.BackColor = Sixth_button.BackColor
                = Forth_button.BackColor = Fifth_button.BackColor = Color.Transparent;

            Project_Status_BindChart("", "");
        }
        private void Second_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
            Second_button.BackColor = Color.Maroon;
            First_button.BackColor = Third_button.BackColor = Sixth_button.BackColor
                = Forth_button.BackColor = Fifth_button.BackColor = Color.Transparent;

            MoneyFinancedAndNeeded_Parish_BindChart();
        }
        private void Third_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 2;
            Third_button.BackColor = Color.Maroon;
            First_button.BackColor = Second_button.BackColor = Sixth_button.BackColor
                = Forth_button.BackColor = Fifth_button.BackColor = Color.Transparent;

            Project_Category_BindChart("", "");
        }
        private void Forth_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 3;
            Forth_button.BackColor = Color.Maroon;
            First_button.BackColor = Second_button.BackColor = Sixth_button.BackColor
                = Third_button.BackColor = Fifth_button.BackColor = Color.Transparent;

            Project_Parish_BindChart("", "");
        }
        private void Fifth_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 4;
            Fifth_button.BackColor = Color.Maroon;
            First_button.BackColor = Second_button.BackColor = Sixth_button.BackColor
                = Third_button.BackColor = Forth_button.BackColor = Color.Transparent;

            Project_Fund_BindChart("", "");
        }
        private void Sixth_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 5;
            Sixth_button.BackColor = Color.Maroon;
            First_button.BackColor = Second_button.BackColor = Fifth_button.BackColor
                = Third_button.BackColor = Forth_button.BackColor = Color.Transparent;

            Project_MaritalStatus_BindChart("", "", "");
        }
        #endregion

        #region export to excel
        private void ExportToExcel_button_Click(object sender, EventArgs e)
        {
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            for (int x = 1; x < results_dataGridView.Columns.Count + 1; x++)
            {
                xlWorkSheet.Cells[1, x] = results_dataGridView.Columns[x - 1].HeaderText;
            }
            for (int i = 0; i < results_dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < results_dataGridView.Columns.Count; j++)
                {
                    xlWorkSheet.Cells[i + 2, j + 1] = results_dataGridView.Rows[i].Cells[j].Value.ToString();
                }
            } 
            //1.create the Excel Chart object
            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            //2. Set the position of chart where you need to place inside the Excel sheet
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(200, 5, 300, 250);
            //3. create a new chart page to display your value
            Excel.Chart chartPage = myChart.Chart;
            //4.Set the X & Y axis Range of data columns   
            //4.1 it takes Excel A Column as as X axis; Data value is from A20-A30
            //4.2 it takes Excel B Column as as Y axis; Data value is from A20-A30
            int rowCount = results_dataGridView.Rows.Count + 1;
            Excel.Range chartRange = xlWorkSheet.get_Range("A1", "B"+ rowCount);
            //5.Set the chart Source data from your chart range
            chartPage.SetSourceData(chartRange, Type.Missing);
            //6.select the chart type to render your data values
            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;
            //7.If you need to declare the chart title please follow the two steps
            myChart.Chart.HasTitle = true;
            chartPage.ChartTitle.Text = "Column Chart";

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion

        #region comboBox bind
        private void par_Category_comboBox_Enter(object sender, EventArgs e)
        {
            par_Category_comboBox.DisplayMember = "C_Name";
            par_Category_comboBox.ValueMember = "C_ID";
            par_Category_comboBox.DataSource = Category_bind();
            
        }
        private void fun_Category_comboBox_Enter(object sender, EventArgs e)
        {
            fun_Category_comboBox.DisplayMember = "C_Name";
            fun_Category_comboBox.ValueMember = "C_ID";
            fun_Category_comboBox.DataSource = Category_bind();
        }

        private void ben_Category_comboBox_Enter(object sender, EventArgs e)
        {
            ben_Category_comboBox.DisplayMember = "C_Name";
            ben_Category_comboBox.ValueMember = "C_ID";
            ben_Category_comboBox.DataSource = Category_bind();
        }
        #endregion

        private string replaceQuotation(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Statistics_Load(sender, e);
        }

    }
}
