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
    public partial class SelectPerson : Form
    {
        public SelectPerson()
        {
            InitializeComponent();
        }

        private void SelectPerson_Load(object sender, EventArgs e)
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
                bind("");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        MySqlComponents MySS;
        public DataRow SelectedDataRow;

        private void bind(string P_Name)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select P_ID as 'ID'"
                                 + ",CONCAT(P_FirstName, ' ', P_FatherName, ' ', P_LastName) as 'Beneficiary Name'"
                                 + " from `person` "
                                 + " where IsProjectOwner like 'YES'";
            string condition = "";
            if (P_Name != "")
            {
                condition += " and ( P_FirstName like N'%" + P_Name + "%' or P_FatherName like N'%" + P_Name + "%' or P_LastName like N'%" + P_Name + "%' )";

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
            DataGridViewColumn dgC1;
            if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
            {
                dgC1 = MP_dataGridView.Columns["ID"];
                dgC1.Visible = false;
            }
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
                    // this.DialogResult = DialogResult.OK;
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
            bind(P_nameTxtBox.Text);
        }
        public DataTable dataTable;
        
        private void OK_button_Click(object sender, EventArgs e)
        {
            try
            {
                //select multiple rows
                int SelectedRowCount = MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (SelectedRowCount > 0)
                {
                    dataTable = new DataTable();
                    dataTable.Clear();
                    dataTable.Columns.Add("ID");
                    dataTable.Columns.Add("Beneficiary Name");

                    for (int i = 0; i < SelectedRowCount; i++)
                    {
                        SelectedDataRow = ((DataRowView)MP_dataGridView.SelectedRows[i].DataBoundItem).Row;
                        if (SelectedDataRow == null)
                            throw new Exception("Please choose at least one beneficiary..");
                        
                        dataTable.Rows.Add(SelectedDataRow.ItemArray);
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
