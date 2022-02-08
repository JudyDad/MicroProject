using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;
using MyWorkApplication.Visit_Forms;
using Application = Microsoft.Office.Interop.Excel.Application;
using ChartArea = System.Windows.Forms.DataVisualization.Charting.ChartArea;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using Legend = System.Windows.Forms.DataVisualization.Charting.Legend;

namespace MyWorkApplication
{
    public partial class Statistics_Form : Form
    {
        private string ApplyDate_condition = "";
        private SeriesChartType Chart_type = SeriesChartType.Column;
        private Evaluation_Statistics es;

        private bool user_mode;
        private DataSet ds;
        private string from = "";
        private string condition = " where 1 ";
        private string from2 = "", from3 = "";
        private string FundedDate_condition = "";
        private bool Hided;
        private readonly MainForm main_form;

        private MySqlComponents MySS;
        private int PW;
        private Application xlApp;
        private Workbook xlWorkBook;
        private Worksheet xlWorkSheet;
        private List<double> xValues = new List<double>();
        private List<double> yValues = new List<double>();

        Street st;
        SubCategory sub;
        DataTable donor_group_dt, sub_category_dt;

        public Statistics_Form(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void Statistics_Form_Load(object sender, EventArgs e)
        {
            try
            {
                var newTheme = new NewTheme();
                if (Settings.Default.theme == "Dark")
                    newTheme.Statistics_ToNight(this, Hided);
                else
                    newTheme.Statistics_ToLight(this, Hided);

                user_mode = false;

                MySS = new MySqlComponents();
                es = new Evaluation_Statistics();
                st = new Street();
                sub = new SubCategory();

                PW = Search_panel.Height;
                Hided = true;
                Search_panel.Height = 0;

                Bind_All_ComboBoxes();

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
                    chart1.Series[3].Enabled = chart1.Series[4].Enabled = chart1.Series[5].Enabled =
                            chart1.Series[6].Enabled = chart1.Series[7].Enabled = false;
                 
                fillChartTypesComboBox();
                comboBox1.SelectedIndex = 0;
                button1_Click(sender, e);

                user_mode = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Check_Visibility()
        {
            //MoneyFinanced_textBox.Visible = MoneyFinanced_label.Visible =
            //    MoneyNeeded_textBox.Visible = MoneyNeeded_label.Visible = false;

            FundType_comboBox.Visible = Type_comboBox.Visible = SubType_comboBox.Visible
                = MP_Status_comboBox.Visible = Partnership_comboBox.Visible
                = Donor_comboBox.Visible = DonorGroup_comboBox.Visible
                = MP_Category_comboBox.Visible = SubCategory_comboBox.Visible
                = P_Parish_comboBox.Visible = Age_comboBox.Visible 
                = Gender_comboBox.Visible = MaritalStatus_comboBox.Visible = true;

            FundType_label.Visible = Type_label.Visible = SubType_label.Visible
                = MP_Status_label.Visible = Partnership_label.Visible
                = Donor_label.Visible = DonorGroup_label.Visible
                = MP_Category_label.Visible = SubCategory_label.Visible
                = P_Parish_label.Visible = Age_label.Visible 
                = Gender_label.Visible = MaritalStatus_label.Visible = true;

            //  // Project Status // //
            if (X_Axis_comboBox.SelectedIndex == 0)
            {
                MP_Status_comboBox.Visible = MP_Status_label.Visible = false; 
            }
            // Type //
            else if (X_Axis_comboBox.SelectedIndex == 1)
            {
                Type_comboBox.Visible = Type_label.Visible = false; 
            }

            // Categories //
            else if (X_Axis_comboBox.SelectedIndex == 2)
            {
                MP_Category_comboBox.Visible = MP_Category_label.Visible = false; 
            }
            // Parishes //
            else if (X_Axis_comboBox.SelectedIndex == 3)
            {
                P_Parish_comboBox.Visible = P_Parish_label.Visible = false; 
            }
            // Donors //
            else if (X_Axis_comboBox.SelectedIndex == 4)
            {
                Donor_comboBox.Visible = Donor_label.Visible = false; 
            }
            // Gender //
            else if (X_Axis_comboBox.SelectedIndex == 5)
            {
                Gender_comboBox.Visible = Gender_label.Visible = false; 
            }
            //  // Marital status // //
            else if (X_Axis_comboBox.SelectedIndex == 6)
            {
                MaritalStatus_comboBox.Visible = MaritalStatus_label.Visible = false; 
            }
            // // age // //
            else if (X_Axis_comboBox.SelectedIndex == 7)
            {
                Age_comboBox.Visible = Age_label.Visible = false; 
            } 
            // Sub Type // // experience // // profit rate //
            else if (X_Axis_comboBox.SelectedIndex == 8 
                    || X_Axis_comboBox.SelectedIndex == 9 
                    || X_Axis_comboBox.SelectedIndex == 10)
            { 
            }
            // // Money // //
            else if (X_Axis_comboBox.SelectedIndex == 11)
            { 
                //MoneyFinanced_textBox.Visible = MoneyFinanced_label.Visible =
                //    MoneyNeeded_textBox.Visible = MoneyNeeded_label.Visible = true;
            }
        }

        private void X_Axis_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Check_Visibility();
                chart1.ChartAreas.Clear();

                from =
                    @" From `microproject` MP
 LEFT OUTER JOIN `person_microproject` PMP on PMP.MicroProject_ID = MP.MP_ID 
 LEFT OUTER JOIN `person` P on P.P_ID = PMP.Person_ID   
 LEFT OUTER JOIN `priest` on Priest_ID = P.P_Priest_ID
 LEFT OUTER JOIN `category` C on C.C_ID = MP.MP_Category_ID
 LEFT OUTER JOIN `subcategory` sub on sub.ID =  MP.SubCategory_ID 
 LEFT OUTER JOIN `state` on state.ID = MP.MP_State 
 LEFT OUTER JOIN `donor` on donor.ID = MP.MP_Donor
 LEFT OUTER JOIN `street` w_street on w_street.ID = MP.MP_Street_ID
 LEFT OUTER JOIN `street` h_street on h_street.ID = P.Street_ID  
 LEFT OUTER JOIN `loan` L on L.MicroProject_ID = MP.MP_ID
 LEFT OUTER JOIN `donorgroup` on donorgroup.ID = MP.DonorGroup_ID  
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID ";
                 
                from2 = "";
                

                Check_Conditions(
                    FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text
                    ,Partnership_comboBox.Text, MP_Status_comboBox.Text
                    , replaceQuotation(Donor_comboBox.Text) , replaceQuotation(DonorGroup_comboBox.Text)
                    , replaceQuotation(MP_Category_comboBox.Text) , SubCategory_comboBox.Text
                    , P_Parish_comboBox.Text, Age_comboBox.Text, Gender_comboBox.Text, MaritalStatus_comboBox.Text
                    , ApplyDate_comboBox.Text, FundDate_comboBox.Text
                    , ApplyDate_condition, FundedDate_condition);

                // Project Status //
                if (X_Axis_comboBox.SelectedIndex == 0)
                {
                    Build_ChartArea("Status", 0);
                     
                    Project_Status_BindChart();
                }

                // Type //
                else if (X_Axis_comboBox.SelectedIndex == 1)
                {
                    Build_ChartArea("Type", 0);

                    Project_Type_BindChart();
                }

                // Categories //
                else if (X_Axis_comboBox.SelectedIndex == 2)
                {
                    Build_ChartArea("Categories", 0);

                    Project_Category_BindChart();
                }

                // Parishes //
                else if (X_Axis_comboBox.SelectedIndex == 3)
                {
                    Build_ChartArea("Parishes", 0);

                    Project_Parish_BindChart();
                }

                // Donors //
                else if (X_Axis_comboBox.SelectedIndex == 4)
                {
                    Build_ChartArea("Donors", 0);

                    Project_Donor_BindChart();
                }

                // Gender //
                else if (X_Axis_comboBox.SelectedIndex == 5)
                {
                    Build_ChartArea("Gender", 0);

                    Project_Gender_BindChart();
                }

                // Marital status //
                else if (X_Axis_comboBox.SelectedIndex == 6)
                {
                    Build_ChartArea("Marital status", 0);

                    Project_MaritalStatus_BindChart();
                }

                //   age  //
                else if (X_Axis_comboBox.SelectedIndex == 7)
                {
                    Build_ChartArea("Age", 0);

                    Project_Age_BindChart();
                }

                // SubType //
                else if (X_Axis_comboBox.SelectedIndex == 8)
                {
                    Build_ChartArea("SubType", 0);

                    Project_SubType_BindChart();
                }

                // experience //
                else if (X_Axis_comboBox.SelectedIndex == 9)
                {
                    Build_ChartArea("Experience", 0);

                    Project_Experience_BindChart();
                }
                 
                // profit rate //
                else if (X_Axis_comboBox.SelectedIndex == 10)
                {
                    Build_ChartArea("Income Change", 0);

                    Project_ProfitRate_BindChart();
                }

                // Partnership //
                else if (X_Axis_comboBox.SelectedIndex == 11)
                {
                    Build_ChartArea("Partnership", 0);

                    Project_Partnership_BindChart();
                }
                
                //// money // 
                //else if (X_Axis_comboBox.SelectedIndex == 11) ////
                //{
                //    Build_ChartArea("Money", 0);

                //    MoneyFinanced_textBox.Visible = MoneyFinanced_label.Visible =
                //        MoneyNeeded_textBox.Visible = MoneyNeeded_label.Visible = true;

                //    MoneyFinancedAndNeeded_Parish_BindChart(replaceQuotation(Donor_comboBox.Text),
                //        replaceQuotation(MP_Category_comboBox.Text), P_Parish_comboBox.Text
                //        , Gender_comboBox.Text, MaritalStatus_comboBox.Text, MP_Status_comboBox.Text
                //        , Reason_comboBox.Text, "", Experience_comboBox.Text, Partnership_comboBox.Text
                //        , ApplyDate_comboBox.Text, FundDate_comboBox.Text
                //        , ApplyDate_condition, FundedDate_condition);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void MP_Category_comboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                X_Axis_comboBox_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdvancedSearch_button_Click(object sender, EventArgs e)
        {
            if (Hided)
            {
                Hided = false;
                if (Settings.Default.theme == "Light")
                    AdvancedSearch_button.BackgroundImage = Resources.Hide2_L;
                else AdvancedSearch_button.BackgroundImage = Resources.Hide2_D;
                Search_panel.Height = PW;
            }
            else
            {
                Hided = true;
                if (Settings.Default.theme == "Light")
                    AdvancedSearch_button.BackgroundImage = Resources.Show2_L;
                else AdvancedSearch_button.BackgroundImage = Resources.Show2_D;
                Search_panel.Height = 0;
            }
        }

        #region export to excel

        private void ExportToExcel_button_Click(object sender, EventArgs e)
        {
            if (Settings.Default.role != 1 && Settings.Default.role != 8 && Settings.Default.role != 5 && Settings.Default.role != 2)
                throw new Exception("Sorry ! You Don't have the permission for this action.");


            object misValue = Missing.Value;
            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Worksheet) xlWorkBook.Worksheets.get_Item(1);
            // see the excel sheet behind the program  
            xlApp.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            xlWorkSheet = xlWorkBook.Sheets[1];
            xlWorkSheet = xlWorkBook.ActiveSheet;
            // changing the name of active sheet  
            xlWorkSheet.Name = "Exported from App";

            for (var x = 1; x < results_dataGridView.Columns.Count + 1; x++)
                xlWorkSheet.Cells[1, x] = results_dataGridView.Columns[x - 1].HeaderText;
            for (var i = 0; i < results_dataGridView.Rows.Count; i++)
            for (var j = 0; j < results_dataGridView.Columns.Count; j++)
                xlWorkSheet.Cells[i + 2, j + 1] = results_dataGridView.Rows[i].Cells[j].Value.ToString();
            //1.create the Excel Chart object
            var xlCharts = (ChartObjects) xlWorkSheet.ChartObjects(Type.Missing);
            //2. Set the position of chart where you need to place inside the Excel sheet
            var myChart = xlCharts.Add(200, 5, 300, 250);
            //3. create a new chart page to display your value
            var chartPage = myChart.Chart;
            //4.Set the X & Y axis Range of data columns   
            //4.1 it takes Excel A Column as as X axis; Data value is from A20-A30
            //4.2 it takes Excel B Column as as Y axis; Data value is from A20-A30
            var rowCount = results_dataGridView.Rows.Count + 1;
            var chartRange = xlWorkSheet.get_Range("A1", "B" + rowCount);
            //5.Set the chart Source data from your chart range
            chartPage.SetSourceData(chartRange, Type.Missing);
            //6.select the chart type to render your data values

            if (comboBox1.SelectedItem.ToString() == SeriesChartType.Column.ToString())
                chartPage.ChartType = XlChartType.xlColumnClustered;

            else if (comboBox1.SelectedItem.ToString() == SeriesChartType.BoxPlot.ToString()
                     || comboBox1.SelectedItem.ToString() == SeriesChartType.Bubble.ToString())
                chartPage.ChartType = XlChartType.xlBubble;

            else if (comboBox1.SelectedItem.ToString() == SeriesChartType.Bar.ToString())
                chartPage.ChartType = XlChartType.xlBarClustered;

            else if (comboBox1.SelectedItem.ToString() == SeriesChartType.SplineRange.ToString()
                     || comboBox1.SelectedItem.ToString() == SeriesChartType.Line.ToString())
                chartPage.ChartType = XlChartType.xlLine;

            else if (comboBox1.SelectedItem.ToString() == SeriesChartType.Area.ToString())
                chartPage.ChartType = XlChartType.xlArea;

            else if (comboBox1.SelectedItem.ToString() == SeriesChartType.Point.ToString())
                chartPage.ChartType = XlChartType.xlXYScatter;

            else if (comboBox1.SelectedItem.ToString() == SeriesChartType.Doughnut.ToString()
                     || comboBox1.SelectedItem.ToString() == SeriesChartType.Pie.ToString())
                chartPage.ChartType = XlChartType.xlPie;

            else chartPage.ChartType = XlChartType.xl3DLine;

            //chartPage.ChartType = Excel.XlChartType.xlColumnClustered;
            //7.If you need to declare the chart title please follow the two steps
            myChart.Chart.HasTitle = true;
            chartPage.ChartTitle.Text = "Column Chart";

            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            //releaseObject(xlWorkSheet);
            //releaseObject(xlWorkBook);
            //releaseObject(xlApp);
        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Chart_type = (SeriesChartType) comboBox1.SelectedItem;
                MP_Category_comboBox_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillChartTypesComboBox()
        {
            comboBox1.Items.Add(SeriesChartType.Column);
            comboBox1.Items.Add(SeriesChartType.BoxPlot);
            comboBox1.Items.Add(SeriesChartType.Bar);
            comboBox1.Items.Add(SeriesChartType.SplineRange);
            comboBox1.Items.Add(SeriesChartType.Area);
            comboBox1.Items.Add(SeriesChartType.Line);
            comboBox1.Items.Add(SeriesChartType.Bubble);
            comboBox1.Items.Add(SeriesChartType.Point);
            comboBox1.Items.Add(SeriesChartType.Doughnut);
            comboBox1.Items.Add(SeriesChartType.Kagi);
            comboBox1.Items.Add(SeriesChartType.StepLine);


            //    comboBox1.Items.Add(SeriesChartType.Pie);
            //    comboBox1.Items.Add(SeriesChartType.Pyramid);
            //    comboBox1.Items.Add(SeriesChartType.Candlestick);
            //    comboBox1.Items.Add(SeriesChartType.ErrorBar);
            //    comboBox1.Items.Add(SeriesChartType.FastLine);
            //    comboBox1.Items.Add(SeriesChartType.FastPoint);
            //    comboBox1.Items.Add(SeriesChartType.Funnel);
            //    comboBox1.Items.Add(SeriesChartType.PointAndFigure);
            //    comboBox1.Items.Add(SeriesChartType.Polar);
            //    comboBox1.Items.Add(SeriesChartType.Radar);
            //    comboBox1.Items.Add(SeriesChartType.Range);
            //    comboBox1.Items.Add(SeriesChartType.RangeBar);
            //    comboBox1.Items.Add(SeriesChartType.RangeColumn);
            //    comboBox1.Items.Add(SeriesChartType.Renko);
            //    comboBox1.Items.Add(SeriesChartType.Spline);
            //    comboBox1.Items.Add(SeriesChartType.SplineArea);
            //    comboBox1.Items.Add(SeriesChartType.StackedArea);
            //    comboBox1.Items.Add(SeriesChartType.StackedBar);
            //    comboBox1.Items.Add(SeriesChartType.StackedColumn);
        }

        private string replaceQuotation(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var dt = es.Select_BeneficiaryWithEval_IDs();
            //    if (dt != null || dt.Rows.Count > 0)
            //        for (var i = 0; i < dt.Rows.Count; i++)
            //        {
            //            var id = Convert.ToInt32(dt.Rows[i].Field<int>(0).ToString());

            //            es.Update_Statistics(id);
            //        }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void ApplyDateFrom_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ApplyDateFrom_dateTimePicker.Value > ApplyDateTo_bcDateTimePicker.Value)
                {
                    ApplyDate_condition = "";
                    throw new Exception("لا يمكن أن يكون تاريخ البداية أكبر من تاريخ النهاية");
                }

                DateTime beg, end;
                beg = ApplyDateFrom_dateTimePicker.Value;
                end = ApplyDateTo_bcDateTimePicker.Value;

                ApplyDate_condition = " and ( MP_DateOfRequest between '" + beg.Year + "/" + beg.Month + "/" + beg.Day +
                                      "' " +
                                      " and '" + end.Year + "/" + end.Month + "/" + end.Day + "' )";

                X_Axis_comboBox_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FundedDateTo_bcDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (FundDateFrom_bcDateTimePicker.Value > FundDateTo_bcDateTimePicker.Value)
                    throw new Exception("لا يمكن أن يكون تاريخ البداية أكبر من تاريخ النهاية");

                DateTime beg, end;
                beg = FundDateFrom_bcDateTimePicker.Value;
                end = FundDateTo_bcDateTimePicker.Value;

                FundedDate_condition = " and MP_ID in (select MicroProject_ID from loan where " +
                                       " Loan_DateTaken between '" + beg.Year + "/" + beg.Month + "/" + beg.Day + "' " +
                                       " and '" + end.Year + "/" + end.Month + "/" + end.Day + "' )";

                X_Axis_comboBox_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        #region bind

        private void Project_Status_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();
                MySS.query = " Select state.Name_en as 'Project State'" +
                             ", count(DISTINCT MP_ID) as numOfProjects ";
                 
                var GroupBycondition = "";
                GroupBycondition += " group by state.Name_en ";
                MySS.query += from + condition + GroupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;

                Program.MyConn.Close();
                //check connection//
                Program.buildConnection();
                //count all
                var sel = "select count(DISTINCT MP_ID) " + from;
                sel += condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                foreach (var series in chart1.Series) //clear chart
                    series.Points.Clear();

                chart1.Series[1].Enabled = chart1.Series[2].Enabled = chart1.Series[3].Enabled =
                    chart1.Series[4].Enabled =
                        chart1.Series[5].Enabled = chart1.Series[6].Enabled = chart1.Series[7].Enabled =
                            chart1.Series[8].Enabled =
                                chart1.Series[9].Enabled = chart1.Series[10].Enabled =
                                    chart1.Series[11].Enabled =   false;
                chart1.Series[0].Enabled = true;

                chart1.Series[0].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[0].Legend = chart1.Legends[0].Name;

                chart1.Series[0].ChartType = Chart_type;

                var max_value = 0;
                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    //fill chart
                    chart1.Series[0].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }

                chart1.Series[0].Name = "Project States";
                chart1.Series[0].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);

                SetChartAreaProperties(max_value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_Type_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();
                MySS.query =
                    "select fundtype.Name_en , count(DISTINCT MP_ID) as numOfProjects ";
 
                  
                var groupBycondition = " group by fundtype.Name ";
                MySS.query += from + condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                Program.MyConn.Close();

                //check connection//
                Program.buildConnection();

                //count all
                var sel = "select count(DISTINCT MP_ID) " + from;
                sel += condition;

                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[0].Enabled = chart1.Series[2].Enabled = chart1.Series[3].Enabled =
                    chart1.Series[4].Enabled
                        = chart1.Series[5].Enabled = chart1.Series[6].Enabled = chart1.Series[7].Enabled =
                            chart1.Series[8].Enabled
                                = chart1.Series[9].Enabled = chart1.Series[10].Enabled =
                                    chart1.Series[11].Enabled =   false;
                chart1.Series[1].Enabled = true;

                chart1.Series[1].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[1].Legend = chart1.Legends[0].Name;

                var max_value = 0;

                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    //fill chart
                    chart1.Series[1].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }

                chart1.Series[1].Name = "Project Type";
                chart1.Series[1].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
                SetChartAreaProperties(max_value);

                chart1.Series[1].ChartType = Chart_type;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_Category_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();
                MySS.query = "select c.C_Name , count(DISTINCT MP_ID) as numOfProjects ";
                
                var groupBycondition = " group by c.C_Name ";
                MySS.query += from + condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                Program.MyConn.Close();

                //check connection//
                Program.buildConnection();

                //count all
                var sel = "select count(DISTINCT MP_ID) " + from;
                sel += condition;

                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[3].Enabled =
                    chart1.Series[4].Enabled
                        = chart1.Series[5].Enabled = chart1.Series[6].Enabled = chart1.Series[7].Enabled =
                            chart1.Series[8].Enabled
                                = chart1.Series[9].Enabled = chart1.Series[10].Enabled =
                                    chart1.Series[11].Enabled =   false;

                chart1.Series[2].Enabled = true;

                chart1.Series[2].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[2].Legend = chart1.Legends[0].Name;

                var max_value = 0;

                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    //fill chart
                    chart1.Series[2].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }

                chart1.Series[2].Name = "Project Categories";
                chart1.Series[2].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
                SetChartAreaProperties(max_value);

                chart1.Series[2].ChartType = Chart_type;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_Parish_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();
                MySS.query = "Select P.P_Parish , count(MP_ID) as numOfProjects ";

                condition += " and `IsProjectOwner` like 'YES' ";
                  
                var groupBycondition = " group by P.P_Parish ";
                MySS.query += from + condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                Program.MyConn.Close();

                //check connection//
                Program.buildConnection();

                //count all
                var sel = "select count(DISTINCT MP_ID) " + from;
                sel += condition;

                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                var max_value = 0;
                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
                    chart1.Series[4].Enabled
                        = chart1.Series[5].Enabled = chart1.Series[6].Enabled = chart1.Series[7].Enabled =
                            chart1.Series[8].Enabled
                                = chart1.Series[9].Enabled = chart1.Series[10].Enabled =
                                    chart1.Series[11].Enabled =   false;
                chart1.Series[3].Enabled = true;

                chart1.Series[3].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[3].Legend = chart1.Legends[0].Name;

                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    //fill chart
                    chart1.Series[3].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }

