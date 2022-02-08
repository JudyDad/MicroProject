using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWorkApplication
{
    public partial class Add_Initial_Visit : Form
    {
        public Add_Initial_Visit(MainForm mainForm)
        {
            InitializeComponent();
            Update_Mood = false;
            this.mainForm = mainForm;
        }
        public Add_Initial_Visit(int VI_ID, MainForm mainForm)
        {
            InitializeComponent();
            Update_Mood = true;
            this.VI_ID = VI_ID;
            this.mainForm = mainForm;
        }

        DataRow SelectedDataRow;
        int MicroProject_ID, VI_ID, User_ID;
        MySqlComponents MySS;
        NewTheme NewTheme;
        Log l; Select s;
        DataTable Visit_Users_dt;
        TasksOfProjects TasksOfProjects;
        bool Update_Mood;
        MainForm mainForm;

        private void Add_Initial_Visit_Load(object sender, EventArgs e)
        {
            NewTheme = new NewTheme();
            if (Properties.Settings.Default.theme == "Light")
            {
                NewTheme.Visit_ToLight(this);
            }
            else
            {
                NewTheme.Visit_ToNight(this);
            }
            mainForm.Project_label.Visible = mainForm.TabName_label.Visible = true;
            mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = false;
            mainForm.TabName_label.Text = "Initial Visits";

            MySS = new MySqlComponents();
            l = new Log(); s = new Select();

            users_bind(V1_comboBox); users_bind(V2_comboBox); users_bind(V3_comboBox); users_bind(V4_comboBox);
            if (Update_Mood)
            {
                fill_Initial_Visit_boxes(VI_ID);
                bind_InitialVisit(MicroProject_ID);

                mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
            }

            Person_Name_textBox1.AutoCompleteCustomSource = s.select_beneficiaries("", "");
            MicroProject_ID_textBox1.AutoCompleteCustomSource = s.select_project_IDs("", "");
        }

        private void MicroProject_ID_textBox1_Leave(object sender, EventArgs e)
        {
            if (MicroProject_ID_textBox1.Text != "")
            {
                //check connection//
                Program.buildConnection();
                string query = "select PMP.MicroProject_ID as 'MicroProject_ID'" 
                    + " , CONCAT(P_FirstName, ' ', P_LastName, ' ابن/ة ', P_FatherName) as 'Beneficiary Name'"
                    + ",L.Loan_Amount as 'Loan Amount'"
                    + ",L.Loan_DateTaken as 'Loan Date'"
                    + " from person_microproject PMP left outer join person P1 on P1.P_ID = PMP.Person_ID "
                    + " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID"
                    + " left outer join loan L on MP.MP_ID = L.MicroProject_ID " +
                " where PMP.MicroProject_ID = " + Convert.ToInt32(MicroProject_ID_textBox1.Text); 

                MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Program.MyConn.Close();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        clear_Initial_Visit_boxes();
                        MicroProject_ID = Int32.Parse(dt.Rows[0]["MicroProject_ID"].ToString());
                        Person_Name_textBox1.Text = dt.Rows[0]["Beneficiary Name"].ToString();

                        if (dt.Rows[0]["Loan Amount"] != DBNull.Value) 
                        {
                            Double dd = Convert.ToDouble(dt.Rows[0]["Loan Amount"].ToString());
                            LoanAmount_textBox1.Text = dd.ToString();
                        }
                        if (dt.Rows[0]["Loan Date"] != DBNull.Value)
                        {
                            DateTime date = Convert.ToDateTime(dt.Rows[0]["Loan Date"].ToString());
                            LoanDate_dateTimePicker.Value = date;
                        }

                        if (Visits_DataGridView.Rows.Count > 1)
                            Visits_DataGridView.Rows.Clear();
                        bind_InitialVisit(MicroProject_ID);

                        mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                        mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
                    }
                }
            }
        }
        private void Person_Name_textBox1_Leave(object sender, EventArgs e)
        {
            if (Person_Name_textBox1.Text != "")
            {
                //check connection//
                Program.buildConnection();
                string query = "select PMP.MicroProject_ID as 'MicroProject_ID'" +
                    " , CONCAT(P_FirstName, ' ', P_LastName, ' ابن/ة ', P_FatherName) as 'Beneficiary Name'" +
                    ",L.Loan_Amount as 'Loan Amount'"
                    + ",L.Loan_DateTaken as 'Loan Date'"
                    + " from person_microproject PMP left outer join person P1 on P1.P_ID = PMP.Person_ID "
                    + " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID"
                    + " left outer join loan L on MP.MP_ID = L.MicroProject_ID " +
    " WHERE CONCAT(TRIM(P1.P_FirstName), ' ', TRIM(P1.P_LastName), ' ابن/ة ', TRIM(P1.P_FatherName)) LIKE '%" + Person_Name_textBox1.Text + "%'";

                MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Program.MyConn.Close();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MicroProject_ID = Int32.Parse(dt.Rows[0]["MicroProject_ID"].ToString());
                        MicroProject_ID_textBox1.Text = MicroProject_ID.ToString();
                        Person_Name_textBox1.Text = dt.Rows[0]["Beneficiary Name"].ToString();

                        if (dt.Rows[0]["Loan Amount"] != DBNull.Value)
                        {
                            Double dd = Convert.ToDouble(dt.Rows[0]["Loan Amount"].ToString());
                            LoanAmount_textBox1.Text = dd.ToString();
                        }
                        if (dt.Rows[0]["Loan Date"] != DBNull.Value)
                        {
                            DateTime date = (DateTime)dt.Rows[0]["Loan Date"];
                            LoanDate_dateTimePicker.Value = date;
                        }
                        if (Visits_DataGridView.Rows.Count > 1)
                            Visits_DataGridView.Rows.Clear();
                        bind_InitialVisit(MicroProject_ID);

                        mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                        mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
                    }
                }
            }
        }

        private void bind_InitialVisit(int MicroProject_ID)
        {
            try
            {//check connection//
                Program.buildConnection();
                string query = "SELECT `ID`"
                        + ", visitinitial.`MicroProject_ID`"
                        + ", CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                        + ", `Date`, `Type`, `Reseption`, `Waiting`, `Visit`, `Lawyer`, `Purchasing`, `Photography`, `OtherComments`"
                        + ", `StartInTime`, `StartInTimeReason`, `PurchaseAllItemsOfBudget`, `PurchaseAllItemsOfBudgetReason`"
                        + ", `SamePlace`, `SamePlaceReason`, `SameQualityAndQuantity`, `SameQualityAndQuantityReason`, `Marketing` "
                        + " FROM visitinitial left outer join person_microproject on visitinitial.MicroProject_ID = person_microproject.MicroProject_ID "
                        + " join person P on P.P_ID = person_microproject.Person_ID "
                        + " where visitinitial.`MicroProject_ID` = " + MicroProject_ID;
                MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Visits_DataGridView.Columns.Clear();
                Visits_DataGridView.ColumnHeadersVisible = false;
                Visits_DataGridView.DataSource = dt;
                Visits_DataGridView.ColumnHeadersVisible = true;

                Program.MyConn.Close();
                DataGridViewColumn dgc1 = Visits_DataGridView.Columns["ID"];
                dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["Reseption"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["Waiting"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["Visit"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["Lawyer"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["Purchasing"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["Photography"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["OtherComments"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["StartInTime"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["StartInTimeReason"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["PurchaseAllItemsOfBudget"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["PurchaseAllItemsOfBudgetReason"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["SamePlace"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["SamePlaceReason"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["SameQualityAndQuantity"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["SameQualityAndQuantityReason"]; dgc1.Visible = false;
                //dgc1 = Visits_DataGridView.Columns["Marketing"]; dgc1.Visible = false;
                Visits_DataGridView.Columns["Marketing"].Visible = false;

                Visits_DataGridView.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";

                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.Name = "VI_DeleteRow";
                col.HeaderText = " ";
                col.FlatStyle = FlatStyle.Flat;
                Visits_DataGridView.Columns.Add(col);

                Visits_DataGridView.Columns["MicroProject_ID"].DefaultCellStyle.Alignment = Visits_DataGridView.Columns["Type"].DefaultCellStyle.Alignment =
                    Visits_DataGridView.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                Visits_DataGridView.Columns["MicroProject_ID"].Width = 100;
                Visits_DataGridView.Columns["Type"].Width = 50;
                Visits_DataGridView.Columns[21].Width = 50;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void users_bind(ComboBox comboBox)
        {
            try
            {
                //check connection//
                Program.buildConnection();
                MySS.da = new MySqlDataAdapter("select UserID as 'U_ID',UserName as 'Visitors' from `user`", Program.MyConn);
                DataSet ds = new DataSet();
                MySS.da.Fill(ds);

                comboBox.DataSource = ds.Tables[0];
                comboBox.DisplayMember = "Visitors";
                comboBox.ValueMember = "U_ID";
                comboBox.SelectedIndex = -1;

                Program.MyConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void insert_InitialVisit()
        {
            Program.buildConnection();
            MySS.query = "INSERT INTO `visitinitial`(`Date`, `Type`, `Reseption`, `Waiting`, `Visit`, `Lawyer`, `Purchasing`, `Photography`, `OtherComments`,"
                        + "`StartInTime`, `StartInTimeReason`, `PurchaseAllItemsOfBudget`, `PurchaseAllItemsOfBudgetReason`, `SamePlace`, `SamePlaceReason`,"
                        + "`SameQualityAndQuantity`, `SameQualityAndQuantityReason`, `Marketing`, `MicroProject_ID`) values(N'"
                + VisitDate_dateTimePicker.Value.Year.ToString() + "/"
                + VisitDate_dateTimePicker.Value.Month.ToString() + "/"
                + VisitDate_dateTimePicker.Value.Day.ToString() + "',N'"

                + VisitType_comboBox1.Text + "',N'"
                + Reseption_textBox.Text + "',N'"
                + Waiting_textBox.Text + "',N'"
                + Visit_textBox.Text + "',N'"
                + Lawyer_textBox.Text + "',N'"
                + PurchaseItemsBudget_textBox.Text + "',N'"
                + Photography_textBox.Text + "',N'"
                + OtherComments_textBox.Text + "',"

                + (StartTime_checkBox.Checked ? 1 : 0) + ",N'"
                + StartTime_textBox.Text + "',"
                + (PurchaseItemsBudget_checkBox.Checked ? 1 : 0) + ",N'"
                + PurchaseItemsBudget_textBox.Text + "',"
                + (SamePlace_checkBox.Checked ? 1 : 0) + ",N'"
                + SamePlace_textBox.Text + "',"
                + (ItemsQuantityQuality_checkBox.Checked ? 1 : 0) + ",N'"
                + ItemsQuantityQuality_textBox.Text + "',N'"
                + Marketing_textBox.Text + "',"
                + MicroProject_ID + " )";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }
        private void insert_InitialVisit_User(int VI_ID, int User_ID)
        {
            MySS.query = "INSERT INTO `visitinitial_user`(`VisitInitial_ID`, `User_ID`) values("
                + VI_ID + ","
                + User_ID + ")";

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            Program.MyConn.Close();
        }
        private void delete_InitialVisit_User(int VI_ID, int User_ID)
        {
            MySS.query = "Delete from `visitinitial_user` where `VisitInitial_ID`= " + VI_ID + " and `User_ID` = " + User_ID + " ";

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }
        private void update_InitialVisit(int VI_ID)
        {
            MySS.query = " UPDATE `visitinitial` SET " +
                  VisitDate_dateTimePicker.Value.Year.ToString() + "/"
                + VisitDate_dateTimePicker.Value.Month.ToString() + "/"
                + VisitDate_dateTimePicker.Value.Day.ToString() + "'" +
                ",`Type`=N'" + VisitType_comboBox1.Text + "'" +
                ",`Reseption`=N'" + Reseption_textBox.Text + "'" +
                ",`Waiting`=N'" + Waiting_textBox.Text + "'" +
                ",`Visit`=N'" + Visit_textBox.Text + "'" +
                ",`Lawyer`=N'" + Lawyer_textBox.Text + "'" +
                ",`Purchasing`=N'" + Purchasing_textBox.Text + "'" +
                ",`Photography`=N'" + Photography_textBox.Text + "'" +
                ",`OtherComments`=N'" + OtherComments_textBox.Text + "'" +
                ",`StartInTime`=" + (StartTime_checkBox.Checked ? 1 : 0) + "" +
                ",`StartInTimeReason`=N'" + StartTime_textBox.Text + "'" +
                ",`PurchaseAllItemsOfBudget`=" + (PurchaseItemsBudget_checkBox.Checked ? 1 : 0) + "" +
                ",`PurchaseAllItemsOfBudgetReason`=N'" + PurchaseItemsBudget_textBox.Text + "'" +
                ",`SamePlace`=" + (SamePlace_checkBox.Checked ? 1 : 0) + "" +
                ",`SamePlaceReason`=N'" + SamePlace_textBox.Text + "'" +
                ",`SameQualityAndQuantity`=" + (ItemsQuantityQuality_checkBox.Checked ? 1 : 0) + "" +
                ",`SameQualityAndQuantityReason`=N'" + ItemsQuantityQuality_textBox.Text + "'" +
                ",`Marketing`=N'" + Marketing_textBox.Text + "'" +
                ",`MicroProject_ID`=" + MicroProject_ID + "" +
                " WHERE ID = " + VI_ID + "";
            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            Program.MyConn.Close();
        }
        private void clear_Initial_Visit_boxes()
        {
            Reseption_textBox.Text = Waiting_textBox.Text = Visit_textBox.Text = Lawyer_textBox.Text =
               Purchasing_textBox.Text = Photography_textBox.Text = LoanAmount_textBox1.Text =
               OtherComments_textBox.Text = StartTime_textBox.Text = PurchaseItemsBudget_textBox.Text = "";
            SamePlace_textBox.Text = ItemsQuantityQuality_textBox.Text = Marketing_textBox.Text = "";

            InsertVisit_button.Visible = true;
            LoanDate_dateTimePicker.Value = VisitDate_dateTimePicker.Value = DateTime.Now;

            VI_ID = -1;
            SelectedDataRow = null;
            V1_comboBox.Text = V2_comboBox.Text = V3_comboBox.Text = V4_comboBox.Text = "";
            V1_comboBox.SelectedIndex = V2_comboBox.SelectedIndex = V3_comboBox.SelectedIndex = V4_comboBox.SelectedIndex = -1;
            Update_Mood = false;
        }
        private void fill_Initial_Visit_boxes(int VI_ID)
        {
            try
            {
                DataTable dt = s.InitialVisit_bind(VI_ID);
                if (dt != null)
                {
                    MicroProject_ID = Int32.Parse(dt.Rows[0]["MicroProject_ID"].ToString());
                    VI_ID = Int32.Parse(dt.Rows[0]["ID"].ToString());

                    VisitType_comboBox1.Text = (string)dt.Rows[0]["Type"];
                    DateTime date1 = (DateTime)dt.Rows[0]["Date"];
                    VisitDate_dateTimePicker.Value = date1;

                    Reseption_textBox.Text = (string)dt.Rows[0]["Reseption"];
                    Waiting_textBox.Text = (string)dt.Rows[0]["Waiting"];
                    Visit_textBox.Text = (string)dt.Rows[0]["Visit"];
                    Lawyer_textBox.Text = (string)dt.Rows[0]["Lawyer"];
                    Purchasing_textBox.Text = (string)dt.Rows[0]["Purchasing"];
                    Photography_textBox.Text = (string)dt.Rows[0]["Photography"];
                    OtherComments_textBox.Text = (string)dt.Rows[0]["OtherComments"];
                    Marketing_textBox.Text = (string)dt.Rows[0]["Marketing"];
                    //StartInTime
                    int StartInTime = Int32.Parse(dt.Rows[0]["StartInTime"].ToString());
                    if (StartInTime == 0)
                        StartTime_checkBox.Checked = false;
                    else
                        StartTime_checkBox.Checked = true;
                    StartTime_textBox.Text = (string)dt.Rows[0]["StartInTimeReason"];
                    //PurchaseAllItemsOfBudget
                    int PurchaseItemsBudget = Int32.Parse(dt.Rows[0]["PurchaseAllItemsOfBudget"].ToString());
                    if (PurchaseItemsBudget == 0)
                        PurchaseItemsBudget_checkBox.Checked = false;
                    else
                        PurchaseItemsBudget_checkBox.Checked = true;
                    PurchaseItemsBudget_textBox.Text = (string)dt.Rows[0]["PurchaseAllItemsOfBudgetReason"];
                    //SamePlace
                    int SamePlace = Int32.Parse(dt.Rows[0]["SamePlace"].ToString());
                    if (SamePlace == 0)
                        SamePlace_checkBox.Checked = false;
                    else
                        SamePlace_checkBox.Checked = true;
                    SamePlace_textBox.Text = (string)dt.Rows[0]["SamePlaceReason"];
                    //SameQualityAndQuantity
                    int ItemsQuantityQuality = Int32.Parse(dt.Rows[0]["SameQualityAndQuantity"].ToString());
                    if (ItemsQuantityQuality == 0)
                        ItemsQuantityQuality_checkBox.Checked = false;
                    else
                        ItemsQuantityQuality_checkBox.Checked = true;
                    ItemsQuantityQuality_textBox.Text = (string)dt.Rows[0]["SameQualityAndQuantityReason"];

                    MicroProject_ID_textBox1.Text = MicroProject_ID.ToString();
                    Person_Name_textBox1.Text = (string)dt.Rows[0]["Beneficiary Name"];

                    Program.buildConnection();

                    MySS.query = "select Loan_Amount as 'Loan Amount'"
                                    + ",Loan_DateTaken as 'Loan Date'"
                                    + " from loan where MicroProject_ID = " + MicroProject_ID + " ";
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    MySS.sc.ExecuteNonQuery();
                    MySS.da = new MySqlDataAdapter(MySS.sc);
                    MySS.dt = new DataTable();
                    MySS.da.Fill(MySS.dt);

                    Program.MyConn.Close();

                    Double dd = Convert.ToDouble(MySS.dt.Rows[0][0].ToString());
                    LoanAmount_textBox1.Text = dd.ToString();
                    DateTime date = (DateTime)MySS.dt.Rows[0][1];
                    LoanDate_dateTimePicker.Value = date;

                    V1_comboBox.Text = V2_comboBox.Text = V3_comboBox.Text = V4_comboBox.Text = "";
                    V1_comboBox.SelectedIndex = V2_comboBox.SelectedIndex = V3_comboBox.SelectedIndex = V4_comboBox.SelectedIndex = -1;

                    Visit_Users_dt = Get_Initial_Visit_Users(VI_ID);
                    if (Visit_Users_dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)
                        {
                            int u_id = Visit_Users_dt.Rows[i].Field<int>(0);
                            string V = "V";
                            string VV = "_comboBox";
                            string fullName = V + (i + 1) + VV;
                            ComboBox cbx = this.Controls.Find(fullName, true).FirstOrDefault() as ComboBox;
                            cbx.SelectedValue = u_id;
                        }
                    }
                    ////clear checkboxs of listbox
                    //foreach (ListViewItem item in Users_listView.CheckedItems)
                    //{
                    //    item.Checked = false;
                    //}
                    ////fill visitors (select them)
                    //Visit_Users_dt = Get_Initial_Visit_Users(VI_ID);
                    //if (Visit_Users_dt.Rows.Count != 0)
                    //{
                    //    for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)
                    //    {
                    //        //// Find the string in ListBox2.
                    //        //int index = Users_listBox.FindString(Visit_Users_dt.Rows[i].Field<string>(1));
                    //        //// User_ID = MySS.dt.Rows[i].Field<int>(0);
                    //        //Users_listBox.SetSelected(index, true);

                    //        // Find the string in Listview.
                    //        string user_Name = Visit_Users_dt.Rows[i].Field<string>(1);
                    //        var item = Users_listView.FindItemWithText(Visit_Users_dt.Rows[i].Field<string>(1));
                    //        int index = Users_listView.Items.IndexOf(item);
                    //        Users_listView.Items[index].Checked = true;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void CheckUserPermission()
        {
            switch (Properties.Settings.Default.role)
            {
                case 1: //admin
                    {

                    }
                    break;
                case 2: //Data
                    {

                    }
                    break;
                case 3: //Financial_Lawful
                    {
                        throw new Exception(" You don't have the permission for this action.");
                    }
                    break;
                case 4: //Guest
                    {
                        throw new Exception(" You don't have the permission for this action.");
                    }
                    break;
                case 5: //manager ???
                    {
                    }
                    break;
            }
        }

        private void InsertVisit_button_Click(object sender, EventArgs e)
        {
            try
            {
                CheckUserPermission();
                if (MicroProject_ID_textBox1.Text == "" || Person_Name_textBox1.Text == "" || MicroProject_ID == -1)
                {
                    throw new Exception("You should choose a project first ..!!");
                }
                if (VisitType_comboBox1.Text == "")
                {
                    throw new Exception("You can't leave empty fields ..!!");
                }

                ////insert Section////
                if (SelectedDataRow == null || Update_Mood == false)
                {
                    insert_InitialVisit();

                    ////////////// GET current V_ID //////////////////
                    Get_Current_Initial_Visit();

                    //check user comboBoxs
                    if (V1_comboBox.Text != "")
                    {
                        User_ID = Convert.ToInt32(V1_comboBox.SelectedValue);
                        insert_InitialVisit_User(VI_ID, User_ID);
                    }
                    if (V2_comboBox.Text != "")
                    {
                        User_ID = Convert.ToInt32(V2_comboBox.SelectedValue);
                        insert_InitialVisit_User(VI_ID, User_ID);
                    }
                    if (V3_comboBox.Text != "")
                    {
                        User_ID = Convert.ToInt32(V3_comboBox.SelectedValue);
                        insert_InitialVisit_User(VI_ID, User_ID);
                    }
                    if (V4_comboBox.Text != "")
                    {
                        User_ID = Convert.ToInt32(V4_comboBox.SelectedValue);
                        insert_InitialVisit_User(VI_ID, User_ID);
                    }
                    l.Insert_Log("Insert visit to " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " ", " Initial Visit ", Properties.Settings.Default.username, DateTime.Now);

                    //make the initial visit task of this project ==> checked
                    //update task ^_^
                    ///////////////////////////   Task IDs = 23   ////////////////////////////////////////////
                    TasksOfProjects = new TasksOfProjects();
                    TasksOfProjects.Update_Task_MicroProject(MicroProject_ID, 23, true, DateTime.Now);
                    //////////////////////////////////////////////////////////////////////////////////////////

                }
                else          ////update section ////
                {
                    update_InitialVisit(VI_ID);
                    //check if visitors changed ...!!!
                    bool found = false;
                    Visit_Users_dt = Get_Initial_Visit_Users(VI_ID);
                    //search if rows in database (users of this visit) are all selected ?
                    if (Visit_Users_dt.Rows.Count == 0)
                    {
                        // check comboBoxs and insert all //
                        if (V1_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V1_comboBox.SelectedValue);
                            insert_InitialVisit_User(VI_ID, User_ID);
                        }
                        if (V2_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V2_comboBox.SelectedValue);
                            insert_InitialVisit_User(VI_ID, User_ID);
                        }
                        if (V3_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V3_comboBox.SelectedValue);
                            insert_InitialVisit_User(VI_ID, User_ID);
                        }
                        if (V4_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V4_comboBox.SelectedValue);
                            insert_InitialVisit_User(VI_ID, User_ID);
                        }
                    }
                    else
                    {
                        //check if visitors changed ...!!!
                        if (V1_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V1_comboBox.SelectedValue);
                            for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)
                            {
                                if (Visit_Users_dt.Rows[i].Field<int>(0) == User_ID) { found = true; break; }
                                else found = false;
                            }
                            if (!found)
                                insert_InitialVisit_User(VI_ID, User_ID);
                        }
                        if (V2_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V2_comboBox.SelectedValue);
                            for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)
                            {
                                if (Visit_Users_dt.Rows[i].Field<int>(0) == User_ID) { found = true; break; }
                                else found = false;
                            }
                            if (!found)
                                insert_InitialVisit_User(VI_ID, User_ID);
                        }
                        if (V3_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V3_comboBox.SelectedValue);
                            for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)
                            {
                                if (Visit_Users_dt.Rows[i].Field<int>(0) == User_ID) { found = true; break; }
                                else found = false;
                            }
                            if (!found)
                                insert_InitialVisit_User(VI_ID, User_ID);
                        }
                        if (V4_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V4_comboBox.SelectedValue);
                            for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)
                            {
                                if (Visit_Users_dt.Rows[i].Field<int>(0) == User_ID) { found = true; break; }
                                else found = false;
                            }
                            if (!found)
                                insert_InitialVisit_User(VI_ID, User_ID);
                        }

                        //search if rows in database (users of this visit) are all selected ?
                        for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)     //For users of this visit
                        {
                            //check al user comboBoxs
                            if (V1_comboBox.Text != "")
                            {
                                User_ID = Convert.ToInt32(V1_comboBox.SelectedValue);
                                if (Visit_Users_dt.Rows[i].Field<int>(0) == User_ID)//user in database and existed in comboBox1
                                    continue;
                            }
                            if (V2_comboBox.Text != "")
                            {
                                User_ID = Convert.ToInt32(V2_comboBox.SelectedValue);
                                if (Visit_Users_dt.Rows[i].Field<int>(0) == User_ID)//user in database and existed in comboBox2
                                    continue;
                            }
                            if (V3_comboBox.Text != "")
                            {
                                User_ID = Convert.ToInt32(V3_comboBox.SelectedValue);
                                if (Visit_Users_dt.Rows[i].Field<int>(0) == User_ID)//user in database and existed in comboBox3
                                    continue;
                            }
                            if (V4_comboBox.Text != "")
                            {
                                User_ID = Convert.ToInt32(V4_comboBox.SelectedValue);
                                if (Visit_Users_dt.Rows[i].Field<int>(0) == User_ID)//user in database and existed in comboBox4
                                    continue;
                            }
                            //remove this user from database
                            delete_InitialVisit_User(VI_ID, Visit_Users_dt.Rows[i].Field<int>(0));
                        }
                    }
                }

                bind_InitialVisit(MicroProject_ID);
                clear_Initial_Visit_boxes();

                InsertVisit_button.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Get_Current_Initial_Visit()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select MAX(ID) from `visitinitial`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out VI_ID);

            Program.MyConn.Close();
        }

        private void Visits_DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 21)
                {
                    Image image = null;
                    if (Properties.Settings.Default.theme == "Light")
                        image = Properties.Resources.KAKA_Alii;
                    else image = Properties.Resources.KAKA_Alii_D;

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                    var size = image.Size;
                    var location = new Point((e.CellBounds.Width - size.Width) / 2, (e.CellBounds.Height - size.Height) / 2);
                    location.Offset(e.CellBounds.Location);
                    e.Graphics.DrawImage(image, location);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Visits_DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if click is on new row or header row
                if (e.RowIndex == Visits_DataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                //Check if click is on specific column 
                if (e.ColumnIndex == Visits_DataGridView.Columns["VI_DeleteRow"].Index)
                {
                    CheckUserPermission();
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this visit?", "Delete", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        VI_ID = Convert.ToInt32(Visits_DataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                        //check connection//
                        Program.buildConnection();
                        MySS.query = "delete from `visitinitial` where ID = " + VI_ID + " ";
                        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                        MySS.sc.ExecuteNonQuery();

                        Program.MyConn.Close();
                        Visits_DataGridView.Rows.RemoveAt(e.RowIndex);
                        clear_Initial_Visit_boxes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Visits_DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedDataRow = ((DataRowView)Visits_DataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                VI_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                fill_Initial_Visit_boxes(VI_ID);
                Update_Mood = true;
            }
        }
        private void Visits_DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)Visits_DataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                VI_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                fill_Initial_Visit_boxes(VI_ID);
                Update_Mood = true;
            }
        }

        private void InsertVisit_button_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.theme == "Light")
                InsertVisit_button.BackgroundImage = Properties.Resources.Save2_L;
            else
                InsertVisit_button.BackgroundImage = Properties.Resources.Save2_D;
        }
        private void InsertVisit_button_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.theme == "Light")
                InsertVisit_button.BackgroundImage = Properties.Resources.Save2_D;
            else
                InsertVisit_button.BackgroundImage = Properties.Resources.Save2_L;
        }

        #region textboxs why word
        private void StartTime_textBox_TextChanged(object sender, EventArgs e)
        {
            if (StartTime_textBox.Text == "")
            {
                StartTime_textBox.Text = "     لماذا؟";
            }
        }
        private void PurchaseItemsBudget_textBox_TextChanged(object sender, EventArgs e)
        {
            if (PurchaseItemsBudget_textBox.Text == "")
            {
                PurchaseItemsBudget_textBox.Text = "     لماذا؟";
            }
        }
        private void SamePlace_textBox_TextChanged(object sender, EventArgs e)
        {
            if (SamePlace_textBox.Text == "")
            {
                SamePlace_textBox.Text = "     لماذا؟";
            }
        }
        private void ItemsQuantityQuality_textBox_TextChanged(object sender, EventArgs e)
        {
            if (ItemsQuantityQuality_textBox.Text == "")
            {
                ItemsQuantityQuality_textBox.Text = "     لماذا؟";
            }
        }
        private void StartTime_textBox_Enter(object sender, EventArgs e)
        {
            StartTime_textBox.Clear();
        }
        private void PurchaseItemsBudget_textBox_Enter(object sender, EventArgs e)
        {
            PurchaseItemsBudget_textBox.Clear();
        }
        private void SamePlace_textBox_Enter(object sender, EventArgs e)
        {
            SamePlace_textBox.Clear();
        }
        private void ItemsQuantityQuality_textBox_Enter(object sender, EventArgs e)
        {
            ItemsQuantityQuality_textBox.Clear();
        }

        #endregion

        private void Reseption_textBox_TextChanged(object sender, EventArgs e)
        {
            if (InsertVisit_button.Enabled == false)
            { InsertVisit_button.Enabled = true; }
        }

        //private void LoanDateDay_comboBox_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    // Allow Combo Box to center aligned
        //    // By using Sender, one method could handle multiple ComboBoxes
        //    ComboBox cbx = sender as ComboBox;
        //    if (cbx != null)
        //    {
        //        // Always draw the background
        //        e.DrawBackground();

        //        // Drawing one of the items?
        //        if (e.Index >= 0)
        //        {
        //            // Set the string alignment.  Choices are Center, Near and Far
        //            StringFormat sf = new StringFormat();
        //            sf.LineAlignment = StringAlignment.Center;
        //            sf.Alignment = StringAlignment.Center;

        //            // Set the Brush to ComboBox ForeColor to maintain any ComboBox color settings
        //            // Assumes Brush is solid
        //            Brush brush = new SolidBrush(cbx.ForeColor);

        //            // If drawing highlighted selection, change brush
        //            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
        //                brush = SystemBrushes.HighlightText;

        //            // Draw the string
        //            e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
        //        }
        //    }
        //}

        public DataTable Get_Initial_Visit_Users(int VI_ID)
        {
            MySS.query = "SELECT `User_ID`,`UserName` FROM `visitinitial_user` join `user` on user.UserID = visitinitial_user.User_ID"
                + " WHERE `VisitInitial_ID` = " + VI_ID + "";
            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            return MySS.dt;
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
