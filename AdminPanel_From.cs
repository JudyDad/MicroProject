using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;
using MyWorkApplication.Visit_Forms;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;

namespace MyWorkApplication
{
    public partial class AdminPanel_From : Form
    {
        private int[] arr;

        private string[] coloursArr =
            {"Red", "Green", "Black", "White", "Orange", "Yellow", "Blue", "Maroon", "Pink", "Purple"};

        private DataGridViewColumnSelector cs;
        private ListSortDirection direction;

        private string direction_1;
        private bool Hided;
        private Log l; 
        private readonly MainForm main_form;
        private int MicroProject_ID;
        private DataGridViewColumn oldColumn, newColumn;
        private int Person_ID;
        private int PW;
        private DataRow SelectedDataRow; 
        private TasksOfProjects TasksOfProjects;
        private string State = "";
        private Street st;
        private SubCategory sub;
        private DataSet ds;
        private int tick;
        private string username;
        private bool user_mode;
        private int total_bind_count;

        public AdminPanel_From(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        public AdminPanel_From(string username, MainForm main_form)
        {
            InitializeComponent();
            this.username = username;
            this.main_form = main_form;
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            try
            { 
                l = new Log();
                st = new Street(); sub = new SubCategory(); 
                user_mode = false;

                //for (var i = 0; i < MP_dataGridView.Columns.Count - 1; i++)
                //    MP_dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;

                PW = Search_panel.Height;
                Hided = true;
                Search_panel.Height = 0;
                MP_dataGridView.DoubleBuffered(true);

                Bind_All_ComboBoxes();
                timer1.Start();
                //Category_bind();
                //SubCategory_bind(""); //to select the subCategory when Fill MicroProject//
                //Street_bind();
                //StateReason_bind();
                //Donor_bind();
                //DonorGroup_bind("");

                // Use of the DataGridViewColumnSelector
                cs = new DataGridViewColumnSelector(MP_dataGridView);
                cs.MaxHeight = 100;
                cs.Width = 110;

                Check_Theme();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region bind queries
        private void bind(string value,string FundType, string Type, string SubType
            , string State, string Donor,string DonorGroup, string Category,string SubCategory
            , string Street, string ApplyDate, string FundDate, string reason
            , string Parish, string Age, string Sex, string MaritalStatus, string Partners
            , string Needed_from, string Needed_To, string Financed_from, string Financed_To)
        { 
            try
            { 
                var from =
                    @" from `microproject` MP 
 LEFT OUTER JOIN `person_microproject` PMP on PMP.MicroProject_ID = MP.MP_ID 
 LEFT OUTER JOIN `person` P1 on P1.P_ID = PMP.Person_ID
 LEFT OUTER JOIN `loan` l on l.MicroProject_ID = MP.MP_ID
 LEFT OUTER JOIN `category` C on MP.MP_Category_ID = C.C_ID
 LEFT OUTER JOIN `subcategory` Sub on MP.SubCategory_ID = Sub.ID
 LEFT OUTER JOIN `state` on state.ID = MP.MP_State
 LEFT OUTER JOIN `statereason` on statereason.ID = MP.MP_StateReason_ID
 LEFT OUTER JOIN `street` on street.ID = P1.Street_ID
 LEFT OUTER JOIN `donor` on donor.ID = MP.MP_Donor 
 LEFT OUTER JOIN `donorgroup` on donorgroup.ID = MP.DonorGroup_ID 
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID ";

                var query = @"SELECT MP.MP_ID as 'رقم المشروع'
,CONCAT(P1.P_FirstName,' ', P1.P_LastName) as 'المستفيد'
,P1.P_FatherName as 'اسم الأب'
,P1.P_MotherName as 'اسم الأم'
,( SELECT CONCAT(p.P_FirstName, ' ',p.P_FatherName, ' ',p.P_LastName) From `person` p join `person_family` pf on p.P_ID = pf.Person_ID where pf.Relation like N'زوج/ة' and pf.P_Provider_ID =PMP.Person_ID limit 1) as 'الزوج / الزوجة'
,P1.P_NationalNumber as 'الرقم الوطني'
,P1.P_RegistrationPlace as 'القيد'
,( SELECT f.F_Number From `family` f join `person_family` pf on f.F_ID = pf.Family_ID where pf.Relation like N'مستفيد' and pf.P_Provider_ID =PMP.Person_ID limit 1) as 'رقم دفتر العائلة'
 
,P1.P_Mobile as 'الموبايل'
,MP.MP_Name as 'اسم المشروع'
,MP.MP_DateOfRequest as 'تاريخ التقديم'
,MP.MP_RequestedAmount as 'المبلغ المطلوب'
,l.Loan_Amount as 'مبلغ التمويل'
,P_Parish as 'الطائفة'

,(select Priest_Name from priest where Priest_ID = P1.P_Priest_ID limit 1) as 'الكاهن'
,street.Name as 'منطقة السكن'
,P_HomeAddress as 'العنوان'

,statereason.Name as 'سبب عدم التمويل'
,P1.P_OtherCourses as 'ملاحظات المستفيد'
,MP.MP_StateReason as 'ملاحظات المشروع'
,P1.P_MaristesCourse as 'دورة تدريبية'

,microprojecttype.Name as 'نوع المشروع'
,state.Name_ar as 'حالة المشروع'
,donor.Name as 'الجهة الممولة'
,P1.P_ID as 'رقم المستفيد'"

+ from;
                //////  عند إضافة عمود جديد لإظهاره تأكد من تفيير Column[index] عند الحفظ
                var orderBy = " order by MP.MP_ID " + direction_1;
                var condition = " where 1 ";  
                if (value != "") orderBy = " order by MP.MP_State,PMP.MicroProject_ID " + direction_1;

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
                 
                //State
                if (State != "")
                {  
                    condition += " and state.Name_Ar like N'" + State + "' ";
                }

                //Donor
                if (Donor != "")
                    condition += " and donor.Name like '" + Donor + "'";

                //DonorGroup
                if (DonorGroup != "")
                    condition += " and donorgroup.Name like '" + DonorGroup + "'";


                //Project Category
                if (Category != "")
                    condition += " and C.C_Name like N'" + Category + "'";

                //Project SUB Category
                if (SubCategory != "")
                    condition += " and Sub.Name like N'" + SubCategory + "'";

                //Home Street
                if (Street != "")
                    condition += " and street.Name like N'" + Street + "'";

                //State Reason
                if (reason != "")
                    condition += " and statereason.Name like N'" + reason + "'";

                //Parish
                if (Parish != "")
                {
                    if (Parish == "Christian")
                        condition += " and P1.P_Parish not like  N'Muslim'";
                    else condition += " and P1.P_Parish like  N'" + Parish + "'";
                }

                ////Age 
                if (Age != "")
                {
                    if (Age.Contains("<16")) condition += " and  TIMESTAMPDIFF(YEAR, `P_DOB`, CURDATE()) < 16 ";
                    else if (Age.Contains("16-26"))
                        condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, CURDATE()) BETWEEN 16 and 26 ";
                    else if (Age.Contains("27-35"))
                        condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, CURDATE()) BETWEEN 27 and 35 ";
                    else if (Age.Contains("36-45"))
                        condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, CURDATE()) BETWEEN 36 and 45 ";
                    else if (Age.Contains("46-60"))
                        condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, CURDATE()) BETWEEN 46 and 60 ";
                    else if (Age.Contains(">60")) condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, CURDATE()) > 60 ";
                }

