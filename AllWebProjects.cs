using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace MyWorkApplication
{
    public partial class AllWebProjects : Form
    {
        public AllWebProjects()
        {
            InitializeComponent();
        }

        public AllWebProjects(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        MySqlComponents MySS;
        private string username;
        public DataRow SelectedDataRow;
        private Log l;
        private int MicroProject_ID, MicroProjectEnglish_ID;
        private string MP_Name;

        private void Delete_MPE(int MPE_ID)
        {
            //check connection//
            if (Program.MyConn.State != ConnectionState.Open)
            {
                Program.buildConnection();
            }
            MySS.query = "delete from `microprojectenglish` where MPE_ID = " + MPE_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void MicroProjectEnglish_bind(string MP_ID, string MP_NAME)
        {
            //check connection//
            if (Program.MyConn.State != ConnectionState.Open)
            {
                Program.buildConnection();
            }
            MySS.query = "select MPE.MPE_ID as 'MicroProjectEnglishID'"
              + ",MPE.MicroProject_ID as 'Project Number'"
              + ",MPE.MPE_BeneficiaryName as 'Beneficiary name'"
              + ",MPE.MPE_PartnerName as 'Partner name'"
              + ",MPE.MPE_Name as 'Project name'"
              + ",MPE.MPE_Done as 'Project funded'"
              + ",MPE.MPE_Image as 'Image Path'"
              + ",MPE.MPE_Location as 'Project Location'"
              + ",MPE.MPE_VideoIn as 'Video In'"
              + ",MPE.MPE_VideoOut as 'Video Out'"
              + ",MPE.MPE_DateOfRequest as 'Date of application'"
              + ",MPE.MPE_AllMoneyNeeded as 'Amount requested'"
              + ",MPE.MPE_PeriodOfExecution as 'Duration for execution'"

              + ",MPE.MPE_Description as 'Project description'"
              + ",MPE.MPE_Suffering as 'Suffering and Need'"
              
              + ",MPE.MPE_MoneyDonated as 'Amount received'"
              
              + ",MPE.MPE_SuccessStory as 'Success Story'"          //success story
              + ",MPE.MPE_Country as 'Country'"
              + ",MPE.MPE_City as 'City'"

              + "\n  FROM `microprojectenglish` as MPE ";

            string condition = "\n";
            if (MP_ID != "")
            {
                //condition = " where CAST(MPE.MicroProject_ID AS nvarchar(Max)) LIKE '" + MP_idTxtBox.Text + "%'";
                condition = " where MPE.MicroProject_ID like CAST('" + MP_idTxtBox.Text + "%' AS CHAR)";
                if (MP_NAME != "")
                {
                    condition += " and MPE.MPE_Name like N'" + MP_nameTxtBox.Text + "%'";
                }
            }
            else if (MP_NAME != "")
            {
                condition = " where MPE.MPE_Name like N'" + MP_nameTxtBox.Text + "%'";
            }
            MySS.query += condition;

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            MicroProject_DataGridView.DataSource = MySS.dt;

            DataGridViewColumn dgC2 = MicroProject_DataGridView.Columns["MicroProjectEnglishID"];
            dgC2.Visible = false;
            //count rows
            string sel = "select count(*) from (" + MySS.query + ") as count";
            MySS.sc = new MySqlCommand(sel, Program.MyConn);
            Counter_textBox.Text = MySS.sc.ExecuteScalar().ToString();
        }

        private void MicroProject_DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)MicroProject_DataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                MicroProjectEnglish_ID = Convert.ToInt32(SelectedDataRow["MicroProjectEnglishID"].ToString());
                MicroProject_ID = Convert.ToInt32(SelectedDataRow["Project Number"].ToString());
                MP_Name = (string)SelectedDataRow["Project name"];
            }
        }

        private void Add_MPE_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Update_MPE_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || MicroProjectEnglish_ID == -1)
                    throw new Exception("Please choose the project you want to update");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_MPE_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || MicroProjectEnglish_ID == -1)
                    throw new Exception("Please choose the project you want to delete");
                Delete_MPE(MicroProjectEnglish_ID);
                l.Insert_Log("Delete the project " + MP_Name, "Micro Project English", username, DateTime.Now);
                MicroProjectEnglish_bind("", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MP_nameTxtBox_TextChanged(object sender, EventArgs e)
        {
            MicroProjectEnglish_bind(MP_idTxtBox.Text, MP_nameTxtBox.Text);
        }

        #region mouse hover

        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            Add_MPE_button.BackgroundImage = Properties.Resources.add;
        }

        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            Add_MPE_button.BackgroundImage = Properties.Resources.add0;
        }

        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateProject_button.BackgroundImage = Properties.Resources.update;
        }

        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateProject_button.BackgroundImage = Properties.Resources.update0;
        }

        private void Delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteProject_button.BackgroundImage = Properties.Resources.delete;
        }

        private void Delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteProject_button.BackgroundImage = Properties.Resources.delete0;
        }

        #endregion mouse hover

        private void AllWebProjects_Load_1(object sender, EventArgs e)
        {
            MySS = new MySqlComponents();
            l = new Log();
            MicroProjectEnglish_bind("", "");
        }
        
        private void MainBack_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}