using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class SelectPersonAndProject : Form
    {
        public SelectPersonAndProject()
        {
            InitializeComponent();
        }

        MySqlComponents MySS;
        DataRow SelectedDataRow;

        private void bind(string MP_ID,string P_Name)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select PMP.MicroProject_ID as 'MicroProject_ID'"
                                 + ",PMP.Person_ID as 'Beneficiary_ID'"
                                 + ",CONCAT(P1.P_FirstName, ' ', P1.P_FatherName, ' ', P1.P_LastName) as 'Beneficiary Name'"
                                 + ",MP.MP_Name as 'Project Name'"
                                 + ",MP.MP_AllPriceNeeded as 'Requested Amount'"
                                 + ",MP.MP_SimpleProfit as 'Minimal Profit'"
                                 + ",L.Loan_Amount as 'Loan Amount'"
                                 + ",L.Loan_DateTaken as 'Loan Date'"
                                 + " from person_microproject PMP left outer join person P1 on P1.P_ID = PMP.Person_ID "
                                 + " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID" 
                                 + " left outer join loan L on MP.MP_ID = L.MicroProject_ID ";

            string condition = "";
            if (P_Name != "")
            {
                condition += " where ( P1.P_FirstName like N'%" + P_Name + "%' or P1.P_FatherName like N'%" + P_Name + "%' or P1.P_LastName like N'%" + P_Name + "%' )";
                
            }
            else if (MP_ID != "")
            {
                //condition += "where PMP.MicroProject_ID = " + Int32.Parse(MP_ID) + " ";
                condition = " where PMP.MicroProject_ID like CAST('" + MP_ID + "%' AS CHAR)";
            }
            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            MP_dataGridView.ColumnHeadersVisible = false;
            MP_dataGridView.DataSource = MySS.dt;
            MP_dataGridView.ColumnHeadersVisible = true;
            DataGridViewColumn dgC1,dgC2,dgC3;
            if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
            {
                dgC1 = MP_dataGridView.Columns["Beneficiary_ID"];
                //    dgC2 = MP_dataGridView.Columns["Loan Amount"];
                //    dgC3 = MP_dataGridView.Columns["Loan Date"];
                dgC1.Visible = false;
                    //dgC2.Visible = dgC3.Visible = false;
            }
        }

        private void SelectPersonAndProject_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.ShowAllForm_ToLight(this);
                else
                    myTheme.ShowAllForm_ToNight(this);

                MySS = new MySqlComponents();
                SelectedDataRow = null;
                bind("", "");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        public DataRow showSelectedMPRow()
        {
            this.MP_dataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MP_dataGridView_RowHeaderMouseDoubleClick);
            if (this.ShowDialog() == DialogResult.OK)
            {
                return SelectedDataRow;
            }
            else
            {
                return null;
            }
        }
        private void MP_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)MP_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
      

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void P_nameTxtBox_TextChanged(object sender, EventArgs e)
        {
            bind(MP_idTxtBox.Text, P_nameTxtBox.Text);
        }

        private void MP_idTxtBox_TextChanged(object sender, EventArgs e)
        {
            bind(MP_idTxtBox.Text, P_nameTxtBox.Text);
        }


    }
}