                chart1.Series[3].Name = "Project Parishes";
                chart1.Series[3].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);

                SetChartAreaProperties(max_value);

                chart1.Series[3].ChartType = Chart_type;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_Donor_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "Select donor.Name , count(DISTINCT MP_ID) as numOfProjects ";
                condition += " and `IsProjectOwner` like 'YES' and donor.Name is not null ";
                 
                 
                var groupBycondition = " group by donor.Name ";
                MySS.query += from + condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                Program.MyConn.Close();

                //check connection//
                Program.buildConnection();

                //count all
                var sel = "select count(DISTINCT MP_ID) " + from;
                sel += condition;

                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                var max_value = 0;

                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[4].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[4].Legend = chart1.Legends[0].Name;

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
                    chart1.Series[3].Enabled
                        = chart1.Series[5].Enabled = chart1.Series[6].Enabled = chart1.Series[7].Enabled =
                            chart1.Series[8].Enabled
                                = chart1.Series[9].Enabled = chart1.Series[10].Enabled =
                                    chart1.Series[11].Enabled =   false;
                chart1.Series[4].Enabled = true;

                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    chart1.Series[4].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }

                chart1.Series[4].Name = "Project Donors";
                chart1.Series[4].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
                SetChartAreaProperties(max_value);

                chart1.Series[4].ChartType = Chart_type;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_Gender_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query =
                    "SELECT (CASE P_Sex WHEN N'أنثى' THEN N'Female' WHEN N'ذكر' THEN N'Male' ELSE N'N/A' End) " +
                    ", COUNT(P_ID) as numOfProjects ";
                
                var groupBycondition = " group by P_Sex ";
                MySS.query += from + condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                Program.MyConn.Close();

                //check connection//
                Program.buildConnection();

                //count the results
                var sel = "select count(DISTINCT P_ID) " + from;
                sel += condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                var max_value = 0;
                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[5].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[5].Legend = chart1.Legends[0].Name;

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
                    chart1.Series[3].Enabled
                        = chart1.Series[4].Enabled = chart1.Series[6].Enabled = chart1.Series[7].Enabled =
                            chart1.Series[8].Enabled
                                = chart1.Series[9].Enabled = chart1.Series[10].Enabled =
                                    chart1.Series[11].Enabled =   false;
                chart1.Series[5].Enabled = true;

                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    chart1.Series[5].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }

                chart1.Series[5].Name = "Beneficiary Gender";
                chart1.Series[5].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
                //chart1.Series[0].IsXValueIndexed = true;
                SetChartAreaProperties(max_value);
                chart1.Series[5].ChartType = Chart_type;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_MaritalStatus_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query =
                    "SELECT (CASE P_MaritalStatus WHEN N'متزوج/ة' THEN N'Married' WHEN N'عازب/ة' THEN N'Single' WHEN N'أرمل/ة' THEN N'Widow' WHEN N'منفصل/ة' THEN N'Separated' WHEN N'مخطوب/ة' THEN N'Engaged'  WHEN N'مطلق/ة' THEN N'Divorced' ELSE N'N/A' End) " +
                    ", COUNT(P_ID) as numOfProjects ";
                condition += " and `IsProjectOwner` like 'YES' ";
                 
                 
                var groupBycondition = " group by P_MaritalStatus ";
                MySS.query += from + condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                Program.MyConn.Close();

                //check connection//
                Program.buildConnection();

                //count the results
                var sel = "select count(DISTINCT P_ID) " + from;
                sel += condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                var max_value = 0;
                chart1.Series[6].Name = "Beneficiary Marital Status";
                chart1.Series[6].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
                SetChartAreaProperties(max_value);

                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[6].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[6].Legend = chart1.Legends[0].Name;

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
                    chart1.Series[3].Enabled =
                        chart1.Series[4].Enabled = chart1.Series[5].Enabled = chart1.Series[7].Enabled =
                            chart1.Series[8].Enabled =
                                chart1.Series[9].Enabled = chart1.Series[10].Enabled =
                                    chart1.Series[11].Enabled =   false;
                chart1.Series[6].Enabled = true;

                chart1.Series[6].ChartType = Chart_type;

                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    chart1.Series[6].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_Age_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "Select CASE WHEN(TIMESTAMPDIFF(YEAR, P.`P_DOB`, MP_DateOfRequest) < 16) THEN '< 16'"
                             + " WHEN(TIMESTAMPDIFF(YEAR, P.`P_DOB`, MP_DateOfRequest) BETWEEN 16 AND 26) THEN '16-26'"
                             + " WHEN(TIMESTAMPDIFF(YEAR, P.`P_DOB`, MP_DateOfRequest) BETWEEN  27 AND 35) THEN '27-35'"
                             + " WHEN(TIMESTAMPDIFF(YEAR, P.`P_DOB`, MP_DateOfRequest) BETWEEN 36 AND 45) THEN '36-45'"
                             + " WHEN(TIMESTAMPDIFF(YEAR, P.`P_DOB`, MP_DateOfRequest) BETWEEN 46 AND 60) THEN '46-60'"
                             + " WHEN(TIMESTAMPDIFF(YEAR, P.`P_DOB`, MP_DateOfRequest) > 60) THEN 'over 60'"
                             + " ELSE 'other' END as 'Age'"
                             + ", count(P_ID) as numOfProjects ";
                condition = " and `IsProjectOwner` like 'YES' ";
                 
                 
                var groupBycondition = " Group by Age ORDER BY `P_DOB` desc";
                MySS.query += from + condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                Program.MyConn.Close();

                //check connection//
                Program.buildConnection();

                //count the results
                var sel = "select count(DISTINCT P_ID) " + from;
                sel += condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                var max_value = 0;
                chart1.Series[7].Name = "Age of Beneficiaries";
                chart1.Series[7].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
                SetChartAreaProperties(max_value);

                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[7].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[7].Legend = chart1.Legends[0].Name;

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
                    chart1.Series[3].Enabled =
                        chart1.Series[4].Enabled = chart1.Series[5].Enabled = chart1.Series[6].Enabled =
                            chart1.Series[8].Enabled =
                                chart1.Series[9].Enabled = chart1.Series[10].Enabled =
                                    chart1.Series[11].Enabled =   false;
                chart1.Series[7].Enabled = true;

                chart1.Series[7].ChartType = Chart_type;

                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    chart1.Series[7].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_SubType_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = " Select microprojectsubtype.Name_en as 'sub Type', count(DISTINCT MP_ID) as numOfProjects ";
                 
                 
                var groupBycondition = "  group by microprojectsubtype.Name ";
                MySS.query += from + condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                Program.MyConn.Close();

                //check connection//
                Program.buildConnection();

                //count the results
                var sel = "select count(DISTINCT MP_ID) " + from;
                sel += condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                var max_value = 0;
                chart1.Series[8].Name = "Sub Type";
                chart1.Series[8].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
                SetChartAreaProperties(max_value);

                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[8].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[8].Legend = chart1.Legends[0].Name;

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
                    chart1.Series[3].Enabled =
                        chart1.Series[4].Enabled = chart1.Series[5].Enabled = chart1.Series[6].Enabled =
                            chart1.Series[7].Enabled =
                                chart1.Series[9].Enabled = chart1.Series[10].Enabled =
                                    chart1.Series[11].Enabled =   false;
                chart1.Series[8].Enabled = true;

                chart1.Series[8].ChartType = Chart_type;

                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    chart1.Series[8].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_Experience_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();
                MySS.query = @"SELECT case ms.`value` when 1 then 'Yes' when 0 then 'No' else 'N/A' end as 'Experience'