                //Marital Status
                if (MaritalStatus != "")
                {
                    if (MaritalStatus.Contains("متزوج")) condition += " and P1.P_MaritalStatus like N'%متزوج%'";
                    else if (MaritalStatus.Contains("عازب")) condition += " and P1.P_MaritalStatus like N'%عازب%'";
                    else if (MaritalStatus.Contains("مطلق")) condition += " and P1.P_MaritalStatus like N'%مطلق%'";
                    else if (MaritalStatus.Contains("أرمل")) condition += " and P1.P_MaritalStatus like N'%أرمل%'";
                    else if (MaritalStatus.Contains("مخطوب")) condition += " and P1.P_MaritalStatus like N'%مخطوب%'";
                }

                //Sex
                if (Sex != "")
                {
                    if (Sex.Contains("ذكر")) condition += " and P1.P_Sex like N'%ذكر%'";
                    else if (Sex.Contains("أنثى")) condition += " and P1.P_Sex like N'%أنثى%'";
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

                //money Needed
                if (Needed_from != "" || Needed_To != "")
                {
                    double Needed_from_d, Needed_to_d;
                    if (Needed_from == "")
                        Needed_from_d = 0;
                    else Needed_from_d = Convert.ToDouble(Needed_from);

                    if (Needed_To == "")
                        Needed_to_d = 0;
                    else Needed_to_d = Convert.ToDouble(Needed_To);
                    condition += " and (`MP_RequestedAmount` BETWEEN " + Needed_from_d + " and " + Needed_to_d + ") ";
                }

                //money Financed
                if (Financed_from != "" || Financed_To != "")
                {
                    double Financed_from_d, Financed_To_d;
                    if (Financed_from == "")
                        Financed_from_d = 0;
                    else Financed_from_d = Convert.ToDouble(Financed_from);

                    if (Financed_To == "")
                        Financed_To_d = 0;
                    else Financed_To_d = Convert.ToDouble(Financed_To);
                    condition += " and (l.Loan_Amount BETWEEN " + Financed_from_d + " and " + Financed_To_d + ") ";
                }

                query += condition;
                query += orderBy + ";";

                //count rows
                string query2 = "select IFNULL(count(DISTINCT MP_ID),0) " + from + condition + ";";

                string all_query = "";
                all_query += query + query2 ;


                //check connection//
                Program.buildConnection();
                MySqlCommand sc = new MySqlCommand(all_query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataSet main_ds = new DataSet();
                da.Fill(main_ds);
                Program.MyConn.Close();

                MP_dataGridView.ColumnHeadersVisible = false;
                MP_dataGridView.DataSource = main_ds.Tables[0];

                total_bind_count = 0;
                //total_bind_count = int.Parse(sc.ExecuteScalar().ToString());
                total_bind_count = Convert.ToInt32(main_ds.Tables[1].Rows[0][0].ToString());
                Count_label.Text = "All:" + total_bind_count.ToString("#,##0");


                MP_dataGridView.Columns["المبلغ المطلوب"].DefaultCellStyle.Format = "#,##0";
                MP_dataGridView.Columns["مبلغ التمويل"].DefaultCellStyle.Format = "#,##0";
                MP_dataGridView.Columns["تاريخ التقديم"].DefaultCellStyle.Format = "yyyy/MM/dd";
                
                var dgC1 = MP_dataGridView.Columns["رقم المستفيد"];
                dgC1.Visible = false;

                MP_dataGridView.Columns["رقم المشروع"].ReadOnly = 
                    MP_dataGridView.Columns["المستفيد"].ReadOnly =
                    MP_dataGridView.Columns["اسم الأب"].ReadOnly =
                    MP_dataGridView.Columns["اسم الأم"].ReadOnly =
                    MP_dataGridView.Columns["الزوج / الزوجة"].ReadOnly =
                    MP_dataGridView.Columns["الرقم الوطني"].ReadOnly =
                    MP_dataGridView.Columns["القيد"].ReadOnly =
                    MP_dataGridView.Columns["رقم دفتر العائلة"].ReadOnly =
                    MP_dataGridView.Columns["تاريخ التقديم"].ReadOnly =
                    MP_dataGridView.Columns["مبلغ التمويل"].ReadOnly =
                    MP_dataGridView.Columns["الكاهن"].ReadOnly =
                    MP_dataGridView.Columns["منطقة السكن"].ReadOnly =
                    MP_dataGridView.Columns["العنوان"].ReadOnly =
                    MP_dataGridView.Columns["الطائفة"].ReadOnly =
                    MP_dataGridView.Columns["نوع المشروع"].ReadOnly = true;

                ///////////////////////////////////
                //var s = new StringBuilder();
                //s.Append("select MP_ID from microproject where MP_ID in ");
                //s.Append("(");
                //s.Append("select PMP.MicroProject_ID as 'MicroProject_ID'" + from + condition);
                //s.Append(")");
                ///////////////////////////////////

                ////count rows
                //var sel = "select count(*) from (" + s + ") as count";
                //MySS.sc = new MySqlCommand(sel, Program.MyConn);
                 
                //total_bind_count = int.Parse(MySS.sc.ExecuteScalar().ToString());
                //Count_label.Text = "All:" + total_bind_count;

                ConvertCellToComboBoxAndColor();
                restore_visible_columns();

                MP_dataGridView.ColumnHeadersVisible = true;
                MP_dataGridView.ClearSelection();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
            string statereason_query = "select ID,Name from `statereason` ORDER BY Name ASC;";

            string query = categroy_query + sub_category_query + street_query + donor_query + donor_group_query
                + fundType_query + microprojecttype_query + microprojectsubtype_query + statereason_query;
            Program.buildConnection();
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            ds = new DataSet();
            da.Fill(ds);
            Program.MyConn.Close();

            Set_comboBox_datasource(Category_comboBox   , ds.Tables[0]);
            Set_comboBox_datasource(SubCategory_comboBox, ds.Tables[1]);
            Set_comboBox_datasource(Street_comboBox     , ds.Tables[2]);
            Set_comboBox_datasource(Donor_comboBox      , ds.Tables[3]);
            Set_comboBox_datasource(DonorGroup_comboBox , ds.Tables[4]);
            Set_comboBox_datasource(FundType_comboBox   , ds.Tables[5]);
            Set_comboBox_datasource(Type_comboBox       , ds.Tables[6]);
            Set_comboBox_datasource(SubType_comboBox    , ds.Tables[7]);
            Set_comboBox_datasource(StateReason_comboBox, ds.Tables[8]);

            ApplyDate_comboBox.Items.Clear();
            FundDate_comboBox.Items.Clear();
            for (int i = 2018; i <= DateTime.Today.Year; i++)
            {
                ApplyDate_comboBox.Items.Add(i.ToString());
                FundDate_comboBox.Items.Add(i.ToString());
            }

            user_mode = true;
        }

        private void Set_comboBox_datasource(ComboBox cBox,DataTable c_dt)
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

        //private void Category_bind()
        //{
        //    DataTable cat_st = sub.Category_Select();
        //    Category_comboBox.DataSource = null;
        //    Category_comboBox.DisplayMember = "C_Name";
        //    Category_comboBox.ValueMember = "C_ID";
        //    Category_comboBox.DataSource = cat_st;
        //    Category_comboBox.SelectedIndex = -1;
        //}
        //private void SubCategory_bind(string Category_ID)
        //{
        //    try
        //    {
        //        DataTable sub_st = sub.Select(Category_ID, "");
        //        SubCategory_comboBox.DataSource = null;
        //        SubCategory_comboBox.DisplayMember = "Name";
        //        SubCategory_comboBox.ValueMember = "ID";
        //        SubCategory_comboBox.DataSource = sub_st;
        //        SubCategory_comboBox.SelectedIndex = -1;
        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.Message); }
        //}
        //private void Street_bind()
        //{
        //    DataTable st_st = st.Select("");
        //    Street_comboBox.DataSource = null;
        //    Street_comboBox.DisplayMember = "Name";
        //    Street_comboBox.ValueMember = "ID";
        //    Street_comboBox.DataSource = st_st;
        //    Street_comboBox.SelectedIndex = -1;
        //}
        //private void StateReason_bind()
        //{
        //    //check connection//
        //    Program.buildConnection();
        //    var sc = new MySqlCommand("select ID,Name from `statereason` ORDER BY Name ASC ", Program.MyConn);
        //    sc.ExecuteNonQuery();
        //    var da = new MySqlDataAdapter(sc);
        //    dt1 = new DataTable();
        //    da.Fill(dt1);
        //    Program.MyConn.Close();
        //    StateReason_comboBox.DataSource = null;
        //    StateReason_comboBox.DisplayMember = "Name";
        //    StateReason_comboBox.ValueMember = "ID";
        //    StateReason_comboBox.DataSource = dt1;
        //    StateReason_comboBox.SelectedIndex = -1;
        //}
        //private void Donor_bind()
        //{
        //    //check connection//
        //    Program.buildConnection();
        //    var sc = new MySqlCommand("select ID,Name from `donor` ORDER BY Name ASC ", Program.MyConn);
        //    sc.ExecuteNonQuery();
        //    var da = new MySqlDataAdapter(sc);
        //    dt2 = new DataTable();
        //    da.Fill(dt2);
        //    Program.MyConn.Close();
        //    Donor_comboBox.DataSource = null;
        //    Donor_comboBox.DisplayMember = "Name";
        //    Donor_comboBox.ValueMember = "ID";
        //    Donor_comboBox.DataSource = dt2;
        //    Donor_comboBox.SelectedIndex = -1;

