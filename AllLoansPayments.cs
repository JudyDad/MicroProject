using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using MyWorkApplication.Classes;
using System.Collections.Generic;

namespace MyWorkApplication
{
    public partial class AllLoansPayments : Form
    {
        public AllLoansPayments()
        {
            InitializeComponent();
        }

        public AllLoansPayments(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        MySqlComponents MySS;
        private string username;
        public DataRow SelectedDataRow;
        private int MicroProject_ID, Loan_ID;
        private Log l;
        int[] Loan_IDs;
        DataRow LoanSelectedDataRow;

        private void Loan_bind(string MP_ID, string Funded_By)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select L.Loan_ID as 'ID'" +
                ",L.MicroProject_ID as 'MicroProject_ID'" +
                ",CONCAT(P.P_FirstName, ' ',P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'" +
                ",L.Loan_Amount as 'Loan Amount'" +
                ",L.Loan_DateTaken as 'Receive Date'" +
                ",L.Loan_PaymentsCount as 'Payments Count'" +
                ",MP.MP_Name as 'Project Name'" +
                ",MP.MP_ResonOfPlace as 'Donor'" +
                " from `loan` L left outer join `microproject` MP on L.MicroProject_ID = MP.MP_ID " +
                " inner join person_microproject PMP on PMP.MicroProject_ID = MP.MP_ID " +
                " inner join person P on P.P_ID = PMP.Person_ID";

            string condition = "\n";
            if (MP_ID != "")
            {
                condition += " where L.MicroProject_ID like CAST('" + MP_ID + "%' AS CHAR) ";
                if (Funded_By != "")
                    condition += "  and MP.MP_ResonOfPlace like N'" + Funded_By + "'";
            }
            else if (Funded_By != "")
                condition += "  where MP.MP_ResonOfPlace like N'" + Funded_By + "'";

            MySS.query += condition;

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            Loan_dataGridView.DataSource = MySS.dt;
            Loan_dataGridView.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            Loan_dataGridView.Columns[3].DefaultCellStyle.Format = "#,##0";
            DataGridViewColumn dgc1 = Loan_dataGridView.Columns["ID"];
            dgc1.Visible = false;

        }

        private void Payment_bind(string Loan_ID)
        {
            //check connection//
            Program.buildConnection();

            string strCmd = "select Pay_ID as 'ID'" +
            ",Pay_Amount as 'Amount'" +
            ",Pay_DueDate as 'Pay Date'" +
            ",Pay_IsPaid as 'State'" +
            ",Pay_RecievedOnDate as 'Actual Pay Date'" +
            ",Loan_ID as 'Loan_ID'" +
            "\n from `payment` ";
            string condition = "\n";
            if (Loan_ID != "")
            {
                condition = " where Loan_ID = " + Int32.Parse(Loan_ID) + " ";
            }
            strCmd += condition;

            MySS.sc = new MySqlCommand(strCmd, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            Payment_dataGridView.DataSource = MySS.dt;
            Payment_dataGridView.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            Payment_dataGridView.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            Payment_dataGridView.Columns[1].DefaultCellStyle.Format = "#,##0";
            DataGridViewColumn dgc1 = Payment_dataGridView.Columns["ID"];
            dgc1.Visible = false;
            DataGridViewColumn dgc2 = Payment_dataGridView.Columns["Loan_ID"];
            dgc2.Visible = false;
        }

        private void deleteLoan(int L_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `loan` where `Loan_ID` = " + L_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void calculate_ImportExportMoney()
        {
            //check connection//
            Program.buildConnection();

            //Exports
            MySS.query = "select sum(`Loan_Amount`) from `loan`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Double exports = Convert.ToDouble(MySS.sc.ExecuteScalar());
            //Export_textBox.Text = ;
            Export_textBox.Text = Convert.ToDecimal(exports).ToString("#,##0");

            //Imports
            MySS.query = "select count(`Pay_Amount`) from `payment` where `Pay_IsPaid` = N'Paid'";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            int count = Convert.ToInt32(MySS.sc.ExecuteScalar());

            if (count != 0)
            {
                MySS.query = "select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` = N'Paid'";
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                Double Paid = Convert.ToDouble(MySS.sc.ExecuteScalar());
                //Import_textBox.Text = Paid.ToString();
                Import_textBox.Text = Convert.ToDecimal(Paid).ToString("#,##0");
            }
            else
            {
                Import_textBox.Text = "0";
            }
        }
        private void calculate_ImportExportMoney(int[] Loan_IDs)
        {
            //check connection//
            Program.buildConnection();

            var str = "(" + string.Join(", ", Array.ConvertAll(Loan_IDs, v => v.ToString(CultureInfo.InvariantCulture))) + ")";
            //Exports
            MySS.query = "select sum(`Loan_Amount`) from `loan` where `Loan_ID` in " + str + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Double exports = Convert.ToDouble(MySS.sc.ExecuteScalar());
            //Export_textBox.Text = ;
            Export_textBox.Text = Convert.ToDecimal(exports).ToString("#,##0");


            //Imports
            MySS.query = "select count(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` in " + str + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            int count = Convert.ToInt32(MySS.sc.ExecuteScalar());

            if (count != 0)
            {
                MySS.query = "select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` in " + str + " ";
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                Double Paid = Convert.ToDouble(MySS.sc.ExecuteScalar());
                //Import_textBox.Text = Paid.ToString();
                Import_textBox.Text = Convert.ToDecimal(Paid).ToString("#,##0");
            }
            else
            {
                Import_textBox.Text = "0";
            }
        }

        private void AddLoan_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateLoan_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null)
                    throw new Exception("Please choose the loan you want to update");
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteLoan_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || Loan_ID == -1)
                    throw new Exception("Please choose the loan you want to delete");
                deleteLoan(Loan_ID);
                l.Insert_Log("Delete Loan of : " + MicroProject_ID + " ", "Loan", username, DateTime.Now);

