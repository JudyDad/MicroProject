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

namespace MyWorkApplication
{
    public partial class Financial_Visit_Form : Form
    {
        public Financial_Visit_Form(MainForm mainForm)
        {
            InitializeComponent();
            this.SelectedDataRow = null;
            Update_Mood = false;
            this.mainForm = mainForm;
        }
        public Financial_Visit_Form(int VF_ID, MainForm mainForm)
        {
            InitializeComponent();
            this.VF_ID = VF_ID;
            Update_Mood = true;
            this.mainForm = mainForm;
        }
        DataRow SelectedDataRow;
        int MicroProject_ID, VF_ID, User_ID;
        MySqlComponents MySS;
        NewTheme NewTheme;
        Log l; Select s;
        DataTable Visit_Users_dt;
        TasksOfProjects TasksOfProjects;
        bool Update_Mood;
        MainForm mainForm;

        private void Financial_Visit_Form_Load(object sender, EventArgs e)
        {
            try
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
                mainForm.TabName_label.Text = "Financial Visits";

                MySS = new MySqlComponents();
                l = new Log(); s = new Select();
                users_bind1(); users_bind2(); users_bind3(); users_bind4();


                Person_Name_textBox2.AutoCompleteCustomSource = s.select_beneficiaries("", "");
                MicroProject_ID_textBox2.AutoCompleteCustomSource = s.select_project_IDs("", "");

                if (Update_Mood)
                {
                    fill_Financial_Visit_boxes(VF_ID);
                    bind_FinancialVisit(MicroProject_ID);

                    mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                    mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bind_FinancialVisit(int MicroProject_ID)
        {
            try
            {

                //check connection//
                Program.buildConnection();
                MySS.query = "SELECT `ID`"
                        + ", visitfinancial.`MicroProject_ID`"
                        + ", CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                        + ", `Date`, `Type`, `Continuance`, `Ledger`, `LedgerReason`, `ProfitRatio`, `AverageSales`"
                        + ", `AveragePrice`, `Indicators` "
                        + " FROM visitfinancial left outer join person_microproject on visitfinancial.MicroProject_ID = person_microproject.MicroProject_ID "
                        + " join person P on P.P_ID = person_microproject.Person_ID "
                        + " where visitfinancial.`MicroProject_ID` = " + MicroProject_ID;
                MySqlCommand sc = new MySqlCommand(MySS.query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Program.MyConn.Close();

                Visits_DataGridView.Columns.Clear();
                Visits_DataGridView.ColumnHeadersVisible = false;
                Visits_DataGridView.DataSource = dt;
                Visits_DataGridView.ColumnHeadersVisible = true;

                DataGridViewColumn dgc1 = Visits_DataGridView.Columns["ID"];
                dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["Continuance"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["Ledger"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["LedgerReason"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["ProfitRatio"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["AverageSales"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["AveragePrice"]; dgc1.Visible = false;
                dgc1 = Visits_DataGridView.Columns["Indicators"]; dgc1.Visible = false;

                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.Name = "VF_DeleteRow";
                col.HeaderText = " ";
                col.FlatStyle = FlatStyle.Flat;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Visits_DataGridView.Columns.Add(col);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void users_bind1()
        {
            try
            {
                Program.MyConn.Close();
                //check connection//
                Program.buildConnection();

                MySS.da = new MySqlDataAdapter("select UserID as 'U_ID',UserName as 'Visitors' from `user`", Program.MyConn);
                DataSet ds = new DataSet();
                MySS.da.Fill(ds);
                
                //Users_listView.Items.Clear();
                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    ListViewItem item = new ListViewItem(row["Visitors"].ToString(), Users_listView.Groups[0]);
                //    item.SubItems.Add(row["U_ID"].ToString());
                //    Users_listView.Items.Add(item);
                //}
                V1_comboBox.DataSource = ds.Tables[0];
                V1_comboBox.DisplayMember = "Visitors";
                V1_comboBox.ValueMember = "U_ID";
                V1_comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void users_bind2()
        {
            try
            {
                Program.MyConn.Close();
                //check connection//
                Program.buildConnection();

                MySS.da = new MySqlDataAdapter("select UserID as 'U_ID',UserName as 'Visitors' from `user`", Program.MyConn);
                DataSet ds = new DataSet();
                MySS.da.Fill(ds);

                //Users_listView.Items.Clear();
                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    ListViewItem item = new ListViewItem(row["Visitors"].ToString(), Users_listView.Groups[0]);
                //    item.SubItems.Add(row["U_ID"].ToString());
                //    Users_listView.Items.Add(item);
                //}

                V2_comboBox.DataSource = ds.Tables[0];
                V2_comboBox.DisplayMember = "Visitors";
                V2_comboBox.ValueMember = "U_ID";
                V2_comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void users_bind3()
        {
            try
            {
                Program.MyConn.Close();
                //check connection//
                Program.buildConnection();

                MySS.da = new MySqlDataAdapter("select UserID as 'U_ID',UserName as 'Visitors' from `user`", Program.MyConn);
                DataSet ds = new DataSet();
                MySS.da.Fill(ds);

                //Users_listView.Items.Clear();
                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    ListViewItem item = new ListViewItem(row["Visitors"].ToString(), Users_listView.Groups[0]);
                //    item.SubItems.Add(row["U_ID"].ToString());
                //    Users_listView.Items.Add(item);
                //}
                V3_comboBox.DataSource = ds.Tables[0];
                V3_comboBox.DisplayMember = "Visitors";
                V3_comboBox.ValueMember = "U_ID";
                V3_comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void users_bind4()
        {
            try
            {
                Program.MyConn.Close();
                //check connection//
                Program.buildConnection();

                MySS.da = new MySqlDataAdapter("select UserID as 'U_ID',UserName as 'Visitors' from `user`", Program.MyConn);
                DataSet ds = new DataSet();
                MySS.da.Fill(ds);

                //Users_listView.Items.Clear();
                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    ListViewItem item = new ListViewItem(row["Visitors"].ToString(), Users_listView.Groups[0]);
                //    item.SubItems.Add(row["U_ID"].ToString());
                //    Users_listView.Items.Add(item);
                //}
                V4_comboBox.DataSource = ds.Tables[0];
                V4_comboBox.DisplayMember = "Visitors";
                V4_comboBox.ValueMember = "U_ID";
                V4_comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void insert_FinancialVisit()
        {
            MySS.query = "INSERT INTO `visitfinancial`(`Date`, `Type`, `Continuance`, `Ledger`, `LedgerReason`, `ProfitRatio`,"
                        + "`AverageSales`, `AveragePrice`, `Indicators`, `MicroProject_ID`) values(N'"
                + VisitDate_dateTimePicker.Value.Year.ToString() + "/"
                + VisitDate_dateTimePicker.Value.Month.ToString() + "/"
                + VisitDate_dateTimePicker.Value.Day.ToString() + "',N'"
                + VisitType_comboBox2.Text + "',N'"

                + Continuance_textBox.Text + "',"
                + (YesLedger_radioButton.Checked ? 1 : 0) + ",N'"
                + LedgerComments_textBox.Text + "',N'"
                + ProfitRatio_textBox.Text + "',N'"
                + AverageSales_textBox.Text + "',N'"
                + AverageItemPrice_textBox.Text + "',N'"
                + Indicators_textBox.Text + "',"

                + MicroProject_ID + " )";

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }
        private void insert_FinancialVisit_User(int VF_ID, int User_ID)
        {
            MySS.query = "INSERT  INTO `visitfinancial_user`(`VisitFinancial_ID`, `User_ID`) VALUES("
               + VF_ID + ","
               + User_ID + " )";

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }
        private void delete_FinancialVisit_User(int VF_ID, int User_ID)
        {
            MySS.query = "Delete from `visitfinancial_user` where `VisitFinancial_ID`= " + VF_ID + " and `User_ID` = " + User_ID + " ";

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }
        private void update_FinancialVisit(int VF_ID)
        {
            MySS.query = " UPDATE `visitfinancial` SET " +
                "`Date`= '" + VisitDate_dateTimePicker.Value.Year.ToString() + "/"
                              + VisitDate_dateTimePicker.Value.Month.ToString() + "/"
                              + VisitDate_dateTimePicker.Value.Day.ToString() + "'" +
                ",`Type`=N'" + VisitType_comboBox2.Text + "'" +
                ",`Continuance`=N'" + Continuance_textBox.Text + "'" +
                ",`Ledger`=" + (YesLedger_radioButton.Checked ? 1 : 0) + "" +
                ",`LedgerReason`=N'" + LedgerComments_textBox.Text + "'" +
                ",`ProfitRatio`=N'" + ProfitRatio_textBox.Text + "'" +
                ",`AverageSales`=N'" + AverageSales_textBox.Text + "'" +
                ",`AveragePrice`=N'" + AverageItemPrice_textBox.Text + "'" +
                ",`Indicators`=N'" + Indicators_textBox.Text + "'" +
                ",`MicroProject_ID`=" + MicroProject_ID + "" +
                " WHERE ID = " + VF_ID + "";
            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }
        private void clear_Financial_Visit_boxes()
        {
            Continuance_textBox.Text = LedgerComments_textBox.Text = ProfitRatio_textBox.Text = LoanAmount_textBox2.Text
               = AverageSales_textBox.Text = AverageItemPrice_textBox.Text = Indicators_textBox.Text = "";

            LoanDate_dateTimePicker.Value = DateTime.Now;
            VF_ID = -1;
            SelectedDataRow = null;
            //clear checkboxs of listbox
            //foreach (ListViewItem item in Users_listView.CheckedItems)
            //{
            //    item.Checked = false;
            //}
            V1_comboBox.Text = V2_comboBox.Text = V3_comboBox.Text = V4_comboBox.Text = "";
            V1_comboBox.SelectedIndex = V2_comboBox.SelectedIndex = V3_comboBox.SelectedIndex = V4_comboBox.SelectedIndex = -1;
            Update_Mood = false;
        }
        private void fill_Financial_Visit_boxes(int VF_ID)
        {
            try
            {
                DataTable dt = s.FinancialVisit_bind(VF_ID);
                if (dt != null)
                {
                    MicroProject_ID = Int32.Parse(dt.Rows[0]["MicroProject_ID"].ToString());
                    VF_ID = Int32.Parse(dt.Rows[0]["ID"].ToString());

                    VisitType_comboBox2.Text = (string)dt.Rows[0]["Type"];
                    DateTime date1 = (DateTime)dt.Rows[0]["Date"];
                    VisitDate_dateTimePicker.Value = date1;

                    Continuance_textBox.Text = (string)dt.Rows[0]["Continuance"];
                    //Ledger
                    int Ledger = Int32.Parse(dt.Rows[0]["Ledger"].ToString());
                    if (Ledger == 0)
                        NoLedger_radioButton.Checked = true;
                    else
                        YesLedger_radioButton.Checked = true;

                    LedgerComments_textBox.Text = (string)dt.Rows[0]["LedgerReason"];
                    ProfitRatio_textBox.Text = (string)dt.Rows[0]["ProfitRatio"];
                    AverageSales_textBox.Text = (string)dt.Rows[0]["AverageSales"];
                    AverageItemPrice_textBox.Text = (string)dt.Rows[0]["AveragePrice"];
                    Indicators_textBox.Text = (string)dt.Rows[0]["Indicators"];

                    MicroProject_ID_textBox2.Text = MicroProject_ID.ToString();
                    Person_Name_textBox2.Text = (string)dt.Rows[0]["Beneficiary Name"];
                    MySS.query = "select Loan_Amount as 'Loan Amount'"
                                    + ",Loan_DateTaken as 'Loan Date'"
                                    + " from loan where MicroProject_ID = " + MicroProject_ID + " ";
                    //check connection//
                    Program.buildConnection();
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    MySS.sc.ExecuteNonQuery();
                    MySS.da = new MySqlDataAdapter(MySS.sc);
                    MySS.dt = new DataTable();
                    MySS.da.Fill(MySS.dt);
                    Program.MyConn.Close();

                    Double dd = Convert.ToDouble(MySS.dt.Rows[0][0].ToString());
                    LoanAmount_textBox2.Text = dd.ToString();
                    DateTime date = (DateTime)MySS.dt.Rows[0][1];
                    LoanDate_dateTimePicker.Value = date;

                    V1_comboBox.Text = V2_comboBox.Text = V3_comboBox.Text = V4_comboBox.Text = "";
                    V1_comboBox.SelectedIndex = V2_comboBox.SelectedIndex = V3_comboBox.SelectedIndex = V4_comboBox.SelectedIndex = -1;
                    //fill visitors (select them)
                    Visit_Users_dt = Get_Financial_Visit_Users(VF_ID);
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                if (MicroProject_ID_textBox2.Text == "" || Person_Name_textBox2.Text == "" || MicroProject_ID == -1)
                {
                    throw new Exception("You should choose a project first ..!!");
                }
                if (VisitType_comboBox2.Text == "")
                {
                    throw new Exception("You can't leave empty fields ..!!");
                }
                ///////// insert /////////////
                if (SelectedDataRow == null || Update_Mood == false)
                {
                    insert_FinancialVisit();

                    ////////////// GET current V_ID //////////////////
                    Get_Current_Financial_Visit();

                    //string User = string.Empty;
                    //foreach (DataRowView selecteditemRow in Users_listBox.SelectedItems) //Find the Providers
                    //{
                    //    User = selecteditemRow.Row["U_ID"].ToString();
                    //    User_ID = Int32.Parse(User);
                    //    insert_FinancialVisit_User(VF_ID, User_ID);
                    //}

                    //string User = string.Empty;
                    //foreach (ListViewItem item in Users_listView.CheckedItems)
                    //{
                    //    User = item.SubItems[1].Text;
                    //    User_ID = Int32.Parse(User);
                    //    insert_FinancialVisit_User(VF_ID, User_ID);
                    //}

                    //check user comboBoxs
                    if (V1_comboBox.Text != "")
                    {
                        User_ID = Convert.ToInt32(V1_comboBox.SelectedValue);
                        insert_FinancialVisit_User(VF_ID, User_ID);
                    }
                    if (V2_comboBox.Text != "")
                    {
                        User_ID = Convert.ToInt32(V2_comboBox.SelectedValue);
                        insert_FinancialVisit_User(VF_ID, User_ID);
                    }
                    if (V3_comboBox.Text != "")
                    {
                        User_ID = Convert.ToInt32(V3_comboBox.SelectedValue);
                        insert_FinancialVisit_User(VF_ID, User_ID);
                    }
                    if (V4_comboBox.Text != "")
                    {
                        User_ID = Convert.ToInt32(V4_comboBox.SelectedValue);
                        insert_FinancialVisit_User(VF_ID, User_ID);
                    }

                    l.Insert_Log("Insert visit to " + MicroProject_ID + " : " + Person_Name_textBox2.Text + " ", " Financial Visit ", Properties.Settings.Default.username, DateTime.Now);

                    //make the initial visit task of this project ==> checked
                    //update task ^_^
                    ///////////////////////////   Task IDs = 24 ? 28 ? 29 ?   ///////////////////////////////////////
                    TasksOfProjects = new TasksOfProjects();

                    //   task 24 if not checked ? ==> we are in the first visit or move to task 28 !!!!!!
                    bool state = false;
                    state = TasksOfProjects.Get_Task_MicroProject_State(MicroProject_ID, 24);
                    if (!state)
                        TasksOfProjects.Update_Task_MicroProject(MicroProject_ID, 24, true, DateTime.Now);
                    else
                    {
                        state = TasksOfProjects.Get_Task_MicroProject_State(MicroProject_ID, 28);
                        if (!state)
                            TasksOfProjects.Update_Task_MicroProject(MicroProject_ID, 28, true, DateTime.Now);
                        else
                        {
                            state = TasksOfProjects.Get_Task_MicroProject_State(MicroProject_ID, 29);
                            if (!state)
                                TasksOfProjects.Update_Task_MicroProject(MicroProject_ID, 29, true, DateTime.Now);
                        }
                    }
                    bind_FinancialVisit(MicroProject_ID);
                    clear_Financial_Visit_boxes();
                }
                else   ///////// update /////////////
                {
                    update_FinancialVisit(VF_ID);

                    //if visit doesn't have visitors...!!!
                    Visit_Users_dt = Get_Financial_Visit_Users(VF_ID);
                    bool found = false;
                    if (Visit_Users_dt.Rows.Count == 0)
                    {
                        // check comboBoxs and insert all //
                        if (V1_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V1_comboBox.SelectedValue);
                            insert_FinancialVisit_User(VF_ID, User_ID);
                        }
                        if (V2_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V2_comboBox.SelectedValue);
                            insert_FinancialVisit_User(VF_ID, User_ID);
                        }
                        if (V3_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V3_comboBox.SelectedValue);
                            insert_FinancialVisit_User(VF_ID, User_ID);
                        }
                        if (V4_comboBox.Text != "")
                        {
                            User_ID = Convert.ToInt32(V4_comboBox.SelectedValue);
                            insert_FinancialVisit_User(VF_ID, User_ID);
                        }
                        
                        //string User = string.Empty;
                        //foreach (DataRowView selecteditemRow in Users_listBox.SelectedItems) //Find the Providers
                        //{
                        //    User = selecteditemRow.Row["U_ID"].ToString();
                        //    User_ID = Int32.Parse(User);
                        //    insert_FinancialVisit_User(VF_ID, User_ID);
                        //}
                        //string User = string.Empty;
                        //foreach (ListViewItem item in Users_listView.CheckedItems)
                        //{
                        //    User = item.SubItems[1].Text;
                        //    User_ID = Int32.Parse(User);
                        //    insert_FinancialVisit_User(VF_ID, User_ID);
                        //}
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
                                insert_FinancialVisit_User(VF_ID, User_ID);
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
                                insert_FinancialVisit_User(VF_ID, User_ID);
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
                                insert_FinancialVisit_User(VF_ID, User_ID);
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
                                insert_FinancialVisit_User(VF_ID, User_ID);
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
                            delete_FinancialVisit_User(VF_ID, Visit_Users_dt.Rows[i].Field<int>(0));
                        }
                    }
                    l.Insert_Log("Update visit to " + MicroProject_ID + " : " + Person_Name_textBox2.Text + " ", " Financial Visit ", Properties.Settings.Default.username, DateTime.Now);

                }
                bind_FinancialVisit(MicroProject_ID);
                clear_Financial_Visit_boxes();
                InsertVisit_button.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable Get_Financial_Visit_Users(int VF_ID)
        {
            MySS.query = "SELECT `User_ID`,`UserName` FROM `visitfinancial_user` join `user` on user.UserID = visitfinancial_user.User_ID"
                + " WHERE `VisitFinancial_ID` = " + VF_ID + "";
            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            return MySS.dt;
        }
       

        private void MicroProject_ID_textBox2_Leave(object sender, EventArgs e)
        {
            if (MicroProject_ID_textBox2.Text != "")
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
                " where PMP.MicroProject_ID = " + Convert.ToInt32(MicroProject_ID_textBox2.Text);

                MySS.sc = new MySqlCommand(query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);

                if (MySS.dt != null)
                {
                    if (MySS.dt.Rows.Count > 0)
                    {
                        MicroProject_ID = Int32.Parse(MySS.dt.Rows[0]["MicroProject_ID"].ToString());
                        Person_Name_textBox2.Text = MySS.dt.Rows[0]["Beneficiary Name"].ToString();

                        if (MySS.dt.Rows[0]["Loan Amount"] != DBNull.Value)
                        {
                            Double dd = Convert.ToDouble(MySS.dt.Rows[0]["Loan Amount"].ToString());
                            LoanAmount_textBox2.Text = dd.ToString();
                        }
                        if (MySS.dt.Rows[0]["Loan Date"] != DBNull.Value)
                        {
                            DateTime date = (DateTime)MySS.dt.Rows[0]["Loan Date"];
                            LoanDate_dateTimePicker.Value = date;
                        }
                        Program.MyConn.Close();
                        Visits_DataGridView.DataSource = null;
                        bind_FinancialVisit(MicroProject_ID);
                        mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                        mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
                    }
                }
            }
        }
        private void Person_Name_textBox2_Leave(object sender, EventArgs e)
        {
            if (Person_Name_textBox2.Text != "")
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
    " WHERE CONCAT(TRIM(P1.P_FirstName), ' ', TRIM(P1.P_LastName), ' ابن/ة ', TRIM(P1.P_FatherName)) LIKE '%" + Person_Name_textBox2.Text + "%'";

                MySS.sc = new MySqlCommand(query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);
                if (MySS.dt != null)
                {
                    if (MySS.dt.Rows.Count > 0)
                    {
                        MicroProject_ID = Int32.Parse(MySS.dt.Rows[0]["MicroProject_ID"].ToString());
                        MicroProject_ID_textBox2.Text = MicroProject_ID.ToString();
                        Person_Name_textBox2.Text = MySS.dt.Rows[0]["Beneficiary Name"].ToString();

                        if (MySS.dt.Rows[0]["Loan Amount"] != DBNull.Value)
                        {
                            Double dd = Convert.ToDouble(MySS.dt.Rows[0]["Loan Amount"].ToString());
                            LoanAmount_textBox2.Text = dd.ToString();
                        }
                        if (MySS.dt.Rows[0]["Loan Date"] != DBNull.Value)
                        {
                            DateTime date = (DateTime)MySS.dt.Rows[0]["Loan Date"];
                            LoanDate_dateTimePicker.Value = date;
                        }
                        Program.MyConn.Close();
                        Visits_DataGridView.DataSource = null;
                        bind_FinancialVisit(MicroProject_ID);

                        mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                        mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
                    }
                }
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
                if (e.ColumnIndex == Visits_DataGridView.Columns["VF_DeleteRow"].Index)
                {
                    CheckUserPermission();
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the Project?", "Delete", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        int VF_ID = Convert.ToInt32(Visits_DataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                        //check connection//
                        Program.buildConnection();
                        MySS.query = "delete from `visitfinancial` where ID = " + VF_ID + " ";
                        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                        MySS.sc.ExecuteNonQuery();
                        Program.MyConn.Close();

                        Visits_DataGridView.Rows.RemoveAt(e.RowIndex);
                        clear_Financial_Visit_boxes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Visits_DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 12)
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
        private void Visits_DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Visits_DataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    VF_ID = Int32.Parse(SelectedDataRow["ID"].ToString()); ;
                    fill_Financial_Visit_boxes(VF_ID);
                    Update_Mood = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Visits_DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Visits_DataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    VF_ID = Int32.Parse(SelectedDataRow["ID"].ToString()); ;
                    fill_Financial_Visit_boxes(VF_ID);
                    Update_Mood = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Get_Current_Financial_Visit()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select MAX(ID) from `visitfinancial`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out VF_ID);

            Program.MyConn.Close();
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void V1_comboBox_TextChanged(object sender, EventArgs e)
        {
            if (InsertVisit_button.Enabled == false)
            { InsertVisit_button.Enabled = true; }
        }
    }
}
