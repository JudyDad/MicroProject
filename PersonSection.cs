using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MyWorkApplication
{
    public partial class PersonSection : Form
    {
        public PersonSection()
        {
            InitializeComponent();
        }
        public PersonSection(int type)
        {
            InitializeComponent();
            page_state = type;
            if (type == 1) //in micro project
            {
                PersonSection_tabControl.Visible = false;
            }
            else //in person
            {
                PersonSection_tabControl.Visible = true;
            }
        }
        public PersonSection(int type, string username)     //add
        {
            InitializeComponent();
            this.username = username;
            page_state = type;
            if (type == 1) //in micro project
            {
                PersonSection_tabControl.Visible = false;
            }
            else //in person
            {
                PersonSection_tabControl.Visible = true;
                InsertPerson_button.Visible = InsertCourse_button.Visible = InsertLanguage_button.Visible = InsertSkill_button.Visible
                    = InsertWork_button.Visible = InsertEducation_button.Visible = InsertExp_button.Visible 
                    = UpdateCourse_button.Visible = UpdateLanguage_button.Visible = UpdateSkill_button.Visible
                    = UpdateWork_button.Visible = UpdateExp_button.Visible = UpdateEducation_button.Visible = true;
                UpdatePerson_button.Visible = false;
            }
        }
        public PersonSection(DataRow dr, string username)     //update
        {
            InitializeComponent();
            this.username = username;
            SelectedDataRow = dr;
            PersonSection_tabControl.Visible = true;
            InsertPerson_button.Visible = false;
            UpdatePerson_button.Visible = UpdateCourse_button.Visible = UpdateLanguage_button.Visible = UpdateSkill_button.Visible
                = UpdateWork_button.Visible = UpdateEducation_button.Visible = UpdateExp_button.Visible
                = InsertCourse_button.Visible = InsertLanguage_button.Visible = InsertSkill_button.Visible
                = InsertWork_button.Visible = InsertEducation_button.Visible = InsertExp_button.Visible = true;
        }

        private int page_state;
        public string username;
        private SqlCommand sc, sc1;
        private SqlDataAdapter da;
        private SqlDataReader reader, reader1;
        private DataTable dt;
        private Thread myTh;
        private byte[] PersonPicArr;
        private ImageConverter converter;
        public DataRow SelectedDataRow, SelectedDataRowEdu;
        public static int Person_ID, Education_ID, Course_ID, Skill_ID, Language_ID, Work_ID, Experience_ID;
        public int Person_Education_ID, Person_Course_ID, Person_Skill_ID, Person_Language_ID, Person_Work_ID, Person_Experience_ID;
        private AddNewEducation AddNewEducation;
        private AddNewCourse AddNewCourse;
        private AddNewExperience AddNewExperience;
        private AddNewSkill AddNewSkill;
        private AddNewLanguage AddNewLanguage;
        private AddNewPlace AddNewPlace;
        private Log l;
        private string imageFilePath;
        AllBeneficiaries AllBeneficiaries;

        public void fill_boxes()
        {
            if (SelectedDataRow != null)
            {
                PersonName_textBox1.Text = PersonName_textBox2.Text = PersonName_textBox3.Text = PersonName_textBox4.Text = PersonName_textBox5.Text = PersonName_textBox6.Text =
                    (string)SelectedDataRow["الاسم"] + " " + (string)SelectedDataRow["اسم الأب"] + " " + (string)SelectedDataRow["الكنية"];

                Person_ID = Int32.Parse(SelectedDataRow["رقم الشخص"].ToString());

                PersonFName_textBox.Text = (string)SelectedDataRow["الاسم"];
                PersonLName_textBox.Text = (string)SelectedDataRow["الكنية"];
                PersonFatherName_textBox.Text = (string)SelectedDataRow["اسم الأب"];
                PersonMName_textBox.Text = (string)SelectedDataRow["اسم الأم"];
                string P_sex = (string)SelectedDataRow["الجنس"];
                if (P_sex.Contains(@"ذكر"))
                    PersonSexMale_radioButton.Checked = true;
                else
                    PersonSexFemale_radioButton.Checked = true;
                //   PersonSex_comboBox.Text = (string)SelectedDataRow["الجنس"];

                if (SelectedDataRow["الرقم الوطني"] != DBNull.Value)
                    PersonNationalNum_textBox.Text = (string)SelectedDataRow["الرقم الوطني"];
                else
                    PersonNationalNum_textBox.Text = "لا يوجد";

                PersonLName_textBox.Text = (string)SelectedDataRow["الكنية"];
                PersonLName_textBox.Text = (string)SelectedDataRow["الكنية"];
                PersonDOB_dateTimePicker.Value = (DateTime)SelectedDataRow["تاريخ الولادة"];
                PersonRegistration_textBox.Text = (string)SelectedDataRow["الجنسية"];
                PersonState_comboBox.Text = (string)SelectedDataRow["الحالة الاجتماعية"];
                PersonNumAtHome_textBox.Text = SelectedDataRow["عدد الأفراد بالمنزل"].ToString();

                string LiveWithAnotherFamily = (string)SelectedDataRow["هل يسكن مع عائلة أخرى"];
                if (LiveWithAnotherFamily.Equals("True"))
                    PersonLiveWithAnotherFamily_radioButton.Checked = true;
                else
                    PersonDontLiveWithAnotherFamily_radioButton.Checked = true;

                PersonEmail_textBox.Text = (string)SelectedDataRow["البريد الإلكتروني"];
                PersonHomeAddress_textBox.Text = (string)SelectedDataRow["عنوان المنزل"];
                PersonHomeTel_textBox.Text = (string)SelectedDataRow["هاتف المنزل"];
                PersonMobile_textBox.Text = (string)SelectedDataRow["موبايل"];

                string MicroProjectOwner = (string)SelectedDataRow["مستفيد من مشروع"];
                if (MicroProjectOwner.Equals("YES"))
                    MicroProjectOwner_checkBox.Checked = true;
                else
                    MicroProjectOwner_checkBox.Checked = true;

                PersonPicArr = null;
                string strCmd = "select [P_Picture] from Person where P_ID = " + Person_ID + " ";
                sc1 = new SqlCommand(strCmd, Program.MyConn);
                //sc1.ExecuteNonQuery();

                reader1 = sc1.ExecuteReader();
                reader1.Read();
                if (reader1.HasRows)
                {
                    PersonPicArr = (byte[])(reader1[0]);
                    reader1.Close();

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
                    reader1.Close();
                }
            }
            this.DialogResult = DialogResult.OK;
            Person_Education_bind();
            Person_Course_bind();
            Person_Work_bind();
            Person_Experience_bind();
            Person_Language_bind();
            Person_Skill_bind();
        }
        private void PersonSection_Load(object sender, EventArgs e)
        {
            l = new Log();
            System.Threading.Thread.CurrentThread.TrySetApartmentState(System.Threading.ApartmentState.STA);
            //        PersonDataGridView.ClearSelection();
            Person_ID = Education_ID = Skill_ID = Language_ID = Work_ID = Experience_ID = -1;
            clear_Person_boxes();
            if (page_state != 2)   //2 is to add new
                fill_boxes();
            Place_bind(0);
            Place_bind(1);
        }
        public int SelectCurrentPerson()
        {
            string selPersonMaxIDQuery = "select IDENT_CURRENT('Person')";
            sc = new SqlCommand(selPersonMaxIDQuery, Program.MyConn);
            Int32.TryParse((sc.ExecuteScalar()).ToString(), out Person_ID);
            return Person_ID;
        }
        private void clear_Person_boxes()
        {
            PersonFName_textBox.Text = PersonLName_textBox.Text = PersonFatherName_textBox.Text = PersonMName_textBox.Text =
               PersonNationalNum_textBox.Text = PersonRegistration_textBox.Text = PersonEmail_textBox.Text =
               PersonHomeAddress_textBox.Text = PersonHomeTel_textBox.Text = PersonMobile_textBox.Text = "";
            PersonNumAtHome_textBox.Text = "";
            PersonState_bind();
            PersonPicture_pictureBox.Image = null;
        }

        #region image

        private void PersonPicture_pictureBox_Click(object sender, MouseEventArgs e)
        {
            myTh = new Thread(new ThreadStart(CallDialog));
            myTh.SetApartmentState(ApartmentState.STA);
            myTh.Start();
        }

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
                //PersonPicture_pictureBox.Image = Image.FromFile(open.FileName);
                imageFilePath = open.FileName;
                PersonPicture_pictureBox.ImageLocation = imageFilePath;
            }
        }

        #endregion image

        #region binding data

        private void PersonEducation_comboBox_Enter(object sender, EventArgs e)
        {
            string Edu_Type = "";
            if (PersonEducationType_comboBox.SelectedItem != null)
            {
                Edu_Type = PersonEducationType_comboBox.SelectedItem.ToString();
                if (Edu_Type == "دبلوم" || Edu_Type == "ماجستير" || Edu_Type == "دكتوراه")
                {
                    Edu_Type = "إجازة جامعية";
                }
                Education_bind(Edu_Type, "");
            }
        }
        private void PersonSkill_comboBox_Enter(object sender, EventArgs e)
        {
            Skill_bind();
        }

        private void PersonCourse_comboBox_Enter(object sender, EventArgs e)
        {
            Course_bind();
        }

        private void PersonWork_comboBox_Enter(object sender, EventArgs e)
        {
            Work_bind();
        }

        private void PersonExp_comboBox_Enter(object sender, EventArgs e)
        {
            Experience_bind();
        }

        private void PersonLanguage_comboBox_Enter(object sender, EventArgs e)
        {
            Language_bind();
        }

        private void PersonCoursePlace_comboBox_Enter(object sender, EventArgs e)
        {
            Place_bind(1);
        }

        private void PersonEducationPlace_comboBox_Enter(object sender, EventArgs e)
        {
            Place_bind(0);
        }

        public void Person_Education_bind()
        {
            string strCmd = "select PE.PEdu_ID as 'Person_Education_ID'" +
                ",PE.Person_ID as 'رقم المستفيد'" +
                ",P.P_FirstName +' ' + P.P_LastName as 'اسم المستفيد'" +
                ",PE.ELevel as 'الدرجة العلمية'" +
                ",PE.Education_ID as 'رقم التحصيل العلمي'" +
                ",E.E_Name as 'التحصيل العلمي'" +
                ",PE.BeginYear as 'سنة البداية'" +
                ",PE.EndYear as 'سنة النهاية'" +
                ",PE.EPlace as 'المكان'" +
                "\n from [dbo].[Person_Education] PE left outer join Person P on PE.Person_ID = P.P_ID " +
                                        "left outer join Education E on PE.Education_ID = E.E_ID " +
                "\n where PE.Person_ID=" + Person_ID + "";
            //"\n from [dbo].[Person_Education] where Person_ID=" + Person_ID + "";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            PersonEducation_dataGridView.DataSource = dt;

            DataGridViewColumn dgC1 = PersonEducation_dataGridView.Columns["رقم المستفيد"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = PersonEducation_dataGridView.Columns["رقم التحصيل العلمي"];
            dgC2.Visible = false;
            DataGridViewColumn dgC3 = PersonEducation_dataGridView.Columns["Person_Education_ID"];
            dgC3.Visible = false;
        }

        public void Person_Course_bind()
        {
            string strCmd = "select PC.PC_ID as 'Person_Course_ID'" +
               ",PC.Person_ID as 'رقم المستفيد'" +
               ",P.P_FirstName +' ' + P.P_LastName as 'اسم المستفيد'" +
               ",PC.[Course_ID] as 'رقم الدورة'" +
               ", C.C_Name as 'الدورة'" +
               ",PC.[C_Duration] as 'المدة'" +
               ",PC.[C_YearTaken] as 'السنة'" +
               ",PC.[CoursePlace] as 'المكان'" +
               "\n from [dbo].[Person_Course] PC left outer join Person P on PC.Person_ID = P.P_ID " +
                                        "left outer join Course C on PC.Course_ID = C.C_ID " +
                "\n where PC.Person_ID=" + Person_ID + "";
            // "\n from [dbo].[Person_Course] where Person_ID=" + Person_ID + "";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            PersonCourse_dataGridView.DataSource = dt;

            DataGridViewColumn dgC1 = PersonCourse_dataGridView.Columns["رقم المستفيد"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = PersonCourse_dataGridView.Columns["رقم الدورة"];
            dgC2.Visible = false;
            DataGridViewColumn dgC3 = PersonCourse_dataGridView.Columns["Person_Course_ID"];
            dgC3.Visible = false;
        }

        public void Person_Experience_bind()
        {
            string strCmd = "select PWE.PW_ID as 'Person_Experience_ID'" +
                ",PWE.Person_ID as 'رقم المستفيد'" +
                ",P.P_FirstName +' ' + P.P_LastName as 'اسم المستفيد'" +
                ",PWE.[Work_ID] as 'رقم الخبرة'" +
                ", W.W_Name as 'مجال الخبرة'" +
                ",PWE.[W_Describtion] as 'الوصف'" +
                ",PWE.[W_BeginDate] as 'تاريخ البداية'" +
                ",PWE.[W_EndDate] as 'تاريخ النهاية'" +
                ",PWE.[W_Place] as 'المكان'" +
                ",PWE.[W_CauseOfLose] as 'سبب الفقدان'" +
                "\n from [dbo].[Person_WorkExp] PWE left outer join Person P on PWE.Person_ID = P.P_ID " +
                                        "left outer join Work W on PWE.Work_ID = W.W_ID " +
                "\n where PWE.Person_ID=" + Person_ID + " and PWE.W_Status='NO' and PWE.W_CauseOfLose = N'لا يوجد' ";
            //                "from [dbo].[Person_WorkExp] where (Person_ID=" + Person_ID + "and W_Status='NO' and W_CauseOfLose is null)";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            PersonExp_dataGridView.DataSource = dt;

            DataGridViewColumn dgC1 = PersonExp_dataGridView.Columns["رقم المستفيد"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = PersonExp_dataGridView.Columns["رقم الخبرة"];
            dgC2.Visible = false;
            DataGridViewColumn dgC3 = PersonExp_dataGridView.Columns["سبب الفقدان"];
            dgC3.Visible = false;
            DataGridViewColumn dgC4 = PersonExp_dataGridView.Columns["Person_Experience_ID"];
            dgC4.Visible = false;
        }

        public void Person_Work_bind()
        {
            string strCmd = "select PWE.PW_ID as 'Person_Work_ID'" +
                ",PWE.Person_ID as 'رقم المستفيد'" +
                ",P.P_FirstName +' ' + P.P_LastName as 'اسم المستفيد'" +
                ",PWE.[Work_ID] as 'رقم العمل'" +
                ", W.W_Name as 'مجال العمل'" +
                ",PWE.[W_Describtion] as 'الوصف'" +
                ",PWE.[W_BeginDate] as 'تاريخ البداية'" +
                ",PWE.[W_EndDate] as 'تاريخ النهاية'" +
                ",PWE.[W_Place] as 'المكان'" +
                ",PWE.[W_Status] as 'يعمل الآن'" +
                ",PWE.[W_CauseOfLose] as 'سبب الفقدان'" +
                ",PWE.[W_CauseOfLoseDescribtion] as 'وصف سبب الفقدان'" +
                ",PWE.[W_Type] as 'هل هي مهنة أساسية'" +

                "\n from [dbo].[Person_WorkExp] PWE left outer join Person P on PWE.Person_ID = P.P_ID " +
                                        "left outer join Work W on PWE.Work_ID = W.W_ID " +
                "\n where PWE.Person_ID=" + Person_ID + " and ((PWE.W_Status='YES' and PWE.W_CauseOfLose = N'لا يوجد') or (PWE.W_Status='NO' and PWE.W_CauseOfLose is not null))";
            //    "from [dbo].[Person_WorkExp] where Person_ID=" + Person_ID + "and (W_Status='YES' and W_CauseOfLose is null) or (W_Status='NO' and W_CauseOfLose is not null)";
            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            PersonWork_dataGridView.DataSource = dt;

            DataGridViewColumn dgC1 = PersonWork_dataGridView.Columns["رقم المستفيد"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = PersonWork_dataGridView.Columns["رقم العمل"];
            dgC2.Visible = false;
            DataGridViewColumn dgC3 = PersonWork_dataGridView.Columns["Person_Work_ID"];
            dgC3.Visible = false;
        }

        public void Person_Language_bind()
        {
            string strCmd = "select PL.PL_ID as 'Person_Language_ID'" +
               ",PL.Person_ID as 'رقم المستفيد'" +
               ",P.P_FirstName +' ' + P.P_LastName as 'اسم المستفيد'" +
               ",PL.[Language_ID] as 'رقم اللغة'" +
               ", L.L_Name as 'اللغة'" +
               ",PL.[L_Level] as 'المستوى'" +
               "\n from [dbo].[Person_Language] PL left outer join Person P on PL.Person_ID = P.P_ID " +
                                        "left outer join Language L on PL.Language_ID = L.L_ID " +
               "\n where PL.Person_ID=" + Person_ID + "";

            //        "from [dbo].[Person_Language] where Person_ID=" + Person_ID +" ";

            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            PersonLang_dataGridView.DataSource = dt;

            DataGridViewColumn dgC1 = PersonLang_dataGridView.Columns["رقم المستفيد"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = PersonLang_dataGridView.Columns["رقم اللغة"];
            dgC2.Visible = false;
            DataGridViewColumn dgC3 = PersonLang_dataGridView.Columns["Person_Language_ID"];
            dgC3.Visible = false;
        }

        public void Person_Skill_bind()
        {
            string strCmd = "select PS.PS_ID as 'Person_Skill_ID'" +
               ",PS.Person_ID as 'رقم المستفيد'" +
               ",P.P_FirstName +' ' + P.P_LastName as 'اسم المستفيد'" +
               ",PS.[Skill_ID] as 'رقم المهارة'" +
               ", S.S_Name as 'المهارة'" +
               ",PS.[S_Describtion] as 'الوصف'" +
               "\n from [dbo].[Person_Skill] PS left outer join Person P on PS.Person_ID = P.P_ID " +
                                        "left outer join Skill S on PS.Skill_ID = S.S_ID " +
               "\n where PS.Person_ID=" + Person_ID + "";
            //      "from [dbo].[Person_Skill] where Person_ID=" + Person_ID + " ";

            //   string strCmd = "select L_ID,L_Name from Language";
            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            PersonSkill_dataGridView.DataSource = dt;

            DataGridViewColumn dgC1 = PersonSkill_dataGridView.Columns["رقم المستفيد"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = PersonSkill_dataGridView.Columns["رقم المهارة"];
            dgC2.Visible = false;
            DataGridViewColumn dgC3 = PersonSkill_dataGridView.Columns["Person_Skill_ID"];
            dgC3.Visible = false;
        }

        public void Education_bind(string Edu_type, string Edu_Name)
        {
            PersonEducation_comboBox.SelectedText = Edu_Name;
            string strCmd = "select E_ID,E_Name from Education where E_Type=N'" + Edu_type + "'";
            string condition = "";

            if (Edu_Name != "")
            {
                condition = " and E_Name like N'" + Edu_Name + "%'";
            }
            strCmd += condition;
            string orderby = " order by E_Name";
            strCmd += orderby;
            sc = new SqlCommand(strCmd, Program.MyConn);
            da = new SqlDataAdapter(strCmd, Program.MyConn);
            reader = sc.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("E_ID", typeof(string));
            dt.Columns.Add("E_Name", typeof(string));
            dt.Load(reader);
            PersonEducation_comboBox.DisplayMember = "E_Name";
            PersonEducation_comboBox.ValueMember = "E_ID";
            PersonEducation_comboBox.DataSource = dt;
        }

        public void Course_bind()
        {
            string strCmd = "select C_ID,C_Name from Course";
            string orderby = " order by C_Name";
            strCmd += orderby;
            sc = new SqlCommand(strCmd, Program.MyConn);
            da = new SqlDataAdapter(strCmd, Program.MyConn);
            reader = sc.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("C_ID", typeof(string));
            dt.Columns.Add("C_Name", typeof(string));
            dt.Load(reader);
            PersonCourse_comboBox.DisplayMember = "C_Name";
            PersonCourse_comboBox.ValueMember = "C_ID";
            PersonCourse_comboBox.DataSource = dt;
        }

        public void Experience_bind()
        {
            string strCmd = "select W_ID,W_Name from Work";
            string orderby = " order by W_Name";
            strCmd += orderby;
            sc = new SqlCommand(strCmd, Program.MyConn);
            da = new SqlDataAdapter(strCmd, Program.MyConn);
            reader = sc.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("W_ID", typeof(string));
            dt.Columns.Add("W_Name", typeof(string));
            dt.Load(reader);
            PersonExp_comboBox.DisplayMember = "W_Name";
            PersonExp_comboBox.ValueMember = "W_ID";
            PersonExp_comboBox.DataSource = dt;
        }

        public void Work_bind()
        {
            string strCmd = "select W_ID,W_Name from Work";
            string orderby = " order by W_Name";
            strCmd += orderby;
            sc = new SqlCommand(strCmd, Program.MyConn);
            da = new SqlDataAdapter(strCmd, Program.MyConn);
            reader = sc.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("W_ID", typeof(string));
            dt.Columns.Add("W_Name", typeof(string));
            dt.Load(reader);
            PersonWork_comboBox.DisplayMember = "W_Name";
            PersonWork_comboBox.ValueMember = "W_ID";
            PersonWork_comboBox.DataSource = dt;
        }

        public void Language_bind()
        {
            string strCmd = "select L_Name,L_ID from Language";
            string orderby = " order by L_Name";
            strCmd += orderby;
            sc = new SqlCommand(strCmd, Program.MyConn);
            da = new SqlDataAdapter(strCmd, Program.MyConn);
            reader1 = sc.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("L_ID", typeof(string));
            dt.Columns.Add("L_Name", typeof(string));
            dt.Load(reader1);
            PersonLanguage_comboBox.DataSource = dt;
            PersonLanguage_comboBox.DisplayMember = "L_Name";
            PersonLanguage_comboBox.ValueMember = "L_ID";
        }

        public void Skill_bind()
        {
            string strCmd = "select S_ID,S_Name from Skill";
            string orderby = " order by S_Name";
            strCmd += orderby;
            sc = new SqlCommand(strCmd, Program.MyConn);
            da = new SqlDataAdapter(strCmd, Program.MyConn);
            reader = sc.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("S_ID", typeof(string));
            dt.Columns.Add("S_Name", typeof(string));
            dt.Load(reader);
            PersonSkill_comboBox.DisplayMember = "S_Name";
            PersonSkill_comboBox.ValueMember = "S_ID";
            PersonSkill_comboBox.DataSource = dt;
        }

        public void Place_bind(int type)
        {
            string strCmd;
            if (type == 0)
            {
                strCmd = "select CPlace_ID,CPlace_Name from CenterPlace where CPlace_type = 0";
                string orderby = " order by CPlace_Name";
                strCmd += orderby;
                sc = new SqlCommand(strCmd, Program.MyConn);
                da = new SqlDataAdapter(strCmd, Program.MyConn);
                reader = sc.ExecuteReader();
                dt = new DataTable();
                dt.Columns.Add("CPlace_ID", typeof(string));
                dt.Columns.Add("CPlace_Name", typeof(string));
                dt.Load(reader);
                PersonEducationPlace_comboBox.DisplayMember = "CPlace_Name";
                PersonEducationPlace_comboBox.ValueMember = "CPlace_ID";
                PersonEducationPlace_comboBox.DataSource = dt;
            }
            else
            {
                strCmd = "select CPlace_ID,CPlace_Name from CenterPlace where CPlace_type = 1";
                string orderby = " order by CPlace_Name";
                strCmd += orderby;
                sc = new SqlCommand(strCmd, Program.MyConn);
                da = new SqlDataAdapter(strCmd, Program.MyConn);
                reader = sc.ExecuteReader();
                dt = new DataTable();
                dt.Columns.Add("CPlace_ID", typeof(string));
                dt.Columns.Add("CPlace_Name", typeof(string));
                dt.Load(reader);
                PersonCoursePlace_comboBox.DisplayMember = "CPlace_Name";
                PersonCoursePlace_comboBox.ValueMember = "CPlace_ID";
                PersonCoursePlace_comboBox.DataSource = dt;
            }
        }

        public void PersonState_bind()
        {
            PersonState_comboBox.Items.Clear();
            if (PersonSexFemale_radioButton.Checked)
            {
                PersonState_comboBox.Items.Add("عازبة");
                PersonState_comboBox.Items.Add("متزوجة");
                PersonState_comboBox.Items.Add("مطلقة");
                PersonState_comboBox.Items.Add("أرملة");
            }
            else if (PersonSexMale_radioButton.Checked)
            {
                PersonState_comboBox.Items.Add("عازب");
                PersonState_comboBox.Items.Add("متزوج");
                PersonState_comboBox.Items.Add("مطلق");
                PersonState_comboBox.Items.Add("أرمل");
            }
            else
            {
                PersonState_comboBox.Items.Add("عازب");
                PersonState_comboBox.Items.Add("متزوج");
                PersonState_comboBox.Items.Add("مطلق");
                PersonState_comboBox.Items.Add("أرمل");
            }
        }

        private void PersonSexFemale__radioButton_CheckedChanged(object sender, EventArgs e)
        {
            PersonState_bind();
        }

        private void PersonSexMale_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            PersonState_bind();
        }

        #endregion binding data

        #region search by first name - last name - national number

        public void selectPerson()
        {
            string selectPersonQuery = "select P_ID as 'رقم الشخص'"
                + ",P_FirstName as 'الاسم'"
                + ",P_LastName as 'الكنية'"
                + ",P_FatherName as 'اسم الأب'"
                + ",P_MotherName as 'اسم الأم'"
                + ",P_Sex as 'الجنس'"
                + ",P_NationalNumber as 'الرقم الوطني'"
                + ",P_DOB as 'تاريخ الولادة'"
                + ",P_RegistrationPlace as 'الجنسية'"
                + ",P_MaritalStatus as 'الحالة الاجتماعية'"
                + ",P_NumAtHome as 'عدد الأفراد بالمنزل'"
                + ",P_IsLivingWithFamily as 'هل يسكن مع عائلة أخرى'"
                + ",P_Email as 'البريد الإلكتروني'"
                + ",P_HomeAddress as 'عنوان المنزل'"
                + ",P_HomeTel as 'هاتف المنزل'"
                + ",P_Mobile as 'موبايل'"
                + ",IsProjectOwner as 'مستفيد من مشروع'"
                //  + ",P_Picture as 'صورة الشخص'"
                + "\nFrom Person";
        }

        private void fnameTxtBox_TextChanged(object sender, EventArgs e)
        {
            //       selectPerson(fnameTxtBox.Text, lNameTxtBox.Text, nationalNumberTxtBox.Text);
        }

        private void lNameTxtBox_TextChanged(object sender, EventArgs e)
        {
            //        selectPerson(fnameTxtBox.Text, lNameTxtBox.Text, nationalNumberTxtBox.Text);
        }

        private void nationalNumberTxtBox_TextChanged(object sender, EventArgs e)
        {
            //      selectPerson(fnameTxtBox.Text, lNameTxtBox.Text, nationalNumberTxtBox.Text);
        }

        #endregion search by first name - last name - national number

        #region insert person and all his information

        private void insertPerson(byte[] PersonPicArr)
        {
            string insPersonQuery = "Insert Into dbo.Person values(N'"
                    + PersonFName_textBox.Text + "',N'"
                    + PersonLName_textBox.Text + "',N'"
                    + PersonFatherName_textBox.Text + "',N'"
                    + PersonMName_textBox.Text + "','"
                    + PersonDOB_dateTimePicker.Value.Month.ToString() + "/"
                    + PersonDOB_dateTimePicker.Value.Day.ToString() + "/"
                    + PersonDOB_dateTimePicker.Value.Year.ToString() + "',N'"
                    + PersonEmail_textBox.Text + "',N'"
                    + PersonMobile_textBox.Text + "',N'"
                    + PersonHomeTel_textBox.Text + "',N'"
                    + PersonHomeAddress_textBox.Text + "',N'"
                    + PersonNationalNum_textBox.Text + "',N'"
                    + PersonRegistration_textBox.Text + "',N'"
                    + (PersonSexMale_radioButton.Checked ? "ذكر" : "أنثى") + "',N'"
                    + PersonState_comboBox.Text + "','"
                    + (PersonLiveWithAnotherFamily_radioButton.Checked ? "True'" : "False'") + ",'"
                    + PersonNumAtHome_textBox.Text + "',"
                    + "@PersonPicArr" + ",N'"
                    + "YES"         //يملك مشروع
                    + "' )";

            sc = new SqlCommand(insPersonQuery, Program.MyConn);
            sc.Parameters.Add(new SqlParameter("@PersonPicArr", PersonPicArr));
            sc.ExecuteNonQuery();
        }

        private void insertPerson()
        {
            string insPersonQuery = "Insert Into dbo.Person values(N'"
                + PersonFName_textBox.Text + "',N'"
                + PersonLName_textBox.Text + "',N'"
                + PersonFatherName_textBox.Text + "',N'"
                + PersonMName_textBox.Text + "','"
                + PersonDOB_dateTimePicker.Value.Month.ToString() + "/"
                + PersonDOB_dateTimePicker.Value.Day.ToString() + "/"
                + PersonDOB_dateTimePicker.Value.Year.ToString() + "',N'"
                + PersonEmail_textBox.Text + "',N'"
                + PersonMobile_textBox.Text + "',N'"
                + PersonHomeTel_textBox.Text + "',N'"
                + PersonHomeAddress_textBox.Text + "',N'"
                + PersonNationalNum_textBox.Text + "',N'"
                + PersonRegistration_textBox.Text + "',N'"
                + (PersonSexMale_radioButton.Checked ? "ذكر" : "أنثى") + "',N'"
                + PersonState_comboBox.Text + "','"
                + (PersonLiveWithAnotherFamily_radioButton.Checked ? "True" : "False") + "','"
                + PersonNumAtHome_textBox.Text + "','"
                + null + "',N'"
                + "YES"         //يملك مشروع
                + "' )";
            sc = new SqlCommand(insPersonQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertPersonEducation(int pNo, int eNo, string eLevel, int eBegYear, int eEndYear, string ePlace)
        {
            string insPEQuery = "Insert Into Person_Education values("
                                      + "(select [P_ID] from [dbo].[Person] where [P_ID] = " + pNo + "),"
                                      + "(select [E_ID] from [dbo].[Education] where [E_ID] = " + eNo + "),N'"
                                      + eLevel + "',"
                                      + eBegYear + ","
                                      + eEndYear + ",N'"
                                      + ePlace + "')";
            sc = new SqlCommand(insPEQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertPersonCourse(int pNo, int cNo, string cDuration, string cYear, string cPlace)
        {
            string insPCQuery = "Insert Into Person_Course values("
                                    + "(select [P_ID] from [dbo].[Person] where [P_ID] = " + pNo + "),"
                                    + "(select [C_ID] from [dbo].[Course] where [C_ID] = " + cNo + "),N'"
                                    + cDuration + "',N'" + cYear + "',N'" + cPlace + "' )";
            sc = new SqlCommand(insPCQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertPersonExp(int pNo, int expNo, string ExpDescrib, string ExpPlace, string ExpBeginDate, string ExpEndDate)
        {
            string insPExpQuery = "Insert Into Person_Experience values("
                                    + "(select [P_ID] from [dbo].[Person] where [P_ID] = " + pNo + "),"
                                    + "(select [Exp_ID] from [dbo].[Experience] where [Exp_ID] = " + expNo + "),N'"
                                    + ExpDescrib + "',N'" + ExpPlace + "',N'" + ExpBeginDate + "',N'" + ExpEndDate + "')";
            sc = new SqlCommand(insPExpQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertPersonWorkExp(int pNo, int wNo, string WDescrib, string WBeginDate, string WEndDate, string WPlace, string wStatus, string wCause, string wCauseDesc, string wType)
        {
            string insPExpQuery = "Insert Into Person_WorkExp values("
                                    + "(select [P_ID] from [dbo].[Person] where [P_ID] = " + pNo + "),"
                                    + "(select [W_ID] from [dbo].[Work] where [W_ID] = " + wNo + "),N'"
                                    + WDescrib + "',N'" + WBeginDate + "',N'" + WEndDate + "',N'" + WPlace
                                    + "',N'" + wStatus + "',N'" + wCause + "',N'" + wCauseDesc + "',N'" + wType
                                    + "')";
            sc = new SqlCommand(insPExpQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertPersonLang(int PNo, int LNo, string LLevel)
        {
            string insPLangQuery = "Insert Into Person_Language values("
                                    + "(select [P_ID] from [dbo].[Person] where [P_ID] = " + PNo + "),"
                                    + "(select [L_ID] from [dbo].[Language] where [L_ID] = " + LNo + "),N'"
                                    + LLevel + "')";
            sc = new SqlCommand(insPLangQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void insertPersonSkill(int PNo, int SNo, string SDescribtion)
        {
            string insPSQuery = "Insert Into Person_Skill values("
                                    + "(select [P_ID] from [dbo].[Person] where [P_ID] = " + PNo + "),"
                                    + "(select [S_ID] from [dbo].[Skill] where [S_ID] = " + SNo + "),N'"
                                    + SDescribtion + "')";
            sc = new SqlCommand(insPSQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        #endregion insert person and all his information

        #region update person and all his information

        private void updatePerson(int PID)
        {
            string updPersonQuery = "Update Person set "
                + "P_FirstName = N'" + PersonFName_textBox.Text + "'"
                + ",P_LastName = N'" + PersonLName_textBox.Text + "'"
                + ",P_FatherName = N'" + PersonFatherName_textBox.Text + "'"
                + ",P_MotherName = N'" + PersonMName_textBox.Text + "'"
                + ",P_Sex = N'" + (PersonSexMale_radioButton.Checked ? "ذكر" : "أنثى") + "'"
                + ",P_NationalNumber = N'" + PersonNationalNum_textBox.Text + "'"
                + ",P_DOB = N'" + PersonDOB_dateTimePicker.Value.Month.ToString() + "/" + PersonDOB_dateTimePicker.Value.Day.ToString() + "/"
                                + PersonDOB_dateTimePicker.Value.Year.ToString() + "'"
                + ",P_RegistrationPlace = N'" + PersonRegistration_textBox.Text + "'"
                + ",P_MaritalStatus = N'" + PersonState_comboBox.Text + "'"
                + ",P_NumAtHome = " + PersonNumAtHome_textBox.Text + ""
                + ",P_IsLivingWithFamily = N'" + (PersonLiveWithAnotherFamily_radioButton.Checked ? "True" : "False") + "'"
                + ",P_Email = N'" + PersonEmail_textBox.Text + "'"
                + ",P_HomeAddress = N'" + PersonHomeAddress_textBox.Text + "'"
                + ",P_HomeTel = N'" + PersonHomeTel_textBox.Text + "'"
                + ",P_Mobile = N'" + PersonMobile_textBox.Text + "'"
                + ",P_Picture = @PersonPicArr "
                + ",IsProjectOwner = N'" + (MicroProjectOwner_checkBox.Checked ? "YES" : "NO") + "' "
                + " where P_ID =" + PID;

            sc = new SqlCommand(updPersonQuery, Program.MyConn);
            sc.Parameters.Add(new SqlParameter("@PersonPicArr", PersonPicArr));
            sc.ExecuteNonQuery();
        }

        private void Update_PersonEducation(int PEdu_ID, int PID, int EduID, string ELevel, int BeginYear, int EndYear, string EPlace)
        {
            string query = "Update [dbo].[Person_Education] set " +
                     "Person_ID =" + PID +
                     ",Education_ID =" + EduID +
                     ",ELevel =N'" + ELevel + "'" +
                     ",BeginYear =N'" + BeginYear + "'" +
                     ",EndYear =N'" + EndYear + "'" +
                     ",EPlace =N'" + EPlace + "' " +
                     " where PEdu_ID = " + PEdu_ID + " ";
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Update_PersonCourse(int PC_ID, int PID, int CID, string C_Duration, string C_YearTaken, string CoursePlace)
        {
            string query = "Update [dbo].[Person_Course] set [Person_ID] = " + PID +
                                ",[Course_ID] = " + CID +
                                ",[C_Duration] =N'" + C_Duration + "'" +
                                ",[C_YearTaken] = N'" + C_YearTaken + "'" +
                                ",[CoursePlace] = N'" + CoursePlace + "' " +
                                " where PC_ID = " + PC_ID + " ";
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Update_PersonSkill(int PS_ID, int PID, int SID, string S_Describtion)
        {
            string query = "update [dbo].[Person_Skill] set " +
                                "[Person_ID] = " + PID +
                                ",[Skill_ID] = " + SID +
                                ",[S_Describtion] = N'" + S_Describtion + "' " +
                                " where PS_ID = " + PS_ID + " ";
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Update_PersonLanguage(int PL_ID, int PID, int LID, string L_Level)
        {
            string query = "update [dbo].[Person_Language] set " +
                                "[Person_ID] = " + PID +
                                ",[Language_ID] = " + LID +
                                ",[L_Level] = N'" + L_Level + "' " +
                                " where PL_ID = " + PL_ID + " ";
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Update_PersonExperience(int PW_ID, int PID, int WID, string W_Describtion, string W_BeginDate, string W_EndDate, string W_Place, string W_Status, string W_CauseOfLose, string W_CauseOfLoseDescribtion, string W_Type)
        {
            string query = "update [Person_WorkExp] set " +
                                "[Person_ID] = " + PID +
                                ",[Work_ID] = " + WID +
                                ",[W_Describtion] = N'" + W_Describtion + "'" +
                                ",[W_BeginDate] =  '" + W_BeginDate + "'" +
                                ",[W_EndDate] =  '" + W_EndDate + "'" +
                                ",[W_Place] = N'" + W_Place + "'" +
                                ",[W_Status] = N'" + W_Status + "'" +
                                ",[W_CauseOfLose] = N'" + W_CauseOfLose + "'" +
                                ",[W_CauseOfLoseDescribtion] = N'" + W_CauseOfLoseDescribtion + "'" +
                                ",[W_Type] = N'" + W_Type + "' " +
                //  " where Work_ID = " + Work_ID + " and Person_ID = " + Person_ID;
                                " where PW_ID = " + PW_ID + " ";
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Update_PersonWork(int PW_ID, int PID, int WID, string W_Describtion, string W_BeginDate, string W_EndDate, string W_Place, string W_Status, string W_CauseOfLose, string W_CauseOfLoseDescribtion, string W_Type)
        {
            string query = "update [Person_WorkExp] set " +
                                "[Person_ID] = " + PID +
                                ",[Work_ID] = " + WID +
                                ",[W_Describtion] = N'" + W_Describtion + "'" +
                                ",[W_BeginDate] =  '" + W_BeginDate + "'" +
                                ",[W_EndDate] =  '" + W_EndDate + "'" +
                                ",[W_Place] = N'" + W_Place + "'" +
                                ",[W_Status] = N'" + W_Status + "'" +
                                ",[W_CauseOfLose] = N'" + W_CauseOfLose + "'" +
                                ",[W_CauseOfLoseDescribtion] = N'" + W_CauseOfLoseDescribtion + "'" +
                                ",[W_Type] = N'" + W_Type + "' " +
                                " where PW_ID = " + PW_ID + " ";
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        #endregion update person and all his information

        #region delete person and all his information

        private void deletePerson(int PID)
        {
            string delPersonQuery = "delete From Person where P_ID =" + PID;
            sc = new SqlCommand(delPersonQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Delete_PersonEducation(int PEdu_ID)
        {
            string query = "delete from [dbo].[Person_Education] " +
                                " where [PEdu_ID] = " + PEdu_ID + " ";
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Delete_PersonCourse(int PC_ID)
        {
            string query = "delete from [Person_Course] " +
                                " where PC_ID = " + PC_ID + " ";
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Delete_PersonLanguage(int PL_ID)
        {
            string query = "delete from [Person_Language] " +
                                " where PL_ID = " + PL_ID + " ";
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Delete_PersonSkill(int PS_ID)
        {
            string query = "delete from [Person_Skill] " +
                                " where [PS_ID] = " + PS_ID + " ";
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void Delete_WorkExp(int PW_ID)
        {
            string query = "delete from [Person_WorkExp] " +
                                " where [PW_ID] = " + PW_ID;
            sc = new SqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        #endregion delete person and all his information

        #region update buttons

        private void UpdatePerson_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (PersonFName_textBox.Text == "" || PersonLName_textBox.Text == "" || PersonFatherName_textBox.Text == ""
                    || PersonMName_textBox.Text == "" || PersonNationalNum_textBox.Text == "" || PersonRegistration_textBox.Text == ""
                    || PersonHomeAddress_textBox.Text == "" || PersonEmail_textBox.Text == "" || PersonHomeTel_textBox.Text == ""
                    || PersonMobile_textBox.Text == ""
                    //    || PersonPicture_pictureBox.Image == null
                    )
                {
                    throw new NoNullAllowedException();
                }
                //    PersonPicArr = Convert_Image_To_Binary(PersonPicture_pictureBox.Image);
                Convert_Picture();

                updatePerson(Person_ID);

                l.Insert_Log("Update " + PersonFName_textBox.Text + " " + PersonLName_textBox.Text, " Benefeciary ", username, DateTime.Now);

                clear_Person_boxes();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("لا يمكن ترك بعض الحقول فارغة");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("NationalNum_UniqueIndex"))
                {
                    MessageBox.Show("الرقم الوطني موجود مسبقاً يرجى التأكد و ثم إدخال البيانات مرة أخرى");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void UpdateEducation_button_Click(object sender, EventArgs e)
        {
            string edu_no_box, edu_name_string, edu_level_box, edu_beginyear_box, edu_endyear_box, edu_place_box;
            edu_beginyear_box = edu_endyear_box = "";
            edu_place_box = "لا يوجد";
            try
            {
                //edu_no_box = PersonEducation_comboBox.Text;
                //sc = new SqlCommand("select E_Name from Education where E_ID = " + edu_no_box, Program.MyConn);
                //edu_name_string = (string)sc.ExecuteScalar();

                edu_name_string = PersonEducation_comboBox.Text;
                sc = new SqlCommand("select E_ID from Education where E_Name like N'" + edu_name_string + "'", Program.MyConn);
                edu_no_box = sc.ExecuteScalar().ToString();

                edu_level_box = PersonEducationType_comboBox.Text;
                //         if (!PersonEducationBeginYear_comboBox.SelectedItem.Equals(null))
                edu_beginyear_box = PersonEducationBeginYear_comboBox.Text;
                //          if (PersonEducationEndYear_comboBox.SelectedIndex != -1)
                edu_endyear_box = PersonEducationEndYear_comboBox.Text;
                edu_place_box = PersonEducationPlace_comboBox.Text;
                if (edu_place_box != "لا يوجد")
                {
                    sc = new SqlCommand("select CPlace_Name from CenterPlace where CPlace_ID = " + edu_place_box, Program.MyConn);
                    edu_place_box = (string)sc.ExecuteScalar();
                }
                // Update_PersonEducation(Person_ID, Education_ID);
                Update_PersonEducation(Person_Education_ID, Person_ID, Int32.Parse(edu_no_box), edu_level_box, Int32.Parse(edu_beginyear_box), Int32.Parse(edu_endyear_box), edu_place_box);
             //   MessageBox.Show("تم تعديل البيانات للمستفيد بنجاح");
                Person_Education_bind();

                PersonEducation_comboBox.SelectedIndex = PersonEducationType_comboBox.SelectedIndex
                    = PersonEducationBeginYear_comboBox.SelectedIndex = PersonEducationEndYear_comboBox.SelectedIndex = PersonEducationPlace_comboBox.SelectedIndex = -1;
                PersonEducation_comboBox.Text = PersonEducationType_comboBox.Text
                    = PersonEducationBeginYear_comboBox.Text = PersonEducationEndYear_comboBox.Text = PersonEducationPlace_comboBox.Text = "";

                //    ShowPerson_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateCourse_button_Click(object sender, EventArgs e)
        {
            string Course_no_box, Course_Name_string, Course_year_box, Course_place_box, Course_Duration_string;
            Course_no_box = Course_Name_string = Course_year_box = "";
            Course_place_box = Course_Duration_string = "لا يوجد";
            // Display
            try
            {
                Course_Name_string = PersonCourse_comboBox.Text;
                sc = new SqlCommand("select C_ID from Course where C_Name like N'" + Course_Name_string + "'", Program.MyConn);
                Course_no_box = sc.ExecuteScalar().ToString();

                Course_year_box = PersonCourseYear_comboBox.Text;
                Course_place_box = PersonCoursePlace_comboBox.Text;
                if (Course_place_box != "لا يوجد")
                {
                    sc = new SqlCommand("select CPlace_Name from CenterPlace where CPlace_ID = " + Course_place_box, Program.MyConn);
                    Course_place_box = (string)sc.ExecuteScalar();
                }
                Course_Duration_string = PersonCourseDuration_textBox.Text;

                Update_PersonCourse(Person_Course_ID, Person_ID, Int32.Parse(Course_no_box), Course_Duration_string, Course_year_box, Course_place_box);

                MessageBox.Show("تم تعديل البيانات للمستفيد بنجاح");
                Person_Course_bind();

                PersonCourse_comboBox.SelectedValue = "";
                PersonCourseYear_comboBox.SelectedIndex = PersonCoursePlace_comboBox.SelectedIndex = -1;
                PersonCourseDuration_textBox.Clear();
                PersonCourse_comboBox.Text = PersonCourseYear_comboBox.Text = PersonCoursePlace_comboBox.Text = "";
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void UpdateSkill_button_Click(object sender, EventArgs e)
        {
            string Skill_no_box, Skill_name_string, Skill_Desc_string;
            Skill_Desc_string = "لا يوجد";
            // Display
            try
            {
                Skill_name_string = PersonSkill_comboBox.Text;
                sc = new SqlCommand("select S_ID from Skill where S_Name like N'" + Skill_name_string + "'", Program.MyConn);
                Skill_no_box = (string)sc.ExecuteScalar().ToString();
                Skill_Desc_string = PersonSkillDesc_textBox.Text;

                Update_PersonSkill(Person_Skill_ID, Person_ID, Int32.Parse(Skill_no_box), Skill_Desc_string);
             //   MessageBox.Show("تم تعديل البيانات للمستفيد بنجاح");
                Person_Skill_bind();

                PersonSkill_comboBox.SelectedValue = "";
                PersonSkillDesc_textBox.Clear();
                PersonSkill_comboBox.Text = "";
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void UpdateLanguage_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                string Lang_no_box, Lang_name_string, Lang_level_string;
                Lang_level_string = "لا يوجد";
                Lang_no_box = Lang_name_string = "";
                // Display
                Lang_name_string = PersonLanguage_comboBox.Text;
                sc = new SqlCommand("select L_ID from Language where L_Name like N'" + Lang_name_string + "'", Program.MyConn);
                Lang_no_box = (string)sc.ExecuteScalar().ToString();
                Lang_level_string = PersonLanguageLevel_comboBox.Text;
                Update_PersonLanguage(Person_Language_ID, Person_ID, Int32.Parse(Lang_no_box), Lang_level_string);
         //       MessageBox.Show("تم تعديل البيانات للمستفيد بنجاح");
                Person_Language_bind();

                PersonLanguage_comboBox.SelectedIndex = PersonLanguageLevel_comboBox.SelectedIndex = -1;
                PersonLanguage_comboBox.Text = PersonLanguageLevel_comboBox.Text = "";
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void UpdateWork_button_Click(object sender, EventArgs e)
        {
            string Work_status_string, Work_no_box, Work_name_string, Work_beginYear_string, Work_endYear_string, Work_Type_string,
                WorkCauseOfLose_string, WorkCauseOfLoseDesc_string, WorkDescribtion_string, WorkPlace_string;
            try
            {
                WorkCauseOfLose_string = WorkCauseOfLoseDesc_string = WorkDescribtion_string = WorkPlace_string = "لا يوجد";
                Work_Type_string = Work_status_string = "";
                if (PersonIsWorking_radioButton.Checked)
                    Work_status_string = "Yes";
                else
                    Work_status_string = "No";
                if (PersonWorkType_checkBox.Checked)
                    Work_Type_string = "مهنة أساسية";
                else
                    Work_Type_string = "ليست مهنة أساسية";

                Work_name_string = PersonWork_comboBox.Text;
                sc = new SqlCommand("select W_ID from Work where W_Name like N'" + Work_name_string + "'", Program.MyConn);
                Work_no_box = (string)sc.ExecuteScalar().ToString();

                Work_beginYear_string = Work_endYear_string = "0000";
                //        if (PersonWorkBeginDate_comboBox.SelectedItem != null)
                Work_beginYear_string = PersonWorkBeginDate_comboBox.Text;
                //        if (PersonWorkEndDate_comboBox.SelectedItem != null)
                Work_endYear_string = PersonWorkEndDate_comboBox.Text;
                //        if (PersonWorkCauseOfLose_comboBox.SelectedItem != null)
                WorkCauseOfLose_string = PersonWorkCauseOfLose_comboBox.Text;
                //        if (PersonWorkCauseOfLoseDesc_textBox.Text != "")
                WorkCauseOfLoseDesc_string = PersonWorkCauseOfLoseDesc_textBox.Text;
                //       if (PersonWorkDescribtion_textBox.Text != "")
                WorkDescribtion_string = PersonWorkDescribtion_textBox.Text;
                //        if (PersonWorkPlace_textBox.Text != "")
                WorkPlace_string = PersonWorkPlace_textBox.Text;

                Update_PersonWork(Person_Work_ID, Person_ID, Int32.Parse(Work_no_box), WorkDescribtion_string, Work_beginYear_string, Work_endYear_string, WorkPlace_string,
                     Work_status_string, WorkCauseOfLose_string, WorkCauseOfLoseDesc_string, Work_Type_string);
             //   MessageBox.Show("تم تعديل البيانات للمستفيد بنجاح");
                Person_Work_bind();

                PersonWorkBeginDate_comboBox.SelectedIndex = PersonWorkEndDate_comboBox.SelectedIndex = PersonWorkCauseOfLose_comboBox.SelectedIndex = -1;
                PersonWork_comboBox.SelectedValue = PersonWorkDescribtion_textBox.Text = PersonWorkPlace_textBox.Text = PersonWorkCauseOfLoseDesc_textBox.Text = "";
                PersonWorkType_checkBox.Checked = false;
                PersonWorkBeginDate_comboBox.Text = PersonWorkEndDate_comboBox.Text = PersonWorkCauseOfLose_comboBox.Text = "";
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void UpdateExp_button_Click(object sender, EventArgs e)
        {
            try
            {
                string Exp_no_box, Exp_name_string, Exp_beginYear_string, Exp_endYear_string, Exp_Desc_string, Exp_Place_string;
                Exp_no_box = Exp_name_string = Exp_beginYear_string = Exp_endYear_string = "";
                Exp_Desc_string = Exp_Place_string = "لا يوجد";

                //Exp_no_box = PersonExp_comboBox.SelectedValue.ToString();
                //sc = new SqlCommand("select W_Name from Work where W_ID = " + Exp_no_box, Program.MyConn);
                ////sc.ExecuteNonQuery();
                //Exp_name_string = (string)sc.ExecuteScalar();
                Exp_name_string = PersonExp_comboBox.Text;
                sc = new SqlCommand("select W_ID from Work where W_Name like N'" + Exp_name_string + "'", Program.MyConn);
                Exp_no_box = (string)sc.ExecuteScalar().ToString();

                Exp_beginYear_string = PersonExpBeginDate_comboBox.Text;
                Exp_endYear_string = PersonExpEndDate_comboBox.Text;
                //        if (PersonExpDescribtion_textBox.Text != "")
                Exp_Desc_string = PersonExpDescribtion_textBox.Text;
                //         if (PersonExpPlace_textBox.Text != "")
                Exp_Place_string = PersonExpPlace_textBox.Text;

                Update_PersonExperience(Person_Experience_ID, Person_ID, Int32.Parse(Exp_no_box), Exp_Desc_string, Exp_beginYear_string, Exp_endYear_string, Exp_Place_string, "NO", null, null, null);
             //   MessageBox.Show("تم تعديل البيانات للمستفيد بنجاح");
                Person_Experience_bind();

                PersonExpBeginDate_comboBox.SelectedIndex = PersonExpEndDate_comboBox.SelectedIndex = -1;
                PersonExp_comboBox.SelectedValue = PersonExpDescribtion_textBox.Text = PersonExpPlace_textBox.Text = "";
                PersonExpBeginDate_comboBox.Text = PersonExpEndDate_comboBox.Text = "";
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        #endregion update buttons

        #region delete buttons

        private void DeletePerson_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل أنت متأكد أنك تريد حذف هذا المستفيد بشكل نهائي؟", "سيتم حذف المستفيد مع جميع بياناته", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes && SelectedDataRow != null && Person_ID != -1)
            {
                deletePerson(Person_ID);

                l.Insert_Log("Delete " + PersonFName_textBox.Text + " " + PersonLName_textBox.Text, " Benefeciary ", username, DateTime.Now);

                PersonSection_Load(sender, e);
            }
        }

        private void DeleteEdu_button_Click(object sender, EventArgs e)
        {
            try
            {
                Delete_PersonEducation(Person_Education_ID);
             //   MessageBox.Show("تم حذف البيانات للمستفيد بنجاح");
                Person_Education_bind();
                PersonEducation_comboBox.Text = PersonEducationBeginYear_comboBox.Text = PersonEducationEndYear_comboBox.Text =
                    PersonEducationPlace_comboBox.Text = PersonEducationType_comboBox.Text = "";
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void DeleteCourse_button_Click(object sender, EventArgs e)
        {
            try
            {
                Delete_PersonCourse(Person_Course_ID);
           //     MessageBox.Show("تم حذف البيانات للمستفيد بنجاح");
                Person_Course_bind();
                PersonCourse_comboBox.Text = PersonCoursePlace_comboBox.Text =
                    PersonCourseYear_comboBox.Text = PersonCourseDuration_textBox.Text = "";
                //    = PersonCourseDuration_listBox.Text= "";
                //PersonCourseDuration_numericUpDown.ResetText();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void DeleteSkill_button_Click(object sender, EventArgs e)
        {
            try
            {
                Delete_PersonSkill(Person_Skill_ID);
          //      MessageBox.Show("تم حذف البيانات للمستفيد بنجاح");
                Person_Skill_bind();
                PersonSkill_comboBox.Text = PersonSkillDesc_textBox.Text = "";
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void DeleteLang_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                Delete_PersonLanguage(Person_Language_ID);
        //        MessageBox.Show("تم حذف البيانات للمستفيد بنجاح");
                Person_Language_bind();
                PersonLanguage_comboBox.Text = PersonLanguageLevel_comboBox.Text = "";
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void DeleteWork_button_Click(object sender, EventArgs e)
        {
            try
            {
                Delete_WorkExp(Person_Work_ID);
        //        MessageBox.Show("تم حذف البيانات للمستفيد بنجاح");
                Person_Work_bind();
                PersonWork_comboBox.Text = PersonWorkBeginDate_comboBox.Text = PersonWorkEndDate_comboBox.Text = PersonWorkPlace_textBox.Text
                   = PersonWorkDescribtion_textBox.Text = PersonWorkCauseOfLose_comboBox.Text = PersonWorkCauseOfLoseDesc_textBox.Text = "";
                PersonWorkType_checkBox.Checked = false;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void DeleteExp_button_Click(object sender, EventArgs e)
        {
            try
            {
                Delete_WorkExp(Person_Experience_ID);
           //     MessageBox.Show("تم حذف البيانات للمستفيد بنجاح");
                Person_Experience_bind();
                PersonExp_comboBox.Text = PersonExpBeginDate_comboBox.Text = PersonExpEndDate_comboBox.Text = PersonExpDescribtion_textBox.Text
                    = PersonExpPlace_textBox.Text = "";
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        #endregion delete buttons

        #region insert buttons

        private void InsertPerson_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (PersonFName_textBox.Text == "" || PersonLName_textBox.Text == "" || PersonFatherName_textBox.Text == ""
                    || PersonMName_textBox.Text == "" || PersonNationalNum_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                sc = new SqlCommand("select count(P_NationalNumber) from Person where P_NationalNumber = '" + PersonNationalNum_textBox.Text + "'", Program.MyConn);
                int check_NationalID = (int)sc.ExecuteScalar();
                if (check_NationalID != 0)
                {
                    throw new Exception("الرقم الوطني موجود مسبقاً الرجاء التحقق");
                }
                if (PersonPicture_pictureBox.Image != null)
                {
                    Convert_Picture();
                    insertPerson(PersonPicArr);
                }
                else
                    insertPerson();
                //    MessageBox.Show("تمت إضافة البيانات الشخصية للشخص بنجاح");

                l.Insert_Log("Insert " + PersonFName_textBox.Text + " " + PersonLName_textBox.Text, " Benefeciary ", username, DateTime.Now);

                string personFullName = PersonFName_textBox.Text + " " + PersonFatherName_textBox.Text + " " + PersonLName_textBox.Text;
                PersonName_textBox1.Text = PersonName_textBox2.Text = PersonName_textBox3.Text =
                    PersonName_textBox4.Text = PersonName_textBox5.Text = PersonName_textBox6.Text = personFullName;
                Person_ID = SelectCurrentPerson();
                clear_Person_boxes();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("لا يمكن ترك بعض الحقول فارغة");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("الرقم الوطني موجود مسبقاً الرجاء التحقق"))
                    MessageBox.Show("الرقم الوطني موجود مسبقاً الرجاء التحقق");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void InsertEducation_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Person_ID == -1)
                { throw new NoNullAllowedException(); }

                string edu_no_box = "", edu_level_box = "", edu_beginyear_box = "0000", edu_endyear_box = "0000", edu_place_box = "لا يوجد";
                // Display
                edu_no_box = PersonEducation_comboBox.SelectedValue.ToString();
                //sc = new SqlCommand("select E_Name from Education where E_ID = " + edu_no_box, Program.MyConn);
                //edu_name_string = (string)sc.ExecuteScalar();
                edu_level_box = PersonEducationType_comboBox.SelectedItem.ToString();

                if (PersonEducationBeginYear_comboBox.Text != "")
                    edu_beginyear_box = PersonEducationBeginYear_comboBox.Text;
                if (PersonEducationEndYear_comboBox.Text != "")
                    edu_endyear_box = PersonEducationEndYear_comboBox.Text;
                if (PersonEducationPlace_comboBox.Text != "")
                {
                    edu_place_box = PersonEducationPlace_comboBox.SelectedValue.ToString();
                    sc = new SqlCommand("select CPlace_Name from CenterPlace where CPlace_ID = " + edu_place_box, Program.MyConn);
                    edu_place_box = (string)sc.ExecuteScalar();
                }
                insertPersonEducation(Person_ID, Int32.Parse(edu_no_box), edu_level_box, Int32.Parse(edu_beginyear_box), Int32.Parse(edu_endyear_box), edu_place_box);
                Person_Education_bind();
                PersonEducation_comboBox.Text = PersonEducationBeginYear_comboBox.Text = PersonEducationEndYear_comboBox.Text =
                    PersonEducationPlace_comboBox.Text = PersonEducationType_comboBox.Text = "";
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("رجاءً اختر المستفيد الذي تريد إضافة تفاصيل لبياناته");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void InsertCourse_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Person_ID == -1)
                { throw new NoNullAllowedException(); }

                string Course_no_box, Course_year_box, Course_place_box, Course_Duration_string;
                Course_no_box = "";
                Course_place_box = Course_Duration_string = Course_year_box = "لا يوجد";
                // Display
                Course_no_box = PersonCourse_comboBox.SelectedValue.ToString();
                //sc = new SqlCommand("select C_Name from Course where C_ID = " + Course_no_box, Program.MyConn);
                //Course_Name_string = (string)sc.ExecuteScalar();
                if (PersonCourseYear_comboBox.Text != "")
                    Course_year_box = PersonCourseYear_comboBox.Text;
                if (PersonCoursePlace_comboBox.Text != "")
                {
                    Course_place_box = PersonCoursePlace_comboBox.SelectedValue.ToString();
                    sc = new SqlCommand("select CPlace_Name from CenterPlace where CPlace_ID = " + Course_place_box, Program.MyConn);
                    Course_place_box = (string)sc.ExecuteScalar();
                }
                //Course_Duration_string = PersonCourseDuration_numericUpDown.Value.ToString() + " " + PersonCourseDuration_listBox.Text;
                Course_Duration_string = PersonCourseDuration_textBox.Text;
                insertPersonCourse(Person_ID, Int32.Parse(Course_no_box), Course_Duration_string, Course_year_box, Course_place_box);
                Person_Course_bind();
                PersonCourse_comboBox.Text = PersonCoursePlace_comboBox.Text = PersonCourseYear_comboBox.Text = PersonCourseDuration_textBox.Text = "";
                //    = PersonCourseDuration_listBox.Text = "";
                //PersonCourseDuration_numericUpDown.ResetText();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("رجاءً اختر المستفيد الذي تريد إضافة تفاصيل لبياناته");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void InsertWork_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Person_ID == -1)
                { throw new NoNullAllowedException(); }

                string Work_status_string, Work_no_box, Work_beginYear_string, Work_endYear_string, Work_Type_string,
               WorkCauseOfLose_string, WorkCauseOfLoseDesc_string, WorkDescribtion_string, WorkPlace_string;

                WorkCauseOfLose_string = WorkCauseOfLoseDesc_string = WorkDescribtion_string = WorkPlace_string = "لا يوجد";
                Work_Type_string = Work_status_string = "";
                if (PersonIsWorking_radioButton.Checked)
                    Work_status_string = "Yes";
                else
                    Work_status_string = "No";
                if (PersonWorkType_checkBox.Checked)
                    Work_Type_string = "مهنة أساسية";
                else
                    Work_Type_string = "ليست مهنة أساسية";

                Work_no_box = PersonWork_comboBox.SelectedValue.ToString();
                //sc = new SqlCommand("select W_Name from Work where W_ID = " + Work_no_box, Program.MyConn);
                //Work_name_string = (string)sc.ExecuteScalar();
                Work_beginYear_string = Work_endYear_string = "0000";

                if (PersonWorkBeginDate_comboBox.Text != "")
                    Work_beginYear_string = PersonWorkBeginDate_comboBox.Text;
                if (PersonWorkEndDate_comboBox.Text != "")
                    Work_endYear_string = PersonWorkEndDate_comboBox.Text;
                if (PersonWorkCauseOfLose_comboBox.Text != "")
                    WorkCauseOfLose_string = PersonWorkCauseOfLose_comboBox.Text;
                if (PersonWorkCauseOfLoseDesc_textBox.Text != "")
                    WorkCauseOfLoseDesc_string = PersonWorkCauseOfLoseDesc_textBox.Text;
                if (PersonWorkDescribtion_textBox.Text != "")
                    WorkDescribtion_string = PersonWorkDescribtion_textBox.Text;
                if (PersonWorkPlace_textBox.Text != "")
                    WorkPlace_string = PersonWorkPlace_textBox.Text;

                insertPersonWorkExp(Person_ID, Int32.Parse(Work_no_box), WorkDescribtion_string, Work_beginYear_string, Work_endYear_string,
                    WorkPlace_string, Work_status_string, WorkCauseOfLose_string, WorkCauseOfLoseDesc_string, Work_Type_string);
                Person_Work_bind();
                PersonWork_comboBox.Text = PersonWorkBeginDate_comboBox.Text = PersonWorkEndDate_comboBox.Text = PersonWorkPlace_textBox.Text
                 = PersonWorkDescribtion_textBox.Text = PersonWorkCauseOfLose_comboBox.Text = PersonWorkCauseOfLoseDesc_textBox.Text = "";
                PersonWorkType_checkBox.Checked = false;
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("رجاءً اختر المستفيد الذي تريد إضافة تفاصيل لبياناته");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void InsertLanguage_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Person_ID ==-1)
                {
                    throw new NoNullAllowedException();
                }
                string Lang_no_box, Lang_name_string, Lang_level_string;
                Lang_level_string = "لا يوجد";
                Lang_no_box = Lang_name_string = "";

                Lang_no_box = PersonLanguage_comboBox.SelectedValue.ToString();
                if (PersonLanguageLevel_comboBox.Text != "")
                    Lang_level_string = PersonLanguageLevel_comboBox.Text;
                insertPersonLang(Person_ID, Int32.Parse(Lang_no_box), Lang_level_string);
                Person_Language_bind();
                PersonLanguage_comboBox.Text = PersonLanguageLevel_comboBox.Text = "";
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("رجاءً اختر المستفيد الذي تريد إضافة تفاصيل لبياناته");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void InsertSkill_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Person_ID == -1)
                {
                    throw new NoNullAllowedException();
                }
                string Skill_no_box, Skill_Desc_string;
                Skill_Desc_string = "لا يوجد";
                // Display
                Skill_no_box = PersonSkill_comboBox.SelectedValue.ToString();
                if (PersonSkillDesc_textBox.Text != "")
                    Skill_Desc_string = PersonSkillDesc_textBox.Text;

                insertPersonSkill(Person_ID, Int32.Parse(Skill_no_box), Skill_Desc_string);
                Person_Skill_bind();
                PersonSkill_comboBox.Text = PersonSkillDesc_textBox.Text = "";
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("رجاءً اختر المستفيد الذي تريد إضافة تفاصيل لبياناته");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void InsertExp_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Person_ID == -1)
                {
                    throw new NoNullAllowedException();
                }
                string Exp_no_box, Exp_name_string, Exp_beginYear_string, Exp_endYear_string, Exp_Desc_string, Exp_Place_string;
                Exp_no_box = Exp_name_string = "";
                Exp_Desc_string = Exp_Place_string = Exp_beginYear_string = Exp_endYear_string = "لا يوجد";

                Exp_no_box = PersonExp_comboBox.SelectedValue.ToString();
                if (PersonExpBeginDate_comboBox.Text != "")
                    Exp_beginYear_string = PersonExpBeginDate_comboBox.Text;
                if (PersonExpEndDate_comboBox.Text != "")
                    Exp_endYear_string = PersonExpEndDate_comboBox.Text;
                if (PersonExpDescribtion_textBox.Text != "")
                    Exp_Desc_string = PersonExpDescribtion_textBox.Text;
                if (PersonExpPlace_textBox.Text != "")
                    Exp_Place_string = PersonExpPlace_textBox.Text;

                insertPersonWorkExp(Person_ID, Int32.Parse(Exp_no_box), Exp_Desc_string, Exp_beginYear_string, Exp_endYear_string,
                    Exp_Place_string, "NO", @"لا يوجد", @"لا يوجد", @"لا يوجد");

                Person_Experience_bind();
                PersonExp_comboBox.Text = PersonExpBeginDate_comboBox.Text = PersonExpEndDate_comboBox.Text = PersonExpDescribtion_textBox.Text
                = PersonExpPlace_textBox.Text = "";
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("رجاءً اختر المستفيد الذي تريد إضافة تفاصيل لبياناته");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        #endregion insert buttons

        #region add to comboBoxes buttons

        private void AddEducation_button_Click(object sender, EventArgs e)
        {
            AddNewEducation = new AddNewEducation(username);
            AddNewEducation.Show();
            //AddCategory = new AddCategory(0, username);
            //AddCategory.Show();
        }

        private void AddEduPlace_button_Click(object sender, EventArgs e)
        {
            //AddNewPlace = new AddNewPlace(0);
            //AddNewPlace.Show();
            AddNewPlace = new AddNewPlace(0, username);
            AddNewPlace.Show();
        }

        private void AddCourse_button_Click_1(object sender, EventArgs e)
        {
            AddNewCourse = new AddNewCourse(username);
            AddNewCourse.Show();
            //AddCategory = new AddCategory(3, username);
            //AddCategory.Show();
        }

        private void AddCoursePlace_button_Click_1(object sender, EventArgs e)
        {
            AddNewPlace = new AddNewPlace(1, username);
            AddNewPlace.Show();
        }

        private void AddWork_button_Click_1(object sender, EventArgs e)
        {
            //AddCategory = new AddCategory(4, username);
            //AddCategory.Show();
            AddNewExperience = new AddNewExperience(username);
            AddNewExperience.Show();
        }

        private void AddExperience_button_Click_1(object sender, EventArgs e)
        {
            //AddCategory = new AddCategory(5, username);
            //AddCategory.Show();
            AddNewExperience = new AddNewExperience(username);
            AddNewExperience.Show();
        }

        private void AddSkill_button_Click_1(object sender, EventArgs e)
        {
            //AddCategory = new AddCategory(1, username);
            //AddCategory.Show();
            AddNewSkill = new AddNewSkill(username);
            AddNewSkill.Show();
        }

        private void AddLanguage_button_Click_1(object sender, EventArgs e)
        {
            AddNewLanguage = new AddNewLanguage(username);
            AddNewLanguage.Show();
            //AddCategory = new AddCategory(2, username);
            //AddCategory.Show();
        }

        #endregion add to comboBoxes buttons

        #region datagrid selecting rows

        private void PersonDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //      SelectedDataRow = ((DataRowView)PersonDataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                PersonName_textBox1.Text = PersonName_textBox2.Text = PersonName_textBox3.Text = PersonName_textBox4.Text = PersonName_textBox5.Text = PersonName_textBox6.Text =
                    (string)SelectedDataRow["الاسم"] + " " + (string)SelectedDataRow["اسم الأب"] + " " + (string)SelectedDataRow["الكنية"];

                Person_ID = Int32.Parse(SelectedDataRow["رقم الشخص"].ToString());

                PersonFName_textBox.Text = (string)SelectedDataRow["الاسم"];
                PersonLName_textBox.Text = (string)SelectedDataRow["الكنية"];
                PersonFatherName_textBox.Text = (string)SelectedDataRow["اسم الأب"];
                PersonMName_textBox.Text = (string)SelectedDataRow["اسم الأم"];
                string P_sex = (string)SelectedDataRow["الجنس"];
                if (P_sex.Contains(@"ذكر"))
                    PersonSexMale_radioButton.Checked = true;
                else
                    PersonSexFemale_radioButton.Checked = true;
                //   PersonSex_comboBox.Text = (string)SelectedDataRow["الجنس"];

                if (SelectedDataRow["الرقم الوطني"] != DBNull.Value)
                    PersonNationalNum_textBox.Text = (string)SelectedDataRow["الرقم الوطني"];
                else
                    PersonNationalNum_textBox.Text = "لا يوجد";

                PersonLName_textBox.Text = (string)SelectedDataRow["الكنية"];
                PersonLName_textBox.Text = (string)SelectedDataRow["الكنية"];
                PersonDOB_dateTimePicker.Value = (DateTime)SelectedDataRow["تاريخ الولادة"];
                PersonRegistration_textBox.Text = (string)SelectedDataRow["الجنسية"];
                PersonState_comboBox.Text = (string)SelectedDataRow["الحالة الاجتماعية"];
                PersonNumAtHome_textBox.Text = SelectedDataRow["عدد الأفراد بالمنزل"].ToString();

                string LiveWithAnotherFamily = (string)SelectedDataRow["هل يسكن مع عائلة أخرى"];
                if (LiveWithAnotherFamily.Equals("True"))
                    PersonLiveWithAnotherFamily_radioButton.Checked = true;
                else
                    PersonDontLiveWithAnotherFamily_radioButton.Checked = true;

                PersonEmail_textBox.Text = (string)SelectedDataRow["البريد الإلكتروني"];
                PersonHomeAddress_textBox.Text = (string)SelectedDataRow["عنوان المنزل"];
                PersonHomeTel_textBox.Text = (string)SelectedDataRow["هاتف المنزل"];
                PersonMobile_textBox.Text = (string)SelectedDataRow["موبايل"];

                string MicroProjectOwner = (string)SelectedDataRow["مستفيد من مشروع"];
                if (MicroProjectOwner.Equals("YES"))
                    MicroProjectOwner_checkBox.Checked = true;
                else
                    MicroProjectOwner_checkBox.Checked = true;

                PersonPicArr = null;
                string strCmd = "select [P_Picture] from Person where P_ID = " + Person_ID + " ";
                sc1 = new SqlCommand(strCmd, Program.MyConn);
                //sc1.ExecuteNonQuery();

                reader1 = sc1.ExecuteReader();
                reader1.Read();
                if (reader1.HasRows)
                {
                    PersonPicArr = (byte[])(reader1[0]);
                    reader1.Close();

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
                    reader1.Close();
                }
            }
            this.DialogResult = DialogResult.OK;
            Person_Education_bind();
            Person_Course_bind();
            Person_Work_bind();
            Person_Experience_bind();
            Person_Language_bind();
            Person_Skill_bind();
        }

        private void PersonEducation_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowEdu = ((DataRowView)PersonEducation_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowEdu != null)
            {
                Person_Education_ID = Int32.Parse(SelectedDataRowEdu["Person_Education_ID"].ToString());
                Person_ID = Int32.Parse(SelectedDataRowEdu["رقم المستفيد"].ToString());
                Education_ID = Int32.Parse(SelectedDataRowEdu["رقم التحصيل العلمي"].ToString());
                sc = new SqlCommand("select [E_Name] from Education where E_ID = " + Education_ID, Program.MyConn);

                PersonEducation_comboBox.Text = sc.ExecuteScalar().ToString();
                PersonEducationType_comboBox.Text = (string)SelectedDataRowEdu["الدرجة العلمية"];
                PersonEducationBeginYear_comboBox.Text = SelectedDataRowEdu["سنة البداية"].ToString();
                PersonEducationEndYear_comboBox.Text = SelectedDataRowEdu["سنة النهاية"].ToString();
                PersonEducationPlace_comboBox.Text = (string)SelectedDataRowEdu["المكان"];
            }
        }

        private void PersonLang_dataGridView_RowHeaderMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowEdu = ((DataRowView)PersonLang_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowEdu != null)
            {
                Person_Language_ID = Int32.Parse(SelectedDataRowEdu["Person_Language_ID"].ToString());
                Person_ID = Int32.Parse(SelectedDataRowEdu["رقم المستفيد"].ToString());
                Language_ID = Int32.Parse(SelectedDataRowEdu["رقم اللغة"].ToString());
                sc = new SqlCommand("select [L_Name] from Language where L_ID = " + Language_ID, Program.MyConn);
                PersonLanguage_comboBox.Text = sc.ExecuteScalar().ToString();
                PersonLanguageLevel_comboBox.Text = (string)SelectedDataRowEdu["المستوى"];
            }
        }

        private void PersonSkill_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowEdu = ((DataRowView)PersonSkill_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowEdu != null)
            {
                Person_Skill_ID = Int32.Parse(SelectedDataRowEdu["Person_Skill_ID"].ToString());
                Person_ID = Int32.Parse(SelectedDataRowEdu["رقم المستفيد"].ToString());
                Skill_ID = Int32.Parse(SelectedDataRowEdu["رقم المهارة"].ToString());
                sc = new SqlCommand("select [S_Name] from Skill where S_ID = " + Skill_ID, Program.MyConn);
                PersonSkill_comboBox.Text = sc.ExecuteScalar().ToString();
                PersonSkillDesc_textBox.Text = (string)SelectedDataRowEdu["الوصف"];
            }
        }

        private void PersonCourse_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowEdu = ((DataRowView)PersonCourse_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowEdu != null)
            {
                Person_Course_ID = Int32.Parse(SelectedDataRowEdu["Person_Course_ID"].ToString());
                Person_ID = Int32.Parse(SelectedDataRowEdu["رقم المستفيد"].ToString());
                Course_ID = Int32.Parse(SelectedDataRowEdu["رقم الدورة"].ToString());
                sc = new SqlCommand("select [C_Name] from Course where C_ID = " + Course_ID, Program.MyConn);

                PersonCourse_comboBox.Text = sc.ExecuteScalar().ToString();
                PersonCourseDuration_textBox.Text = (string)SelectedDataRowEdu["المدة"];
                PersonCourseYear_comboBox.Text = SelectedDataRowEdu["السنة"].ToString();
                PersonCoursePlace_comboBox.Text = SelectedDataRowEdu["المكان"].ToString();
            }
        }

        private void PersonWork_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowEdu = ((DataRowView)PersonWork_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowEdu != null)
            {
                Person_Work_ID = Int32.Parse(SelectedDataRowEdu["Person_Work_ID"].ToString());
                Person_ID = Int32.Parse(SelectedDataRowEdu["رقم المستفيد"].ToString());
                Work_ID = Int32.Parse(SelectedDataRowEdu["رقم العمل"].ToString());
                sc = new SqlCommand("select [W_Name] from Work where W_ID = " + Work_ID, Program.MyConn);
                PersonWork_comboBox.Text = sc.ExecuteScalar().ToString();
                PersonWorkDescribtion_textBox.Text = (string)SelectedDataRowEdu["الوصف"];
                PersonWorkBeginDate_comboBox.Text = SelectedDataRowEdu["تاريخ البداية"].ToString();
                PersonWorkEndDate_comboBox.Text = SelectedDataRowEdu["تاريخ النهاية"].ToString();
                PersonWorkPlace_textBox.Text = (string)SelectedDataRowEdu["المكان"];
                string W_status = SelectedDataRowEdu["يعمل الآن"].ToString();
                if (W_status == "YES") PersonIsWorking_radioButton.Checked = true;
                PersonWorkCauseOfLose_comboBox.Text = SelectedDataRowEdu["سبب الفقدان"].ToString();
                PersonWorkCauseOfLoseDesc_textBox.Text = SelectedDataRowEdu["وصف سبب الفقدان"].ToString();
                string W_type = SelectedDataRowEdu["هل هي مهنة أساسية"].ToString();
                if (W_type == "YES") PersonWorkType_checkBox.Checked = true;
            }
        }

        private void PersonExp_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRowEdu = ((DataRowView)PersonWork_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRowEdu != null)
            {
                Person_Experience_ID = Int32.Parse(SelectedDataRowEdu["Person_Experience_ID"].ToString());
                Person_ID = Int32.Parse(SelectedDataRowEdu["رقم المستفيد"].ToString());
                Experience_ID = Int32.Parse(SelectedDataRowEdu["رقم العمل"].ToString());
                sc = new SqlCommand("select [W_Name] from Work where W_ID = " + Experience_ID, Program.MyConn);
                PersonExp_comboBox.Text = sc.ExecuteScalar().ToString();
                PersonExpDescribtion_textBox.Text = (string)SelectedDataRowEdu["الوصف"];
                PersonExpBeginDate_comboBox.Text = SelectedDataRowEdu["تاريخ البداية"].ToString();
                PersonExpEndDate_comboBox.Text = SelectedDataRowEdu["تاريخ النهاية"].ToString();
                PersonExpPlace_textBox.Text = (string)SelectedDataRowEdu["المكان"];
            }
        }

        public DataRow showSelectedPersonRow()
        {
            //this.PersonDataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.PersonDataGridView_RowHeaderMouseDoubleClick);
            if (this.ShowDialog() == DialogResult.OK)
            {
                return SelectedDataRow;
            }
            else
            {
                return null;
            }
        }

        #endregion datagrid selecting rows

        #region tab control buttons

        private void PersonalInf_button_Click(object sender, EventArgs e)
        {
            PersonSection_tabControl.Visible = true;
            PersonSection_tabControl.SelectedIndex = 0;
        }

        private void Education_button_Click_1(object sender, EventArgs e)
        {
            PersonSection_tabControl.Visible = true;
            PersonSection_tabControl.SelectedIndex = 1;
        }

        private void Skill_button_Click(object sender, EventArgs e)
        {
            PersonSection_tabControl.Visible = true;
            PersonSection_tabControl.SelectedIndex = 2;
        }

        private void Language_button_Click(object sender, EventArgs e)
        {
            PersonSection_tabControl.Visible = true;
            PersonSection_tabControl.SelectedIndex = 3;
        }

        private void Course_button_Click_1(object sender, EventArgs e)
        {
            PersonSection_tabControl.Visible = true;
            PersonSection_tabControl.SelectedIndex = 4;
        }

        private void Work_button_Click(object sender, EventArgs e)
        {
            PersonSection_tabControl.Visible = true;
            PersonSection_tabControl.SelectedIndex = 5;
        }

        private void Experience_button_Click(object sender, EventArgs e)
        {
            PersonSection_tabControl.Visible = true;
            PersonSection_tabControl.SelectedIndex = 6;
        }

        private void button20_Click(object sender, EventArgs e)
        {
        }

        #endregion tab control buttons
        #region change btn backgrounds

        private void PersonalInfromation_button_MouseEnter(object sender, EventArgs e)
        {
            PersonalInfromation_button.BackgroundImage = Properties.Resources.B_Personal;
        }

        private void PersonalInfromation_button_MouseLeave(object sender, EventArgs e)
        {
            PersonalInfromation_button.BackgroundImage = null;
        }

        private void Skills_button_MouseEnter(object sender, EventArgs e)
        {
            Skills_button.BackgroundImage = Properties.Resources.B_Skill;
        }

        private void Skills_button_MouseLeave(object sender, EventArgs e)
        {
            Skills_button.BackgroundImage = null;
        }

        private void Education1_button_MouseEnter(object sender, EventArgs e)
        {
            Education1_button.BackgroundImage = Properties.Resources.B_Edu;
        }

        private void Education1_button_MouseLeave(object sender, EventArgs e)
        {
            Education1_button.BackgroundImage = null;
        }

        private void Languages_button_MouseEnter(object sender, EventArgs e)
        {
            Languages_button.BackgroundImage = Properties.Resources.B_Lang;
        }

        private void Languages_button_MouseLeave(object sender, EventArgs e)
        {
            Languages_button.BackgroundImage = null;
        }

        private void Courses_button_MouseEnter(object sender, EventArgs e)
        {
            Courses_button.BackgroundImage = Properties.Resources.B_Course;
        }

        private void Courses_button_MouseLeave(object sender, EventArgs e)
        {
            Courses_button.BackgroundImage = null;
        }

        private void Work_button_MouseEnter(object sender, EventArgs e)
        {
            Work_button.BackgroundImage = Properties.Resources.B_Job;
        }

        private void Work_button_MouseLeave(object sender, EventArgs e)
        {
            Work_button.BackgroundImage = null;
        }

        private void Experiences_button_MouseEnter(object sender, EventArgs e)
        {
            Experiences_button.BackgroundImage = Properties.Resources.B_Exp;
        }

        private void Experiences_button_MouseLeave(object sender, EventArgs e)
        {
            Experiences_button.BackgroundImage = null;
        }

        private void AddEducation_button_MouseEnter(object sender, EventArgs e)
        {
            AddEducation_button.BackgroundImage = AddCourse_button.BackgroundImage = AddSkill_button.BackgroundImage
                = AddWork_button.BackgroundImage = AddExperience_button.BackgroundImage = AddLanguage_button.BackgroundImage =
               Properties.Resources.plus;
        }

        private void AddEducation_button_MouseLeave(object sender, EventArgs e)
        {
            AddEducation_button.BackgroundImage = AddCourse_button.BackgroundImage = AddSkill_button.BackgroundImage
                = AddWork_button.BackgroundImage = AddExperience_button.BackgroundImage = AddLanguage_button.BackgroundImage =
                Properties.Resources.plus00;
        }

        private void AddPlace_button_MouseEnter(object sender, EventArgs e)
        {
            AddEduPlace_button.BackgroundImage = AddCoursePlace_button.BackgroundImage =
                Properties.Resources.plus;
        }

        private void AddPlace_button_MouseLeave(object sender, EventArgs e)
        {
            AddEduPlace_button.BackgroundImage = AddCoursePlace_button.BackgroundImage =
                Properties.Resources.plus00;
        }

        #endregion change btn backgrounds

        #region add - save - delete
        private void AddSave_button_MouseEnter(object sender, EventArgs e)
        {
            InsertPerson_button.BackgroundImage = Properties.Resources.save;
        }
        private void AddSave_button_MouseLeave(object sender, EventArgs e)
        {
            InsertPerson_button.BackgroundImage = Properties.Resources.save0;
        }

        private void UpdateSave_button_MouseEnter(object sender, EventArgs e)
        {
            UpdatePerson_button.BackgroundImage = Properties.Resources.save;
        }

        private void UpdateSave_button_MouseLeave(object sender, EventArgs e)
        {
            UpdatePerson_button.BackgroundImage = Properties.Resources.save0;
        }
        private void Delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteEducation_button.BackgroundImage = DeleteCourse_button.BackgroundImage = DeleteSkill_button.BackgroundImage
                = DeleteWork_button.BackgroundImage = DeleteExp_button.BackgroundImage = DeleteLang_button.BackgroundImage
                = Properties.Resources.delete;
        }
        private void Delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteEducation_button.BackgroundImage = DeleteCourse_button.BackgroundImage = DeleteSkill_button.BackgroundImage
                = DeleteWork_button.BackgroundImage = DeleteExp_button.BackgroundImage = DeleteLang_button.BackgroundImage
                = Properties.Resources.delete0;
        }
        private void add_button_MouseEnter(object sender, EventArgs e)
        {
            InsertEducation_button.BackgroundImage = InsertCourse_button.BackgroundImage = InsertSkill_button.BackgroundImage
                = InsertWork_button.BackgroundImage = InsertExp_button.BackgroundImage = InsertLanguage_button.BackgroundImage
                = Properties.Resources.add;
        }
        private void add_button_MouseLeave(object sender, EventArgs e)
        {
            InsertEducation_button.BackgroundImage = InsertCourse_button.BackgroundImage = InsertSkill_button.BackgroundImage
                = InsertWork_button.BackgroundImage = InsertExp_button.BackgroundImage = InsertLanguage_button.BackgroundImage = 
                Properties.Resources.add0;
        }
        private void update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateEducation_button.BackgroundImage = UpdateCourse_button.BackgroundImage = UpdateSkill_button.BackgroundImage
                = UpdateWork_button.BackgroundImage = UpdateExp_button.BackgroundImage = UpdateLanguage_button.BackgroundImage
                = Properties.Resources.update;
        }
        private void update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateEducation_button.BackgroundImage = UpdateCourse_button.BackgroundImage = UpdateSkill_button.BackgroundImage
                = UpdateWork_button.BackgroundImage = UpdateExp_button.BackgroundImage = UpdateLanguage_button.BackgroundImage
                = Properties.Resources.update0;
        }
        #endregion

        private void PersonPicture_pictureBox_Click(object sender, EventArgs e)
        {
            myTh = new Thread(new ThreadStart(CallDialog));
            myTh.SetApartmentState(ApartmentState.STA);
            myTh.Start();
        }

        private void P_Back0_button_Click(object sender, EventArgs e)
        {
            AllBeneficiaries = new AllBeneficiaries(username);
            AllBeneficiaries.Show();
            this.Close();
        }
    }
}