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
    public partial class AllExeFiles : Form
    {
        public AllExeFiles()
        {
            InitializeComponent();
        }
        public AllExeFiles(string username)
        {
            this.username = username;
            InitializeComponent();
        }

        MySqlComponents MySS;
        public DataRow SelectedDataRow;
        private int ExeFile_ID, MicroProject_ID;
        string username;
        Log l;

        private void deleteExeFile(int ExeF_ID)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "delete from `exefile` where `ExeF_ID` = " + ExeF_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void ExeFile_bind(string mp_ID, string exe_NO, string begDate, string endDate)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "SELECT `ExeF_ID` as 'ID'" +
                            ",`ExeF_No` as 'Number'" +
                            ",`ExeF_BeginDate` as 'Begin Date'" +
                            ",`ExeF_CurrentDate` as 'Current Date'" +
                            ",`MicroProject_ID` as 'MicroProject_ID'" +
                            ",`ExeF_ImpoundDate` as 'Impound Date'" +
                            ",`ExeF_ImpoundType` as 'Impound Type'" +
                            " FROM `exefile` ";
            string condition = "\n";
            if (mp_ID != "")
            {
                condition = " where MicroProject_ID like CAST('" + mp_ID + "%' AS CHAR)";
                if (exe_NO != "")
                {
                    condition = " and ExeF_No = " + Int32.Parse(exe_NO) + " ";
                    if (begDate != "")
                    {
                        condition = " and ExeF_BeginDate = " + DateTime.Parse(begDate) + " ";
                        if (endDate != "")
                        {
                            condition = " and ExeF_CurrentDate = " + DateTime.Parse(endDate) + " ";
                        }
                    }
                }
            }
            if (exe_NO != "")
            {
                condition = " where ExeF_No = " + Int32.Parse(exe_NO) + " ";
                if (begDate != "")
                {
                    condition = " and ExeF_BeginDate = " + DateTime.Parse(begDate) + " ";
                    if (endDate != "")
                    {
                        condition = " and ExeF_CurrentDate = " + DateTime.Parse(endDate) + " ";
                    }
                }
            }
            else if (begDate != "")
            {
                condition = " where ExeF_BeginDate = " + DateTime.Parse(begDate) + " ";
                if (endDate != "")
                {
                    condition = " and ExeF_CurrentDate = " + DateTime.Parse(endDate) + " ";
                }
            }
            else if (endDate != "")
            {
                condition = " where ExeF_CurrentDate = " + DateTime.Parse(endDate) + " ";
            }
            MySS.query += condition;

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            ExeFile_dataGridView.DataSource = MySS.dt;
            ExeFile_dataGridView.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            ExeFile_dataGridView.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            ExeFile_dataGridView.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy";

            DataGridViewColumn dgc1 = ExeFile_dataGridView.Columns["ID"];
            dgc1.Visible = false;
        }

        private void AllExeFiles_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.ShowAllForm_ToLight(this);
                else
                    myTheme.ShowAllForm_ToNight(this);

                MySS = new MySqlComponents();
                l = new Log();
                ExeFile_bind("", "", "", "");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        private void AddExeFile_button_Click(object sender, EventArgs e)
        {
            try
            {
                SelectedDataRow = null;
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void UpdateExeFile_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || ExeFile_ID == -1)
                    throw new Exception("Please choose the project you want to update it");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteExeFile_button_Click(object sender, EventArgs e)
        {
            try
            {
                deleteExeFile(ExeFile_ID);
                l.Insert_Log("Delete Executional file of : " + MicroProject_ID + " ", "Executional file", username, DateTime.Now);
                ExeFile_bind("", "", "", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExeFile_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)ExeFile_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                ExeFile_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                
            }
        }
        private void MP_idTxtBox_TextChanged(object sender, EventArgs e)
        {
            ExeFile_bind(MP_idTxtBox.Text, ExeF_No_textBox1.Text, "", "");
        }
        private void ExeF_No_textBox1_TextChanged(object sender, EventArgs e)
        {
            ExeFile_bind(MP_idTxtBox.Text, ExeF_No_textBox1.Text, "", "");
        }
        private void ExeF_ExpiredDate_dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ExeFile_bind(MP_idTxtBox.Text, ExeF_No_textBox1.Text, "", ExeF_ExpiredDate_dateTimePicker1.Value.ToShortDateString());
        }

        private void ExeF_BeginDate_dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ExeFile_bind(MP_idTxtBox.Text, ExeF_No_textBox1.Text, ExeF_BeginDate_dateTimePicker1.Value.ToShortDateString(), "");
        }

        private void MainBack0_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region mouse hover
        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            AddExeFile_button.BackgroundImage = Properties.Resources.add;
        }
        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            AddExeFile_button.BackgroundImage = Properties.Resources.add0;
        }
        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateExeFile_button.BackgroundImage = Properties.Resources.update;
        }
        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateExeFile_button.BackgroundImage = Properties.Resources.update0;
        }
        private void Delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteExeFile_button.BackgroundImage = Properties.Resources.delete;
        }
        private void Delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteExeFile_button.BackgroundImage = Properties.Resources.delete0;
        }
        #endregion

    }
}