                Loan_bind("", "");
                Loan_ID = -1;

                Payment_bind("");
                calculate_ImportExportMoney();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Loan_dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Loan_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    Loan_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                    MicroProject_ID = (int)SelectedDataRow["MicroProject_ID"];
                }
                Payment_bind(Loan_ID.ToString());

                int SelectedRowCount = Loan_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (SelectedRowCount > 0)
                {
                    Loan_IDs = new int[SelectedRowCount];
                    for (int i = 0; i < SelectedRowCount; i++)
                    {
                        LoanSelectedDataRow = ((DataRowView)Loan_dataGridView.SelectedRows[i].DataBoundItem).Row;

                        Loan_IDs[i] = Int32.Parse(LoanSelectedDataRow["ID"].ToString());
                    }
                    calculate_ImportExportMoney(Loan_IDs);
                }
                else
                    calculate_ImportExportMoney();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MP_idTxtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Loan_bind(MP_idTxtBox.Text, replaceQuotation(FundedBy_comboBox.Text));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string replaceQuotation(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }
        private void AllLoansPayments_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.ShowAllForm_ToLight(this);
                else
                    myTheme.ShowAllForm_ToNight(this);

                MySS = new MySqlComponents();
                Loan_bind("", "");
                l = new Log();
                calculate_ImportExportMoney();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region mouse hover
        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            AddLoan_button.BackgroundImage = Properties.Resources.add;
        }
        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            AddLoan_button.BackgroundImage = Properties.Resources.add0;
        }
        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateLoan_button.BackgroundImage = Properties.Resources.update;
        }
        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateLoan_button.BackgroundImage = Properties.Resources.update0;
        }
        private void Delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteLoan_button.BackgroundImage = Properties.Resources.delete;
        }
        private void Delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteLoan_button.BackgroundImage = Properties.Resources.delete0;
        }
        #endregion mouse hover



        private void MainBack1_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int mp_id;
            string P_name;

            UserNotification userNotification = new UserNotification();
            foreach (DataGridViewRow r in Loan_dataGridView.Rows)
            {
                mp_id = Convert.ToInt32(r.Cells["MicroProject_ID"].Value.ToString());
                P_name = r.Cells["Beneficiary Name"].Value.ToString();
                DateTime Loan_Date = Convert.ToDateTime(r.Cells["Receive Date"].Value);

                //add user notification " VISITS " for micro users
                DateTime first_visit_date = Loan_Date.AddDays(27);

                DateTime second_visit_date = first_visit_date.AddMonths(2);
                second_visit_date = second_visit_date.AddDays(27);

                DateTime third_visit_date = second_visit_date.AddMonths(3);
                third_visit_date = third_visit_date.AddDays(27);

                DateTime forth_visit_date = third_visit_date.AddMonths(3);
                forth_visit_date = forth_visit_date.AddDays(27);

                List<int> User_IDs = new List<int>() { 8, 16, 23 };   //8, 16, 23,20
                foreach (int u in User_IDs)
                {
                    userNotification.Insert_UserNotification(first_visit_date, "الزيارة الأولى - مطابقة", P_name, mp_id, u ,-5);
                    userNotification.Insert_UserNotification(second_visit_date, "الزيارة الثانية - مالي", P_name, mp_id, u, -10);
                    userNotification.Insert_UserNotification(third_visit_date, "الزيارة الثالثة - مالي", P_name, mp_id, u, -15);
                    userNotification.Insert_UserNotification(forth_visit_date, "الزيارة الرابعة - مالي", P_name, mp_id, u, -20);
                }
            }
        }
    }
}