        //}
        //private void DonorGroup_bind(string Donor_ID)
        //{
        //    string condition = "";
        //    DonorGroup_comboBox.DataSource = null;
        //    DonorGroup_comboBox.DisplayMember = "Name";
        //    DonorGroup_comboBox.ValueMember = "ID";

        //    if (Donor_ID == "") //First Time//
        //    {
        //        //check connection//
        //        Program.buildConnection();
        //        string query = "select ID, Name, Donor_ID from `donorgroup` ORDER BY Name ASC ";
        //        var sc = new MySqlCommand(query, Program.MyConn);
        //        sc.ExecuteNonQuery();
        //        var da = new MySqlDataAdapter(sc);
        //        donor_group_dt = new DataTable();
        //        da.Fill(donor_group_dt);
        //        Program.MyConn.Close();
        //        DonorGroup_comboBox.DataSource = donor_group_dt;
        //        DonorGroup_comboBox.SelectedIndex = -1;
        //    }
        //    else //filter from the datatable
        //    {
        //        condition = "Donor_ID=" + Donor_ID;
        //        DataRow[] rows = null;
        //        rows = donor_group_dt.Select(condition);

        //        DataTable selected_rows_dt = new DataTable();
        //        selected_rows_dt = donor_group_dt.Copy();
        //        selected_rows_dt.Rows.Clear();
        //        foreach (DataRow row in rows)
        //            selected_rows_dt.ImportRow(row);

        //        if (selected_rows_dt.Rows.Count != 0 && selected_rows_dt != null)
        //            DonorGroup_comboBox.DataSource = selected_rows_dt;
        //    }
        //}
        #endregion
             
