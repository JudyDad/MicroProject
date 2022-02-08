﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using Label = System.Windows.Forms.Label;
using System.Drawing;

namespace MyWorkApplication
{
    public partial class TaskBoard_Form : Form
    {
        private int[] arr;
        private DataGridViewImageCell cell;
        private int col_index;

        private readonly List<int> Communication_IDs = new List<int>(); 
        private string condition = "";
        private readonly ImageConverter converter = new ImageConverter();
        private DataGridViewColumnSelector cs;
        private string direction = "asc";

        private readonly string from = @" from `task_microproject` TMP
 LEFT outer join `person_microproject` PMP on TMP.MicroProject_ID = PMP.MicroProject_ID
 LEFT outer join `microproject` MP on PMP.MicroProject_ID = MP.MP_ID
 LEFT outer join `person` P1 on P1.P_ID = PMP.Person_ID
 LEFT outer join `loan` L on L.MicroProject_ID = MP.MP_ID
 LEFT outer join `donor` on donor.ID = MP.MP_Donor
 LEFT outer join `state` on state.ID = MP.MP_State 
 LEFT outer join `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID ";

        private readonly Image img = Resources.OK24;
        private Log l;

        private readonly MainForm main_form;
        private int MicroProject_ID, Task_ID, User_ID;
        private MySqlComponents MySS;
        private NewTheme NewTheme;
        private int Person_ID, PMP_ID;
        private byte[] PicArr;
        private DataRow SelectedDataRow;
        private DataTable tasks_users_dt;
        private TasksOfProjects TasksOfProjects;
        private int tick;
        private UserNotification u;

        public TaskBoard_Form(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void ProjectsTasks_Load(object sender, EventArgs e)
        {
            try
            {
                NewTheme = new NewTheme();
                if (Settings.Default.theme == "Light")
                    NewTheme.Tasks_ToLight(this);
                else
                    NewTheme.Tasks_ToNight(this);
                main_form.Project_label.Visible = main_form.TabName_label.Visible = true;
                main_form.ProjectNumber_label.Visible = main_form.MP_ID_label.Visible = false;
                main_form.TabName_label.Text = "Task Board";

                MySS = new MySqlComponents();
                l = new Log();
                TasksOfProjects = new TasksOfProjects();
                u = new UserNotification();

                Fill_User_Names(); 

                cs = new DataGridViewColumnSelector(MP_dataGridView);
                cs.MaxHeight = 100;
                cs.Width = 110;

                MP_dataGridView.DoubleBuffered(true);
                MP_dataGridView.Focus();

                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
        private void Donor_bind()
        { 
            Program.buildConnection();
            var sc = new MySqlCommand("select ID,Name_abb from `donor` ORDER BY Name ASC ", Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            Program.MyConn.Close();
             
            // بدنا نضيف سطر فاضي قبل الداتا //
            DataRow row = dt2.NewRow();
            row[0] = -1;
            row[1] = "";
            dt2.Rows.InsertAt(row, 0); 

           // f05_Donor_comboBox.DataSource = null;
            f05_Donor_comboBox.DisplayMember = "Name_abb";
            f05_Donor_comboBox.ValueMember = "ID";
            f05_Donor_comboBox.DataSource = dt2;
            f05_Donor_comboBox.SelectedIndex = -1;
        }

        private void Fill_User_Names()
        {
            tasks_users_dt = new DataTable();
            tasks_users_dt = TasksOfProjects.Get_Tasks_Users();

            if (tasks_users_dt.Rows.Count != 0)
                foreach (Control c in Filters_tableLayoutPanel.Controls)
                    if (c.GetType() == typeof(Label))
                    {
                        var lbl = c as Label;
                        DataRow[] row = null;
                        var col_index = lbl.Name.Substring(1, 2);
                        var col_name = "";

                        if (col_index == "07") row = tasks_users_dt.Select("Task like 'القصة انكليزي'");
                        else if (col_index == "08") row = tasks_users_dt.Select("Task like 'تدقيق القصة'");
                        else if (col_index == "09") row = tasks_users_dt.Select("Task like 'القصة فرنسي'");
                        else if (col_index == "10") row = tasks_users_dt.Select("Task like 'صورة شخصية'");
                        else if (col_index == "11") row = tasks_users_dt.Select("Task like 'توقيع العقود'");
                        else if (col_index == "12") row = tasks_users_dt.Select("Task like 'طلب المشتريات'");
                        else if (col_index == "13") row = tasks_users_dt.Select("Task like 'تصوير المشتريات'");
                        else if (col_index == "14") row = tasks_users_dt.Select("Task like 'مشتريات'");
                        else if (col_index == "15") row = tasks_users_dt.Select("Task like 'تسليم كامل المبلغ و تحديد الأقساط'");
                        else if (col_index == "17") row = tasks_users_dt.Select("Task like 'تصوير بعد افتتاح المشروع'");
                        else if (col_index == "18") row = tasks_users_dt.Select("Task like 'Map Location'");
                        else if (col_index == "19") row = tasks_users_dt.Select("Task like 'Web'");  
                        else if (col_index == "20") row = tasks_users_dt.Select("Task like 'Opening Visit'");
                        else if (col_index == "21") row = tasks_users_dt.Select("Task like 'M1'");
                        else if (col_index == "22") row = tasks_users_dt.Select("Task like 'M2'");
                        else if (col_index == "23") row = tasks_users_dt.Select("Task like 'M3'");
                        else if (col_index == "24") row = tasks_users_dt.Select("Task like 'M4'");
                        else if (col_index == "25") row = tasks_users_dt.Select("Task like 'M5'"); 
                        else if (col_index == "26") row = tasks_users_dt.Select("Task like 'Closing Visit'"); 
                        else if (col_index == "27") row = tasks_users_dt.Select("Task like 'Video Captured'");
                        else if (col_index == "28") row = tasks_users_dt.Select("Task like 'Video Edited'");
                        else if (col_index == "29") row = tasks_users_dt.Select("Task like 'Video Uploaded'");

                        if (row == null || row.Length == 0) lbl.Text = "";
                        else lbl.Text = row[0].Field<string>("Username");
                    }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tick == 0)
            {
                tick++;
                bind(null); //first time
                Donor_bind();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void bind(DataTable dt)
        {
            var orderBy_column = "";
            var orderBy = "";

            if (dt != null)
            {
                MySS.dt = new DataTable();
                MySS.dt = dt;
            }
            else
            {
                orderBy = " order by MP.MP_State ASC "; 
                Program.buildConnection();
                var query = @"SELECT PMP.MicroProject_ID as 'رقم المشروع'
,GROUP_CONCAT(DISTINCT CONCAT(P_FirstName,' ', P_LastName,' ابن/ة ',P_FatherName)) as 'المستفيد'
,P_Mobile as 'الموبايل'
,MP.MP_Name as 'اسم المشروع'
,state.Name_ar AS 'حالة المشروع'
,donor.Name_abb AS 'الجهة الممولة'
, microprojecttype.Name AS 'نوع المشروع' 
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 3 limit 1)AS 'القصة انكليزي'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 4 limit 1)AS 'تدقيق القصة'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 5 limit 1)AS 'القصة فرنسي'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 7 limit 1)AS 'صورة شخصية'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 11 limit 1)AS 'توقيع العقود'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 13 limit 1)AS 'طلب المشتريات'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 15 limit 1)AS 'تصوير المشتريات'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 16 limit 1)AS 'مشتريات'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 17 limit 1)AS 'تسليم كامل المبلغ و تحديد الأقساط'
,L.Loan_DateTaken as 'تاريخ بداية المشروع'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 19 limit 1)AS 'تصوير بعد افتتاح المشروع'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 21 limit 1)AS 'Map Location'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 20 limit 1)AS 'Web'

,(SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 25 limit 1)AS 'Opening Visit'
,(SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 26 limit 1)AS 'M1'
,(SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 27 limit 1)AS 'M2'
,(SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 28 limit 1)AS 'M3'
,(SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 29 limit 1)AS 'M4'
,(SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 30 limit 1)AS 'M5'
,(SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 31 limit 1)AS 'Closing Visit'

,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 35 limit 1)AS 'Video Captured'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 36 limit 1)AS 'Video Edited'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 37 limit 1)AS 'Video Uploaded'

,PMP.Person_ID as 'رقم المستفيد'
" + from;
                condition = " where MP.MP_State in (1,4,5) ";
                query += condition;
                query += " Group By PMP.MicroProject_ID ";
                query += orderBy;
                MySS.da = new MySqlDataAdapter(query, Program.MyConn);
                MySS.dt = new DataTable(); 
                MySS.da.Fill(MySS.dt);
            }

            Filters_tableLayoutPanel.Visible = false; 
            MP_dataGridView.DataSource = null;
            MP_dataGridView.Columns.Clear();
            MP_dataGridView.Rows.Clear();

            MP_dataGridView.ColumnHeadersVisible = false;
            MP_dataGridView.DataSource = MySS.dt;
            MP_dataGridView.Columns["تاريخ بداية المشروع"].DefaultCellStyle.Format = "dd/MM/yyyy";

            DataGridViewColumn col = MP_dataGridView.Columns["رقم المستفيد"];
            col.Visible = false;

            ConvertCellsToCheckBox_Color();
            restore_visible_columns();

            count_by_projects();
            Filters_tableLayoutPanel.Visible = true;
        }

        private void count_by_projects()
        {
            /////////////////////////////////
            var s = new StringBuilder();
            s.Append("select MP_ID from `microproject` where MP_ID in ");
            s.Append("(");
            s.Append("select PMP.MicroProject_ID " + from + condition);
            s.Append(")");
            /////////////////////////////////

            Program.buildConnection();
            //count rows
            var sel = "select count(DISTINCT MP_ID) from (" + s + ") as count";
            var sc = new MySqlCommand(sel, Program.MyConn);
            Counter_textBox.Text = sc.ExecuteScalar().ToString();
            MP_dataGridView.ColumnHeadersVisible = true;
            Program.MyConn.Close();
        }

        private void ConvertCellsToCheckBox_Color()
        {
            MP_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            int lastcolumnindex = MP_dataGridView.ColumnCount - 1;

            for (var j = 0; j < MP_dataGridView.ColumnCount; j++)
            {
                MP_dataGridView.Columns[j].SortMode = DataGridViewColumnSortMode.Programmatic;
                //رقم المشروع //المستفيد//اسم المشروع//الموبايل//تاريخ بداية المشروع//رقم المستفيد
                if (j == 16 || j == 0 || j == 1 || j == 2 || j == 3 || j == lastcolumnindex)
                {
                    if (j == 16 || j == 0)
                        for (var i = 0; i < MP_dataGridView.RowCount; i++)
                            MP_dataGridView[j, i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //continue;
                }
                else if (j == 4 || j == 5 || j == 6)
                {
                    for (var i = 0; i < MP_dataGridView.RowCount; i++)
                    {
                        MP_dataGridView[j, i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        if (MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value.ToString() == "منتهي")
                            MP_dataGridView.Rows[i].DefaultCellStyle.BackColor =
                                MP_dataGridView.Rows[i].Cells[j].Style.BackColor = Color.DimGray;
                    }
                }
                else
                {
                    var imageColumn = new DataGridViewImageColumn();
                    imageColumn.HeaderText = imageColumn.Name = MP_dataGridView.Columns[j].HeaderText;
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    imageColumn.DefaultCellStyle.Padding = new Padding(5);
                    imageColumn.DefaultCellStyle.NullValue = new Bitmap(1, 1);
                    imageColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    MP_dataGridView.Columns.Insert(j + 1, imageColumn);
                    for (var k = 0; k < MP_dataGridView.RowCount; k++)
                    {
                        if (MP_dataGridView.Rows[k].Cells[j].Value.ToString() == "0")
                        {
                            cell = new DataGridViewImageCell();
                            cell.Value = DBNull.Value;
                            MP_dataGridView.Rows[k].Cells[j + 1] = cell;
                            MP_dataGridView.Rows[k].Height = 36;
                        }
                        else
                        {
                            cell = new DataGridViewImageCell();
                            PicArr = (byte[]) converter.ConvertTo(img, typeof(byte[]));
                            cell.Value = PicArr;
                            MP_dataGridView.Rows[k].Cells[j + 1] = cell;
                            MP_dataGridView.Rows[k].Height = 36;
                        }

                        MP_dataGridView[j + 1, k].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        if (MP_dataGridView.Rows[k].Cells["حالة المشروع"].Value.ToString() == "منتهي")
                            MP_dataGridView.Rows[k].DefaultCellStyle.BackColor =
                                MP_dataGridView.Rows[k].Cells[j].Style.BackColor = Color.DimGray;
                    }

                    MP_dataGridView.Columns.RemoveAt(j);
                }
            }

            var width = 0;
            var grid_width = 0;

            for (var k = 0; k < MP_dataGridView.Columns.Count; k++)
            {
                if (k == 0)
                    width = 60;
                else if (k == 1 || k == 3)
                    width = 200;
                else if (k == 2 || k == 16 || k == MP_dataGridView.Columns.Count - 1) //last column
                    width = 100;
                else
                    width = 74;
                grid_width += width;
                MP_dataGridView.Columns[k].Width = width;
            }
        }

        private void Refresh_button_Click(object sender, EventArgs e)
        {
            try
            {
                save_visible_columns();
                bind(null);
                MP_idTxtBox_TextChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Refresh_button_MouseEnter(object sender, EventArgs e)
        {
            Refresh_button.BackgroundImage = Resources.Refresh2_L;
        }

        private void Refresh_button_MouseLeave(object sender, EventArgs e)
        {
            Refresh_button.BackgroundImage = Resources.Refresh2_D;
        }

        private void ExportToExcel_button_Click(object sender, EventArgs e)
        {
            try
            {
                _Application app = new Application();
                // creating new WorkBook within Excel application  
                _Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                _Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet = workbook.Sheets[1];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "Exported from App";
                // storing header part in Excel  

                var k = 1;
                for (var i = 1; i < MP_dataGridView.Columns.Count + 1; i++)
                    if (MP_dataGridView.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, k] = MP_dataGridView.Columns[i - 1].HeaderText;
                        k++;
                    }

                // storing Each row and column value to excel sheet  
                var e_i = 2;
                var e_j = 1;

                for (var i = 0; i < MP_dataGridView.Rows.Count - 1; i++)
                    if (MP_dataGridView.Rows[i].Visible)
                    {
                        for (var j = 0; j < MP_dataGridView.Columns.Count; j++)
                            if (MP_dataGridView.Columns[j].Visible)
                                if (MP_dataGridView.Rows[i].Cells[j].Value.ToString() == "System.Byte[]")
                                    worksheet.Cells[e_i, e_j++] = "Done";
                                else
                                    worksheet.Cells[e_i, e_j++] = MP_dataGridView.Rows[i].Cells[j].Value.ToString();
                        e_i++;
                        e_j = 1;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
        private void ExportToExcel_button_MouseEnter(object sender, EventArgs e)
        {
            ExportToExcel_button.BackgroundImage = Resources.Excel_L;
        }

        private void ExportToExcel_button_MouseLeave(object sender, EventArgs e)
        {
            ExportToExcel_button.BackgroundImage = Resources.Excel_D;
        }

        private int GetPerson_ID(int MP_ID)
        {
            //check connection//
            Program.buildConnection();
            var query = "SELECT `Person_ID` FROM `person_microproject` WHERE `MicroProject_ID` = " + MP_ID + "";
            var sc = new MySqlCommand(query, Program.MyConn);
            var id = int.Parse(sc.ExecuteScalar().ToString());
            Program.MyConn.Close();
            return id;
        }

        private void MP_dataGridView_Sorted(object sender, EventArgs e)
        {
            try
            {
                count_by_projects();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region columns visibility

        private void save_visible_columns()
        {
            arr = new int[MP_dataGridView.ColumnCount];
            for (var j = 0; j < MP_dataGridView.ColumnCount; j++)
                if (MP_dataGridView.Columns[j].Visible)
                    arr[j] = 1;
                else arr[j] = 0;
            var value = string.Join(",", arr.Select(i => i.ToString()).ToArray());
            Settings.Default.Task_Visible_arr = value;
            Settings.Default.Save();
        }

        private void restore_visible_columns()
        {
            try
            {
                if (Settings.Default.Task_Visible_arr != "")
                {
                    arr = Settings.Default.Task_Visible_arr.Split(',').Select(s => int.Parse(s)).ToArray();
                    for (var i = 0; i < MP_dataGridView.ColumnCount; i++)
                        if (arr[i] == 1)
                            MP_dataGridView.Columns[i].Visible = true;
                        else MP_dataGridView.Columns[i].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //REFRESH Button Click//
                bind(null);
            }
        }

        private void ProjectsTasks_FormClosing(object sender, FormClosingEventArgs e)
        {
            save_visible_columns();
        }

        private void ShowHide_button_MouseClick(object sender, MouseEventArgs e)
        {
            cs.mDataGridView_MouseClick(sender, e);
        }

        #endregion

        #region search and filter

        private void MP_idTxtBox_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in MP_dataGridView.Rows)
            {
                var found = 0;
                for (var i = 0; i < MP_dataGridView.ColumnCount; i++)
                    if (ExactSearch_checkBox.Checked)
                    {
                        if (MP_dataGridView.Columns[i].Visible)
                            if (r.Cells[i].Value.ToString().Equals(Search_TxtBox.Text))
                            {
                                MP_dataGridView.Rows[r.Index].Visible = true;
                                found = 1;
                            }
                    }
                    else
                    {
                        if (r.Cells[i].Value.ToString().Contains(Search_TxtBox.Text))
                        {
                            MP_dataGridView.Rows[r.Index].Visible = true;
                            found = 1;
                        }
                    }

                if (found == 0)
                {
                    MP_dataGridView.CurrentCell = null;
                    MP_dataGridView.Rows[r.Index].Visible = false;
                }
            } 
        }

        private void ExactSearch_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in MP_dataGridView.Rows)
            {
                var found = 0;
                for (var i = 0; i < MP_dataGridView.ColumnCount; i++)
                    if (ExactSearch_checkBox.Checked)
                    {
                        if (MP_dataGridView.Columns[i].Visible)
                            if (r.Cells[i].Value.ToString().Equals(Search_TxtBox.Text))
                            {
                                MP_dataGridView.Rows[r.Index].Visible = true;
                                found = 1;
                            }
                    }
                    else
                    {
                        if (r.Cells[i].Value.ToString().Contains(Search_TxtBox.Text))
                        {
                            MP_dataGridView.Rows[r.Index].Visible = true;
                            found = 1;
                        }
                    }

                if (found == 0)
                {
                    MP_dataGridView.CurrentCell = null;
                    MP_dataGridView.Rows[r.Index].Visible = false;
                }
            } 
        }

        private void State_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var s = new StringBuilder();
                var _items = new List<KeyValue_List>();
                var f = 0;

                foreach (Control c in Filters_tableLayoutPanel.Controls)
                    if (c.GetType() == typeof(ComboBox))
                    {
                        var CBox = c as ComboBox;
                        if (CBox.SelectedIndex != -1)
                        {
                            f++;
                            var col_index = CBox.Name.Substring(1, 2);
                            var col_name = "";
                            if (col_index == "04") col_name = "حالة المشروع";
                            else if (col_index == "05") col_name = "الجهة الممولة";
                            else if (col_index == "06") col_name = "نوع المشروع";
                            else if (col_index == "07") col_name = "القصة انكليزي";
                            else if (col_index == "08") col_name = "تدقيق القصة";
                            else if (col_index == "09") col_name = "القصة فرنسي";
                            else if (col_index == "10") col_name = "صورة شخصية";
                            else if (col_index == "11") col_name = "توقيع العقود";
                            else if (col_index == "12") col_name = "طلب المشتريات";
                            else if (col_index == "13") col_name = "تصوير المشتريات";
                            else if (col_index == "14") col_name = "مشتريات";
                            else if (col_index == "15") col_name = "تسليم كامل المبلغ و تحديد الأقساط";
                            else if (col_index == "17") col_name = "تصوير بعد افتتاح المشروع";
                            else if (col_index == "18") col_name = "Map Location";
                            else if (col_index == "19") col_name = "Web";  

                            else if (col_index == "20") col_name = "Opening Visit";
                            else if (col_index == "21") col_name = "M1";
                            else if (col_index == "22") col_name = "M2";
                            else if (col_index == "23") col_name = "M3";
                            else if (col_index == "24") col_name = "M4";
                            else if (col_index == "25") col_name = "M5";
                            else if (col_index == "26") col_name = "Closing Visit";

                            else if (col_index == "27") col_name = "Video Captured";
                            else if (col_index == "28") col_name = "Video Edited";
                            else if (col_index == "29") col_name = "Video Uploaded"; 

                            var filter = "";

                            // combo box with text
                            if (col_index == "04" || col_index == "05" || col_index == "06")
                            {
                                if (CBox.Text.ToString() != "") filter = CBox.Text.ToString();
                                else filter = "-1";
                            }
                            else // combo box with checks
                            {
                                // x = U+E10A  // check  = U+E10B
                                if (CBox.SelectedItem.ToString() == '\uE10b'.ToString()) filter = "1";
                                else if (CBox.SelectedItem.ToString() == '\uE10a'.ToString()) filter = "0";
                                else filter = "-1";
                            }

                            if (filter != "-1")
                            {
                                ///// store this column and filter value in a list //////
                                _items.Add(new KeyValue_List {ColumnName = col_name, FilterValue = filter});
                                //////////////////////////////////////////////////////////
                                if (f == 1)
                                {
                                    s.Append("[");
                                    s.Append(col_name);
                                    s.Append("]=");
                                    s.Append(filter);
                                }
                                else
                                {
                                    s.Append(" AND ");
                                    s.Append("[");
                                    s.Append(col_name);
                                    s.Append("]=");
                                    s.Append(filter);
                                }
                            }
                            else
                            {
                                // remove this column from filtered list !
                                foreach (var element in _items)
                                    if (element.ColumnName == col_name)
                                        _items.Remove(element);
                            }
                        }
                    }

                var currencyManager1 = (CurrencyManager) BindingContext[MP_dataGridView.DataSource];
                currencyManager1.SuspendBinding();

                // Make all rows visible
                foreach (DataGridViewRow r in MP_dataGridView.Rows) r.Visible = true;

                var hiden_col_num = 0;
                foreach (var element in _items) //for each column is filtered columns list//
                    for (var i = 0; i < MP_dataGridView.ColumnCount; i++) // search in grid columns //
                        if (element.ColumnName == MP_dataGridView.Columns[i].HeaderText
                        ) // if we found the filtered column //
                            foreach (DataGridViewRow r in MP_dataGridView.Rows) //search in cells of this column
                                if (element.FilterValue == "0")
                                {
                                    if (!r.Cells[element.ColumnName].Value.Equals(DBNull.Value)) // not null
                                    {
                                        MP_dataGridView.Rows[r.Index].Visible = false;
                                        hiden_col_num++;
                                    }
                                }
                                else if (element.FilterValue == "1")
                                {
                                    if (r.Cells[element.ColumnName].Value.Equals(DBNull.Value)) // null
                                    {
                                        MP_dataGridView.Rows[r.Index].Visible = false;
                                        hiden_col_num++;
                                    }
                                }
                                else //text
                                {
                                    if (!r.Cells[element.ColumnName].Value.Equals(element.FilterValue)
                                    ) // not like filter text
                                    {
                                        MP_dataGridView.Rows[r.Index].Visible = false;
                                        hiden_col_num++;
                                    }
                                }

                Counter_textBox.Text = MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();

                currencyManager1.ResumeBinding();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region datagridview events

        private void MP_dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void MP_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ///////////// Check User's Permission ////////////
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 ||
                    e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
                    throw new Exception("Can't edit this value from this window...");
                if (e.ColumnIndex <= -1 || e.RowIndex <= -1)
                    return;

                var r = e.RowIndex;
                var c = e.ColumnIndex;
                //check connection//
                Program.buildConnection();  
                string query = "SELECT `User_ID` FROM `task_user` WHERE `Task_ID` = (SELECT `T_ID` FROM `task` where T_Name like '" + MP_dataGridView.Columns[c].HeaderText + "' limit 1) ";
                var da = new MySqlDataAdapter(query, Program.MyConn);
                var dt = new DataTable();
                var userHasPermission = false;
                da.Fill(dt); 
                Program.MyConn.Close();

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    User_ID = dt.Rows[i].Field<int>(0);
                    if (User_ID == Settings.Default.userID)
                        userHasPermission = true;
                }

                if (!userHasPermission)
                    throw new Exception("You don't have permission to edit this cell...");
                ///////////////////////////////////////////////////////////////////////

                bool isChecked;
                if (MP_dataGridView.Rows[r].Cells[c].Value.Equals(DBNull.Value))
                {
                    isChecked = true;
                    cell = new DataGridViewImageCell();
                    PicArr = (byte[]) converter.ConvertTo(img, typeof(byte[]));
                    cell.Value = PicArr;
                    MP_dataGridView.Rows[r].Cells[c] = cell;
                    MP_dataGridView.Rows[r].Height = 36;
                }
                else
                {
                    isChecked = false;
                    cell = new DataGridViewImageCell();
                    cell.Value = DBNull.Value;
                    MP_dataGridView.Rows[r].Cells[c] = cell;
                    MP_dataGridView.Rows[r].Height = 36;
                }

                MP_dataGridView[c, r].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                MicroProject_ID = Convert.ToInt32(MP_dataGridView.Rows[r].Cells["رقم المشروع"].Value);

                var selectedtaskName = MP_dataGridView.Columns[c].HeaderText;
                TasksOfProjects.Update_Task_MicroProject(MicroProject_ID, selectedtaskName, isChecked, DateTime.Now); 
                l.Insert_Log("Update the task: " + selectedtaskName + " of project: " + MicroProject_ID + " ",
                    " Task_Project ", Settings.Default.username, DateTime.Now);

                if (selectedtaskName == "مشتريات")
                {
                    if (isChecked)
                    { 
                        // send notification to (communication) //
                        var s = new Select();
                        var com_db = s.select_Communication();
                        if (com_db != null)
                            for (var i = 0; i < com_db.Rows.Count; i++)
                            {
                                Communication_IDs.Add(com_db.Rows[i].Field<int>(0));
                                u.Insert_UserNotification(DateTime.Now, "انتهاء المشتريات",
                                    MP_dataGridView.Rows[r].Cells["المستفيد"].Value.ToString(), MicroProject_ID,
                                    Communication_IDs[i], Settings.Default.userID, -3);
                            }
                    }
                    else
                    {
                        u.Update_NotificationWithPaymentID(-3,"","","1");
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MP_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
                {
                    SelectedDataRow = ((DataRowView) MP_dataGridView.CurrentRow.DataBoundItem).Row;
                    if (SelectedDataRow != null)
                    {
                        if (SelectedDataRow["رقم المشروع"] == null || SelectedDataRow["رقم المشروع"] == DBNull.Value)
                            MicroProject_ID = -1;
                        else MicroProject_ID = int.Parse(SelectedDataRow["رقم المشروع"].ToString());
                        //Person_ID = GetPerson_ID(MicroProject_ID);
                        
                        if (SelectedDataRow["رقم المستفيد"] == null ||
                            SelectedDataRow["رقم المستفيد"] == DBNull.Value)
                            Person_ID = -1;
                        else Person_ID = int.Parse(SelectedDataRow["رقم المستفيد"].ToString()); 
                    }

                    Form application_Form = new Application_Form(Person_ID, MicroProject_ID, main_form);
                    main_form.showNewTab(application_Form, "Application",0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MP_dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 ||
                    e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
                    throw new Exception("Can't edit this value from this window...");
                if (e.ColumnIndex <= -1 || e.RowIndex <= -1)
                    return;

                var r = e.RowIndex;
                var c = e.ColumnIndex;

                Program.buildConnection();  
                var query = "SELECT `User_ID` FROM `task_user` WHERE `Task_ID` = (SELECT `T_ID` FROM `task` where T_Name like '" + MP_dataGridView.Columns[c].HeaderText +"' limit 1)";
                var da = new MySqlDataAdapter(query, Program.MyConn);
                var dt = new DataTable();
                var userHasPermission = false;
                da.Fill(dt);
                Program.MyConn.Close();

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    User_ID = dt.Rows[i].Field<int>(0);
                    if (User_ID == Settings.Default.userID)
                        userHasPermission = true;
                }

                if (!userHasPermission)
                    throw new Exception("You don't have permission to edit this cell...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MP_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex <= -1 || e.RowIndex <= -1)
                    return;

                var r = e.RowIndex;
                var c = e.ColumnIndex;

                if (!MP_dataGridView.Rows[r].Cells[c].Value.Equals(DBNull.Value))
                {
                    var isChecked = Convert.ToBoolean(MP_dataGridView.Rows[r].Cells[c].Value);
                    MySS.dt.Rows[r][c] = MP_dataGridView.Rows[r].Cells[c].Value;

                    MicroProject_ID = Convert.ToInt32(MP_dataGridView.Rows[r].Cells["رقم المشروع"].Value);

                    var selectedtaskName = MP_dataGridView.Columns[c].HeaderText;
                    TasksOfProjects.Update_Task_MicroProject(MicroProject_ID, selectedtaskName, isChecked, DateTime.Now);

                    l.Insert_Log("update the task: " + selectedtaskName + " of project: " + MicroProject_ID + " ",
                        " Task_Project ", Settings.Default.username, DateTime.Now);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MP_dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var sorted_dt = MySS.dt;
                var col_name = MP_dataGridView.Columns[e.ColumnIndex].HeaderText;
                if (direction == "asc")
                    direction = "desc";
                else direction = "asc";

                sorted_dt.DefaultView.Sort = col_name + " " + direction;
                sorted_dt = sorted_dt.DefaultView.ToTable();

                bind(sorted_dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MP_dataGridView_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Visible)
            {
                if (e.Column.Visible == false)
                    Filters_tableLayoutPanel.ColumnStyles[e.Column.Index + 1].Width = 0;
                //Filters_tableLayoutPanel.Controls[e.Column.Index + 1].Visible = false;
                else
                    Filters_tableLayoutPanel.ColumnStyles[e.Column.Index + 1].Width = e.Column.Width + 1;
                //Filters_tableLayoutPanel.Controls[e.Column.Index + 1].Visible = true;
            }

            Filters_tableLayoutPanel.Width = MP_dataGridView.Width + 10;
            //Grid_panel.HorizontalScroll.Value = HorizontalScroll.Maximum;
        }

        #endregion

        #region right click menu

        private void refreshPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_button_Click(sender, e);
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Search_Form = new Search_Form(main_form);
                main_form.showNewTab(Search_Form, "Search",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Statistics_Form = new Statistics_Form(main_form);
                main_form.showNewTab(Statistics_Form, "Statistics",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void taskBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var projectsTasks = new TaskBoard_Form(main_form);
                main_form.showNewTab(projectsTasks, "Task Board",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void Filters_tableLayoutPanel_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void clearAllFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MP_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Search_TxtBox.Text = "";
            //Tasks_comboBox.Text = "";
            //Tasks_comboBox.SelectedIndex = 0;
            foreach (Control c in Filters_tableLayoutPanel.Controls)
                if (c.GetType() == typeof(ComboBox))
                {
                    var CBox = c as ComboBox;
                    CBox.SelectedIndex = 0;
                }

            MP_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
        }

        private void showAllColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Make all columns visible
            foreach (DataGridViewColumn c in MP_dataGridView.Columns) c.Visible = true;
        }

        #endregion

    }
}