, count(DISTINCT MP_ID) as numOfProjects ";
                condition += " and Score_ID = 3 ";

                var from3 = " left join microproject_score ms on MP.MP_ID = ms.MicroProject_ID ";
                 
                 
                var groupBycondition = "  group by ms.`value` ";
                MySS.query += from + from3 + condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                Program.MyConn.Close();

                //check connection//
                Program.buildConnection();

                //count the results
                var sel = "select count(DISTINCT MP_ID) " + from + from3;
                sel += condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                var max_value = 0;
                chart1.Series[9].Name = "Experience";
                chart1.Series[9].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
                SetChartAreaProperties(max_value);

                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[9].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[9].Legend = chart1.Legends[0].Name;

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
                    chart1.Series[3].Enabled =
                        chart1.Series[4].Enabled = chart1.Series[5].Enabled = chart1.Series[6].Enabled =
                            chart1.Series[7].Enabled =
                                chart1.Series[8].Enabled = chart1.Series[10].Enabled =
                                    chart1.Series[11].Enabled =   false;
                chart1.Series[9].Enabled = true;

                chart1.Series[9].ChartType = Chart_type;

                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    chart1.Series[9].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_Partnership_BindChart()
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.query = "Select CASE Partnership WHEN '1' THEN 'Individual'"
                             + " ELSE 'Partnership' END as 'Partnership'"
                             + ", count(DISTINCT MP_ID) as numOfProjects ";
                
                 
                var groupBycondition = "  group by Partnership ";
                MySS.query += from + condition + groupBycondition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                results_dataGridView.DataSource = MySS.dt;
                Program.MyConn.Close();

                //check connection//
                Program.buildConnection();

                //count the results
                var sel = "select count(DISTINCT MP_ID) " + from;
                sel += condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Count_label.Text = "All:" + MySS.sc.ExecuteScalar();
                Program.MyConn.Close();

                var max_value = 0;
                chart1.Series[11].Name = "Partnership";
                chart1.Series[11].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
                SetChartAreaProperties(max_value);

                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[11].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[11].Legend = chart1.Legends[0].Name;

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
                    chart1.Series[3].Enabled =
                        chart1.Series[4].Enabled = chart1.Series[5].Enabled = chart1.Series[6].Enabled =
                            chart1.Series[7].Enabled = chart1.Series[8].Enabled =
                                chart1.Series[9].Enabled = chart1.Series[10].Enabled = false;
                chart1.Series[11].Enabled = true;

                chart1.Series[11].ChartType = Chart_type;

                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    chart1.Series[11].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Project_ProfitRate_BindChart()
        {
            try
            {  
                MySS.sc = new MySqlCommand("calculate_profit_rates", Program.MyConn);
                MySS.sc.CommandType = CommandType.StoredProcedure; 
                MySS.sc.Parameters.AddWithValue("@MyCondition", condition);

                using (MySqlDataAdapter sda = new MySqlDataAdapter(MySS.sc))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    results_dataGridView.DataSource = dt;
                    Program.MyConn.Close();
                }

                var max_value = 0;
                chart1.Series[10].Name = "Income Change";
                chart1.Series[10].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
                SetChartAreaProperties(max_value);

                foreach (var series in chart1.Series)
                    series.Points.Clear();

                chart1.Series[10].ChartArea = chart1.ChartAreas[0].Name;
                chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                chart1.Series[10].Legend = chart1.Legends[0].Name;

                chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
                    chart1.Series[3].Enabled =
                        chart1.Series[4].Enabled = chart1.Series[5].Enabled = chart1.Series[6].Enabled =
                            chart1.Series[7].Enabled =
                                chart1.Series[8].Enabled = chart1.Series[9].Enabled =
                                    chart1.Series[11].Enabled = false;
                chart1.Series[10].Enabled = true;

                chart1.Series[10].ChartType = Chart_type;

                int sum_numOfProjects = 0;
                for (var i = 0; i < results_dataGridView.Rows.Count; i++)
                {
                    sum_numOfProjects += Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString());

                    chart1.Series[10].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
                        Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
                    if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
                        max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
                }
                Count_label.Text = "All:" + sum_numOfProjects.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region old rate
        //private void Project_ProfitRate_BindChart(string Type, string State, string Donor,string DonorGroup, string Category,
        //    string Parish, string Age, string Sex, string MaritalStatus, string ProjectReason, string ProfitRate,
        //    string HasExperience,string Partners,
        //    string ApplyDate, string FundDate, string ApplyDate_Condition, string FundedDate_Condition)
        //{
        //    try
        //    {
        //        //check connection//
        //        Program.buildConnection();
        //        var from2 = " left join person_statistics ps on ps.Person_ID = PMP.Person_ID ";

        //        MySS.query = "Select CASE when EvalSal_Percentage < 0 then '<0 %' " +
        //                     " when EvalSal_Percentage = 0 then '0 %' " +
        //                     " when EvalSal_Percentage BETWEEN 1 and 25 then '1-25 %' " +
        //                     " when EvalSal_Percentage BETWEEN 26 and 50 then '26-50 %' " +
        //                     " when EvalSal_Percentage BETWEEN 51 and 75 then '51-75 %' " +
        //                     " when EvalSal_Percentage BETWEEN 76 and 99 then '76-99 %' " +
        //                     " when EvalSal_Percentage = 100 then '100 %' " +
        //                     " when EvalSal_Percentage > 100 then '>100 %' End as 'Rate' " +
        //                     ", count(DISTINCT MP_ID) as 'numOfProjects' ";
        //        var condition = " where EvalSal_Percentage is not null ";

        //        //Type
        //        if (Type != "")
        //        {
        //            if (Type.Contains("Loan")) condition += " and MP.MP_Type = 0";
        //            else if (Type.Contains("Grant")) condition += " and MP.MP_Type = 1";
        //        }

        //        //MP State
        //        if (State != "")
        //        {
        //            if (State == "ممول+منتهي")
        //                condition += " and state.ID in (4,5) ";
        //            else if (State == "ممول+منتهي+ملغى")
        //                condition += " and state.ID in (4,5,7) ";
        //            else
        //                condition += " and state.Name_Ar like N'" + State + "' ";
        //        }
        //        //Donor
        //        if (Donor != "")
        //            condition += " and donor.Name like '" + Donor + "'";
        //        //Category
        //        if (Category != "")
        //            condition += " and C.C_Name like N'" + Category + "'";
        //        //Parish
        //        if (Parish != "")
        //        {
        //            if (Parish == "Christian")
        //                condition += " and P.P_Parish not like  N'Muslim'";
        //            else condition += " and P.P_Parish like  N'" + Parish + "'";
        //        }

        //        //Marital Status
        //        if (MaritalStatus != "")
        //        {
        //            if (MaritalStatus.Contains("متزوج")) condition += " and P.P_MaritalStatus like N'%متزوج%'";
        //            else if (MaritalStatus.Contains("عازب")) condition += " and P.P_MaritalStatus like N'%عازب%'";
        //            else if (MaritalStatus.Contains("مطلق")) condition += " and P.P_MaritalStatus like N'%مطلق%'";
        //            else if (MaritalStatus.Contains("أرمل")) condition += " and P.P_MaritalStatus like N'%أرمل%'";
        //            else if (MaritalStatus.Contains("مخطوب")) condition += " and P.P_MaritalStatus like N'%مخطوب%'";
        //        }

        //        //Sex
        //        if (Sex != "")
        //        {
        //            if (Sex.Contains("ذكر")) condition += " and P.P_Sex like N'%ذكر%'";
        //            else if (Sex.Contains("أنثى")) condition += " and P.P_Sex like N'%أنثى%'";
        //        }

        //        ////Age 
        //        if (Age != "")
        //        {
        //            if (Age.Contains("<16")) condition += " and  TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) < 16 ";
        //            else if (Age.Contains("16-26"))
        //                condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 16 and 26 ";
        //            else if (Age.Contains("27-35"))
        //                condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 27 and 35 ";
        //            else if (Age.Contains("36-45"))
        //                condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 36 and 45 ";
        //            else if (Age.Contains("46-60"))
        //                condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 46 and 60 ";
        //            else if (Age.Contains(">60"))
        //                condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) > 60 ";
        //        }

        //        //Kind
        //        if (ProjectReason != "")
        //        {
        //            if (ProjectReason.Contains("توسيع/تطوير مشروع حالي"))
        //                condition += " and MP.MP_ProjectKind like 'Expand'";
        //            else if (ProjectReason.Contains("إنشاء مشروع جديد"))
        //                condition += " and MP.MP_ProjectKind like 'New'";
        //            else condition += " and MP.MP_ProjectKind like ''";
        //        }

        //        //Rate
        //        if (ProfitRate != "")
        //        {
        //            if (ProfitRate == "<0 %") condition += " and EvalSal_Percentage < 0 ";
        //            else if (ProfitRate == "0 %") condition += " and EvalSal_Percentage = 0 ";
        //            else if (ProfitRate == "1-25 %") condition += " and EvalSal_Percentage BETWEEN 1 and 25 ";
        //            else if (ProfitRate == "26-50 %") condition += " and EvalSal_Percentage BETWEEN 26 and 50 ";
        //            else if (ProfitRate == "51-75 %") condition += " and EvalSal_Percentage BETWEEN 51 and 75 ";
        //            else if (ProfitRate == "76-99 %") condition += " and EvalSal_Percentage BETWEEN 76 and 99 ";
        //            else if (ProfitRate == "100 %") condition += " and EvalSal_Percentage = 100 ";
        //            else condition += " and EvalSal_Percentage > 100 ";
        //        }

        //        //Experience
        //        if (HasExperience != "")
        //        {
        //            from3 = " left join microproject_score ms on MP.MP_ID = ms.MicroProject_ID ";
        //            from += from3;

        //            if (HasExperience.Contains("يوجد")) condition += " and ms.value = 1";
        //            else if (HasExperience.Contains("لا يوجد")) condition += " and  ms.value = 0";
        //            else condition += " and  ms.value = -1 ";
        //        }

        //        //Partners
        //        if (Partners != "")
        //        {
        //            if (Partners.Contains("شراكة"))
        //                condition += " and Partnership = 2 ";
        //            else if (Partners.Contains("فردي")) condition += " and Partnership = 1 ";
        //            else condition += "";
        //        }
        //        //Apply Date
        //        if (ApplyDate != "") condition += " and Year(MP.MP_DateOfRequest) like '" + ApplyDate + "'";

        //        //Fund Date
        //        if (FundDate != "") condition += " and Year(l.Loan_DateTaken) like '" + FundDate + "'";

        //        //Apply Date
        //        if (ApplyDate_Condition != "") condition += ApplyDate_Condition;
        //        //Funded Date
        //        if (FundedDate_Condition != "") condition += FundedDate_Condition;

        //        var groupBycondition = " group by Rate ORDER BY `EvalSal_Percentage` ";
        //        MySS.query += from + from2 + condition + groupBycondition;
        //        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
        //        MySS.sc.ExecuteNonQuery();
        //        MySS.da = new MySqlDataAdapter(MySS.sc);
        //        MySS.dt = new DataTable();
        //        MySS.da.Fill(MySS.dt);
        //        results_dataGridView.DataSource = MySS.dt;
        //        Program.MyConn.Close();

        //        //check connection//
        //        Program.buildConnection();

        //        //count the results
        //        var sel = "select count(DISTINCT P_ID) " + from + from2;
        //        sel += condition;
        //        MySqlCommand sc = new MySqlCommand(sel, Program.MyConn);
        //        Count_label.Text = "All:" + sc.ExecuteScalar();
        //        Program.MyConn.Close();

        //        var max_value = 0;
        //        chart1.Series[10].Name = "Income Change";
        //        chart1.Series[10].Font = new Font("AvenirNext LT Pro Bold", 14f, FontStyle.Bold);
        //        SetChartAreaProperties(max_value);

        //        foreach (var series in chart1.Series)
        //            series.Points.Clear();

        //        chart1.Series[10].ChartArea = chart1.ChartAreas[0].Name;
        //        chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
        //        chart1.Series[10].Legend = chart1.Legends[0].Name;

        //        chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
        //            chart1.Series[3].Enabled =
        //                chart1.Series[4].Enabled = chart1.Series[5].Enabled = chart1.Series[6].Enabled =
        //                    chart1.Series[7].Enabled =
        //                        chart1.Series[8].Enabled = chart1.Series[9].Enabled =
        //                            chart1.Series[11].Enabled =   false;
        //        chart1.Series[10].Enabled = true;

        //        chart1.Series[10].ChartType = Chart_type;

        //        for (var i = 0; i < results_dataGridView.Rows.Count; i++)
        //        {
        //            chart1.Series[10].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
        //                Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
        //            if (Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value) > max_value)
        //                max_value = Convert.ToInt32(results_dataGridView.Rows[i].Cells[1].Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        #endregion

        //private void MoneyFinancedAndNeeded_Parish_BindChart(string Donor,string DonorGroup, string Category, string Parish, string Sex,
        //    string Marital_Status, string state, string ProjectReason, string ProfitRate, string HasExperience,string Partners
        //    ,string ApplyDate, string FundDate, string ApplyDate_Condition, string FundedDate_Condition)
        //{
        //    try
        //    {
        //        //check connection//
        //        Program.buildConnection();
        //        var from2 = " left outer join loan L on L.MicroProject_ID = MP.MP_ID ";

        //        MySS.query =
        //            "SELECT P_Parish ,sum(cast(MP_RequestedAmount as DECIMAL(9, 2))) as MoneyNeeded ,sum(L.Loan_Amount) as MoneyFunded "
        //            + from + from2;

        //        var condition = " where P_Parish != '' ";

        //        //// Donor  ////
        //        if (Donor != "")
        //            condition += " and MP_Donor like N'" + Donor + "'";

        //        //// category  ////
        //        if (Category != "")
        //            condition += " and C.C_Name like N'%" + Category + "%'";


        //        ////  parish ////
        //        if (Parish != "")
        //            condition += " and P.P_Parish like N'" + Parish + "'";

        //        ////  Sex ////
        //        if (Sex != "")
        //            condition += " and P.P_Sex like N'" + Sex + "'";

        //        ////  Marital_Status ////
        //        if (Marital_Status != "")
        //        {
        //            var m = Marital_Status.Remove(Marital_Status.Length - 2, 2);
        //            condition += " and P.P_MaritalStatus like N'" + m + "%'";
        //        }

        //        //MP State
        //        if (state != "")
        //        {
        //            if (state == "ممول+منتهي")
        //                condition += " and state.ID in (4,5) ";
        //            else if (state == "ممول+منتهي+ملغى")
        //                condition += " and state.ID in (4,5,7) ";
        //            else
        //                condition += " and state.Name_Ar like N'" + state + "' ";
        //        }

        //        //Kind
        //        if (ProjectReason != "")
        //        {
        //            if (ProjectReason.Contains("توسيع/تطوير مشروع حالي"))
        //                condition += " and MP.MP_ProjectKind like 'Expand'";
        //            else if (ProjectReason.Contains("إنشاء مشروع جديد"))
        //                condition += " and MP.MP_ProjectKind like 'New'";
        //            else condition += " and MP.MP_ProjectKind like ''";
        //        }

        //        //Rate
        //        if (ProfitRate != "")
        //        {
        //            if (ProfitRate == "<0 %") condition += " and EvalSal_Percentage < 0 ";
        //            else if (ProfitRate == "0 %") condition += " and EvalSal_Percentage = 0 ";
        //            else if (ProfitRate == "1-25 %") condition += " and EvalSal_Percentage BETWEEN 1 and 25 ";
        //            else if (ProfitRate == "26-50 %") condition += " and EvalSal_Percentage BETWEEN 26 and 50 ";
        //            else if (ProfitRate == "51-75 %") condition += " and EvalSal_Percentage BETWEEN 51 and 75 ";
        //            else if (ProfitRate == "76-99 %") condition += " and EvalSal_Percentage BETWEEN 76 and 99 ";
        //            else if (ProfitRate == "100 %") condition += " and EvalSal_Percentage = 100 ";
        //            else condition += " and EvalSal_Percentage > 100 ";

        //            from2 = " left outer join person_statistics ps on ps.Person_ID = P.P_ID ";
        //            from += from2;
        //        }

        //        //Experience
        //        if (HasExperience != "")
        //        {
        //            from3 = " left join microproject_score ms on MP.MP_ID = ms.MicroProject_ID ";
        //            from += from3;

        //            if (HasExperience.Contains("يوجد")) condition += " and ms.value = 1";
        //            else if (HasExperience.Contains("لا يوجد")) condition += " and  ms.value = 0";
        //            else condition += " and  ms.value = -1 ";
        //        }
        //        //Apply Date
        //        if (ApplyDate != "") condition += " and Year(MP.MP_DateOfRequest) like '" + ApplyDate + "'";

        //        //Fund Date
        //        if (FundDate != "") condition += " and Year(l.Loan_DateTaken) like '" + FundDate + "'";

        //        //Apply Date
        //        if (ApplyDate_Condition != "") condition += ApplyDate_Condition;
        //        //Funded Date
        //        if (FundedDate_Condition != "") condition += FundedDate_Condition;

        //        var groupBy = " group by P_Parish ";
        //        MySS.query += condition + groupBy;
        //        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
        //        MySS.sc.ExecuteNonQuery();
        //        MySS.da = new MySqlDataAdapter(MySS.sc);
        //        MySS.dt = new DataTable();
        //        MySS.da.Fill(MySS.dt);
        //        results_dataGridView.DataSource = MySS.dt;
        //        Program.MyConn.Close();

        //        //check connection//
        //        Program.buildConnection();
        //        //count the results
        //        var sel = "select count(DISTINCT MP_ID) " + from + from2;
        //        sel += condition;
        //        MySS.sc = new MySqlCommand(sel, Program.MyConn);
        //        Count_label.Text = "All:" + MySS.sc.ExecuteScalar();

        //        //count all   ,
        //        sel = "select sum(cast(MP_RequestedAmount as DECIMAL(9, 2))) " + from + from2;
        //        sel += condition;
        //        MySS.sc = new MySqlCommand(sel, Program.MyConn);
        //        var Paid = Convert.ToDouble(MySS.sc.ExecuteScalar());
        //        MoneyNeeded_textBox.Text = Convert.ToDecimal(Paid).ToString("#,##0");

        //        sel = "select sum(L.Loan_Amount) " + from + from2;
        //        sel += condition;
        //        MySS.sc = new MySqlCommand(sel, Program.MyConn);
        //        Paid = Convert.ToDouble(MySS.sc.ExecuteScalar());
        //        MoneyFinanced_textBox.Text = Convert.ToDecimal(Paid).ToString("#,##0");
        //        Program.MyConn.Close();

        //        decimal max_value = 0;
        //        //clear chart
        //        foreach (var series in chart1.Series) series.Points.Clear();

        //        chart1.Series[0].Enabled = chart1.Series[1].Enabled = chart1.Series[2].Enabled =
        //            chart1.Series[3].Enabled =
        //                chart1.Series[4].Enabled = chart1.Series[5].Enabled = chart1.Series[6].Enabled =
        //                    chart1.Series[7].Enabled =
        //                        chart1.Series[8].Enabled = chart1.Series[9].Enabled = chart1.Series[10].Enabled = false;

        //        chart1.Series[11].Enabled = chart1.Series[12].Enabled = true;
        //        chart1.Series[11].ChartType = chart1.Series[12].ChartType = Chart_type;

        //        chart1.Series[11].ChartArea = chart1.Series[12].ChartArea = chart1.ChartAreas[0].Name;
        //        chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;

        //        chart1.Series[11].Legend = chart1.Series[12].Legend = chart1.Legends[0].Name;

        //        for (var i = 0; i < results_dataGridView.Rows.Count; i++)
        //        {
        //            //fill chart
        //            chart1.Series[11].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
        //                Convert.ToDecimal(results_dataGridView.Rows[i].Cells[1].Value.ToString()));
        //            //check if is there null values
        //            var value = results_dataGridView.Rows[i].Cells[2].Value.ToString();
        //            if (string.IsNullOrEmpty(value)) value = "0";

        //            chart1.Series[12].Points.AddXY(results_dataGridView.Rows[i].Cells[0].Value.ToString(),
        //                Convert.ToDecimal(value));
        //        }

        //        chart1.ChartAreas[0].AxisY.Title = "Money(SP)";
        //        chart1.ChartAreas[0].AxisX.Title = "Parishes";
        //        chart1.ChartAreas[0].AxisX.Interval = 1;
        //        chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0,,M";

        //        chart1.Series[11].SmartLabelStyle.Enabled = chart1.Series[12].SmartLabelStyle.Enabled = false;
        //        chart1.Series[11].LabelAngle = chart1.Series[12].LabelAngle = -90;
        //        chart1.Series[11].SmartLabelStyle.AllowOutsidePlotArea =
        //            chart1.Series[12].SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Partial;

        //        chart1.Series[11].Name = "Requested";
        //        chart1.Series[12].Name = "Funded";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void Bind_All_ComboBoxes()
        {
            user_mode = false;

            string categroy_query = "SELECT C_ID as 'ID',C_Name as 'Name' from category ORDER BY C_Name ASC;";
            string sub_category_query = "SELECT ID, Name, Category_ID from `subcategory` ORDER BY Name ASC;";
            string street_query = "SELECT ID, Name from `street` order by Name ASC;";
            string donor_query = "SELECT ID, Name from `donor` ORDER BY Name ASC;";
            string donor_group_query = "SELECT ID, Name, Donor_ID from `donorgroup` ORDER BY Name ASC;";
            string fundType_query = "SELECT ID, Name from `fundtype` ORDER BY ID ASC;";
            string microprojecttype_query = "SELECT ID, Name from `microprojecttype` ORDER BY ID ASC;";
            string microprojectsubtype_query = "SELECT ID, Name,Type_ID from `microprojectsubtype` ORDER BY ID ASC;";

            string query = categroy_query + sub_category_query + street_query + donor_query + donor_group_query
                + fundType_query + microprojecttype_query + microprojectsubtype_query;
            Program.buildConnection();
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            ds = new DataSet();
            da.Fill(ds);
            Program.MyConn.Close();

            Set_comboBox_datasource(MP_Category_comboBox, ds.Tables[0]);
            Set_comboBox_datasource(SubCategory_comboBox, ds.Tables[1]);
            Set_comboBox_datasource(Street_comboBox, ds.Tables[2]);
            Set_comboBox_datasource(Donor_comboBox, ds.Tables[3]);
            Set_comboBox_datasource(DonorGroup_comboBox, ds.Tables[4]);
            Set_comboBox_datasource(FundType_comboBox, ds.Tables[5]);
            Set_comboBox_datasource(Type_comboBox, ds.Tables[6]);
            Set_comboBox_datasource(SubType_comboBox, ds.Tables[7]);

            ApplyDate_comboBox.Items.Clear();
            FundDate_comboBox.Items.Clear();
            for (int i = 2018; i <= DateTime.Today.Year; i++)
            {
                ApplyDate_comboBox.Items.Add(i.ToString());
                FundDate_comboBox.Items.Add(i.ToString());
            }

            user_mode = true;
        }

        private void Set_comboBox_datasource(ComboBox cBox, DataTable c_dt)
        {
            cBox.DataSource = null;
            cBox.DisplayMember = "Name";
            cBox.ValueMember = "ID";
            cBox.DataSource = c_dt;
            cBox.SelectedIndex = -1;
        }

        private void SubCategory_bind(string Category_ID)
        {
            user_mode = false;
            SubCategory_comboBox.DataSource = null;
            SubCategory_comboBox.DisplayMember = "Name";
            SubCategory_comboBox.ValueMember = "ID";

            if (Category_ID != "")
            {
                DataRow[] rows = null;
                rows = ds.Tables[1].Select("Category_ID=" + Category_ID);

                DataTable selected_rows_dt = new DataTable();
                selected_rows_dt = ds.Tables[1].Copy();
                selected_rows_dt.Rows.Clear();
                foreach (DataRow row in rows)
                    selected_rows_dt.ImportRow(row);

                if (selected_rows_dt.Rows.Count != 0 && selected_rows_dt != null)
                    SubCategory_comboBox.DataSource = selected_rows_dt;
            }
            else
            {
                SubCategory_comboBox.DataSource = ds.Tables[1];

            }
            SubCategory_comboBox.SelectedIndex = -1;
            user_mode = true;
        }
        private void SubType_bind(string Type_ID)
        {
            user_mode = false;
            SubType_comboBox.DataSource = null;
            SubType_comboBox.DisplayMember = "Name";
            SubType_comboBox.ValueMember = "ID";

            if (Type_ID != "")
            {
                DataRow[] rows = null;
                rows = ds.Tables[7].Select("Type_ID=" + Type_ID);

                DataTable selected_rows_dt = new DataTable();
                selected_rows_dt = ds.Tables[7].Copy();
                selected_rows_dt.Rows.Clear();
                foreach (DataRow row in rows)
                    selected_rows_dt.ImportRow(row);

                if (selected_rows_dt.Rows.Count != 0 || selected_rows_dt != null)
                    SubType_comboBox.DataSource = selected_rows_dt;
                else
                {

                }
            }
            else
            {
                SubType_comboBox.DataSource = ds.Tables[7];

            }
            SubType_comboBox.SelectedIndex = -1;
            user_mode = true;
        }
        private void DonorGroup_bind(string Donor_ID)
        {
            user_mode = false;
            DonorGroup_comboBox.DataSource = null;
            DonorGroup_comboBox.DisplayMember = "Name";
            DonorGroup_comboBox.ValueMember = "ID";

            if (Donor_ID != "")
            {
                DataRow[] rows = null;
                rows = ds.Tables[4].Select("Donor_ID=" + Donor_ID);

                DataTable selected_rows_dt = new DataTable();
                selected_rows_dt = ds.Tables[4].Copy();
                selected_rows_dt.Rows.Clear();
                foreach (DataRow row in rows)
                    selected_rows_dt.ImportRow(row);

                if (selected_rows_dt.Rows.Count != 0 || selected_rows_dt != null)
                    DonorGroup_comboBox.DataSource = selected_rows_dt;
                else
                {

                }
            }
            else
            {
                DonorGroup_comboBox.DataSource = ds.Tables[4];

            }
            DonorGroup_comboBox.SelectedIndex = -1;
            user_mode = true;
        }
        #endregion

        private void DonorGroup_comboBox_Enter(object sender, EventArgs e)
        {
            if (Donor_comboBox.SelectedIndex != -1)
            {
                int donor_id = Convert.ToInt32(Donor_comboBox.SelectedValue);
                DonorGroup_bind(donor_id.ToString());
            }
            else
            {
                // set the default one //
                DonorGroup_comboBox.DataSource = null;
                DonorGroup_comboBox.DisplayMember = "Name";
                DonorGroup_comboBox.ValueMember = "ID";
                DonorGroup_comboBox.DataSource = donor_group_dt;
            }
        }
        private void SubCategory_comboBox_Enter(object sender, EventArgs e)
        {
            if (MP_Category_comboBox.SelectedIndex != -1)
            {
                int id = Convert.ToInt32(MP_Category_comboBox.SelectedValue);
                SubCategory_bind(id.ToString());
            }
            else
            {
                // set the default one //
                SubCategory_comboBox.DataSource = null;
                SubCategory_comboBox.DisplayMember = "Name";
                SubCategory_comboBox.ValueMember = "ID";
                SubCategory_comboBox.DataSource = sub_category_dt;
            }
        }

        #region chart methods

        private void SetChartAreaProperties(int max_value)
        {
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#,#";
        }

        private void Build_Legend(int index)
        {
            try
            {
                var legend1 = new Legend();
                legend1.Font = new Font("Avenir LT Std 65 Medium", 10f, FontStyle.Regular);
                legend1.TitleAlignment = StringAlignment.Center;
                legend1.DockedToChartArea = chart1.ChartAreas[index].Name;
                legend1.Docking = Docking.Top;
                legend1.LegendStyle = LegendStyle.Table;
                legend1.TableStyle = LegendTableStyle.Wide;
                legend1.IsEquallySpacedItems = true;
                legend1.IsDockedInsideChartArea = false;
                legend1.Alignment = StringAlignment.Far;

                if (Settings.Default.theme == "Light")
                {
                    legend1.BackColor = Color.FromArgb(189, 189, 189);
                    legend1.ForeColor = Color.Black;
                }
                else
                {
                    legend1.ForeColor = Color.White;
                    legend1.BackColor = Color.FromArgb(75, 75, 75);
                }

                chart1.Legends.Add(legend1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Build_ChartArea(string name, int index)
        {
            try
            {
                var chartArea1 = new ChartArea(name);
                chartArea1.AxisY.LabelStyle.Font = new Font("Avenir LT Std 65 Medium", 11f, FontStyle.Regular);
                chartArea1.AxisX.LabelStyle.Font = new Font("Avenir LT Std 65 Medium", 11f, FontStyle.Regular);
                chartArea1.AxisX.IsLabelAutoFit = true;
                chartArea1.AxisX.LabelAutoFitMaxFontSize = 11;
                chartArea1.AxisX.LabelAutoFitMinFontSize = 8;
                chartArea1.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont;

                if (Settings.Default.theme == "Light")
                {
                    chartArea1.BackColor = Color.FromArgb(189, 189, 189);
                    chartArea1.BackHatchStyle = ChartHatchStyle.LightVertical;
                    chartArea1.BorderColor = Color.FromArgb(75, 75, 75);

                    chartArea1.AxisX.InterlacedColor = Color.Black;
                    chartArea1.AxisX.LineColor = Color.Black;
                    chartArea1.AxisX.TitleForeColor = Color.Black;
                    chartArea1.AxisX.LabelStyle.ForeColor = Color.Black;
                    chartArea1.AxisX.MajorGrid.LineColor = Color.FromArgb(96, 96, 96);
                    chartArea1.AxisX.MinorGrid.LineColor = Color.FromArgb(96, 96, 96);

                    chartArea1.AxisY.InterlacedColor = Color.Black;
                    chartArea1.AxisY.LineColor = Color.Black;
                    chartArea1.AxisY.TitleForeColor = Color.Black;
                    chartArea1.AxisY.LabelStyle.ForeColor = Color.Black;
                    chartArea1.AxisY.MajorGrid.LineColor = Color.FromArgb(96, 96, 96);
                    chartArea1.AxisY.MinorGrid.LineColor = Color.FromArgb(96, 96, 96);
                }
                else
                {
                    chartArea1.BackColor = Color.FromArgb(75, 75, 75);
                    chartArea1.BackHatchStyle = ChartHatchStyle.None;
                    chartArea1.BorderColor = Color.FromArgb(189, 189, 189);

                    chartArea1.AxisX.InterlacedColor = Color.FromArgb(96, 96, 96);
                    chartArea1.AxisX.LineColor = Color.FromArgb(189, 189, 189);
                    chartArea1.AxisX.TitleForeColor = Color.FromArgb(189, 189, 189);
                    chartArea1.AxisX.LabelStyle.ForeColor = Color.FromArgb(189, 189, 189);
                    chartArea1.AxisX.MajorGrid.LineColor = Color.FromArgb(96, 96, 96);
                    chartArea1.AxisX.MinorGrid.LineColor = Color.FromArgb(96, 96, 96);

                    chartArea1.AxisY.InterlacedColor = Color.FromArgb(96, 96, 96);
                    chartArea1.AxisY.LineColor = Color.FromArgb(189, 189, 189);
                    chartArea1.AxisY.TitleForeColor = Color.FromArgb(189, 189, 189);
                    chartArea1.AxisY.LabelStyle.ForeColor = Color.FromArgb(189, 189, 189);
                    chartArea1.AxisY.MajorGrid.LineColor = Color.FromArgb(96, 96, 96);
                    chartArea1.AxisY.MinorGrid.LineColor = Color.FromArgb(96, 96, 96);
                }

                chart1.ChartAreas.Insert(index, chartArea1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region mouse hover

        private void AdvancedSearch_button_MouseEnter(object sender, EventArgs e)
        {
            if (Hided)
                AdvancedSearch_button.BackgroundImage = Resources.Show2_D;
            else
                AdvancedSearch_button.BackgroundImage = Resources.Hide2_D;
        }

        private void AdvancedSearch_button_MouseLeave(object sender, EventArgs e)
        {
            if (Hided)
                AdvancedSearch_button.BackgroundImage = Resources.Show2_L;
            else
                AdvancedSearch_button.BackgroundImage = Resources.Hide2_L;
        }

        private void ExportToExcel_button_MouseEnter(object sender, EventArgs e)
        {
            ExportToExcel_button.BackgroundImage = Resources.Excel_L;
        }

        private void ExportToExcel_button_MouseLeave(object sender, EventArgs e)
        {
            ExportToExcel_button.BackgroundImage = Resources.Excel_D;
        }

        #endregion

        #region right click menu

        private void refreshPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            X_Axis_comboBox_SelectedIndexChanged(sender, e);
        }

        private void showHideFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdvancedSearch_button_Click(sender, e);
        }

        private void clearAllFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FundType_comboBox.Text = Type_comboBox.Text = SubType_comboBox.Text
                = MP_Status_comboBox.Text = Partnership_comboBox.Text
                = Donor_comboBox.Text = DonorGroup_comboBox.Text 
                = MP_Category_comboBox.Text = SubCategory_comboBox.Text
                = P_Parish_comboBox.Text = Age_comboBox.Text = Gender_comboBox.Text = MaritalStatus_comboBox.Text
                = ApplyDate_comboBox.Text = FundDate_comboBox.Text = "";

            //MoneyFinanced_textBox.Text = MoneyNeeded_textBox.Text = "";
            ApplyDateFrom_dateTimePicker.Value = FundDateFrom_bcDateTimePicker.Value = DateTime.Parse("01-01-2018");
            ApplyDateTo_bcDateTimePicker.Value = FundDateTo_bcDateTimePicker.Value = DateTime.Now;
            FundedDate_checkBox.Checked = ApplyDate_checkBox.Checked =
                Funded_checkBox.Checked = Requested_checkBox.Checked = false;

            X_Axis_comboBox_SelectedIndexChanged(sender, e);
        }

        private void showDetailsInSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var search_Form = new Search_Form(main_form
                    , FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text
                    , MP_Status_comboBox.Text, Partnership_comboBox.Text
                    , replaceQuotation(Donor_comboBox.Text),replaceQuotation(DonorGroup_comboBox.Text)
                    , replaceQuotation(MP_Category_comboBox.Text), SubCategory_comboBox.Text
                    , P_Parish_comboBox.Text, Age_comboBox.Text, Gender_comboBox.Text, MaritalStatus_comboBox.Text);

                main_form.showNewTab(search_Form, "Search",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        public void Check_Conditions(
            string FundType, string Type, string SubType
            , string Partners , string State
            , string Donor , string DonorGroup
            , string Category, string SubCategory
            , string Parish, string Age, string Sex, string MaritalStatus    
            , string ApplyDate, string FundDate
            , string ApplyDate_Condition, string FundedDate_Condition)
        {
            condition = " where 1 ";

            //Fund Type
            if (FundType != "")
            {
                condition += " and fundtype.Name like '" + FundType + "'";
            }
            //Type
            if (Type != "")
            {
                condition += " and microprojecttype.Name like '" + Type + "'";
            }
            //Sub Type
            if (SubType != "")
            {
                condition += " and microprojectsubtype.Name like '" + SubType + "'";
            }

            //MP State
            if (State != "")
            {
                if (State == "ممول+منتهي")
                    condition += " and state.ID in (4,5) ";
                else if (State == "ممول+منتهي+ملغى")
                    condition += " and state.ID in (4,5,7) ";
                else
                    condition += " and state.Name_Ar like N'" + State + "' ";
            }
            //Donor
            if (Donor != "")
                condition += " and donor.Name like '" + Donor + "'";

            //Donor Group
            if (DonorGroup != "")
                condition += " and donorgroup.Name like '" + DonorGroup + "'";

            //Category
            if (Category != "")
                condition += " and C.C_Name like N'" + Category + "'";

            //Project SUB Category
            if (SubCategory != "")
                condition += " and sub.Name like N'" + SubCategory + "'";
             
            //Parish
            if (Parish != "")
            {
                if (Parish == "Christian")
                    condition += " and P.P_Parish not like  N'Muslim'";
                else condition += " and P.P_Parish like  N'" + Parish + "'";
            }

            //Marital Status
            if (MaritalStatus != "")
            {
                if (MaritalStatus.Contains("متزوج")) condition += " and P.P_MaritalStatus like N'%متزوج%'";
                else if (MaritalStatus.Contains("عازب")) condition += " and P.P_MaritalStatus like N'%عازب%'";
                else if (MaritalStatus.Contains("مطلق")) condition += " and P.P_MaritalStatus like N'%مطلق%'";
                else if (MaritalStatus.Contains("أرمل")) condition += " and P.P_MaritalStatus like N'%أرمل%'";
                else if (MaritalStatus.Contains("مخطوب")) condition += " and P.P_MaritalStatus like N'%مخطوب%'";
            }

            //Sex
            if (Sex != "")
            {
                if (Sex.Contains("ذكر")) condition += " and P.P_Sex like N'%ذكر%'";
                else if (Sex.Contains("أنثى")) condition += " and P.P_Sex like N'%أنثى%'";
            }

            ////Age 
            if (Age != "")
            {
                if (Age.Contains("<16")) condition += " and  TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) < 16 ";
                else if (Age.Contains("16-26"))
                    condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 16 and 26 ";
                else if (Age.Contains("27-35"))
                    condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 27 and 35 ";
                else if (Age.Contains("36-45"))
                    condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 36 and 45 ";
                else if (Age.Contains("46-60"))
                    condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 46 and 60 ";
                else if (Age.Contains(">60"))
                    condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) > 60 ";
            }
             
            //Partners
            if (Partners != "")
            {
                if (Partners.Contains("شراكة"))
                    condition += " and Partnership = 2 ";
                else if (Partners.Contains("فردي")) condition += " and Partnership = 1 ";
                else condition += "";
            }

            //Apply Date
            if (ApplyDate != "") condition += " and Year(MP.MP_DateOfRequest) like '" + ApplyDate + "'";

            //Fund Date
            if (FundDate != "") condition += " and Year(l.Loan_DateTaken) like '" + FundDate + "'";

            //Apply Date
            if (ApplyDate_Condition != "") condition += ApplyDate_Condition;
            //Funded Date
            if (FundedDate_Condition != "") condition += FundedDate_Condition; 
        }

        #region checkbox clicks

        private void ApplyDate_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ApplyDate_checkBox.Checked)
            {
                ApplyDateFrom_dateTimePicker.Enabled = ApplyDateFrom_label.Enabled
                    = ApplyDateTo_bcDateTimePicker.Enabled = ApplyDateTo_label.Enabled = true;
                ApplyDateFrom_dateTimePicker_ValueChanged(sender, e);
            }
            else
            {
                ApplyDate_condition = "";
                ApplyDateFrom_dateTimePicker.Enabled = ApplyDateFrom_label.Enabled
                    = ApplyDateTo_bcDateTimePicker.Enabled = ApplyDateTo_label.Enabled = false;
                X_Axis_comboBox_SelectedIndexChanged(sender, e);
            }
        }

        private void FundedDate_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FundedDate_checkBox.Checked)
            {
                FundDateFrom_bcDateTimePicker.Enabled = FundedDateFrom_label.Enabled
                    = FundDateTo_bcDateTimePicker.Enabled = FundedDateTo_label.Enabled = true;
                FundedDateTo_bcDateTimePicker_ValueChanged(sender, e);
            }
            else
            {
                FundedDate_condition = "";
                FundDateFrom_bcDateTimePicker.Enabled = FundedDateFrom_label.Enabled
                    = FundDateTo_bcDateTimePicker.Enabled = FundedDateTo_label.Enabled = false;
                X_Axis_comboBox_SelectedIndexChanged(sender, e);
            }
        }

        private void Requested_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Requested_checkBox.Checked)
                RequestedFrom_label.Enabled = RequestedFrom_textBox.Enabled
                    = RequestedTo_label.Enabled = RequestedTo_textBox.Enabled = true;
            else
                RequestedFrom_label.Enabled = RequestedFrom_textBox.Enabled
                    = RequestedTo_label.Enabled = RequestedTo_textBox.Enabled = false;
        }

        private void Funded_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Funded_checkBox.Checked)
                FundedFrom_label.Enabled = FundedFrom_textBox.Enabled
                    = FundedTo_label.Enabled = FundedTo_textBox.Enabled = true;
            else
                FundedFrom_label.Enabled = FundedFrom_textBox.Enabled
                    = FundedTo_label.Enabled = FundedTo_textBox.Enabled = false;
        }

        private void RequestedFrom_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (RequestedFrom_textBox.Text != "")
                {
                    RequestedFrom_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(RequestedFrom_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    RequestedFrom_textBox.SelectionStart = RequestedFrom_textBox.Text.Length;
                    RequestedFrom_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RequestedTo_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (RequestedTo_textBox.Text != "")
                {
                    RequestedTo_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(RequestedTo_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    RequestedTo_textBox.SelectionStart = RequestedTo_textBox.Text.Length;
                    RequestedTo_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
        private void FundedFrom_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (FundedFrom_textBox.Text != "")
                {
                    FundedFrom_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(FundedFrom_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    FundedFrom_textBox.SelectionStart = FundedFrom_textBox.Text.Length;
                    FundedFrom_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
        private void FundedTo_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (FundedTo_textBox.Text != "")
                {
                    FundedTo_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(FundedTo_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    FundedTo_textBox.SelectionStart = FundedTo_textBox.Text.Length;
                    FundedTo_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void MP_Category_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //add the type_comboBox_selectedValue_changed to fill the data before binding subType  
            try
            {
                if (user_mode)
                    SubCategory_bind(MP_Category_comboBox.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Type_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //add the type_comboBox_selectedValue_changed to fill the data before binding subType  
            try
            {
                if (user_mode)
                    SubType_bind(Type_comboBox.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Donor_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //add the type_comboBox_selectedValue_changed to fill the data before binding subType  
            try
            {
                if (user_mode)
                    DonorGroup_bind(Donor_comboBox.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}