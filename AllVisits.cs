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
    public partial class AllVisits : Form
    {
        public AllVisits()
        {
            InitializeComponent();
        }
        public AllVisits(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        MyTheme myTheme;
        MySqlComponents MySS;
        Log l;
        int MicroProject_ID, VI_ID,VF_ID;
        string username;
        string Beneficiary_Name;
        int[] V_IDs;
        public DataRow SelectedDataRow;
        public int Visit_Type;

        private void bind_InitialVisit()
        {
            try { 
                MySS.query = "SELECT `ID`"
                        + ", visitinitial.`MicroProject_ID`"
                        + ", CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                        + ", `Date`, `Type`, `Reseption`, `Waiting`, `Visit`, `Lawyer`, `Purchasing`, `Photography`, `OtherComments`"
                        + ", `StartInTime`, `StartInTimeReason`, `PurchaseAllItemsOfBudget`, `PurchaseAllItemsOfBudgetReason`"
                        + ", `SamePlace`, `SamePlaceReason`, `SameQualityAndQuantity`, `SameQualityAndQuantityReason`, `Marketing` "
                        + " FROM visitinitial left outer join person_microproject on visitinitial.MicroProject_ID = person_microproject.MicroProject_ID "
                        + " join person P on P.P_ID = person_microproject.Person_ID ";

                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);

                Visits_DataGridView.ColumnHeadersVisible = false;
                Visits_DataGridView.DataSource = MySS.dt;
                Visits_DataGridView.ColumnHeadersVisible = true;

                DataGridViewColumn dgc1 = Visits_DataGridView.Columns["ID"];
                dgc1.Visible = false;

                //count rows
                string sel = "select count(*) from `visitinitial` as count";
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Counter_textBox.Text = MySS.sc.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bind_FinancialVisit()
        {
            try
            {
                MySS.query = "SELECT `ID`"
                        + ", visitfinancial.`MicroProject_ID`"
                        + ", CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                        + ", `Date`, `Type`, `Continuance`, `Ledger`, `LedgerReason`, `ProfitRatio`, `AverageSales`"
                        + ", `AveragePrice`, `Indicators` "
                        + " FROM visitfinancial left outer join person_microproject on visitfinancial.MicroProject_ID = person_microproject.MicroProject_ID "
                        + " join person P on P.P_ID = person_microproject.Person_ID ";

                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);

                Visits_DataGridView.ColumnHeadersVisible = false;
                Visits_DataGridView.DataSource = MySS.dt;
                Visits_DataGridView.ColumnHeadersVisible = true;

                DataGridViewColumn dgC1 = Visits_DataGridView.Columns["ID"];
                dgC1.Visible = false;


                //count rows
                string sel = "select count(*) from `visitfinancial` as count";
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Counter_textBox.Text = MySS.sc.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bind_InitialVisit(string username)
        {
            try
            {
                string from =" FROM visitinitial left outer join person_microproject on visitinitial.MicroProject_ID = person_microproject.MicroProject_ID "
                        + " join person P on P.P_ID = person_microproject.Person_ID" +
                        " left outer join visitinitial_user on visitinitial_user.VisitInitial_ID = visitinitial.ID " +
                        " join user on visitinitial_user.User_ID = user.UserID ";
                MySS.query = "SELECT visitinitial.`ID`"
                        + ", visitinitial.`MicroProject_ID`"
                        + ", CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                        + ", `Date`, `Type`, `Reseption`, `Waiting`, `Visit`, `Lawyer`, `Purchasing`, `Photography`, `OtherComments`"
                        + ", `StartInTime`, `StartInTimeReason`, `PurchaseAllItemsOfBudget`, `PurchaseAllItemsOfBudgetReason`"
                        + ", `SamePlace`, `SamePlaceReason`, `SameQualityAndQuantity`, `SameQualityAndQuantityReason`, `Marketing` "
                        + ", `UserName` as 'visitor'"
                        + from ;
                string condition = " where `UserName` like '" + username + "%'";

                MySS.query += condition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);

                Visits_DataGridView.ColumnHeadersVisible = false;
                Visits_DataGridView.DataSource = MySS.dt;
                Visits_DataGridView.ColumnHeadersVisible = true;

                DataGridViewColumn dgc1 = Visits_DataGridView.Columns["ID"];
                dgc1.Visible = false;


                StringBuilder s = new StringBuilder();
                s.Append("select MP_ID from microproject where MP_ID in ");
                s.Append("(");
                s.Append("select visitinitial.`MicroProject_ID` as 'MicroProject_ID'" + from + condition);
                s.Append(")");
                /////////////////////////////////

                //count rows
                string sel = "select count(*) from (" + s + ") as count";
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Counter_textBox.Text = MySS.sc.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bind_FinancialVisit(string username)
        {
            try
            {
                string from =" FROM visitfinancial left outer join person_microproject on visitfinancial.MicroProject_ID = person_microproject.MicroProject_ID "
                        + " join person P on P.P_ID = person_microproject.Person_ID " +
                        " left outer join visitfinancial_user on visitfinancial_user.VisitFinancial_ID = visitfinancial.ID " +
                        " join user on visitfinancial_user.User_ID = user.UserID ";
                MySS.query = "SELECT visitfinancial.`ID`"
                        + ", visitfinancial.`MicroProject_ID`"
                        + ", CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                        + ", `Date`, `Type`, `Continuance`, `Ledger`, `LedgerReason`, `ProfitRatio`, `AverageSales`"
                        + ", `AveragePrice`, `Indicators` "
                        + ", `UserName` as 'visitor'"
                        + from;
                string condition = " where `UserName` like '" + username + "%'";

                MySS.query += condition;
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);

                Visits_DataGridView.ColumnHeadersVisible = false;
                Visits_DataGridView.DataSource = MySS.dt;
                Visits_DataGridView.ColumnHeadersVisible = true;

                DataGridViewColumn dgC1 = Visits_DataGridView.Columns["ID"];
                dgC1.Visible = false;

                StringBuilder s = new StringBuilder();
                s.Append("select MP_ID from microproject where MP_ID in ");
                s.Append("(");
                s.Append("select visitfinancial.`MicroProject_ID` as 'MicroProject_ID'" + from + condition);
                s.Append(")");
                /////////////////////////////////

                //count rows
                string sel = "select count(*) from (" + s + ") as count";
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                Counter_textBox.Text = MySS.sc.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_InitialVisit(int VI_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `visitinitial` where ID = " + VI_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void Delete_FinancialVisit(int VF_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `visitfinancial` where ID = " + VF_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void bind_Users_into_ComboBox()
        {
            //check connection//
            Program.buildConnection();
            string str = " select `UserID`,`UserName` from `user`";
            MySqlCommand sc = new MySqlCommand(str, Program.MyConn);
            MySqlDataAdapter da = new MySqlDataAdapter(str, Program.MyConn);
            MySqlDataReader reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("UserID", typeof(string));
            dt.Columns.Add("UserName", typeof(string));
            dt.Load(reader);
            Users_comboBox.DisplayMember = "UserName";
            Users_comboBox.ValueMember = "UserID";
            Users_comboBox.DataSource = dt;

            Users_comboBox.Text = "";
        }

        private void AllVisits_Load(object sender, EventArgs e)
        {
            try
            {
                myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.ShowAllForm_ToLight(this);
                else
                    myTheme.ShowAllForm_ToNight(this);

                MySS = new MySqlComponents();
                MicroProject_ID = -1;
                l = new Log();
                bind_Users_into_ComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddVisit_button_Click(object sender, EventArgs e)
        {
            //this.Close();
            SelectedDataRow = null;
            if (Initial_radioButton.Checked)
            {
                Add_Initial_Visit add_Initial_Visit = new Add_Initial_Visit(VI_ID);
                add_Initial_Visit.Show();
            }
            else if (Financial_radioButton.Checked)
            {
                Add_Financial_Visit add_Financial_Visit = new Add_Financial_Visit(VF_ID);
                add_Financial_Visit.Show();
            }
        }

        private void UpdateVisit_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Initial_radioButton.Checked)
                {
                    if (SelectedDataRow == null || VI_ID == -1)
                        throw new Exception("Please choose the visit you want to update");
                    //this.DialogResult = DialogResult.OK;
                    //this.Close();
                    Add_Initial_Visit add_Initial_Visit = new Add_Initial_Visit(VI_ID);
                    add_Initial_Visit.Show();
                }
                else if (Financial_radioButton.Checked)
                {
                    if (SelectedDataRow == null || VI_ID == -1)
                        throw new Exception("Please choose the visit you want to update");
                    //this.DialogResult = DialogResult.OK;
                    //this.Close();
                    Add_Financial_Visit add_Financial_Visit = new Add_Financial_Visit(VF_ID);
                    add_Financial_Visit.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteVisit_button_Click(object sender, EventArgs e)
        {
            try
            {
                //select multiple rows
                int SelectedRowCount = Visits_DataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (SelectedRowCount > 0)
                {
                    V_IDs = new int[SelectedRowCount];
                    for (int i = 0; i < SelectedRowCount; i++)
                    {
                        SelectedDataRow = ((DataRowView)Visits_DataGridView.SelectedRows[i].DataBoundItem).Row;
                        if (SelectedDataRow == null)
                            throw new Exception("Please choose the visit you want to delete");
                        V_IDs[i] = Int32.Parse(SelectedDataRow["ID"].ToString());
                        if (Initial_radioButton.Checked)
                        {
                            Delete_InitialVisit(V_IDs[i]);
                            l.Insert_Log("delete the visit of  " + MicroProject_ID + ": " + Beneficiary_Name + " ", "Initial Visit", username, DateTime.Now);

                        }
                        else if (Financial_radioButton.Checked)
                        {
                            Delete_FinancialVisit(V_IDs[i]);
                            l.Insert_Log("delete the visit of  " + MicroProject_ID + ": " + Beneficiary_Name + " ", "Financial Visit", username, DateTime.Now);
                        }
                    }
                    if (Initial_radioButton.Checked)
                    {
                        bind_InitialVisit();
                    }
                    else if (Financial_radioButton.Checked)
                    {
                        bind_FinancialVisit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainBack_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Initial_radioButton.Checked)
            {
                Visit_Type = 1;
                bind_InitialVisit();
            }
            else if (Financial_radioButton.Checked)
            {
                Visit_Type = 2;
                bind_FinancialVisit();
            }
        }

        private void Search_TxtBox_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in Visits_DataGridView.Rows)
            {
                int found = 0;
                for (int i = 0; i < Visits_DataGridView.ColumnCount; i++)
                {
                    if ((r.Cells[i].Value).ToString().Contains(Search_TxtBox.Text))
                    {
                        Visits_DataGridView.Rows[r.Index].Visible = true;
                        found = 1;
                    }
                }
                if (found == 0)
                {
                    Visits_DataGridView.CurrentCell = null;
                    Visits_DataGridView.Rows[r.Index].Visible = false;
                }
            }
        }

        private void Users_comboBox_TextChanged(object sender, EventArgs e)
        {
            if (Initial_radioButton.Checked)
            {
                Visit_Type = 1;
                bind_InitialVisit(Users_comboBox.Text);
            }
            else if (Financial_radioButton.Checked)
            {
                Visit_Type = 2;
                bind_FinancialVisit(Users_comboBox.Text);
            }
        }

        private void Initial_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Initial_radioButton.Checked)
            {
                Visit_Type = 1;
                bind_InitialVisit();
            }
            else if(Financial_radioButton.Checked)
            {
                Visit_Type = 2;
                bind_FinancialVisit();
            }
            else
            {
                Visit_Type = 3;
                Visits_DataGridView.DataSource = null;
            }
        }
        private void Visits_DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)Visits_DataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                if (Initial_radioButton.Checked)
                {
                    VI_ID = Int32.Parse(SelectedDataRow["ID"].ToString());

                }
                else
                {
                    VF_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                }
            }
        }
    }
}
