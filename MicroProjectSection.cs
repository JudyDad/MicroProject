using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MyWorkApplication
{
    public partial class MicroProject_Section : Form
    {
        public MicroProject_Section()
        {
            InitializeComponent();
            MProjectSection_tabControl.Visible = false;
        }
        public MicroProject_Section(int i, string username)
        {
            InitializeComponent();
            pageState = i;
            this.username = username;
            if (pageState == 1) //if the project is already exsist and just select
            {
                MProjectSection_tabControl.Visible = false;
            }
            else //micro project section
            {
                MProjectSection_tabControl.Visible = true;
                InsertMP_button.Visible = InsertPersonProject_button.Visible = InsertIntermidate_button.Visible
                = InsertPriest_button.Visible = InsertActivity_button.Visible = AddItem_button.Visible
                = UpdatePersonProject_button.Visible = UpdateIntermidate_button.Visible
                = UpdatePriest_button.Visible = UpdateActivity_button.Visible = UpdateItem_button.Visible
                = FileSave_button.Visible = true;

                UpdateMP_button.Visible = false;
            }
        }
        public MicroProject_Section(DataRow dr, string username)
        {
            InitializeComponent();
            this.username = username;
            SelectedDataRow = dr;
            MProjectSection_tabControl.Visible = true;
            InsertMP_button.Visible = false;
            UpdateMP_button.Visible = UpdatePersonProject_button.Visible = UpdateIntermidate_button.Visible
            = UpdatePriest_button.Visible = UpdateActivity_button.Visible = UpdateItem_button.Visible
            = InsertPersonProject_button.Visible = InsertIntermidate_button.Visible = FileSave_button.Visible
            = InsertPriest_button.Visible = InsertActivity_button.Visible = AddItem_button.Visible = true;
        }

        public void fillBoxes()
        {
            if (SelectedDataRow != null)
            {
                MicroProject_ID = Int32.Parse(SelectedDataRow["رقم المشروع"].ToString());
                MPID_textBox.Text = SelectedDataRow["رقم المشروع"].ToString();
                MP_Country_textBox.Text = (string)SelectedDataRow["البلد"];
                MP_Parish_textBox.Text = (string)SelectedDataRow["الطائفة"];
                MP_City_textBox.Text = (string)SelectedDataRow["المدينة"];
                MP_DateOfRequest_dateTimePicker.Value = (DateTime)SelectedDataRow["تاريخ تعبئة الطلب"];
                MP_Name_textBox.Text = (string)SelectedDataRow["اسم المشروع"];
                MP_AllPriceNeeded_textBox.Text = (string)SelectedDataRow["المبلغ المطلوب"];
                MP_PeriodOfExecution_textBox.Text = (string)SelectedDataRow["مدة التنفيذ"];
                MP_SufferanceOfPerson_textBox.Text = (string)SelectedDataRow["معاناة المستفيد"];
                MP_Describtion_textBox.Text = (string)SelectedDataRow["الوصف"];
                MP_ResonOfProject_textBox.Text = (string)SelectedDataRow["رغبة المستفيد بالمشروع"];

                string NeedLicense = (string)SelectedDataRow["هل يحتاج رخصة"];
                if (NeedLicense.Equals("True"))
                    MP_NeedLicense_radioButton.Checked = true;
                else
                    MP_DontNeedLicense_radioButton.Checked = true;
                MP_LicenseSide_textBox.Text = (string)SelectedDataRow["الرخصة"];
                MP_PlaceOfExecution_textBox.Text = SelectedDataRow["مكان التنفيذ"].ToString();
                MP_ResonOfPlace_textBox.Text = (string)SelectedDataRow["سبب اختيار مكان التنفيذ"];
        //        MP_DurationGuarantee_textBox.Text = (string)SelectedDataRow["الاستدامة"];
                MP_OtherInformation_textBox.Text = (string)SelectedDataRow["معلومات أخرى"];
       //         MP_DirectPayee_textBox.Text = (string)SelectedDataRow["المستفيدون المباشرون"];
       //         int num = (int)SelectedDataRow["عدد المستفيدون المباشرون"];
      //          MP_DirectPayeeNum_textBox.Text = num.ToString();
                //MP_InDirectPayee_textBox.Text = (string)SelectedDataRow["المستفيدون الغير المباشرون"];
                int num = (int)SelectedDataRow["الربح المتوقع"];
                MP_SimpleProfit_textBox.Text = num.ToString();
                // Person_ID = (int)SelectedDataRow["رقم المستفيد"];

                MP_SourceOfIncome_textBox.Text = (string)SelectedDataRow["مصدر الدخل"];
                MP_MedicalCond_textBox.Text = (string)SelectedDataRow["حالات مرضية"];
                MP_FinancialLoss_textBox.Text = (string)SelectedDataRow["خسارة مادية"];
                MP_SentimentalLoss_textBox.Text = (string)SelectedDataRow["خسارة معنوية"];
                string HomeState = (string)SelectedDataRow["حالة المنزل"];
                if (HomeState == "أجار")
                    MP_HomeRent_radioButton.Checked = true;
                else if (HomeState == "ملك")
                    MP_HomeOwnership_radioButton.Checked = true;
                else if (HomeState == "استضافة")
                    MP_HomeHost_radioButton.Checked = true;
                else
                    MP_HomeOther_radioButton.Checked = true;
                string ShopState = (string)SelectedDataRow["حالة المحل"];
                if (ShopState == "أجار")
                    MP_ShopRent_radioButton.Checked = true;
                else if (ShopState == "ملك")
                    MP_ShopOwnership_radioButton.Checked = true;
                else if (ShopState == "استضافة")
                    MP_ShopHost_radioButton.Checked = true;
                else
                    MP_ShopOther_radioButton.Checked = true;

                string MP_Accepted = (string)SelectedDataRow["حالة المشروع"];
                if (MP_Accepted == "مقبول")
                    MP_Accepted_radioButton.Checked = true;
                else if (MP_Accepted == "مرفوض")
                    MP_NotAccepted_radioButton.Checked = true;
                else if (MP_Accepted == "مؤجل")
                    MP_Delayed_radioButton.Checked = true;
                else
                    MP_Hold_radioButton.Checked = true;

                MP_StatusReason_textBox.Text = (string)SelectedDataRow["سبب حالة المشروع"];
                MP_StatusComment_textBox.Text = (string)SelectedDataRow["تعليقات"];

                if (SelectedDataRow["درجة التقييم"].ToString() != null)
                {
                    Level_comboBox.Text = (string)SelectedDataRow["درجة التقييم"].ToString();
                }
            }

            MicroProject_Intermidiate_bind();
            Budget_Item_bind();
            Plan_Activity_bind();
            Person_MicroProject_bind();
            this.DialogResult = DialogResult.OK;

            MicroProjectID_textBox.Text = SelectedDataRow["رقم المشروع"].ToString();
            microProjectName_textBox5.Text = microProjectName_textBox4.Text = microProjectName_textBox3.Text
                = microProjectName_textBox2.Text = microProjectName_textBox1.Text = MP_Name_textBox.Text;
            microProjectID_textBox1.Text = microProjectID_textBox2.Text = microProjectID_textBox3.Text
                = microProjectID_textBox4.Text = microProjectID_textBox5.Text = MicroProject_ID.ToString();

        }

        private int pageState = -1;
        private PersonSection PersonSection;
        private SqlCommand sc;
        private SqlDataAdapter da;
        private DataTable dt;
        public static DataRow SelectedDataRow, SelectedDataRowM;
        private int MicroProject_ID, Person_ID, PMP_ID;
        private int Intermidiary_ID, Priest_ID;
        private int Budget_ID, Item_ID;
        private int Plan_ID, Activity_ID, image_ID;
        private int Level_ID;
        private Double sumOfItemsPrice_SYR;
        private Log l;
        private String username;
        SqlDataReader reader;
        AllProjects AllProjects;

        #region bind

        private void MicroProject_Intermidiate_bind()
        {
            string strCmd = "select [IS_ID] as 'رقم الوسيط'" +
                ",[IS_Name] as 'اسم الوسيط'" +
                ",[IS_Job] as 'العمل'" +
                ",[IS_Phone] as 'الهاتف'" +
                ",[IS_Email] as 'الإيميل'" +
                ",[IS_Address] as 'العنوان'" +
                ",[IS_isPriest] as 'هل هو كاهن'" +
                ",[MicroProject_ID] as 'رقم المشروع'" +
                "\n from IntermediarySide where IS_isPriest = 'NO' and MicroProject_ID= " + MicroProject_ID + " ";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);

            MP_Intermidiary_dataGridView.ColumnHeadersVisible = false;
            MP_Intermidiary_dataGridView.DataSource = dt;
            MP_Intermidiary_dataGridView.ColumnHeadersVisible = true;

            strCmd = "select [IS_ID] as 'رقم الكاهن'" +
                ",[IS_Name] as 'اسم الكاهن'" +
                ",[IS_Job] as 'العمل'" +
                ",[IS_Phone] as 'الهاتف'" +
                ",[IS_Email] as 'الإيميل'" +
                ",[IS_Address] as 'العنوان'" +
                ",[IS_isPriest] as 'هل هو كاهن'" +
                ",[MicroProject_ID] as 'رقم المشروع'" +
                "\n from IntermediarySide where IS_isPriest = 'YES' and MicroProject_ID= " + MicroProject_ID + " ";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            MP_Priest_dataGridView.DataSource = dt;
            DataGridViewColumn dgC1 = MP_Intermidiary_dataGridView.Columns["هل هو كاهن"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = MP_Intermidiary_dataGridView.Columns["رقم المشروع"];
            dgC2.Visible = false;
            DataGridViewColumn dgC3 = MP_Priest_dataGridView.Columns["هل هو كاهن"];
            dgC3.Visible = false;
            DataGridViewColumn dgC4 = MP_Priest_dataGridView.Columns["رقم المشروع"];
            dgC4.Visible = false;
            DataGridViewColumn dgC5 = MP_Intermidiary_dataGridView.Columns["رقم الوسيط"];
            dgC5.Visible = false;
            DataGridViewColumn dgC6 = MP_Priest_dataGridView.Columns["رقم الكاهن"];
            dgC6.Visible = false;
        }

        private void Budget_Item_bind()
        {
            string str = "select BI.[Budget_ID] as 'رقم الفاتورة'" +
                        ",BI.[Item_ID] as 'رقم المادة'" +
                        ",I.I_Name as 'اسم المادة'" +
                        ",I.I_Price as 'السعر'" +
                        ",BI.[Item_Amount] as 'الكمية'" +
                        ",I.I_LocalContribution as 'المساهمة المحلية'" +
                        ",I.I_Comment as 'معلومات أخرى'" +
                        ",B.MicroProject_ID as 'رقم المشروع'" +
                        "\n from [dbo].[Item] I right outer join [dbo].[Budget_Item] BI on BI.Item_ID = I.I_ID " +
                        "left outer join [dbo].[Budget] B on BI.Budget_ID = B.B_ID" +
                        "\n where B.MicroProject_ID = " + MicroProject_ID + " ";
            sc = new SqlCommand(str, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);

            MP_Item_dataGridView.ColumnHeadersVisible = false;
            MP_Item_dataGridView.DataSource = dt;
            MP_Item_dataGridView.ColumnHeadersVisible = true;

            DataGridViewColumn dgC3 = MP_Item_dataGridView.Columns["رقم المادة"];
            dgC3.Visible = false;
        }

        private void Plan_Activity_bind()
        {
            AddNewPlan_radioButton.Checked = UpdateOrDeletePlan_radioButton.Checked = false;
            string str = " select AP.Activity_ID as 'رقم النشاط'" +
                        ",AP.ProjectPlan_ID as 'رقم الخطة'" +
                        ",A.[A_Name] as 'اسم النشاط'" +
                        ",[A_Month1] as 'الشهر1'" +
                        ",[A_Month2] as 'الشهر2'" +
                        ",[A_Month3] as 'الشهر3'" +
                        ",[A_Month4] as 'الشهر4'" +
                        ",P.MicroProject_ID as 'رقم المشروع'" +
                        "\n from [dbo].[ProjectPlan] P right outer join [dbo].[Activity_Plan] AP on P.PP_ID = AP.ProjectPlan_ID " +
                        "left outer join Activity A on A.A_ID = AP.Activity_ID " +
                        "\n where P.MicroProject_ID = " + MicroProject_ID + " ";
            sc = new SqlCommand(str, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);

            MP_Activity_dataGridView.ColumnHeadersVisible = false;
            MP_Activity_dataGridView.DataSource = dt;
            MP_Activity_dataGridView.ColumnHeadersVisible = true;

            DataGridViewColumn dgC1 = MP_Activity_dataGridView.Columns["رقم النشاط"];
            dgC1.Visible = false;
            //DataGridViewColumn dgC2 = MP_Activity_dataGridView.Columns["رقم المشروع"];
            //dgC2.Visible = false;
            //DataGridViewColumn dgC3 = MP_Activity_dataGridView.Columns["رقم النشاط"];
            //dgC3.Visible = false;
        }

        private void Person_MicroProject_bind()
        {
            string selectMPQuery = "select PMP.PMP_ID as 'رقم السجل'"
                + ",PMP.MicroProject_ID as 'رقم المشروع'"
                //   + ",MP.[MP_Name] as 'اسم المشروع'"
                + ",PMP.[Person_ID] as 'رقم المستفيد'"
                + ",(P1.P_FirstName + ' ' + P1.P_FatherName + ' ' + P1.P_LastName) as 'اسم المستفيد'"
                + ",PMP.[PersonType] as 'حالة المستفيد'"

                + "\n from Person_MicroProject PMP left outer join Person P1 on P1.P_ID = PMP.Person_ID "
                + "\n where PMP.MicroProject_ID = " + MicroProject_ID + " ";
            sc = new SqlCommand(selectMPQuery, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);

            PersonDataGridView.ColumnHeadersVisible = false;
            PersonDataGridView.DataSource = dt;
            PersonDataGridView.ColumnHeadersVisible = true;

            DataGridViewColumn dgC1 = PersonDataGridView.Columns["رقم السجل"];
            dgC1.Visible = false;
            //DataGridViewColumn dgC2 = PersonDataGridView.Columns["رقم المستفيد"];
            //dgC2.Visible = false;
        }

        private void Image_bind(string MP_ID)
        {
            string strCmd = "SELECT [Image_ID] as N'رقم الصورة'" +
                //       ",[Image_Content] as N'محتوى الصورة'" +
                    ",[Image_Path] as N'مسار الصورة'" +
                    ",[Image_Type] as N'نوع الصورة'" +
                    ",[MicroProject_ID] as N'رقم المشروع'" +
                    " \n FROM [MyWork].[dbo].[Image] " +
                    " \n Where MicroProject_ID = " + Int32.Parse(MP_ID) + " ";
            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            Images_dataGridView.DataSource = dt;
            DataGridViewColumn dgC2 = Images_dataGridView.Columns["رقم الصورة"];
            dgC2.Visible = false;
        }

        private void Level_bind()
        {
            string strCmd = "select [Level_ID],[Level_Symbol] from [Level]";
            sc = new SqlCommand(strCmd, Program.MyConn);
            da = new SqlDataAdapter(strCmd, Program.MyConn);
            reader = sc.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("Level_ID", typeof(string));
            dt.Columns.Add("Level_Symbol", typeof(string));
            dt.Load(reader);
            Level_comboBox.DisplayMember = "Level_Symbol";
            Level_comboBox.ValueMember = "Level_ID";
            Level_comboBox.DataSource = dt;
        }

        #endregion bind

        #region inserts

        private void insertMicroProject()
        {
            int status,home_state,shop_state;
            if (MP_Accepted_radioButton.Checked)
            { status = 1; }
            else if (MP_NotAccepted_radioButton.Checked)
            { status = 0; }
            else if (MP_Delayed_radioButton.Checked)
            { status = 2; }
            else    //HOLD
            { status = 3; }

            if (MP_HomeRent_radioButton.Checked)    
            { home_state = 0; }                 //أجار
            else if (MP_HomeOwnership_radioButton.Checked)
            { home_state = 1; }                 //ملك
            else if (MP_HomeHost_radioButton.Checked)
            { home_state = 2; }                 //استضافة
            else    
            { home_state = 3; }                 //other

            if (MP_ShopRent_radioButton.Checked)
            { shop_state = 0; }                 //أجار
            else if (MP_ShopOwnership_radioButton.Checked)
            { shop_state = 1; }                 //ملك
            else if (MP_ShopHost_radioButton.Checked)
            { shop_state = 2; }                 //استضافة
            else    
            { shop_state = 3; }                 //other
            
            string insMPQuery = "Insert Into MicroProject values(N'"
                + MP_Country_textBox.Text + "',N'"
                + MP_Parish_textBox.Text + "',N'"
                + MP_City_textBox.Text + "','"
                + MP_DateOfRequest_dateTimePicker.Value.Month.ToString() + "/"
                + MP_DateOfRequest_dateTimePicker.Value.Day.ToString() + "/"
                + MP_DateOfRequest_dateTimePicker.Value.Year.ToString() + "',N'"
                + MP_Name_textBox.Text + "',N'"
                + MP_AllPriceNeeded_textBox.Text + "',N'"
                + MP_PeriodOfExecution_textBox.Text + "',N'"
                + MP_Describtion_textBox.Text + "',N'"
                + MP_ResonOfProject_textBox.Text + "',N'"
                + MP_SufferanceOfPerson_textBox.Text + "',N'"
                + (MP_NeedLicense_radioButton.Checked ? "True" : "False") + "',N'"
                + MP_LicenseSide_textBox.Text + "',N'"
                + MP_PlaceOfExecution_textBox.Text + "',N'"
                + MP_ResonOfPlace_textBox.Text + "',"

                //+ "NoThing" + "','"
                //+ MP_DirectPayee_textBox.Text + "',N'"
                //+ MP_InDirectPayee_textBox.Text + "','"
                //+ MP_DirectPayeeNum_textBox.Text + "','"
                //+ MP_DurationGuarantee_textBox.Text + "',N'"

                + MP_SimpleProfit_textBox.Text + ",N'"
                + MP_OtherInformation_textBox.Text + "',N'"
                
                + MP_SourceOfIncome_textBox.Text + "',N'"
                + MP_MedicalCond_textBox.Text + "',N'"
                + MP_FinancialLoss_textBox.Text + "',N'"
                + MP_SentimentalLoss_textBox.Text + "',"
                + home_state + ","
                + shop_state + ","

                + status + ",N'"
                + MP_StatusReason_textBox.Text + "',N'"
                + MP_StatusComment_textBox.Text + "',"
                + Int32.Parse(Level_comboBox.SelectedValue.ToString()) + ")" ;
            sc = new SqlCommand(insMPQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertMicroProject_withIdentity()
        {
            int status,home_state,shop_state;
            if (MP_Accepted_radioButton.Checked)
            { status = 1; }
            else if (MP_NotAccepted_radioButton.Checked)
            { status = 0; }
            else if (MP_Delayed_radioButton.Checked)
            { status = 2; }
            else    //HOLD
            { status = 3; }

            if (MP_HomeRent_radioButton.Checked)
            { home_state = 0; }                 //أجار
            else if (MP_HomeOwnership_radioButton.Checked)
            { home_state = 1; }                 //ملك
            else if (MP_HomeHost_radioButton.Checked)
            { home_state = 2; }                 //استضافة
            else
            { home_state = 3; }                 //other

            if (MP_ShopRent_radioButton.Checked)
            { shop_state = 0; }                 //أجار
            else if (MP_ShopOwnership_radioButton.Checked)
            { shop_state = 1; }                 //ملك
            else if (MP_ShopHost_radioButton.Checked)
            { shop_state = 2; }                 //استضافة
            else
            { shop_state = 3; }                 //other

            string insMPQuery = "SET IDENTITY_INSERT  MicroProject ON "
                + " Insert Into MicroProject ([MP_ID],[MP_Country],[MP_Parish],[MP_City],[MP_DateOfRequest],[MP_Name],[MP_AllPriceNeeded]" 
                + ",[MP_PeriodOfExecution],[MP_Describtion],[MP_ResonOfProject],[MP_SufferanceOfPerson],[MP_IsNeedLicense]"
                + ",[MP_LicenseSide],[MP_PlaceOfExecution],[MP_ResonOfPlace],[MP_SimpleProfit],[MP_OtherComment]"
                + ",[MP_SourceOfIncome],[MP_MedicalCond],[MP_FinancialLoss],[MP_SentimentalLoss],[MP_HomeState],[MP_ShopState]"
                + ",[MP_State],[MP_StateReason],[MP_StateComment],[MP_Level_ID]) values("
                + Int32.Parse(MPID_textBox.Text.ToString()) + ",N'"
                + MP_Country_textBox.Text + "',N'"
                + MP_Parish_textBox.Text + "',N'"
                + MP_City_textBox.Text + "','"
                + MP_DateOfRequest_dateTimePicker.Value.Month.ToString() + "/"
                        + MP_DateOfRequest_dateTimePicker.Value.Day.ToString() + "/"
                        + MP_DateOfRequest_dateTimePicker.Value.Year.ToString() + "',N'"
                + MP_Name_textBox.Text + "',N'"
                + MP_AllPriceNeeded_textBox.Text + "',N'"
                + MP_PeriodOfExecution_textBox.Text + "',N'"
                + MP_Describtion_textBox.Text + "',N'"
                + MP_ResonOfProject_textBox.Text + "',N'"
                + MP_SufferanceOfPerson_textBox.Text + "',N'"
                + (MP_NeedLicense_radioButton.Checked ? "True" : "False") + "',N'"
                + MP_LicenseSide_textBox.Text + "',N'"
                + MP_PlaceOfExecution_textBox.Text + "',N'"
                + MP_ResonOfPlace_textBox.Text + "',"
                //+ MP_DirectPayee_textBox.Text + "',N'"
                // + MP_InDirectPayee_textBox.Text + "','"
                //+ "NoThing" + "','"
                //+ MP_DirectPayeeNum_textBox.Text + "','"
                //+ MP_DurationGuarantee_textBox.Text + "',N'"
                + Int32.Parse(MP_SimpleProfit_textBox.Text.ToString()) + ",N'"
                + MP_OtherInformation_textBox.Text + "',N'"
               
                + MP_SourceOfIncome_textBox.Text + "',N'"
                + MP_MedicalCond_textBox.Text + "',N'"
                + MP_FinancialLoss_textBox.Text + "',N'"
                + MP_SentimentalLoss_textBox.Text + "',"
                + home_state + ","
                + shop_state + ","

                + status + ",N'"
                + MP_StatusReason_textBox.Text + "',N'" 
                + MP_StatusComment_textBox.Text + "',"
                + (Level_comboBox.SelectedValue != null ? Level_comboBox.SelectedValue : null) + ")"
                + "\nSET IDENTITY_INSERT  MicroProject OFF";

            sc = new SqlCommand(insMPQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertPerson_MicroProject(int personID, int microProjectID, string type)
        {
            string insQuery = "insert into [dbo].[Person_MicroProject] values ("
                            + personID + ","
                            + microProjectID + ",N'"
                            + type + "' " +
                            ")";
            sc = new SqlCommand(insQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertIntermediarySide(bool isPriest, int mpNo)
        {
            string insISQuery;
            if (isPriest)
            {
                insISQuery = "Insert Into IntermediarySide values(N'"
                + PriestName_textBox.Text + "',N'"
                + PriestJob_textBox.Text + "',N'"
                + PriestPhone_textBox.Text + "',N'"
                + PriestEmail_textBox.Text + "',N'"
                + PriestAddress_textBox.Text + "','"
                + null + "','"
                + "Yes" + "',"
                + mpNo + ")";
            }
            else
            {
                insISQuery = "Insert Into IntermediarySide values(N'"
                + IntermediaryName_textBox.Text + "',N'"
                + IntermediaryJob_textBox.Text + "',N'"
                + IntermediaryPhone_textBox.Text + "',N'"
                + IntermediaryEmail_textBox.Text + "',N'"
                + IntermediaryAddress_textBox.Text + "','"
                + null + "','"
                + "No" + "',"
+ mpNo + ")";
            }
            sc = new SqlCommand(insISQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertProjectPlan(int microProjectID)
        {
            string insPPQuery = "Insert Into ProjectPlan values(" + "null" + "," + microProjectID + ")";
            sc = new SqlCommand(insPPQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertActivity(string Aname, string AM1, string AM2, string AM3, string AM4)
        {
            string insAQuery = "Insert Into Activity values(N'"
                                      + Aname + "',N'"
                                      + AM1 + "',N'"
                                      + AM2 + "',N'"
                                      + AM3 + "',N'"
                                      + AM4 + "')";
            sc = new SqlCommand(insAQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertActivityPlan(int ANo, int PPNo)
        {
            string insAPQuery = "Insert Into [dbo].[Activity_Plan] values("
                                      + "(select [A_ID] from Activity where [A_ID] = " + ANo + "),"
                                      + "(select [PP_ID] from ProjectPlan where [PP_ID] = " + PPNo + "))";
            sc = new SqlCommand(insAPQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertItem(string Iname, string IlocalCont, double Lprice, string Lcomment)
        {
            string insIQuery = "Insert Into Item values(N'"
                                      + Iname + "',N'"
                                      + IlocalCont + "',"
                                      + Lprice + ",N'"
                                      + Lcomment + "')";
            sc = new SqlCommand(insIQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertBudget()
        {
            string insBQuery = "Insert Into Budget values("
                //     + Double.Parse(OverallSyrian_label.Text) + ","
                //     + Double.Parse(OverallEuro_label.Text) + ","
                //                      + Double.Parse(EuroToSyrianPrice_textBox.Text) + ",'"
                                      + 500 + ",'"
                                      + DateTime.Now.Month.ToString() + "/"
                                      + DateTime.Now.Day.ToString() + "/"
                                      + DateTime.Now.Year.ToString() + "',"
                                      + MicroProject_ID + ")";
            sc = new SqlCommand(insBQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertBudgetItem(int bNo, int iNo, Double amount)
        {
            string insBIQuery = "Insert Into [dbo].[Budget_Item] values("
                                      + "(select [B_ID] from [dbo].[Budget] where [B_ID] = " + bNo + "),"
                                      + "(select [I_ID] from [dbo].[Item] where [I_ID] = " + iNo + "),"
                                      + amount
                                      + ")";
            sc = new SqlCommand(insBIQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        public void insertImage(int MP_ID, string path, byte[] array, string type)
        {
            sc = new SqlCommand("select IDENT_CURRENT('Image')", Program.MyConn);
            Int32.TryParse((sc.ExecuteScalar()).ToString(), out image_ID);
            image_ID++;
            string destinationFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string insertImg = "Insert Into [dbo].[Image] values("
                                + "@Image_Content" + ",N'"
                                + destinationFolderPath + "\\Micro_Project " + image_ID + ".jpg' ,N'"
                                + type + "',"
                                + MP_ID + ")";

            sc = new SqlCommand(insertImg, Program.MyConn);
            sc.Parameters.Add(new SqlParameter("@Image_Content", array));
            sc.ExecuteNonQuery();
        }

        #endregion inserts

        #region updates
        private void Update_MP(int MP_ID)
        {
            int status, home_state, shop_state;
            if (MP_Accepted_radioButton.Checked)
            { status = 1; }
            else if (MP_NotAccepted_radioButton.Checked)
            { status = 0; }
            else if (MP_Delayed_radioButton.Checked)
            { status = 2; }
            else    //HOLD
            { status = 3; }

            if (MP_HomeRent_radioButton.Checked)
            { home_state = 0; }                 //أجار
            else if (MP_HomeOwnership_radioButton.Checked)
            { home_state = 1; }                 //ملك
            else if (MP_HomeHost_radioButton.Checked)
            { home_state = 2; }                 //استضافة
            else
            { home_state = 3; }                 //other

            if (MP_ShopRent_radioButton.Checked)
            { shop_state = 0; }                 //أجار
            else if (MP_ShopOwnership_radioButton.Checked)
            { shop_state = 1; }                 //ملك
            else if (MP_ShopHost_radioButton.Checked)
            { shop_state = 2; }                 //استضافة
            else
            { shop_state = 3; }                 //other

            string updMP = "update [dbo].[MicroProject] set " +
                "[MP_Country] = N'" + MP_Country_textBox.Text + "'" +
                ",[MP_Parish] = N'" + MP_Parish_textBox.Text + "'" +
                ",[MP_City] = N'" + MP_City_textBox.Text + "'" +
                ",[MP_DateOfRequest] = N'" + MP_DateOfRequest_dateTimePicker.Value.Month.ToString() + "/" + MP_DateOfRequest_dateTimePicker.Value.Day.ToString() + "/"
                                + MP_DateOfRequest_dateTimePicker.Value.Year.ToString() + "'" +
                ",[MP_Name] = N'" + MP_Name_textBox.Text + "'" +
                ",[MP_AllPriceNeeded]= N'" + MP_AllPriceNeeded_textBox.Text + "'" +
                ",[MP_PeriodOfExecution]= N'" + MP_PeriodOfExecution_textBox.Text + "'" +
                ",[MP_Describtion] = N'" + MP_Describtion_textBox.Text + "'" +
                ",[MP_ResonOfProject] = N'" + MP_ResonOfProject_textBox.Text + "'" +
                ",[MP_SufferanceOfPerson] = N'" + MP_SufferanceOfPerson_textBox.Text + "'" +
                ",[MP_IsNeedLicense] = N'" + (MP_NeedLicense_radioButton.Checked ? "True" : "False") + "'" +
                ",[MP_LicenseSide] = N'" + MP_LicenseSide_textBox.Text + "'" +
                ",[MP_PlaceOfExecution] =N'" + MP_PlaceOfExecution_textBox.Text + "'" +
                ",[MP_ResonOfPlace] =N'" + MP_ResonOfPlace_textBox.Text + "'" +
                ",[MP_SimpleProfit] = N'" + MP_SimpleProfit_textBox.Text + "'" +
                ",[MP_OtherComment] = N'" + MP_OtherInformation_textBox.Text + "'" +
                ",[MP_SourceOfIncome] = N'" + MP_SourceOfIncome_textBox.Text + "'" +
                ",[MP_MedicalCond] = N'" + MP_MedicalCond_textBox.Text + "'" +
                ",[MP_FinancialLoss] = N'" + MP_FinancialLoss_textBox.Text + "'" +
                ",[MP_SentimentalLoss] = N'" + MP_SentimentalLoss_textBox.Text + "'" +
                ",[MP_HomeState] = " + home_state + " " + 
                ",[MP_ShopState] = " + shop_state + " " +

                ",[MP_State] =  " + status + " " +
                ",[MP_StateReason] = N'" + MP_StatusReason_textBox.Text + "'" + 
                ",[MP_StateComment] = N'" + MP_StatusComment_textBox.Text + "'" +
                ",[MP_Level_ID] = " + (Level_comboBox.SelectedValue != null ? Level_comboBox.SelectedValue : null) + " " +
                " where [MP_ID] = " + MP_ID;
            sc = new SqlCommand(updMP, Program.MyConn);
            sc.ExecuteNonQuery();
        }
        private void Update_MP_Person(int PMP_ID, int M_ID, int P_ID, string type)
        {
            string updMP = "update [dbo].[Person_MicroProject] set " +
                "Person_ID = " + P_ID +
                ",MicroProject_ID = " + M_ID +
                ",PersonType = N'" + type + "' " +
                "\n where PMP_ID = " + PMP_ID + " ";
            sc = new SqlCommand(updMP, Program.MyConn);
            sc.ExecuteNonQuery();
        }
        private void Update_Intermidiary(int IS_ID, string name, string job, string phone, string email, string address, string isPriest)
        {
            string strCmd = "update [dbo].[IntermediarySide] set " +
            "[IS_Name] = N'" + name + "'," +
            "[IS_Job] = N'" + job + "'," +
            "[IS_Phone] = N'" + phone + "'," +
            "[IS_Email] =  N'" + email + "'," +
            "[IS_Address] = N'" + address + "'," +
            "[IS_isPriest] = N'" + isPriest + "'" +
            "\n where [IS_ID] = " + IS_ID + " ";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
        }
        private void Update_Item(int B_ID, int I_ID, string I_name, Double I_price, Double I_Amount, string Local, string comment)
        {
            string str = " update [dbo].[Budget_Item] set " +
                        " [Item_Amount] = " + I_Amount +
                        " \n where [Budget_ID] = " + B_ID + " and [Item_ID] = " + I_ID + " ";
            sc = new SqlCommand(str, Program.MyConn);
            sc.ExecuteNonQuery();

            str = "update [Item] set " +
                  "I_Name = N'" + I_name + "'," +
                  "I_LocalContribution = N'" + Local + "'," +
                  "I_Price = " + I_price + "," +
                  "I_Comment = N'" + comment + "'" +
                  "\n where [I_ID] = " + I_ID + " ";
            sc = new SqlCommand(str, Program.MyConn);
            sc.ExecuteNonQuery();
        }
        private void Update_Activity(int P_ID, int A_ID, string name, string M1, string M2, string M3, string M4)
        {
            string str = "update [Activity] set " +
                  "A_Name = N'" + name + "'," +
                  "A_Month1 = N'" + M1 + "'," +
                  "A_Month2 = N'" + M2 + "'," +
                  "A_Month3 = N'" + M3 + "'," +
                  "A_Month4 = N'" + M4 + "' " +
                  "\n where [A_ID] = " + A_ID + " ";
            sc = new SqlCommand(str, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        #endregion updates

        #region deletes

        private void Delete_MP(int MP_ID)
        {
            string delMP = "delete from [dbo].[MicroProject] where [MP_ID] = " + MP_ID + " ";
            sc = new SqlCommand(delMP, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Delete_Intermidiary(int IS_ID)
        {
            string strCmd = "delete from IntermediarySide where IS_ID = " + IS_ID + " ";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Delete_Item(int I_ID)
        {
            string strCmd = "delete from Item where I_ID = " + I_ID + " ";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Delete_Activity(int A_ID)
        {
            string strCmd = "delete from Activity where A_ID = " + A_ID + " ";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Delete_PersonFromMicroProject(int PMP)
        {
            string strCmd = "delete from [Person_MicroProject] where PMP_ID =" + PMP + " ";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void deleteImage(int image_ID)
        {
            string strCmd = "delete from [dbo].[Image] where [Image_ID] = " + image_ID + " ";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        #endregion deletes

        #region updates button click

        private void UpdateMP_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || MicroProject_ID == -1)
                    throw new Exception("الرجاء اختر المشروع الذي تريد التعديل عليه");
                Update_MP(MicroProject_ID);

                l.Insert_Log("update the project: " + MicroProject_ID, "Micro Project", username, DateTime.Now);

                clear_boxes();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المشروع الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر المشروع الذي تريد التعديل عليه");
                else MessageBox.Show(ex.Message);
            }
        }

        private void UpdateIntermidate_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || MicroProject_ID == -1)
                    throw new Exception("الرجاء اختر المشروع الذي تريد التعديل عليه");
                if (SelectedDataRowM == null || Intermidiary_ID == -1)
                    throw new Exception("الرجاء اختر الهيئة الوسيطة التي تريد التعديل عليها");
                string name, job, email, phone, address;
                name = IntermediaryName_textBox.Text;
                job = IntermediaryJob_textBox.Text;
                email = IntermediaryEmail_textBox.Text;
                phone = IntermediaryPhone_textBox.Text;
                address = IntermediaryAddress_textBox.Text;
                Update_Intermidiary(Intermidiary_ID, name, job, phone, email, address, "NO");
          //      MessageBox.Show("تم تعديل بيانات الهيئة الوسيطة بنجاح");
                //ShowMicroProject_Load(sender, e);
                MicroProject_Intermidiate_bind();

                IntermediaryName_textBox.Text = IntermediaryJob_textBox.Text =
                    IntermediaryEmail_textBox.Text = IntermediaryPhone_textBox.Text = IntermediaryAddress_textBox.Text = "";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المشروع الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر المشروع الذي تريد التعديل عليه");
                else if (ex.Message.Contains("الرجاء اختر الهيئة الوسيطة التي تريد التعديل عليها"))
                    MessageBox.Show("الرجاء اختر الهيئة الوسيطة التي تريد التعديل عليها");
                else MessageBox.Show(ex.Message);
            }
        }

        private void UpdatePriest_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || MicroProject_ID == -1)
                    throw new Exception("الرجاء اختر المشروع الذي تريد التعديل عليه");
                if (SelectedDataRowM == null || Priest_ID == -1)
                    throw new Exception("الرجاء اختر الكاهن المحلي الذي تريد التعديل عليه");

                string name, job, email, phone, address;
                name = PriestName_textBox.Text;
                job = PriestJob_textBox.Text;
                email = PriestEmail_textBox.Text;
                phone = PriestPhone_textBox.Text;
                address = PriestAddress_textBox.Text;
                Update_Intermidiary(Priest_ID, name, job, phone, email, address, "YES");
               // MessageBox.Show("تم تعديل بيانات الكاهن بنجاح");
                //ShowMicroProject_Load(sender, e);
                MicroProject_Intermidiate_bind();

                PriestName_textBox.Text = PriestJob_textBox.Text =
                    PriestEmail_textBox.Text = PriestPhone_textBox.Text = PriestAddress_textBox.Text = "";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المشروع الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر المشروع الذي تريد التعديل عليه");
                else if (ex.Message.Contains("الرجاء اختر الكاهن المحلي الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر الكاهن المحلي الذي تريد التعديل عليه");
                else MessageBox.Show(ex.Message);
            }
        }

        private void UpdateItem_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRowM == null || Budget_ID == -1 || Item_ID == -1)
                    throw new Exception("الرجاء اختر المادة التي تريد التعديل عليها");
                string name, price, Amount, Local, comment;
                name = ItemName_textBox.Text;
                price = ItemPrice_textBox.Text;
                Amount = ItemAmount_textBox.Text;
                Local = ItemLocalContribution_textBox.Text;
                comment = ItemComment_textBox.Text;

                Update_Item(Budget_ID, Item_ID, name, Double.Parse(price), Double.Parse(Amount), Local, comment);
       //         MessageBox.Show("تم تعديل بيانات المادة بنجاح");
                //ShowMicroProject_Load(sender, e);
                Budget_Item_bind();

                ItemName_textBox.Text = ItemPrice_textBox.Text =
                    ItemAmount_textBox.Text = ItemLocalContribution_textBox.Text = ItemComment_textBox.Text = "";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المادة التي تريد التعديل عليها"))
                    MessageBox.Show("الرجاء اختر المادة التي تريد التعديل عليها");
                else MessageBox.Show(ex.Message);
            }
        }

        private void UpdateActivity_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRowM == null || Activity_ID == -1 || Plan_ID == -1)
                    throw new Exception("الرجاء اختر النشاط الذي تريد التعديل عليه");

                string name, m1, m2, m3, m4;
                name = ActivityName_textBox.Text;
                m1 = ActivityM1_textBox.Text;
                m2 = ActivityM2_textBox.Text;
                m3 = ActivityM3_textBox.Text;
                m4 = ActivityM4_textBox.Text;

                Update_Activity(Plan_ID, Activity_ID, name, m1, m2, m3, m4);
             //   MessageBox.Show("تم تعديل بيانات الخطة بنجاح");
                // ShowMicroProject_Load(sender, e);
                Plan_Activity_bind();
                ActivityName_textBox.Text = ActivityM1_textBox.Text =
                    ActivityM2_textBox.Text = ActivityM3_textBox.Text = ActivityM4_textBox.Text = "";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر النشاط الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر النشاط الذي تريد التعديل عليه");
                else MessageBox.Show(ex.Message);
            }
        }

        private void UpdatePersonProject_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (PMP_ID == -1)
                    throw new Exception("الرجاء اختر المشروع الذي تريد التعديل عليه");
                if (Person_ID == -1 || MicroProject_ID == -1)
                    throw new Exception("الرجاء اختر المستفيد الذي تريد التعديل عليه");
                int newPersonId = Int32.Parse(Person_ID_textBox.Text);
                string type = (MP_Owner_radioButton.Checked ? "مستفيد" : "شريك");
                Update_MP_Person(PMP_ID, MicroProject_ID, newPersonId, type);
             //   MessageBox.Show("تم تعديل بيانات المستفيد بنجاح");
                //ShowMicroProject_Load(sender, e);
                Person_MicroProject_bind();

                Person_ID_textBox.Text = PersonName_textBox.Text = "";
                MP_Owner_radioButton.Checked = MP_Partner_radioButton.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المشروع الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر المشروع الذي تريد التعديل عليه");
                else if (ex.Message.Contains("الرجاء اختر المستفيد الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر المستفيد الذي تريد التعديل عليه");
                else MessageBox.Show(ex.Message);
            }
        }

        #endregion updates button click

        #region deletes button click

        private void DeleteMP_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || MicroProject_ID == -1)
                    throw new Exception("الرجاء اختر المشروع الذي تريد التعديل عليه");

                Delete_MP(MicroProject_ID);

                l.Insert_Log("delete the project: " + MicroProject_ID, "Micro Project", username, DateTime.Now);

            //    MicroProject_Section_Load(sender, e);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المشروع الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر المشروع الذي تريد التعديل عليه");
                else MessageBox.Show(ex.Message);
            }
        }

        private void DeleteIntermidate_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || MicroProject_ID == -1)
                    throw new Exception("الرجاء اختر المشروع الذي تريد التعديل عليه");
                if (SelectedDataRowM == null || Intermidiary_ID == -1)
                    throw new Exception("الرجاء اختر الهيئة الوسيطة التي تريد التعديل عليها");

                Delete_Intermidiary(Intermidiary_ID);
             //   MessageBox.Show("تم حذف الهيئة الوسيطة بنجاح");
                //ShowMicroProject_Load(sender, e);

                MicroProject_Intermidiate_bind();
                IntermediaryName_textBox.Text = IntermediaryJob_textBox.Text =
                    IntermediaryEmail_textBox.Text = IntermediaryPhone_textBox.Text = IntermediaryAddress_textBox.Text = "";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المشروع الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر المشروع الذي تريد التعديل عليه");
                else if (ex.Message.Contains("الرجاء اختر الهيئة الوسيطة التي تريد التعديل عليها"))
                    MessageBox.Show("الرجاء اختر الهيئة الوسيطة التي تريد التعديل عليها");
                else MessageBox.Show(ex.Message);
            }
        }

        private void DeletePriest_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || MicroProject_ID == -1)
                    throw new Exception("الرجاء اختر المشروع الذي تريد التعديل عليه");
                if (SelectedDataRowM == null || Priest_ID == -1)
                    throw new Exception("الرجاء اختر الكاهن التي تريد التعديل عليها");

                Delete_Intermidiary(Priest_ID);
                //MessageBox.Show("تم حذف الهيئة الوسيطة بنجاح");
                //ShowMicroProject_Load(sender, e);

                MicroProject_Intermidiate_bind();
                PriestName_textBox.Text = PriestJob_textBox.Text =
                    PriestEmail_textBox.Text = PriestPhone_textBox.Text = PriestAddress_textBox.Text = "";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المشروع الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر المشروع الذي تريد التعديل عليه");
                else if (ex.Message.Contains("الرجاء اختر الهيئة الوسيطة التي تريد التعديل عليها"))
                    MessageBox.Show("الرجاء اختر الهيئة الوسيطة التي تريد التعديل عليها");
                else MessageBox.Show(ex.Message);
            }
        
        }

        private void DeleteActivity_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRowM == null || Activity_ID == -1 || Plan_ID == -1)
                    throw new Exception("الرجاء اختر النشاط الذي تريد التعديل عليه");

                Delete_Activity(Activity_ID);
            //    MessageBox.Show("تم حذف النشاط بنجاح");
                //   ShowMicroProject_Load(sender, e);
                Plan_Activity_bind();
                ActivityName_textBox.Text = ActivityM1_textBox.Text =
                    ActivityM2_textBox.Text = ActivityM3_textBox.Text = ActivityM4_textBox.Text = "";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر النشاط الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر النشاط الذي تريد التعديل عليه");
                else MessageBox.Show(ex.Message);
            }
        }

        private void DeleteItem_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRowM == null || Item_ID == -1 || Budget_ID == -1)
                    throw new Exception("الرجاء اختر المادة الذي تريد التعديل عليها");

                Delete_Item(Item_ID);
           //     MessageBox.Show("تم حذف المادة بنجاح");
                //   ShowMicroProject_Load(sender, e);
                Budget_Item_bind();

                ItemName_textBox.Text = ItemAmount_textBox.Text = ItemPrice_textBox.Text =
                    ItemLocalContribution_textBox.Text = ItemComment_textBox.Text = "";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المادة الذي تريد التعديل عليها"))
                    MessageBox.Show("الرجاء اختر المادة الذي تريد التعديل عليها");
                else MessageBox.Show(ex.Message);
            }
        }

        private void DeletePersonProject_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID == -1 || Person_ID == -1)
                    throw new Exception("الرجاء اختر المستفيد الذي تريد التعديل عليه");

                Delete_PersonFromMicroProject(PMP_ID);
           //     MessageBox.Show("تم حذف ربط هذا المستفيد بهذا المشروع بنجاح");

                Person_MicroProject_bind();

                Person_ID_textBox.Text = PersonName_textBox.Text = "";
                MP_Owner_radioButton.Checked = MP_Partner_radioButton.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المستفيد الذي تريد التعديل عليه"))
                    MessageBox.Show("الرجاء اختر المستفيد الذي تريد التعديل عليه");
                else MessageBox.Show(ex.Message);
            }
        }

        #endregion deletes button click

        #region inserts button click

        private void InsertMP_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (MP_Country_textBox.Text == "" || MP_Parish_textBox.Text == "" || MP_City_textBox.Text == "" || MP_Name_textBox.Text == "" ||
                        MP_AllPriceNeeded_textBox.Text == "" || MP_PeriodOfExecution_textBox.Text == "" || MP_Describtion_textBox.Text == "" ||
                        MP_ResonOfProject_textBox.Text == "" || MP_PlaceOfExecution_textBox.Text == "" || MPID_textBox.Text == "" ||
                        MP_ResonOfPlace_textBox.Text == "")
                {
                    throw new Exception("لا يمكنك ترك بعض الحقول فارغة");
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
                        string selMPMaxIDQuery = "select IDENT_CURRENT('MicroProject')";
                        sc = new SqlCommand(selMPMaxIDQuery, Program.MyConn);
                        Int32.TryParse((sc.ExecuteScalar()).ToString(), out MicroProject_ID);
                    }

                    l.Insert_Log("insert the project: " + MicroProject_ID, "Micro Project", username, DateTime.Now);

                    clear_boxes();
                    // MicroProject_bind("", "");
                    microProjectName_textBox5.Text = microProjectName_textBox4.Text = microProjectName_textBox3.Text
                        = microProjectName_textBox2.Text = microProjectName_textBox1.Text = MP_Name_textBox.Text;
                    microProjectID_textBox1.Text = microProjectID_textBox2.Text = microProjectID_textBox3.Text
                        = microProjectID_textBox4.Text = microProjectID_textBox5.Text = MicroProject_ID.ToString();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("لا يمكنك ترك بعض الحقول فارغة"))
                    MessageBox.Show("لا يمكنك ترك بعض الحقول فارغة");
                else MessageBox.Show(ex.Message);
            }
        }

        private void InsertIntermidate_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID == -1)
                    throw new Exception("الرجاء اختر المشروع الذي تريد الإضافة عليه");

                if (IntermediaryName_textBox.Text != "")
                {
                    insertIntermediarySide(false, MicroProject_ID);
                 //   MessageBox.Show("تم إضافة الهيئة الوسيطة المشروع بنجاح");
                    //ShowMicroProject_Load(sender, e);
                    MicroProject_Intermidiate_bind();

                    IntermediaryName_textBox.Text = IntermediaryJob_textBox.Text =
                        IntermediaryEmail_textBox.Text = IntermediaryPhone_textBox.Text = IntermediaryAddress_textBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المشروع الذي تريد الإضافة عليه"))
                    MessageBox.Show("الرجاء اختر المشروع الذي تريد الإضافة عليه");
                else MessageBox.Show(ex.Message);
            }
        }

        private void InsertPriest_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID == -1)
                    throw new Exception("الرجاء اختر المشروع الذي تريد الإضافة عليه");

                if (PriestName_textBox.Text != "")
                {
                    insertIntermediarySide(true, MicroProject_ID);
            //        MessageBox.Show("تم إضافة الكاهن المحلي المشروع بنجاح");
                    //ShowMicroProject_Load(sender, e);
                    MicroProject_Intermidiate_bind();

                    PriestName_textBox.Text = PriestJob_textBox.Text =
                        PriestEmail_textBox.Text = PriestPhone_textBox.Text = PriestAddress_textBox.Text = "";

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المشروع الذي تريد الإضافة عليه"))
                    MessageBox.Show("الرجاء اختر المشروع الذي تريد الإضافة عليه");
                else MessageBox.Show(ex.Message);
            }
        }

        private void InsertActivity_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Display
                string AName, AM1, AM2, AM3, AM4;
                AName = AM1 = AM2 = AM3 = AM4 = "لا يوجد";
                //GetCurrentActivityId();
                if (ActivityName_textBox.Text != "") AName = ActivityName_textBox.Text;
                if (ActivityM1_textBox.Text != "") AM1 = ActivityM1_textBox.Text;
                if (ActivityM2_textBox.Text != "") AM2 = ActivityM2_textBox.Text;
                if (ActivityM3_textBox.Text != "") AM3 = ActivityM3_textBox.Text;
                if (ActivityM4_textBox.Text != "") AM4 = ActivityM4_textBox.Text;
                //string[] row = { Activity_ID.ToString(), AName, AM1, AM2, AM3, AM4 };
                ////Activity_dataGridView.Rows.Add(Arow);
                //table.Rows.Add(row);
                //MP_Activity_dataGridView.DataSource = table;

                insertActivity(AName, AM1, AM2, AM3, AM4);          //insert activity
                GetCurrentActivityId();
                insertActivityPlan(Activity_ID, Plan_ID);
                //Activity_ID++;

                //// Save in file
                //fs = new FileStream(A_path_name, FileMode.Append);
                //file_sw = new StreamWriter(fs);
                //file_add = Activity_ID.ToString() + "\t" + AName + "\t" + AM1 + "\t" + AM2 + "\t" + AM3 + "\t" + AM4 + "*";
                //file_add = file_add.Replace("*", Environment.NewLine);
                //file_sw.Write(file_add);
                //file_sw.Close();

                ActivityName_textBox.Text = ActivityM1_textBox.Text = ActivityM2_textBox.Text = ActivityM3_textBox.Text = ActivityM4_textBox.Text = "";
                Plan_Activity_bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddItem_button_Click(object sender, EventArgs e)
        {
            try
            {

                if (ItemName_textBox.Text == "" || ItemAmount_textBox.Text == "" || ItemLocalContribution_textBox.Text == "" || ItemPrice_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                Double itemOverAllsyr = 0;
                Double itemOverAlleuro = 0;

                Double realAmount = Double.Parse(ItemAmount_textBox.Text) - Double.Parse(ItemLocalContribution_textBox.Text);
                string I_Comment_string = "..";

                // Display
                //string[] Arow = { ItemName_textBox.Text, ItemAmount_textBox.Text, ItemLocalContribution_textBox.Text, ItemPrice_textBox.Text };
                //table1.Rows.Add(Arow);
                //MP_Item_dataGridView.DataSource = table;

                //ItemName_label.Text += ItemName_textBox.Text + "\n";
                //ItemAmount_label.Text += realAmount.ToString() + "\n";
                //ItemPrice_label.Text += ItemPrice_textBox.Text + "\n";

                itemOverAllsyr = realAmount * Double.Parse(ItemPrice_textBox.Text);

                //save the overAll for each item
                sumOfItemsPrice_SYR += itemOverAllsyr;

                //ItemOverallSyrian_label.Text += itemOverAllsyr + "\n";
                itemOverAlleuro = itemOverAllsyr / 500.00;
                //ItemOverallEuro_label.Text += itemOverAlleuro + "\n";

                OverallSyrian_label.Text = sumOfItemsPrice_SYR.ToString();
                OverallEuro_label.Text = (sumOfItemsPrice_SYR / 500.00).ToString();

                if (ItemComment_textBox.Text != "")
                    I_Comment_string = ItemComment_textBox.Text;

                insertItem(ItemName_textBox.Text, ItemLocalContribution_textBox.Text, Double.Parse(ItemPrice_textBox.Text.ToString()), I_Comment_string);
                GetCurrentItemId();
                GetCurrentBudgetId();
                insertBudgetItem(Budget_ID, Item_ID, Double.Parse(ItemAmount_textBox.Text.ToString()));

                Budget_Item_bind();

                ItemName_textBox.Text = ItemAmount_textBox.Text = ItemPrice_textBox.Text =
                    ItemLocalContribution_textBox.Text = ItemComment_textBox.Text = "";
            }
            catch (NoNullAllowedException)
            {
               MessageBox.Show("لا يمكن ترك بعض الحقول فارغة");
            }
        }

        private void InsertPersonProject_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Person_ID == -1)
                {
                    throw new Exception("الرجاء اختر المستفيد من المشروع");
                }
                if (MicroProject_ID == -1)
                    throw new Exception("الرجاء اختر المشروع الذي تريد الإضافة عليه");
                if (MP_Owner_radioButton.Checked)
                {
                    insertPerson_MicroProject(Person_ID, MicroProject_ID, "مستفيد");
                }
                else
                    insertPerson_MicroProject(Person_ID, MicroProject_ID, "شريك");
                //ShowMicroProject_Load(sender, e);
                Person_MicroProject_bind();

                Person_ID_textBox.Text = PersonName_textBox.Text = "";
                MP_Owner_radioButton.Checked = MP_Partner_radioButton.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرجاء اختر المستفيد من المشروع"))
                    MessageBox.Show("الرجاء اختر المستفيد من المشروع");
                else if (ex.Message.Contains("الرجاء اختر المشروع الذي تريد الإضافة عليه"))
                    MessageBox.Show("الرجاء اختر المشروع الذي تريد الإضافة عليه");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        #endregion inserts button click

        #region datagridviews double click

        private void MP_Intermidiary_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowM = ((DataRowView)MP_Intermidiary_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowM != null)
            {
                Intermidiary_ID = Int32.Parse(SelectedDataRowM["رقم الوسيط"].ToString());
                IntermediaryName_textBox.Text = SelectedDataRowM["اسم الوسيط"].ToString();
                IntermediaryJob_textBox.Text = SelectedDataRowM["العمل"].ToString();
                IntermediaryPhone_textBox.Text = SelectedDataRowM["الهاتف"].ToString();
                IntermediaryEmail_textBox.Text = SelectedDataRowM["الإيميل"].ToString();
                IntermediaryAddress_textBox.Text = SelectedDataRowM["العنوان"].ToString();
            }
        }

        private void MP_Priest_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowM = ((DataRowView)MP_Priest_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowM != null)
            {
                Priest_ID = Int32.Parse(SelectedDataRowM["رقم الكاهن"].ToString());
                PriestName_textBox.Text = SelectedDataRowM["اسم الكاهن"].ToString();
                PriestJob_textBox.Text = SelectedDataRowM["العمل"].ToString();
                PriestPhone_textBox.Text = SelectedDataRowM["الهاتف"].ToString();
                PriestEmail_textBox.Text = SelectedDataRowM["الإيميل"].ToString();
                PriestAddress_textBox.Text = SelectedDataRowM["العنوان"].ToString();
            }
        }

        private void MP_Budget_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowM = ((DataRowView)MP_Item_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowM != null)
            {
                Budget_ID = Int32.Parse(SelectedDataRowM["رقم الفاتورة"].ToString());
                Item_ID = Int32.Parse(SelectedDataRowM["رقم المادة"].ToString());
                ItemName_textBox.Text = SelectedDataRowM["اسم المادة"].ToString();
                ItemPrice_textBox.Text = SelectedDataRowM["السعر"].ToString();
                ItemAmount_textBox.Text = SelectedDataRowM["الكمية"].ToString();
                ItemLocalContribution_textBox.Text = SelectedDataRowM["المساهمة المحلية"].ToString();
                ItemComment_textBox.Text = SelectedDataRowM["معلومات أخرى"].ToString();
            }
        }

        private void MP_Plan_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowM = ((DataRowView)MP_Activity_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowM != null)
            {
                UpdateOrDeletePlan_radioButton.Checked = true;
                Activity_ID = Int32.Parse(SelectedDataRowM["رقم النشاط"].ToString());
                Plan_ID = Int32.Parse(SelectedDataRowM["رقم الخطة"].ToString());
                ActivityName_textBox.Text = SelectedDataRowM["اسم النشاط"].ToString();
                ActivityM1_textBox.Text = SelectedDataRowM["الشهر1"].ToString();
                ActivityM2_textBox.Text = SelectedDataRowM["الشهر2"].ToString();
                ActivityM3_textBox.Text = SelectedDataRowM["الشهر3"].ToString();
                ActivityM4_textBox.Text = SelectedDataRowM["الشهر4"].ToString();
            }
        }

        private void PersonDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowM = ((DataRowView)PersonDataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowM != null)
            {
                PMP_ID = Int32.Parse(SelectedDataRowM["رقم السجل"].ToString());
                Person_ID = Int32.Parse(SelectedDataRowM["رقم المستفيد"].ToString());
                MicroProject_ID = Int32.Parse(SelectedDataRowM["رقم المشروع"].ToString());
                Person_ID_textBox.Text = Person_ID.ToString();
                PersonName_textBox.Text = SelectedDataRowM["اسم المستفيد"].ToString();

                string Type = (string)SelectedDataRowM["حالة المستفيد"];
                if (Type.Contains(@"م"))
                    MP_Owner_radioButton.Checked = true;
                else
                    MP_Partner_radioButton.Checked = true;
            }
        }

        #endregion datagridviews double click

        #region get IDs

        private void GetCurrentProjectPlanId()
        {
            sc = new SqlCommand("select IDENT_CURRENT('ProjectPlan')", Program.MyConn);
            Int32.TryParse((sc.ExecuteScalar()).ToString(), out Plan_ID);
        }

        private void GetCurrentActivityId()
        {
            sc = new SqlCommand("select IDENT_CURRENT('Activity')", Program.MyConn);
            Int32.TryParse(sc.ExecuteScalar().ToString(), out Activity_ID);
        }

        private void GetCurrentItemId()
        {
            sc = new SqlCommand("select IDENT_CURRENT('Item')", Program.MyConn);
            Int32.TryParse(sc.ExecuteScalar().ToString(), out Item_ID);
        }

        private void GetCurrentBudgetId()
        {
            string selBudgetMaxIDQuery = "select IDENT_CURRENT('Budget')";
            sc = new SqlCommand(selBudgetMaxIDQuery, Program.MyConn);
            Int32.TryParse((sc.ExecuteScalar()).ToString(), out Budget_ID);
        }

        #endregion get IDs

        #region tabcontrol buttons

        private void ProjectDetails_button_Click(object sender, EventArgs e)
        {
            MProjectSection_tabControl.Visible = true;
            MProjectSection_tabControl.SelectedIndex = 0;
        }

        private void Beneficiaries_button_Click(object sender, EventArgs e)
        {
            MProjectSection_tabControl.Visible = true;
            MProjectSection_tabControl.SelectedIndex = 1;
        }

        private void Intermediate_button_Click(object sender, EventArgs e)
        {
            MProjectSection_tabControl.Visible = true;
            MProjectSection_tabControl.SelectedIndex = 2;
        }

        private void LocalPriest_button_Click(object sender, EventArgs e)
        {
            MProjectSection_tabControl.Visible = true;
            MProjectSection_tabControl.SelectedIndex = 3;
        }

        private void WorkPlan_button_Click(object sender, EventArgs e)
        {
            MProjectSection_tabControl.Visible = true;
            MProjectSection_tabControl.SelectedIndex = 4;
        }

        private void Budget_button_Click(object sender, EventArgs e)
        {
            MProjectSection_tabControl.Visible = true;
            MProjectSection_tabControl.SelectedIndex = 5;
        }

        private void Attachments_button_Click(object sender, EventArgs e)
        {
            MProjectSection_tabControl.Visible = true;
            MProjectSection_tabControl.SelectedIndex = 6;
        }

        #endregion tabcontrol buttons

        private void MicroProject_Section_Load(object sender, EventArgs e)
        {
            l = new Log();
            sumOfItemsPrice_SYR = 0;

            if (pageState == 2)
            {
                clear_boxes();

                string selMPCountQuery = "select IDENT_CURRENT('[dbo].[MicroProject]')";
                sc = new SqlCommand(selMPCountQuery, Program.MyConn);
                int n = 0;
                Int32.TryParse((sc.ExecuteScalar()).ToString(), out n);
                MPID_textBox.Text = (++n).ToString();
                MPID_textBox.Enabled = false;
            }
            else fillBoxes();
            MicroProject_Intermidiate_bind();
            Budget_Item_bind();
            Plan_Activity_bind();
            Person_MicroProject_bind();
            Level_bind();
            Image_bind(MicroProject_ID.ToString());
        }

        private void SelectPerson_button_Click(object sender, EventArgs e)
        {
            try
            {
                AllBeneficiaries AllBeneficiaries = new AllBeneficiaries();
                SelectedDataRowM = AllBeneficiaries.showSelectedPersonRow();
                if (SelectedDataRowM != null)
                {
                    Person_ID = (int)SelectedDataRowM["رقم الشخص"];
                    Person_ID_textBox.Text = Person_ID.ToString();
                    PersonName_textBox.Text = (string)SelectedDataRowM["الاسم"] + " " + (string)SelectedDataRowM["اسم الأب"] + " " + (string)SelectedDataRowM["الكنية"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Person_ID = -1;
            }
            finally
            {
                PersonSection = null;
                SelectedDataRowM = null;
            }
        }
        private void MP_IdentityInsert_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MP_IdentityInsert_checkBox.Checked)
                MPID_textBox.Enabled = true;
            else
                MPID_textBox.Enabled = false;
        }

        private void AddNewPlan_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (AddNewPlan_radioButton.Checked)
                {
                    //Plan inserting Part
                    if (SelectedDataRow == null || MicroProject_ID == -1)
                        throw new Exception("الرجاء اختر المشروع الذي تريد الإضافة عليه");
                    insertProjectPlan(MicroProject_ID);
                    GetCurrentProjectPlanId();
                }
                else
                {
                    Plan_Activity_bind();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddNewBudget_radioButton_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (AddNewBudget_radioButton.Checked)
                {
                    //Budget inserting Part
                    if (SelectedDataRow == null || MicroProject_ID == -1)
                        throw new Exception("الرجاء اختر المشروع الذي تريد الإضافة عليه");
                    insertBudget();
                    GetCurrentBudgetId();
                }
                else
                {
                    Budget_Item_bind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear_boxes()
        {

            Person_ID = Item_ID = Budget_ID = Activity_ID = Plan_ID = PMP_ID = Intermidiary_ID = Priest_ID = -1;
            MP_Country_textBox.Text = "سوريا";
            MP_City_textBox.Text = "حلب";
            MP_Parish_textBox.Text = MP_Name_textBox.Text = MP_AllPriceNeeded_textBox.Text
                = MP_PeriodOfExecution_textBox.Text = MP_Describtion_textBox.Text = MP_ResonOfProject_textBox.Text = MP_SufferanceOfPerson_textBox.Text
                = MP_LicenseSide_textBox.Text = MP_PlaceOfExecution_textBox.Text = MP_ResonOfPlace_textBox.Text
             //   = MP_DirectPayee_textBox.Text = MP_DirectPayeeNum_textBox.Text = MP_DurationGuarantee_textBox.Text 
                = MP_StatusReason_textBox.Text = MP_StatusComment_textBox.Text
                = MP_SourceOfIncome_textBox.Text = MP_MedicalCond_textBox.Text = MP_FinancialLoss_textBox.Text = MP_SentimentalLoss_textBox.Text 
                = MP_SimpleProfit_textBox.Text = MP_OtherInformation_textBox.Text = "";
            PriestName_textBox.Text = PriestPhone_textBox.Text = PriestJob_textBox.Text = PriestEmail_textBox.Text = PriestAddress_textBox.Text = "";
            IntermediaryName_textBox.Text = IntermediaryPhone_textBox.Text = IntermediaryJob_textBox.Text =
                IntermediaryEmail_textBox.Text = IntermediaryAddress_textBox.Text = "";
            PersonName_textBox.Text = "";
            MP_DateOfRequest_dateTimePicker.Value = DateTime.Now;
            MP_IdentityInsert_checkBox.Checked = false;
            MP_Accepted_radioButton.Checked = MP_NotAccepted_radioButton.Checked = MP_Delayed_radioButton.Checked = false;
            ItemName_textBox.Text = ItemAmount_textBox.Text = ItemPrice_textBox.Text = ItemLocalContribution_textBox.Text = ItemComment_textBox.Text = "";
            OverallSyrian_label.Text = OverallEuro_label.Text = "";
            ActivityM1_textBox.Text = ActivityM2_textBox.Text = ActivityM3_textBox.Text = ActivityM4_textBox.Text = ActivityName_textBox.Text = "";
            PersonName_textBox.Text = Person_ID_textBox.Text = "";
        }

        private Thread myTh;

        private string image_type;
        private int File_ID;
        private string full_file_name, file_extention, file_name_without_extention, file_name, destinationFolderPath;
        private OpenFileDialog open;
        private byte[] arr;

        private void ImageOpen_button_Click(object sender, EventArgs e)
        {
            if (Files_radioButton.Checked)
                myTh = new Thread(new ThreadStart(CallBrowseFileDialog));
            else
                myTh = new Thread(new ThreadStart(CallDialog));
            myTh.SetApartmentState(ApartmentState.STA);
            myTh.Start();
            myTh.Join();
            ImageLocation_textBox.Text = full_file_name;
            //pictureBox1.Image = Image.FromFile(full_image_name);
        }

        private void CallDialog()
        {
            open = new OpenFileDialog();
            open.Filter = "jpeg|*.jpg|bmp|*.bmp|allfiles|*.*";
            DialogResult res = open.ShowDialog();
            if (res == DialogResult.OK)
            {
                //PersonPicture_pictureBox.Image = Image.FromFile(open.FileName);
                full_file_name = open.FileName;
                pictureBox1.ImageLocation = full_file_name;
            }
        }

        private void CallBrowseFileDialog()
        {
            open = new OpenFileDialog();
            open.Filter = @"All Files|*.txt;*.docx;*.pdf;*.xls;*.xlsx|Text File (.txt)|*.txt|Word File (.docx,.doc)|*.docx;*.doc
                                |PDF (.pdf)|*.pdf|Spreadsheet (.xls,.xlsx)| *.xls;*.xlsx";
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (open.CheckFileExists)
                {
                    sc = new SqlCommand("select IDENT_CURRENT('File')", Program.MyConn);
                    Int32.TryParse((sc.ExecuteScalar()).ToString(), out File_ID);
                    File_ID++;
                    full_file_name = open.FileName;
                    file_extention = Path.GetExtension(full_file_name);
                    file_name_without_extention = Path.GetFileNameWithoutExtension(full_file_name);
                    file_name = Path.GetFileName(full_file_name);
                    destinationFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
            }
        }

        private void Priest_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Priest_radioButton.Checked)
            { image_type = "Priest"; }
            else if (Files_radioButton.Checked)
            { image_type = "Files"; }
            else if (EconomicFeasibility_radioButton.Checked)
            { image_type = "EconomicFeasibility"; }
        }

        private void Convert_Picture()
        {
            arr = null;
            FileStream fs = new FileStream(full_file_name, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            arr = br.ReadBytes((int)fs.Length);
        }

        private void FileSave_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID == -1 || MicroProjectID_textBox.Text == "")
                {
                    throw new Exception("الرجاء اختر المشروع التي تريد الإضافة عليه");
                }
                if (Files_radioButton.Checked)
                {
                    File.Copy(open.FileName, destinationFolderPath + "\\Micro_Project_File " + File_ID + file_extention);

                    byte[] file_content = File.ReadAllBytes(full_file_name);
                    string insFQuery = "insert into [dbo].[File] (File_Name,File_Content,MicroProject_ID) values(@file_name,@file_content,@MicroProject_ID)";
                    sc = new SqlCommand(insFQuery, Program.MyConn);
                    SqlParameter param = sc.Parameters.Add("@file_name", SqlDbType.NVarChar);
                    param.Value = file_name;
                    param = sc.Parameters.Add("@file_content", SqlDbType.VarBinary);
                    param.Value = file_content;
                    param = sc.Parameters.Add("@MicroProject_ID", SqlDbType.Int);
                    param.Value = MicroProject_ID;
                    sc.ExecuteNonQuery();
                 //   MessageBox.Show("تم حفظ الملف بنجاح");
                    l.Insert_Log("Insert a file to the project :" + MicroProjectID_textBox.Text, "Files", username, DateTime.Now);

                    ImageLocation_textBox.Clear();
                    Image_bind(MicroProject_ID.ToString());
                }
                else
                {
                    if (pictureBox1.Image == null)
                    {
                        throw new Exception("الرجاء اختر الصورة التي تريد إضافتها");
                    }
                    Convert_Picture();
                    if (image_type == "")
                    {
                        throw new Exception("الرجاء اختر نوع الصورة التي تريد إضافتها");
                    }
                    insertImage(MicroProject_ID, full_file_name, arr, image_type);
                    // MessageBox.Show("تم الإرفاق بنجاح");

                    l.Insert_Log("insert image to: " + MicroProject_ID + " ", "Images", username, DateTime.Now);

                    ImageLocation_textBox.Text = "";
                    pictureBox1.Image = null;
                    Image_bind(MicroProject_ID.ToString());
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("الرجاء اختر الصورة التي تريد إضافتها"))
                    MessageBox.Show("الرجاء اختر الصورة التي تريد إضافتها");
                else if (ex.Message.Equals("الرجاء اختر نوع الصورة التي تريد إضافتها"))
                    MessageBox.Show("الرجاء اختر نوع الصورة التي تريد إضافتها");
                else MessageBox.Show(ex.Message);
            }
        }

        private void FileDelete_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (image_ID == -1 || SelectedDataRow_P == null)
                {
                    throw new Exception("الرجاء اختر الصورة التي تريد حذفها");
                }
                deleteImage(image_ID);
                //   MessageBox.Show("تم حذف الصورة بنجاح");
                l.Insert_Log("Delete Image of : " + MicroProject_ID + " ", "Image", username, DateTime.Now);

                Image_bind(MicroProject_ID.ToString());
                ImageLocation_textBox.Text = "";
                pictureBox1.Image = null;
                image_ID = -1;
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("الرجاء اختر الصورة التي تريد حذفها"))
                    MessageBox.Show("الرجاء اختر الصورة التي تريد حذفها");
                else MessageBox.Show(ex.Message);
            }
        }

        private DataRow SelectedDataRow_P;
        private SqlDataReader reader1;

        private void Images_dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow_P = ((DataRowView)Images_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow_P != null)
            {
                image_ID = Int32.Parse(SelectedDataRow_P["رقم الصورة"].ToString());
                arr = null;
                string strCmd = "select [Image_Content] from [dbo].[Image] where Image_ID = " + image_ID + " ";
                sc = new SqlCommand(strCmd, Program.MyConn);
                ////sc1.ExecuteNonQuery();

                reader1 = sc.ExecuteReader();
                reader1.Read();
                if (reader1.HasRows)
                {
                    arr = (byte[])(reader1[0]);
                    reader1.Close();

                    if (arr == null || arr.Length == 0)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(arr);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    MessageBox.Show("data not available");
                    reader1.Close();
                }
                ImageLocation_textBox.Text = (string)SelectedDataRow_P["مسار الصورة"];
                image_type = (string)SelectedDataRow_P["نوع الصورة"];
                if (Priest_radioButton.Checked)
                { image_type = "Priest"; }
                else if (Files_radioButton.Checked)
                { image_type = "Files"; }
                else if (EconomicFeasibility_radioButton.Checked)
                { image_type = "EconomicFeasibility"; }
                switch (image_type)
                {
                    case "Priest": Priest_radioButton.Checked = true;
                        break;

                    case "Files": Files_radioButton.Checked = true;
                        break;

                    case "EconomicFeasibility": EconomicFeasibility_radioButton.Checked = true;
                        break;

                    default: Priest_radioButton.Checked = Files_radioButton.Checked = EconomicFeasibility_radioButton.Checked = false;
                        break;
                }
                MicroProject_ID = (int)SelectedDataRow_P["رقم المشروع"];
            }
        }

        #region change btn backgrounds

        private void ProjectDetails_button_MouseEnter(object sender, EventArgs e)
        {
            ProjectDetails_button.BackgroundImage = Properties.Resources.m_btn_03;
        }

        private void ProjectDetails_button_MouseLeave(object sender, EventArgs e)
        {
            ProjectDetails_button.BackgroundImage = null;
        }

        private void Beneficiaries_button_MouseEnter(object sender, EventArgs e)
        {
            Beneficiaries_button.BackgroundImage = Properties.Resources.m_btn_04;
        }

        private void Beneficiaries_button_MouseLeave(object sender, EventArgs e)
        {
            Beneficiaries_button.BackgroundImage = null;
        }

        private void Intermediate_button_MouseEnter(object sender, EventArgs e)
        {
            Intermediate_button.BackgroundImage = Properties.Resources.m_btn_05;
        }

        private void Intermediate_button_MouseLeave(object sender, EventArgs e)
        {
            Intermediate_button.BackgroundImage = null;
        }

        private void LocalPriest_button_MouseEnter(object sender, EventArgs e)
        {
            LocalPriest_button.BackgroundImage = Properties.Resources.m_btn_06;
        }

        private void LocalPriest_button_MouseLeave(object sender, EventArgs e)
        {
            LocalPriest_button.BackgroundImage = null;
        }

        private void WorkPlan_button_MouseEnter(object sender, EventArgs e)
        {
            WorkPlan_button.BackgroundImage = Properties.Resources.m_btn_07;
        }

        private void WorkPlan_button_MouseLeave(object sender, EventArgs e)
        {
            WorkPlan_button.BackgroundImage = null;
        }

        private void Budget_button_MouseEnter(object sender, EventArgs e)
        {
            Budget_button.BackgroundImage = Properties.Resources.m_btn_08;
        }

        private void Budget_button_MouseLeave(object sender, EventArgs e)
        {
            Budget_button.BackgroundImage = null;
        }

        private void Attachments_button_MouseEnter(object sender, EventArgs e)
        {
            Attachments_button.BackgroundImage = Properties.Resources.m_btn_09;
        }

        private void Attachments_button_MouseLeave(object sender, EventArgs e)
        {
            Attachments_button.BackgroundImage = null;
        }

        private void AddPlace_button_MouseEnter(object sender, EventArgs e)
        {
            AddLevel_button.BackgroundImage = Properties.Resources.plus;
        }

        private void AddPlace_button_MouseLeave(object sender, EventArgs e)
        {
            AddLevel_button.BackgroundImage = Properties.Resources.plus00;
        }

        #endregion change btn backgrounds

        #region save - delete

        private void AddSave_button_MouseEnter(object sender, EventArgs e)
        {
            InsertMP_button.BackgroundImage = FileSave_button.BackgroundImage = Properties.Resources.save;
        }

        private void AddSave_button_MouseLeave(object sender, EventArgs e)
        {
            InsertMP_button.BackgroundImage = FileSave_button.BackgroundImage = Properties.Resources.save0;
        }

        private void UpdateSave_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateMP_button.BackgroundImage = Properties.Resources.save;
        }

        private void UpdateSave_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateMP_button.BackgroundImage = Properties.Resources.save0;
        }

        private void delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeletePersonProject_button.BackgroundImage = DeleteIntermidate_button.BackgroundImage
                = DeletePriest_button.BackgroundImage = DeleteActivity_button.BackgroundImage = DeleteItem_button.BackgroundImage =
                FileDelete_button.BackgroundImage = Properties.Resources.delete;
        }

        private void delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeletePersonProject_button.BackgroundImage = DeleteIntermidate_button.BackgroundImage
                = DeletePriest_button.BackgroundImage = DeleteActivity_button.BackgroundImage = DeleteItem_button.BackgroundImage =
                FileDelete_button.BackgroundImage = Properties.Resources.delete0;
        }

        private void add_button_MouseEnter(object sender, EventArgs e)
        {
            InsertPersonProject_button.BackgroundImage = InsertIntermidate_button.BackgroundImage
                = InsertPriest_button.BackgroundImage = InsertActivity_button.BackgroundImage = AddItem_button.BackgroundImage =
             Properties.Resources.add;
        }
        private void add_button_MouseLeave(object sender, EventArgs e)
        {
            InsertPersonProject_button.BackgroundImage = InsertIntermidate_button.BackgroundImage
                = InsertPriest_button.BackgroundImage = InsertActivity_button.BackgroundImage = AddItem_button.BackgroundImage =
            Properties.Resources.add0;
        }
        private void update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdatePersonProject_button.BackgroundImage = UpdateIntermidate_button.BackgroundImage
                = UpdatePriest_button.BackgroundImage = UpdateActivity_button.BackgroundImage = UpdateItem_button.BackgroundImage
            = Properties.Resources.update;
        }

        private void update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdatePersonProject_button.BackgroundImage = UpdateIntermidate_button.BackgroundImage
                = UpdatePriest_button.BackgroundImage = UpdateActivity_button.BackgroundImage = UpdateItem_button.BackgroundImage
            = Properties.Resources.update0;
        }
        #endregion save - delete

        private void AddLevel_button_Click(object sender, EventArgs e)
        {
            AddNewLevel AddNewLevel = new AddNewLevel(username);
            AddNewLevel.Show();
        }

        private void Level_comboBox_Enter(object sender, EventArgs e)
        {
            Level_bind();
        }

        private void MP_Back0_button_Click(object sender, EventArgs e)
        {
            AllProjects = new AllProjects(username);
            AllProjects.Show();

            this.Close();
        }

    }
}