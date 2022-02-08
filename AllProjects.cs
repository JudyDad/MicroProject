using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class AllProjects : Form
    {
        public AllProjects()
        {
            InitializeComponent();
        }

        public AllProjects(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        MySqlComponents MySS;
        private string username;
        public DataRow SelectedDataRow;
        private int MicroProject_ID;
        private Log l;
        private string MP_Name;

        private void Delete_MP(int MP_ID)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "delete from `microproject` where MP_ID = " + MP_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void MicroProject_bind(string MP_ID, string MP_Name)
        {
            try
            {
                //check connection//
                Program.buildConnection();
                
                MySS.query = "select MP.MP_ID as 'MicroProject_ID'"
                    + ",CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                    + ",MP.MP_Name as 'Project Name'"
                    + ",MP.MP_DateOfRequest as 'Application Date'"
                    + ",MP.MP_AllPriceNeeded as 'Requested Amount'"
                    + ",MP.MP_PeriodOfExecution as 'Timeline'"
                    + ",MP.MP_Describtion as 'Description'"
                    + ",MP.MP_SimpleProfit as 'Minimal Profit'"
                    + ",MP.MP_ResonOfProject as 'Reason of the Project'"
                    + ",MP.MP_IsNeedLicense as 'needs a license'"
                    + ",MP.MP_LicenseSide as 'License Side'"
                    + ",MP.MP_PlaceOfExecution as 'Place of Project'"
                    + ",MP.MP_ResonOfPlace as 'Funded By'"
                    + ",MP.MP_OtherComment as 'Other Comments'"
                    + ",MP.MP_Country as 'Country'"
                    + ",MP.MP_City as 'City'"
                    + ",MP.MP_ClientsAndContinuance  as 'Clients And Continuance'"
                    + ",PMP.Person_ID as 'Beneficiary_ID'"


                    + " ,CASE MP.MP_State WHEN 0 THEN N'Rejected' WHEN 1 THEN N'Accepted' WHEN 2 THEN N'Delayed' WHEN 4 THEN N'Financed' WHEN 5 THEN N'Closed' ELSE N'On Hold' End as 'Project State'"
                    //+ ",'Project State' = CASE WHEN MP.MP_State = 0 THEN N'مرفوض' WHEN MP.MP_State = 1 THEN N'مقبول' WHEN MP.MP_State = 2 THEN N'مؤجل' ELSE N'بالانتظار' END"

                    //  + ",MP.MP_StateReason as 'Project status reason'"
                    + ",MP.MP_StateComment as 'Comments'"
                    + ",L.Level_Symbol as  'Level'"
                    + ",C.C_Name as 'Category'"
                    
                    + " From `microproject` MP left outer join `person_microproject` PMP on MP.MP_ID = PMP.MicroProject_ID "
                    + " left outer join `person` P on P.P_ID = PMP.Person_ID "
                    + " left outer join `priest` Pr on Pr.Priest_ID = MP.MP_Priest_ID "
                    + " left outer join `level` L on MP.MP_Level_ID = L.Level_ID "
                    + " left outer join `category` C on MP.MP_Category_ID = C.C_ID ";
                
                string condition = "\n";
                if (MP_ID != "")
                {
                    //condition = " where CAST(MP.MP_ID AS nvarchar(Max)) LIKE '" + MP_idTxtBox.Text + "%'";
                    condition = " where MP.MP_ID like CAST('" + MP_idTxtBox.Text + "%' AS CHAR)";
                    if (MP_Name != "")
                    {
                        condition += " and MP.MP_Name like N'" + MP_nameTxtBox.Text + "%'";
                    }
                }
                else if (MP_Name != "")
                {
                    condition = " where MP.MP_Name like N'" + MP_nameTxtBox.Text + "%'";
                }
                MySS.query += condition;

                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);

                MicroProject_DataGridView.ColumnHeadersVisible = false;
                MicroProject_DataGridView.DataSource = MySS.dt;
                MicroProject_DataGridView.ColumnHeadersVisible = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void MicroProject_DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)MicroProject_DataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                MicroProject_ID = Int32.Parse(SelectedDataRow["MicroProject_ID"].ToString());
                MP_Name = SelectedDataRow["Project Name"].ToString();
                //this.DialogResult = DialogResult.OK;
            }
        }

        private void AddProject_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateProject_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || MicroProject_ID == -1)
                    throw new Exception("Please choose the Project you want to update");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteProject_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the Project ??", "The Project will be deleted with its all details", MessageBoxButtons.YesNo);
            try
            {
                if (dialogResult == DialogResult.Yes)
                {
                    if (SelectedDataRow == null || MicroProject_ID == -1)
                        throw new Exception("Please choose the Project you want to update");
                    Delete_MP(MicroProject_ID);
                    l.Insert_Log("delete the project " + MP_Name, "Micro Project", username, DateTime.Now);
                    AllProjects_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AllProjects_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.ShowAllForm_ToLight(this);
                else
                    myTheme.ShowAllForm_ToNight(this);

                MySS = new MySqlComponents();
                MicroProject_ID = -1;
                l = new Log();
                MicroProject_bind("", "");
                MicroProject_DataGridView.ClearSelection();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void MP_nameTxtBox_TextChanged(object sender, EventArgs e)
        {
            MicroProject_bind(MP_idTxtBox.Text, MP_nameTxtBox.Text);
        }

        #region mouse hover

        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            AddProject_button.BackgroundImage = Properties.Resources.add;
        }

        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            AddProject_button.BackgroundImage = Properties.Resources.add0;
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

        private void MainBack_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}