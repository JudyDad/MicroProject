using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;
using MyWorkApplication.Visit_Forms;

namespace MyWorkApplication
{
    public partial class Application_Form : Form
    {
        private int Beneficiary_Count;
        private FtpConnector c; 
        private string ftpPath = "";
        private string fullFtpPath = "";

        private int image_state; 
        private string imageFilePath, imageName, RequestedAmount, SimpleProfit;
        private Log l;
        private User user;
        private Street st;
        private SubCategory sub;
        private readonly MainForm mainForm;
        private MicroProject mp;
        private MySqlComponents MySS;
        private Thread myTh;
        private bool OldBeneficiary_NewProject;

        private int Person_ID, Family_ID, Priest_ID, Husband_Wife_ID;
        public int MicroProject_ID; // to be visible in main form //

        private byte[] PersonPicArr;
        private Select s;
        private double sumOfItemsPrice_SYR;
        private int type;
        private UpdateQueries u;
        private bool Update_Mode, NoFamily, CanAddFamilyMembers;
        // Beneficiary_In_Existing_Family
        bool user_mode;

        public Application_Form(MainForm mainForm)
        {
            InitializeComponent();

            clear_Person_boxes();
            Person_ID = -1;
            MicroProject_ID = -1;

            Loan_radioButton.Checked = true;

            this.mainForm = mainForm;
            Update_Mode = false; AddPartner_button.Visible = false; 
        }

        public Application_Form(int Person_ID, int MicroProject_ID, MainForm mainForm)
        {
            InitializeComponent();
            this.Person_ID = Person_ID;
            this.MicroProject_ID = MicroProject_ID;

            Update_Mode = true; AddPartner_button.Visible = true;

            this.mainForm = mainForm;
        }

