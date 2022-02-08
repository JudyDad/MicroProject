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
    public partial class Add_Financial_Visit : Form
    {
        public Add_Financial_Visit()
        {
            InitializeComponent();
            this.SelectedDataRow = null;
            Update_Mood = false;
        }
        public Add_Financial_Visit(int VF_ID)
        {
            InitializeComponent();
            this.VF_ID = VF_ID;
            Update_Mood = true;
        }

     //   SelectPersonAndProject SelectPersonAndProject;
        DataRow SelectedDataRow;
        int MicroProject_ID, VF_ID, User_ID;
        MySqlComponents MySS;
        MyTheme myTheme;
        Log l;Select s;
        DataTable Visit_Users_dt;
        TasksOfProjects TasksOfProjects;
        bool Update_Mood;

        private void Add_Financial_Visit_Load(object sender, EventArgs e)
        {
            myTheme = new MyTheme();
            if (Properties.Settings.Default.theme == "Light")
            {
                myTheme.AddUpdate_ToLight(this.panel1);
            }
            else
            {
                myTheme.AddUpdate_ToNight(this.panel1);
            }
            MySS = new MySqlComponents();
            l = new Log(); s = new Select();
            users_bind();

            if (Update_Mood)
            {
                fill_Financial_Visit_boxes(VF_ID);
                bind_FinancialVisit(MicroProject_ID);
            }
            fill_beneficiary_box();
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
                        + " where visitfinancial.`MicroProject_ID` = " + MicroProject_ID ;
                MySqlCommand sc = new MySqlCommand(MySS.query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
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
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Visits_DataGridView.Columns.Add(col);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void users_bind()
        {
            try
            {
                Program.MyConn.Close();
                //check connection//
                Program.buildConnection();

                MySS.da = new MySqlDataAdapter("select UserID as 'U_ID',UserName as 'Visitors' from `user`", Program.MyConn);
                DataSet ds = new DataSet();
                MySS.da.Fill(ds);

                Users_listBox.DataSource = ds.Tables[0];
                Users_listBox.DisplayMember = "Visitors";
                Users_listBox.ValueMember = "U_ID";
                Users_listBox.SelectedIndex = -1;
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
                + VisitDate_dateTimePicker2.Value.Year.ToString() + "/"
                + VisitDate_dateTimePicker2.Value.Month.ToString() + "/"
                + VisitDate_dateTimePicker2.Value.Day.ToString() + "',N'"
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
        }
        private void delete_FinancialVisit_User(int VF_ID, int User_ID)
        {
            MySS.query = "Delete from `visitfinancial_user` where `VisitFinancial_ID`= " + VF_ID + " and `User_ID` = " + User_ID + " ";

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void update_FinancialVisit(int VF_ID)
        {
            MySS.query = " UPDATE `visitfinancial` SET " +
                "`Date`= '" + VisitDate_dateTimePicker2.Value.Year.ToString() + "/"
                              + VisitDate_dateTimePicker2.Value.Month.ToString() + "/"
                              + VisitDate_dateTimePicker2.Value.Day.ToString() + "'" +
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
        }
        private void clear_Financial_Visit_boxes()
        {
            Continuance_textBox.Text = LedgerComments_textBox.Text = ProfitRatio_textBox.Text = LoanAmount_textBox2.Text 
               = AverageSales_textBox.Text = AverageItemPrice_textBox.Text = Indicators_textBox.Text = "";

            LoanDate_dateTimePicker2.Value = DateTime.Now;
            VF_ID = -1;
            SelectedDataRow = null;
            Users_listBox.SelectedIndex = -1;
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
                    VisitDate_dateTimePicker2.Value = date1;

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
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    MySS.sc.ExecuteNonQuery();
                    MySS.da = new MySqlDataAdapter(MySS.sc);
                    MySS.dt = new DataTable();
                    MySS.da.Fill(MySS.dt);

                    Double dd = Convert.ToDouble(MySS.dt.Rows[0][0].ToString());
                    LoanAmount_textBox2.Text = dd.ToString();
                    DateTime date = (DateTime)MySS.dt.Rows[0][1];
                    LoanDate_dateTimePicker2.Value = date;

                    //fill visitors (select them)
                    Visit_Users_dt = Get_Financial_Visit_Users(VF_ID);
                    if (Visit_Users_dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)
                        {
                            // Find the string in ListBox2.
                            string user_Name = Visit_Users_dt.Rows[i].Field<string>(1);
                            int jjj = Users_listBox.FindString(user_Name);
                            
                            Users_listBox.SetSelected(jjj, true);
                            //   int index = Users_listBox.FindString(Visit_Users_dt.Rows[i].Field<string>(1));

                            // User_ID = MySS.dt.Rows[i].Field<int>(0);
                            //Users_listBox.SetSelected(index, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //private void Select_button_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SelectPersonAndProject = new SelectPersonAndProject();
        //        SelectedDataRow = SelectPersonAndProject.showSelectedMPRow();
        //        if (SelectedDataRow != null)
        //        {
        //            Person_Name_textBox2.Text = (string)SelectedDataRow["Beneficiary Name"];
        //            MicroProject_ID = (int)SelectedDataRow["MicroProject_ID"];
        //            MicroProject_ID_textBox2.Text = MicroProject_ID.ToString();
        //            DateTime date = (DateTime)SelectedDataRow["Loan Date"];
        //            LoanDate_dateTimePicker2.Value = date;
        //            Double amount = Convert.ToDouble(SelectedDataRow["Loan Amount"].ToString());
        //            LoanAmount_textBox2.Text = amount.ToString();

        //            //users_bind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        //Person_ID = -1;
        //        MicroProject_ID = -1;
        //    }
        //    finally
        //    {
        //        SelectPersonAndProject = null;
        //        SelectedDataRow = null;
        //    }
        //}
        
        private void InsertVisit_button_Click(object sender, EventArgs e)
        {
            try
            {
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

                    string User = string.Empty;
                    foreach (DataRowView selecteditemRow in Users_listBox.SelectedItems) //Find the Providers
                    {
                        User = selecteditemRow.Row["U_ID"].ToString();
                        User_ID = Int32.Parse(User);
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
                        string User = string.Empty;
                        foreach (DataRowView selecteditemRow in Users_listBox.SelectedItems) //Find the Providers
                        {
                            User = selecteditemRow.Row["U_ID"].ToString();
                            User_ID = Int32.Parse(User);
                            insert_FinancialVisit_User(VF_ID, User_ID);
                        }
                    }
                    else
                    {
                        //check if visitors changed ...!!!
                        foreach (DataRowView selecteditemRow in Users_listBox.SelectedItems) //search selected visitors
                        {
                            User_ID = Convert.ToInt32(selecteditemRow.Row["U_ID"].ToString());
                            for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)
                            {
                                if (Visit_Users_dt.Rows[i].Field<int>(0) == User_ID)
                                    found = true;
                                else
                                    found = false;
                            }
                            if (!found)
                                insert_FinancialVisit_User(VF_ID, User_ID);
                        }
                        //search if rows in database (users of this visit) are all selected ?
                        if (Visit_Users_dt.Rows.Count != 0)
                        {
                            for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)     //For users of this visit
                            {
                                for (int x = 0; x < Users_listBox.Items.Count; x++)     //search all items of listbox
                                {
                                    int index = Users_listBox.FindString(Visit_Users_dt.Rows[i].Field<string>(1));
                                    if (Users_listBox.GetSelected(index) == true)       //user in database and checked in listBox
                                        continue;
                                    else                                                //user in database and NOT checked in listBox
                                    {
                                        //remove this user from database
                                        delete_FinancialVisit_User(VF_ID, Visit_Users_dt.Rows[i].Field<int>(0));
                                    }
                                }
                            }
                        }
                    }
                    l.Insert_Log("Update visit to " + MicroProject_ID + " : " + Person_Name_textBox2.Text + " ", " Financial Visit ", Properties.Settings.Default.username, DateTime.Now);
                    bind_FinancialVisit(MicroProject_ID);
                    clear_Financial_Visit_boxes();
                }
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
        
        private void fill_beneficiary_box()
        {
            //check connection//
            Program.buildConnection();
            string query = "select CONCAT(P_FirstName, ' ', P_FatherName, ' ', P_LastName) as 'Beneficiary Name'"
                                 + " from `person` where IsProjectOwner like 'YES'";

            MySqlCommand mySqlCommand = new MySqlCommand(query, Program.MyConn);
            MySS.reader = mySqlCommand.ExecuteReader();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            while (MySS.reader.Read())
            {
                collection.Add(MySS.reader.GetString(0));
            }
            Person_Name_textBox2.AutoCompleteCustomSource = collection;
        }
        private void MicroProject_ID_textBox2_Leave(object sender, EventArgs e)
        {
            if (MicroProject_ID_textBox2.Text != "")
            {
                //check connection//
                Program.buildConnection();
                string query = "select PMP.MicroProject_ID as 'MicroProject_ID'" +
                    " ,CONCAT(P1.P_FirstName, ' ', P1.P_FatherName, ' ', P1.P_LastName) as 'Beneficiary Name'" +
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
                            LoanDate_dateTimePicker2.Value = date;
                        }
                        Program.MyConn.Close();
                        Visits_DataGridView.DataSource = null;
                        bind_FinancialVisit(MicroProject_ID);
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
                    " ,CONCAT(P1.P_FirstName, ' ', P1.P_FatherName, ' ', P1.P_LastName) as 'Beneficiary Name'" +
                    ",L.Loan_Amount as 'Loan Amount'"
                    + ",L.Loan_DateTaken as 'Loan Date'"
                    + " from person_microproject PMP left outer join person P1 on P1.P_ID = PMP.Person_ID "
                    + " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID"
                    + " left outer join loan L on MP.MP_ID = L.MicroProject_ID " +
                " WHERE CONCAT(TRIM(P1.P_FirstName), ' ', TRIM(P1.P_FatherName), ' ', TRIM(P1.P_LastName)) LIKE '%" + Person_Name_textBox2.Text + "%'";

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
                            LoanDate_dateTimePicker2.Value = date;
                        }
                        Program.MyConn.Close();
                        Visits_DataGridView.DataSource = null;
                        bind_FinancialVisit(MicroProject_ID);
                    }
                }
            }
        }

        private void Visits_DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == Visits_DataGridView.NewRowIndex || e.RowIndex < 0)
                return;

            //Check if click is on specific column 
            if (e.ColumnIndex == Visits_DataGridView.Columns["VF_DeleteRow"].Index)
            {
                int VF_ID = Convert.ToInt32(Visits_DataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                //check connection//
                Program.buildConnection();
                MySS.query = "delete from `visitfinancial` where ID = " + VF_ID + " ";
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();

                Visits_DataGridView.Rows.RemoveAt(e.RowIndex);
                clear_Financial_Visit_boxes();
            }
            
        }
        private void Visits_DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {//e.ColumnIndex == Visits_DataGridView.Columns["VI_DeleteRow"].Index && 
                if (e.RowIndex >= 0 && e.ColumnIndex == 12)
                {
                    var image = Properties.Resources.trash_16;
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
            SelectedDataRow = ((DataRowView)Visits_DataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                VF_ID = Int32.Parse(SelectedDataRow["ID"].ToString()); ;
                fill_Financial_Visit_boxes(VF_ID);
                Update_Mood = true;
            }
        }

        public void Get_Current_Financial_Visit()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select MAX(ID) from `visitfinancial`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out VF_ID);
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
