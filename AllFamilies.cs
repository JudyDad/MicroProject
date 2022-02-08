using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class AllFamilies : Form
    {
        public AllFamilies()
        {
            InitializeComponent();
        }

        public AllFamilies(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        MySqlComponents MySS;
        private int Family_ID;
        private string username;
        private Log l;
        public DataRow SelectedDataRow;
        private string Family_BookNum;

        private void deleteFamily(int FID)
        {
            //check connection//
             Program.buildConnection();
            
            MySS.query = "Delete from `family` "
                                   + "where F_ID =" + FID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void FamilyDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)FamilyDataGridView.CurrentRow.DataBoundItem).Row;
            Family_BookNum = (string)SelectedDataRow["Book Number"];
            Family_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
            FamilyMember_bind(Family_ID.ToString(), "");
        }
        #region binds

        public void Family_bind(string strNumber, string strFname)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "select F_ID as 'ID'"
                + ",F_FirstName as 'First Name'"
                + ",F_LastName as 'Last Name'"
                + ",F_FatherName as 'Father Name'"
                + ",F_Number as 'Book Number'"
                + "\n From `family`";

            string condition = "";
            if (strNumber != "")
            {
                condition = " where F_Number like N'" + FamilyNum_textBox1.Text + "%'";
                if (strFname != "")
                {
                    condition += " and F_FirstName like N'" + FamilyFName_textBox1.Text + "%'";
                }
            }
            else if (strFname != "")
            {
                condition = " where F_FirstName like N'" + FamilyFName_textBox1.Text + "%'";
                
            }
            MySS.query += condition;

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            FamilyDataGridView.DataSource = MySS.dt;
        }

        public void FamilyMember_bind(string family_ID,string P_Name)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "select p.P_ID as 'ID'"
                + ",CONCAT(p.P_FirstName, ' ', p.P_FatherName, ' ', p.P_LastName) as 'Name'"
                + ",p.P_MotherName as 'Mother Name'"
                + ",p.P_Sex as 'Gender'"
                + ",p.P_MaritalStatus as 'Marital Status'"
                + ",pf.IsInNow as 'IsInNow'"
                + ",pf.Relation as 'Relation'"
                + ",pf.Work_Name as 'Work'"
                + ",pf.Family_ID as 'Family_ID'"
                + ",p.P_DOB as 'Birth Date'"
                + ",p.IsProjectOwner as 'Project Owner'"
                + "\n From `person` p right outer join `person_family` pf on p.P_ID = pf.Person_ID ";
            string condition = "";
            if (P_Name != "")
            {
                condition = " where ( p.P_FirstName like N'%" + P_Name + "%' or p.P_FatherName like N'%" + P_Name + "%' or p.P_LastName like N'%" + P_Name + "%' )";
            }
            else if (family_ID != "")
            {
                //condition = " where pf.Family_ID = " + Family_ID;
                condition = " where pf.Family_ID like CAST('" + Family_ID + "' AS CHAR)";
            }
            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            PersonDataGridView.DataSource = MySS.dt;
            DataGridViewColumn dgC1 = PersonDataGridView.Columns["ID"];
            dgC1.Visible = false;
        }

        #endregion binds

        private void FamilyFName_textBox1_TextChanged(object sender, EventArgs e)
        {
            Family_bind(FamilyNum_textBox1.Text, FamilyFName_textBox1.Text);
        }

        private void fnameTxtBox_TextChanged(object sender, EventArgs e)
        {
            FamilyMember_bind("","");
        }

        private void AllFamilies_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.ShowAllForm_ToLight(this);
                else
                    myTheme.ShowAllForm_ToNight(this);

                MySS = new MySqlComponents();
                Family_ID = -1;
                l = new Log();
                Family_bind("", "");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddFamily_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateFamily_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || Family_ID == -1)
                    throw new Exception("Please choose the family you want to update");
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteFamily_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || Family_ID == -1)
                {
                    throw new NoNullAllowedException();
                }
                deleteFamily(Family_ID);
                l.Insert_Log("delete the family " + Family_BookNum, "Family", username, DateTime.Now);
                Family_bind("", "");
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("Please choose the family you want to delete");
            }
        }

        #region mouse hover

        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            AddFamily_button.BackgroundImage = Properties.Resources.add;
        }

        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            AddFamily_button.BackgroundImage = Properties.Resources.add0;
        }

        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateFamily_button.BackgroundImage = Properties.Resources.update;
        }

        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateFamily_button.BackgroundImage = Properties.Resources.update0;
        }

        private void Delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteFamily_button.BackgroundImage = Properties.Resources.delete;
        }

        private void Delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteFamily_button.BackgroundImage = Properties.Resources.delete0;
        }

        #endregion mouse hover

        private void MainBack_button_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void PersonFirstName_textBox_TextChanged(object sender, EventArgs e)
        {
            FamilyMember_bind("",PersonName_textBox.Text);
        }
    }
}