using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Dynamic;

namespace MyWorkApplication
{
    public partial class TaskBoard : Form
    {
        public TaskBoard()
        {
            InitializeComponent();
        }
        MySqlComponents MySS;
        Log l;
        byte[] PicArr = null;
        Image img = Properties.Resources.OK24;
        ImageConverter converter = new ImageConverter();
        DataGridViewImageCell cell;
        DataGridViewColumnSelector cs;
        int[] arr;

        string from = @" from `task_microproject` TMP LEFT outer join `person_microproject` PMP on TMP.MicroProject_ID = PMP.MicroProject_ID
 LEFT outer join `microproject` MP on PMP.MicroProject_ID = MP.MP_ID
 LEFT outer join `person` P1 on P1.P_ID = PMP.Person_ID
 LEFT outer join `loan` L on L.MicroProject_ID = MP.MP_ID ";

        private void TaskBoard_Load(object sender, EventArgs e)
        {
            try
            {
                //NewTheme = new NewTheme();
                //if (Properties.Settings.Default.theme == "Light")
                //{
                //    NewTheme.Tasks_ToLight(this);
                //}
                //else
                //{
                //    NewTheme.Tasks_ToNight(this);
                //}
                //main_form.Project_label.Visible = main_form.TabName_label.Visible = true;
                //main_form.ProjectNumber_label.Visible = main_form.MP_ID_label.Visible = false;
                //main_form.TabName_label.Text = "Task Board";

                MySS = new MySqlComponents();
                l = new Log();

                //cs = new DataGridViewColumnSelector(MP_dataGridView);
                //cs.MaxHeight = 100;
                //cs.Width = 110;

                //bind_Tasks_into_ComboBox();
                //timer1.Start();

                //DataGridViewImageColumn imageColumn = MP_dataGridView.Columns["EnglishStory"] as DataGridViewImageColumn;
                //imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                //imageColumn.DefaultCellStyle.Padding = new Padding(5);
                //imageColumn.DefaultCellStyle.NullValue = DBNull.Value; //new System.Drawing.Bitmap(1, 1);
                //imageColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                bind(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bind(int c_index)
        {
            string orderBy_column = "";
            string orderBy = "";
            
            //check connection//
            Program.buildConnection();
            string query = @"SELECT DISTINCT PMP.MicroProject_ID as 'رقم المشروع'
,CONCAT(P1.P_FirstName, ' ',P1.P_FatherName, ' ', P1.P_LastName) as 'المستفيد'
,MP.MP_Name as 'اسم المشروع'
,(CASE MP.MP_State WHEN 0 THEN N'مرفوض' WHEN 1 THEN N'مقبول' WHEN 2 THEN N'مؤجل' WHEN 4 THEN N'ممول' WHEN 5 THEN N'منتهي' ELSE N'بالانتظار' End )AS 'حالة المشروع'
,(CASE MP.MP_Donor WHEN 'Caritas Poland' THEN N'CP' WHEN 'Salt' THEN N'S' WHEN 'L\'oeuvre d\'orient' THEN N'LD' WHEN 'Kerk in Actie' THEN N'KIA' ELSE N'N/A' End )AS 'الجهة الممولة'
,(CASE MP.MP_Type WHEN 0 THEN N'Loan' ELSE N'Grant' End )AS 'نوع المشروع'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 3 limit 1)AS 'القصة انكليزي'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 4 limit 1)AS 'تدقيق القصة'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 5 limit 1)AS 'القصة فرنسي'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 6 limit 1)AS 'طلب تصوير صورة شخصية'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 7 limit 1)AS 'صورة شخصية'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 10 limit 1)AS 'طلب توقيع العقود'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 11 limit 1)AS 'توقيع العقود'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 12 limit 1)AS 'web waiting'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 13 limit 1)AS 'طلب المشتريات'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 14 limit 1)AS 'طلب تصوير المشتريات'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 15 limit 1)AS 'تصوير المشتريات'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 16 limit 1)AS 'مشتريات'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 17 limit 1)AS 'تسليم كامل المبلغ و تحديد الأقساط'
,L.Loan_DateTaken as 'تاريخ بداية المشروع'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 18 limit 1)AS 'طلب تصوير افتتاح المشروع'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 19 limit 1)AS 'تصوير بعد افتتاح المشروع'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 20 limit 1)AS 'web funded'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 21 limit 1)AS 'Map Location'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 22 limit 1)AS 'M&E 1'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 23 limit 1)AS 'M&E 2'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 24 limit 1)AS 'M&E 3'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 25 limit 1)AS 'Story'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 26 limit 1)AS 'طلب فيديو'
,( SELECT State From `task_microproject` where MicroProject_ID = PMP.MicroProject_ID and Task_ID = 27 limit 1)AS 'فيديو'
" + from;
            string condition = " where MP.MP_State =1 or MP.MP_State = 4 or MP.MP_State = 5 ";
            query += condition;
            query += orderBy;
            MySS.da = new MySqlDataAdapter(query, Program.MyConn);
            MySS.dt = new DataTable();
            MySS.dt.Clear();    //dataset.clear
            MySS.da.Fill(MySS.dt);

            MP_dataGridView.DataSource = null;
            MP_dataGridView.Columns.Clear();
            MP_dataGridView.Rows.Clear();

            MP_dataGridView.ColumnHeadersVisible = false;
            MP_dataGridView.DataSource = MySS.dt;

            //Font f = new Font("Segue UI Symbol", 12f);
            //MP_dataGridView.Columns["EnglishStory"].DefaultCellStyle.Font = f;
            //MP_dataGridView.ColumnHeadersDefaultCellStyle.Font = f;
            //// fill column by column //
            /////////////////////////////
            //for (int i = 0; i < MySS.dt.Rows.Count; i++)
            //{
            //    MP_dataGridView.Rows.Add();

            //    MP_dataGridView.Rows[i].Cells["MicroProject_ID"].Value = MySS.dt.Rows[i].Field<int>("رقم المشروع").ToString();

            //    MP_dataGridView.Rows[i].Cells["BeneficiaryName"].Value = MySS.dt.Rows[i].Field<string>("المستفيد").ToString();
            //    MP_dataGridView.Rows[i].Cells["ProjectName"].Value = MySS.dt.Rows[i].Field<string>("اسم المشروع").ToString();
            //    MP_dataGridView.Rows[i].Cells["State"].Value = MySS.dt.Rows[i].Field<string>(3).ToString();
            //    MP_dataGridView.Rows[i].Cells["Donor"].Value = MySS.dt.Rows[i].Field<string>(4).ToString();
            //    MP_dataGridView.Rows[i].Cells["Type"].Value = MySS.dt.Rows[i].Field<string>(5).ToString();

            //    int state = Convert.ToInt32( MySS.dt.Rows[i].Field<Int64>("القصة انكليزي"));
            //    //MP_dataGridView.Rows[i].Cells["EnglishStory"].Value = state;
            //    //DataGridViewImageCell ch2 = MP_dataGridView.Rows[i].Cells["EnglishStory"] as DataGridViewImageCell;


            //    if (state == 0)
            //    {
            //        MP_dataGridView.Rows[i].Cells["EnglishStory"].Value = "";
            //        MP_dataGridView.Columns["EnglishStory"].DefaultCellStyle.Font = f;
            //        MP_dataGridView.ColumnHeadersDefaultCellStyle.Font = f;
            //        //cell = new DataGridViewImageCell();
            //        //cell.Value = DBNull.Value;
            //        //MP_dataGridView.Rows[i].Cells["EnglishStory"] = cell;
            //        //MP_dataGridView.Rows[i].Height = 36;
            //    }
            //    else
            //    {
            //        MP_dataGridView.Rows[i].Cells["EnglishStory"].Value = "";
            //        MP_dataGridView.Columns["EnglishStory"].DefaultCellStyle.Font = f;
            //        MP_dataGridView.ColumnHeadersDefaultCellStyle.Font = f;
            //        //cell = new DataGridViewImageCell();
            //        //PicArr = (byte[])converter.ConvertTo(img, typeof(byte[]));
            //        //cell.Value = PicArr;
            //        //MP_dataGridView.Rows[i].Cells["EnglishStory"] = cell;
            //        //MP_dataGridView.Rows[i].Height = 36;
            //    }
            //}

            MP_dataGridView.Columns["تاريخ بداية المشروع"].DefaultCellStyle.Format = "dd/MM/yyyy";
            MP_dataGridView.ColumnHeadersVisible = true;

            //ConvertCellsToCheckBox_Color();


            /////////////////////////////////
            StringBuilder s = new StringBuilder();
            s.Append("select MP_ID from `microproject` where MP_ID in ");
            s.Append("(");
            s.Append("select PMP.MicroProject_ID " + from + condition);
            s.Append(")");
            /////////////////////////////////
            //count rows
            string sel = "select count(*) from (" + s + ") as count";
            MySS.sc = new MySqlCommand(sel, Program.MyConn);
            // Counter_textBox.Text = MySS.sc.ExecuteScalar().ToString();
            Program.MyConn.Close();


        }
        private void ConvertCellsToCheckBox_Color()
        {
            for (int j = 0; j < MP_dataGridView.ColumnCount; j++)
            {

                if (j == 19 || j == 0 || j == 1 || j == 2)
                {
                    if (j == 19 || j == 0)
                    {
                        for (int i = 0; i < MP_dataGridView.RowCount; i++)
                        {
                            MP_dataGridView[j, i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    continue;
                }
                else if (j == 3 || j == 4 || j == 5)
                {
                    for (int i = 0; i < MP_dataGridView.RowCount; i++)
                    {
                        MP_dataGridView[j, i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        if (MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value.ToString() == "منتهي")
                            MP_dataGridView.Rows[i].DefaultCellStyle.BackColor = MP_dataGridView.Rows[i].Cells[j].Style.BackColor = Color.DimGray;
                    }
                }
                else
                {
                    DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                    imageColumn.HeaderText = imageColumn.Name = MP_dataGridView.Columns[j].HeaderText;
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    imageColumn.DefaultCellStyle.Padding = new Padding(5);
                    imageColumn.DefaultCellStyle.NullValue = new System.Drawing.Bitmap(1, 1);
                    imageColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    MP_dataGridView.Columns.Insert(j + 1, imageColumn);
                    for (int k = 0; k < MP_dataGridView.RowCount; k++)
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
                            PicArr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                            cell.Value = PicArr;
                            MP_dataGridView.Rows[k].Cells[j + 1] = cell;
                            MP_dataGridView.Rows[k].Height = 36;
                        }
                        MP_dataGridView[j + 1, k].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        if (MP_dataGridView.Rows[k].Cells["حالة المشروع"].Value.ToString() == "منتهي")
                            MP_dataGridView.Rows[k].DefaultCellStyle.BackColor = MP_dataGridView.Rows[k].Cells[j].Style.BackColor = Color.DimGray;
                    }
                    MP_dataGridView.Columns.RemoveAt(j);
                }
            }

            MP_dataGridView.Columns[1].Width = MP_dataGridView.Columns[2].Width = 140;
            MP_dataGridView.Columns[0].Width = MP_dataGridView.Columns[3].Width = MP_dataGridView.Columns[4].Width = MP_dataGridView.Columns[5].Width = 60;

            MP_dataGridView.Columns[6].Width = MP_dataGridView.Columns[7].Width = MP_dataGridView.Columns[8].Width
                = MP_dataGridView.Columns[9].Width = MP_dataGridView.Columns[10].Width = MP_dataGridView.Columns[11].Width
                 = MP_dataGridView.Columns[14].Width
                = MP_dataGridView.Columns[15].Width = MP_dataGridView.Columns[16].Width = MP_dataGridView.Columns[17].Width
                = MP_dataGridView.Columns[18].Width = MP_dataGridView.Columns[20].Width
                //= MP_dataGridView.Columns[23].Width= MP_dataGridView.Columns[13].WidthMP_dataGridView.Columns[19].Width =
                = MP_dataGridView.Columns[24].Width = MP_dataGridView.Columns[25].Width = MP_dataGridView.Columns[26].Width
                = MP_dataGridView.Columns[27].Width = MP_dataGridView.Columns[28].Width = MP_dataGridView.Columns[29].Width = 75;
            MP_dataGridView.Columns[19].Width
                 = MP_dataGridView.Columns[13].Width = MP_dataGridView.Columns[22].Width = MP_dataGridView.Columns[23].Width = 90;
        }


        public class DataPointGridViewModel
        {
            public int DataPointId { get; set; }
            public string Description { get; set; }
            public bool InAlarm { get; set; }
            public DateTime LastUpdate { get; set; }
            public double ScalingMultiplier { get; set; }
            public decimal Price { get; set; }
        }
        List<DataPointGridViewModel> m_dataGridBindingList = null;
        List<DataPointGridViewModel> m_filteredList = null;


        private void MP_dataGridView_FilterStringChanged(object sender, EventArgs e)
        {
            try
            {
                var myDataGrid = sender as ADGV.AdvancedDataGridView;
                (myDataGrid.DataSource as DataTable).DefaultView.RowFilter = myDataGrid.FilterString;
                
                //if (string.IsNullOrEmpty(MP_dataGridView.FilterString) == true)
                //{
                //    m_filteredList = m_dataGridBindingList;
                //    MP_dataGridView.DataSource = m_dataGridBindingList;
                //}
                //else
                //{
                //    var listfilter = FilterStringconverter(MP_dataGridView.FilterString);

                //    m_filteredList = m_filteredList.Where(listfilter).ToList();

                //    MP_dataGridView.DataSource = m_filteredList;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MP_dataGridView_SortStringChanged(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(MP_dataGridView.SortString) == true)
                //    return;

                //var sortStr = MP_dataGridView.SortString.Replace("[", "").Replace("]", "");

                //if (string.IsNullOrEmpty(MP_dataGridView.FilterString) == true)
                //{
                //    // the grid is not filtered!
                //    m_dataGridBindingList = m_dataGridBindingList.OrderBy(sortStr).ToList();
                //    MP_dataGridView.DataSource = m_dataGridBindingList;
                //}
                //else
                //{
                //    // the grid is filtered!
                //    m_filteredList = m_filteredList.OrderBy(sortStr).ToList();
                //    MP_dataGridView.DataSource = m_filteredList;
                //}
                var myDataGrid = sender as ADGV.AdvancedDataGridView;
                (myDataGrid.DataSource as DataTable).DefaultView.Sort = myDataGrid.SortString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string FilterStringconverter(string filter)
        {
            string newColFilter = "";

            // get rid of all the parenthesis 
            filter = filter.Replace("(", "").Replace(")", "");

            // now split the string on the 'and' (each grid column)
            var colFilterList = filter.Split(new string[] { "AND" }, StringSplitOptions.None);

            string andOperator = "";

            foreach (var colFilter in colFilterList)
            {
                newColFilter += andOperator;

                // split string on the 'in'
                var temp1 = colFilter.Trim().Split(new string[] { "IN" }, StringSplitOptions.None);

                // get string between square brackets
                var colName = temp1[0].Split('[', ']')[1].Trim();

                // prepare beginning of linq statement
                newColFilter += string.Format("({0} != null && (", colName);

                string orOperator = "";

                var filterValsList = temp1[1].Split(',');

                foreach (var filterVal in filterValsList)
                {
                    // remove any single quotes before testing if filter is a num or not
                    var cleanFilterVal = filterVal.Replace("'", "").Trim();

                    double tempNum = 0;
                    if (Double.TryParse(cleanFilterVal, out tempNum))
                        newColFilter += string.Format("{0} {1} = {2}", orOperator, colName, cleanFilterVal.Trim());
                    else
                        newColFilter += string.Format("{0} {1}.Contains('{2}')", orOperator, colName, cleanFilterVal.Trim());

                    orOperator = " OR ";
                }

                newColFilter += "))";

                andOperator = " AND ";
            }

            // replace all single quotes with double quotes
            return newColFilter.Replace("'", "\"");
        }
        private void Search_TxtBox_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in MP_dataGridView.Rows)
            {
                int found = 0;
                for (int i = 0; i < MP_dataGridView.ColumnCount; i++)
                {
                    if (ExactSearch_checkBox.Checked)
                    {
                        if (MP_dataGridView.Columns[i].Visible)
                        {
                            if ((r.Cells[i].Value).ToString().Equals(Search_TxtBox.Text))
                            {
                                MP_dataGridView.Rows[r.Index].Visible = true;
                                found = 1;
                            }
                        }
                    }
                    else
                    {
                        if ((r.Cells[i].Value).ToString().Contains(Search_TxtBox.Text))
                        {
                            MP_dataGridView.Rows[r.Index].Visible = true;
                            found = 1;
                        }
                    }
                }
                if (found == 0)
                {
                    MP_dataGridView.CurrentCell = null;
                    MP_dataGridView.Rows[r.Index].Visible = false;
                }
            }
            Counter_textBox.Text = MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void ExactSearch_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in MP_dataGridView.Rows)
            {
                int found = 0;
                for (int i = 0; i < MP_dataGridView.ColumnCount; i++)
                {
                    if (ExactSearch_checkBox.Checked)
                    {
                        if (MP_dataGridView.Columns[i].Visible)
                        {
                            if ((r.Cells[i].Value).ToString().Equals(Search_TxtBox.Text))
                            {
                                MP_dataGridView.Rows[r.Index].Visible = true;
                                found = 1;
                            }
                        }
                    }
                    else
                    {
                        if ((r.Cells[i].Value).ToString().Contains(Search_TxtBox.Text))
                        {
                            MP_dataGridView.Rows[r.Index].Visible = true;
                            found = 1;
                        }
                    }
                }
                if (found == 0)
                {
                    MP_dataGridView.CurrentCell = null;
                    MP_dataGridView.Rows[r.Index].Visible = false;
                }
            }
            Counter_textBox.Text = MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void MP_dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
