using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace MyWorkApplication
{
    public partial class Person_MicroProject_Section : Form
    {
        public Person_MicroProject_Section()
        {
            InitializeComponent();
        }

        private SqlCommand sc;
        private SqlDataAdapter da;
        private SqlDataReader reader;
        private DataTable dt;
        private string PersonNo = "";

        #region bind

        private void selectMicroProject(string MP_ID, string MP_Name, string intermidiarySide, string priest, string P_ID, string cost, string timeline)
        {
            string selectMPQuery = "select PMP.MicroProject_ID as 'رقم المشروع'"
                    + ",MP.[MP_Parish] as 'الطائفة'"
                    + ",MP.[MP_DateOfRequest] as 'تاريخ تعبئة الطلب'"
                    + ",MP.[MP_Name] as 'اسم المشروع'"
                    + ",MP.[MP_AllPriceNeeded] as 'المبلغ المطلوب'"
                    + ",MP.[MP_PeriodOfExecution] as 'مدة التنفيذ'"
                    + ",MP.[MP_Describtion] as 'الوصف'"
                    + ",MP.[MP_SimpleProfit] as 'الربح المتوقع'"
                    + ",MP.[MP_ResonOfProject] as 'رغبة المستفيد بالمشروع'"
                    + ",MP.[MP_SufferanceOfPerson] as 'معاناة المستفيد'"
                    + ",MP.[MP_IsNeedLicense] as 'هل يحتاج رخصة'"
                    //+ ",[MP_LicenseSide] as 'الرخصة'"
                    + ",MP.[MP_PlaceOfExecution] as 'مكان التنفيذ'"

                    + ",MP.[MP_SourceOfIncome] as 'مصدر الدخل'"
                    + ",MP.[MP_MedicalCond] as 'حالات مرضية'"
                    + ",MP.[MP_FinancialLoss] as 'خسارة مادية'"
                    + ",MP.[MP_SentimentalLoss] as 'خسارة معنوية'"
                    + ",'حالة المنزل' = CASE WHEN MP.[MP_HomeState] = 0 THEN N'أجار' WHEN MP.[MP_HomeState] = 1 THEN N'ملك' WHEN MP.[MP_HomeState] = 2 THEN N'استضافة' ELSE N'أسباب أخرى' END"
                    + ",'حالة المحل' =  CASE WHEN MP.[MP_ShopState] = 0 THEN N'أجار' WHEN MP.[MP_ShopState] = 1 THEN N'ملك' WHEN MP.[MP_ShopState] = 2 THEN N'استضافة' ELSE N'أسباب أخرى' END"
                  

                    + ",'حالة المشروع' = CASE WHEN MP.[MP_State] = 0 THEN N'مرفوض' WHEN MP.[MP_State] = 1 THEN N'مقبول' WHEN MP.[MP_State] = 2 THEN N'مؤجل' ELSE N'بالانتظار' END"
                    +",MP.[MP_StateReason] as 'سبب حالة المشروع'"
                    + ",MP.[MP_StateComment] as 'تعليقات'"
                    + ",L.Level_Symbol as  'درجة التقييم'"
                    + ",PMP.[Person_ID] as 'رقم المستفيد'"
                    + ",(P1.P_FirstName + ' ' + P1.P_LastName) as 'اسم المستفيد'"
                    + ",Iss1.IS_Name as 'الهيئة الوسيطة'"
                    + ",Iss2.IS_Name as 'الكاهن المحلي'"

              + ",MP.[MP_Country] as 'البلد'"
              + ",MP.[MP_City] as 'المدينة'"
          //    + ",count(MP.MP_ID) as 'عدد المشاريع'"

                + "\n from Person_MicroProject PMP left outer join Person P1 on P1.P_ID = PMP.Person_ID "
                                + "left outer join MicroProject MP on PMP.MicroProject_ID = MP.MP_ID "
                                + "left outer join [Level] L on MP.MP_Level_ID = L.Level_ID "
                                + "left outer join IntermediarySide Iss1 on Iss1.MicroProject_ID = MP.MP_ID and Iss1.[IS_isPriest] like N'%No' "
                                + "left outer  join IntermediarySide Iss2 on Iss2.MicroProject_ID = Iss1.MicroProject_ID and Iss2.[IS_isPriest] like N'%Yes' ";
                                
            string condition = "";
            if (MP_ID != "")
            {
                condition = "where PMP.MicroProject_ID = " + Int32.Parse(MP_ID) + " ";
                if (MP_Name != "")
                    condition += " and MP.[MP_Name] like N'" + MP_Name + "%'";
                if (intermidiarySide != "")
                    condition += " and Iss1.IS_Name like N'" + intermidiarySide + "%'";
                if (priest != "")
                    condition = " and Iss2.IS_Name like N'" + priest + "%'";
            }
            else if (MP_Name != "")
            {
                condition += " where MP.[MP_Name] like N'" + MP_Name + "%'";
                if (intermidiarySide != "")
                    condition += " and Iss1.IS_Name like N'" + intermidiarySide + "%'";
                if (priest != "")
                    condition = " and Iss2.IS_Name like N'" + priest + "%'";
            }
            else if (intermidiarySide != "")
            {
                condition += " where Iss1.IS_Name like N'" + intermidiarySide + "%'";
                if (priest != "")
                    condition = " and Iss2.IS_Name like N'" + priest + "%'";
            }
            else if (priest != "")
            {
                condition = " where Iss2.IS_Name like N'" + priest + "%'";
            }
            else if (P_ID != "" && PersonProject_radioButton.Checked)
                condition += "  where PMP.[Person_ID] = " + Int32.Parse(P_ID) + " ";
            else if (MP_Financed_radioButton.Checked)
                condition += " where MP.[MP_State] = 1";
            else if (MP_NotFinanced_radioButton.Checked)
                condition += " where MP.[MP_State] = 0";
            else if (MP_Delayed_radioButton.Checked)
                condition += " where MP.[MP_State] = 2";

            else if (MP_Hold_radioButton.Checked)
                condition += " where MP.[MP_State] = 3";

            else if (MP_NeedLicense_radioButton.Checked)
                condition += " where MP.[MP_IsNeedLicense] like N'True'";
            else if (MP_NoNeedLicense_radioButton.Checked)
                condition += " where MP.[MP_IsNeedLicense] like N'False'";
            else if (cost != "")
                condition += " where MP.[MP_AllPriceNeeded] <= '" + cost + "'";
            else if (timeline != "")
                condition += " where MP.[MP_AllPriceNeeded] like N'%" + timeline + "%'";

            selectMPQuery += condition;
            sc = new SqlCommand(selectMPQuery, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            MP_dataGridView.ColumnHeadersVisible = false;
            MP_dataGridView.DataSource = dt;
            MP_dataGridView.ColumnHeadersVisible = true;
        }

        private void selectPerson(string ID, string FName, string LName, string NationalNumber, string NumAtHome)
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
               // + ",count(P_ID) as 'عدد المستفيدين'"
                + "\nFrom Person where [IsProjectOwner] like N'YES' ";

            string condition = "";
            if (ID != "")
            {
                condition = "and P_ID = " + Int32.Parse(ID) + " ";
                if (FName != "")
                    condition = "and P_FirstName like N'" + FName + "%'";
                if (LName != "")
                    condition += " and P_LastName like N'" + LName + "%'";
                if (NationalNumber != "")
                    condition += " and P_NationalNumber like N'" + NationalNumber + "%'";
                if (NumAtHome != "0" && NumAtHome != "")
                    condition = "and P_NumAtHome =" + Int32.Parse(NumAtHome) + " ";
            }
            else if (FName != "")
            {
                condition = "and P_FirstName like N'" + FName + "%'";
                if (LName != "")
                    condition += " and P_LastName like N'" + LName + "%'";
                if (NationalNumber != "")
                    condition += " and P_NationalNumber like N'" + NationalNumber + "%'";
                if (NumAtHome != "0" && NumAtHome != "")
                    condition = "and P_NumAtHome =" + Int32.Parse(NumAtHome) + " ";
            }
            else if (LName != "")
            {
                condition = " and P_LastName like N'" + LName + "%'";
                if (NationalNumber != "")
                    condition += " and P_NationalNumber like N'" + NationalNumber + "%'";
                if (NumAtHome != "0" && NumAtHome != "")
                    condition = "and P_NumAtHome =" + Int32.Parse(NumAtHome) + " ";
            }
            else if (NationalNumber != "")
            {
                condition += " and P_NationalNumber like N'" + NationalNumber + "%'";
                if (NumAtHome != "0" && NumAtHome != "")
                    condition = "and P_NumAtHome =" + Int32.Parse(NumAtHome) + " ";
            }
            else if (NumAtHome != "0" && NumAtHome != "")
                condition = "and P_NumAtHome =" + Int32.Parse(NumAtHome) + " ";
            else if (P_IsLivingWithFamily_radioButton.Checked)
                condition += " and P_IsLivingWithFamily like N'True'";
            else if (P_IsNotLivingWithFamily_radioButton.Checked)
                condition += " and P_IsLivingWithFamily like N'False'";
            else if (P_Male_radioButton.Checked)
                condition += " and P_Sex like N'ذكر'";
            else if (P_Female_radioButton.Checked)
                condition += " and P_Sex not like N'ذكر'";
            else if (P_Married_radioButton.Checked)
                condition += " and P_MaritalStatus like N'متزوج%'";
            else if (P_NotMarried_radioButton.Checked)
                condition += " and P_MaritalStatus not like N'متزوج%'";

            selectPersonQuery += condition;
            sc = new SqlCommand(selectPersonQuery, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            Person_dataGridView.ColumnHeadersVisible = false;
            Person_dataGridView.DataSource = dt;
            Person_dataGridView.ColumnHeadersVisible = true;
        }

        //public void Education_bind(string Edu_type)
        //{
        //    string strCmd;
        //    if (Edu_type != "-1")
        //        strCmd = "select E_ID,E_Name from Education where E_Type=N'" + Edu_type + "'";
        //    else
        //        strCmd = "select E_ID,E_Name from Education ";
        //    sc = new SqlCommand(strCmd, Program.MyConn);
        //    da = new SqlDataAdapter(strCmd, Program.MyConn);
        //    reader = sc.ExecuteReader();
        //    dt = new DataTable();
        //    dt.Columns.Add("E_ID", typeof(string));
        //    dt.Columns.Add("E_Name", typeof(string));
        //    dt.Load(reader);
        //    PersonEducation_comboBox.DisplayMember = "E_Name";
        //    PersonEducation_comboBox.ValueMember = "E_ID";
        //    PersonEducation_comboBox.DataSource = dt;
        //}

        //public void Course_bind()
        //{
        //    string strCmd = "select C_ID,C_Name from Course";
        //    sc = new SqlCommand(strCmd, Program.MyConn);
        //    da = new SqlDataAdapter(strCmd, Program.MyConn);
        //    reader = sc.ExecuteReader();
        //    dt = new DataTable();
        //    dt.Columns.Add("C_ID", typeof(string));
        //    dt.Columns.Add("C_Name", typeof(string));
        //    dt.Load(reader);
        //    PersonCourse_comboBox.DisplayMember = "C_Name";
        //    PersonCourse_comboBox.ValueMember = "C_ID";
        //    PersonCourse_comboBox.DataSource = dt;
        //}

        //public void Experience_bind()
        //{
        //    string strCmd = "select  W_ID,W_Name from Work";
        //    sc = new SqlCommand(strCmd, Program.MyConn);
        //    da = new SqlDataAdapter(strCmd, Program.MyConn);
        //    reader = sc.ExecuteReader();
        //    dt = new DataTable();
        //    dt.Columns.Add("W_ID", typeof(string));
        //    dt.Columns.Add("W_Name", typeof(string));
        //    dt.Load(reader);
        //    PersonExp_comboBox.DisplayMember = "W_Name";
        //    PersonExp_comboBox.ValueMember = "W_ID";
        //    PersonExp_comboBox.DataSource = dt;
        //}

        //public void Work_bind()
        //{
        //    string strCmd = "select W_ID,W_Name from Work";
        //    sc = new SqlCommand(strCmd, Program.MyConn);
        //    da = new SqlDataAdapter(strCmd, Program.MyConn);
        //    reader = sc.ExecuteReader();
        //    dt = new DataTable();
        //    dt.Columns.Add("W_ID", typeof(string));
        //    dt.Columns.Add("W_Name", typeof(string));
        //    dt.Load(reader);
        //    PersonWork_comboBox.DisplayMember = "W_Name";
        //    PersonWork_comboBox.ValueMember = "W_ID";
        //    PersonWork_comboBox.DataSource = dt;
        //}

        //public void Skill_bind()
        //{
        //    string strCmd = "select S_ID,S_Name from Skill";
        //    sc = new SqlCommand(strCmd, Program.MyConn);
        //    da = new SqlDataAdapter(strCmd, Program.MyConn);
        //    reader = sc.ExecuteReader();
        //    dt = new DataTable();
        //    dt.Columns.Add("S_ID", typeof(string));
        //    dt.Columns.Add("S_Name", typeof(string));
        //    dt.Load(reader);
        //    PersonSkill_comboBox.DisplayMember = "S_Name";
        //    PersonSkill_comboBox.ValueMember = "S_ID";
        //    PersonSkill_comboBox.DataSource = dt;
        //}

        #endregion bind

        #region radio check changed events

        private void P_IsLivingWithFamily_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            selectPerson("", "", "", "", "");
        }

        private void P_Male_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            selectPerson("", "", "", "", "");
        }

        private void P_isWorking_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            selectPerson("", "", "", "", "");
        }

        private void P_Married_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            selectPerson("", "", "", "", "");
        }

        private void AllPersonOldradioButton_CheckedChanged(object sender, EventArgs e)
        {
            selectPerson("", "", "", "", "");
        }

        private void AllProject_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PersonProject_radioButton.Checked)
                selectMicroProject("", "", "", "", PersonNo, "", "");
            else
                selectMicroProject(MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, "", "", "");
        }

        private void MP_AllFinanced_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            selectMicroProject(MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, "", "", "");
        }

        private void MP_AllNeedLicense_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            selectMicroProject(MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, "", "", "");
        }

        #endregion radio check changed events

        #region text changed events

        private void PersonID_textBox_TextChanged(object sender, EventArgs e)
        {
            selectPerson(PersonID_textBox.Text, PersonFirstName_textBox.Text, PersonLastName_textBox.Text, PersonNationalNum_textBox.Text, PersonNumAtHome_numericUpDown.Text);
        }

        private void PersonNationalNum_textBox_TextChanged(object sender, EventArgs e)
        {
            selectPerson(PersonID_textBox.Text, PersonFirstName_textBox.Text, PersonLastName_textBox.Text, PersonNationalNum_textBox.Text, PersonNumAtHome_numericUpDown.Text);
        }

        private void PersonFirstName_textBox_TextChanged(object sender, EventArgs e)
        {
            selectPerson(PersonID_textBox.Text, PersonFirstName_textBox.Text, PersonLastName_textBox.Text, PersonNationalNum_textBox.Text, PersonNumAtHome_numericUpDown.Text);
        }

        private void PersonLastName_textBox_TextChanged(object sender, EventArgs e)
        {
            selectPerson(PersonID_textBox.Text, PersonFirstName_textBox.Text, PersonLastName_textBox.Text, PersonNationalNum_textBox.Text, PersonNumAtHome_numericUpDown.Text);
        }

        private void PersonNumAtHome_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            selectPerson(PersonID_textBox.Text, PersonFirstName_textBox.Text, PersonLastName_textBox.Text, PersonNationalNum_textBox.Text, PersonNumAtHome_numericUpDown.Text);
        }

        private void MP_ID_textBox_TextChanged(object sender, EventArgs e)
        {
            selectMicroProject(MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, PersonNo, "", "");
        }

        private void MP_Name_textBox_TextChanged(object sender, EventArgs e)
        {
            selectMicroProject(MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, PersonNo, "", "");
        }

        private void MP_Intermidary_textBox_TextChanged(object sender, EventArgs e)
        {
            selectMicroProject(MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, PersonNo, "", "");
        }

        private void MP_Priest_textBox_TextChanged(object sender, EventArgs e)
        {
            selectMicroProject(MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, PersonNo, "", "");
        }

        private void MP_Cost_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            selectMicroProject("", "", "", "", "", MP_Cost_numericUpDown.Value.ToString(), "");
        }

        private void MP_Duration_textBox_TextChanged(object sender, EventArgs e)
        {
            selectMicroProject("", "", "", "", "", "", MP_Duration_textBox.Text);
        }

        #endregion text changed events

        private void Person_MicroProject_Section_Load(object sender, EventArgs e)
        {
            selectPerson("", "", "", "", "");
            selectMicroProject("", "", "", "", "", "", "");
            //Education_bind("-1");
            //Course_bind();
            //Experience_bind();
            //Work_bind();
            //Skill_bind();
        }

        #region print to pdf

        private void Plan_button_Click(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter write = PdfWriter.GetInstance(doc, new FileStream("Report.pdf", FileMode.Create));
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

            doc.Open();
            //add picture
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("hope center logo.png");
            //resize img- 25%
            //       img.ScalePercent(20f);
            //possition img
            //       img.SetAbsolutePosition(doc.PageSize.Width - 36f - 72f, doc.PageSize.Height - 36f - 216.6f);
            img.ScaleToFit(50f, 100f);
            doc.Add(img);
            //write
            Paragraph p = new Paragraph("               HOPE CENTER - MICRO PROJECTS\n\n");
            doc.Add(p);

            //  PdfPTable table = new PdfPTable(3);
            //  PdfPCell cell = new PdfPCell(new Phrase("Header",new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL,15f,iTextSharp.text.Font.NORMAL,iTextSharp.text.BaseColor.RED)));
            ////  cell.BackgroundColor = new iTextSharp.text.BaseColor(100,0,0);
            //  cell.Colspan = 3;
            //  cell.HorizontalAlignment = 1; //0-left ,1-center, 2-right
            //  table.AddCell(cell);
            //  table.AddCell("col 1 row 1");
            //  table.AddCell("col 2 row 1");
            //  table.AddCell("col 3 row 1");
            //  doc.Add(table);

            BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\Arial.ttf", BaseFont.IDENTITY_H, true);
            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            font.Color = new iTextSharp.text.BaseColor(Person_dataGridView.ForeColor);

            //         float[] columnWidths = { 1, 5, 5 };
            PdfPTable table = new PdfPTable(Person_dataGridView.Columns.Count);
            //  table.DefaultCell.FixedHeight = 100f;
            //Set the column widths

            //      int[] widths = new int[Person_dataGridView.Columns.Count];

            for (int j = 0; j < Person_dataGridView.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(12, Person_dataGridView.Columns[j].HeaderText, font));
                table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.WidthPercentage = 100;
                cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table.AddCell(cell);

                //      cell.FixedHeight = 30f;
                //      table.AddCell(new Phrase(Person_dataGridView.Columns[j].HeaderText));
            }
            table.HeaderRows = 1;
            for (int i = 0; i < Person_dataGridView.Rows.Count; i++)
            {
                for (int k = 0; k < Person_dataGridView.Columns.Count; k++)
                {
                    if (Person_dataGridView[k, i].Value != null)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(12, Person_dataGridView[k, i].Value.ToString(), font));
                        cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        //   cell.FixedHeight = 20f;
                        table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                        //   table.AddCell(new Phrase(Person_dataGridView[k, i].Value.ToString()));
                    }
                }
            }
            doc.Add(table);
            doc.Close();
        }

        #endregion print to pdf

        private void Person_dataGridView_SelectionChanged_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            //PersonNo = "";
            //Person_dataGridView.Refresh();
            if (Person_dataGridView.DisplayedRowCount(false) != 0)
            {
                DataGridViewRow selectedPerson = Person_dataGridView.CurrentRow;
                PersonNo = (string)selectedPerson.Cells["رقم الشخص"].Value.ToString();
            }
            if (PersonProject_radioButton.Checked)
            {
                selectMicroProject("", "", "", "", PersonNo, "", "");
            }
            else
            {
                selectMicroProject(MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, "", "", "");
            }
        }

        private void MP_Cost_numericUpDown_ValueChanged_1(object sender, EventArgs e)
        {
            selectMicroProject("", "", "", "", "", MP_Cost_numericUpDown.Value.ToString(), "");
        }

        private void MP_Duration_textBox_TextChanged_1(object sender, EventArgs e)
        {
            selectMicroProject("", "", "", "", "", "", MP_Duration_textBox.Text);
        }
    }
}