        private void Application_Form_Load(object sender, EventArgs e)
        {
            try
            {
                Check_Theme();

                MySS = new MySqlComponents();
                l = new Log();
                s = new Select();
                u = new UpdateQueries();
                user = new User();
                c = new FtpConnector();
                mp = new MicroProject();
                st = new Street(); sub = new SubCategory();
                Get_Last_MicroProjectID();
                
                Education_dataGridView.AutoGenerateColumns = false;
                Work_dataGridView.AutoGenerateColumns = Exp_dataGridView.AutoGenerateColumns 
                    = FamilyMember_dataGridView.AutoGenerateColumns = false;
                Materials_dataGridView.AutoGenerateColumns = false;

                switch (Settings.Default.role)
                {
                    case 1: //admin
                    case 5: //manager
                    case 8: //admin_l0
                        {
                        }
                        break;
                    case 2: //Data
                        { 
                              payments_toolStripMenuItem.Visible 
                              = addNotesToolStripMenuItem.Visible = false;
                        }
                        break;
                    case 3: //Financial 
                    case 7: //Lawful
                        {
                            attachments_toolStripMenuItem.Visible
                            = payments_toolStripMenuItem.Visible 
                            = addNotesToolStripMenuItem.Visible = false;
                        }
                        break;
                    case 4: //Guest
                        {
                            attachments_toolStripMenuItem.Visible
                            = payments_toolStripMenuItem.Visible
                            = Visits_toolStripMenuItem.Visible
                            = showChecklistToolStripMenuItem.Visible
                            = addNotesToolStripMenuItem.Visible = false;
                        }
                        break;
                }

                //////////////////// handle auto scrolling for all comboBoxes //////////////////////
                Type_comboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                SubType_comboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                Category_comboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                SubCategory_comboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                P_Parish_comboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                P_Priest_comboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                Street_comboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                P_Sex_comboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                FPersonSex_comboBox1.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                P_State_comboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                FPersonState_comboBox1.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                PaperApplicationUser_comboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                ///////////////////////////////////////////////////////////////////////////////////////

                //Category_bind();
                //SubCategory_bind(""); //to select the subCategory when Fill MicroProject//
                //Street_bind();
                //Priest_bind();

                Bind_All_ComboBoxes();
                Type_comboBox.SelectedIndex = 0; // default MICRO

                users_bind(PaperApplicationUser_comboBox);

                Save_MP_ID_button.Visible = true;
                Save_MP_ID_button.BackgroundImage = Properties.Resources.Unchecked;
                toolTip1.SetToolTip(Save_MP_ID_button, "لم يتم حجز الرقم بعد");

                user_mode = false;
                PersonType_comboBox.SelectedIndex = 0;
                user_mode = true;

                if (MicroProject_ID != -1)
                {
                    user_mode = false;
                    if (Person_ID != -1)
                    {
                        Fill_Person();
                        Fill_PersonDetails();
                        Fill_FamilyMembers(0);
                    }

                    Fill_Project();

                    Save_MP_ID_button.Visible = false;
                    Save_MP_ID_button.BackgroundImage = Properties.Resources.Cheked;
                    toolTip1.SetToolTip(Save_MP_ID_button, "محجوز");

                    Fill_ProjectDetails();

                    Program.buildConnection();
                    /// check if this project got partners or not ///
                    var query = "select count(*) from `person_microproject` where MicroProject_ID = " + MicroProject_ID;
                    MySS.sc = new MySqlCommand(query, Program.MyConn);
                    var partners_count = Convert.ToInt32(MySS.sc.ExecuteScalar());
                    Program.MyConn.Close();
                    if (partners_count > 1) Partners_lable.Text = "شراكة";
                    else Partners_lable.Text = "فردي";
                    user_mode = true;
                }

                //unable to cast db.null to system.string !!
                mainForm.MP_ID_label.Visible = mainForm.ProjectNumber_label.Visible =
                    mainForm.Project_label.Visible = mainForm.TabName_label.Visible = true;
                mainForm.TabName_label.Text = "Application";
                mainForm.MP_ID_label.Text = MPID_textBox.Text;

                Education_dataGridView.Columns["DeleteRow"].Width = Work_dataGridView.Columns["W_DeleteRow"].Width =
                    Exp_dataGridView.Columns["Exp_DeleteRow"].Width =
                        FamilyMember_dataGridView.Columns["P_DeleteRow"].Width = 50;

                Main_panel.AutoScrollPosition = new Point(0, 0);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        public void Check_Theme()
        {
            var newTheme = new NewTheme();
            if (Settings.Default.theme == "Dark")
                newTheme.Application_ToNight(this);
            else
                newTheme.Application_ToLight(this);
        }

        private void CheckUserPermission()
        {  
            switch (Settings.Default.role)
            {
                case 1: //admin
                case 5: //manager
                case 8: //admin_l0
                { 
                }
                    break;
                case 2: //Data
                { 
                }
                    break;
                case 3: //Financial 
                case 7: //Lawful
                    {
                    throw new Exception(" نعتذر ! لا تملك الصلاحية للقيام بهذا الفعل.");
                }
                    break;
                case 4: //Guest
                {
                    throw new Exception(" نعتذر ! لا تملك الصلاحية للقيام بهذا الفعل.");
                }
                    break; 
            }
        }

        private void calculate_SumOfBudgetItems(int RowIndex)
        {
            try
            {
                double itemOverAllsyr = 0;
                // Double itemOverAlleuro = 0;

                if (Materials_dataGridView.Rows[RowIndex].Cells["M_LocalContribution"].Value == null)
                    Materials_dataGridView.Rows[RowIndex].Cells["M_LocalContribution"].Value = "0";

                if (Materials_dataGridView.Rows[RowIndex].Cells["M_Amount"].Value == null)
                    Materials_dataGridView.Rows[RowIndex].Cells["M_LocalContribution"].Value = "1";

                var price = Materials_dataGridView.Rows[RowIndex].Cells["M_Price"].Value.ToString().Replace(",", "");
                var local = Materials_dataGridView.Rows[RowIndex].Cells["M_LocalContribution"].Value.ToString()
                    .Replace(",", "");
                var amount = Materials_dataGridView.Rows[RowIndex].Cells["M_Amount"].Value.ToString().Replace(",", "");

                var AllPrice = double.Parse(price) * int.Parse(amount);

                itemOverAllsyr = AllPrice - double.Parse(local);

                //save the overAll for each item
                sumOfItemsPrice_SYR += itemOverAllsyr;
                //itemOverAlleuro = itemOverAllsyr / 650.00;
                OverallSyrian_label.Text = sumOfItemsPrice_SYR.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void s1_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var sum = 0;
                if (s1_radioButton.Checked) sum += 1;
                if (s2_radioButton.Checked) sum += 1;
                if (s3_radioButton.Checked) sum += 1;
                if (s4_radioButton.Checked) sum += 1;
                if (s5_radioButton.Checked) sum += 1;
                if (s6_radioButton.Checked) sum += 1;
                if (s7_radioButton.Checked) sum += 1;
                if (s8_radioButton.Checked) sum += 1;
                if (s9_radioButton.Checked) sum += 1;
                if (s0_radioButton.Checked) sum += 1;
                Sum_label.Text = "المجموع : " + sum;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearPlace_button_Click(object sender, EventArgs e)
        {
            Street_comboBox.SelectedIndex = -1;
        }

        #region sql queries

        private void Insert_Person()
        {
            string home_state, military;
            var other_properties = 0;
            var other_incomes = 0;

            if (MilitaryFinished_radioButton.Checked) military = "مسرح";
            else if (MilitaryNow_radioButton.Checked) military = "عسكري";
            else if (MilitaryDelayed_radioButton.Checked) military = "مؤجل";
            else if (MilitarySingle_radioButton.Checked) military = "وحيد";
            else military = "";

            if (P_HomeRent_radioButton.Checked)
                home_state = P_HomeRent_radioButton.Text; // -0-أجار  
            else if (P_HomeOwnership_radioButton.Checked)
                home_state = P_HomeOwnership_radioButton.Text; // -1-ملك 
            else home_state = "";

            if (P_Car_checkBox.Checked && P_Rented_checkBox.Checked) other_properties = 1; // 1 = سيارةوملك مؤجر
            else if (P_Car_checkBox.Checked && !P_Rented_checkBox.Checked) other_properties = 2; // 2 = سيارة
            else if (!P_Car_checkBox.Checked && P_Rented_checkBox.Checked) other_properties = 3; // 3 = ملك مؤجر
            else other_properties = 0; // 0 -none of them checked

            if (P_SourceRelatives_checkBox.Checked && P_SourceRation_checkBox.Checked)
                other_incomes = 1; // 1 = معونة و أقارب
            else if (P_SourceRelatives_checkBox.Checked && !P_SourceRation_checkBox.Checked)
                other_incomes = 2; // 2 = أقارب
            else if (!P_SourceRelatives_checkBox.Checked && P_SourceRation_checkBox.Checked)
                other_incomes = 3; // 3 = معونة
            else other_incomes = 0; // 0 -none of them checked

            MySS.query = "Insert Into `person`(`P_FirstName`, `P_LastName`, `P_FatherName`, `P_MotherName`, `P_DOB`" +
                         ", `P_MilitaryService`,`P_MilitaryService_Note`, `P_Mobile`, `P_HomeTel`, `Street_ID`, `P_HomeAddress`, `P_NationalNumber`, `P_RegistrationPlace`" +
                         ", `P_Sex`, `P_MaritalStatus`, `P_IsLivingWithFamily`,`P_LiveWithFamily_Note`, `P_NumAtHome`, `P_Picture`,P_PicturePath, `IsProjectOwner`, `IsProvider`" +
                         ", `P_SourceOfIncome`, `P_MedicalCond`, `P_MedicalHelp`, `P_MedicalHelp_Note`, `P_Loss`, `P_IntermidiarySide`" +
                         ", `P_HomeState`,`P_HomeState_Note`, `P_OtherProperties`, `P_OtherProperties_Note`,`P_OtherIncomeSources`, `P_OtherIncomeSources_Note`" +
                         ", `P_Courses_Note`, `P_MaristesCourse`, `P_OtherCourses`, `P_Parish`, `P_Priest_ID`) values ("
                         + "TRIM(N'" + P_FirstName_textBox.Text + "'),"
                         + "TRIM(N'" + P_LastName_textBox.Text + "'),"
                         + "TRIM(N'" + P_FatherName_textBox.Text + "'),"
                         + "TRIM(N'" + P_MotherName_textBox.Text + "'),'"
                         + P_DOB_textBox.Text + "/1/1" + "',N'"
                         + military + "',N'"
                         + Military_textBox.Text + "',N'"
                         + P_Mobile_textBox.Text + "',N'"
                         + P_HomeTel_textBox.Text + "',"
                         + (Street_comboBox.SelectedValue == null
                             ? SqlInt32.Null
                             : Convert.ToInt32(Street_comboBox.SelectedValue.ToString())) + ",N'"
                         + P_Address_textBox.Text + "',N'"
                         + P_NationalNum_textBox.Text + "',N'"
                         + P_Registration_textBox.Text + "',N'"
                         + P_Sex_comboBox.Text + "',N'"
                         + P_State_comboBox.Text + "','"
                         + (P_LiveWithFamily_radioButton.Checked ? "Yes" : "No") + "','"
                         + LiveWithFamilyDesc_textBox.Text + "',N'"
                         + P_NumAtHome_textBox.Text + "','"
                         + null + "',N'"
                         + "" + "',N'"
                         + "YES',N'" // يملك مشروع IsProjectOwner 
                         + (IsProvider_radioButton.Checked ? "Yes" : "No") + "','"
                         + P_SourceOfIncome_textBox.Text + "',N'"
                         + P_MedicalCond_textBox.Text + "',"
                         + (MedicalHelp_radioButton.Checked ? 1 : 0) + ",N'"
                         + P_MedicalHelp_textBox.Text + "',N'"
                         + P_Loss_textBox.Text + "',N'"
                         + P_IntermidiaryName_textBox.Text + "',N'"
                         + home_state + "',N'"
                         + HomeDesc_textBox.Text + "',"
                         + other_properties + ",N'"
                         + OtherProperties_textBox.Text + "',"
                         + other_incomes + ",N'"
                         + P_OtherIncomeSources_textBox.Text + "',N'"
                         + OtherCourses_textBox.Text + "',N'"
                         + "" + "',N'" //maristes course
                         + "" + "',N'" //other courses
                         + P_Parish_comboBox.Text + "',"
                         + (P_Priest_comboBox.SelectedValue != null ? P_Priest_comboBox.SelectedValue : SqlInt32.Null) +
                         " )";

            //check connection//
            Program.buildConnection();
            {
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        private void Insert_MicroProject()
        {
            MySS.query = " Insert Into `microproject`(`MP_ID`,`MP_State`,`MP_StateReason_ID`,`MP_ProgramUser_ID`) values(" +
                Convert.ToInt32(MPID_textBox.Text) +",3,1,"+Properties.Settings.Default.userID+")";

            //check connection//
            Program.buildConnection();
            {
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        private void Insert_MicroProject_scoring(int MP_ID, int Score_ID, int value, string notes)
        {
            MySS.query = " Insert Into `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values("
                         + MP_ID + "," + Score_ID + "," + value + ",N'" + notes + "')";
            //check connection//
            Program.buildConnection();
            {
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        private void Insert_MicroProject_risk(int MP_ID, string Risk)
        {
            MySS.query = "Insert Into `microproject_risk`(`MicroProject_ID`, `Risk_ID`) values (" + MP_ID
                + ",(select ID from `risk` where Name like '" + Risk + "') )";
            //check connection//
            Program.buildConnection();
            {
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        private void Insert_MicroProject_Material(string name, int amount, double price, double contribution,
            string comments, int MP_ID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query =
                "Insert INTO `material`(`Name`, `Amount`, `Price`,`LocalContribution`,`Comments`, `MicroProject_ID`) VALUES ("
                + "N'" + name + "'," + amount + "," + price + "," + contribution + ",N'" + comments + "'," + MP_ID +
                ")";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        private void Insert_Person_MicroProject(int personID, int microProjectID, string type)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "Insert into `person_microproject`(`Person_ID`, `MicroProject_ID`, `PersonType`) values ("
                         + personID + ","
                         + microProjectID + ",N'"
                         + type + "' " +
                         ")";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }
        private void Update_Person_MicroProject(int personID, int microProjectID, string type)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = " update `person_microproject` set PersonType = N'" + type + "' " +
                " where Person_ID = " + personID + " and MicroProject_ID = " + microProjectID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        private void Insert_Family()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `family`(`F_FirstName`, `F_LastName`, `F_FatherName`, `F_Number`) values(" +
                "N'',N'',N'',N'" + FamilyNum_textBox1.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        private void Insert_FamilyMember(string f_name, string l_name, string date)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `person`(`P_FirstName`, `P_LastName`, `P_FatherName`, `P_MotherName`, `P_DOB`" +
                         ", `P_MilitaryService`,`P_MilitaryService_Note`, `P_Mobile`, `P_HomeTel`, `P_HomeAddress`, `P_NationalNumber`, `P_RegistrationPlace`" +
                         ", `P_Sex`, `P_MaritalStatus`, `P_IsLivingWithFamily`,`P_LiveWithFamily_Note`, `P_NumAtHome`" +
                         ", `P_Picture`,P_PicturePath, `IsProjectOwner`, `IsProvider`" +
                         ", `P_SourceOfIncome`, `P_MedicalCond`, `P_MedicalHelp`, `P_MedicalHelp_Note`, `P_Loss`, `P_IntermidiarySide`" +
                         ", `P_HomeState`, `P_HomeState_Note`, `P_OtherProperties`, `P_OtherProperties_Note`,`P_OtherIncomeSources`, `P_OtherIncomeSources_Note`" +
                         ", `P_Courses_Note`, `P_MaristesCourse`, `P_OtherCourses`, `P_Parish`, `P_Priest_ID`) values(N'"
                         + f_name + "',N'"
                         + (l_name != "" ? l_name : "") + "',N'"
                         + "" + "',N'" //Father Name
                         + "" + "','" //Mother Name
                         + date + "/" + 01 + "/" + 01 + "',N'"
                         + "" + "',N'" //Military service
                         + "" + "',N'" //Military service Note
                         + "" + "','" //Mobile
                         + "" + "',N'" //HomeTel
                         + "" + "'," //HomeAddress
                         + "null" + ",N'" //NationalNumber
                         + "" + "',N'" //RegistrationPlace
                         + "" + "',N'" //sex
                         + "" + "','" //marital state
                         + "" + "','" //LiveWithAnotherFamily
                         + "" + "',N'" //LiveWithAnotherFamily Note
                         + "00" + "','" //NumAtHome
                         + null + "',N'" //picture
                         + "" + "',N'" //picture path
                         + "NO" + "',N'" //Is Project Owner
                         + "No" + "',N'" //is provider
                         + "No" + "',N'" //SourceOfIncome
                         + "" + "','" //MedicalCondition
                         + null + "',N'" //P_MedicalHelp
                         + "" + "',N'" //P_MedicalHelp_Note
                         + null + "',N'" //Loss
                         + null + "',N'" //IntermidiarySide
                         + "" + "',N'" //home_state
                         + "" + "','" //home_state Note
                         + null + "','" //P_OtherProperties
                         + "" + "'," //P_OtherProperties_Note
                         + "0" + ",'" //P_OtherIncomeSources
                         + "" + "','" //P_OtherIncomeSources_Note
                         + "" + "','" //P_Courses_Note
                         + "" + "',N'" //MaristesCourse
                         + "" + "',N'" //OtherCourses
                         + "" + "'," //Parish
                         + SqlInt32.Null //Priest_ID
                         + " )";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        private void Insert_Wife_Husband()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `person`(`P_FirstName`, `P_LastName`, `P_FatherName`, `P_MotherName`, `P_DOB`" +
                         ", `P_MilitaryService`,`P_MilitaryService_Note`, `P_Mobile`, `P_HomeTel`, `P_HomeAddress`, `P_NationalNumber`, `P_RegistrationPlace`" +
                         ", `P_Sex`, `P_MaritalStatus`, `P_IsLivingWithFamily`,`P_LiveWithFamily_Note`, `P_NumAtHome`" +
                         ", `P_Picture`,P_PicturePath, `IsProjectOwner`, `IsProvider`" +
                         ", `P_SourceOfIncome`, `P_MedicalCond`, `P_MedicalHelp`, `P_MedicalHelp_Note`, `P_Loss`, `P_IntermidiarySide`" +
                         ", `P_HomeState`, `P_HomeState_Note`, `P_OtherProperties`, `P_OtherProperties_Note`, `P_OtherIncomeSources`, `P_OtherIncomeSources_Note`" +
                         ", `P_Courses_Note`, `P_MaristesCourse`, `P_OtherCourses`, `P_Parish`, `P_Priest_ID`) values(N'"
                         + FPersonFName_textBox1.Text + "',N'"
                         + (FPersonLName_textBox1.Text != "" ? FPersonLName_textBox1.Text : "") + "',N'"
                         + (FPersonFatherName_textBox1.Text != "" ? FPersonFatherName_textBox1.Text : "") + "',N'"
                         + (FPersonMotherName_textBox1.Text != "" ? FPersonMotherName_textBox1.Text : "") + "','"
                         + FPersonDOB_textBox1.Text + "/1/1" + "',N'"
                         + "" + "',N'" // Military service
                         + "" + "',N'" // Military service Note
                         + "" + "','" // Mobile
                         + "" + "',N'" // HomeTel
                         + "" + "','" // HomeAdd
                         + (FPersonNationalNum_textBox1.Text != "" ? FPersonNationalNum_textBox1.Text : "null") +
                         "',N'" //NationalNumber 
                         + "" + "',N'" // RegistrationPlace
                         + FPersonSex_comboBox1.Text + "',N'"
                         + FPersonState_comboBox1.Text + "','"
                         + "" + "',N'" // LiveWithAnotherFamily
                         + "" + "','" // LiveWithAnotherFamily Note
                         + "00" + "','" // NumAtHome
                         + null + "',N'" // Picture
                         + "" + "',N'"
                         + "NO" + "',N'" // IsProjectOwner
                         + "No" + "',N'" //is provider
                         + "No" + "',N'" //SourceOfIncome
                         + "" + "','" //MedicalCondition
                         + null + "',N'" //P_MedicalHelp
                         + "" + "',N'" //P_MedicalHelp_Note
                         + null + "',N'" //Loss
                         + null + "',N'" //IntermidiarySide
                         + "" + "',N'" //home_state
                         + "" + "','" //home_state Note
                         + null + "','" //P_OtherProperties
                         + "" + "'," //P_OtherProperties_Note
                         + "0" + ",'" //P_OtherIncomeSources
                         + "" + "','" //P_OtherIncomeSources_Note
                         + "" + "','" //P_Courses_Note
                         + "" + "',N'" //MaristesCourse
                         + "" + "',N'" //OtherCourses
                         + "" + "'," //Parish
                         + SqlInt32.Null //Priest_ID
                         + " )";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        private void Insert_FamilyMemberDetails(int fNo, int pNo, string relation, int providerID, string WorkName)
        {
            //check connection//
            Program.buildConnection();

            MySS.query =
                "Insert Into `person_family`(`Family_ID`, `Person_ID`, `IsInNow`, `Relation`, `P_Provider_ID`, `Work_Name`) values("
                + "(select F_ID from `Family` where F_ID = " + fNo + "),"
                + "(select P_ID from `Person` where P_ID = " + pNo + "),"
                + "'Yes',N'"
                + relation + "',"
                + "(select P_ID from `person` where P_ID =  " + providerID + "), N'"
                + WorkName + "' "
                + ")";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        private void Update_Person(int PID)
        {
            string home_state, military, LiveWithFamily, IsProvider;
            var other_properties = 0;
            var other_incomes = 0;
            var MedicalHelp = 0;

            if (P_HomeRent_radioButton.Checked)
                home_state = P_HomeRent_radioButton.Text; // -0-أجار  
            else if (P_HomeOwnership_radioButton.Checked)
                home_state = P_HomeOwnership_radioButton.Text; // -1-ملك 
            else home_state = "";

            if (MilitaryFinished_radioButton.Checked) military = "مسرح";
            else if (MilitaryNow_radioButton.Checked) military = "عسكري";
            else if (MilitaryDelayed_radioButton.Checked) military = "مؤجل";
            else if (MilitarySingle_radioButton.Checked) military = "وحيد";
            else military = "";
             
            if (P_LiveWithFamily_radioButton.Checked) LiveWithFamily = "Yes"; // 
            else if (P_NotLiveWithFamily_radioButton.Checked) LiveWithFamily = "No"; // 
            else LiveWithFamily = "";

            if (IsProvider_radioButton.Checked) IsProvider = "Yes"; //  
            else if (IsNotProvider_radioButton.Checked) IsProvider = "No"; //
            else IsProvider = "";
              
            if (MedicalHelp_radioButton.Checked) MedicalHelp = 1; //  
            else if (NoMedicalHelp_radioButton.Checked) MedicalHelp = 0; //  
            else MedicalHelp = -1; //non of them

            if (P_Car_checkBox.Checked && P_Rented_checkBox.Checked) other_properties = 1; // 1 = سيارةوملك مؤجر
            else if (P_Car_checkBox.Checked && !P_Rented_checkBox.Checked) other_properties = 2; // 2 = سيارة
            else if (!P_Car_checkBox.Checked && P_Rented_checkBox.Checked) other_properties = 3; // 3 = ملك مؤجر
            else other_properties = 0; // 0 -none of them checked

            if (P_SourceRelatives_checkBox.Checked && P_SourceRation_checkBox.Checked)
                other_incomes = 1; // 1 = معونة و أقارب
            else if (P_SourceRelatives_checkBox.Checked && !P_SourceRation_checkBox.Checked)
                other_incomes = 2; // 2 = أقارب
            else if (!P_SourceRelatives_checkBox.Checked && P_SourceRation_checkBox.Checked)
                other_incomes = 3; // 3 = معونة
            else other_incomes = 0; // 0 -none of them checked

            MySS.query = "Update `person` set "
                         + "P_FirstName = TRIM(N'" + P_FirstName_textBox.Text + "')"
                         + ",P_LastName = TRIM(N'" + P_LastName_textBox.Text + "')"
                         + ",P_FatherName = TRIM(N'" + P_FatherName_textBox.Text + "')"
                         + ",P_MotherName = TRIM(N'" + P_MotherName_textBox.Text + "')"
                         + ",P_DOB = N'" + P_DOB_textBox.Text + "/1/1" + "'"
                         + ",P_MilitaryService = N'" + military + "'"
                         + ",P_MilitaryService_Note = N'" + Military_textBox.Text + "'"
                         + ",P_Mobile = N'" + P_Mobile_textBox.Text + "'"
                         + ",P_HomeTel = N'" + P_HomeTel_textBox.Text + "'"
                         + ",Street_ID = " + (Street_comboBox.SelectedValue != null
                             ? int.Parse(Street_comboBox.SelectedValue.ToString())
                             : SqlInt32.Null) + " "
                         + ",P_HomeAddress = N'" + P_Address_textBox.Text + "'"
                         + ",P_NationalNumber = N'" + P_NationalNum_textBox.Text + "'"
                         + ",P_RegistrationPlace = N'" + P_Registration_textBox.Text + "'"
                         + ",P_Sex = N'" + P_Sex_comboBox.Text + "'"
                         + ",P_MaritalStatus = N'" + P_State_comboBox.Text + "'"
                         + ",P_IsLivingWithFamily = N'" + LiveWithFamily + "'"
                         + ",P_LiveWithFamily_Note  = N'" + LiveWithFamilyDesc_textBox.Text + "'"
                         + ",P_NumAtHome = " +
                         (P_NumAtHome_textBox.Text == "" ? 0 : Convert.ToInt32(P_NumAtHome_textBox.Text)) + ""
                         
                         + ",IsProvider = N'" + IsProvider + "' "
                         + ",P_SourceOfIncome = N'" + P_SourceOfIncome_textBox.Text + "'"
                         + ",P_MedicalCond = N'" + P_MedicalCond_textBox.Text + "'"
                         + ",P_MedicalHelp = " + (MedicalHelp == -1 ? SqlInt32.Null : MedicalHelp) + ""
                         + ",P_MedicalHelp_Note = N'" + P_MedicalHelp_textBox.Text + "'"
                         + ",P_Loss = N'" + P_Loss_textBox.Text + "'"
                         + ",P_IntermidiarySide = N'" + P_IntermidiaryName_textBox.Text + "'"
                         + ",P_HomeState = N'" + home_state + "'"
                         + ",P_HomeState_Note = N'" + HomeDesc_textBox.Text + "'"
                         + ",P_OtherProperties = N'" + other_properties + "'"
                         + ",P_OtherProperties_Note = N'" + OtherProperties_textBox.Text + "'"
                         + ",P_OtherIncomeSources = " + other_incomes + " "
                         + ",P_OtherIncomeSources_Note = N'" + P_OtherIncomeSources_textBox.Text + "' "
                         + ",P_Courses_Note = N'" + OtherCourses_textBox.Text + "'"
                         + ",P_Parish = N'" + P_Parish_comboBox.Text + "'"
                         + ",P_Priest_ID = " + (P_Priest_comboBox.SelectedValue != null
                             ? P_Priest_comboBox.SelectedValue
                             : SqlInt32.Null) + " "
                         + " where P_ID =" + PID;

            //check connection//
            Program.buildConnection();
            {
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        private void Update_PersonPicture(int PID, byte[] PersonPicArr, string fullFtpPath)
        {
            MySS.query = "Update `person` set "
                         + "P_Picture = @PersonPicArr"
                         + ",P_PicturePath = N'" + fullFtpPath + "'"
                         + " where P_ID =" + PID;
            //check connection//
            Program.buildConnection();
            {
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.Parameters.Add(new MySqlParameter("@PersonPicArr", PersonPicArr));
                MySS.sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        private void Update_MP(int MP_ID)
        {
            RequestedAmount = MP_RequestedAmount_textBox.Text.Replace(",", "");
            SimpleProfit = MP_SimpleProfit_textBox.Text.Replace(",", "");
            
            string HasPreviousDonors, IsNeedLicense, HasCompetitors
                , SuppliesInsurance, IncomeKind, HasLedger;

            //if (New_radioButton.Checked) ProjectKind = "New"; // 
            //else if (Expand_radioButton.Checked) ProjectKind = "Expand"; // 
            //else ProjectKind = "";
             
            if (HasPreviousDonors_radioButton.Checked) HasPreviousDonors = "Yes"; // 
            else if (HasNotPreviousDonors_radioButton.Checked) HasPreviousDonors = "No"; // 
            else HasPreviousDonors = "";

            if (MP_NeedLicense_radioButton.Checked) IsNeedLicense = "Yes"; // 
            else if (MP_DontNeedLicense_radioButton.Checked) IsNeedLicense = "No"; // 
            else IsNeedLicense = "";

            if (HasCompetitors_radioButton.Checked) HasCompetitors = "Yes"; // 
            else if (HasNotCompetitors_radioButton.Checked) HasCompetitors = "No"; // 
            else HasCompetitors = "";

            if (EasySupplies_radioButton.Checked) SuppliesInsurance = "Easy"; // 
            else if (HardSupplies_radioButton.Checked) SuppliesInsurance = "Hard"; // 
            else SuppliesInsurance = "";

            if (SingleIncome_radioButton.Checked) IncomeKind = "Single"; // 
            else if (AdditionalIncome_radioButton.Checked) IncomeKind = "Additional"; // 
            else IncomeKind = "";

            if (HasLedger_radioButton.Checked) HasLedger = "Yes"; // 
            else if (HasNotLedger_radioButton.Checked) HasLedger = "No"; // 
            else HasLedger = "";

            MySS.query = "Update `microproject` set " +
                " MP_Name = N'" + MP_Name_textBox.Text + "'" +
                ",MP_RequestedAmount = " + Convert.ToDouble(RequestedAmount) + " " +
                ",MP_PeriodOfExecution = N'" + MP_RequestedTime_textBox.Text + "'" +
                ",MP_DateOfRequest = N'" + ApplyDate_bcDateTimePicker.Value.ToString("yyyy/MM/dd") + "'" +
                ",MP_Description = N'" + MP_Description_textBox.Text + "'" +

                ////",MP_Type = " + (Loan_radioButton.Checked ? 0 : 1) + // 0 = loan , 1 = grant
                ////",MP_ProjectKind = N'" + ProjectKind + "'" +

                ",MP_FundType_ID = " + (Loan_radioButton.Checked ? 1 : 2) + // 1 = loan , 2 = grant
                ",MP_Type_ID = " + (Type_comboBox.SelectedValue != null
                    ? int.Parse(Type_comboBox.SelectedValue.ToString())
                    : 1) + " " + //Default is Micro

                ",MP_SubType_ID = " + (SubType_comboBox.SelectedValue != null
                    ? int.Parse(SubType_comboBox.SelectedValue.ToString())
                    : SqlInt32.Null) + " " +  
                
                ",MP_Category_ID = " + (Category_comboBox.SelectedValue != null
                    ? int.Parse(Category_comboBox.SelectedValue.ToString())
                    : SqlInt32.Null) + " " +

                ",SubCategory_ID = " + (SubCategory_comboBox.SelectedValue != null
                    ? int.Parse(SubCategory_comboBox.SelectedValue.ToString())
                    : SqlInt32.Null) + " " +
  
                ",MP_HasPreviousDonors = N'" + HasPreviousDonors + "'" +
                ",MP_OtherDonors = N'" + MP_OtherDonors_textBox.Text + "'" +
                ",MP_IsNeedLicense = N'" +  IsNeedLicense + "'" +
                ",MP_LicenseSide = N'" + MP_LicenseSide_textBox.Text + "'" +
                ",MP_HasCompetitors = N'" +  HasCompetitors + "'" +
                ",MP_Competitors = N'" + MP_Competitors_textBox.Text + "'" +
                ",MP_SuppliesInsurance = N'" + SuppliesInsurance + "'" +
                ",MP_Suppliers = N'" + MP_Suppliers_textBox.Text + "'" +
                ",MP_Risk = N'" + MP_Risk_textBox.Text + "'" +
                ",MP_Protection = N'" + MP_Protection_textBox.Text + "'" +
                ",MP_OtherNotes = N'" + MP_OtherNotes_textBox.Text + "'" +
                ",MP_Marketing = N'" + MP_Marketing_textBox.Text + "'" +
                ",MP_SimpleProfit = " + (SimpleProfit == "" ? 0 : Convert.ToInt32(SimpleProfit)) + "" +
                ",MP_Profit_Description = N'" + MP_SimpleProfit2_textBox.Text + "'" +
                ",MP_IncomeKind = N'" + IncomeKind + "'" +
                ",MP_IncomeKind_Note = N'" + IncomeDesc_textBox.Text + "'" +
                ",MP_HasLedger = N'" + HasLedger + "'" +
                
                ",Partnership = " + (Partners_lable.Text == "شراكة" ? 2 : 1) + // شراكة 2 - فردي 1
                          
                ",MP_PaperUser_ID = " + (PaperApplicationUser_comboBox.SelectedValue != null
                    ? int.Parse(PaperApplicationUser_comboBox.SelectedValue.ToString())
                    : SqlInt32.Null) + " " +
                
                ",MP_EditedByUser_ID = " + Properties.Settings.Default.userID +
                " where MP_ID = " + MP_ID;
            
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        private void Update_Family(int FID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "Update family set "
                         + "F_Number = N'" + FamilyNum_textBox1.Text + "' "
                         + "where F_ID =" + FID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        private void Update_Wife_Husband(int PID)
        {
            Program.buildConnection();
            var query = "Update `person` set "
                        + "P_FirstName = N'" + FPersonFName_textBox1.Text + "'"
                        + ",P_LastName = N'" + (FPersonLName_textBox1.Text != "" ? FPersonLName_textBox1.Text : "") +
                        "'"
                        + ",P_FatherName = N'" +
                        (FPersonFatherName_textBox1.Text != "" ? FPersonFatherName_textBox1.Text : "") + "'"
                        + ",P_MotherName = N'" +
                        (FPersonMotherName_textBox1.Text != "" ? FPersonMotherName_textBox1.Text : "") + "'"
                        + ",P_Sex = N'" + FPersonSex_comboBox1.Text + "'"
                        + ",P_NationalNumber = N'" + (FPersonNationalNum_textBox1.Text != ""
                            ? FPersonNationalNum_textBox1.Text
                            : "null") + "'"
                        + ",P_DOB = N'" + FPersonDOB_textBox1.Text + "/1/1" + "'"
                        + ",P_MaritalStatus = N'" + FPersonState_comboBox1.Text + "'"
                        + " where P_ID =" + PID;
            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        private void Get_Last_MicroProjectID()
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "select MAX(MP_ID) from `microproject`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            var n = 0;
            int.TryParse(MySS.sc.ExecuteScalar().ToString(), out n);
            MPID_textBox.Text = (++n).ToString();
            Program.MyConn.Close();
        }

        private int Get_FamilyID(string Family_Num)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "select F_ID from `family` where F_Number like '" + Family_Num + "'";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            var id = int.Parse(MySS.sc.ExecuteScalar().ToString());
            Program.MyConn.Close();
            return id;
        }

        private int Get_CurrentPersonID()
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "select MAX(P_ID) from `person` ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            var id = int.Parse(MySS.sc.ExecuteScalar().ToString());
            Program.MyConn.Close();
            return id;
        }

        private void Delete_Person(int PID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete From `person` where P_ID =" + PID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        private void Delete_MP(int MP_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `microproject` where MP_ID = " + MP_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }
        private void Edit_MP(int old_MP_ID,int new_MP_ID)
        {
            //check connection//
            Program.buildConnection(); 
            MySS.query = "update `microproject` set MP_ID = " + new_MP_ID+" where MP_ID = " + old_MP_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        DataSet ds;
        private void Bind_All_ComboBoxes()
        {
            user_mode = false;

            string categroy_query = "SELECT C_ID,C_Name from category ORDER BY C_Name ASC;";
            string sub_category_query = "SELECT ID, Name, Category_ID from `subcategory` ORDER BY Name ASC;";
            string street_query = "SELECT ID, Name from `street` order by Name ASC;";
            string priest_query = "select Priest_ID as 'ID',Priest_Name as 'Name' from `priest` order by Priest_Name ASC;";
            string type_query = "select ID,Name from microprojecttype order by ID ASC;";
            string subtype_query = "select ID,Name,Type_ID from microprojectsubtype order by ID ASC;";

            //string donor_query = "SELECT ID, Name from `donor` ORDER BY Name ASC;";
            //string donor_group_query = "SELECT ID, Name from `donorgroup` ORDER BY Name ASC;";

            string query = categroy_query + sub_category_query + street_query 
                + priest_query + type_query + subtype_query;
            Program.buildConnection();
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            ds = new DataSet();
            da.Fill(ds);
            Program.MyConn.Close();

            Category_comboBox.DataSource = null;
            Category_comboBox.DisplayMember = "C_Name";
            Category_comboBox.ValueMember = "C_ID";
            Category_comboBox.DataSource = ds.Tables[0];
            Category_comboBox.SelectedIndex = -1;

            SubCategory_comboBox.DataSource = null;
            SubCategory_comboBox.DisplayMember = "Name";
            SubCategory_comboBox.ValueMember = "ID";
            SubCategory_comboBox.DataSource = ds.Tables[1];
            SubCategory_comboBox.SelectedIndex = -1;

            Street_comboBox.DataSource = null;
            Street_comboBox.DisplayMember = "Name";
            Street_comboBox.ValueMember = "ID";
            Street_comboBox.DataSource = ds.Tables[2];
            Street_comboBox.SelectedIndex = -1;

            P_Priest_comboBox.DataSource = null;
            P_Priest_comboBox.DisplayMember = "Name";
            P_Priest_comboBox.ValueMember = "ID";
            P_Priest_comboBox.DataSource = ds.Tables[3];
            P_Priest_comboBox.SelectedIndex = -1;

            Type_comboBox.DataSource = null;
            Type_comboBox.DisplayMember = "Name";
            Type_comboBox.ValueMember = "ID";
            Type_comboBox.DataSource = ds.Tables[4];
            Type_comboBox.SelectedIndex = -1;

            SubType_comboBox.DataSource = null;
            SubType_comboBox.DisplayMember = "Name";
            SubType_comboBox.ValueMember = "ID";
            SubType_comboBox.DataSource = ds.Tables[5];
            SubType_comboBox.SelectedIndex = -1;
            user_mode = true;
        }

        //private void Category_bind()
        //{
        //    DataTable cat_st = sub.Category_Select();
        //    Category_comboBox.DataSource = null;
        //    Category_comboBox.DisplayMember = "C_Name";
        //    Category_comboBox.ValueMember = "C_ID";
        //    Category_comboBox.DataSource = cat_st;
        //    Category_comboBox.SelectedIndex = - 1;
        //}
        private void SubCategory_bind(string Category_ID)
        {
            user_mode = false;
            SubCategory_comboBox.DataSource = null;
            SubCategory_comboBox.DisplayMember = "Name";
            SubCategory_comboBox.ValueMember = "ID";

            if (Category_ID != "")
            { 
                DataRow[] rows = null;
                rows = ds.Tables[1].Select("Category_ID=" + Category_ID);

                DataTable selected_rows_dt = new DataTable();
                selected_rows_dt = ds.Tables[1].Copy();
                selected_rows_dt.Rows.Clear();
                foreach (DataRow row in rows)
                    selected_rows_dt.ImportRow(row);

                if (selected_rows_dt.Rows.Count != 0 && selected_rows_dt != null)
                    SubCategory_comboBox.DataSource = selected_rows_dt;
            }
            else
            { 
                SubCategory_comboBox.DataSource = ds.Tables[1];
                SubCategory_comboBox.SelectedIndex = -1;
            }
            user_mode = true;

            //DataTable sub_st = sub.Select(Category_ID, "");
            //SubCategory_comboBox.DataSource = null;
            //SubCategory_comboBox.DisplayMember = "Name";
            //SubCategory_comboBox.ValueMember = "ID";
            //SubCategory_comboBox.DataSource = sub_st;
            //SubCategory_comboBox.SelectedIndex = -1;
        }
        private void SubType_bind(string Type_ID)
        {
            user_mode = false;
            SubType_comboBox.DataSource = null;
            SubType_comboBox.DisplayMember = "Name";
            SubType_comboBox.ValueMember = "ID";

            if (Type_ID != "")
            {
                DataRow[] rows = null;
                rows = ds.Tables[5].Select("Type_ID=" + Type_ID);

                DataTable selected_rows_dt = new DataTable();
                selected_rows_dt = ds.Tables[5].Copy();
                selected_rows_dt.Rows.Clear();
                foreach (DataRow row in rows)
                    selected_rows_dt.ImportRow(row);

                if (selected_rows_dt.Rows.Count != 0 || selected_rows_dt != null)
                    SubType_comboBox.DataSource = selected_rows_dt;
                else
                {

                }
            }
            else
            {
                SubType_comboBox.DataSource = ds.Tables[5];
                SubType_comboBox.SelectedIndex = -1;
            }
            user_mode = true;
        }
        //private void Street_bind()
        //{
        //    DataTable st_st = st.Select("");
        //    Street_comboBox.DataSource = null;
        //    Street_comboBox.DisplayMember = "Name";
        //    Street_comboBox.ValueMember = "ID";
        //    Street_comboBox.DataSource = st_st;
        //    Street_comboBox.SelectedIndex = -1;
        //}
        //private void Priest_bind()
        //{
        //    try
        //    {
        //        //check connection//
        //        Program.buildConnection();
        //        MySS.query = "select Priest_ID,Priest_Name from `priest`";
        //        var orderby = " order by Priest_Name";
        //        MySS.query += orderby;
        //        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
        //        MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
        //        MySS.reader = MySS.sc.ExecuteReader();
        //        MySS.dt = new DataTable();
        //        MySS.dt.Columns.Add("Priest_ID", typeof(string));
        //        MySS.dt.Columns.Add("Priest_Name", typeof(string));
        //        MySS.dt.Load(MySS.reader);
        //        P_Priest_comboBox.DisplayMember = "Priest_Name";
        //        P_Priest_comboBox.ValueMember = "Priest_ID";
        //        P_Priest_comboBox.DataSource = MySS.dt;
        //        P_Priest_comboBox.SelectedIndex = -1;
        //        Program.MyConn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void users_bind(ComboBox comboBox)
        {
            try
            {
                var dt = s.select_visitors();
                comboBox.DataSource = dt;
                comboBox.DisplayMember = "Visitors";
                comboBox.ValueMember = "U_ID";
                comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        #endregion

        #region functions

        private void Insert_Update_FamilyMembers()
        {
            string relationInFamily, WorkName, F_FName, F_LName, F_DOB;
            relationInFamily = WorkName = F_FName = F_LName = F_DOB = "";

            for (var i = 0; i < FamilyMember_dataGridView.RowCount - 1; i++)
            {
                F_FName = FamilyMember_dataGridView.Rows[i].Cells["P_FirstName"].Value.ToString();
                F_DOB = FamilyMember_dataGridView.Rows[i].Cells["P_DOB"].Value.ToString();

                if (FamilyMember_dataGridView.Rows[i].Cells["P_LastName"].Value == null ||
                    FamilyMember_dataGridView.Rows[i].Cells["P_LastName"].Value.ToString() == null)
                    F_LName = "";
                else F_LName = FamilyMember_dataGridView.Rows[i].Cells["P_LastName"].Value.ToString();

                if (FamilyMember_dataGridView.Rows[i].Cells["Work_Name"].Value == null ||
                    FamilyMember_dataGridView.Rows[i].Cells["Work_Name"].Value.ToString() == null)
                    WorkName = "";
                else WorkName = FamilyMember_dataGridView.Rows[i].Cells["Work_Name"].Value.ToString();

                if (FamilyMember_dataGridView.Rows[i].Cells["Relation"].Value == null ||
                    FamilyMember_dataGridView.Rows[i].Cells["Relation"].Value.ToString() == null)
                    relationInFamily = "";
                else relationInFamily = FamilyMember_dataGridView.Rows[i].Cells["Relation"].Value.ToString();

                if (FamilyMember_dataGridView.Rows[i].Cells["P_InDataBase"].Value == null)
                {
                    Insert_FamilyMember(F_FName, F_LName, F_DOB);
                    l.Insert_Log("Insert the Family Member: " + F_FName + " " + F_LName, "Family Member",
                        Settings.Default.username, DateTime.Now);

                    // *** link other family members to same family *** //
                    var F_Member_ID = Get_CurrentPersonID();
                    Insert_FamilyMemberDetails(Family_ID, F_Member_ID, relationInFamily, Person_ID, WorkName);

                    //set P_ID columns
                    FamilyMember_dataGridView.Rows[i].Cells["FP_ID"].Value = F_Member_ID;
                    FamilyMember_dataGridView.Rows[i].Cells["F_ID"].Value = Family_ID;

                    // set that it is in database
                    FamilyMember_dataGridView.Rows[i].Cells["P_InDataBase"].Value = "1";
                }
                else
                {
                    var F_Member_ID = Convert.ToInt32(FamilyMember_dataGridView.Rows[i].Cells["FP_ID"].Value);
                    var F_ID = Convert.ToInt32(FamilyMember_dataGridView.Rows[i].Cells["F_ID"].Value);

                    u.Update_Person_FMember(F_Member_ID, F_FName, F_LName, F_DOB);
                    u.Update_Person_FMember_Details(F_Member_ID, F_ID, relationInFamily, WorkName);
                    l.Insert_Log("Update the Family Member: " + F_FName + " " + F_LName, "Family Member",
                        Settings.Default.username, DateTime.Now);
                }
            }
        }

        private void Insert_Update_PersonDetails(int Person_ID)
        {
            // Course //
            // DELETE old courses and re Insert the new one //
            var Course_ID = -1;
            Program.buildConnection();
            var q = "Delete from `person_course` where Person_ID = " + Person_ID;
            var sc = new MySqlCommand(q, Program.MyConn);
            sc.ExecuteNonQuery();
            Program.MyConn.Close();
            //////////////////////////////////////////////////////////
            foreach (Control c in Course_tableLayoutPanel.Controls)
                if (c.GetType() == typeof(CheckBox))
                {
                    var CBox = c as CheckBox;
                    if (CBox.Checked)
                    {
                        Program.buildConnection();
                        MySS.query = "select ID from `course` where Name like '" + CBox.Text + "' limit 1";
                        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                        Course_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());

                        MySS.query = "Insert Into `person_course`(`Person_ID`, `Course_ID`) values("
                                     + Person_ID + ","
                                     + Course_ID + ")";
                        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                        MySS.sc.ExecuteNonQuery();
                        Program.MyConn.Close();
                        l.Insert_Log(
                            "Insert the Course: " + CBox.Text + " to Beneficiary : " + P_FirstName_textBox.Text + " " +
                            P_FirstName_textBox.Text, "Person_Course", Settings.Default.username, DateTime.Now);
                    }
                }

            // Financial % Sentimental Loss //
            // DELETE old Loss and re Insert the new one //
            var Loss_ID = -1;
            Program.buildConnection();
            q = "Delete from `person_loss` where Person_ID = " + Person_ID;
            sc = new MySqlCommand(q, Program.MyConn);
            sc.ExecuteNonQuery();
            Program.MyConn.Close();
            //////////////////////////////////////////////////////////
            foreach (Control c in Loss_tableLayoutPanel.Controls)
                if (c.GetType() == typeof(CheckBox))
                {
                    var CBox = c as CheckBox;
                    if (CBox.Checked)
                    {
                        Program.buildConnection();
                        MySS.query = "select ID from `loss` where Name like '" + CBox.Text + "' limit 1";
                        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                        Loss_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());

                        MySS.query = "Insert Into `person_loss`(`Person_ID`, `Loss_ID`) values("
                                     + Person_ID + ","
                                     + Loss_ID + ")";
                        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                        MySS.sc.ExecuteNonQuery();
                        Program.MyConn.Close();
                        l.Insert_Log(
                            "Insert the Loss: " + CBox.Text + " to Beneficiary : " + P_FirstName_textBox.Text + " " +
                            P_FirstName_textBox.Text, "Person_Course", Settings.Default.username, DateTime.Now);
                    }
                }

            // Education //
            var edu_Name = "";
            for (var i = 0; i < Education_dataGridView.RowCount - 1; i++)
            {
                if (Education_dataGridView.Rows[i].Cells["E_Name"].Value == null ||
                    Education_dataGridView.Rows[i].Cells["E_Name"].Value.ToString() == null)
                    edu_Name = "";
                else edu_Name = Education_dataGridView.Rows[i].Cells["E_Name"].Value.ToString();

                //set E_ID and P_ID columns
                Education_dataGridView.Rows[i].Cells["P_ID"].Value = Person_ID;
                Program.buildConnection();
                MySS.query = "select E_ID from `education` where E_Name like '" + edu_Name + "' limit 1";
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                var E_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());
                Education_dataGridView.Rows[i].Cells["E_ID"].Value = E_ID;
                Program.MyConn.Close();

                if (Education_dataGridView.Rows[i].Cells["Edu_InDataBase"].Value == null)
                {
                    //Insert into person_education_Table
                    Program.buildConnection();
                    var query = "select MAX(PEdu_ID) from `person_education` ";
                    sc = new MySqlCommand(query, Program.MyConn);
                    var ID = 0;

                    ID = sc.ExecuteScalar() == DBNull.Value ? 0 : Convert.ToInt32(sc.ExecuteScalar().ToString());

                    ID = ID + 1;
                    Program.MyConn.Close();
                    //check connection//
                    Program.buildConnection();
                    MySS.query = "Insert Into `person_education`(`Person_ID`, `Education_ID`,`PEdu_ID`) values("
                                 + Person_ID + ","
                                 + E_ID + ","
                                 + ID + ")";
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    MySS.sc.ExecuteNonQuery();
                    Program.MyConn.Close();
                    Education_dataGridView.Rows[i].Cells["Edu_InDataBase"].Value = "1";
                    Education_dataGridView.Rows[i].Cells["Edu_RowID"].Value = ID;
                    l.Insert_Log(
                        "Insert the Education: " + edu_Name + " to Beneficiary : " + P_FirstName_textBox.Text + " " +
                        P_FirstName_textBox.Text, "Person_Education", Settings.Default.username, DateTime.Now);
                }
                else /// update
                {
                    var ID = Convert.ToInt32(Education_dataGridView.Rows[i].Cells["Edu_RowID"].Value);
                    u.Update_Person_Education(ID, E_ID);
                    l.Insert_Log(
                        "Update the Education: " + edu_Name + " to Beneficiary : " + P_FirstName_textBox.Text + " " +
                        P_FirstName_textBox.Text, "Person_Education", Settings.Default.username, DateTime.Now);
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////

            // work //
            string Work_status_string,
                Work_Name_string,
                Work_beginYear_string,
                Work_endYear_string,
                WorkCauseOfLoseDesc_string,
                WorkDescription_string,
                WorkPlace_string,
                WorkPlaceState_string;

            float WorkSalary = 0;
            Work_beginYear_string = Work_endYear_string = "0";
            WorkCauseOfLoseDesc_string = WorkDescription_string = WorkPlace_string = WorkPlaceState_string = " ";
            Work_status_string = " ";
            for (var i = 0; i < Work_dataGridView.RowCount - 1; i++)
            {
                // get the data of 1 row
                if (Work_dataGridView.Rows[i].Cells["W_Name"].Value == null ||
                    Work_dataGridView.Rows[i].Cells["W_Name"].Value.ToString() == null)
                    Work_Name_string = " ";
                else Work_Name_string = Work_dataGridView.Rows[i].Cells["W_Name"].Value.ToString();

                if (Work_dataGridView.Rows[i].Cells["W_Description"].Value == null ||
                    Work_dataGridView.Rows[i].Cells["W_Description"].Value.ToString() == null)
                    WorkDescription_string = " ";
                else WorkDescription_string = Work_dataGridView.Rows[i].Cells["W_Description"].Value.ToString();

                if (Work_dataGridView.Rows[i].Cells["W_BeginDate"].Value == null ||
                    Work_dataGridView.Rows[i].Cells["W_BeginDate"].Value.ToString() == null)
                    Work_beginYear_string = "0000";
                else Work_beginYear_string = Work_dataGridView.Rows[i].Cells["W_BeginDate"].Value.ToString();

                if (Work_dataGridView.Rows[i].Cells["W_EndDate"].Value == null ||
                    string.IsNullOrWhiteSpace(Work_dataGridView.Rows[i].Cells["W_EndDate"].Value.ToString()))
                    Work_endYear_string = "0000";
                else Work_endYear_string = Work_dataGridView.Rows[i].Cells["W_EndDate"].Value.ToString();

                if (Work_dataGridView.Rows[i].Cells["W_Place"].Value == null ||
                    string.IsNullOrWhiteSpace(Work_dataGridView.Rows[i].Cells["W_Place"].Value.ToString()))
                    WorkPlace_string = "";
                else WorkPlace_string = Work_dataGridView.Rows[i].Cells["W_Place"].Value.ToString();

                if (Work_dataGridView.Rows[i].Cells["W_PlaceState"].Value == null ||
                    string.IsNullOrWhiteSpace(Work_dataGridView.Rows[i].Cells["W_PlaceState"].Value.ToString()))
                    WorkPlaceState_string = "";
                else WorkPlaceState_string = Work_dataGridView.Rows[i].Cells["W_PlaceState"].Value.ToString();

                if (Work_dataGridView.Rows[i].Cells["W_CauseOfLose"].Value == null ||
                    string.IsNullOrWhiteSpace(Work_dataGridView.Rows[i].Cells["W_CauseOfLose"].Value.ToString()))
                    WorkCauseOfLoseDesc_string = " ";
                else WorkCauseOfLoseDesc_string = Work_dataGridView.Rows[i].Cells["W_CauseOfLose"].Value.ToString();

                if (Work_dataGridView.Rows[i].Cells["W_Salary"].Value == null ||
                    string.IsNullOrWhiteSpace(Work_dataGridView.Rows[i].Cells["W_Salary"].Value.ToString()))
                    WorkSalary = 0;
                else float.TryParse(Work_dataGridView.Rows[i].Cells["W_Salary"].Value.ToString(), out WorkSalary);

                //set W_ID and P_ID columns

                Program.buildConnection();
                Work_dataGridView.Rows[i].Cells["WP_ID"].Value = Person_ID;
                MySS.query = "select W_ID from `work` where W_Name like '" + Work_Name_string + "' limit 1";
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                var W_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());
                Work_dataGridView.Rows[i].Cells["W_ID"].Value = W_ID;
                Program.MyConn.Close();

                Work_status_string = "Yes"; //حالي

                if (Work_dataGridView.Rows[i].Cells["W_InDataBase"].Value == null)
                {
                    //Insert into person_workexp_Table
                    //check connection//
                    Program.buildConnection();
                    var query = "select MAX(PW_ID) from `person_workexp` ";
                    sc = new MySqlCommand(query, Program.MyConn);
                    var ID = 0;

                    ID = sc.ExecuteScalar() == DBNull.Value ? 0 : Convert.ToInt32(sc.ExecuteScalar().ToString());
                    ID = ID + 1;
                    Program.MyConn.Close();
                    //check connection//
                    Program.buildConnection();
                    MySS.query = "Insert Into `person_workexp`(`PW_ID`,`Person_ID`, `Work_ID`" +
                                 ", `W_Description`, `W_BeginDate`, `W_EndDate`, `W_Place`,`W_PlaceState`, `W_Status`" +
                                 ", `W_CauseOfLose`, `W_CauseOfLoseDescription`, `W_Salary`,`MicroProject_ID`) values("
                                 + ID + "," + Person_ID + "," + W_ID
                                 + ",N'" + WorkDescription_string + "',N'" + Work_beginYear_string + "',N'" +
                                 Work_endYear_string + "',N'" + WorkPlace_string
                                 + "',N'" + WorkPlaceState_string + "',N'" + Work_status_string + "',N' ',N'" +
                                 WorkCauseOfLoseDesc_string + "'," + WorkSalary + "," + MicroProject_ID
                                 + ")";
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    MySS.sc.ExecuteNonQuery();
                    Program.MyConn.Close();

                    Work_dataGridView.Rows[i].Cells["W_RowID"].Value = ID;
                    Work_dataGridView.Rows[i].Cells["W_InDataBase"].Value = "1";
                    l.Insert_Log(
                        "Insert the Work: " + Work_Name_string + " to Beneficiary : " + P_FirstName_textBox.Text + " " +
                        P_FirstName_textBox.Text, "Person_Work", Settings.Default.username, DateTime.Now);
                }
                else /// update
                {
                    var ID = Convert.ToInt32(Work_dataGridView.Rows[i].Cells["W_RowID"].Value);
                    u.Update_Person_WorkExp(ID, W_ID, WorkDescription_string, Work_beginYear_string,
                        Work_endYear_string, WorkPlace_string, WorkPlaceState_string, WorkCauseOfLoseDesc_string,
                        WorkSalary);
                    l.Insert_Log(
                        "Update the Work: " + Work_Name_string + " to Beneficiary : " + P_FirstName_textBox.Text + " " +
                        P_FirstName_textBox.Text, "Person_Work", Settings.Default.username, DateTime.Now);
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////

            Work_beginYear_string = Work_endYear_string = "0";
            WorkCauseOfLoseDesc_string = WorkDescription_string = WorkPlace_string = " ";
            Work_status_string = " ";
            // experience //
            for (var i = 0; i < Exp_dataGridView.RowCount - 1; i++)
            {
                // get the data of 1 row
                if (Exp_dataGridView.Rows[i].Cells["Exp_Name"].Value == null ||
                    Exp_dataGridView.Rows[i].Cells["Exp_Name"].Value.ToString() == null)
                    Work_Name_string = " ";
                else Work_Name_string = Exp_dataGridView.Rows[i].Cells["Exp_Name"].Value.ToString();

                if (Exp_dataGridView.Rows[i].Cells["Exp_Description"].Value == null)
                    WorkDescription_string = " ";
                else WorkDescription_string = Exp_dataGridView.Rows[i].Cells["Exp_Description"].Value.ToString();

                if (Exp_dataGridView.Rows[i].Cells["Exp_BeginDate"].Value == null)
                    Work_beginYear_string = "0000";
                else Work_beginYear_string = Exp_dataGridView.Rows[i].Cells["Exp_BeginDate"].Value.ToString();

                if (Exp_dataGridView.Rows[i].Cells["Exp_EndDate"].Value == null)
                    Work_endYear_string = "0000";
                else Work_endYear_string = Exp_dataGridView.Rows[i].Cells["Exp_EndDate"].Value.ToString();

                if (Exp_dataGridView.Rows[i].Cells["Exp_Place"].Value == null)
                    WorkPlace_string = " ";
                else WorkPlace_string = Exp_dataGridView.Rows[i].Cells["Exp_Place"].Value.ToString();

                if (Exp_dataGridView.Rows[i].Cells["Exp_PlaceState"].Value == null ||
                    string.IsNullOrWhiteSpace(Exp_dataGridView.Rows[i].Cells["Exp_PlaceState"].Value.ToString()))
                    WorkPlaceState_string = "";
                else WorkPlaceState_string = Exp_dataGridView.Rows[i].Cells["Exp_PlaceState"].Value.ToString();


                if (Exp_dataGridView.Rows[i].Cells["Exp_CauseOfLose"].Value == null)
                    WorkCauseOfLoseDesc_string = " ";
                else WorkCauseOfLoseDesc_string = Exp_dataGridView.Rows[i].Cells["Exp_CauseOfLose"].Value.ToString();

                if (Exp_dataGridView.Rows[i].Cells["Exp_Salary"].Value == null)
                    WorkSalary = 0;
                else float.TryParse(Exp_dataGridView.Rows[i].Cells["Exp_Salary"].Value.ToString(), out WorkSalary);

                //set W_ID and P_ID columns
                Program.buildConnection();
                Exp_dataGridView.Rows[i].Cells["ExpP_ID"].Value = Person_ID;
                MySS.query = "select W_ID from `work` where W_Name like '" + Work_Name_string + "' limit 1";
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                var Exp_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());
                Exp_dataGridView.Rows[i].Cells["Exp_ID"].Value = Exp_ID;
                Program.MyConn.Close();


                Work_status_string = "No"; //سابق
                if (Exp_dataGridView.Rows[i].Cells["Exp_InDataBase"].Value == null)
                {
                    //Insert into person_workexp_Table
                    //check connection//
                    Program.buildConnection();
                    var query = "select MAX(PW_ID) from `person_workexp` ";
                    sc = new MySqlCommand(query, Program.MyConn);

                    var ID = 0;
                    ID = sc.ExecuteScalar() == DBNull.Value ? 0 : Convert.ToInt32(sc.ExecuteScalar().ToString());

                    ID = ID + 1;
                    Program.MyConn.Close();
                    //check connection//
                    Program.buildConnection();
                    MySS.query = "Insert Into `person_workexp`(`PW_ID`,`Person_ID`, `Work_ID`," +
                                 " `W_Description`, `W_BeginDate`, `W_EndDate`, `W_Place`, `W_PlaceState`, `W_Status`," +
                                 " `W_CauseOfLose`, `W_CauseOfLoseDescription`, `W_Salary`,`MicroProject_ID`) values("
                                 + ID + "," + Person_ID + "," + Exp_ID
                                 + ",N'" + WorkDescription_string + "',N'" + Work_beginYear_string + "',N'" +
                                 Work_endYear_string + "',N'" + WorkPlace_string + "',N'" + WorkPlaceState_string
                                 + "',N'" + Work_status_string + "',N' ',N'" + WorkCauseOfLoseDesc_string + "'," +
                                 WorkSalary + "," + MicroProject_ID
                                 + ")";
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    MySS.sc.ExecuteNonQuery();
                    Program.MyConn.Close();

                    Exp_dataGridView.Rows[i].Cells["Exp_InDataBase"].Value = "1";
                    Exp_dataGridView.Rows[i].Cells["Exp_RowID"].Value = ID;
                    l.Insert_Log(
                        "Insert the Work: " + Work_Name_string + " to Beneficiary : " + P_FirstName_textBox.Text + " " +
                        P_FirstName_textBox.Text, "Person_Work", Settings.Default.username, DateTime.Now);
                }
                else /// update
                {
                    var ID = Convert.ToInt32(Exp_dataGridView.Rows[i].Cells["Exp_RowID"].Value);
                    u.Update_Person_WorkExp(ID, Exp_ID, WorkDescription_string, Work_beginYear_string,
                        Work_endYear_string, WorkPlace_string, WorkPlaceState_string, WorkCauseOfLoseDesc_string,
                        WorkSalary);
                    l.Insert_Log(
                        "Update the Work: " + Work_Name_string + " to Beneficiary : " + P_FirstName_textBox.Text + " " +
                        P_FirstName_textBox.Text, "Person_Work", Settings.Default.username, DateTime.Now);
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////
        }

        private void Insert_Update_ProjectDetails(int MicroProject_ID)
        {
            //Budget Materials//
            string Name, Comments, Amount, Price, LocalContribution;
            for (var i = 0; i < Materials_dataGridView.RowCount - 1; i++)
            {
                Name = Materials_dataGridView.Rows[i].Cells["M_Name"].Value.ToString();
                Amount = Materials_dataGridView.Rows[i].Cells["M_Amount"].Value.ToString();
                Price = Materials_dataGridView.Rows[i].Cells["M_Price"].Value.ToString();

                if (Materials_dataGridView.Rows[i].Cells["M_LocalContribution"].Value == null)
                    LocalContribution = "0";
                else
                    LocalContribution = Materials_dataGridView.Rows[i].Cells["M_LocalContribution"].Value.ToString();

                if (Materials_dataGridView.Rows[i].Cells["M_Comments"].Value == null)
                    Comments = "";
                else Comments = Materials_dataGridView.Rows[i].Cells["M_Comments"].Value.ToString();

                Materials_dataGridView.Rows[i].Cells["MMP_ID"].Value = MicroProject_ID;

                if (Materials_dataGridView.Rows[i].Cells["M_InDataBase"].Value == null)
                {
                    //Insert into materials table
                    Insert_MicroProject_Material(Name, Convert.ToInt32(Amount), Convert.ToDouble(Price),
                        Convert.ToDouble(LocalContribution), Comments, MicroProject_ID);

                    Program.buildConnection();
                    MySS.query = "select MAX(ID) from `material` ";
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    var M_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());
                    Program.MyConn.Close();

                    Materials_dataGridView.Rows[i].Cells["M_ID"].Value = M_ID;
                    Materials_dataGridView.Rows[i].Cells["M_InDataBase"].Value = "1";

                    l.Insert_Log("Insert the material: " + Name + " to the project: " + MicroProject_ID, "Material",
                        Settings.Default.username, DateTime.Now);
                }
                else /// Update on existing rows
                {
                    var M_ID = Convert.ToInt32(Materials_dataGridView.Rows[i].Cells["M_ID"].Value);
                    u.Update_Material(M_ID, Name, Convert.ToInt32(Amount), Convert.ToDouble(Price),
                        Convert.ToDouble(LocalContribution), Comments);

                    l.Insert_Log("Update the material: " + Name + " to the project: " + MicroProject_ID, "Material",
                        Settings.Default.username, DateTime.Now);
                }
            }

            // Risks //
            // DELETE old Risks and re Insert the new one //
            var Risk_ID = -1;
            Program.buildConnection();
            var q = "Delete from `microproject_risk` where MicroProject_ID = " + MicroProject_ID;
            var sc = new MySqlCommand(q, Program.MyConn);
            sc.ExecuteNonQuery();
            Program.MyConn.Close();
            //////////////////////////////////////////////////////////
            foreach (Control c in Risks_tableLayoutPanel.Controls)
                if (c.GetType() == typeof(CheckBox))
                {
                    var CBox = c as CheckBox;
                    if (CBox.Checked)
                    {
                        Program.buildConnection();
                        MySS.query = "select ID from `risk` where Name like '" + CBox.Text + "' limit 1";
                        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                        Risk_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());

                        MySS.query = "Insert Into `microproject_risk`(`MicroProject_ID`, `Risk_ID`) values("
                                     + MicroProject_ID + ","
                                     + Risk_ID + ")";
                        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                        MySS.sc.ExecuteNonQuery();
                        Program.MyConn.Close();
                        l.Insert_Log(
                            "Insert the Risk: " + CBox.Text + " to Beneficiary : " + P_FirstName_textBox.Text + " " +
                            P_FirstName_textBox.Text, "microproject_risk", Settings.Default.username, DateTime.Now);
                    }
                }

            // Score //
            // DELETE old Score and re Insert the new one //
            var Score_ID = -1;
            var Value = -1;
            Program.buildConnection();
            q = "Delete from `microproject_score` where MicroProject_ID = " + MicroProject_ID;
            sc = new MySqlCommand(q, Program.MyConn);
            sc.ExecuteNonQuery();
            //////////////////////////////////////////////////////////
            
            if (s1_radioButton.Checked && !n1_radioButton.Checked)
            {
                Score_ID = 1;
                Value = 1;
            }
            else if (!s1_radioButton.Checked && n1_radioButton.Checked)
            {
                Score_ID = 1;
                Value = 0;
            }
            else if (!s1_radioButton.Checked && !n1_radioButton.Checked)
            {
                Score_ID = 1;
                Value = -1;
            }

            MySS.query = "INSERT INTO `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values(" +
                         MicroProject_ID + "," + Score_ID + "," + Value + ",N'" + s1_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            if (s2_radioButton.Checked && !n2_radioButton.Checked)
            {
                Score_ID = 2;
                Value = 1;
            }
            else if (!s2_radioButton.Checked && n2_radioButton.Checked)
            {
                Score_ID = 2;
                Value = 0;
            }
            else if (!s2_radioButton.Checked && !n2_radioButton.Checked)
            {
                Score_ID = 2;
                Value = -1;
            }

            MySS.query = "INSERT INTO `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values(" +
                         MicroProject_ID + "," + Score_ID + "," + Value + ",N'" + s2_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            if (s3_radioButton.Checked && !n3_radioButton.Checked)
            {
                Score_ID = 3;
                Value = 1;
            }
            else if (!s3_radioButton.Checked && n3_radioButton.Checked)
            {
                Score_ID = 3;
                Value = 0;
            }
            else if (!s3_radioButton.Checked && !n3_radioButton.Checked)
            {
                Score_ID = 3;
                Value = -1;
            }

            MySS.query = "INSERT INTO `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values(" +
                         MicroProject_ID + "," + Score_ID + "," + Value + ",N'" + s3_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            if (s4_radioButton.Checked && !n4_radioButton.Checked)
            {
                Score_ID = 4;
                Value = 1;
            }
            else if (!s4_radioButton.Checked && n4_radioButton.Checked)
            {
                Score_ID = 4;
                Value = 0;
            }
            else if (!s4_radioButton.Checked && !n4_radioButton.Checked)
            {
                Score_ID = 4;
                Value = -1;
            }

            MySS.query = "INSERT INTO `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values(" +
                         MicroProject_ID + "," + Score_ID + "," + Value + ",N'" + s4_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            if (s5_radioButton.Checked && !n5_radioButton.Checked)
            {
                Score_ID = 5;
                Value = 1;
            }
            else if (!s5_radioButton.Checked && n5_radioButton.Checked)
            {
                Score_ID = 5;
                Value = 0;
            }
            else if (!s5_radioButton.Checked && !n5_radioButton.Checked)
            {
                Score_ID = 5;
                Value = -1;
            }

            MySS.query = "INSERT INTO `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values(" +
                         MicroProject_ID + "," + Score_ID + "," + Value + ",N'" + s5_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            if (s6_radioButton.Checked && !n6_radioButton.Checked)
            {
                Score_ID = 6;
                Value = 1;
            }
            else if (!s6_radioButton.Checked && n6_radioButton.Checked)
            {
                Score_ID = 6;
                Value = 0;
            }
            else if (!s6_radioButton.Checked && !n6_radioButton.Checked)
            {
                Score_ID = 6;
                Value = -1;
            }

            MySS.query = "INSERT INTO `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values(" +
                         MicroProject_ID + "," + Score_ID + "," + Value + ",N'" + s6_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            if (s7_radioButton.Checked && !n7_radioButton.Checked)
            {
                Score_ID = 7;
                Value = 1;
            }
            else if (!s7_radioButton.Checked && n7_radioButton.Checked)
            {
                Score_ID = 7;
                Value = 0;
            }
            else if (!s7_radioButton.Checked && !n7_radioButton.Checked)
            {
                Score_ID = 7;
                Value = -1;
            }

            MySS.query = "INSERT INTO `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values(" +
                         MicroProject_ID + "," + Score_ID + "," + Value + ",N'" + s7_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            if (s8_radioButton.Checked && !n8_radioButton.Checked)
            {
                Score_ID = 8;
                Value = 1;
            }
            else if (!s8_radioButton.Checked && n8_radioButton.Checked)
            {
                Score_ID = 8;
                Value = 0;
            }
            else if (!s8_radioButton.Checked && !n8_radioButton.Checked)
            {
                Score_ID = 8;
                Value = -1;
            }

            MySS.query = "INSERT INTO `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values(" +
                         MicroProject_ID + "," + Score_ID + "," + Value + ",N'" + s8_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            if (s9_radioButton.Checked && !n9_radioButton.Checked)
            {
                Score_ID = 9;
                Value = 1;
            }
            else if (!s9_radioButton.Checked && n9_radioButton.Checked)
            {
                Score_ID = 9;
                Value = 0;
            }
            else if (!s9_radioButton.Checked && !n9_radioButton.Checked)
            {
                Score_ID = 9;
                Value = -1;
            }

            MySS.query = "INSERT INTO `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values(" +
                         MicroProject_ID + "," + Score_ID + "," + Value + ",N'" + s9_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

            if (s0_radioButton.Checked && !n0_radioButton.Checked)
            {
                Score_ID = 10;
                Value = 1;
            }
            else if (!s0_radioButton.Checked && n0_radioButton.Checked)
            {
                Score_ID = 10;
                Value = 0;
            }
            else if (!s0_radioButton.Checked && !n0_radioButton.Checked)
            {
                Score_ID = 10;
                Value = -1;
            }

            MySS.query = "INSERT INTO `microproject_score`(`MicroProject_ID`, `Score_ID`, `value`, `notes`) values(" +
                         MicroProject_ID + "," + Score_ID + "," + Value + ",N'" + s0_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void Insert_Update_AllFamilyDetails()
        {
            try
            {
                if (FamilyNum_textBox1.Text == "")
                {
                    MessageBox.Show("انتبه! لم تقم بتعبئة رقم دفتر العائلة للمستفيد ! ولن يقوم البرنامج بحفظ بيانات أفراد العائلة.");
                }
                else
                {
                    //// create new family /////
                    if (NoFamily)
                    {
                        Insert_Family();
                        l.Insert_Log("Insert the Family: " + FamilyNum_textBox1.Text, "Family",
                            Settings.Default.username, DateTime.Now);
                        Family_ID = Get_FamilyID(FamilyNum_textBox1.Text);

                        // *** Insert the beneficiary to this family *** //
                        Insert_FamilyMemberDetails(Family_ID, Person_ID, "مستفيد", Person_ID, "لا يوجد");
                        l.Insert_Log(
                            "Insert the Beneficiary " + P_FirstName_textBox.Text + " " + P_LastName_textBox.Text +
                            " to Family: " + FamilyNum_textBox1.Text, "Person_Family", Settings.Default.username,
                            DateTime.Now);

                        NoFamily = false;
                    }
                    //// Update family /////
                    else
                    {
                        Update_Family(Family_ID);
                        l.Insert_Log("Update the Family: " + FamilyNum_textBox1.Text, "Family",
                            Settings.Default.username, DateTime.Now);
                    }

                    // ***  Wife - Hunband *** // 
                    Insert_Update_WifeHusband();

                    //// *** Insert other family members *** //  only that aren't in database
                    /////////////////////////////////////////
                    Insert_Update_FamilyMembers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Update_WifeHusband()
        {
            try
            {
                /////// wife / husband /////////
                if (FPersonFName_textBox1.Text != "" || FPersonLName_textBox1.Text != "" ||
                    FPersonFatherName_textBox1.Text != "") //there is a wife
                {
                    //Program.buildConnection();
                    ///// check if he/she was in database
                    //var query = "select count(*) from `person` where P_ID = " + Husband_Wife_ID +
                    //            " or ( P_FirstName like '" + FPersonFName_textBox1.Text + "'"
                    //            + " and P_LastName like '" + FPersonLName_textBox1.Text + "'"
                    //            + " and P_FatherName like '" + FPersonFatherName_textBox1.Text + "' )";
                    //MySS.sc = new MySqlCommand(query, Program.MyConn);
                    //var ID = Convert.ToInt32(MySS.sc.ExecuteScalar());
                    //Program.MyConn.Close();

                    //if (ID == 0)
                    if(Husband_Wife_ID == -1)
                    {
                        //check the national number of wife/husband
                        if (FPersonNationalNum_textBox1.Text != "")
                            Check_National_Number(FPersonNationalNum_textBox1.Text);

                        // *** Insert the wife/husband *** //
                        Insert_Wife_Husband();

                        // *** link the wife/husband to same family *** //
                        Husband_Wife_ID = Get_CurrentPersonID();
                        Insert_FamilyMemberDetails(Family_ID, Husband_Wife_ID, "زوج/ة", Person_ID, " ");
                        l.Insert_Log("Insert the wife/husband: " + FPersonFName_textBox1.Text + " " +
                            FPersonLName_textBox1.Text + " to Family: " + FamilyNum_textBox1.Text, "Person_Family",
                            Settings.Default.username, DateTime.Now);
                    }
                    else /// udpade wife data
                    {
                        /////////////////////////////
                        Update_Wife_Husband(Husband_Wife_ID);
                        l.Insert_Log("Update the wife/husband: " + FPersonFName_textBox1.Text + " " +
                            FPersonLName_textBox1.Text + " to Family: " + FamilyNum_textBox1.Text, "Person_Family",
                            Settings.Default.username, DateTime.Now);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Check_MicroProject_ID(string MP_ID)
        {  
            Program.buildConnection();
            MySqlCommand sc = new MySqlCommand("select count(MP_ID) from `microproject` where MP_ID = " + MP_ID, Program.MyConn);
            var check_MP_ID = Convert.ToInt32(sc.ExecuteScalar());
            Program.MyConn.Close();
            if (check_MP_ID == 0)
            {
                return true; //امورنا بالسليم
            }
            else
            {
                return false; //في حدا تاني بهالرقم
            }
        }
        private void Check_National_Number(string PersonNationalNum)
        {
            var PersonNationalNum_without_zero = "";
            if (PersonNationalNum.ElementAt(0).ToString() == "0")
                PersonNationalNum_without_zero = PersonNationalNum.Remove(0, 1);
            else
                PersonNationalNum_without_zero = PersonNationalNum;

            Program.buildConnection();
            MySS.sc = new MySqlCommand(
                "select count(P_NationalNumber) from `person` where P_NationalNumber like '%" +
                PersonNationalNum_without_zero + "'", Program.MyConn);
            var check_NationalID = Convert.ToInt32(MySS.sc.ExecuteScalar());
            Program.MyConn.Close();
            if (check_NationalID != 0)
            {
                Program.buildConnection();
                // Get ID 
                var query = "select CONCAT(P.P_FirstName, ' ',P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                            + ",MP.MP_ID 'MicroProject_ID'"
                            + ",P.P_ID 'P_ID'"
                            + " from `microproject` MP inner join person_microproject PMP on PMP.MicroProject_ID = MP.MP_ID "
                            + " inner join person P on P.P_ID = PMP.Person_ID "
                            + " where P_NationalNumber like '%" + PersonNationalNum_without_zero + "'";
                var sc = new MySqlCommand(query, Program.MyConn);
                sc.ExecuteNonQuery();
                var da = new MySqlDataAdapter(sc);
                var dt = new DataTable();
                da.Fill(dt);

                Program.MyConn.Close();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        int P_ID = int.Parse(dt.Rows[0]["P_ID"].ToString());
                        int MP_ID = int.Parse(dt.Rows[0]["MicroProject_ID"].ToString());
                        var Name = dt.Rows[0]["Beneficiary Name"].ToString();

                        var _MessageBox = new Application_MessageBox(P_ID, MP_ID, Name);
                        var r = _MessageBox.ShowDialog();
                        if (r == DialogResult.Yes)
                        {
                            //////open Old Application in new tab 
                            Form application_Form = new Application_Form(P_ID, MP_ID, mainForm);
                            mainForm.showNewTab(application_Form, "Application",1);
                        }
                        else if (r == DialogResult.No)
                        {
                            OldBeneficiary_NewProject = true;
                            Person_ID = int.Parse(dt.Rows[0]["P_ID"].ToString());
                            MicroProject_ID = int.Parse(dt.Rows[0]["MicroProject_ID"].ToString()); 

                            Fill_Person();
                            Fill_PersonDetails();
                            Fill_FamilyMembers(0);

                            clear_Project_boxes();
                        }
                        else
                        {
                            //تراجع
                            //Nothing to do//
                            //P_NationalNum_textBox.Focus();

                            //Person_ID = MicroProject_ID = -1;
                            //OldBeneficiary_NewProject = false;
                            //P_NationalNum_textBox.Clear();
                        }
                    }
                }
                //}
                else
                {
                    throw new Exception(
                        "حدث مشكلة أثناء حفظ البيانات ! الرجاء التأكد من البيانات والمحاولة مرة ثانية.");
                }
            }
        }

        private int Check_By_Beneficiary_Name(string f_name, string l_name, string father_name)
        {
            Program.buildConnection();
            var query = "select `P_ID` from `person` where P_FirstName like '" + f_name + "%' and P_LastName like '" +
                        l_name + "%' father_name like '" + father_name + "%' ";
            MySS.sc = new MySqlCommand(query, Program.MyConn);
            var P_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());
            Program.MyConn.Close();
            return P_ID;
        }


        #endregion

        #region fill to Update ---- clear

        private void Fill_Person()
        {
            try
            {
                MySS.dt = s.Person_bind(Person_ID);
                if (MySS.dt != null)
                    if (MySS.dt.Rows.Count > 0)
                    {
                        Beneficiary_Count++;

                        Person_ID = int.Parse(MySS.dt.Rows[0]["ID"].ToString());
                        P_FirstName_textBox.Text = MySS.dt.Rows[0]["First Name"].ToString();
                        P_LastName_textBox.Text = (string) MySS.dt.Rows[0]["Last Name"];
                        P_FatherName_textBox.Text = (string) MySS.dt.Rows[0]["Father Name"];
                        P_MotherName_textBox.Text = (string) MySS.dt.Rows[0]["Mother Name"];

                        if (MySS.dt.Rows[0]["PersonType"] == DBNull.Value || MySS.dt.Rows[0]["PersonType"] == null)
                            PersonType_comboBox.SelectedIndex = 0;
                        else PersonType_comboBox.SelectedItem = ((string)MySS.dt.Rows[0]["PersonType"] == "مستفيد" ? "المستفيد:" : "الشريك:");

                        var date = (DateTime) MySS.dt.Rows[0]["Birth Date"];
                        P_DOB_textBox.Text = date.Year.ToString();

                        var MilitaryServices = (string) MySS.dt.Rows[0]["Military Services"];
                        if (MilitaryServices.Equals("مسرح")) MilitaryFinished_radioButton.Checked = true;
                        else if (MilitaryServices.Equals("عسكري")) MilitaryNow_radioButton.Checked = true;
                        else if (MilitaryServices.Equals("مؤجل")) MilitaryDelayed_radioButton.Checked = true;
                        else if (MilitaryServices.Equals("وحيد")) MilitarySingle_radioButton.Checked = true;
                        else MilitaryFinished_radioButton.Checked = MilitaryNow_radioButton.Checked 
                                = MilitaryDelayed_radioButton.Checked = MilitarySingle_radioButton.Checked = false;

                        Military_textBox.Text = (string) MySS.dt.Rows[0]["Military Services Note"];
                         
                        P_Mobile_textBox.Text = (string) MySS.dt.Rows[0]["Mobile"];
                        P_HomeTel_textBox.Text = (string) MySS.dt.Rows[0]["Land Line"];

                        if (MySS.dt.Rows[0]["Street_ID"] == DBNull.Value || MySS.dt.Rows[0]["Street_ID"] == null)
                            Street_comboBox.SelectedIndex = -1;
                        else Street_comboBox.SelectedValue = int.Parse(MySS.dt.Rows[0]["Street_ID"].ToString());

                        P_Address_textBox.Text = (string) MySS.dt.Rows[0]["Home Address"];
                        if (MySS.dt.Rows[0]["National Number"] != DBNull.Value)
                            P_NationalNum_textBox.Text = (string) MySS.dt.Rows[0]["National Number"];
                        else
                            P_NationalNum_textBox.Text = "لا يوجد";

                        P_Registration_textBox.Text = (string) MySS.dt.Rows[0]["Nationality"];
                        P_Sex_comboBox.Text = (string) MySS.dt.Rows[0]["Gender"];
                        P_State_comboBox.Text = (string) MySS.dt.Rows[0]["Marital Status"];

                        var LiveWithAnotherFamily = (string) MySS.dt.Rows[0]["Is Living With Family"];
                        if (LiveWithAnotherFamily == "Yes") P_LiveWithFamily_radioButton.Checked = true;
                        else if (LiveWithAnotherFamily == "No") P_NotLiveWithFamily_radioButton.Checked = true;
                        else P_LiveWithFamily_radioButton.Checked = P_NotLiveWithFamily_radioButton.Checked = false;

                        LiveWithFamilyDesc_textBox.Text = (string) MySS.dt.Rows[0]["Living With Family Note"];

                        P_NumAtHome_textBox.Text = MySS.dt.Rows[0]["Family members at home"].ToString();

                        var Provider = (string) MySS.dt.Rows[0]["Provider"];
                        if (Provider == "Yes") IsProvider_radioButton.Checked = true;
                        else if (Provider == "No") IsNotProvider_radioButton.Checked = true;
                        else IsProvider_radioButton.Checked = IsNotProvider_radioButton.Checked = false;

                        P_SourceOfIncome_textBox.Text = (string) MySS.dt.Rows[0]["Source Of Income"];
                        P_MedicalCond_textBox.Text = (string) MySS.dt.Rows[0]["Medical Conditions"];

                        if (MySS.dt.Rows[0]["Medical Help"] != DBNull.Value)
                        {
                            var MedicalHelp = (int) MySS.dt.Rows[0]["Medical Help"];
                            if (MedicalHelp == 1) MedicalHelp_radioButton.Checked = true;
                            else if (MedicalHelp == 0) NoMedicalHelp_radioButton.Checked = true;
                            else MedicalHelp_radioButton.Checked = NoMedicalHelp_radioButton.Checked = false;
                        }

                        P_MedicalHelp_textBox.Text = (string) MySS.dt.Rows[0]["Medical Help Note"];
                        P_Loss_textBox.Text = (string) MySS.dt.Rows[0]["Loss"];
                        P_IntermidiaryName_textBox.Text = (string) MySS.dt.Rows[0]["Intermidiary Side"];
                        /////////////////////////////////////////////////////////////////////////////////

                        string HomeState, OtherIncomes;
                        if (MySS.dt.Rows[0]["Home State"] == DBNull.Value || MySS.dt.Rows[0]["Home State"].ToString() == "")
                        {
                            P_HomeRent_radioButton.Checked = P_HomeOwnership_radioButton.Checked = false;
                        }
                        else
                        {
                            HomeState = (string) MySS.dt.Rows[0]["Home State"];
                            if (HomeState ==  P_HomeRent_radioButton.Text) P_HomeRent_radioButton.Checked = true;
                            else if (HomeState == P_HomeOwnership_radioButton.Text) P_HomeOwnership_radioButton.Checked = true;
                            else P_HomeRent_radioButton.Checked = P_HomeOwnership_radioButton.Checked = false;
                        }

                        HomeDesc_textBox.Text = (string) MySS.dt.Rows[0]["Home State Note"];
                        //////////////////////////////////////////////////

                        if (MySS.dt.Rows[0]["Other Properties"] == DBNull.Value)
                            P_Car_checkBox.Checked = P_Rented_checkBox.Checked = false; 
                        else
                        {
                            var OtherProperties = (int) MySS.dt.Rows[0]["Other Properties"];
                             
                            if (OtherProperties == 1) // 1 = سيارةوملك مؤجر
                            {
                                P_Car_checkBox.Checked = P_Rented_checkBox.Checked = true; 
                            }
                            else if (OtherProperties == 2) // 2 = سيارة
                            {
                                P_Car_checkBox.Checked = true;
                                P_Rented_checkBox.Checked = false;
                            } 
                            else if (OtherProperties == 3) // 3 = ملك مؤجر
                            {
                                P_Car_checkBox.Checked = false;
                                P_Rented_checkBox.Checked = true;
                            }
                            else  // 0 -none of them checked
                            {
                                P_Car_checkBox.Checked = P_Rented_checkBox.Checked = false;
                            }
                        }

                        OtherProperties_textBox.Text = (string) MySS.dt.Rows[0]["Other Properties Note"];
                         
                        if (MySS.dt.Rows[0]["Other Income Sources"] == DBNull.Value)
                        {
                            P_SourceRation_checkBox.Checked = P_SourceRelatives_checkBox.Checked = false;
                        }
                        else
                        {
                            var otherIncome = (int) MySS.dt.Rows[0]["Other Income Sources"];

                            if (otherIncome == 1)
                            {
                                P_SourceRation_checkBox.Checked =
                                    P_SourceRelatives_checkBox.Checked = true; // 1 = أقارب و معونة
                            }
                            else if (otherIncome == 2)
                            {
                                P_SourceRelatives_checkBox.Checked = true;
                                P_SourceRation_checkBox.Checked = false;
                            } // 2 = أقارب
                            else if (otherIncome == 3)
                            {
                                P_SourceRelatives_checkBox.Checked = false;
                                P_SourceRation_checkBox.Checked = true;
                            } // 3 = معونة
                            else
                            {
                                P_SourceRation_checkBox.Checked = P_SourceRelatives_checkBox.Checked = false;
                            } // 0 -none of them checked
                        }

                        P_OtherIncomeSources_textBox.Text = MySS.dt.Rows[0]["Other Income Sources Note"].ToString();

                        OtherCourses_textBox.Text = MySS.dt.Rows[0]["Other Courses"].ToString();

                        P_Parish_comboBox.Text = MySS.dt.Rows[0]["Parish"] != DBNull.Value
                            ? (string) MySS.dt.Rows[0]["Parish"]
                            : "";

                        if (MySS.dt.Rows[0]["Priest_ID"] == DBNull.Value || MySS.dt.Rows[0]["Priest_ID"] == null)
                            P_Priest_comboBox.SelectedIndex = -1; 
                        else P_Priest_comboBox.SelectedValue = Convert.ToInt32(MySS.dt.Rows[0]["Priest_ID"]); 
                    }

                fullFtpPath = (string) MySS.dt.Rows[0]["Picture Path"];
                //person picture
                PersonPicArr = null;
                var reader = s.Person_Image(Person_ID);
                reader.Read();
                if (reader.HasRows)
                {
                    PersonPicArr = (byte[]) reader[0];
                    string Image_Path = reader[1].ToString();

                    if (PersonPicArr == null || PersonPicArr.Length == 0)
                    {
                        if(Image_Path != "")// if content = null => bring the image from online
                        {
                            PersonPicture_pictureBox.Image = null;
                            PersonPicture_pictureBox.Image = c.Download(Image_Path);
                        }
                        else
                            PersonPicture_pictureBox.Image = Resources.Unknown_User;
                    }
                    else
                    {
                        var ms = new MemoryStream(PersonPicArr);
                        PersonPicture_pictureBox.Image = Image.FromStream(ms);
                    }  
                }
                else
                {
                    MessageBox.Show("الصورة غير متوفرة !");
                }

                reader.Close();
                Program.MyConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Fill_PersonDetails()
        {
            try
            {
                // Education //
                var e_dt = new DataTable();
                e_dt = s.Person_Education_bind(Person_ID);
                var edu_Name = "";
                if (e_dt != null || e_dt.Rows.Count > 0)
                    for (var i = 0; i < e_dt.Rows.Count; i++)
                    {
                        Education_dataGridView.Rows.Add();

                        var ID = e_dt.Rows[i].Field<int>("ID");
                        Education_dataGridView.Rows[i].Cells["Edu_RowID"].Value = ID;

                        // get the data of 1 row
                        Education_dataGridView.Rows[i].Cells["E_Type"].Value = e_dt.Rows[i].Field<string>("Level");

                        edu_Name = e_dt.Rows[i].Field<string>("Name");
                        Education_dataGridView.Rows[i].Cells["E_Name"] = Load_Education_ComboBox("", edu_Name);
                        Education_dataGridView.Rows[i].Cells["E_Name"].Value = edu_Name;

                        //set E_ID and P_ID columns
                        Education_dataGridView.Rows[i].Cells["P_ID"].Value = Person_ID;

                        Program.buildConnection();
                        MySS.query = "select E_ID from `education` where E_Name like '%" + edu_Name + "%' limit 1";
                        MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                        var E_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());
                        Program.MyConn.Close();

                        Education_dataGridView.Rows[i].Cells["E_ID"].Value = E_ID;
                        Education_dataGridView.Rows[i].Cells["Edu_InDataBase"].Value = "1";
                    }

                if (!OldBeneficiary_NewProject)
                {
                    // Work //
                    var w_dt = new DataTable();
                    w_dt = s.Person_Work_bind(Person_ID, MicroProject_ID);
                    var Work_Name_string = "";
                    if (w_dt != null)
                        for (var i = 0; i < w_dt.Rows.Count; i++)
                        {
                            Work_dataGridView.Rows.Add();
                            // get the data of 1 row
                            Work_dataGridView.Rows[i].Cells["W_RowID"].Value = w_dt.Rows[i].Field<int>("ID").ToString();

                            Work_Name_string = w_dt.Rows[i].Field<string>("Name");
                            Work_dataGridView.Rows[i].Cells["W_Name"] = Load_Work_ComboBox(Work_Name_string);
                            Work_dataGridView.Rows[i].Cells["W_Name"].Value = w_dt.Rows[i].Field<string>("Name");

                            if (w_dt.Rows[i].Field<string>("Description") == string.Empty ||
                                w_dt.Rows[i].Field<string>("Description") == null)
                                Work_dataGridView.Rows[i].Cells["W_Description"].Value = "";
                            else
                                Work_dataGridView.Rows[i].Cells["W_Description"].Value =
                                    w_dt.Rows[i].Field<string>("Description");

                            if (w_dt.Rows[i].Field<string>("Begin Date") == string.Empty ||
                                w_dt.Rows[i].Field<string>("Begin Date") == null)
                                Work_dataGridView.Rows[i].Cells["W_BeginDate"].Value = "0000";
                            else
                                Work_dataGridView.Rows[i].Cells["W_BeginDate"].Value =
                                    w_dt.Rows[i].Field<string>("Begin Date");

                            if (w_dt.Rows[i].Field<string>("End Date") == string.Empty ||
                                w_dt.Rows[i].Field<string>("End Date") == null)
                                Work_dataGridView.Rows[i].Cells["W_EndDate"].Value = "0000";
                            else
                                Work_dataGridView.Rows[i].Cells["W_EndDate"].Value =
                                    w_dt.Rows[i].Field<string>("End Date");

                            if (w_dt.Rows[i].Field<string>("Place") == string.Empty ||
                                w_dt.Rows[i].Field<string>("Place") == null)
                                Work_dataGridView.Rows[i].Cells["W_Place"].Value = "";
                            else Work_dataGridView.Rows[i].Cells["W_Place"].Value = w_dt.Rows[i].Field<string>("Place");

                            if (w_dt.Rows[i].Field<string>("Place State") == string.Empty ||
                                w_dt.Rows[i].Field<string>("Place State") == null)
                                Work_dataGridView.Rows[i].Cells["W_PlaceState"].Value = "";
                            else
                                Work_dataGridView.Rows[i].Cells["W_PlaceState"].Value =
                                    w_dt.Rows[i].Field<string>("Place State");

                            if (w_dt.Rows[i].Field<string>("Description Reason Of Lose") == string.Empty ||
                                w_dt.Rows[i].Field<string>("Description Reason Of Lose") == null)
                                Work_dataGridView.Rows[i].Cells["W_CauseOfLose"].Value = "";
                            else
                                Work_dataGridView.Rows[i].Cells["W_CauseOfLose"].Value =
                                    w_dt.Rows[i].Field<string>("Description Reason Of Lose");

                            if (w_dt.Rows[i].Field<float>("Salary") == 0)
                                Work_dataGridView.Rows[i].Cells["W_Salary"].Value = 0;
                            else
                                Work_dataGridView.Rows[i].Cells["W_Salary"].Value =
                                    w_dt.Rows[i].Field<float>("Salary").ToString();

                            //set W_ID and P_ID columns
                            Program.buildConnection();
                            Work_dataGridView.Rows[i].Cells["WP_ID"].Value = Person_ID;
                            MySS.query = "select W_ID from `work` where W_Name like '%" + Work_Name_string +
                                         "%' limit 1";
                            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                            var W_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());
                            Program.MyConn.Close();

                            Work_dataGridView.Rows[i].Cells["W_ID"].Value = W_ID;
                            Work_dataGridView.Rows[i].Cells["W_InDataBase"].Value = "1";
                        }

                    // Experience //
                    w_dt = new DataTable();
                    w_dt = s.Person_Experience_bind(Person_ID, MicroProject_ID);
                    var Exp_Name_string = "";
                    if (w_dt != null)
                        for (var i = 0; i < w_dt.Rows.Count; i++)
                        {
                            Exp_dataGridView.Rows.Add();
                            // get the data of 1 row
                            Exp_dataGridView.Rows[i].Cells["Exp_RowID"].Value =
                                w_dt.Rows[i].Field<int>("ID").ToString();

                            Exp_Name_string = w_dt.Rows[i].Field<string>("Name");
                            Exp_dataGridView.Rows[i].Cells["Exp_Name"] = Load_Work_ComboBox(Exp_Name_string);
                            Exp_dataGridView.Rows[i].Cells["Exp_Name"].Value = w_dt.Rows[i].Field<string>("Name");

                            if (w_dt.Rows[i].Field<string>("Description") == string.Empty ||
                                w_dt.Rows[i].Field<string>("Description") == null)
                                Exp_dataGridView.Rows[i].Cells["Exp_Description"].Value = "";
                            else
                                Exp_dataGridView.Rows[i].Cells["Exp_Description"].Value =
                                    w_dt.Rows[i].Field<string>("Description");

                            if (w_dt.Rows[i].Field<string>("Begin Date") == string.Empty ||
                                w_dt.Rows[i].Field<string>("Begin Date") == null)
                                Exp_dataGridView.Rows[i].Cells["Exp_BeginDate"].Value = "0000";
                            else
                                Exp_dataGridView.Rows[i].Cells["Exp_BeginDate"].Value =
                                    w_dt.Rows[i].Field<string>("Begin Date");

                            if (w_dt.Rows[i].Field<string>("End Date") == string.Empty ||
                                w_dt.Rows[i].Field<string>("End Date") == null)
                                Exp_dataGridView.Rows[i].Cells["Exp_EndDate"].Value = "0000";
                            else
                                Exp_dataGridView.Rows[i].Cells["Exp_EndDate"].Value =
                                    w_dt.Rows[i].Field<string>("End Date");

                            if (w_dt.Rows[i].Field<string>("Place") == string.Empty ||
                                w_dt.Rows[i].Field<string>("Place") == null)
                                Exp_dataGridView.Rows[i].Cells["Exp_Place"].Value = "";
                            else
                                Exp_dataGridView.Rows[i].Cells["Exp_Place"].Value = w_dt.Rows[i].Field<string>("Place");

                            if (w_dt.Rows[i].Field<string>("Place State") == string.Empty ||
                                w_dt.Rows[i].Field<string>("Place State") == null)
                                Exp_dataGridView.Rows[i].Cells["Exp_PlaceState"].Value = "";
                            else
                                Exp_dataGridView.Rows[i].Cells["Exp_PlaceState"].Value =
                                    w_dt.Rows[i].Field<string>("Place State");

                            if (w_dt.Rows[i].Field<string>("Description Reason Of Lose") == string.Empty ||
                                w_dt.Rows[i].Field<string>("Description Reason Of Lose") == null)
                                Exp_dataGridView.Rows[i].Cells["Exp_CauseOfLose"].Value = "";
                            else
                                Exp_dataGridView.Rows[i].Cells["Exp_CauseOfLose"].Value =
                                    w_dt.Rows[i].Field<string>("Description Reason Of Lose");

                            if (w_dt.Rows[i].Field<float>("Salary") == 0)
                                Exp_dataGridView.Rows[i].Cells["Exp_Salary"].Value = 0;
                            else
                                Exp_dataGridView.Rows[i].Cells["Exp_Salary"].Value =
                                    w_dt.Rows[i].Field<float>("Salary").ToString();

                            //set W_ID and P_ID columns
                            Exp_dataGridView.Rows[i].Cells["ExpP_ID"].Value = Person_ID;

                            Program.buildConnection();
                            MySS.query = "select W_ID from `work` where W_Name like '%" + Work_Name_string +
                                         "%' limit 1";
                            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                            var Exp_ID = Convert.ToInt32(MySS.sc.ExecuteScalar());
                            Program.MyConn.Close();

                            Exp_dataGridView.Rows[i].Cells["Exp_ID"].Value = Exp_ID;
                            Exp_dataGridView.Rows[i].Cells["Exp_InDataBase"].Value = "1";
                        }
                }
                else
                {
                    Work_dataGridView.Rows.Clear();
                    Exp_dataGridView.Rows.Clear();
                }

                // Courses //
                var dt1 = new DataTable();
                dt1 = s.Person_Courses_bind(Person_ID);
                if (dt1 != null || dt1.Rows.Count > 0)
                    for (var i = 0; i < dt1.Rows.Count; i++)
                        foreach (Control c in Course_tableLayoutPanel.Controls)
                            if (c.GetType() == typeof(CheckBox))
                            {
                                var CBox = c as CheckBox;
                                if (CBox.Text == dt1.Rows[i].Field<string>("Name")) CBox.Checked = true;
                            }

                // Loss //
                var dt = new DataTable();
                dt = s.Person_Loss_bind(Person_ID);
                if (dt != null || dt.Rows.Count > 0)
                    for (var i = 0; i < dt.Rows.Count; i++)
                        foreach (Control c in Loss_tableLayoutPanel.Controls)
                            if (c.GetType() == typeof(CheckBox))
                            {
                                var CBox = c as CheckBox;
                                if (CBox.Text == dt.Rows[i].Field<string>("Name")) CBox.Checked = true;
                            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Fill_FamilyMembers(int x)
        {
            try
            {
                // FAMILY MEMBERS //
                MySS.dt = new DataTable();
                MySS.dt = s.FamilyMembers_bind(-1, Person_ID);
                var Relation = "";
                FamilyNum_textBox1.Text = "";
                Husband_Wife_ID = -1; 
                Family_ID = -1;
                NoFamily = true;

                var j = 0;
                if (MySS.dt != null && MySS.dt.Rows.Count > 0)
                {
                    //get family book number and family id from first row (0) //
                    if (MySS.dt.Rows[0].Field<string>("Family Number") == null)
                    { 
                        MessageBox.Show("انتبه ! هذا المستفيد لا يملك رقم دفتر العائلة");
                    }
                    else
                    {
                        FamilyNum_textBox1.Text = MySS.dt.Rows[0].Field<string>("Family Number");
                        NoFamily = false;
                        Family_ID = MySS.dt.Rows[0].Field<int>("Family_ID");
                    }

                    //fill family members //
                    for (var i = 0; i < MySS.dt.Rows.Count; i++)
                    {
                        Relation = MySS.dt.Rows[i].Field<string>("Relation");
                        if (Relation.Contains("مستفيد"))
                        {
                            if (x == 0) continue;
                            goto Finish;
                        }

                        if (Relation.Equals("زوج/ة")) //add to husband/wife textboxs   
                        {
                            Husband_Wife_ID = MySS.dt.Rows[i].Field<int>("ID");

                            FPersonFName_textBox1.Text = MySS.dt.Rows[i].Field<string>("FirstName");
                            FPersonDOB_textBox1.Text = MySS.dt.Rows[i].Field<DateTime>("Birth Date").Year.ToString();

                            if (MySS.dt.Rows[i].Field<string>("LastName") == null)
                                FPersonLName_textBox1.Text = "";
                            else FPersonLName_textBox1.Text = MySS.dt.Rows[i].Field<string>("LastName");

                            if (MySS.dt.Rows[i].Field<string>("FatherName") == null)
                                FPersonFatherName_textBox1.Text = "";
                            else FPersonFatherName_textBox1.Text = MySS.dt.Rows[i].Field<string>("FatherName");

                            if (MySS.dt.Rows[i].Field<string>("MotherName") == null)
                                FPersonMotherName_textBox1.Text = "";
                            else FPersonMotherName_textBox1.Text = MySS.dt.Rows[i].Field<string>("MotherName");

                            if (MySS.dt.Rows[i].Field<string>("National Number") == null)
                                FPersonNationalNum_textBox1.Text = "";
                            else FPersonNationalNum_textBox1.Text = MySS.dt.Rows[i].Field<string>("National Number");

                            FPersonSex_comboBox1.Text = MySS.dt.Rows[i].Field<string>("Gender");
                            FPersonState_comboBox1.Text = MySS.dt.Rows[i].Field<string>("Marital Status");
                            continue;
                        }

                        Finish:
                        {
                            //add to datagridview//
                            FamilyMember_dataGridView.Rows.Add();

                            FamilyMember_dataGridView.Rows[j].Cells["FP_ID"].Value =
                                MySS.dt.Rows[i].Field<int>("ID").ToString();
                            FamilyMember_dataGridView.Rows[j].Cells["F_ID"].Value = Family_ID;

                            FamilyMember_dataGridView.Rows[j].Cells["P_FirstName"].Value =
                                MySS.dt.Rows[i].Field<string>("FirstName");

                            if (MySS.dt.Rows[i].Field<string>("LastName") == null ||
                                MySS.dt.Rows[i].Field<string>("LastName") == null)
                                FamilyMember_dataGridView.Rows[j].Cells["P_LastName"].Value = "";
                            else
                                FamilyMember_dataGridView.Rows[j].Cells["P_LastName"].Value =
                                    MySS.dt.Rows[i].Field<string>("LastName");

                            FamilyMember_dataGridView.Rows[j].Cells["P_DOB"].Value =
                                MySS.dt.Rows[i].Field<DateTime>("Birth Date").Year;
                            FamilyMember_dataGridView.Rows[j].Cells["Relation"].Value = Relation;

                            if (MySS.dt.Rows[i].Field<string>("Work") == null ||
                                MySS.dt.Rows[i].Field<string>("Work") == null)
                                FamilyMember_dataGridView.Rows[j].Cells["Work_Name"].Value = "";
                            else
                                FamilyMember_dataGridView.Rows[j].Cells["Work_Name"].Value =
                                    MySS.dt.Rows[i].Field<string>("Work");

                            FamilyMember_dataGridView.Rows[j].Cells["P_InDataBase"].Value = "1";
                            j++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Fill_Project()
        {
            try
            {
                MySS.dt = new DataTable();
                MySS.dt = s.MicroProject_bind(Person_ID, MicroProject_ID);
                if (MySS.dt != null)
                    if (MySS.dt.Rows.Count > 0)
                    {
                        MicroProject_ID = int.Parse(MySS.dt.Rows[0]["MicroProject_ID"].ToString());
                        MPID_textBox.Text = MicroProject_ID.ToString();

                        MP_Name_textBox.Text = (string) MySS.dt.Rows[0]["Project Name"];
                        var price = Convert.ToDouble(MySS.dt.Rows[0]["Requested Amount"].ToString());
                        MP_RequestedAmount_textBox.Text = price.ToString();
                        MP_RequestedTime_textBox.Text = (string) MySS.dt.Rows[0]["Requested Time"];

                        if (MySS.dt.Rows[0]["Application Date"] == DBNull.Value || MySS.dt.Rows[0]["Application Date"].ToString().Contains("0001"))
                            ApplyDate_bcDateTimePicker.Value = DateTime.Now;
                        else
                        {
                            var date = (DateTime)MySS.dt.Rows[0]["Application Date"];
                            ApplyDate_bcDateTimePicker.Value = date;
                        }

                        MP_Description_textBox.Text = (string) MySS.dt.Rows[0]["Description"];
                        var temp = "";

                        //temp = (string) MySS.dt.Rows[0]["Project Kind"];
                        //if (temp == "New") New_radioButton.Checked = true;
                        //else if (temp == "Expand") Expand_radioButton.Checked = true;
                        //else New_radioButton.Checked = Expand_radioButton.Checked = false;

                        //category
                        //if (MySS.dt.Rows[0]["Category"].ToString() != null)
                        //{
                        //    Category_comboBox.Text = MySS.dt.Rows[0]["Category"].ToString();
                        //    var result = MySS.dt.Rows[0]["Category_ID"];

                        //    result = result == DBNull.Value ? null : result;
                        //    var countDis = Convert.ToInt32(result);

                        //    var Selected = -1;
                        //    var count = Category_comboBox.Items.Count;
                        //    for (var i = 0; i <= count - 1; i++)
                        //    {
                        //        Category_comboBox.SelectedIndex = i;
                        //        if ((int)Category_comboBox.SelectedValue == countDis) Selected = i;
                        //    }

                        //    Category_comboBox.SelectedIndex = Selected;
                        //    Category_comboBox.SelectedValue = countDis;
                        //}
                        //else
                        //{
                        //    Category_comboBox.SelectedIndex = -1;
                        //}
                        //sub category
                        //if (MySS.dt.Rows[0]["SubCategory"].ToString() != null)
                        //{
                        //    SubCategory_comboBox.Text = MySS.dt.Rows[0]["SubCategory"].ToString();
                        //    var result = MySS.dt.Rows[0]["SubCategory_ID"];

                        //    result = result == DBNull.Value ? null : result;
                        //    var countDis = Convert.ToInt32(result);

                        //    var Selected = -1;
                        //    var count = SubCategory_comboBox.Items.Count;
                        //    for (var i = 0; i <= count - 1; i++)
                        //    {
                        //        SubCategory_comboBox.SelectedIndex = i;
                        //        if ((int)SubCategory_comboBox.SelectedValue == countDis) Selected = i;
                        //    }

                        //    SubCategory_comboBox.SelectedIndex = Selected;
                        //    SubCategory_comboBox.SelectedValue = countDis;
                        //}
                        //else
                        //{
                        //    SubCategory_comboBox.SelectedIndex = -1;
                        //}

                        //fund type
                        if (MySS.dt.Rows[0]["FundType_ID"] == DBNull.Value || MySS.dt.Rows[0]["FundType_ID"] == null)
                            Loan_radioButton.Checked = Grant_radioButton.Checked = false;
                        else {
                            if ((int)MySS.dt.Rows[0]["FundType_ID"] == 1) Loan_radioButton.Checked = true;
                            else Grant_radioButton.Checked = true;
                        }


                        //project type
                        if (MySS.dt.Rows[0]["Type_ID"] == DBNull.Value || MySS.dt.Rows[0]["Type_ID"] == null)
                            Type_comboBox.SelectedIndex = -1;
                        else
                        {
                            //to trigger the event type_selectedValueChanged//
                            user_mode = true;
                            Type_comboBox.SelectedValue = Convert.ToInt32(MySS.dt.Rows[0]["Type_ID"]);
                            user_mode = false;
                        }
                        //project sub type
                        if (MySS.dt.Rows[0]["SubType_ID"] == DBNull.Value || MySS.dt.Rows[0]["SubType_ID"] == null)
                            SubType_comboBox.SelectedIndex = -1;
                        else SubType_comboBox.SelectedValue = Convert.ToInt32(MySS.dt.Rows[0]["SubType_ID"]);
                         
                        //category
                        if (MySS.dt.Rows[0]["Category_ID"] == DBNull.Value || MySS.dt.Rows[0]["Category_ID"]== null)
                            Category_comboBox.SelectedIndex = -1;
                        else Category_comboBox.SelectedValue = Convert.ToInt32(MySS.dt.Rows[0]["Category_ID"]);
                          
                        //sub category
                        if (MySS.dt.Rows[0]["SubCategory_ID"] == DBNull.Value || MySS.dt.Rows[0]["SubCategory_ID"] == null)
                            SubCategory_comboBox.SelectedIndex = -1;
                        else SubCategory_comboBox.SelectedValue = Convert.ToInt32(MySS.dt.Rows[0]["SubCategory_ID"]);
                         
                        temp = (string) MySS.dt.Rows[0]["Has Previous Donors"];
                        if (temp == "Yes") HasPreviousDonors_radioButton.Checked = true;
                        else if (temp == "No") HasNotPreviousDonors_radioButton.Checked = true;
                        else HasPreviousDonors_radioButton.Checked = HasNotPreviousDonors_radioButton.Checked = false;

                        MP_OtherDonors_textBox.Text = (string) MySS.dt.Rows[0]["Other Donors"];

                        temp = (string) MySS.dt.Rows[0]["needs a license"];
                        if (temp == "Yes") MP_NeedLicense_radioButton.Checked = true;
                        else MP_DontNeedLicense_radioButton.Checked = true;
                        MP_LicenseSide_textBox.Text = (string) MySS.dt.Rows[0]["License Side"];

                        temp = (string) MySS.dt.Rows[0]["Has Competitors"];
                        if (temp == "Yes") HasCompetitors_radioButton.Checked = true;
                        else if (temp == "No") HasNotCompetitors_radioButton.Checked = true;
                        else HasCompetitors_radioButton.Checked = HasNotCompetitors_radioButton.Checked = false;

                        MP_Competitors_textBox.Text = MySS.dt.Rows[0]["Competitors"].ToString();

                        temp = (string) MySS.dt.Rows[0]["Supplies Insurance"];
                        if (temp == "Easy") EasySupplies_radioButton.Checked = true;
                        else if (temp == "Hard") HardSupplies_radioButton.Checked = true;
                        else EasySupplies_radioButton.Checked = HardSupplies_radioButton.Checked = false;

                        MP_Suppliers_textBox.Text = MySS.dt.Rows[0]["Suppliers"].ToString();
                        MP_Risk_textBox.Text = MySS.dt.Rows[0]["Risk"].ToString();
                        MP_Protection_textBox.Text = MySS.dt.Rows[0]["Protection"].ToString();

                        MP_OtherNotes_textBox.Text = (string) MySS.dt.Rows[0]["Other Notes"];
                        MP_Marketing_textBox.Text = (string) MySS.dt.Rows[0]["Marketing"];
                        var num = (int) MySS.dt.Rows[0]["Simple Profit"];
                        MP_SimpleProfit_textBox.Text = num.ToString();
                        MP_SimpleProfit2_textBox.Text = (string) MySS.dt.Rows[0]["Profit Comment"];

                        temp = (string) MySS.dt.Rows[0]["Income Kind"];
                        if (temp == "Single") SingleIncome_radioButton.Checked = true;
                        else if (temp == "Additional") AdditionalIncome_radioButton.Checked = true;
                        else SingleIncome_radioButton.Checked = AdditionalIncome_radioButton.Checked = false;

                        IncomeDesc_textBox.Text = (string) MySS.dt.Rows[0]["Income Kind Note"];

                        temp = (string) MySS.dt.Rows[0]["Has Ledger"];
                        if (temp == "Yes") HasLedger_radioButton.Checked = true;
                        else if (temp == "No") HasNotLedger_radioButton.Checked = true;
                        else HasLedger_radioButton.Checked = HasNotLedger_radioButton.Checked = false;

                        var visited = (int) MySS.dt.Rows[0]["Visited"];
                        if (visited == 0) Visit_checkBox.Checked = false;
                        else Visit_checkBox.Checked = true;

                        var IsContentUpdated = (int)MySS.dt.Rows[0]["IsContentUpdated"];
                        if (IsContentUpdated == 0) ContentUpdated_checkBox.Checked = false;
                        else ContentUpdated_checkBox.Checked = true;

                        var message = (int) MySS.dt.Rows[0]["Message"];
                        if (message == 0)
                        {
                            Reject_Message_checkBox.Checked = false;
                            Reject_Message_checkBox.Text = "";
                        }
                        else
                        {
                            Reject_Message_checkBox.Checked = true;
                            if (MySS.dt.Rows[0]["Message Date"] != DBNull.Value)
                            {
                                var date = (DateTime) MySS.dt.Rows[0]["Message Date"];
                                Reject_Message_checkBox.Text = date.ToString();
                            }
                        } 

                        //user// 
                        if (MySS.dt.Rows[0]["PaperUser_ID"] == DBNull.Value || MySS.dt.Rows[0]["PaperUser_ID"] == null)
                            PaperApplicationUser_comboBox.SelectedIndex = -1;
                        else PaperApplicationUser_comboBox.SelectedValue = Convert.ToInt32(MySS.dt.Rows[0]["PaperUser_ID"]);
                         
                        if (MySS.dt.Rows[0]["ProgramUser"] != DBNull.Value)
                            CreatedBy_User_label.Text = (string)MySS.dt.Rows[0]["ProgramUser"];
                        else CreatedBy_User_label.Text = "";

                        if (MySS.dt.Rows[0]["EditedByUser"] != DBNull.Value)
                            EditedBy_User_label.Text = (string) MySS.dt.Rows[0]["EditedByUser"];
                        else EditedBy_User_label.Text = ""; 

                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Fill_ProjectDetails()
        {
            try
            {
                ////// fill material table  /////////
                MySS.dt = s.Materials_bind(MicroProject_ID);
                if (MySS.dt != null)
                    if (MySS.dt.Rows.Count > 0)
                        for (var i = 0; i < MySS.dt.Rows.Count; i++)
                        {
                            Materials_dataGridView.Rows.Add();

                            Materials_dataGridView.Rows[i].Cells["MMP_ID"].Value =
                                MySS.dt.Rows[i].Field<int>("MicroProject_ID").ToString();
                            Materials_dataGridView.Rows[i].Cells["M_ID"].Value =
                                MySS.dt.Rows[i].Field<int>("ID").ToString();
                            Materials_dataGridView.Rows[i].Cells["M_Name"].Value =
                                MySS.dt.Rows[i].Field<string>("Name");

                            if (MySS.dt.Rows[i].Field<int>("Amount") == null ||
                                MySS.dt.Rows[i].Field<int>("Amount").ToString() == null)
                                Materials_dataGridView.Rows[i].Cells["M_Amount"].Value = 0;
                            else
                                Materials_dataGridView.Rows[i].Cells["M_Amount"].Value =
                                    MySS.dt.Rows[i].Field<int>("Amount").ToString();

                            if (MySS.dt.Rows[i].Field<double>("Price") == null ||
                                MySS.dt.Rows[i].Field<double>("Price").ToString() == null)
                                Materials_dataGridView.Rows[i].Cells["M_Price"].Value = 0;
                            else
                                Materials_dataGridView.Rows[i].Cells["M_Price"].Value =
                                    MySS.dt.Rows[i].Field<double>("Price").ToString();

                            if (MySS.dt.Rows[i].Field<double>("LocalContribution") == null ||
                                MySS.dt.Rows[i].Field<double>("LocalContribution").ToString() == null)
                                Materials_dataGridView.Rows[i].Cells["M_LocalContribution"].Value = "";
                            else
                                Materials_dataGridView.Rows[i].Cells["M_LocalContribution"].Value =
                                    MySS.dt.Rows[i].Field<double>("LocalContribution").ToString();

                            if (MySS.dt.Rows[i].Field<string>("Comments") == null ||
                                MySS.dt.Rows[i].Field<string>("Comments") == null)
                                Materials_dataGridView.Rows[i].Cells["M_Comments"].Value = "";
                            else
                                Materials_dataGridView.Rows[i].Cells["M_Comments"].Value =
                                    MySS.dt.Rows[i].Field<string>("Comments");

                            Materials_dataGridView.Rows[i].Cells["M_InDataBase"].Value = "1";
                            //calculate sum of items //
                            ///////////////////////////
                            calculate_SumOfBudgetItems(i);
                        }

                ////// fill risks /////////
                MySS.dt = s.Risks_bind(MicroProject_ID);
                if (MySS.dt != null)
                    if (MySS.dt.Rows.Count > 0)
                        for (var i = 0; i < MySS.dt.Rows.Count; i++)
                        {
                            var risk = MySS.dt.Rows[i].Field<string>("Name");
                            foreach (Control c in Risks_tableLayoutPanel.Controls)
                                if (c.GetType() == typeof(CheckBox))
                                {
                                    var CBox = c as CheckBox;
                                    if (CBox.Text == risk) CBox.Checked = true;
                                }
                        }

                ////// fill score /////////
                MySS.dt = s.Score_bind(MicroProject_ID);
                if (MySS.dt != null)
                    if (MySS.dt.Rows.Count > 0)
                        for (var i = 0; i < MySS.dt.Rows.Count; i++)
                        {
                            var Score_ID = MySS.dt.Rows[i].Field<int>("Score_ID");
                            var Value = MySS.dt.Rows[i].Field<int>("value");

                            switch (Score_ID)
                            {
                                case 1:
                                {
                                    if (Value == 1) s1_radioButton.Checked = true;
                                    else if (Value == 0) n1_radioButton.Checked = true;
                                    else s1_radioButton.Checked = n1_radioButton.Checked = false;
                                    s1_textBox.Text = MySS.dt.Rows[i].Field<string>("notes");
                                }
                                    break;

                                case 2:
                                {
                                    if (Value == 1) s2_radioButton.Checked = true;
                                    else if (Value == 0) n2_radioButton.Checked = true;
                                    else s2_radioButton.Checked = n2_radioButton.Checked = false;
                                    s2_textBox.Text = MySS.dt.Rows[i].Field<string>("notes");
                                }
                                    break;

                                case 3:
                                {
                                    if (Value == 1) s3_radioButton.Checked = true;
                                    else if (Value == 0) n3_radioButton.Checked = true;
                                    else s3_radioButton.Checked = n3_radioButton.Checked = false;
                                    s3_textBox.Text = MySS.dt.Rows[i].Field<string>("notes");
                                }
                                    break;

                                case 4:
                                {
                                    if (Value == 1) s4_radioButton.Checked = true;
                                    else if (Value == 0) n4_radioButton.Checked = true;
                                    else s4_radioButton.Checked = n4_radioButton.Checked = false;
                                    s4_textBox.Text = MySS.dt.Rows[i].Field<string>("notes");
                                }
                                    break;

                                case 5:
                                {
                                    if (Value == 1) s5_radioButton.Checked = true;
                                    else if (Value == 0) n5_radioButton.Checked = true;
                                    else s5_radioButton.Checked = n5_radioButton.Checked = false;
                                    s5_textBox.Text = MySS.dt.Rows[i].Field<string>("notes");
                                }
                                    break;

                                case 6:
                                {
                                    if (Value == 1) s6_radioButton.Checked = true;
                                    else if (Value == 0) n6_radioButton.Checked = true;
                                    else s6_radioButton.Checked = n6_radioButton.Checked = false;
                                    s6_textBox.Text = MySS.dt.Rows[i].Field<string>("notes");
                                }
                                    break;

                                case 7:
                                {
                                    if (Value == 1) s7_radioButton.Checked = true;
                                    else if (Value == 0) n7_radioButton.Checked = true;
                                    else s7_radioButton.Checked = n7_radioButton.Checked = false;
                                    s7_textBox.Text = MySS.dt.Rows[i].Field<string>("notes");
                                }
                                    break;

                                case 8:
                                {
                                    if (Value == 1) s8_radioButton.Checked = true;
                                    else if (Value == 0) n8_radioButton.Checked = true;
                                    else s8_radioButton.Checked = n8_radioButton.Checked = false;
                                    s8_textBox.Text = MySS.dt.Rows[i].Field<string>("notes");
                                }
                                    break;

                                case 9:
                                {
                                    if (Value == 1) s9_radioButton.Checked = true;
                                    else if (Value == 0) n9_radioButton.Checked = true;
                                    else s9_radioButton.Checked = n9_radioButton.Checked = false;
                                    s9_textBox.Text = MySS.dt.Rows[i].Field<string>("notes");
                                }
                                    break;

                                case 10:
                                {
                                    if (Value == 1) s0_radioButton.Checked = true;
                                    else if (Value == 0) n0_radioButton.Checked = true;
                                    else s0_radioButton.Checked = n0_radioButton.Checked = false;
                                    s0_textBox.Text = MySS.dt.Rows[i].Field<string>("notes");
                                }
                                    break;
                            }
                        }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear_Person_boxes()
        {
            user_mode = false;
            PersonType_comboBox.SelectedIndex = 0;
            user_mode = true;

            P_FirstName_textBox.Text = P_LastName_textBox.Text = P_FatherName_textBox.Text = P_MotherName_textBox.Text
                = FPersonSex_comboBox1.Text = FPersonState_comboBox1.Text = P_NationalNum_textBox.Text = P_DOB_textBox.Text
                = Military_textBox.Text = P_IntermidiaryName_textBox.Text = P_Parish_comboBox.Text
                = P_Mobile_textBox.Text = P_HomeTel_textBox.Text = P_Registration_textBox.Text = P_Address_textBox.Text
                = LiveWithFamilyDesc_textBox.Text = HomeDesc_textBox.Text = OtherProperties_textBox.Text
                = P_NumAtHome_textBox.Text = FamilyNum_textBox1.Text = OtherCourses_textBox.Text
                = P_SourceOfIncome_textBox.Text = P_OtherIncomeSources_textBox.Text = P_MedicalHelp_textBox.Text 
                = P_MedicalCond_textBox.Text = P_Loss_textBox.Text = "";

            MilitaryDelayed_radioButton.Checked = MilitaryFinished_radioButton.Checked 
                = MilitaryNow_radioButton.Checked = MilitarySingle_radioButton.Checked
                = P_LiveWithFamily_radioButton.Checked = P_NotLiveWithFamily_radioButton.Checked
                = P_HomeOwnership_radioButton.Checked = P_HomeRent_radioButton.Checked 
                = P_Car_checkBox.Checked = P_Rented_checkBox.Checked
                = IsProvider_radioButton.Checked = IsNotProvider_radioButton.Checked
                = P_SourceRation_checkBox.Checked = P_SourceRelatives_checkBox.Checked 
                = MedicalHelp_radioButton.Checked = NoMedicalHelp_radioButton.Checked = false;

            //Family
            FPersonFName_textBox1.Text = FPersonFatherName_textBox1.Text = FPersonLName_textBox1.Text 
                = FPersonMotherName_textBox1.Text = FPersonNationalNum_textBox1.Text = FPersonSex_comboBox1.Text 
                = FPersonState_comboBox1.Text = FPersonDOB_textBox1.Text = "";
            Family_ID = -1;
            Husband_Wife_ID = -1;
            NoFamily = true;

            //clear datagrids
            FamilyMember_dataGridView.Rows.Clear();
            Education_dataGridView.Rows.Clear();
            Work_dataGridView.Rows.Clear();
            Exp_dataGridView.Rows.Clear();

            foreach (Control c in Course_tableLayoutPanel.Controls)
                if (c.GetType() == typeof(CheckBox))
                {
                    var CBox = c as CheckBox;
                    CBox.Checked = false;
                }

            foreach (Control c in Loss_tableLayoutPanel.Controls)
                if (c.GetType() == typeof(CheckBox))
                {
                    var CBox = c as CheckBox;
                    CBox.Checked = false;
                }

            P_Priest_comboBox.SelectedIndex = -1;
            P_ID_textBox.Text = "";
            Person_ID = -1;
            OldBeneficiary_NewProject = false;

            // image //
            PersonPicture_pictureBox.Image = Resources.Unknown_User;
            PersonPicArr = null;
            ImageLocation_textBox.Text = "";
            ftpPath = fullFtpPath = "";

            Main_panel.AutoScrollPosition = new Point(0, 0);
        }

        private void clear_Project_boxes()
        {
            MicroProject_ID = -1;
            Save_MP_ID_button.Visible = true;
            Save_MP_ID_button.BackgroundImage = Properties.Resources.Unchecked;
            toolTip1.SetToolTip(Save_MP_ID_button, "لم يتم حجز الرقم بعد");

            EditedBy_User_label.Text = CreatedBy_User_label.Text = "";

            Beneficiary_Count = 0;
            type = 0;
            Visit_checkBox.Checked = Reject_Message_checkBox.Checked = false;
            Category_comboBox.SelectedIndex = SubCategory_comboBox.SelectedIndex = -1;
            Materials_dataGridView.Rows.Clear();
            OverallSyrian_label.Text = Sum_label.Text = "";

            // first part //
            MP_Name_textBox.Text = MP_RequestedAmount_textBox.Text = MP_RequestedTime_textBox.Text = "";


            List<TableLayoutPanel> tables_list = new List<TableLayoutPanel>();
            tables_list.Add( second_tableLayoutPanel );
            tables_list.Add( Projects2_tableLayoutPanel );
            tables_list.Add( Projects3_tableLayoutPanel );

            for (int i = 0; i < tables_list.Count; i++)
            {
                foreach (Control c in tables_list.ElementAt<TableLayoutPanel>(i).Controls)
                {
                    if (c.GetType() == typeof(TableLayoutPanel))
                        foreach (Control bb in c.Controls)
                        {
                            if (bb.GetType() == typeof(TableLayoutPanel))
                                foreach (Control aaa in bb.Controls)
                                {
                                    if (aaa.GetType() == typeof(TextBox)) aaa.Text = "";
                                    if (aaa.GetType() == typeof(RadioButton))
                                    {
                                        var CBox = aaa as RadioButton;
                                        CBox.Checked = false;
                                    }

                                    if (aaa.GetType() == typeof(CheckBox))
                                    {
                                        var CBox = aaa as CheckBox;
                                        CBox.Checked = false;
                                    }
                                }

                            if (bb.GetType() == typeof(TextBox)) bb.Text = "";

                            if (bb.GetType() == typeof(RadioButton))
                            {
                                var CBox = bb as RadioButton;
                                CBox.Checked = false;
                            }

                            if (bb.GetType() == typeof(CheckBox))
                            {
                                var CBox = bb as CheckBox;
                                CBox.Checked = false;
                            }
                        }

                    if (c.GetType() == typeof(TextBox)) c.Text = "";

                    if (c.GetType() == typeof(RadioButton))
                    {
                        var CBox = c as RadioButton;
                        CBox.Checked = false;
                    }

                    if (c.GetType() == typeof(CheckBox))
                    {
                        var CBox = c as CheckBox;
                        CBox.Checked = false;
                    }
                }
            }

            Get_Last_MicroProjectID();
            ApplyDate_bcDateTimePicker.Value = DateTime.Now;

            Partners_lable.Text = "فردي";
        }

        #endregion

        #region education Work Exp material family

        private DataGridViewComboBoxCell Load_Education_ComboBox(string Edu_type, string Name)
        {
            //check connection//
            Program.buildConnection();
            var str = " select `E_ID`,`E_Name` from `education`";

            var condition = "";
            if (Edu_type != "")
                condition += " where `E_Type` like '%" + Edu_type + "%'";
            else if (Name != "")
                condition += " where `E_Name` like '%" + Name + "%'";
            else condition = "";
            str += condition;

            var orderby = " order by `E_Name`";
            str += orderby;
            MySS.sc = new MySqlCommand(str, Program.MyConn);
            MySS.da = new MySqlDataAdapter();
            MySS.ds = new DataSet();
            var dt = new DataTable();
            MySS.da.SelectCommand = MySS.sc;
            MySS.da.Fill(MySS.ds);
            dt = MySS.ds.Tables[0];

            var cell = new DataGridViewComboBoxCell();
            var row = new ArrayList();
            //add items to array list from datatable
            foreach (DataRow dr in dt.Rows) row.Add(dr[1].ToString());
            cell.Items.AddRange(row.ToArray());
            return cell;
        }

        private DataGridViewComboBoxCell Load_Work_ComboBox(string name)
        {
            //check connection//
            Program.buildConnection();
            var strCmd = "select W_ID,W_Name from `work`";
            var condition = "";
            if (name != "")
                condition += " where W_Name like '" + name + "'";
            strCmd += condition;
            var orderby = " order by W_Name";
            strCmd += orderby;

            MySS.sc = new MySqlCommand(strCmd, Program.MyConn);
            MySS.da = new MySqlDataAdapter();
            MySS.ds = new DataSet();
            var dt = new DataTable();

            MySS.da.SelectCommand = MySS.sc;
            MySS.da.Fill(MySS.ds);
            dt = MySS.ds.Tables[0];

            var cell = new DataGridViewComboBoxCell();
            var row = new ArrayList();
            //add items to array list from datatable
            foreach (DataRow dr in dt.Rows) row.Add(dr[1].ToString());
            cell.Items.AddRange(row.ToArray());
            cell.MaxDropDownItems = 8;
            cell.AutoComplete = true;
            return cell;
        }

        //////////////////////////////////////////////////////////////////////////
        private void Education_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4) //type updated and enter to name
                {
                    var Edu_Type = "";
                    if (Education_dataGridView.Rows[e.RowIndex].Cells["E_Type"].Value == null ||
                        Education_dataGridView.Rows[e.RowIndex].Cells["E_Type"].Value.ToString() == "")
                        throw new Exception("الرجاء قم باختيار درجة التحصيل العلمي أولا ثم حاول إدخال مجال الدراسة");
                    Edu_Type = Education_dataGridView.Rows[e.RowIndex].Cells["E_Type"].Value.ToString();

                    //if (Edu_Type == "دبلوم" || Edu_Type == "ماجستير" || Edu_Type == "دكتوراه")
                    //{
                    //    Edu_Type = "إجازة جامعية";
                    //}
                    Education_dataGridView.Rows[e.RowIndex].Cells[4] = Load_Education_ComboBox(Edu_Type, "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Education_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == Education_dataGridView.NewRowIndex || e.RowIndex < 0)
                return;

            //Check if click is on specific column 
            if (e.ColumnIndex == Education_dataGridView.Columns["DeleteRow"].Index)
            {
                if (Education_dataGridView["Edu_InDataBase", e.RowIndex].Value == null)
                {
                    Education_dataGridView.Rows.RemoveAt(e.RowIndex);
                }
                else
                {
                    var dialogResult = MessageBox.Show("هل أنت متأكد أنك تريد حذف التحصيل العلمي للمستفيد؟", "Delete",
                        MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var P_ID = Convert.ToInt32(Education_dataGridView.Rows[e.RowIndex].Cells["P_ID"].Value
                            .ToString());
                        var E_ID = Convert.ToInt32(Education_dataGridView.Rows[e.RowIndex].Cells["E_ID"].Value
                            .ToString());
                        var ID = Convert.ToInt32(Education_dataGridView.Rows[e.RowIndex].Cells["Edu_RowID"].Value
                            .ToString());

                        //check connection//
                        Program.buildConnection();
                        var query = "delete from `person_education` where PEdu_ID = " + ID;
                        var sc = new MySqlCommand(query, Program.MyConn);
                        sc.ExecuteNonQuery();
                        Program.MyConn.Close();

                        l.Insert_Log("Delete education: " + Education_dataGridView.Rows[e.RowIndex].Cells["E_Name"].Value +
                            " from project: " + MicroProject_ID,
                            "Education", Settings.Default.username, DateTime.Now);

                        Education_dataGridView.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }

        private void Education_dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == Education_dataGridView.NewRowIndex || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == Education_dataGridView.Columns["DeleteRow"].Index)
            {
                Image image = null;
                if (Settings.Default.theme == "Light")
                    image = Resources.KAKA_Alii;
                else image = Resources.KAKA_Alii_D;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var size = image.Size;
                var location = new Point((e.CellBounds.Width - size.Width) / 2,
                    (e.CellBounds.Height - size.Height) / 2);
                location.Offset(e.CellBounds.Location);
                e.Graphics.DrawImage(image, location);
                e.Handled = true;
            }
        }

        private void Education_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4) //type updated and enter to name
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //////////////////////////////////////////////////////////////////////////
        private void Work_dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2) //enter to name
                    Work_dataGridView.Rows[e.RowIndex].Cells[2] = Load_Work_ComboBox("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Work_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if click is on new row or header row
                if (e.RowIndex == Work_dataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                //Check if click is on specific column 
                if (e.ColumnIndex == Work_dataGridView.Columns["W_DeleteRow"].Index)
                {
                    if (Work_dataGridView["W_InDataBase", e.RowIndex].Value == null)
                    {
                        Work_dataGridView.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        var dialogResult = MessageBox.Show("هل أنت متأكد أنك تريد حذف مجال العمل للمستفيد؟", "Delete",
                            MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            var P_ID = Convert.ToInt32(Work_dataGridView.Rows[e.RowIndex].Cells["WP_ID"].Value
                                .ToString());
                            var W_ID = Convert.ToInt32(
                                Work_dataGridView.Rows[e.RowIndex].Cells["W_ID"].Value.ToString());
                            var PW_ID = Convert.ToInt32(Work_dataGridView.Rows[e.RowIndex].Cells["W_RowID"].Value
                                .ToString());
                            //check connection//
                            Program.buildConnection();
                            var query = "delete from `person_workexp` where W_Status like 'Yes' and PW_ID = " + PW_ID +
                                        "";

                            var sc = new MySqlCommand(query, Program.MyConn);
                            sc.ExecuteNonQuery();
                            Program.MyConn.Close();

                            l.Insert_Log(
                                "delete work: " + Work_dataGridView.Rows[e.RowIndex].Cells["W_Name"].Value +
                                " from project: " + MicroProject_ID,
                                "Work", Settings.Default.username, DateTime.Now);


                            Work_dataGridView.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Work_dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex == Work_dataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == Work_dataGridView.Columns["W_DeleteRow"].Index)
                {
                    Image image = null;
                    if (Settings.Default.theme == "Light")
                        image = Resources.KAKA_Alii;
                    else image = Resources.KAKA_Alii_D;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                    var size = image.Size;
                    var location = new Point((e.CellBounds.Width - size.Width) / 2,
                        (e.CellBounds.Height - size.Height) / 2);
                    location.Offset(e.CellBounds.Location);
                    e.Graphics.DrawImage(image, location);

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Work_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        //////////////////////////////////////////////////////////////////////////
        private void Exp_dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2) //enter to name
                    Exp_dataGridView.Rows[e.RowIndex].Cells[2] = Load_Work_ComboBox("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Exp_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if click is on new row or header row
                if (e.RowIndex == Exp_dataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                //Check if click is on specific column 
                if (e.ColumnIndex == Exp_dataGridView.Columns["Exp_DeleteRow"].Index)
                {
                    if (Exp_dataGridView["Exp_InDataBase", e.RowIndex].Value == null)
                    {
                        Exp_dataGridView.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        var dialogResult = MessageBox.Show("هل أنت متأكد أنك تريد حذف مجال العمل للمستفيد؟", "Delete",
                            MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            var P_ID = Convert.ToInt32(Exp_dataGridView.Rows[e.RowIndex].Cells["ExpP_ID"].Value
                                .ToString());
                            var W_ID = Convert.ToInt32(Exp_dataGridView.Rows[e.RowIndex].Cells["Exp_ID"].Value
                                .ToString());
                            var PW_ID = Convert.ToInt32(Exp_dataGridView.Rows[e.RowIndex].Cells["Exp_RowID"].Value
                                .ToString());
                            //check connection//
                            Program.buildConnection();
                            var query = "delete from `person_workexp` " +
                                        " where W_Status like 'No' and PW_ID = " + PW_ID + "";
                            var sc = new MySqlCommand(query, Program.MyConn);
                            sc.ExecuteNonQuery();
                            Program.MyConn.Close();
                            l.Insert_Log(
                                "delete experiance: " + Exp_dataGridView.Rows[e.RowIndex].Cells["Exp_Name"].Value +
                                " from project: " + MicroProject_ID,
                                "Experiance", Settings.Default.username, DateTime.Now);


                            Exp_dataGridView.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Exp_dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex == Exp_dataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == Exp_dataGridView.Columns["Exp_DeleteRow"].Index)
                {
                    Image image = null;
                    if (Settings.Default.theme == "Light")
                        image = Resources.KAKA_Alii;
                    else image = Resources.KAKA_Alii_D;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                    var size = image.Size;
                    var location = new Point((e.CellBounds.Width - size.Width) / 2,
                        (e.CellBounds.Height - size.Height) / 2);
                    location.Offset(e.CellBounds.Location);
                    e.Graphics.DrawImage(image, location);

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Exp_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        ///////////////////////////////////////////////////////////////////////////
        private void Materials_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if click is on new row or header row
                if (e.RowIndex == Materials_dataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                //Check if click is on specific column 
                if (e.ColumnIndex == Materials_dataGridView.Columns["M_DeleteRow"].Index)
                {
                    if (Materials_dataGridView["M_InDataBase", e.RowIndex].Value == null)
                    {
                        Materials_dataGridView.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        var dialogResult = MessageBox.Show("هل أنت متأكد أنك تريد حذف المادة من الميزانية؟", "Delete",
                            MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            var M_ID = Convert.ToInt32(Materials_dataGridView.Rows[e.RowIndex].Cells["M_ID"].Value
                                .ToString());

                            //check connection//
                            Program.buildConnection();
                            MySS.query = "DELETE FROM `material` WHERE `ID` = " + M_ID;
                            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                            MySS.sc.ExecuteNonQuery();
                            Program.MyConn.Close();
                            l.Insert_Log(
                                "delete material: " + Materials_dataGridView.Rows[e.RowIndex].Cells["M_Name"].Value +
                                " from project: " + MicroProject_ID,
                                "Material", Settings.Default.username, DateTime.Now);

                            Materials_dataGridView.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                }

                if (e.ColumnIndex == Materials_dataGridView.Columns["M_Price"].Index)
                    if (e.RowIndex > 1)
                        calculate_SumOfBudgetItems(e.RowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Materials_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == Materials_dataGridView.Columns["M_Price"].Index)
                    if (Materials_dataGridView.CurrentCell.Value.ToString() != "")
                        Materials_dataGridView.CurrentCell.Value =
                            Regex.Replace(
                                string.Format("{0:n" + 4 + "}",
                                    Convert.ToDecimal(Materials_dataGridView.CurrentCell.Value.ToString())),
                                @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    //Loan_Amount_textBox.SelectionStart = Loan_Amount_textBox.Text.Length;
                    //Loan_Amount_textBox.SelectionLength = 0;
                if (e.ColumnIndex == Materials_dataGridView.Columns["M_LocalContribution"].Index)
                    if (Materials_dataGridView.CurrentCell.Value.ToString() != "")
                        Materials_dataGridView.CurrentCell.Value =
                            Regex.Replace(
                                string.Format("{0:n" + 4 + "}",
                                    Convert.ToDecimal(Materials_dataGridView.CurrentCell.Value.ToString())),
                                @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    //Loan_Amount_textBox.SelectionStart = Loan_Amount_textBox.Text.Length;
                    //Loan_Amount_textBox.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Materials_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == Materials_dataGridView.Columns["M_LocalContribution"].Index)
                {
                    sumOfItemsPrice_SYR = 0;

                    for (var i = 0; i < Materials_dataGridView.RowCount - 1; i++)
                        //calculate_SumOfBudgetItems(e.RowIndex);
                        calculate_SumOfBudgetItems(i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Materials_dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex == Materials_dataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == Materials_dataGridView.Columns["M_DeleteRow"].Index)
                {
                    Image image = null;
                    if (Settings.Default.theme == "Light")
                        image = Resources.KAKA_Alii;
                    else image = Resources.KAKA_Alii_D;

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                    var size = image.Size;
                    var location = new Point((e.CellBounds.Width - size.Width) / 2,
                        (e.CellBounds.Height - size.Height) / 2);
                    location.Offset(e.CellBounds.Location);
                    e.Graphics.DrawImage(image, location);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        private void FamilyMember_dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (FamilyNum_textBox1.Text != "")
            {
                FamilyMember_dataGridView["F_Number", e.RowIndex].Value = FamilyNum_textBox1.Text;

                FamilyMember_dataGridView.Refresh();
            }
        }

        private void FamilyMembers_DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if click is on new row or header row
                if (e.RowIndex == FamilyMember_dataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                //Check if click is on specific column 
                if (e.ColumnIndex == FamilyMember_dataGridView.Columns["P_DeleteRow"].Index)
                {
                    if (FamilyMember_dataGridView.Rows[e.RowIndex].Cells["P_InDataBase"].Value == null)
                    {
                        FamilyMember_dataGridView.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        var dialogResult = MessageBox.Show("هل أنت متأكد أنك تريد حذف فرد من العائلة؟", "Delete",
                            MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            //get person ID
                            var Member_ID = Convert.ToInt32(FamilyMember_dataGridView.Rows[e.RowIndex].Cells["FP_ID"]
                                .Value.ToString());
                            var F_ID = Convert.ToInt32(FamilyMember_dataGridView.Rows[e.RowIndex].Cells["F_ID"].Value
                                .ToString());
                            //check connection//
                            Program.buildConnection();

                            MySS.query = "DELETE FROM `person` WHERE `P_ID` = " + Member_ID;
                            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                            MySS.sc.ExecuteNonQuery();
                            Program.MyConn.Close();

                            l.Insert_Log(
                                "delete family member : " +
                                FamilyMember_dataGridView.Rows[e.RowIndex].Cells["P_FirstName"].Value + " " +
                                FamilyMember_dataGridView.Rows[e.RowIndex].Cells["P_LastName"].Value,
                                "Beneficiary_Family", Settings.Default.username, DateTime.Now);


                            FamilyMember_dataGridView.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FamilyMembers_DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex == FamilyMember_dataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == FamilyMember_dataGridView.Columns["P_DeleteRow"].Index)
                {
                    Image image = null;
                    if (Settings.Default.theme == "Light")
                        image = Resources.KAKA_Alii;
                    else image = Resources.KAKA_Alii_D;

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                    var size = image.Size;
                    var location = new Point((e.CellBounds.Width - size.Width) / 2,
                        (e.CellBounds.Height - size.Height) / 2);
                    location.Offset(e.CellBounds.Location);
                    e.Graphics.DrawImage(image, location);

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /////////////////////////////////////////////////////////////////////////// 
        private void Education_dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        /// ### ///  /// ### ///  /// ### ///  /// ### ///  /// ### ///
        #endregion

        #region save delete - visit - button clicks
            
        private void Delete_MP_button_Click(object sender, EventArgs e)
        {
            try
            {
                CheckUserPermission();
                var dialogResult = MessageBox.Show("هل أنت متأكد أنك تريد حذف المشروع؟", "Delete",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (MicroProject_ID == -1)
                        throw new Exception(
                            "لا يمكن حذف المشروع! الرجاء اختيار مشروع من قائمة البحث أولاً ثم المحاولة مرة ثانية.");
                     
                    if (Person_ID != -1)
                    { 
                        Delete_Person(Person_ID);
                        l.Insert_Log("Delete " + P_FirstName_textBox.Text + " " + P_LastName_textBox.Text, " Benefeciary ",
                            Settings.Default.username, DateTime.Now);
                    }

                    //حذف الشريك دون حذف المشروع بحال الشراكة
                    //اما اذا كان المشروع فردي ف نحذف المشروع مع المستفيد
                    if (Partners_lable.Text == "فردي")
                    {
                        Delete_MP(MicroProject_ID);
                        l.Insert_Log("Delete the project " + MicroProject_ID, "Micro Project", Settings.Default.username,DateTime.Now); 
                    }

                    clear_Person_boxes();
                    clear_Project_boxes();
                    Person_ID = -1;
                    MicroProject_ID = -1;

                    Save_MP_ID_button.Visible = true;
                    Save_MP_ID_button.BackgroundImage = Properties.Resources.Unchecked;
                    toolTip1.SetToolTip(Save_MP_ID_button, "لم يتم حجز الرقم بعد");


                    Update_Mode = false;
                    mainForm.TabName_label.Text = "Application";
                    mainForm.MP_ID_label.Text = MPID_textBox.Text;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Visit_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID == -1 && Visit_checkBox.Checked)
                {
                    Visit_checkBox.Checked = false;
                    throw new Exception("الرجاء حفظ المشروع أولاً ثم يمكنك التعديل على هذه الخلية.");
                }

                //Update project just in visit cell// 
                var state = "";

                if (Visit_checkBox.Checked == false) //not visited
                {
                    mp.Update_Project(MicroProject_ID, "MP_Visited", "0");
                    toolTip1.SetToolTip(Visit_checkBox, "غير مزار حتى الآن");
                    state = "Not Visited";
                }
                else // visited
                {
                    mp.Update_Project(MicroProject_ID, "MP_Visited", "1");
                    toolTip1.SetToolTip(Visit_checkBox, "تمت زيارته");
                    state = "Visited";
                }

                l.Insert_Log("Update the project: " + MicroProject_ID + " SET visited to:" + state + "",
                    "micro project", Settings.Default.username, DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Reject_Message_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID == -1 && Reject_Message_checkBox.Checked)
                {
                    Reject_Message_checkBox.Checked = false;
                    throw new Exception("الرجاء حفظ المشروع أولاً ثم يمكنك التعديل على هذه الخلية.");
                } 
                var query = "";
                var state = "";

                if (Reject_Message_checkBox.Checked == false)
                {
                    mp.Update_Project(MicroProject_ID, "MP_Message", "0");
                    toolTip1.SetToolTip(Reject_Message_checkBox, "لم يتم إرسال رسالة رفض حتى الآن");
                    state = " Delete Sent Message"; 
                }
                else
                {
                    mp.Update_Project(MicroProject_ID, "MP_Message", "1");
                    toolTip1.SetToolTip(Reject_Message_checkBox, "تم إرسال رسالة الرفض");
                    state = " Message Sent"; 
                }

                l.Insert_Log("Update the project: " + MicroProject_ID + state, "micro project",
                    Settings.Default.username, DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateContent_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID == -1 && ContentUpdated_checkBox.Checked)
                {
                    ContentUpdated_checkBox.Checked = false;
                    throw new Exception("الرجاء حفظ المشروع أولاً ثم يمكنك التعديل على هذه الخلية.");
                }
                
                var query = "";
                var state = "";

                if (ContentUpdated_checkBox.Checked == false)
                {
                    mp.Update_Project(MicroProject_ID, "IsContentUpdated", "0");
                    toolTip1.SetToolTip(ContentUpdated_checkBox, "المشروع غير معدل");
                    state = " Project content Is Not updated"; 
                }
                else
                {
                    mp.Update_Project(MicroProject_ID, "IsContentUpdated", "1");
                    toolTip1.SetToolTip(ContentUpdated_checkBox, "تم تبديل المشروع");
                    state = " Project content updated"; 
                }

                l.Insert_Log("Update the project: " + MicroProject_ID +":"+ state, "micro project",
                    Settings.Default.username, DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region image

        private void AddImage_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Person_ID == -1)
                    throw new Exception(
                        "لا يمكن إضافة صورة شخصية الآن! الرجاء حفظ بيانات المشروع أولاً ثم إضافة الصورة الشخصية");
                PersonPicArr = null;

                CheckUserPermission();
                myTh = new Thread(CallDialog);
                myTh.SetApartmentState(ApartmentState.STA);
                myTh.Start();
                myTh.Join();

                //////////////// Upload Image to server ////////////////////
                if (fullFtpPath != "" && image_state == 1)
                {
                    c.Upload(fullFtpPath, imageFilePath);
                    l.Insert_Log(
                        "Upload Image of:" + MicroProject_ID + ":" + P_FirstName_textBox.Text + " " +
                        P_FirstName_textBox.Text + " to the server", "Images", Settings.Default.username, DateTime.Now);

                    //////////////// Update and save Image and path to person ////////////////////
                    Update_PersonPicture(Person_ID, PersonPicArr, fullFtpPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteImage_button_Click(object sender, EventArgs e)
        {
            try
            {
                var dialogResult = MessageBox.Show("هل أنت متأكد أنك تريد حذف الصورة الشخصية للمستفيد ؟", "Delete",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    CheckUserPermission();

                    //delete from server//
                    c.Delete(fullFtpPath);
                    l.Insert_Log(
                        "Delete Image of:" + MicroProject_ID + ":" + P_FirstName_textBox.Text + " " +
                        P_FirstName_textBox.Text + " from the server", "Image", Settings.Default.username,
                        DateTime.Now);
                    /////////////////////

                    PersonPicture_pictureBox.Image = Resources.Unknown_User;
                    PersonPicArr = null;
                    ImageLocation_textBox.Text = "";
                    fullFtpPath = ftpPath = "";

                    Update_PersonPicture(Person_ID, PersonPicArr, fullFtpPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[]) converter.ConvertTo(img, typeof(byte[]));
        }

        private void CallDialog()
        {
            var open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            var res = open.ShowDialog();
            if (res == DialogResult.OK)
            {
                imageFilePath = open.FileName;
                imageName = Path.GetFileName(open.FileName);
                PersonPicture_pictureBox.ImageLocation = imageFilePath;

                var fs = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
                var br = new BinaryReader(fs);
                PersonPicArr = br.ReadBytes((int) fs.Length);

                imageName = Path.GetFileName(imageFilePath);
                 
                var save_folder_name = "";
                //if (Settings.Default.selected_hope == "Aleppo")
                //    save_folder_name = "/micro_images/profile/";
                //else if (Settings.Default.selected_hope == "Homs")
                //    save_folder_name = "/homs_micro/profile/";
                //else
                //    save_folder_name = "/new_folder/profile/"; 
                string remote_folder = user.Get_FTP_Path(Settings.Default.selected_hope);
                save_folder_name = remote_folder + "profile/";

                ImageLocation_textBox.Text = save_folder_name + imageName;
                ftpPath = save_folder_name + imageName;
                fullFtpPath = "ftp://judy@hcsyria.org" + ftpPath;

                image_state = 1;
            }
        }

        #endregion


        #region combo texbox selection change
        private void FamilyNum_textBox1_TextChanged(object sender, EventArgs e)
        {
            if (FamilyNum_textBox1.Text != "")
                CanAddFamilyMembers = true;
            else
                CanAddFamilyMembers = false;
        }

        private void FamilyNum_textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!CanAddFamilyMembers)
                    throw new Exception("لا يمكن ترك رقم دفتر العائلة فارغ !");
                //check if exsist//
                //check if family is already inserted//
                Program.buildConnection();
                MySS.sc = new MySqlCommand(
                    "select count(F_Number) from `family` where F_Number like '" + FamilyNum_textBox1.Text + "'",
                    Program.MyConn);
                var FamilyBookID = Convert.ToInt32(MySS.sc.ExecuteScalar());
                Program.MyConn.Close();
                if (FamilyBookID != 0)
                {
                    // get beneficiary names of this family
                    Program.buildConnection();
                    var query =
                        @"select CONCAT(p.P_FirstName, ' ', p.P_FatherName, ' ', p.P_LastName) as 'الاسم الثلاثي'
,PMP.Person_ID as 'ID' , PMP.MicroProject_ID as 'MicroProject_ID' 
From `person` p right outer join `person_family` pf on p.P_ID = pf.Person_ID 
left outer join `family` f on f.F_ID = pf.Family_ID 
left outer join `person_microproject` PMP on PMP.Person_ID = p.P_ID 
where f.F_Number like '" + FamilyNum_textBox1.Text + "' and pf.Relation like 'مستفيد'";
                    MySS.sc = new MySqlCommand(query, Program.MyConn);
                    var name = MySS.sc.ExecuteScalar().ToString();
                    Program.MyConn.Close();

                    //Beneficiary_In_Existing_Family = true;

                    var r = MessageBox.Show(
                        "رقم دفتر العائلة موجود مسبقاً ! " + Environment.NewLine +
                        " المستفيد التابع لدفتر العائلة هذا هو: " + name, "Dublicate", MessageBoxButtons.OK);
                    if (r == DialogResult.OK)
                    {
                        //int P_ID = Int32.Parse(dt.Rows[0]["P_ID"].ToString());
                        //int MP_ID = Int32.Parse(dt.Rows[0]["MicroProject_ID"].ToString());

                        ////////open new tab 
                        //Form application_Form = new Application_Form(Person_ID, MicroProject_ID, mainForm);
                        //mainForm.showNewTab(application_Form, "Application");

                        //Fill_FamilyMembers(1);
                        //FamilyMember_dataGridView.Refresh();
                    }
                }
                else
                {
                    FamilyMember_dataGridView["F_Number", 0].Value = FamilyNum_textBox1.Text;
                    FamilyMember_dataGridView.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void P_Sex_comboBox_TextChanged(object sender, EventArgs e)
        {
            if (P_Sex_comboBox.Text == "أنثى")
                Military_label.Visible = Military_label2.Visible =
                    Military_tableLayoutPanel.Visible = Military_textBox.Visible = false;
            else
                Military_label.Visible = Military_label2.Visible =
                    Military_tableLayoutPanel.Visible = Military_textBox.Visible = true;
        }

        private void Grant_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            P_Priest_comboBox.Enabled = P_IntermidiaryName_textBox.Enabled =
                s4_textBox.Enabled = MP_Marketing_textBox.Enabled =
                MP_NeedLicense_radioButton.Enabled = MP_DontNeedLicense_radioButton.Enabled =
                MP_LicenseSide_textBox.Enabled = true;

            P_IntermidiaryName_label.Enabled = MP_Marketing_lable.Enabled =
                P_Priest_label.Enabled = AddPriest_button.Enabled = MP_NeedLicense_label.Enabled = true;

            if (Loan_radioButton.Checked)
            {
                type = 0; 
                TrainingOthers_lable.Text = "هل توافق على الالتزام بدفتر حسابات يمكننا الاطلاع عليه في حال قبول المشروع؟";
            }
            else
            {
                type = 1;

                P_Priest_comboBox.Enabled = P_IntermidiaryName_textBox.Enabled =
                    s4_textBox.Enabled = MP_Marketing_textBox.Enabled =
                        MP_NeedLicense_radioButton.Enabled = MP_DontNeedLicense_radioButton.Enabled =
                            MP_LicenseSide_textBox.Enabled = false;

                P_IntermidiaryName_label.Enabled = P_Priest_label.Enabled =
                    AddPriest_button.Enabled = MP_Marketing_lable.Enabled = MP_NeedLicense_label.Enabled = false;
                TrainingOthers_lable.Text = "هل توافق على تدريب أشخاص في هذه المهنة؟";
            }
        }

        private void MP_Category_comboBox_Enter(object sender, EventArgs e)
        {
            //Category_bind();
        }
          
        private void MPID_textBox_TextChanged(object sender, EventArgs e)
        {
            mainForm.MP_ID_label.Text = MPID_textBox.Text;
        }

        private void MP_RequestedAmount_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (MP_RequestedAmount_textBox.Text != "")
                {
                    MP_RequestedAmount_textBox.Text =
                        Regex.Replace(
                            string.Format("{0:n" + 4 + "}", Convert.ToDecimal(MP_RequestedAmount_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    MP_RequestedAmount_textBox.SelectionStart = MP_RequestedAmount_textBox.Text.Length;
                    MP_RequestedAmount_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يمكن إدخال أحرف ضمن هذه الخلية، تأكد من إدخال أرقام فقط.");
            }
        }

        private void OverallSyrian_label_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (OverallSyrian_label.Text != "")
                    OverallSyrian_label.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(OverallSyrian_label.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                //OverallSyrian_label.SelectionStart = OverallSyrian_label.Text.Length;
                //OverallSyrian_label.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MP_SimpleProfit_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (MP_SimpleProfit_textBox.Text != "")
                {
                    MP_SimpleProfit_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(MP_SimpleProfit_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    MP_SimpleProfit_textBox.SelectionStart = MP_SimpleProfit_textBox.Text.Length;
                    MP_SimpleProfit_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PersonDOB_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && e.KeyChar != '.' && !(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void PersonDOB_textBox_Leave(object sender, EventArgs e)
        {
            if (FPersonDOB_textBox1.Text != "")
            {
                var date = Convert.ToInt32(FPersonDOB_textBox1.Text);
                if (date < 1900)
                {
                    MessageBox.Show("الرجاء إدخال تاريخ صالح!", "Date error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    FPersonDOB_textBox1.Text = "";
                }
            }

            if (P_DOB_textBox.Text != "")
            {
                var date = Convert.ToInt32(P_DOB_textBox.Text);
                if (date < 1900)
                {
                    MessageBox.Show("الرجاء إدخال تاريخ صالح!", "Date error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    P_DOB_textBox.Text = "";
                }
            }
        }

        private void PersonNationalNum_textBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(P_NationalNum_textBox.Text))
                {
                    if (P_NationalNum_textBox.Text.Length != 11)
                        MessageBox.Show("انتبه" + Environment.NewLine + Environment.NewLine + "عدد خانات الرقم الوطني لا تساوي 11", "Alert");

                    Check_National_Number(P_NationalNum_textBox.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Category_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (user_mode)
                {
                    if (Category_comboBox.SelectedIndex != -1 || Category_comboBox.Text != "")
                    {
                        SubCategory_bind(Category_comboBox.SelectedValue.ToString());
                        SubCategory_comboBox.Visible = true;
                    }
                    else
                    {
                        SubCategory_comboBox.DataSource = null;
                        SubCategory_comboBox.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Type_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //add the type_comboBox_selectedValue_changed to fill the data before binding subType  
            try
            {
                if (user_mode)
                    SubType_bind(Type_comboBox.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region new buttons

        private void NewEducation_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (var AddNewEducation = new AddNewEducation())
                {
                    AddNewEducation.ShowDialog();
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewWork_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (var AddNewExperience = new AddNewExperience())
                {
                    AddNewExperience.ShowDialog();
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddPriest_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (var AddNewPriest = new AddNewPriest())
                {
                    //AddNewPriest.ShowDialog();
                    var r = AddNewPriest.ShowDialog();
                    if (r == DialogResult.OK)
                    {
                        //refresh only this datatable -_-
                        //Bind_All_ComboBoxes();
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region mouse hover

        private void AddImage_button_MouseEnter(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Plus_Sq_L;
            else image = Resources.Plus_Sq_D;

            AddImage_button.BackgroundImage = image;
        }

        private void AddImage_button_MouseLeave(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Plus_Sq_D;
            else image = Resources.Plus_Sq_L;

            AddImage_button.BackgroundImage = image;
        }

        private void DeleteImage_button_MouseEnter(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Trash_L;
            else image = Resources.Trash_D;

            DeleteImage_button.BackgroundImage = image;
        }

        private void DeleteImage_button_MouseLeave(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Trash_D;
            else image = Resources.Trash_L;

            DeleteImage_button.BackgroundImage = image;
        }

        private void AddPriest_button_MouseEnter(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Plus_Sq_L_24;
            else image = Resources.Plus_Sq_D_24;

            AddPriest_button.BackgroundImage = image;
        }
         
        private void AddPriest_button_MouseLeave(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Plus_Sq_D_24;
            else image = Resources.Plus_Sq_L_24;

            AddPriest_button.BackgroundImage = image;
        }

        private void NewEducation_button_MouseEnter(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Plus_Sq_L_24;
            else image = Resources.Plus_Sq_D_24;

            NewEducation_button.BackgroundImage = image;
        }
        
        private void NewEducation_button_MouseLeave(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Plus_Sq_D_24;
            else image = Resources.Plus_Sq_L_24;

            NewEducation_button.BackgroundImage = image;
        }
         
        private void Save_button_MouseEnter(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Save2_L;
            else image = Resources.Save2_D;

            Save_button.BackgroundImage = image;
        }
        
        private void Save_button_MouseLeave(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Save2_D;
            else image = Resources.Save2_L;

            Save_button.BackgroundImage = image;
        }

        private void Delete_Beneficiary_button_MouseEnter(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Delete2_L;
            else image = Resources.Delete2_D;

            //Delete_Beneficiary_button.BackgroundImage = 
            Delete_MP_button.BackgroundImage = image;
        }

        private void P_Mobile_textBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(P_Mobile_textBox.Text))
            {
                if (P_Mobile_textBox.Text.Length != 10)
                    MessageBox.Show("انتبه" + Environment.NewLine + Environment.NewLine + "عدد خانات رقم الجوال لا تساوي 10", "Alert");
            }
        }

        private void PersonType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (user_mode)
            {
                string message;
                if (PersonType_comboBox.SelectedItem == "المستفيد:")
                    message = "انتبه! سوف يتم تغيير صفة الشخص من شريك إلى مستفيد";
                else message = "انتبه! سوف يتم تغيير صفة الشخص من مستفيد إلى شريك";

                MessageBox.Show(message
                    , "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Delete_Beneficiary_button_MouseLeave(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Delete2_D;
            else image = Resources.Delete2_L;

            //Delete_Beneficiary_button.BackgroundImage =
            Delete_MP_button.BackgroundImage = image;
        }
        
        private void NewWork_button_MouseEnter(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Plus_Sq_L_24;
            else image = Resources.Plus_Sq_D_24;

            NewWork_button.BackgroundImage = image;
        }
        
        private void NewWork_button_MouseLeave(object sender, EventArgs e)
        {
            Image image = null;
            if (Settings.Default.theme == "Light")
                image = Resources.Plus_Sq_D_24;
            else image = Resources.Plus_Sq_L_24;

            NewWork_button.BackgroundImage = image;
        }

        private void MPID_textBox_Leave(object sender, EventArgs e)
        {
            //عند قيام المستخدم بتغيير رقم المشروع//
            try{
                if (Save_MP_ID_button.BackgroundImage == Properties.Resources.Cheked || MicroProject_ID != -1)
                {
                    if (MPID_textBox.Text == "")
                        MessageBox.Show("لا يمكن ترك خلية رقم المشروع فارغة!");
                    else
                    {
                        var dialogResult = MessageBox.Show("هل أنت متأكد أنك تريد تغيير رقم المشروع من: " +
                            MicroProject_ID.ToString() + " ؟ " + Environment.NewLine +
                            "ليصبح الرقم: " + MPID_textBox.Text, "Edit", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            //check if this number exist//
                            if (Check_MicroProject_ID(MPID_textBox.Text))
                            {
                                int New_MP_ID = Convert.ToInt32(MPID_textBox.Text);
                                Edit_MP(MicroProject_ID, New_MP_ID);
                                l.Insert_Log("Update the project_ID from:" + MicroProject_ID + " to:" + MPID_textBox.Text, "Micro Project", Settings.Default.username, DateTime.Now);

                                MicroProject_ID = New_MP_ID;
                                MessageBox.Show("تم تعديل رقم المشروع بنجاح");
                            }
                            else
                            {
                                MessageBox.Show("الرجاء استخدام رقم مشروع آخر لأن هذا الرقم مستخدم من قبل مشروع آخر");
                                MPID_textBox.Text = MicroProject_ID.ToString();
                                MPID_textBox.Focus();
                            }
                        }
                        else
                        {
                            MPID_textBox.Text = MicroProject_ID.ToString();
                            MPID_textBox.Focus();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
        #endregion

        //prevent auto scrolling in comboboxs//
        void comboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

      
        #region context menu strip
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var _Form = new ChooseVisitKind_Form(MicroProject_ID, mainForm);
            _Form.ShowDialog();
        } 
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var _Form = new Loans_Form(MicroProject_ID, mainForm);
            mainForm.showNewTab(_Form, "Loans", 0);
        } 
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var _Form = new Attachments_Form(MicroProject_ID, mainForm);
            mainForm.showNewTab(_Form, "Attachments", 0);
        }
        private void showChecklistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _Form = new CheckList_Form(MicroProject_ID, mainForm);
            mainForm.showNewTab(_Form, "CheckList", 0);
        }

        private void addNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                //Find the admin in this database + who entered the application//
                string my_condition = " and ( UserName like '" + CreatedBy_User_label.Text + "'";
                if (CreatedBy_User_label.Text != EditedBy_User_label.Text)
                    my_condition += " or UserName like '" + EditedBy_User_label.Text + "'";
                my_condition += " or role.Role_Name like 'Admin' ) ";

                DataTable u_dt = user.Select_Users("", "", my_condition);
                List<string> emails = new List<string>();

                if (u_dt.Rows.Count != 0)
                {
                    for (int i = 0; i < u_dt.Rows.Count; i++)
                    {
                        string email = u_dt.Rows[i].Field<string>("Email");
                        if(email != "")
                            emails.Add(email);
                    }
                }

                if (emails.Count == 0)
                {
                    string msg = "لا يمكنك كتابة ملاحظات على المشروع لأن الأشخاص المعنيين بإرسال الملاحظات لهم لا يملكون ايميل محفوظ على البرنامج !"
                        + Environment.NewLine 
                        + "لذلك يُرجى إضافتها من واجهة التحكم بالمستخدمين واعادة المحاولة لاحقاً";
                    throw new Exception(msg);
                }

                string project = MicroProject_ID.ToString() + ":" + P_FirstName_textBox.Text + " " + P_LastName_textBox.Text;
                var _Form = new NewIdea_Form("Application", project, emails);
                _Form.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            }
        }
        #endregion

        private void Save_MP_ID_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Save_MP_ID_button.BackgroundImage == Properties.Resources.Cheked)
                {
                    var dialogResult = MessageBox.Show("هل تريد حذف المشروع ذي الرقم: " + MPID_textBox.Text, "Delete", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Delete_MP(MicroProject_ID);
                        l.Insert_Log("Delete the project " + MicroProject_ID, "Micro Project", Settings.Default.username, DateTime.Now);

                        clear_Project_boxes();
                        Person_ID = -1;
                        MicroProject_ID = -1;
                        EditedBy_User_label.Text = "";

                        Save_MP_ID_button.BackgroundImage = Properties.Resources.Unchecked;
                        toolTip1.SetToolTip(Save_MP_ID_button, "لم يتم حجز الرقم بعد");

                        Update_Mode = false;
                        mainForm.TabName_label.Text = "Application";
                        mainForm.MP_ID_label.Text = MPID_textBox.Text;
                    }
                }
                else
                {
                    if (MPID_textBox.Text == "")
                        throw new Exception("لا يمكن حجز الرقم. الرجاء التأكد من الرقم وإعادة المحاولة.");
                    else
                    {
                        CheckUserPermission();

                        if (Check_MicroProject_ID(MPID_textBox.Text))
                        {
                            Insert_MicroProject();
                            MicroProject_ID = int.Parse(MPID_textBox.Text);
                            l.Insert_Log("Insert the project: " + MicroProject_ID, "Micro Project",
                                Settings.Default.username, DateTime.Now);
                            CreatedBy_User_label.Text = Settings.Default.username;

                            Save_MP_ID_button.BackgroundImage = Properties.Resources.Cheked;
                            toolTip1.SetToolTip(Save_MP_ID_button, "محجوز");
                        }
                        else
                        {
                            MessageBox.Show("الرجاء استخدام رقم مشروع آخر لأن هذا الرقم مستخدم من قبل مشروع آخر");
                            MPID_textBox.Focus();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void AddPartner_button_Click(object sender, EventArgs e)
        {
            try
            {
                //  create new beneficiary //
                var dialogResult = MessageBox.Show("هل تريد إضافة شريك آخر لهذا المشروع؟", "Partners", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Update_Mode = false; 
                    clear_Person_boxes();
                    user_mode = false;
                    PersonType_comboBox.SelectedIndex = 1;
                    user_mode = true;

                    Main_panel.AutoScrollPosition = new Point(0, 0);
                    first_tableLayoutPanel.Focus();
                } 
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            try
            {
                //التأكد من أنه تم حفظ المشروع
                if (Save_MP_ID_button.BackgroundImage == Properties.Resources.Unchecked || MicroProject_ID == -1)
                    throw new Exception("لا يمكن الحفظ قبل حجز رقم المشروع! الرجاء حفظ رقم المشروع ثم إعادة المحاولة..");

                ////////////////  Insert Person ///////////////////////
                if (P_FirstName_textBox.Text == "" || P_LastName_textBox.Text == "" || P_FatherName_textBox.Text == ""
                    || P_NationalNum_textBox.Text == "" || P_NumAtHome_textBox.Text == "" || P_Parish_comboBox.Text == "")
                    throw new Exception("لا يمكن الحفظ بوجود خلايا فارغة، يُرجى التأكد من كافة البيانات والمحاولة مرة أخرى.");

                Check_National_Number(P_NationalNum_textBox.Text);

                //احتمال يكونوا حافظين المشروع فقط (حاجزين رقم ومامدخلين مستفيد أبدا
                // أي اننا في مود التعديل ولكن رقم المستفيد -1
                if (!Update_Mode || (Person_ID == -1 && Update_Mode))
                { 
                    Insert_Person();
                    l.Insert_Log("Insert " + P_FirstName_textBox.Text + " " + P_LastName_textBox.Text, " Benefeciary ", Settings.Default.username, DateTime.Now);

                    Person_ID = Get_CurrentPersonID();
                    P_ID_textBox.Text = Person_ID.ToString();
                    Beneficiary_Count++;

                    //////////// Link Person to Project ////////////////////////
                    if (Person_ID != -1 || P_ID_textBox.Text != "")
                    {
                        if (Beneficiary_Count == 1 && PersonType_comboBox.SelectedItem.ToString().Contains("مستفيد"))
                            Insert_Person_MicroProject(Convert.ToInt32(Person_ID), MicroProject_ID, "مستفيد");
                        else
                        {
                            Insert_Person_MicroProject(Convert.ToInt32(Person_ID), MicroProject_ID, "شريك");
                            Partners_lable.Text = "شراكة";
                        }
                        l.Insert_Log("Insert and link the Beneficiary: " + P_FirstName_textBox.Text + " " + P_LastName_textBox.Text + " with the project:" + MicroProject_ID,
                            " Beneficiary_Project ", Settings.Default.username, DateTime.Now);
                    }
                    /////////////////////////////////////////////////////////////

                    // Turn to Update mood before end all the save operation// 
                    Update_Mode = true;
                    AddPartner_button.Visible = true;

                }
                else
                {
                    Update_Person(Person_ID);
                    if (Beneficiary_Count == 1 && PersonType_comboBox.SelectedItem.ToString().Contains("مستفيد"))
                        Update_Person_MicroProject(Person_ID, MicroProject_ID, "مستفيد");
                    else Update_Person_MicroProject(Person_ID, MicroProject_ID, "شريك");


                    l.Insert_Log("Update " + P_FirstName_textBox.Text + " " + P_LastName_textBox.Text, " Benefeciary ", Settings.Default.username, DateTime.Now);
                    
                }

                Update_MP(MicroProject_ID);
                l.Insert_Log("Update the project: " + MicroProject_ID, "Micro Project", Settings.Default.username,DateTime.Now);
                EditedBy_User_label.Text = Settings.Default.username;

                Insert_Update_ProjectDetails(MicroProject_ID);

                Insert_Update_AllFamilyDetails(); 

                /// *** Insert course - education - work - experience - loss *** /// only that aren't in database
                Insert_Update_PersonDetails(Person_ID);
                
                //  create new beneficiary //
                var dialogResult = MessageBox.Show("تم حفظ كافة بيانات المشروع. هل تريد إضافة مشروع جديد ؟", "New Application", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    clear_Person_boxes();
                    clear_Project_boxes();

                    Update_Mode = false;
                    AddPartner_button.Visible = false;
                     
                    mainForm.TabName_label.Text = "Application";
                    mainForm.MP_ID_label.Text = MPID_textBox.Text;

                    Main_panel.AutoScrollPosition = new Point(0, 0);
                }
                else
                {
                    Update_Mode = true;
                    AddPartner_button.Visible = true;
                    Save_MP_ID_button.Visible = false;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message,"Save Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation); }
        }
    }
}