using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWorkApplication
{
    public partial class Monitoring : Form
    {
        public Monitoring()
        {
            InitializeComponent();
        }
        public Monitoring(string username)
        {
            InitializeComponent();
            this.username = username;
            tabControl.SelectedIndex = 0;
            MySS = new MySqlComponents();
            clear_Financial_Visit_boxes();
            clear_Initial_Visit_boxes();
            InsertVisit_button.Visible = true;
            UpdateVisit_button.Visible = false;
        }
        SelectPersonAndProject SelectPersonAndProject;
        DataRow SelectedDataRow;
        int MicroProject_ID, VI_ID, VF_ID, User_ID;
        MySqlComponents MySS;
        MyTheme myTheme;
        Log l;
        string username;

        private void user_bind()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select u.UserID as 'UserID',u.UserName as 'Visitors' from `user` u ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Users_DataGridView1.DataSource = Users_DataGridView2.DataSource =  MySS.dt;

            DataGridViewColumn dgC1 = Users_DataGridView1.Columns["UserID"];
            DataGridViewColumn dgC2 = Users_DataGridView2.Columns["UserID"];
            dgC1.Visible = dgC2.Visible = false;
        }
        private void users_bind()
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.da = new MySqlDataAdapter("select u.UserID as 'U_ID',u.UserName as 'Username' as 'Visitors' from `user` u", Program.MyConn);
                DataSet ds = new DataSet();
                MySS.da.Fill(ds);

                Users_listBox.DataSource = ds.Tables[0];
                Users_listBox.DisplayMember = "Username";
                Users_listBox.ValueMember = "U_ID";
                Users_listBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region insert
        private void insert_InitialVisit()
        {
            MySS.query = "INSERT INTO `visitinitial`(`Date`, `Type`, `Reseption`, `Waiting`, `Visit`, `Lawyer`, `Purchasing`, `Photography`, `OtherComments`,"
                        + "`StartInTime`, `StartInTimeReason`, `PurchaseAllItemsOfBudget`, `PurchaseAllItemsOfBudgetReason`, `SamePlace`, `SamePlaceReason`,"
                        + "`SameQualityAndQuantity`, `SameQualityAndQuantityReason`, `Marketing`, `MicroProject_ID`) values(N'"
                + VisitDate_dateTimePicker1.Value.Year.ToString() + "/"
                + VisitDate_dateTimePicker1.Value.Month.ToString() + "/"
                + VisitDate_dateTimePicker1.Value.Day.ToString() + "',N'"
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

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void insert_FinantialVisit()
        {
            MySS.query = "INSERT INTO `visitfinantial`(`Date`, `Type`, `Continuance`, `Ledger`, `LedgerReason`, `ProfitRatio`,"
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
        private void insert_InitialVisit_User(int VI_ID, int User_ID)
        {
            MySS.query = "INSERT `visitinitial_user`(`ID`, `VisitInitial_ID`, `User_ID`) values(N'"
                //
                //
                //
                
               + " )";

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void insert_FinancialVisit_User(int VF_ID,int User_ID)
        {
            MySS.query = "INSERT `visitfinantial_user`(`ID`, `VisitFinantial_ID`, `User_ID`) values(N'"
               //
               //
               //
               + " )";

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        #endregion

        #region update
        private void update_InitialVisit(int VI_ID)
        {
            MySS.query = " UPDATE `visitinitial` SET " +
                "`Date`= '" + VisitDate_dateTimePicker1.Value.Year.ToString() + " / "
                              + VisitDate_dateTimePicker1.Value.Month.ToString() + "/"
                              + VisitDate_dateTimePicker1.Value.Day.ToString() + "'" +
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
        }
        private void update_FinantialVisit(int VF_ID)
        {
            MySS.query = " UPDATE `visitfinantial` SET " +
                "`Date`= '" + VisitDate_dateTimePicker2.Value.Year.ToString() + " / "
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
        #endregion

        #region clear and fill boxes
        private void clear_Initial_Visit_boxes()
        {
            Reseption_textBox.Text = Waiting_textBox.Text = Visit_textBox.Text = Lawyer_textBox.Text =
               Purchasing_textBox.Text = Photography_textBox.Text =
               OtherComments_textBox.Text = StartTime_textBox.Text = PurchaseItemsBudget_textBox.Text = "";
            SamePlace_textBox.Text = ItemsQuantityQuality_textBox.Text = Marketing_textBox.Text= "";

            InsertVisit_button.Visible = true;
            UpdateVisit_button.Visible = false;

            MicroProject_ID_textBox1.Text = Person_Name_textBox1.Text = LoanAmount_textBox1.Text = "";
            LoanDate_dateTimePicker1.Value = DateTime.Now;

            MicroProject_ID_textBox2.Text = Person_Name_textBox2.Text = LoanAmount_textBox2.Text = "";
            LoanDate_dateTimePicker2.Value = DateTime.Now;

            MicroProject_ID = -1;
        }
        private void clear_Financial_Visit_boxes()
        {
            Continuance_textBox.Text = LedgerComments_textBox.Text = ProfitRatio_textBox.Text
               = AverageSales_textBox.Text = AverageItemPrice_textBox.Text = Indicators_textBox.Text = "";

            InsertVisit_button.Visible = true;
            UpdateVisit_button.Visible = false;

            MicroProject_ID_textBox1.Text = Person_Name_textBox1.Text = LoanAmount_textBox1.Text = "";
            LoanDate_dateTimePicker1.Value = DateTime.Now;

            MicroProject_ID_textBox2.Text = Person_Name_textBox2.Text = LoanAmount_textBox2.Text = "";
            LoanDate_dateTimePicker2.Value = DateTime.Now;

            MicroProject_ID = -1;
        }

        private void fill_Initial_Visit_boxes(DataRow dt)
        {
            if (dt != null)
            {
                MicroProject_ID = Int32.Parse(dt["MicroProject_ID"].ToString());
                VI_ID = Int32.Parse(dt["ID"].ToString());

                VisitType_comboBox1.Text = (string)dt["Type"];
                DateTime date1 = (DateTime)dt["Date"];
                VisitDate_dateTimePicker1.Value = date1;

                Reseption_textBox.Text = (string)dt["Reseption"];
                Waiting_textBox.Text = (string)dt["Waiting"];
                Visit_textBox.Text = (string)dt["Visit"];
                Lawyer_textBox.Text = (string)dt["Lawyer"];
                Purchasing_textBox.Text = (string)dt["Purchasing"];
                Photography_textBox.Text = (string)dt["Photography"];
                OtherComments_textBox.Text = (string)dt["OtherComments"];
                //StartInTime
                int StartInTime = Int32.Parse(dt["StartInTime"].ToString());
                if (StartInTime == 0)
                    StartTime_checkBox.Checked = false;
                else
                    StartTime_checkBox.Checked = true;
                StartTime_textBox.Text = (string)dt["StartInTimeReason"];
                //PurchaseAllItemsOfBudget
                int PurchaseItemsBudget = Int32.Parse(dt["PurchaseAllItemsOfBudget"].ToString());
                if (PurchaseItemsBudget == 0)
                    PurchaseItemsBudget_checkBox.Checked = false;
                else
                    PurchaseItemsBudget_checkBox.Checked = true;
                PurchaseItemsBudget_textBox.Text = (string)dt["PurchaseAllItemsOfBudgetReason"];
                //SamePlace
                int SamePlace = Int32.Parse(dt["SamePlace"].ToString());
                if (SamePlace == 0)
                    SamePlace_checkBox.Checked = false;
                else
                    SamePlace_checkBox.Checked = true;
                SamePlace_textBox.Text = (string)dt["SamePlaceReason"];
                //SameQualityAndQuantity
                int ItemsQuantityQuality = Int32.Parse(dt["SameQualityAndQuantity"].ToString());
                if (ItemsQuantityQuality == 0)
                    ItemsQuantityQuality_checkBox.Checked = false;
                else
                    ItemsQuantityQuality_checkBox.Checked = true;
                ItemsQuantityQuality_textBox.Text = (string)dt["SameQualityAndQuantityReason"];

                MicroProject_ID_textBox1.Text = MicroProject_ID.ToString();
                    Person_Name_textBox1.Text = (string)dt["Beneficiary Name"];
                MySS.query = "select Loan_Amount as 'Loan Amount'"
                                + ",Loan_DateTaken as 'Loan Date'"
                                + " from loan where MicroProject_ID = " + MicroProject_ID + " ";
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);

                Double dd = Convert.ToDouble(MySS.dt.Rows[0][0].ToString());
                LoanAmount_textBox1.Text = dd.ToString();
                DateTime date = (DateTime)MySS.dt.Rows[0][1];
                LoanDate_dateTimePicker1.Value = date;
            }
        }
        private void fill_Finantial_Visit_boxes(DataRow dt)
        {
            if (dt != null)
            {
                MicroProject_ID = Int32.Parse(dt["MicroProject_ID"].ToString());
                VF_ID = Int32.Parse(dt["ID"].ToString());

                VisitType_comboBox2.Text = (string)dt["Type"];
                DateTime date1 = (DateTime)dt["Date"];
                VisitDate_dateTimePicker2.Value = date1;

                Continuance_textBox.Text = (string)dt["Continuance"];
                //Ledger
                int Ledger = Int32.Parse(dt["Ledger"].ToString());
                if (Ledger == 0)
                    NoLedger_radioButton.Checked = true;
                else
                    YesLedger_radioButton.Checked = true;

                LedgerComments_textBox.Text = (string)dt["LedgerReason"];
                ProfitRatio_textBox.Text = (string)dt["ProfitRatio"];
                AverageSales_textBox.Text = (string)dt["AverageSales"];
                AverageItemPrice_textBox.Text = (string)dt["AveragePrice"];
                Indicators_textBox.Text = (string)dt["Indicators"];
              
                MicroProject_ID_textBox2.Text = MicroProject_ID.ToString();
                Person_Name_textBox2.Text = (string)dt["Beneficiary Name"];
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
            }
        }
        #endregion

        private void InitialVisit_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }
        private void FinancialVisit_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void AllVisites_button_Click(object sender, EventArgs e)
        {
            using (AllVisits allVisits = new AllVisits(username))
            {
                if (allVisits.ShowDialog() == DialogResult.OK)
                {
                    if (allVisits.Visit_Type == 1)
                    {
                        fill_Initial_Visit_boxes(allVisits.SelectedDataRow);
                        tabControl.SelectedIndex = 0;
                    }
                    else
                    {
                        fill_Finantial_Visit_boxes(allVisits.SelectedDataRow);
                        tabControl.SelectedIndex = 1;
                    }
                    UpdateVisit_button.Visible = true;
                    InsertVisit_button.Visible = false;
                }
                else
                {
                    if (allVisits.Visit_Type == 1)
                        clear_Initial_Visit_boxes();
                    else
                        clear_Financial_Visit_boxes();

                    UpdateVisit_button.Visible = false;
                    InsertVisit_button.Visible = true;
                }
            }
        }
        private void Select_button_Click(object sender, EventArgs e)
        {
            try
            {
                SelectPersonAndProject = new SelectPersonAndProject();
                SelectedDataRow = SelectPersonAndProject.showSelectedMPRow();
                if (SelectedDataRow != null)
                {
                    Person_Name_textBox1.Text = Person_Name_textBox2.Text = (string)SelectedDataRow["Beneficiary Name"];
                    MicroProject_ID = (int)SelectedDataRow["MicroProject_ID"];
                    MicroProject_ID_textBox1.Text = MicroProject_ID_textBox2.Text = MicroProject_ID.ToString();
                    DateTime date = (DateTime)SelectedDataRow["Loan Date"];
                    LoanDate_dateTimePicker1.Value = LoanDate_dateTimePicker2.Value = date;
                    Double amount = Convert.ToDouble(SelectedDataRow["Loan Amount"].ToString());
                    LoanAmount_textBox1.Text = LoanAmount_textBox2.Text = amount.ToString() ;

                    user_bind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MicroProject_ID = -1;
            }
            finally
            {
                SelectPersonAndProject = null;
                SelectedDataRow = null;
            }
        }

        private void Monitoring_Load(object sender, EventArgs e)
        {
            myTheme = new MyTheme();
            if (Properties.Settings.Default.theme == "Light")
            {
                myTheme.Application_ToLight(this.tabControl);
            }
            else
            {
                myTheme.Application_ToNight(this.tabControl);
            }
            MySS = new MySqlComponents();
            l = new Log();
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateVisit_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedIndex == 0)     // initial visit //
                {
                    if (MicroProject_ID_textBox1.Text == "" || Person_Name_textBox1.Text == "" || VI_ID == -1)
                    {
                        throw new Exception("You should choose a project first ..!!");
                    }
                    if (VisitType_comboBox1.Text == "")
                    {
                        throw new Exception("You can't leave empty fields ..!!");
                    }
                    update_InitialVisit(VI_ID);
                    l.Insert_Log("Update visit to " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " ", " Initial Visit ", username, DateTime.Now);
                    clear_Initial_Visit_boxes();
                }
                else                                //// finantial visit
                {
                    if (MicroProject_ID_textBox2.Text == "" || Person_Name_textBox2.Text == "" || VF_ID == -1)
                    {
                        throw new Exception("You should choose a project first ..!!");
                    }
                    if (VisitType_comboBox2.Text == "")
                    {
                        throw new Exception("You can't leave empty fields ..!!");
                    }
                    update_FinantialVisit(VF_ID);
                    l.Insert_Log("Update visit to " + MicroProject_ID + " : " + Person_Name_textBox2.Text + " ", " Finantial Visit ", username, DateTime.Now);
                    clear_Financial_Visit_boxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InsertVisit_button_Click(object sender, EventArgs e)
        {
            try {
                if(tabControl.SelectedIndex==0)     // initial visit //
                {
                    if (MicroProject_ID_textBox1.Text == "" || Person_Name_textBox1.Text == "" || MicroProject_ID==-1)
                    {
                        throw new Exception("You should choose a project first ..!!");
                    }
                    if(VisitType_comboBox1.Text == "")
                    {
                        throw new Exception("You can't leave empty fields ..!!");
                    }
                    insert_InitialVisit();
                    l.Insert_Log("Insert visit to " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " " , " Initial Visit ", username, DateTime.Now);

                    ////////////// GET current V_ID //////////////////

                    string User = string.Empty;
                    foreach (DataRowView selecteditemRow in Users_listBox.SelectedItems) //Find the Providers
                    {
                        User = selecteditemRow.Row["U_ID"].ToString();
                        User_ID = Int32.Parse(User);
                   //     insert_InitialVisit_User(VI_ID,User_ID,"");
                    }

                    clear_Initial_Visit_boxes();
                }
                else                                //// finantial visit
                {
                    if (MicroProject_ID_textBox2.Text == "" || Person_Name_textBox2.Text == "" || MicroProject_ID == -1)
                    {
                        throw new Exception("You should choose a project first ..!!");
                    }
                    if (VisitType_comboBox2.Text == "")
                    {
                        throw new Exception("You can't leave empty fields ..!!");
                    }
                    insert_FinantialVisit();
                    l.Insert_Log("Insert visit to " + MicroProject_ID + " : " + Person_Name_textBox2.Text + " ", " Finantial Visit ", username, DateTime.Now);

                    clear_Financial_Visit_boxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