        #region state buttons
        private void Accepted_button_Click(object sender, EventArgs e)
        {
            try
            {
                State = "مقبول";
                FundedBy_comboBox_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Financed_button_Click(object sender, EventArgs e)
        {
            try
            {
                State = "ممول";
                FundedBy_comboBox_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Regected_button_Click(object sender, EventArgs e)
        {
            try
            {
                State = "مرفوض";
                FundedBy_comboBox_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delayed_button_Click(object sender, EventArgs e)
        {
            try
            {
                State = "مؤجل";
                FundedBy_comboBox_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Waiting_button_Click(object sender, EventArgs e)
        {
            try
            {
                State = "بالانتظار";
                FundedBy_comboBox_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Closed_button_Click(object sender, EventArgs e)
        {
            try
            {
                State = "منتهي";
                FundedBy_comboBox_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Withdrew_button_Click(object sender, EventArgs e)
        {
            try
            {
                State = "منسحب";
                FundedBy_comboBox_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancelled_button_Click(object sender, EventArgs e)
        {
            try
            {
                State = "ملغى";
                FundedBy_comboBox_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region save-restore columns
        private void save_visible_columns()
        {
            arr = new int[MP_dataGridView.ColumnCount];
            for (var j = 0; j < MP_dataGridView.ColumnCount; j++)
                if (MP_dataGridView.Columns[j].Visible)
                    arr[j] = 1;
                else arr[j] = 0;
            var value = string.Join(",", arr.Select(i => i.ToString()).ToArray());
            Settings.Default.Admin_Visible_arr = value;
            Settings.Default.Save();
        }

        private void restore_visible_columns()
        {
            try
            {
                if (Settings.Default.Admin_Visible_arr != "")
                {
                    arr = Settings.Default.Admin_Visible_arr.Split(',').Select(s => int.Parse(s)).ToArray();
                    for (var i = 0; i < MP_dataGridView.ColumnCount; i++)
                        if (arr[i] == 1)
                            MP_dataGridView.Columns[i].Visible = true;
                        else MP_dataGridView.Columns[i].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowHideFilters_button_Click(object sender, EventArgs e)
        {
            if (Hided)
            {
                Hided = false;
                if (Settings.Default.theme == "Light")
                    ShowHideFilters_button.BackgroundImage = Resources.Hide2_L;
                else ShowHideFilters_button.BackgroundImage = Resources.Hide2_D;
                Search_panel.Height = PW;
            }
            else
            {
                Hided = true;
                if (Settings.Default.theme == "Light")
                    ShowHideFilters_button.BackgroundImage = Resources.Show2_L;
                else ShowHideFilters_button.BackgroundImage = Resources.Show2_D;
                Search_panel.Height = 0;
            }
        }

        private void ShowHideColumns_button_MouseClick(object sender, MouseEventArgs e)
        {
            cs.mDataGridView_MouseClick(sender, e);
        }

        private void ShowHideColumns_button_Click(object sender, EventArgs e)
        {
            //
        }
        #endregion
         
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tick == 0)
            {
                bind("", FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text, State, replaceQuotation(Donor_comboBox.Text), replaceQuotation(Donor_comboBox.Text), Category_comboBox.Text
                    , SubCategory_comboBox.Text, Street_comboBox.Text, ApplyDate_comboBox.Text, FundDate_comboBox.Text, StateReason_comboBox.Text
                    , P_Parish_comboBox.Text, Age_comboBox.Text, Gender_comboBox.Text, MaritalStatus_comboBox.Text, Partnership_comboBox.Text
                    , "", "", "", "");
                tick++;
            }
            else
            {
                timer1.Stop();
            }
        }
        private void ConvertCellToComboBoxAndColor()
        { 
                for (var i = 0; i < MP_dataGridView.RowCount; i++)
                {
                    //حالة المشروع//
                    var ComboBoxCell1 = new DataGridViewComboBoxCell();
                    ComboBoxCell1.Items.AddRange("مرفوض", "مقبول", "مؤجل", "بالانتظار", "ممول", "منتهي", "منسحب", "ملغى");
                    ComboBoxCell1.Value = MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value;
                    ComboBoxCell1.FlatStyle = FlatStyle.Popup;
                    MP_dataGridView["حالة المشروع", i] = ComboBoxCell1;

                    //الجهة الممولة//
                    var ComboBoxCell2 = new DataGridViewComboBoxCell();
                    ComboBoxCell2.Items.Add("None");
                    for (int dd = 0; dd < ds.Tables[3].Rows.Count; dd++)
                    {
                        ComboBoxCell2.Items.Add(ds.Tables[3].Rows[dd].Field<string>(1));
                    }
                    ComboBoxCell2.Value = MP_dataGridView.Rows[i].Cells["الجهة الممولة"].Value;
                    ComboBoxCell2.FlatStyle = FlatStyle.Flat;
                    MP_dataGridView["الجهة الممولة", i] = ComboBoxCell2;

                    //////نوع المشروع//
                    ////var ComboBoxCell3 = new DataGridViewComboBoxCell();
                    ////ComboBoxCell3.Items.AddRange("Loan", "Grant");
                    ////ComboBoxCell3.Value = MP_dataGridView.Rows[i].Cells["نوع المشروع"].Value;
                    ////ComboBoxCell3.FlatStyle = FlatStyle.Flat;
                    ////MP_dataGridView["نوع المشروع", i] = ComboBoxCell3;

                    //حالة المستفيد// سبب عدم التمويل
                    var ComboBoxCell4 = new DataGridViewComboBoxCell();
                    for (int jj = 0; jj < ds.Tables[8].Rows.Count; jj++)
                    {
                        ComboBoxCell4.Items.AddRange(ds.Tables[8].Rows[jj].Field<string>(1));
                    }
                    ComboBoxCell4.Value = MP_dataGridView.Rows[i].Cells["سبب عدم التمويل"].Value;
                    ComboBoxCell4.FlatStyle = FlatStyle.Popup;
                    MP_dataGridView["سبب عدم التمويل", i] = ComboBoxCell4;

                    //color
                    if (MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value.ToString() == "مرفوض")
                    {
                        MP_dataGridView.Rows[i].DefaultCellStyle.BackColor =
                            MP_dataGridView.Rows[i].Cells["حالة المشروع"].Style.BackColor =
                                Regected_button.BackColor;
                        MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value.ToString() == "مقبول")
                    {
                        MP_dataGridView.Rows[i].DefaultCellStyle.BackColor =
                            MP_dataGridView.Rows[i].Cells["حالة المشروع"].Style.BackColor =
                                Accepted_button.BackColor;
                        MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value.ToString() == "ممول")
                    {
                        MP_dataGridView.Rows[i].DefaultCellStyle.BackColor =
                            MP_dataGridView.Rows[i].Cells["حالة المشروع"].Style.BackColor =
                                Financed_button.BackColor;
                        MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value.ToString() == "منتهي")
                    {
                        MP_dataGridView.Rows[i].DefaultCellStyle.BackColor =
                            MP_dataGridView.Rows[i].Cells["حالة المشروع"].Style.BackColor = Closed_button.BackColor;
                        MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value.ToString() == "مؤجل")
                    {
                        MP_dataGridView.Rows[i].DefaultCellStyle.BackColor =
                            MP_dataGridView.Rows[i].Cells["حالة المشروع"].Style.BackColor =
                                Delayed_button.BackColor;
                        MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value.ToString() == "بالانتظار")
                    {
                        MP_dataGridView.Rows[i].DefaultCellStyle.BackColor =
                            MP_dataGridView.Rows[i].Cells["حالة المشروع"].Style.BackColor =
                                Waiting_button.BackColor;
                        MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value.ToString() == "منسحب")
                    {
                        MP_dataGridView.Rows[i].DefaultCellStyle.BackColor =
                            MP_dataGridView.Rows[i].Cells["حالة المشروع"].Style.BackColor =
                                Withdrew_button.BackColor;
                        MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (MP_dataGridView.Rows[i].Cells["حالة المشروع"].Value.ToString() == "ملغى")
                    {
                        MP_dataGridView.Rows[i].DefaultCellStyle.BackColor =
                            MP_dataGridView.Rows[i].Cells["حالة المشروع"].Style.BackColor =
                                Cancelled_button.BackColor;
                        MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    }
                }

                MP_dataGridView.Columns["اسم الأم"].Width = MP_dataGridView.Columns["اسم الأب"].Width = 60;
                MP_dataGridView.Columns["رقم المشروع"].Width = 70;

                MP_dataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                    MP_dataGridView.Columns["الموبايل"].DefaultCellStyle.Alignment =
                        MP_dataGridView.Columns["حالة المشروع"].DefaultCellStyle.Alignment =
                            MP_dataGridView.Columns["مبلغ التمويل"].DefaultCellStyle.Alignment =
                                MP_dataGridView.Columns["المبلغ المطلوب"].DefaultCellStyle.Alignment =
                                    MP_dataGridView.Columns["الرقم الوطني"].DefaultCellStyle.Alignment =
                                        MP_dataGridView.Columns["رقم دفتر العائلة"].DefaultCellStyle.Alignment =
                                            MP_dataGridView.Columns["الجهة الممولة"].DefaultCellStyle.Alignment =
                                                MP_dataGridView.Columns["نوع المشروع"].DefaultCellStyle.Alignment =
                                                    DataGridViewContentAlignment.MiddleCenter;
             
        }
         
        private void AdminPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            save_visible_columns();
        }
        private void AdminPanel_Activated(object sender, EventArgs e)
        {
            Check_Theme();
        }

        private void Refresh_button_Click_1(object sender, EventArgs e)
        {
            save_visible_columns();
            var NeededFrom = NeededFrom_textBox.Text.Replace(",", "");
            var NeededTo = NeededTo_textBox.Text.Replace(",", "");
            var FinancedFrom = FinancedFrom_textBox.Text.Replace(",", "");
            var FinancedTo = FinancedTo_textBox.Text.Replace(",", "");

            bind("", FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text, State, replaceQuotation(Donor_comboBox.Text), replaceQuotation(DonorGroup_comboBox.Text), Category_comboBox.Text
                , SubCategory_comboBox.Text, Street_comboBox.Text, ApplyDate_comboBox.Text, FundDate_comboBox.Text, StateReason_comboBox.Text
                , P_Parish_comboBox.Text, Age_comboBox.Text, Gender_comboBox.Text, MaritalStatus_comboBox.Text, Partnership_comboBox.Text
                , NeededFrom, NeededTo, FinancedFrom, FinancedTo);
            Search_TxtBox_TextChanged(sender, e);
        }
        private void ExportToExcel_button_Click(object sender, EventArgs e)
        {
            try
            {
                Thread myTh = new Thread(SaveCallDialog);
                myTh.SetApartmentState(ApartmentState.STA);
                myTh.Start();
                myTh.Join();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveCallDialog()
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save Excel File";
                saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.ValidateNames = true;

                var res = saveFileDialog1.ShowDialog();
                if (res == DialogResult.OK && saveFileDialog1.FileName != "")
                {
                    string filename = filename = saveFileDialog1.FileName;
                    XLWorkbook wb = new XLWorkbook { RightToLeft = true };
                    //defaultView gets the visible rows only//
                    DataTable ex_dt = ((DataTable)MP_dataGridView.DataSource).DefaultView.ToTable();

                    //Remove invisible columns//
                    for (var j = MP_dataGridView.ColumnCount - 1; j >= 0; j--)
                        if (MP_dataGridView.Columns[j].Visible == false)
                            ex_dt.Columns.RemoveAt(j);

                    wb.Worksheets.Add(ex_dt, "Exported from App");

                    wb.SaveAs(filename);
                    wb.Dispose();

                    Process.Start(filename);
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Check_Theme()
        {
            var NewTheme = new NewTheme();
            if (Settings.Default.theme == "Light")
                NewTheme.AdminPanel_ToLight(this, Hided);
            else
                NewTheme.AdminPanel_ToNight(this, Hided);
        }
        private string replaceQuotation(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }

        #region datagridview functions
        private void MP_dataGridView_OLD_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                // رقم المشروع - - المستفيد -الأب - الأم - الزوج - الرقم الوطني -القيد - رقم العائلة - مبلغ التمويل - الطائفة   
                // منطقة السكن - العنوان التفصيلي - الكاهن
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 
                    || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7
                    || e.ColumnIndex == 12 || e.ColumnIndex == 13
                    || e.ColumnIndex == 14 || e.ColumnIndex == 15 || e.ColumnIndex == 16)
                {
                    MP_dataGridView.EndEdit();
                    throw new Exception("لا يمكن التعديل على هذه الخلية.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        private void MP_dataGridView_OLD_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dialogResult = MessageBox.Show("هل تريد حفظ التعديلات التي قمت بها؟", "Confirm before save",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //check connection//
                    Program.buildConnection();
                    var r = e.RowIndex;

                    MicroProject_ID = Convert.ToInt32(MP_dataGridView.Rows[r].Cells["رقم المشروع"].Value);
                    Person_ID = Convert.ToInt32(MP_dataGridView.Rows[r].Cells["رقم المستفيد"].Value);
                    var query = "";
                    var Updated_col_name = "";

                    if (e.ColumnIndex == 22) // if we update just the state
                    {
                        var state = -1;
                        if (MP_dataGridView.Rows[r].Cells["حالة المشروع"].Value.ToString() == "مقبول")
                        {
                            if (Settings.Default.role != 1 && Settings.Default.role != 8)
                                throw new Exception("Can't edit this value from this window...");
                            TasksOfProjects = new TasksOfProjects();
                            TasksOfProjects.Insert_AllTasks_MicroProject(MicroProject_ID, DateTime.Now);
                            state = 1;
                            l.Insert_Log("Insert all tasks of project: " + MicroProject_ID + " ", " Task_Project ",
                                Settings.Default.username, DateTime.Now); 
                        }

                        if ( MP_dataGridView.Rows[r].Cells["حالة المشروع"].Value.ToString() == "منسحب" ||
                             MP_dataGridView.Rows[r].Cells["حالة المشروع"].Value.ToString() == "ملغى"  ||
                             MP_dataGridView.Rows[r].Cells["حالة المشروع"].Value.ToString() == "منتهي")
                        {
                            // Delete all notificattions of this project (Mark Seen)
                            //user_ID = -1 //// clear for all users 
                            var noti = new UserNotification();
                            noti.Update_UserNotification(MicroProject_ID, -1);
                        }

                        var date = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day ;
                        query = "update `microproject` set" +
                                " MP_State = (select ID from `state` where Name_ar like N'" +
                                MP_dataGridView.Rows[r].Cells["حالة المشروع"].Value + "')" +
                                ",MP_StateDate = '" + date + "' " +
                                " where MP_ID = " + MicroProject_ID + " ";
                        Updated_col_name = "State:"+ MP_dataGridView.Rows[r].Cells["حالة المشروع"].Value;
                    }

                    else if (e.ColumnIndex == 8) // الموبايل
                    {
                        query = "update `person` set" +
                                " P_Mobile = N'" + MP_dataGridView.Rows[r].Cells["الموبايل"].Value + "' " +
                                " where P_ID = " + Person_ID + " ";
                        Updated_col_name = "Mobile:"+ MP_dataGridView.Rows[r].Cells["الموبايل"].Value;
                    }
                    else if (e.ColumnIndex == 9 || e.ColumnIndex == 11) //اسم المشروع - المبلغ المطلوب
                    {
                        query = "update `microproject` set" +
                                " MP_Name = N'" + MP_dataGridView.Rows[r].Cells["اسم المشروع"].Value + "'," +
                                " MP_RequestedAmount = N'" + MP_dataGridView.Rows[r].Cells["المبلغ المطلوب"].Value +
                                "'" +
                                " where MP_ID = " + MicroProject_ID + " ";
                        Updated_col_name = "Project Name/Requested money";
                    }
                    else if (e.ColumnIndex == 17) // حالة المستفيد -سبب عدم التمويل
                    {
                        query = "update `microproject` set " +
                            " MP_StateReason_ID = (select ID from `statereason` where Name like N'" +
                              MP_dataGridView.Rows[r].Cells["سبب عدم التمويل"].Value + "')" +
                            " where MP_ID = " + MicroProject_ID + " ";
                        Updated_col_name = "State Reason:"+ MP_dataGridView.Rows[r].Cells["سبب عدم التمويل"].Value;
                    }
                    else if (e.ColumnIndex == 19) // ملاحظات-المشروع 
                    {
                        query = "update `microproject` set MP_StateReason = N'" +
                                replaceQuotation(MP_dataGridView.Rows[r].Cells["ملاحظات المشروع"].Value.ToString()) + "' "
                                + " where MP_ID = " + MicroProject_ID + " ";
                        Updated_col_name = "Comments";
                    }
                    else if (e.ColumnIndex == 18 || e.ColumnIndex == 20) //دورة تدريبية - ملاحظات-المستفيد
                    {
                        query = "update `person` set" +
                                " P_MaristesCourse = N'" + replaceQuotation(MP_dataGridView.Rows[r].Cells["دورة تدريبية"].Value.ToString()) +
                                "'," +
                                " P_OtherCourses = N'" + replaceQuotation(MP_dataGridView.Rows[r].Cells["ملاحظات المستفيد"].Value.ToString()) +
                                "' " +
                                " where P_ID = " + Person_ID + " ";
                        Updated_col_name = "Maristes Course/Beneficiary State";
                    }
                    ////else if (e.ColumnIndex == 21) // نوع المشروع
                    ////{
                    ////    var type = -1;
                    ////    if (MP_dataGridView.Rows[r].Cells["نوع المشروع"].Value.ToString() == "Loan") type = 0;
                    ////    else type = 1;

                    ////    query = "update `microproject` set MP_Type = " + type + " where MP_ID = " + MicroProject_ID +
                    ////            " ";
                    ////    Updated_col_name = "Type";
                    ////}
                    else if (e.ColumnIndex == 23) // الجهة الممولة
                    {
                         
                        if(replaceQuotation(MP_dataGridView.Rows[r].Cells["الجهة الممولة"].Value.ToString()) == "None")
                            query = "update `microproject` set MP_Donor = " + SqlInt32.Null
                                + " where MP_ID = " + MicroProject_ID + " ";
                        else
                            query = "update `microproject` set MP_Donor = (Select ID from donor where donor.Name like '" 
                                + replaceQuotation(MP_dataGridView.Rows[r].Cells["الجهة الممولة"].Value.ToString()) + "')"
                                + " where MP_ID = " + MicroProject_ID + " ";
                        Updated_col_name = "Donor:"+ replaceQuotation(MP_dataGridView.Rows[r].Cells["الجهة الممولة"].Value.ToString());
                    }
                     
                    Program.buildConnection();
                    using (var sc = new MySqlCommand(query, Program.MyConn))
                    {
                        sc.ExecuteNonQuery();
                        Program.MyConn.Close();
                    }

                    l.Insert_Log(
                        "Update " + Updated_col_name + " of " + MP_dataGridView.Rows[r].Cells["المستفيد"].Value +
                        " : " + MicroProject_ID + " ", "Admin Panel", Settings.Default.username, DateTime.Now);

                    MP_dataGridView.EndEdit();
                    ConvertCellToComboBoxAndColor();
                }
                else
                {
                    MP_dataGridView.CancelEdit(); // Set cell value back to what it was prior to user's change 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MP_dataGridView_OLD_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        private void MP_dataGridView_OLD_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //if (!show_relative)
                //{
                    SelectedDataRow = ((DataRowView) MP_dataGridView.CurrentRow.DataBoundItem).Row;
                    if (SelectedDataRow != null)
                    {
                        SelectedDataRow = ((DataRowView) MP_dataGridView.CurrentRow.DataBoundItem).Row;
                        if (SelectedDataRow != null)
                        {
                            if (SelectedDataRow["رقم المشروع"] == null ||
                                SelectedDataRow["رقم المشروع"] == DBNull.Value)
                                MicroProject_ID = -1;
                            else MicroProject_ID = int.Parse(SelectedDataRow["رقم المشروع"].ToString());

                            if (SelectedDataRow["رقم المستفيد"] == null ||
                                SelectedDataRow["رقم المستفيد"] == DBNull.Value)
                                Person_ID = -1;
                            else Person_ID = int.Parse(SelectedDataRow["رقم المستفيد"].ToString());
                        }

                        Form application_Form = new Application_Form(Person_ID, MicroProject_ID, main_form);
                        main_form.showNewTab(application_Form, "Application",1);
                    }
                    else
                    {
                        throw new Exception("Please Select a row first");
                    }
                //}
                //else /// show relatives dialog
                //{
                //    SelectedDataRow = ((DataRowView) MP_dataGridView.CurrentRow.DataBoundItem).Row;
                //    if (SelectedDataRow != null)
                //    {
                //        Person_ID = int.Parse(SelectedDataRow["رقم المستفيد"].ToString());

                //        Program.buildConnection();
                //        MySS.query =
                //            "select P_LastName as 'P_LastName' , P_FatherName as 'P_FatherName' from `person` where P_ID = " +
                //            Person_ID + " ";
                //        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                //        MySS.sc.ExecuteNonQuery();
                //        MySS.da = new MySqlDataAdapter(MySS.sc);
                //        MySS.dt = new DataTable();
                //        MySS.da.Fill(MySS.dt);
                //        Program.MyConn.Close();
                //        var dat = bind_relatives(Person_ID, MySS.dt.Rows[0][1].ToString(),
                //            MySS.dt.Rows[0][0].ToString());


                //        var b = new StringBuilder();
                //        foreach (DataRow r in dat.Rows)
                //        {
                //            foreach (DataColumn c in dat.Columns) b.Append(r[c.ColumnName] + " - ");
                //            b.Append(" \n ");
                //        }

                //        MessageBox.Show(b.ToString());
                //    }
                //    else
                //    {
                //        throw new Exception("Please Select a row first");
                //    }
                //}
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Please Select a row first"))
                    MessageBox.Show("Please Select a row first");
                else
                    MessageBox.Show(ex.Message);
            }
        }
        private void MP_dataGridView_OLD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0) // mp_id
                {
                    if (MP_dataGridView.Rows[e.RowIndex].Cells["رقم المشروع"] == null ||
                        MP_dataGridView.Rows[e.RowIndex].Cells["رقم المشروع"].Value == DBNull.Value)
                        MicroProject_ID = -1;
                    else
                        MicroProject_ID = int.Parse(MP_dataGridView.Rows[e.RowIndex].Cells["رقم المشروع"].Value
                            .ToString());

                    if (MP_dataGridView.Rows[e.RowIndex].Cells["رقم المستفيد"] == null ||
                        MP_dataGridView.Rows[e.RowIndex].Cells["رقم المستفيد"].Value == DBNull.Value)
                        Person_ID = -1;
                    else
                        Person_ID = int.Parse(MP_dataGridView.Rows[e.RowIndex].Cells["رقم المستفيد"].Value
                            .ToString());


                    Form application_Form = new Application_Form(Person_ID, MicroProject_ID, main_form);
                    main_form.showNewTab(application_Form, "Application",1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MP_dataGridView_OLD_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            newColumn = MP_dataGridView.Columns[e.ColumnIndex];

            // If oldColumn is null, then the DataGridView is not currently sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (direction == ListSortDirection.Ascending)
                {
                    direction_1 = "desc";
                    direction = ListSortDirection.Descending;
                    //  newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction_1 = "asc";
                    direction = ListSortDirection.Ascending;
                    //   newColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                oldColumn = newColumn;
                direction_1 = "asc";
                direction = ListSortDirection.Ascending;
                // oldColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            }

            if (MP_dataGridView.Columns[e.ColumnIndex].HeaderText == "حالة المشروع")
            {
                bind("bb", FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text, State, replaceQuotation(Donor_comboBox.Text), replaceQuotation(Donor_comboBox.Text), Category_comboBox.Text,
                    SubCategory_comboBox.Text, Street_comboBox.Text, ApplyDate_comboBox.Text, FundDate_comboBox.Text, StateReason_comboBox.Text
                    , P_Parish_comboBox.Text, Age_comboBox.Text, Gender_comboBox.Text, MaritalStatus_comboBox.Text, Partnership_comboBox.Text
                    , "", "", "", "");
            }
            else
            {
                MP_dataGridView.Sort(MP_dataGridView.Columns[e.ColumnIndex], direction);
                MP_dataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;
                ConvertCellToComboBoxAndColor();
            }
        }
        private void MP_dataGridView_OLD_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var SelectedRowCount = MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
                Selected_label.Text = "Selected: " + SelectedRowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MP_dataGridView_OLD_Sorted(object sender, EventArgs e)
        {
            ConvertCellToComboBoxAndColor();
        }
        #endregion

        #region search functions
        private void Search_TxtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Search_TxtBox.Text))
                { 
                    foreach (DataGridViewRow r in MP_dataGridView.Rows)
                        r.Visible = true;

                    Count_label.Text = "All:" + total_bind_count;
                }
                else
                {
                    foreach (DataGridViewRow r in MP_dataGridView.Rows)
                    {
                        var found = 0;
                        for (var i = 0; i < MP_dataGridView.ColumnCount; i++)
                            if (ExactSearch_checkBox.Checked)
                            {
                                if (MP_dataGridView.Columns[i].Visible)
                                    if (r.Cells[i].Value.ToString().ToLower().Equals(Search_TxtBox.Text.ToLower()))
                                    {
                                        MP_dataGridView.Rows[r.Index].Visible = true;
                                        found = 1;
                                    }
                            }
                            else
                            {
                                if (r.Cells[i].Value.ToString().ToLower().Contains(Search_TxtBox.Text.ToLower()))
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

                        Count_label.Text = "All:" + MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ExactSearch_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Search_TxtBox_TextChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
         
        #region mouse enter-leave
        private void Refresh_button_MouseEnter(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                Refresh_button.BackgroundImage = Resources.Refresh2_L;
            else Refresh_button.BackgroundImage = Resources.Refresh2_D;
        }

        private void Refresh_button_MouseLeave(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                Refresh_button.BackgroundImage = Resources.Refresh2_D;
            else Refresh_button.BackgroundImage = Resources.Refresh2_L;
        }

        private void ExportToExcel_button_MouseEnter(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                ExportToExcel_button.BackgroundImage = Resources.Excel_L;
            else ExportToExcel_button.BackgroundImage = Resources.Excel_D;
        }

        private void ExportToExcel_button_MouseLeave(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                ExportToExcel_button.BackgroundImage = Resources.Excel_D;
            else ExportToExcel_button.BackgroundImage = Resources.Excel_L;
        }

        private void ShowHide_button_MouseEnter(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                ShowHideColumns_button.BackgroundImage = Resources.Down_L;
            else ShowHideColumns_button.BackgroundImage = Resources.Down_D;
        }

        private void ShowHide_button_MouseLeave(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                ShowHideColumns_button.BackgroundImage = Resources.Down_D;
            else ShowHideColumns_button.BackgroundImage = Resources.Down_L;
        }

        private void ShowHideFilters_button_MouseEnter(object sender, EventArgs e)
        {
            if (Hided)
            {
                if (Settings.Default.theme == "Light")
                    ShowHideFilters_button.BackgroundImage = Resources.Show2_L;
                else ShowHideFilters_button.BackgroundImage = Resources.Show2_D;
            }
            else
            {
                if (Settings.Default.theme == "Light")
                    ShowHideFilters_button.BackgroundImage = Resources.Hide2_L;
                else ShowHideFilters_button.BackgroundImage = Resources.Hide2_D;
            }
        }

        private void ShowHideFilters_button_MouseLeave(object sender, EventArgs e)
        {
            if (Hided)
            {
                if (Settings.Default.theme == "Light")
                    ShowHideFilters_button.BackgroundImage = Resources.Show2_D;
                else ShowHideFilters_button.BackgroundImage = Resources.Show2_L;
            }
            else
            {
                if (Settings.Default.theme == "Light")
                    ShowHideFilters_button.BackgroundImage = Resources.Hide2_D;
                else ShowHideFilters_button.BackgroundImage = Resources.Hide2_L;
            }
        }
        #endregion
         
        #region textbox , combobox events
        private void FundedBy_comboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                var NeededFrom = NeededFrom_textBox.Text.Replace(",", "");
                var NeededTo = NeededTo_textBox.Text.Replace(",", "");
                var FinancedFrom = FinancedFrom_textBox.Text.Replace(",", "");
                var FinancedTo = FinancedTo_textBox.Text.Replace(",", "");

                bind("", FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text, State, replaceQuotation(Donor_comboBox.Text), replaceQuotation(DonorGroup_comboBox.Text), Category_comboBox.Text
                    , SubCategory_comboBox.Text, Street_comboBox.Text, ApplyDate_comboBox.Text, FundDate_comboBox.Text, StateReason_comboBox.Text
                    , P_Parish_comboBox.Text, Age_comboBox.Text, Gender_comboBox.Text, MaritalStatus_comboBox.Text,Partnership_comboBox.Text
                    , NeededFrom, NeededTo, FinancedFrom, FinancedTo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NeededFrom_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (NeededFrom_textBox.Text != "")
                {
                    NeededFrom_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(NeededFrom_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    NeededFrom_textBox.SelectionStart = NeededFrom_textBox.Text.Length;
                    NeededFrom_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NeededTo_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (NeededTo_textBox.Text != "")
                {
                    NeededTo_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(NeededTo_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    NeededTo_textBox.SelectionStart = NeededTo_textBox.Text.Length;
                    NeededTo_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FinancedFrom_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (FinancedFrom_textBox.Text != "")
                {
                    FinancedFrom_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(FinancedFrom_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    FinancedFrom_textBox.SelectionStart = FinancedFrom_textBox.Text.Length;
                    FinancedFrom_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FinancedTo_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (FinancedTo_textBox.Text != "")
                {
                    FinancedTo_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(FinancedTo_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    FinancedTo_textBox.SelectionStart = FinancedTo_textBox.Text.Length;
                    FinancedTo_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MP_Category_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //add the type_comboBox_selectedValue_changed to fill the data before binding subType  
            try
            {
                if (user_mode)
                    SubCategory_bind(Category_comboBox.SelectedValue.ToString());
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
        #endregion
         
        #region right click menu

        private void refreshPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_button_Click_1(sender, e);
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

        private void showHideFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHideFilters_button_Click(sender, e);
        }
        
        private void clearAllFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Type_comboBox.Text = Donor_comboBox.Text = DonorGroup_comboBox.Text = Category_comboBox.Text = SubCategory_comboBox.Text
                = Street_comboBox.Text = ApplyDate_comboBox.Text = FundDate_comboBox.Text = StateReason_comboBox.Text
                = P_Parish_comboBox.Text = Age_comboBox.Text = Gender_comboBox.Text = MaritalStatus_comboBox.Text = "";
            NeededFrom_textBox.Text = NeededTo_textBox.Text = FinancedFrom_textBox.Text = FinancedTo_textBox.Text = "";
            State = "";

            Search_TxtBox.Text = "";
            Refresh_button_Click_1(sender, e);
        }

        #endregion
         
    }
}