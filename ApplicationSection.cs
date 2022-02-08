using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class ApplicationSection : Form
    {
        public ApplicationSection()
        {
            InitializeComponent();
        }

        public ApplicationSection(string username)
        {
            InitializeComponent();
            this.username = username;
            tabControl.SelectedIndex = 0;
            MySS = new MySqlComponents();
            clear_Person_boxes();
            clear_Project_boxes();
            clear_FamilyMember_boxes();
        }

        MySqlComponents MySS;
        string username;
        private Thread myTh;
        private byte[] PersonPicArr;
        public int Person_ID, MicroProject_ID, Priest_ID;
        private Log l;
        private string imageFilePath, imageName;
        private DataRow MicroProjectOwnerDataRow, SelectedDataRowFM;
        private int ProviderID, FamilyID, FamilyMember_ID;
        private int MicroProjectOwner_ID;
        private string activeInFamily, MP_Owner,relationInFamily, WorkName;
        

        private void InsertingSection_Load(object sender, EventArgs e)
        {
            //////////////////////////////////////////////
            MyTheme myTheme = new MyTheme();
            if (Properties.Settings.Default.theme == "Light")
            {
                myTheme.Application_ToLight(this.tabControl);
            }
            else
            {
                myTheme.Application_ToNight(this.tabControl);
            }
            ////////////////////////////////////////////////////
            MySS = new MySqlComponents();
            l = new Log();
        }

        #region functions
        private void Convert_Picture()
        {
            if (PersonPicture_pictureBox.Image != null)
            {
                PersonPicArr = null;
                FileStream fs = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                PersonPicArr = br.ReadBytes((int)fs.Length);
            }
        }
        private void CallDialog()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            DialogResult res = open.ShowDialog();
            if (res == DialogResult.OK)
            {
                imageFilePath = open.FileName;
                PersonPicture_pictureBox.ImageLocation = imageFilePath;
                imageName = Path.GetFileName(open.FileName);
            }
        }
        public void fill_Person_boxes(DataRow dt)
        {
            Priest_bind();

            if (dt != null)
            {
                Person_ID = Int32.Parse(dt["ID"].ToString());
                PersonFName_textBox.Text = (string)dt["First Name"];
                PersonLName_textBox.Text = (string)dt["Last Name"];
                PersonFatherName_textBox.Text = (string)dt["Father Name"];
                PersonMName_textBox.Text = (string)dt["Mother Name"];
                string P_sex = (string)dt["Gender"];
                if (P_sex.Contains(@"ذكر"))
                    PersonSexMale_radioButton.Checked = true;
                else
                    PersonSexFemale_radioButton.Checked = true;

                if (dt["National Number"] != DBNull.Value)
                    PersonNationalNum_textBox.Text = (string)dt["National Number"];
                else
                    PersonNationalNum_textBox.Text = "لا يوجد";

                PersonDOB_dateTimePicker.Value = (DateTime)dt["Birth Date"];
                PersonRegistration_textBox.Text = (string)dt["Nationality"];
                PersonState_comboBox.Text = (string)dt["Marital Status"];
                PersonNumAtHome_textBox.Text = dt["Family members at home"].ToString();

                string LiveWithAnotherFamily = (string)dt["Is Living With another Family"];
                if (LiveWithAnotherFamily.Equals("Yes"))
                    P_LiveWithAnotherFamily_checkBox.Checked = true;
                else
                    P_LiveWithAnotherFamily_checkBox.Checked = false;

                string Military_Services = (string)dt["In Military Services"];
                if (Military_Services.Equals("Yes"))
                    P_Military_checkBox.Checked = true;
                else
                    P_Military_checkBox.Checked = false;

                PersonHomeAddress_textBox.Text = (string)dt["Home Address"];
                PersonHomeTel_textBox.Text = (string)dt["Land Line"];
                PersonMobile_textBox.Text = (string)dt["Mobile"];

                PersonPicArr = null;
                //check connection//
                Program.buildConnection();

                string strCmd = "select P_Picture from `person` where P_ID = " + Person_ID + " ";
                MySS.sc = new MySqlCommand(strCmd, Program.MyConn);
                MySS.reader = MySS.sc.ExecuteReader();
                MySS.reader.Read();
                if (MySS.reader.HasRows)
                {
                    PersonPicArr = (byte[])(MySS.reader[0]);
                    MySS.reader.Close();
                    if (PersonPicArr == null || PersonPicArr.Length == 0)
                    {
                        PersonPicture_pictureBox.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(PersonPicArr);
                        PersonPicture_pictureBox.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    MessageBox.Show("data not available");
                    MySS.reader.Close();
                }
                P_SufferanceOfPerson_textBox.Text = (string)dt["Suffering"];

                P_SourceOfIncome_textBox.Text = (string)dt["Source Of Income"];
                P_MedicalCond_textBox.Text = (string)dt["Medical Conditions"];
                P_FinancialLoss_textBox.Text = (string)dt["Financial Loss"];
                P_SentimentalLoss_textBox.Text = (string)dt["Sentimental Loss"];

                string HomeState, ShopState, OtherProperties, OtherIncomes;

                HomeState = (string)dt["Home State"];
                if (HomeState == "Rent")
                    P_HomeRent_radioButton.Checked = true;
                else if (HomeState == "Owner")
                    P_HomeOwnership_radioButton.Checked = true;
                else if (HomeState == "Host")
                    P_HomeHost_radioButton.Checked = true;
                else
                    P_HomeOther_radioButton.Checked = true;
                //////////////////////////////////////////////////
                ShopState = (string)dt["Shop State"];
                if (ShopState == "Rent")
                    P_ShopRent_radioButton.Checked = true;
                else if (ShopState == "Owner")
                    P_ShopOwnership_radioButton.Checked = true;
                else if (ShopState == "Host")
                    P_ShopHost_radioButton.Checked = true;
                else
                    P_ShopOther_radioButton.Checked = true;
                //////////////////////////////////////////////////////
                OtherProperties = (string)dt["Other Properties"];
                if (OtherProperties == "Car")
                    P_Car_radioButton.Checked = true;
                else if (OtherProperties == "Land")
                    P_Land_radioButton.Checked = true;
                else if (OtherProperties == "Rented Property")
                    P_Rented_radioButton.Checked = true;
                else
                    P_OtherProperties_radioButton.Checked = true;
                //////////////////////////////////////////////////////
                OtherIncomes = (string)dt["Other Incomes"];
                if (OtherProperties == "Ration")
                    P_SourceRation_radioButton.Checked = true;
                else if (OtherProperties == "Relatives")
                    P_SourceRelatives_radioButton.Checked = true;
                else if (OtherProperties == "Aid")
                    P_SourceAid_radioButton.Checked = true;
                else
                    P_SourceOther_radioButton.Checked = true;
                ////////////////////////////////////////////////////////
                //string Maristes_Course = (string)dt["Maristes Course"];
                //if (Maristes_Course.Equals("Yes"))
                //    P_MaristesCourse_checkBox.Checked = true;
                //else
                //    P_MaristesCourse_checkBox.Checked = false;

                //P_OtherCourses_textBox.Text = (string)dt["Other Courses"];
                P_Parish_comboBox.Text = (string)dt["Parish"];
                //////////////////////////////////////////////////////////////////
                if (dt["Priest"].ToString() != null)
                {
                    P_Priest_comboBox.Text = (string)dt["Priest"].ToString();
                    Priest_ID = Convert.ToInt32(dt["Priest_ID"].ToString());
                    //check connection//
                    Program.buildConnection();

                    MySS.query = "select Priest_ID from `priest` where Priest_Name like N'%" + P_Priest_comboBox.Text + "%'";
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    object result = MySS.sc.ExecuteScalar();
                    result = (result == DBNull.Value) ? null : result;
                    int countDis = Convert.ToInt32(result);

                    int Selected = -1;
                    int count = P_Priest_comboBox.Items.Count;
                    for (int i = 0; (i <= (count - 1)); i++)
                    {
                        P_Priest_comboBox.SelectedIndex = i;
                        if ((string)(P_Priest_comboBox.SelectedValue) == countDis.ToString())
                        {
                            Selected = i;
                        }
                    }
                    P_Priest_comboBox.SelectedIndex = Selected;
                    P_Priest_comboBox.SelectedValue = countDis;
                }
            }
        }
        public void fill_Project_boxes(DataRow dr)
        {
            if (dr != null)
            {
                
                Category_bind();
                Level_bind();

                MicroProject_ID = Int32.Parse(dr["MicroProject_ID"].ToString());
                MPID_textBox.Text = dr["MicroProject_ID"].ToString();
                MP_Country_textBox.Text = (string)dr["Country"];
                MP_ClientsAndContinuance_textBox.Text = (string)dr["Clients And Continuance"];
                MP_City_textBox.Text = (string)dr["City"];
                MP_DateOfRequest_dateTimePicker.Value = (DateTime)dr["Application Date"];
                MP_Name_textBox.Text = (string)dr["Project Name"];
                MP_AllPriceNeeded_textBox.Text = (string)dr["Requested Amount"];
                MP_PeriodOfExecution_textBox.Text = (string)dr["Timeline"];
                MP_Describtion_textBox.Text = (string)dr["Description"];
                MP_ResonOfProject_textBox.Text = (string)dr["Reason of the Project"];

                string NeedLicense = (string)dr["needs a license"];
                if (NeedLicense.Equals("Yes"))
                    MP_NeedLicense_radioButton.Checked = true;
                else
                    MP_DontNeedLicense_radioButton.Checked = true;
                MP_LicenseSide_textBox.Text = (string)dr["License Side"];
                MP_PlaceOfExecution_textBox.Text = dr["Place of Project"].ToString();


                MP_OtherInformation_textBox.Text = (string)dr["Other Comments"];
                int num = (int)dr["Minimal Profit"];
                MP_SimpleProfit_textBox.Text = num.ToString();

                if (dr["Level"].ToString() != null)
                {
                    Level_comboBox.Text = (string)dr["Level"].ToString();
                    //check connection//
                    Program.buildConnection();
                    
                    MySS.query = "select Level_ID from `level` where Level_Symbol like N'%" + Level_comboBox.Text + "%'";
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    object result = MySS.sc.ExecuteScalar();
                    result = (result == DBNull.Value) ? null : result;
                    int countDis = Convert.ToInt32(result);

                    int Selected = -1;
                    int count = Level_comboBox.Items.Count;
                    for (int i = 0; (i <= (count - 1)); i++)
                    {
                        Level_comboBox.SelectedIndex = i;
                        if ((string)(Level_comboBox.SelectedValue) == countDis.ToString())
                        {
                            Selected = i;
                        }
                    }
                    Level_comboBox.SelectedIndex = Selected;
                    Level_comboBox.SelectedValue = countDis;
                }
                if (dr["Category"].ToString() != null)
                {
                    MP_Category_comboBox.Text = (string)dr["Category"].ToString();
                    //check connection//
                    Program.buildConnection();
                    
                    MySS.query = "select C_ID from `category` where C_Name like N'%" + MP_Category_comboBox.Text + "%'";
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    object result = MySS.sc.ExecuteScalar();
                    result = (result == DBNull.Value) ? null : result;
                    int countDis = Convert.ToInt32(result);

                    int Selected = -1;
                    int count = MP_Category_comboBox.Items.Count;
                    for (int i = 0; (i <= (count - 1)); i++)
                    {
                        MP_Category_comboBox.SelectedIndex = i;
                        if ((string)(MP_Category_comboBox.SelectedValue) == countDis.ToString())
                        {
                            Selected = i;
                        }
                    }
                    MP_Category_comboBox.SelectedIndex = Selected;
                    MP_Category_comboBox.SelectedValue = countDis;
                }
            }
        }
        private void fill_Family_boxes(DataRow dr)
        {
            if (dr != null)
            {
                FamilyNum_textBox.Text = (string)dr["Book Number"];
                FamilyFName_textBox.Text = (string)dr["First Name"];
                FamilyLName_textBox.Text = (string)dr["Last Name"];
                FamilyID = Int32.Parse(dr["ID"].ToString());

                Family_FamilyMember_bind(FamilyID.ToString());
            }
            provider_bind();
        }
        public int SelectCurrentPerson()
        {
            //check connection//
             Program.buildConnection();
            
            //MySS.query = "select IDENT_CURRENT('Person')";
            MySS.query = "select MAX(P_ID) from `person`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out Person_ID);
            return Person_ID;
        }
        private void clear_Person_boxes()
        {
            PersonFName_textBox.Text = PersonLName_textBox.Text = PersonFatherName_textBox.Text = PersonMName_textBox.Text =
               PersonNationalNum_textBox.Text = PersonRegistration_textBox.Text =
               PersonHomeAddress_textBox.Text = PersonHomeTel_textBox.Text = PersonMobile_textBox.Text = "";
            PersonNumAtHome_textBox.Text = "";
            PersonState_bind();
            PersonPicture_pictureBox.Image = null;

            P_SufferanceOfPerson_textBox.Text = P_SourceOfIncome_textBox.Text = P_MedicalCond_textBox.Text = P_FinancialLoss_textBox.Text = P_SentimentalLoss_textBox.Text = "";
            
            InsertPerson_button.Visible = true;
            UpdatePerson_button.Visible = false;

            //P_OtherCourses_textBox.Text =
            P_Parish_comboBox.Text = "";
            //P_MaristesCourse_checkBox.Checked = 
            P_Military_checkBox.Checked = P_LiveWithAnotherFamily_checkBox.Checked = false;

            P_Priest_comboBox.SelectedIndex = -1;
        }
        private void clear_Project_boxes()
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "select MAX(MP_ID) from `microproject`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            int n = 0;
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out n);
            MPID_textBox.Text = (++n).ToString();
            MPID_textBox.Enabled = false;

            MP_Country_textBox.Text = "سوريا";
            MP_City_textBox.Text = "حلب";
            MP_ClientsAndContinuance_textBox.Text = MP_Name_textBox.Text = MP_AllPriceNeeded_textBox.Text
                = MP_PeriodOfExecution_textBox.Text = MP_Describtion_textBox.Text = MP_ResonOfProject_textBox.Text
                = MP_LicenseSide_textBox.Text = MP_PlaceOfExecution_textBox.Text
                = MP_SimpleProfit_textBox.Text = MP_OtherInformation_textBox.Text = "";
            MP_DateOfRequest_dateTimePicker.Value = DateTime.Now;
            MP_IdentityInsert_checkBox.Checked = false;
            Level_comboBox.SelectedIndex = MP_Category_comboBox.SelectedIndex = -1;


            AddProject_button.Visible = true;
            UpdateProject_button.Visible = false;
        }
        private void clear_FamilyMember_boxes()
        {
            FPersonFName_textBox.Text = FPersonLName_textBox.Text = FPersonFatherName_textBox.Text = FPersonWorkDescribtion_textBox.Text = "";
            FPersonIsProvider_checkBox.Checked = false;
            FPersonDOB_dateTimePicker.Value = DateTime.Now;
            FPersonSexMale_radioButton.Checked = FPersonSexFemale_radioButton.Checked = false;

            PersonState_bind();
            FProviderID_listBox.SelectedIndex = -1;

            FamilyMember_ID = -1;
            ProviderID = -1;

            InsertFamily_button.Visible = true;
            UpdateFamily_button.Visible = false;
        }

        #endregion

        #region bind methods
        public void PersonState_bind()
        {
            PersonState_comboBox.Items.Clear();
            FPersonState_comboBox.Items.Clear();

            if (PersonSexFemale_radioButton.Checked)
            {
                PersonState_comboBox.Items.Add("عازبة");
                PersonState_comboBox.Items.Add("متزوجة");
                PersonState_comboBox.Items.Add("مطلقة");
                PersonState_comboBox.Items.Add("أرملة");
                PersonState_comboBox.Items.Add("منفصلة");
                PersonState_comboBox.Items.Add("مخطوبة");
            }
            else
            {
                PersonState_comboBox.Items.Add("عازب");
                PersonState_comboBox.Items.Add("متزوج");
                PersonState_comboBox.Items.Add("مطلق");
                PersonState_comboBox.Items.Add("أرمل");
                PersonState_comboBox.Items.Add("منفصل");
                PersonState_comboBox.Items.Add("مخطوب");
            }
            if (FPersonSexFemale_radioButton.Checked)
            {
                FPersonState_comboBox.Items.Add("عازبة");
                FPersonState_comboBox.Items.Add("متزوجة");
                FPersonState_comboBox.Items.Add("مطلقة");
                FPersonState_comboBox.Items.Add("أرملة");
                FPersonState_comboBox.Items.Add("منفصلة");
                FPersonState_comboBox.Items.Add("مخطوبة");
            }
            else
            {
                FPersonState_comboBox.Items.Add("عازب");
                FPersonState_comboBox.Items.Add("متزوج");
                FPersonState_comboBox.Items.Add("مطلق");
                FPersonState_comboBox.Items.Add("أرمل");
                FPersonState_comboBox.Items.Add("منفصل");
                FPersonState_comboBox.Items.Add("مخطوب");
            }
        }
        private void provider_bind()
        {
            try
            {
                //check connection//
                Program.buildConnection();

                MySS.da = new MySqlDataAdapter("select CONCAT(P_FirstName, ' ', P_FatherName, ' ', P_LastName) as P_Name ,  P_ID from `person`", Program.MyConn);
                DataSet ds = new DataSet();
                MySS.da.Fill(ds);

                FProviderID_listBox.DataSource = ds.Tables[0];
                FProviderID_listBox.DisplayMember = "P_Name";
                FProviderID_listBox.ValueMember = "P_ID";
                FProviderID_listBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Family_FamilyMember_bind(string FamilyNum)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = @"select CONCAT(P1.P_FirstName, ' ', P1.P_FatherName, ' ', P1.P_LastName) as 'Beneficiary Name'
,F.F_Number as 'Book Number'
,PF.Family_ID as 'Family_ID'
,PF.Person_ID 'Person_ID'
,P1.P_MaritalStatus as 'Marital Status'
,PF.IsInNow as 'IsInNow'
,PF.Relation as 'Relation'
,PF.Work_Name as 'Work'
,PF.P_Provider_ID as 'Provider_ID'
,CONCAT(P2.P_FirstName, ' ', P2.P_LastName) as 'Provider Name'
 from `person_family` PF left outer join `family` F on PF.Family_ID = F.F_ID
							  left outer join `person` P1 on PF.Person_ID = P1.P_ID
							  left outer join `person` P2 on PF.P_Provider_ID = P2.P_ID ";
            string condition = "";
            if (FamilyNum != "")
            {
                condition = " where PF.Family_ID = " + Int32.Parse(FamilyNum) + " ";
            }
            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            FamilyMembers_DataGridView.DataSource = MySS.dt;
            DataGridViewColumn dgC2 = FamilyMembers_DataGridView.Columns["Person_ID"];
            dgC2.Visible = false;
            DataGridViewColumn dgC3 = FamilyMembers_DataGridView.Columns["Provider_ID"];
            dgC3.Visible = false;
            DataGridViewColumn dgC4 = FamilyMembers_DataGridView.Columns["Family_ID"];
            dgC4.Visible = false;
        }
        private void Level_bind()
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "select Level_ID,Level_Symbol from `level`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.reader = MySS.sc.ExecuteReader();
            MySS.dt = new DataTable();
            MySS.dt.Columns.Add("Level_ID", typeof(string));
            MySS.dt.Columns.Add("Level_Symbol", typeof(string));
            MySS.dt.Load(MySS.reader);
            Level_comboBox.DisplayMember = "Level_Symbol";
            Level_comboBox.ValueMember = "Level_ID";
            Level_comboBox.DataSource = MySS.dt;
        }
        private void Category_bind()
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "select C_ID,C_Name from `category`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.reader = MySS.sc.ExecuteReader();
            MySS.dt = new DataTable();
            MySS.dt.Columns.Add("C_ID", typeof(string));
            MySS.dt.Columns.Add("C_Name", typeof(string));
            MySS.dt.Load(MySS.reader);
            MP_Category_comboBox.DisplayMember = "C_Name";
            MP_Category_comboBox.ValueMember = "C_ID";
            MP_Category_comboBox.DataSource = MySS.dt;
        }
        private void Priest_bind()
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "select Priest_ID,Priest_Name from `priest`";
            string orderby = " order by Priest_Name";
            MySS.query += orderby;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.reader = MySS.sc.ExecuteReader();
            MySS.dt = new DataTable();
            MySS.dt.Columns.Add("Priest_ID", typeof(string));
            MySS.dt.Columns.Add("Priest_Name", typeof(string));
            MySS.dt.Load(MySS.reader);
            P_Priest_comboBox.DisplayMember = "Priest_Name";
            P_Priest_comboBox.ValueMember = "Priest_ID";
            P_Priest_comboBox.DataSource = MySS.dt;
        }


        #endregion

        #region insert
        private void insertPerson(byte[] PersonPicArr)
        {
            int home_state, shop_state,other_properties, other_incomes;

            if (P_HomeRent_radioButton.Checked)
            { home_state = 0; }                 //أجار
            else if (P_HomeOwnership_radioButton.Checked)
            { home_state = 1; }                 //ملك
            else if (P_HomeHost_radioButton.Checked)
            { home_state = 2; }                 //استضافة
            else
            { home_state = 3; }                 //other

            if (P_ShopRent_radioButton.Checked)
            { shop_state = 0; }                 //أجار
            else if (P_ShopOwnership_radioButton.Checked)
            { shop_state = 1; }                 //ملك
            else if (P_ShopHost_radioButton.Checked)
            { shop_state = 2; }                 //استضافة
            else
            { shop_state = 3; }                 //other

            if (P_Car_radioButton.Checked)
            { other_properties = 0; }                 //سيارة
            else if (P_Land_radioButton.Checked)
            { other_properties = 1; }                 //أرض
            else if (P_Rented_radioButton.Checked)
            { other_properties = 2; }                 //ملك مؤجر
            else
            { other_properties = 3; }                 //other

            if (P_SourceRation_radioButton.Checked)
            { other_incomes = 0; }                 //معونة
            else if (P_SourceRelatives_radioButton.Checked)
            { other_incomes = 1; }                 //أقارب
            else if (P_SourceAid_radioButton.Checked)
            { other_incomes = 2; }                 //مساعدات
            else
            { other_incomes = 3; }                 //other

            MySS.query = "Insert Into `person`(`P_FirstName`, `P_LastName`, `P_FatherName`, `P_MotherName`, `P_DOB`, `P_MilitaryService`, `P_Mobile`, `P_HomeTel`, `P_HomeAddress`, `P_NationalNumber`, `P_RegistrationPlace`, `P_Sex`, `P_MaritalStatus`, `P_IsLivingWithFamily`, `P_NumAtHome`, `P_Picture`, `IsProjectOwner`, `P_SufferanceOfPerson`, `P_SourceOfIncome`, `P_MedicalCond`, `P_FinancialLoss`, `P_SentimentalLoss`, `P_HomeState`, `P_ShopState`,"
                + "`P_OtherProperties`,`P_OtherIncomeSources`,`P_MaristesCourse`,`P_OtherCourses`,`P_Parish`,`P_Priest_ID`) values(N'"
                    + PersonFName_textBox.Text + "',N'"
                    + PersonLName_textBox.Text + "',N'"
                    + PersonFatherName_textBox.Text + "',N'"
                    + PersonMName_textBox.Text + "','"
                    //+ PersonDOB_dateTimePicker.Value.Month.ToString() + "/"
                    //+ PersonDOB_dateTimePicker.Value.Day.ToString() + "/"
                    //+ PersonDOB_dateTimePicker.Value.Year.ToString() + "',N'"

                    + PersonDOB_dateTimePicker.Value.Year.ToString() + "/"
                    + PersonDOB_dateTimePicker.Value.Month.ToString() + "/"
                    + PersonDOB_dateTimePicker.Value.Day.ToString() + "',N'"
                     + (P_Military_checkBox.Checked ? "Yes" : "No") + "','"
                    //+ PersonEmail_textBox.Text + "',N'"
                    + PersonMobile_textBox.Text + "',N'"
                    + PersonHomeTel_textBox.Text + "',N'"
                    + PersonHomeAddress_textBox.Text + "',N'"
                    + PersonNationalNum_textBox.Text + "',N'"
                    + PersonRegistration_textBox.Text + "',N'"
                    + (PersonSexMale_radioButton.Checked ? "ذكر" : "أنثى") + "',N'"
                    + PersonState_comboBox.Text + "','"
                    + (P_LiveWithAnotherFamily_checkBox.Checked ? "Yes'" : "No'") + ",'"
                    + PersonNumAtHome_textBox.Text + "',"
                    + "@PersonPicArr" + ",N'"
                    + "YES',N'"         //يملك مشروع

                + P_SufferanceOfPerson_textBox.Text + "',N'"
                + P_SourceOfIncome_textBox.Text + "',N'"
                + P_MedicalCond_textBox.Text + "',N'"
                + P_FinancialLoss_textBox.Text + "',N'"
                + P_SentimentalLoss_textBox.Text + "',"
                + home_state + ","
                + shop_state + ","
                ////////////////////////////////////// new ////////////////////////////////////
                + other_properties + ","
                + other_incomes + ",'"
                + " " + "',N'"          //maristes course
                + " " + "',N'"          //other courses

                + P_Parish_comboBox.Text + "',"
                + (Priest_ID == -1 ? System.Data.SqlTypes.SqlInt32.Null : Priest_ID) + " )";


            //check connection//
            Program.buildConnection();
            
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.Parameters.Add(new MySqlParameter("@PersonPicArr", PersonPicArr));
            MySS.sc.ExecuteNonQuery();
        }
        private void insertPerson()
        {
            int home_state, shop_state,other_properties,other_incomes;

            if (P_HomeRent_radioButton.Checked)
            { home_state = 0; }                 //أجار
            else if (P_HomeOwnership_radioButton.Checked)
            { home_state = 1; }                 //ملك
            else if (P_HomeHost_radioButton.Checked)
            { home_state = 2; }                 //استضافة
            else
            { home_state = 3; }                 //other

            if (P_ShopRent_radioButton.Checked)
            { shop_state = 0; }                 //أجار
            else if (P_ShopOwnership_radioButton.Checked)
            { shop_state = 1; }                 //ملك
            else if (P_ShopHost_radioButton.Checked)
            { shop_state = 2; }                 //استضافة
            else
            { shop_state = 3; }                 //other

            if (P_Car_radioButton.Checked)
            { other_properties = 0; }                 //سيارة
            else if (P_Land_radioButton.Checked)
            { other_properties = 1; }                 //أرض
            else if (P_Rented_radioButton.Checked)
            { other_properties = 2; }                 //ملك مؤجر
            else
            { other_properties = 3; }                 //other

            if (P_SourceRation_radioButton.Checked)
            { other_incomes = 0; }                 //معونة
            else if (P_SourceRelatives_radioButton.Checked)
            { other_incomes = 1; }                 //أقارب
            else if (P_SourceAid_radioButton.Checked)
            { other_incomes = 2; }                 //مساعدات
            else
            { other_incomes = 3; }                 //other


            //date in mySql is like yyyy-MM-dd
            // and in sql           MM-dd-yyyy

            MySS.query = "Insert Into `person`(`P_FirstName`, `P_LastName`, `P_FatherName`, `P_MotherName`, `P_DOB`, `P_MilitaryService`, `P_Mobile`, `P_HomeTel`,"
                + "`P_HomeAddress`, `P_NationalNumber`, `P_RegistrationPlace`, `P_Sex`, `P_MaritalStatus`, `P_IsLivingWithFamily`, `P_NumAtHome`, `P_Picture`,"
                + "`IsProjectOwner`, `P_SufferanceOfPerson`, `P_SourceOfIncome`, `P_MedicalCond`, `P_FinancialLoss`, `P_SentimentalLoss`, `P_HomeState`, `P_ShopState`,"
                + "`P_OtherProperties`,`P_OtherIncomeSources`,`P_MaristesCourse`,`P_OtherCourses`,`P_Parish`,`P_Priest_ID`) values(N'"
                + PersonFName_textBox.Text + "',N'"
                + PersonLName_textBox.Text + "',N'"
                + PersonFatherName_textBox.Text + "',N'"
                + PersonMName_textBox.Text + "','"
                //+ PersonDOB_dateTimePicker.Value.Month.ToString() + "/"
                //+ PersonDOB_dateTimePicker.Value.Day.ToString() + "/"
                //+ PersonDOB_dateTimePicker.Value.Year.ToString() + "',N'"

                + PersonDOB_dateTimePicker.Value.Year.ToString() + "/"
                + PersonDOB_dateTimePicker.Value.Month.ToString() + "/"
                + PersonDOB_dateTimePicker.Value.Day.ToString() + "',N'"
                + (P_Military_checkBox.Checked ? "Yes" : "No") + "','"
                //+ PersonEmail_textBox.Text + "',N'"
                + PersonMobile_textBox.Text + "',N'"
                + PersonHomeTel_textBox.Text + "',N'"
                + PersonHomeAddress_textBox.Text + "',N'"
                + PersonNationalNum_textBox.Text + "',N'"
                + PersonRegistration_textBox.Text + "',N'"
                + (PersonSexMale_radioButton.Checked ? "ذكر" : "أنثى") + "',N'"
                + PersonState_comboBox.Text + "','"
                + (P_LiveWithAnotherFamily_checkBox.Checked ? "Yes" : "No") + "','"
                + PersonNumAtHome_textBox.Text + "','"
                + null + "',N'"
                + "YES',N'"         //يملك مشروع
                + P_SufferanceOfPerson_textBox.Text + "',N'"
                + P_SourceOfIncome_textBox.Text + "',N'"
                + P_MedicalCond_textBox.Text + "',N'"
                + P_FinancialLoss_textBox.Text + "',N'"
                + P_SentimentalLoss_textBox.Text + "',"
                + home_state + ","
                + shop_state + ","
                ////////////////////////////////////// new ////////////////////////////////////
                + other_properties + ","
                + other_incomes + ",'"
                + " " + "',N'"          //maristes course
                + " " + "',N'"          //other courses

                + P_Parish_comboBox.Text + "',"
                + (Priest_ID == -1 ? System.Data.SqlTypes.SqlInt32.Null : Priest_ID) + " )";
            
            //check connection//
             Program.buildConnection();
            
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void insertMicroProject()
        {
            string reasonOfProject = "";
            if (MP_ImproveIncome_checkBox.Checked)
                reasonOfProject = MP_ImproveIncome_checkBox.Text;
            else if (MP_AdditionalIncome_checkBox.Checked)
                reasonOfProject = MP_AdditionalIncome_checkBox.Text;
            else if (MP_ExpandProject_checkBox.Checked)
                reasonOfProject = MP_ExpandProject_checkBox.Text;
            else reasonOfProject = MP_ResonOfProject_textBox.Text;

            MySS.query = "Insert Into `microproject`(`MP_Country`, `MP_ClientsAndContinuance`, `MP_City`, `MP_DateOfRequest`, `MP_Name`, `MP_AllPriceNeeded`, `MP_PeriodOfExecution`, `MP_Describtion`, `MP_ResonOfProject`, `MP_IsNeedLicense`, `MP_LicenseSide`, `MP_PlaceOfExecution`, `MP_ResonOfPlace`, `MP_SimpleProfit`, `MP_OtherComment`, `MP_State`, `MP_StateReason`, `MP_StateComment`, `MP_Level_ID`, `MP_Category_ID`, `MP_Priest_ID`) values(N'"
                + MP_Country_textBox.Text + "',N'"
                + MP_ClientsAndContinuance_textBox.Text + "',N'"
                + MP_City_textBox.Text + "','"
                //+ MP_DateOfRequest_dateTimePicker.Value.Month.ToString() + "/"
                //+ MP_DateOfRequest_dateTimePicker.Value.Day.ToString() + "/"
                //+ MP_DateOfRequest_dateTimePicker.Value.Year.ToString() + "',N'"
                
                + MP_DateOfRequest_dateTimePicker.Value.Year.ToString() + "/"  
                + MP_DateOfRequest_dateTimePicker.Value.Month.ToString() + "/"
                + MP_DateOfRequest_dateTimePicker.Value.Day.ToString() + "',N'"

                + MP_Name_textBox.Text + "',N'"
                + MP_AllPriceNeeded_textBox.Text + "',N'"
                + MP_PeriodOfExecution_textBox.Text + "',N'"
                + MP_Describtion_textBox.Text + "',N'"
                + reasonOfProject + "',N'"

                + (MP_NeedLicense_radioButton.Checked ? "Yes" : "No") + "',N'"
                + MP_LicenseSide_textBox.Text + "',N'"
                + MP_PlaceOfExecution_textBox.Text + "',N'"
            //  + MP_ResonOfPlace_textBox.Text + "',"
            //   + replaceQuotation(FundedBy_comboBox.Text) + "',"
                + "None" + "',"
                + MP_SimpleProfit_textBox.Text + ",N'"
                + MP_OtherInformation_textBox.Text + "',"
                + 3 + ",N'"
                //+ status + ",N'"
                + " " + "',N'"
                + " " + "',"
                //+ MP_StatusComment_textBox.Text + "',"
                + (Level_comboBox.SelectedValue == null ? System.Data.SqlTypes.SqlInt32.Null : Int32.Parse(Level_comboBox.SelectedValue.ToString())) + ","
                + (MP_Category_comboBox.SelectedValue == null ? System.Data.SqlTypes.SqlInt32.Null : Int32.Parse(MP_Category_comboBox.SelectedValue.ToString())) + ","
                + (P_Priest_comboBox.SelectedValue == null ? System.Data.SqlTypes.SqlInt32.Null : Int32.Parse(P_Priest_comboBox.SelectedValue.ToString())) + " )";

            //check connection//
            Program.buildConnection();
            
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void insertMicroProject_withIdentity()
        {
            string reasonOfProject = "";
            if (MP_ImproveIncome_checkBox.Checked)
                reasonOfProject = MP_ImproveIncome_checkBox.Text;
            else if (MP_AdditionalIncome_checkBox.Checked)
                reasonOfProject = MP_AdditionalIncome_checkBox.Text;
            else if (MP_ExpandProject_checkBox.Checked)
                reasonOfProject = MP_ExpandProject_checkBox.Text;
            else reasonOfProject = MP_ResonOfProject_textBox.Text;

            MySS.query =
                //"SET IDENTITY_INSERT MicroProject ON "
                 " Insert Into `microproject`(`MP_ID`, `MP_Country`, `MP_ClientsAndContinuance`, `MP_City`, `MP_DateOfRequest`, `MP_Name`, `MP_AllPriceNeeded`, `MP_PeriodOfExecution`, `MP_Describtion`, `MP_ResonOfProject`, `MP_IsNeedLicense`, `MP_LicenseSide`, `MP_PlaceOfExecution`, `MP_ResonOfPlace`, `MP_SimpleProfit`, `MP_OtherComment`, `MP_State`, `MP_StateReason`, `MP_StateComment`, `MP_Level_ID`, `MP_Category_ID`, `MP_Priest_ID`) values("
                + Int32.Parse(MPID_textBox.Text.ToString()) + ",N'"
                + MP_Country_textBox.Text + "',N'"
                + MP_ClientsAndContinuance_textBox.Text + "',N'"
                + MP_City_textBox.Text + "','"
                //+ MP_DateOfRequest_dateTimePicker.Value.Month.ToString() + "/"
                //        + MP_DateOfRequest_dateTimePicker.Value.Day.ToString() + "/"
                //        + MP_DateOfRequest_dateTimePicker.Value.Year.ToString() + "',N'"
                + MP_DateOfRequest_dateTimePicker.Value.Year.ToString() + "/"
                + MP_DateOfRequest_dateTimePicker.Value.Month.ToString() + "/"
                + MP_DateOfRequest_dateTimePicker.Value.Day.ToString() + "',N'"

                + MP_Name_textBox.Text + "',N'"
                + MP_AllPriceNeeded_textBox.Text + "',N'"
                + MP_PeriodOfExecution_textBox.Text + "',N'"
                + MP_Describtion_textBox.Text + "',N'"
                + reasonOfProject + "',N'"
                + (MP_NeedLicense_radioButton.Checked ? "Yes" : "No") + "',N'"
                + MP_LicenseSide_textBox.Text + "',N'"
                + MP_PlaceOfExecution_textBox.Text + "',N'"
               // + MP_ResonOfPlace_textBox.Text + "',"
               // + replaceQuotation(FundedBy_comboBox.Text) + "',"
                + "None" + "',"
                + Int32.Parse(MP_SimpleProfit_textBox.Text.ToString()) + ",N'"
                + MP_OtherInformation_textBox.Text + "',"
                + 3 + ",N'"
                //+ status + ",N'"
                + " " + "',N'"
                //+ MP_StatusComment_textBox.Text + "',"
                + " " + "',"
                + (Level_comboBox.SelectedValue == null ? System.Data.SqlTypes.SqlInt32.Null : Int32.Parse(Level_comboBox.SelectedValue.ToString())) + " ,"
                + (MP_Category_comboBox.SelectedValue == null ? System.Data.SqlTypes.SqlInt32.Null : Int32.Parse(MP_Category_comboBox.SelectedValue.ToString())) + " ,"
                + (P_Priest_comboBox.SelectedValue == null ? System.Data.SqlTypes.SqlInt32.Null : Int32.Parse(P_Priest_comboBox.SelectedValue.ToString())) + " )";

            //     + "\nSET IDENTITY_INSERT  MicroProject OFF";

            //check connection//
             Program.buildConnection();
            
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
       
        private void insertFamily()
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Insert Into `family`(`F_FirstName`, `F_LastName`, `F_FatherName`, `F_Number`) values(N'"
                        + FamilyFName_textBox.Text + "',N'"
                        + FamilyLName_textBox.Text + "',N'"
                        + "  " + "',N'"
                        + FamilyNum_textBox.Text + "' )";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        public void insertFamilyMember()
        {
            //check connection//
             Program.buildConnection();
            
            MySS.query = "Insert Into `person`(`P_FirstName`, `P_LastName`, `P_FatherName`, `P_MotherName`, `P_DOB`, `P_MilitaryService`, `P_Mobile`, `P_HomeTel`, `P_HomeAddress`, `P_NationalNumber`, `P_RegistrationPlace`, `P_Sex`, `P_MaritalStatus`, `P_IsLivingWithFamily`, `P_NumAtHome`, `P_Picture`, `IsProjectOwner`, `P_SufferanceOfPerson`, `P_SourceOfIncome`, `P_MedicalCond`, `P_FinancialLoss`, `P_SentimentalLoss`, `P_HomeState`, `P_ShopState`,"
                  + "`P_OtherProperties`,`P_OtherIncomeSources`,`P_MaristesCourse`,`P_OtherCourses`,`P_Parish`,`P_Priest_ID`) values(N'"
                + FPersonFName_textBox.Text + "',N'"
                + (FPersonLName_textBox.Text != "" ? FPersonLName_textBox.Text : "") + "',N'"
                + (FPersonFatherName_textBox.Text != "" ? FPersonFatherName_textBox.Text : "") + "',N'"
                + "" + "','"       //Mother Name

                //+ FPersonDOB_dateTimePicker.Value.Month.ToString() + "/"+ FPersonDOB_dateTimePicker.Value.Day.ToString() + "/"+ FPersonDOB_dateTimePicker.Value.Year.ToString() + "',N'"

                + FPersonDOB_dateTimePicker.Value.Year.ToString() + "/"
                + FPersonDOB_dateTimePicker.Value.Month.ToString() + "/"
                + FPersonDOB_dateTimePicker.Value.Day.ToString() + "',N'"

                + "" + "',N'"      //Military service
                + "" + "','"       // Mobile
                + "" + "',N'"      //HomeTel
                + "" + "',"        //HomeAdd
                + "null" + ",N'"    //NationalNumber
                + "" + "',N'"      //RegistrationPlace
                + (FPersonSexMale_radioButton.Checked ? "ذكر" : "أنثى") + "',N'"
                + FPersonState_comboBox.Text + "','"
                + "" + "','"       //LiveWithAnotherFamily
                + "00" + "','"      //NumAtHome
                + null + "',N'"     //picture
                + "NO" + "',N'"     //يملك مشروع
                
                + null + "',N'"     //SufferanceOfPerson
                + null + "',N'"     //SourceOfIncome
                + null + "',N'"     //MedicalCondition
                + null + "',N'"     //FinancialLoss
                + null + "',"       //SentimentalLoss
                + 3 + ","           //home_state
                + 3 + ","           //shop_state
                
                ///////////////////new///////////////////

                + 3 + ","           //OtherProperties
                + 3 + ",'"          //OtherIncomeSources
                + "" + "',N'"      //MaristesCourse
                + "" + "',N'"      //OtherCourses
                + "" + "',"        //Parish
                + System.Data.SqlTypes.SqlInt32.Null    //Priest_ID
                + " )";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        public void insertFamilyPersonDetails(int fNo, int pNo, string isInNow, string relation, int providerID, string WorkName)
        {
            //check connection//
             Program.buildConnection();
            
            MySS.query = "Insert Into `person_family`(`Family_ID`, `Person_ID`, `IsInNow`, `Relation`, `P_Provider_ID`, `Work_Name`) values("
                                      + "(select F_ID from `Family` where F_ID = " + fNo + "),"
                                      + "(select P_ID from `Person` where P_ID = " + pNo + "), N'"
                                      + isInNow + "',N'"
                                      + relation + "',"
                                      + "(select P_ID from `person` where P_ID =  " + providerID + "), N'"
                                      + WorkName + "' "
                                      + ")";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        #endregion

        #region updates
        private void updatePerson(int PID)
        {
            int home_state, shop_state, other_properties, other_incomes;
            if (P_HomeRent_radioButton.Checked)
            { home_state = 0; }                 //أجار
            else if (P_HomeOwnership_radioButton.Checked)
            { home_state = 1; }                 //ملك
            else if (P_HomeHost_radioButton.Checked)
            { home_state = 2; }                 //استضافة
            else
            { home_state = 3; }                 //other

            if (P_ShopRent_radioButton.Checked)
            { shop_state = 0; }                 //أجار
            else if (P_ShopOwnership_radioButton.Checked)
            { shop_state = 1; }                 //ملك
            else if (P_ShopHost_radioButton.Checked)
            { shop_state = 2; }                 //استضافة
            else
            { shop_state = 3; }                 //other

            if (P_Car_radioButton.Checked)
            { other_properties = 0; }                 //سيارة
            else if (P_Land_radioButton.Checked)
            { other_properties = 1; }                 //أرض
            else if (P_Rented_radioButton.Checked)
            { other_properties = 2; }                 //ملك مؤجر
            else
            { other_properties = 3; }                 //other

            if (P_SourceRation_radioButton.Checked)
            { other_incomes = 0; }                 //معونة
            else if (P_SourceRelatives_radioButton.Checked)
            { other_incomes = 1; }                 //أقارب
            else if (P_SourceAid_radioButton.Checked)
            { other_incomes = 2; }                 //مساعدات
            else
            { other_incomes = 3; }                 //other

            

            MySS.query = "Update `person` set "
                + "P_FirstName = N'" + PersonFName_textBox.Text + "'"
                + ",P_LastName = N'" + PersonLName_textBox.Text + "'"
                + ",P_FatherName = N'" + PersonFatherName_textBox.Text + "'"
                + ",P_MotherName = N'" + PersonMName_textBox.Text + "'"
                + ",P_Sex = N'" + (PersonSexMale_radioButton.Checked ? "ذكر" : "أنثى") + "'"
                + ",P_NationalNumber = N'" + PersonNationalNum_textBox.Text + "'"
                //+ ",P_DOB = N'" + PersonDOB_dateTimePicker.Value.Month.ToString() + "/" + PersonDOB_dateTimePicker.Value.Day.ToString() + "/"
                //                + PersonDOB_dateTimePicker.Value.Year.ToString() + "'"
                + ",P_DOB = N'" + PersonDOB_dateTimePicker.Value.Year.ToString() + "/" + PersonDOB_dateTimePicker.Value.Month.ToString() + "/"
                                + PersonDOB_dateTimePicker.Value.Day.ToString() + "'"

                + ",P_RegistrationPlace = N'" + PersonRegistration_textBox.Text + "'"
                + ",P_MaritalStatus = N'" + PersonState_comboBox.Text + "'"
                + ",P_NumAtHome = " + PersonNumAtHome_textBox.Text + ""
                + ",P_IsLivingWithFamily = N'" + (P_LiveWithAnotherFamily_checkBox.Checked ? "Yes" : "No") + "'"
                //+ ",P_MilitaryService = N'" + PersonEmail_textBox.Text + "'"
                + ",P_MilitaryService = N'" + (P_Military_checkBox.Checked ? "Yes" : "No") + "'"
                + ",P_HomeAddress = N'" + PersonHomeAddress_textBox.Text + "'"
                + ",P_HomeTel = N'" + PersonHomeTel_textBox.Text + "'"
                + ",P_Mobile = N'" + PersonMobile_textBox.Text + "'"
                + ",P_Picture = @PersonPicArr"
                + ",IsProjectOwner = N'" + "YES" + "' "

                + ",P_SufferanceOfPerson = N'" + P_SufferanceOfPerson_textBox.Text + "'" +
                ",P_SourceOfIncome = N'" + P_SourceOfIncome_textBox.Text + "'" +
                ",P_MedicalCond = N'" + P_MedicalCond_textBox.Text + "'" +
                ",P_FinancialLoss = N'" + P_FinancialLoss_textBox.Text + "'" +
                ",P_SentimentalLoss = N'" + P_SentimentalLoss_textBox.Text + "'" +
                ",P_HomeState = " + home_state + " " +
                ",P_ShopState = " + shop_state + " " +
                ///////////////////////////////////////////////////////////////////////
                ",P_OtherProperties = " + other_properties + " " +
                ",P_OtherIncomeSources = " + other_incomes + " " +
              //  ",P_MaristesCourse = N'" + (P_MaristesCourse_checkBox.Checked ? "Yes" : "No") + "'" +
              //  ",P_OtherCourses = N'" + P_OtherCourses_textBox.Text + "'" +
                ",P_Parish = N'" + P_Parish_comboBox.Text + "'" +
                ",P_Priest_ID = " + (Priest_ID != -1 ? Priest_ID : System.Data.SqlTypes.SqlInt32.Null)

            + " where P_ID =" + PID;

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.Parameters.Add(new MySqlParameter("@PersonPicArr", PersonPicArr));
            MySS.sc.ExecuteNonQuery();
        }

        private void Update_MP(int MP_ID)
        {
            string reasonOfProject = "";
            if (MP_ImproveIncome_checkBox.Checked)
                reasonOfProject = MP_ImproveIncome_checkBox.Text;
            else if (MP_AdditionalIncome_checkBox.Checked)
                reasonOfProject = MP_AdditionalIncome_checkBox.Text;
            else if (MP_ExpandProject_checkBox.Checked)
                reasonOfProject = MP_ExpandProject_checkBox.Text;
            else reasonOfProject = MP_ResonOfProject_textBox.Text;

            MySS.query = "update `microproject` set " +
                "MP_Country = N'" + MP_Country_textBox.Text + "'" +
                ",MP_ClientsAndContinuance  = N'" + MP_ClientsAndContinuance_textBox.Text + "'" +
                ",MP_City = N'" + MP_City_textBox.Text + "'" +
                ",MP_DateOfRequest = N'" + MP_DateOfRequest_dateTimePicker.Value.Year.ToString() + "/" + MP_DateOfRequest_dateTimePicker.Value.Month.ToString() + "/"
                                + MP_DateOfRequest_dateTimePicker.Value.Day.ToString() + "'" +
                ",MP_Name = N'" + MP_Name_textBox.Text + "'" +
                ",MP_AllPriceNeeded = N'" + MP_AllPriceNeeded_textBox.Text + "'" +
                ",MP_PeriodOfExecution = N'" + MP_PeriodOfExecution_textBox.Text + "'" +
                ",MP_Describtion = N'" + MP_Describtion_textBox.Text + "'" +
                ",MP_ResonOfProject = N'" + reasonOfProject + "'" +

                ",MP_IsNeedLicense = N'" + (MP_NeedLicense_radioButton.Checked ? "Yes" : "No") + "'" +
                ",MP_LicenseSide = N'" + MP_LicenseSide_textBox.Text + "'" +
                ",MP_PlaceOfExecution =N'" + MP_PlaceOfExecution_textBox.Text + "'" +
              //   ",MP_ResonOfPlace =N'" + MP_ResonOfPlace_textBox.Text + "'" +
             //   ",MP_ResonOfPlace =N'" + replaceQuotation(FundedBy_comboBox.Text) + "'" +
                ",MP_SimpleProfit = N'" + MP_SimpleProfit_textBox.Text + "'" +
                ",MP_OtherComment = N'" + MP_OtherInformation_textBox.Text + "'" +

              //  ",MP_State =  " + status + " " +
             //   ",MP_StateReason = N'" + MP_StatusReason_textBox.Text + "'" +
             //   ",MP_StateComment = N'" + MP_StatusComment_textBox.Text + "'" +
                ",MP_Level_ID = " + (Level_comboBox.SelectedValue != null ? Int32.Parse(Level_comboBox.SelectedValue.ToString()) : System.Data.SqlTypes.SqlInt32.Null ) + " " +
                ",MP_Category_ID = " + (MP_Category_comboBox.SelectedValue != null ? Int32.Parse(MP_Category_comboBox.SelectedValue.ToString()) : System.Data.SqlTypes.SqlInt32.Null) + " " +
                ",MP_Priest_ID = " + (P_Priest_comboBox.SelectedValue!=null?P_Priest_comboBox.SelectedValue:System.Data.SqlTypes.SqlInt32.Null)+ " " +
                 
                " where MP_ID = " + MP_ID;

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void updateFamily(int FID)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Update family set "
                        + "F_FirstName = N'" + FamilyFName_textBox.Text + "', "
                        + "F_LastName = N'" + FamilyLName_textBox.Text + "', "
                        + "F_Number = N'" + FamilyNum_textBox.Text + "' "
                        + "where F_ID =" + FID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        #endregion

        #region deletes
        public void deleteFamilyMember(int PersonID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete From `person` where P_ID =" + PersonID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        #endregion deletes

        #region get data
        private void getCurrentFamilyID()
        {
            //check connection//
            Program.buildConnection();
            //    MySS.query = "select Ident_current('[dbo].[Family]')";
            MySS.query = "select MAX(F_ID) from `family`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse(MySS.sc.ExecuteScalar().ToString(), out FamilyID);
        }
        private void getCurrentFamilyMemberID()
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "select MAX(P_ID) from `person` ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse(MySS.sc.ExecuteScalar().ToString(), out FamilyMember_ID);
        }
        private int getCurrentPersonID()
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "select MAX(P_ID) from `person` ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            return Int32.Parse(MySS.sc.ExecuteScalar().ToString());
        }
        #endregion

        #region btn clicks (add-update-delete)
        private void InsertPerson_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (PersonFName_textBox.Text == "" || PersonLName_textBox.Text == "" || PersonFatherName_textBox.Text == ""
                    || PersonMName_textBox.Text == "" || PersonNationalNum_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                string PersonNationalNum_without_zero = "";
                string PersonNationalNum = PersonNationalNum_textBox.Text;
                if (PersonNationalNum.ElementAt(0).Equals("0"))
                    PersonNationalNum_without_zero = PersonNationalNum.Remove(0, 1);
                else
                    PersonNationalNum_without_zero = PersonNationalNum;

                MySS.sc = new MySqlCommand("select count(P_NationalNumber) from `person` where P_NationalNumber like '%" + PersonNationalNum_without_zero + "'", Program.MyConn);
                int check_NationalID = Convert.ToInt32( MySS.sc.ExecuteScalar());
                if (check_NationalID != 0)
                {
                    throw new Exception("The national number is already existing");
                }
                if (PersonPicture_pictureBox.Image != null)
                {
                    Convert_Picture();
                    insertPerson(PersonPicArr);
                }
                else
                {
                    insertPerson();
                }
                
                l.Insert_Log("Insert " + PersonFName_textBox.Text + " " + PersonLName_textBox.Text, " Benefeciary ", username, DateTime.Now);

                Person_ID = SelectCurrentPerson();
                clear_Person_boxes();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("Can't leave empty fields");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdatePerson_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (PersonFName_textBox.Text == "" || PersonLName_textBox.Text == "" || PersonFatherName_textBox.Text == ""
                    || PersonMName_textBox.Text == "" || PersonNationalNum_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                if (PersonPicture_pictureBox.Image != null)
                {
                    Convert_Picture();
                }
                updatePerson(Person_ID);

                l.Insert_Log("Update " + PersonFName_textBox.Text + " " + PersonLName_textBox.Text, " Benefeciary ", username, DateTime.Now);
                clear_Person_boxes();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("Can't leave empty fields");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("NationalNum_UniqueIndex"))
                {
                    MessageBox.Show("The national number is already existing");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void AddProject_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (MP_Name_textBox.Text == "" || MP_AllPriceNeeded_textBox.Text == "" || MP_PeriodOfExecution_textBox.Text == "" || MP_Describtion_textBox.Text == "" ||
                         MP_PlaceOfExecution_textBox.Text == "" || MPID_textBox.Text == "" || MP_Category_comboBox.Text == "")
                {
                    throw new Exception("Can't leave empty fields");
                }
                else
                {
                    //insert Micro Project
                    if (MP_IdentityInsert_checkBox.Checked)
                    {
                        insertMicroProject_withIdentity();
                        MicroProject_ID = Int32.Parse(MPID_textBox.Text.ToString());
                    }
                    else
                    {
                        insertMicroProject();
                        MySS.query= "select Max(MP_ID) from `microproject`";
                        //check connection//
                        Program.buildConnection();
                        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                        Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out MicroProject_ID);
                    }

                    l.Insert_Log("insert the project: " + MicroProject_ID, "Micro Project", username, DateTime.Now);
                    clear_Project_boxes();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateProject_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID == -1)
                    throw new Exception("Please select the project you want to update");
                Update_MP(MicroProject_ID);

                l.Insert_Log("update the project: " + MicroProject_ID, "Micro Project", username, DateTime.Now);

                clear_Project_boxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InsertFamily_button_Click(object sender, EventArgs e)
        {
            try
            {
                //check connection//
                Program.buildConnection();
                MySS.sc = new MySqlCommand("select count(F_Number) from `family` where F_Number like '" + FamilyNum_textBox.Text + "'", Program.MyConn);
                int FamilyBookID = Convert.ToInt32(MySS.sc.ExecuteScalar());
                if (FamilyBookID != 0)
                {
                    throw new Exception("The family book number is already existing");
                }
                insertFamily();
                l.Insert_Log("insert the family " + FamilyFName_textBox.Text + " " + FamilyLName_textBox.Text, "Family", username, DateTime.Now);

               DialogResult r = MessageBox.Show("Add the beneficiary to this family before adding other family members", "", MessageBoxButtons.OKCancel);
                if (r == DialogResult.OK)
                {
                    SelectPerson SelectPerson = new SelectPerson();
                    MicroProjectOwnerDataRow = SelectPerson.showSelectedMPRow();

                    if (MicroProjectOwnerDataRow != null)
                    {
                        MicroProjectOwner_ID = (int)MicroProjectOwnerDataRow["ID"];
                        getCurrentFamilyID();
                        //ADD the Micro Project Owner to his Family
                        insertFamilyPersonDetails(FamilyID, MicroProjectOwner_ID, "YES", "مستفيد", MicroProjectOwner_ID, "لا يوجد");
                        r = MessageBox.Show("The beneficiary has been added to the family successfully");
                        l.Insert_Log("insert the family member" + FPersonFName_textBox.Text + " " + FPersonLName_textBox.Text, " ", username, DateTime.Now);

                        Family_FamilyMember_bind(FamilyID.ToString());
                    }
                }
                FamilyFName_textBox.Clear();
                FamilyLName_textBox.Clear();
            }
            catch (NoNullAllowedException)
            { MessageBox.Show("Choose the family you want to add members to"); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void UpdateFamily_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (FamilyNum_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                updateFamily(FamilyID);

                l.Insert_Log("update the family " + FamilyFName_textBox.Text + " " + FamilyLName_textBox.Text, "Family", username, DateTime.Now);
                DialogResult r = MessageBox.Show("Do you want to add the beneficiary to the family you created ?", "", MessageBoxButtons.OKCancel);
                if (r == DialogResult.OK)
                {
                    SelectPerson SelectPerson = new SelectPerson();
                    MicroProjectOwnerDataRow = SelectPerson.showSelectedMPRow();

                    if (MicroProjectOwnerDataRow != null)
                    {
                        //MicroProjectOwner_ID = (int)MicroProjectOwnerDataRow["Person_ID"];
                        MicroProjectOwner_ID = (int)MicroProjectOwnerDataRow["ID"];
                        //getCurrentFamilyID();
                        //ADD the Micro Project Owner to his Family
                        insertFamilyPersonDetails(FamilyID, MicroProjectOwner_ID, "YES", "مستفيد", MicroProjectOwner_ID, "لا يوجد");
                        r = MessageBox.Show("The beneficiary has been added to the family successfully");
                        l.Insert_Log("insert the family member" + FPersonFName_textBox.Text + " " + FPersonLName_textBox.Text, " ", username, DateTime.Now);

                        Family_FamilyMember_bind(FamilyID.ToString());
                    }
                }
                FamilyFName_textBox.Clear();
                FamilyLName_textBox.Clear();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("Please choose the family you want to update");
            }
        }
        private void InsertFamilyMember_button_Click(object sender, EventArgs e)
        {
            try
            {
                insertFamilyMember();
                relationInFamily = WorkName = " ";

                if (FamilyID == -1 || FamilyNum_textBox.Text == "")
                {
                    throw new Exception("Please choose the family you want to add members to");
                }
                getCurrentFamilyMemberID();
                //  FPersonID_textBox.Text = (++n).ToString();
                activeInFamily = (FPersonActiveInFamily_radioButton.Checked ? "Yes" : "No");
                MP_Owner = "No";
                if (FPersonFamilyStatus_comboBox.SelectedIndex != -1)
                    relationInFamily = FPersonFamilyStatus_comboBox.SelectedItem.ToString();
                if (FPersonWorkDescribtion_textBox.Text != "")
                    WorkName = FPersonWorkDescribtion_textBox.Text;
                if (FPersonIsProvider_checkBox.Checked) //if the current person is the provider for himself
                {
                    ProviderID = FamilyMember_ID;
                    insertFamilyPersonDetails(FamilyID, FamilyMember_ID, activeInFamily, relationInFamily, ProviderID, WorkName);
                }
                string strItem = string.Empty;
                foreach (DataRowView selecteditemRow in FProviderID_listBox.SelectedItems) //Find the Providers
                {
                    strItem = selecteditemRow.Row["P_ID"].ToString();
                    ProviderID = Int32.Parse(strItem);
                    insertFamilyPersonDetails(FamilyID, FamilyMember_ID, activeInFamily, relationInFamily, ProviderID, WorkName);
                }
                l.Insert_Log("insert the family member" + FPersonFName_textBox.Text + " " + FPersonLName_textBox.Text, " ", username, DateTime.Now);
                
                provider_bind();
                Family_FamilyMember_bind(FamilyID.ToString());
                clear_FamilyMember_boxes();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeleteFamilyMember_button_Click(object sender, EventArgs e)
        {
            try
            {
                if(SelectedDataRowFM == null || FamilyMember_ID ==-1 )
                { throw new Exception("Please choose the family member you want to delete"); }
                if(FamilyMember_ID == Person_ID || relationInFamily == "مستفيد")
                { throw new Exception("You can't delete the Micro Project Beneficiary from this section"); }
                deleteFamilyMember(FamilyMember_ID);
                l.Insert_Log("Delete " + FPersonFName_textBox.Text , " Family Member ", username, DateTime.Now);

                provider_bind();
                Family_FamilyMember_bind(FamilyID.ToString());
                clear_FamilyMember_boxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddImage_button_Click(object sender, EventArgs e)
        {
            myTh = new Thread(new ThreadStart(CallDialog));
            myTh.SetApartmentState(ApartmentState.STA);
            myTh.Start();

        }
        private void DeleteImage_button_Click(object sender, EventArgs e)
        {
            imageFilePath = imageName= "";
            PersonPicture_pictureBox.ImageLocation = null;
            PersonPicture_pictureBox.Image = null;
        }
        #endregion

        #region mouse hover
        private void AddSave_button_MouseEnter(object sender, EventArgs e)
        {
            AddProject_button.BackgroundImage = InsertPerson_button.BackgroundImage =
            UpdateProject_button.BackgroundImage = UpdatePerson_button.BackgroundImage =
            InsertFamily_button.BackgroundImage = UpdateFamily_button.BackgroundImage
                = Properties.Resources.save;
        }
        private void AddSave_button_MouseLeave(object sender, EventArgs e)
        {
            AddProject_button.BackgroundImage = InsertPerson_button.BackgroundImage =
            UpdateProject_button.BackgroundImage = UpdatePerson_button.BackgroundImage =
            InsertFamily_button.BackgroundImage = UpdateFamily_button.BackgroundImage 
                = Properties.Resources.save0;
        }
        private void SaveFamilyMember_button_MouseEnter(object sender, EventArgs e)
        {
            InsertFamilyMember_button.BackgroundImage = Properties.Resources.save;
        }
        private void SaveFamilyMember_MouseLeave(object sender, EventArgs e)
        {
            InsertFamilyMember_button.BackgroundImage = Properties.Resources.save0;
        }
        private void AddLevel_button_MouseEnter(object sender, EventArgs e)
        {
            AddLevel_button.BackgroundImage = Properties.Resources.plus;
        }
        private void AddLevel_button_MouseLeave(object sender, EventArgs e)
        {
            AddLevel_button.BackgroundImage = Properties.Resources.plus0b;
        }
        private void AddCategory_button_MouseEnter(object sender, EventArgs e)
        {
            AddCategory_button.BackgroundImage = Properties.Resources.plus;
        }
        private void AddCategory_button_MouseLeave(object sender, EventArgs e)
        {
            AddCategory_button.BackgroundImage = Properties.Resources.plus0b;
        }
        private void AddPriest_button_MouseEnter(object sender, EventArgs e)
        {
            AddPriest_button.BackgroundImage = Properties.Resources.plus;
        }
        private void AddPriest_button_MouseLeave(object sender, EventArgs e)
        {
            AddPriest_button.BackgroundImage = Properties.Resources.plus0b;
        }
        private void Delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteFamilyMember_button.BackgroundImage = Properties.Resources.delete;
        }
        private void Delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteFamilyMember_button.BackgroundImage = Properties.Resources.delete0;
        }
        private void AddImage_button_MouseEnter(object sender, EventArgs e)
        {
            AddImage_button.BackgroundImage = Properties.Resources.plus;
        }
        private void AddImage_button_MouseLeave(object sender, EventArgs e)
        {
            AddImage_button.BackgroundImage = Properties.Resources.plus0b;
        }
        private void DeleteImage_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteImage_button.BackgroundImage = Properties.Resources.x;
        }
        private void DeleteImage_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteImage_button.BackgroundImage = Properties.Resources.x0b;
        }
        //tab btn clicks
        private void Beneficiary_button_MouseEnter(object sender, EventArgs e)
        {
            Beneficiary_button.BackgroundImage = Properties.Resources.blank;
        }
        private void Beneficiary_button_MouseLeave(object sender, EventArgs e)
        {
            Beneficiary_button.BackgroundImage = null;
        }
        private void Project_button_MouseEnter(object sender, EventArgs e)
        {
            Project_button.BackgroundImage = Properties.Resources.blank;
        }
        private void Project_button_MouseLeave(object sender, EventArgs e)
        {
            Project_button.BackgroundImage = null;
        }
        private void Family_button_MouseEnter(object sender, EventArgs e)
        {
            Family_button.BackgroundImage = Properties.Resources.blank;
        }
        private void Family_button_MouseLeave(object sender, EventArgs e)
        {
            Family_button.BackgroundImage = null;
        }
        #endregion

        #region add new categories - show all
        private void Details_button_Click(object sender, EventArgs e)
        {
            try
            {
                BeneficiaryDetails BeneficiaryDetails = new BeneficiaryDetails(username, Person_ID);
                BeneficiaryDetails.Show();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void ProjectDetails_button_Click(object sender, EventArgs e)
        {
             try
            {
                ProjectDetails ProjectDetails = new ProjectDetails(username, MicroProject_ID);
                ProjectDetails.Show();
            }
             catch (Exception ex)
             { MessageBox.Show(ex.Message); }
        }
        private void AddLevel_button_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    AddNewLevel AddNewLevel = new AddNewLevel(username);
            //    AddNewLevel.Show();
            // }
            //catch(Exception ex)
            //{ MessageBox.Show(ex.Message); }
        }
        private void AddCategory_button_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewProjectCategory AddNewProjectCategory = new AddNewProjectCategory(username);
                AddNewProjectCategory.Show();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void AllBeneficiary_button_Click(object sender, EventArgs e)
        {
            using (AllBeneficiaries AllBeneficiaries = new AllBeneficiaries(username))
            {
                if (AllBeneficiaries.ShowDialog() == DialogResult.OK)
                {
                    fill_Person_boxes(AllBeneficiaries.SelectedDataRow);
                    UpdatePerson_button.Visible = true;
                    InsertPerson_button.Visible = false;
                }
                else
                {
                    clear_Person_boxes();
                    UpdatePerson_button.Visible = false;
                    InsertPerson_button.Visible = true;
                }
            }
        }
        private void AllProjects_button_Click(object sender, EventArgs e)
        {
            using (AllProjects AllProjects = new AllProjects(username))
            {
                if (AllProjects.ShowDialog() == DialogResult.OK)
                {
                    fill_Project_boxes(AllProjects.SelectedDataRow);
                    UpdateProject_button.Visible = true;
                    AddProject_button.Visible = false;
                }
                else
                {
                    clear_Project_boxes();

                    UpdateProject_button.Visible = false;
                    AddProject_button.Visible = true;
                }
            }
        }
        private void AllFamilies_button_Click(object sender, EventArgs e)
        {
            using (AllFamilies AllFamilies = new AllFamilies(username))
            {
                if (AllFamilies.ShowDialog() == DialogResult.OK)
                {
                    fill_Family_boxes(AllFamilies.SelectedDataRow);
                    UpdateFamily_button.Visible = true;
                    InsertFamily_button.Visible = false;
                }
                else
                {
                    clear_FamilyMember_boxes();
                    UpdateFamily_button.Visible = false;
                    InsertFamily_button.Visible = true;
                }
            }
        }
        private void AddPriest_button_Click(object sender, EventArgs e)
        {
            AddNewPriest AddNewPriest = new AddNewPriest();
            AddNewPriest.Show();
        }
        #endregion

        #region tab btns
        private void Beneficiary_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
            Family_button.BackColor = Project_button.BackColor = Color.Transparent;
            Beneficiary_button.BackColor = Color.Maroon;

            //Family_button.BackgroundImage = null;
            //Beneficiary_button.BackgroundImage = Properties.Resources.blank;
            //Project_button.BackgroundImage = null;
        }
        private void Project_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
            Family_button.BackColor = Beneficiary_button.BackColor = Color.Transparent;
            Project_button.BackColor = Color.Maroon;

            //Family_button.BackgroundImage = null;
            //Beneficiary_button.BackgroundImage = null;
            //Project_button.BackgroundImage = Properties.Resources.blank;
        }
        private void Family_button_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 2;
            Project_button.BackColor = Beneficiary_button.BackColor = Color.Transparent;
            Family_button.BackColor = Color.Maroon;

            //Family_button.BackgroundImage = Properties.Resources.blank;
            //Beneficiary_button.BackgroundImage = null;
            //Project_button.BackgroundImage = null;

           // provider_bind();
        }
        #endregion

        private void MP_Priest_comboBox_Enter(object sender, EventArgs e)
        {
            Priest_bind();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            InsertingSection_Load(sender, e);
        }

        private void BindProvider_button_Click(object sender, EventArgs e)
        {
            provider_bind();
        }

        private void PersonPicture_pictureBox_Click(object sender, EventArgs e)
        {
            myTh = new Thread(new ThreadStart(CallDialog));
            myTh.SetApartmentState(ApartmentState.STA);
            myTh.Start();
        }
        private void MP_IdentityInsert_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MP_IdentityInsert_checkBox.Checked)
                MPID_textBox.Enabled = true;
            else
                MPID_textBox.Enabled = false;
        }
        private void Level_comboBox_Enter(object sender, EventArgs e)
        {
            Level_bind();
        }
        private void Category_comboBox_Enter(object sender, EventArgs e)
        {
            Category_bind();
        }
        private void P_Back0_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FamilyMembers_DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedDataRowFM = ((DataRowView)FamilyMembers_DataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowFM != null)
            {
                FamilyMember_ID = Int32.Parse(SelectedDataRowFM["Person_ID"].ToString());
                FPersonFName_textBox.Text = SelectedDataRowFM["Beneficiary Name"].ToString();
                relationInFamily = SelectedDataRowFM["Relation"].ToString();
            }
        }

        private void FPersonSexMale_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            PersonState_bind();
        }

        private string replaceQuotation(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }

        private void P_Priest_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (P_Priest_comboBox.SelectedValue == null)
                Priest_ID = -1;
            else
                Priest_ID = Int32.Parse(P_Priest_comboBox.SelectedValue.ToString());
        }
    }
}
