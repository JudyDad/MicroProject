using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class AllBeneficiaries : Form
    {
        public AllBeneficiaries()
        {
            InitializeComponent();
        }
        public AllBeneficiaries(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        MySqlComponents MySS;
        private int Person_ID;
        private string username;
        private Log l;
        public DataRow SelectedDataRow;
        private string Person_Name;

        private void AllBeneficiaries_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.ShowAllForm_ToLight(this);
                else
                    myTheme.ShowAllForm_ToNight(this);

                MySS = new MySqlComponents();
                Person_ID = -1;
                l = new Log();
                Person_bind("", "", "");
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        #region delete and bind
        private void deletePerson(int PID)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "delete From `person` where P_ID =" + PID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        public void Person_bind(string strFname, string strLname, string strnationalnumber)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "select P_ID as 'ID'"
                + ",P_FirstName as 'First Name'"
                + ",P_LastName as 'Last Name'"
                + ",P_FatherName as 'Father Name'"
                + ",P_MotherName as 'Mother Name'"
                + ",P_Sex as 'Gender'"
                + ",P_NationalNumber as 'National Number'"
                + ",P_DOB as 'Birth Date'"
                + ",P_RegistrationPlace as 'Nationality'"
                + ",P_MaritalStatus as 'Marital Status'"
                + ",P_NumAtHome as 'Family members at home'"
                + ",P_IsLivingWithFamily as 'Is Living With another Family'"
                + ",P_MilitaryService as 'In Military Services'"
                + ",P_HomeAddress as 'Home Address'"
                + ",P_HomeTel as 'Land Line'"
                + ",P_Mobile as 'Mobile'"
                + ",IsProjectOwner as 'Project Owner'"
                + ",P_SufferanceOfPerson as 'Suffering'"
                + ",P_SourceOfIncome as 'Source Of Income'"
                + ",P_MedicalCond as 'Medical Conditions'"
                + ",P_FinancialLoss as 'Financial Loss'"
                + ",P_SentimentalLoss as 'Sentimental Loss'"

                + ",CASE P_HomeState WHEN 0 THEN N'Rent' WHEN 1 THEN N'Owner' WHEN 2 THEN N'Host' ELSE N'Other' END as 'Home State'"
                + ",CASE P_ShopState WHEN 0 THEN N'Rent' WHEN 1 THEN N'Owner' WHEN 2 THEN N'Host' ELSE N'Other' END as 'Shop State'"

            //+ ",CASE WHEN P_HomeState = 0 THEN 'أجار'  WHEN P_HomeState = 1 THEN 'ملك' WHEN P_HomeState = 2 THEN 'استضافة' ELSE 'أسباب أخرى' END as 'Home State'"
            //+ ",CASE WHEN P_ShopState = 0 THEN 'أجار' WHEN P_ShopState = 1 THEN 'ملك' WHEN P_ShopState = 2 THEN 'استضافة' ELSE 'أسباب أخرى' END as 'Shop State'"

            //+ ",'Home State' = CASE WHEN P_HomeState = 0 THEN N'أجار' WHEN P_HomeState = 1 THEN N'ملك' WHEN P_HomeState = 2 THEN N'استضافة' ELSE N'أسباب أخرى' END"
            //+ ",'Shop State' =  CASE WHEN P_ShopState = 0 THEN N'أجار' WHEN P_ShopState = 1 THEN N'ملك' WHEN P_ShopState = 2 THEN N'استضافة' ELSE N'أسباب أخرى' END"
            
                + ",CASE P_OtherProperties WHEN 0 THEN N'Car' WHEN 1 THEN N'Land' WHEN 2 THEN N'Rented Property' ELSE N'Other' END as 'Other Properties'"
                + ",CASE P_OtherIncomeSources WHEN 0 THEN N'Ration' WHEN 1 THEN N'Relatives' WHEN 2 THEN N'Aid' ELSE N'Other' END as 'Other Incomes'"
                //+ ",P_MaristesCourse as 'Maristes Course'"
                //+ ",P_OtherCourses as 'Other Courses'"
                + ",P_Parish as 'Parish'"
                + ",P_Priest_ID as 'Priest_ID'"
                + ",Priest_Name as 'Priest'"

                + "\n From `person` left outer join `priest` on priest.Priest_ID = person.P_Priest_ID";

            string condition = " \n where IsProjectOwner like N'YES'";
            if (strFname != "")
            {
                condition += " and P_FirstName like N'" + fnameTxtBox.Text + "%'";
                if (strLname != "")
                {
                    condition += " and P_LastName like N'" + lNameTxtBox.Text + "%'";
                }
                if (strnationalnumber != "")
                {
                    condition += " and P_NationalNumber like N'" + nationalNumberTxtBox.Text + "%'";
                }
            }
            else if (strLname != "")
            {
                condition += " and P_LastName like N'" + lNameTxtBox.Text + "%'";
                if (strnationalnumber != "")
                {
                    condition += " and P_NationalNumber like N'" + nationalNumberTxtBox.Text + "%'";
                }
            }
            else if (strnationalnumber != "")
            {
                condition += " and P_NationalNumber like N'" + nationalNumberTxtBox.Text + "%'";
            }
            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            PersonDataGridView.ColumnHeadersVisible = false;
            PersonDataGridView.DataSource = MySS.dt;
            PersonDataGridView.ColumnHeadersVisible = true;

            DataGridViewColumn dgC1 = PersonDataGridView.Columns["ID"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = PersonDataGridView.Columns["Priest_ID"];
            dgC2.Visible = false;
        }
        #endregion

        private void PersonDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)PersonDataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                Person_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                Person_Name = (string)SelectedDataRow["First Name"] + " " + (string)SelectedDataRow["Father Name"] + " " + (string)SelectedDataRow["Last Name"];
                //this.DialogResult = DialogResult.OK;
            }
        }

        #region search
        private void fnameTxtBox_TextChanged(object sender, EventArgs e)
        {
            Person_bind(fnameTxtBox.Text, lNameTxtBox.Text, nationalNumberTxtBox.Text);
        }
        #endregion

        #region btn clicks
        private void AddBeneficiary_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UpdateBeneficiary_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || Person_ID == -1)
                    throw new Exception("Please choose the beneficiary you want to update");
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message);
            }
        }
        private void DeleteBeneficiary_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this beneficiary permanently ?", "This beneficiary will be deleted with all his data", MessageBoxButtons.YesNo);
            try
            {
                if (dialogResult == DialogResult.Yes)
                {
                    if (SelectedDataRow == null || Person_ID == -1)
                        throw new Exception("Please choose the beneficiary you want to delete");
                    deletePerson(Person_ID);

                    l.Insert_Log("Delete " + Person_Name, " Benefeciary ", username, DateTime.Now);

                    AllBeneficiaries_Load(sender, e);
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
        #endregion

        #region mouse hover
        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            AddBeneficiary_button.BackgroundImage = Properties.Resources.add;
        }
        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            AddBeneficiary_button.BackgroundImage = Properties.Resources.add0;
        }
        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateBeneficiary_button.BackgroundImage = Properties.Resources.update;
        }
        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateBeneficiary_button.BackgroundImage = Properties.Resources.update0;
        }
        private void Delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteBeneficiary_button.BackgroundImage = Properties.Resources.delete;
        }
        private void Delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteBeneficiary_button.BackgroundImage = Properties.Resources.delete0;
        }
        #endregion mouse hover
    }
}