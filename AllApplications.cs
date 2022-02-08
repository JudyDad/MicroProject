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
    public partial class AllApplications : Form
    {
        public AllApplications()
        {
            InitializeComponent();
        }
        MySqlComponents MySS;
        DataRow SelectedDataRow;
        Log l;
        int MicroProject_ID;
        int Person_ID;
        int PMP_ID;
        string Beneficiary_Name;

        private void Application_bind()
        {
            try
            {
                //check connection//
                Program.buildConnection();
                string from = " From `microproject` MP left outer join `person_microproject` PMP on MP.MP_ID = PMP.MicroProject_ID "
                            + " left outer join `person` P on P.P_ID = PMP.Person_ID ";

                MySS.query = "select PMP.PMP_ID as 'ID'"
                    + ",MP.MP_ID as 'MicroProject_ID'"
                    + ",CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                    + ",MP.MP_Name as 'Project Name'"
                    + ",PMP.Person_ID as 'Beneficiary_ID'"
                    + " ,CASE MP.MP_State WHEN 0 THEN N'Rejected' WHEN 1 THEN N'Accepted' WHEN 2 THEN N'Delayed' WHEN 4 THEN N'Financed' WHEN 5 THEN N'Closed' ELSE N'On Hold' End as 'Project State'"
                    + from;

                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);

                Application_DataGridView.ColumnHeadersVisible = false;
                Application_DataGridView.DataSource = MySS.dt;
                Application_DataGridView.ColumnHeadersVisible = true;

                DataGridViewColumn dgC1 = Application_DataGridView.Columns["ID"];
                dgC1.Visible = false;
                DataGridViewColumn dgC2 = Application_DataGridView.Columns["Beneficiary_ID"];
                dgC2.Visible = false;

                StringBuilder s = new StringBuilder();
                s.Append("select MP_ID from microproject where MP_ID in ");
                s.Append("(");
                s.Append("select MP.MP_ID  as 'MicroProject_ID'" + from);
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
        private void Delete_Application(int PMP_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `person_microproject` where PMP_ID = " + PMP_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void Delete_Person(int P_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete From `person` where P_ID =" + P_ID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void Delete_MicroProject(int MP_ID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "delete from `microproject` where MP_ID = " + MP_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void AllApplications_Load(object sender, EventArgs e)
        {
            try
            {
                //MyTheme myTheme = new MyTheme();
                //if (Properties.Settings.Default.theme == "Light")
                //    myTheme.ShowAllForm_ToLight(this);
                //else
                //    myTheme.ShowAllForm_ToNight(this);

                MySS = new MySqlComponents();
                MicroProject_ID = -1;
                l = new Log();
                Application_bind();
                Application_DataGridView.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Application_DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)Application_DataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                if (SelectedDataRow["MicroProject_ID"] == null || SelectedDataRow["MicroProject_ID"] == DBNull.Value)
                    MicroProject_ID = -1;
                else MicroProject_ID = Int32.Parse(SelectedDataRow["MicroProject_ID"].ToString());

                if (SelectedDataRow["Beneficiary_ID"] == null || SelectedDataRow["Beneficiary_ID"] == DBNull.Value)
                    Person_ID = -1;
                else Person_ID = Int32.Parse(SelectedDataRow["Beneficiary_ID"].ToString());

                if (SelectedDataRow["ID"] == null || SelectedDataRow["ID"] == DBNull.Value)
                    PMP_ID = -1;
                else PMP_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
            }
        }
        private void DeleteApplication_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the Project ??", "The Project will be deleted with its all details", MessageBoxButtons.YesNo);
            try
            {
                if (dialogResult == DialogResult.Yes)
                {
                    if (SelectedDataRow == null)
                        throw new Exception("Please choose the Application you want to delete");

                    if (PMP_ID == -1 || SelectedDataRow["ID"] == null)
                    {
                        if (MicroProject_ID == -1 || SelectedDataRow["MicroProject_ID"] == null)
                        {
                            if (Person_ID == -1 || SelectedDataRow["Beneficiary_ID"] == null)
                            {
                                throw new Exception("the Application you want to delete");
                            }
                            else   // there is Person_ID
                            {
                                Delete_Person(Person_ID);
                                l.Insert_Log("delete the beneficiary: " + Beneficiary_Name, " Beneficiary ", Properties.Settings.Default.username, DateTime.Now);
                            }
                        }
                        else // there is MicroProject_ID
                        {
                            Delete_MicroProject(MicroProject_ID);
                            l.Insert_Log("deletethe project: " + MicroProject_ID, " Project ", Properties.Settings.Default.username, DateTime.Now);
                        }
                    }
                    else //there is application link
                    {
                        Delete_Application(PMP_ID);
                        l.Insert_Log("delete the project: " + MicroProject_ID + " of the beneficiary: " + Beneficiary_Name, " Application ", Properties.Settings.Default.username, DateTime.Now);

                        Delete_Person(Person_ID);
                        l.Insert_Log("delete the beneficiary: " + Beneficiary_Name, " Beneficiary ", Properties.Settings.Default.username, DateTime.Now);

                        Delete_MicroProject(MicroProject_ID);
                        l.Insert_Log("deletethe project: " + MicroProject_ID, " Project ", Properties.Settings.Default.username, DateTime.Now);
                    }
                    PMP_ID = Person_ID = MicroProject_ID = -1;
                    Application_bind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddApplication_button_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Form application_Form = new Application_Form();
                application_Form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateApplication_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || MicroProject_ID == -1)
                    throw new Exception("Please choose the Project you want to update");
                Application_Form application_Form = new Application_Form(Person_ID, MicroProject_ID);
                application_Form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MP_idTxtBox_TextChanged(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.DataGridViewRow r in Application_DataGridView.Rows)
            {
                int found = 0;
                for (int i = 0; i < Application_DataGridView.ColumnCount; i++)
                {
                    if ((r.Cells[i].Value).ToString().Contains(MP_idTxtBox.Text))
                    {
                        Application_DataGridView.Rows[r.Index].Visible = true;
                        //MP_dataGridView.Rows[r.Index].Selected = true;
                        found = 1;
                    }
                }
                if (found == 0)
                {
                    Application_DataGridView.CurrentCell = null;
                    Application_DataGridView.Rows[r.Index].Visible = false;
                }
            }
            
        }

        //        private void button1_Click(object sender, EventArgs e)
        //        {
        //            //check connection//
        //            Program.buildConnection();

        //            MySS.query = "SELECT person_microproject.Person_ID as 'P_ID' FROM `intermediaryside` join `person_microproject` on intermediaryside.MicroProject_ID = person_microproject.MicroProject_ID ";
        //            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
        //            MySS.sc.ExecuteNonQuery();
        //            MySS.da = new MySqlDataAdapter(MySS.sc);
        //            MySS.dt = new DataTable();
        //            MySS.da.Fill(MySS.dt);

        //            if (MySS.dt != null)
        //            {
        //                if (MySS.dt.Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < MySS.dt.Rows.Count; i++)
        //                    {
        //                        int P_ID = Int32.Parse(MySS.dt.Rows[i][0].ToString());
        //                        string q = @"UPDATE `person` SET `P_SentimentalLoss`= (SELECT `IS_Name` as 'P_SentimentalLoss'
        //FROM `intermediaryside` join `person_microproject`on intermediaryside.MicroProject_ID = person_microproject.MicroProject_ID
        //where person.P_ID = person_microproject.Person_ID LIMIT 1)
        //where `P_ID` = " + P_ID + "";
        //                        MySqlCommand sc = new MySqlCommand(q, Program.MyConn);
        //                        sc.ExecuteNonQuery();
        //                    }
        //                    MessageBox.Show("Done");
        //                }
        //            }
        //}
    }
}
