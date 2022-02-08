using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MyWorkApplication
{
    public partial class FamilySection : Form
    {
        public FamilySection()
        {
            InitializeComponent();
        }
        public FamilySection(int i, string username)
        {
            InitializeComponent();
            this.username = username;
            type = i;
            if (type == 2)   //add
            {
                FamilySection_tabControl.Visible = true;
                InsertFamily_button.Visible = InsertFamilyMember_button.Visible = UpdateFamilyMember_button.Visible = true;
                UpdateFamily_button.Visible =  false;
            }
        }
        public FamilySection(DataRow dr, string username)   //update
        {
            InitializeComponent();
            this.username = username;
            SelectedDataRow = dr;
            type = 0;
            FamilySection_tabControl.Visible = true;
            InsertFamily_button.Visible =false;
            UpdateFamily_button.Visible = UpdateFamilyMember_button.Visible = InsertFamilyMember_button.Visible = true;
        }

        private Log l;
        private SqlCommand sc;
        private SqlDataAdapter da;
        private SqlDataReader reader;
        private DataTable dt;
        public DataRow SelectedDataRow, MicroProjectOwnerDataRow, FP_SelectedDataRow;
        private string username;
        private int ProviderID, FamilyID, PersonID, FamilyPerson_ID;
        private int MicroProjectOwner_ID;
        private string activeInFamily, MP_Owner, relationInFamily, WorkName;
        private int type;
        AllFamilies AllFamilies;

        private void FamilySection_Load(object sender, EventArgs e)
        {
            l = new Log();
            FP_SelectedDataRow = null;

            if (type == 2)
            {
                clearBoxes();
            }
            else
            {
                fill_boxes();
            }
            //FPersonID_textBox.Enabled = false;
            int n = getCurrentPersonID();
            FPersonID_textBox.Text = (++n).ToString();
            FamilyMember_bind("", "");
            bindPersonIntoProvider();
            //FProviderID_listBox.SelectedIndex = -1;
            //FProviderID_listBox1.SelectedIndex = -1;
            FProviderID_listBox.ClearSelected();
            FProviderID_listBox1.ClearSelected();
        }

        private void fill_boxes()
        {
            //  SelectedDataRow = ((DataRowView)FamilyDataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                FamilyNum_textBox.Text = (string)SelectedDataRow["رقم دفتر العائلة"];
                FamilyFName_textBox.Text = (string)SelectedDataRow["اسم الزوج"];
                FamilyLName_textBox.Text = (string)SelectedDataRow["الكنية"];
                FamilyFaName_textBox.Text = (string)SelectedDataRow["اسم الأب"];
                FamilyID = Int32.Parse(SelectedDataRow["رقم العائلة"].ToString());
                FamilyNum_txtBox.Text = FamilyNum_textBox.Text;
            }
        }

        #region bind
        public void Family_bind()
        {
            string selectFamilyQuery = "select F_ID as 'رقم العائلة'"
                + ",F_FirstName as 'اسم الزوج'"
                + ",F_LastName as 'الكنية'"
                + ",F_FatherName as 'اسم الأب'"
                + ",F_Number as 'رقم دفتر العائلة'"
                + "\nFrom Family";

            SqlCommand selectFamilyComm = new SqlCommand(selectFamilyQuery, Program.MyConn);
            selectFamilyComm.ExecuteNonQuery();
            SqlDataAdapter myAdapt = new SqlDataAdapter(selectFamilyComm);
            DataTable table = new DataTable();
            myAdapt.Fill(table);
            Family_dataGridView.DataSource = table;
        }
        public void Person_bind()
        {
            string selectFamilyQuery = "select P_ID as 'رقم الشخص'"
                + ",P_FirstName as 'الاسم'"
                + ",P_LastName as 'الكنية'"
                + ",P_FatherName as 'اسم الأب'"
                + ",P_MotherName as 'اسم الأم'"
                + ",P_Sex as 'الجنس'"
                + ",P_MaritalStatus as 'الحالة الاجتماعية'"
                + ",IsProjectOwner as 'مستفيد من مشروع'"
                + "\nFrom Person";

            SqlCommand selectFamilyComm = new SqlCommand(selectFamilyQuery, Program.MyConn);
            selectFamilyComm.ExecuteNonQuery();
            SqlDataAdapter myAdapt = new SqlDataAdapter(selectFamilyComm);
            DataTable table = new DataTable();
            myAdapt.Fill(table);
            Person_dataGridView.DataSource = table;
        }
        public void FamilyMember_bind(string strFname, string strLname)
        {
            string selectPersonQuery = "select p.P_ID as 'رقم الشخص'"
                + ",p.P_FirstName as 'الاسم'"
                + ",p.P_LastName as 'الكنية'"
                + ",p.P_FatherName as 'اسم الأب'"
                + ",p.P_MotherName as 'اسم الأم'"
                + ",p.P_Sex as 'الجنس'"
                + ",p.P_MaritalStatus as 'الحالة الاجتماعية'"

                + ",pf.IsInNow as 'فعال'"
                + ",pf.Relation as 'علاقته مع المستفيد'"
                + ",pf.Work_Name as 'العمل'"
                + ",pf.Family_ID as 'رقم دفتر العائلة'"
                + ",p.P_DOB as 'تاريخ الولادة'"
                + ",p.[IsProjectOwner] as 'مستفيد من مشروع'"
                + "\n From Person p left outer join [dbo].[Person_Family] pf on p.P_ID = pf.Person_ID ";

            string condition = "";
            if (strFname != "")
            {
                condition = " where p.P_FirstName like N'" + fnameTxtBox.Text + "%'";
                if (strLname != "")
                {
                    condition += " and p.P_LastName like N'" + lNameTxtBox.Text + "%'";
                }
            }
            else if (strLname != "")
            {
                condition = " where p.P_LastName like N'" + lNameTxtBox.Text + "%'";
            }

            selectPersonQuery += condition;
            SqlCommand selectPersonComm = new SqlCommand(selectPersonQuery, Program.MyConn);
            selectPersonComm.ExecuteNonQuery();
            SqlDataAdapter myAdapt = new SqlDataAdapter(selectPersonComm);
            DataTable table = new DataTable();
            myAdapt.Fill(table);
            PersonDataGridView.DataSource = table;

        }
        public void Family_FamilyMember_bind(string strNumber)
        {
            string Query = @"select PF.PF_ID as 'رقم السجل'
,F.F_Number as 'رقم دفتر العائلة'
,PF.Family_ID as 'رقم العائلة'
,PF.Person_ID 'رقم الشخص'
,(P1.P_FirstName + ' ' + P1.P_LastName + ' ' ) as 'اسم الشخص'
,p1.P_MaritalStatus as 'حالة الشخص'
,PF.IsInNow as 'فعال'
,PF.Relation as 'علاقته مع المستفيد'
,PF.Work_Name as 'عمله الحالي'
,PF.P_Provider_ID as 'رقم المعيل'
,(P1.P_FirstName + ' ' + P1.P_LastName + ' ' ) as 'اسم المعيل'

from [dbo].[Person_Family] PF left outer join [dbo].[Family] F on PF.Family_ID = F.F_ID
							  left outer join [dbo].[Person] P1 on PF.Person_ID = P1.P_ID
							  left outer join [dbo].[Person] P2 on PF.P_Provider_ID = P2.P_ID 
 order by Family_ID ";

            string condition = "";
            if (strNumber != "")
            {
                condition = " where F.F_Number like N'" + FamilyNum_textBox1.Text + "%'";
            }
            Query += condition;
            SqlCommand selectPersonComm = new SqlCommand(Query, Program.MyConn);
            selectPersonComm.ExecuteNonQuery();
            SqlDataAdapter myAdapt = new SqlDataAdapter(selectPersonComm);
            DataTable table = new DataTable();
            myAdapt.Fill(table);
            FamilyPersonRelation_dataGridView.DataSource = table;
            FamilyPerson_dataGridView.DataSource = table;
            DataGridViewColumn dgC1 = FamilyPerson_dataGridView.Columns["رقم السجل"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = FamilyPersonRelation_dataGridView.Columns["رقم السجل"];
            dgC2.Visible = false;
            DataGridViewColumn dgC3 = FamilyPersonRelation_dataGridView.Columns["رقم العائلة"];
            dgC3.Visible = false;
        }

        //to fill the list box
        private void bindPersonIntoProvider()
        {
            string strCmd = "select P_ID from Person";
            sc = new SqlCommand(strCmd, Program.MyConn);
            da = new SqlDataAdapter(strCmd, Program.MyConn);
            reader = sc.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("P_ID", typeof(string));
            dt.Columns.Add("P_Name", typeof(string));
            dt.Load(reader);
            FProviderID_listBox.DisplayMember = "P_ID";
            FProviderID_listBox.ValueMember = "P_ID";
            FProviderID_listBox.DataSource = dt;

            FProviderID_listBox1.DisplayMember = "P_ID";
            FProviderID_listBox1.ValueMember = "P_ID";
            FProviderID_listBox1.DataSource = dt;
        }

        public void PersonState_bind()
        {
            FPersonState_comboBox.Items.Clear();
            if (FPersonSexFemale_radioButton.Checked)
            {
                FPersonState_comboBox.Items.Add("عازبة");
                FPersonState_comboBox.Items.Add("متزوجة");
                FPersonState_comboBox.Items.Add("مطلقة");
                FPersonState_comboBox.Items.Add("أرملة");
            }
            else if (FPersonSexMale_radioButton.Checked)
            {
                FPersonState_comboBox.Items.Add("عازب");
                FPersonState_comboBox.Items.Add("متزوج");
                FPersonState_comboBox.Items.Add("مطلق");
                FPersonState_comboBox.Items.Add("أرمل");
            }
            else
            {
                FPersonState_comboBox.Items.Add("عازب");
                FPersonState_comboBox.Items.Add("متزوج");
                FPersonState_comboBox.Items.Add("مطلق");
                FPersonState_comboBox.Items.Add("أرمل");
            }
        }

        #endregion bind

        #region inserts

        private void insertFamily()
        {
            string insFamilyQuery = "Insert Into Family values(N'"
                        + FamilyFName_textBox.Text + "',N'"
                        + FamilyLName_textBox.Text + "',N'"
                        + FamilyFaName_textBox.Text + "',N'"
                        + FamilyNum_textBox.Text + "' )";
            SqlCommand insFamilyComm = new SqlCommand(insFamilyQuery, Program.MyConn);
            insFamilyComm.ExecuteNonQuery();
        }

        //////////////////////////////////////////////////////////////////////
        public void insertFamilyMember()
        {
            string insPersonQuery = "Insert Into dbo.Person values(N'"
                + FPersonFName_textBox.Text + "',N'"
                + (FPersonLName_textBox.Text != "" ? FPersonLName_textBox.Text : "لا يوجد") + "',N'"
                + (FPersonFatherName_textBox.Text != "" ? FPersonFatherName_textBox.Text : "لا يوجد") + "',N'"
                + "لا يوجد" + "','" //Mother Name
                + FPersonDOB_dateTimePicker.Value.Month.ToString() + "/"
                + FPersonDOB_dateTimePicker.Value.Day.ToString() + "/"
                + FPersonDOB_dateTimePicker.Value.Year.ToString() + "',N'"
                //    + " " + "',N'"  //DOB
                + "لا يوجد" + "',N'"  //POB
                + "لا يوجد" + "','"    // Mobile
                + "لا يوجد" + "',N'"  //HomeTel
                + "لا يوجد" + "',"  //HomeAdd
                + "null" + ",N'"  //NationalNumber
                + "لا يوجد" + "',N'"  //RegistrationPlace
                //     + FPersonSex_comboBox.Text + "',N'"
                + (FPersonSexMale_radioButton.Checked ? "ذكر" : "أنثى") + "',N'"
                + FPersonState_comboBox.Text + "','"
                + "لا يوجد" + "','"    //LiveWithAnotherFamily
                + "00" + "','"     //NumAtHome
                + null + "',N'"
                + (FMicroProjectOwner_checkBox.Checked == true ? "YES" : "NO")   //يملك مشروع
                + "')";    //picture
            SqlCommand insPersonComm = new SqlCommand(insPersonQuery, Program.MyConn);
            insPersonComm.ExecuteNonQuery();
        }

        public void insertFamilyPersonDetails(int fNo, int pNo, string isInNow, string relation, int providerID, string WorkName)
        {
            string insPFQuery = "Insert Into Person_Family values("
                                      + "(select [F_ID] from [dbo].[Family] where [F_ID] = " + fNo + "),"
                                      + "(select [P_ID] from [dbo].[Person] where [P_ID] = " + pNo + "), N'"
                                      + isInNow + "',N'"
                                      + relation + "',"
                                      + "(select [P_ID] from [dbo].[Person] where P_ID =  " + providerID + "), N'"
                                      + WorkName + "' "
                                      + ")";
            SqlCommand insPFComm = new SqlCommand(insPFQuery, Program.MyConn);
            insPFComm.ExecuteNonQuery();
        }

        #endregion inserts

        #region updates

        private void updateFamily(int FID)
        {
            string updFamilyQuery = "Update Family set "
                        + "F_FirstName = N'" + FamilyFName_textBox.Text + "', "
                        + "F_LastName = N'" + FamilyLName_textBox.Text + "', "
                        + "F_FatherName = N'" + FamilyFaName_textBox.Text + "', "
                        + "F_Number = N'" + FamilyNum_textBox.Text + "' "
                        + "where F_ID =" + FID;
            SqlCommand updFamilyComm = new SqlCommand(updFamilyQuery, Program.MyConn);
            updFamilyComm.ExecuteNonQuery();
        }

        /////////////////////////////////////////////////////////////////////////////
        private void updateFamilyMember(int PID)
        {
            string updPersonQuery = "Update Person set "
                + "P_FirstName = N'" + FPersonFName_textBox.Text + "'"
                + ",P_LastName = N'" + (FPersonLName_textBox.Text != "" ? FPersonLName_textBox.Text : "لا يوجد") + "'"
                + ",P_FatherName = N'" + (FPersonFatherName_textBox.Text != "" ? FPersonFatherName_textBox.Text : "لا يوجد") + "'"
                + ",P_MotherName = N'" + "لا يوجد" + "'" //Mother Name
                + ",P_Sex = N'" + (FPersonSexMale_radioButton.Checked ? "ذكر" : "أنثى") + "'"
                + ",P_NationalNumber = N'" + "null" + "'" //NationalNumber
                + ",P_DOB = N'" + FPersonDOB_dateTimePicker.Value.Month.ToString() + "/"
                                + FPersonDOB_dateTimePicker.Value.Day.ToString() + "/"
                                + FPersonDOB_dateTimePicker.Value.Year.ToString() + "'"
                + ",P_RegistrationPlace = N'" + "لا يوجد" + "'"
                + ",P_MaritalStatus = N'" + FPersonState_comboBox.Text + "'"
                + ",P_NumAtHome = " + "00" + ""
                + ",P_IsLivingWithFamily = N'" + "لا يوجد" + "'"
                + ",P_Email = N'" + "لا يوجد" + "'"
                + ",P_HomeAddress = N'" + "لا يوجد" + "'"
                + ",P_HomeTel = N'" + "لا يوجد" + "'"
                + ",P_Mobile = N'" + "لا يوجد" + "'"
                + ",IsProjectOwner = N'" + (FMicroProjectOwner_checkBox.Checked == true ? "YES" : "NO") + "' "
                + " where P_ID = " + PID;

            sc = new SqlCommand(updPersonQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        public void updateFamilyPersonDetails(int fNo, int pNo, string isInNow, string relation, int providerID, string WorkName, int PF_ID)
        {
            string updPFQuery = "Update Person_Family set "
                + "Family_ID = " + fNo + " "
                + ",Person_ID = " + pNo + " "
                                      + ",IsInNow = N'" + isInNow + "'"
                                      + ",Relation = N'" + relation + "'"
                                      + ",P_Provider_ID =" + providerID + " "
                                      + ",Work_Name = N'" + WorkName + "' "
                //       + " where Family_ID =" + fNo + " and Person_ID = " + pNo + " ";
                + " where PF_ID = " + PF_ID;
            SqlCommand updPFComm = new SqlCommand(updPFQuery, Program.MyConn);
            updPFComm.ExecuteNonQuery();
        }

        #endregion updates

        #region deletes
        private void deleteFamily(int FID)
        {
            string delFamilyQuery = "Delete from Family "
                                   + "where F_ID =" + FID;
            SqlCommand delFamilyComm = new SqlCommand(delFamilyQuery, Program.MyConn);
            delFamilyComm.ExecuteNonQuery();
        }

        ////////////////////////////////////////////////////////////////////////////////
        public void deleteFamilyMember(int PersonID)
        {
            string delPersonQuery = "delete From Person where P_ID =" + PersonID;
            sc = new SqlCommand(delPersonQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        public void deleteFamilyPerson(int FamilyPerson_ID)
        {
            string delPersonQuery = "delete from [Person_Family] where PF_ID = " + FamilyPerson_ID;
            sc = new SqlCommand(delPersonQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        #endregion deletes

        private void getCurrentFamilyID()
        {
            string query = "select Ident_current('[dbo].[Family]')";
            sc = new SqlCommand(query, Program.MyConn);
            Int32.TryParse(sc.ExecuteScalar().ToString(), out FamilyID);
        }
        private int getCurrentPersonID()
        {
            string query = "select Ident_current('Person')";
            sc = new SqlCommand(query, Program.MyConn);
            Int32.TryParse(sc.ExecuteScalar().ToString(), out PersonID);
            return PersonID;
        }

        #region Add buttons

        private void AddFamily_button_Click(object sender, EventArgs e)
        {
            try
            {
                insertFamily();

                l.Insert_Log("insert the family " + FamilyFName_textBox.Text + " " + FamilyLName_textBox.Text, "Family", username, DateTime.Now);

                DialogResult r = MessageBox.Show("الرجاء إضافة الشخص المستفيد من المشروع إلى العائلة قبل إضافة باقي الأفراد", "", MessageBoxButtons.OKCancel);
                if (r == DialogResult.OK)
                {
                    AllBeneficiaries AllBeneficiaries = new AllBeneficiaries();
                    MicroProjectOwnerDataRow = AllBeneficiaries.showSelectedPersonRow();

                    if (MicroProjectOwnerDataRow != null)
                    {
                        MicroProjectOwner_ID = (int)MicroProjectOwnerDataRow["رقم الشخص"];
                        //FamilyID = Int32.Parse(SelectedDataRow["رقم العائلة"].ToString());
                        getCurrentFamilyID();
                        //ADD the Micro Project Owner to his Family
                        insertFamilyPersonDetails(FamilyID, MicroProjectOwner_ID, "YES", "لا يوجد", MicroProjectOwner_ID, "لا يوجد");
                        r = MessageBox.Show("تم إضافة الفرد المستفيد بنجاح");
                        l.Insert_Log("insert the family member" + FPersonFName_textBox.Text + " " + FPersonLName_textBox.Text, " ", username, DateTime.Now);

                        FamilyMember_bind("", "");
                    }
                }
                FamilyFName_textBox.Clear();
                FamilyLName_textBox.Clear();
                FamilyFaName_textBox.Clear();

                FamilyNum_txtBox.Text = FamilyNum_textBox.Text;
                FamilyNum_textBox.Clear();
            }
            catch (NoNullAllowedException)
            { MessageBox.Show("اختر العائلة التي تريد إضافة أفراد لها"); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        ///////////////////////////////////////////////////////////////////////////////
        private void AddFamilyMember_button_Click(object sender, EventArgs e)
        {
            try
            {
                insertFamilyMember();
                relationInFamily = WorkName = "لا يوجد";

                if (FamilyID == -1)
                {
                    throw new Exception("اختر العائلة التي تريد إضافة أفراد عليها");
                }
                //FamilyID = Int32.Parse(AddNewFamily_Form.SelectedDataRow["رقم العائلة"].ToString());
                //PersonID = Int32.Parse(FPersonID_textBox.Text);

                int n = getCurrentPersonID();
              //  FPersonID_textBox.Text = (++n).ToString();

                activeInFamily = (FPersonActiveInFamily_radioButton.Checked ? "Yes" : "No");
                MP_Owner = (FMicroProjectOwner_checkBox.Checked ? "Yes" : "No");
                if (FPersonFamilyStatus_comboBox.SelectedIndex != -1)
                    relationInFamily = FPersonFamilyStatus_comboBox.SelectedItem.ToString();
                if (FPersonWorkDescribtion_textBox.Text != "")
                    WorkName = FPersonWorkDescribtion_textBox.Text;
                if (FPersonIsProvider_checkBox.Checked) //if the current person is the provider for himself
                {
                    ProviderID = PersonID;
                    insertFamilyPersonDetails(FamilyID, PersonID, activeInFamily, relationInFamily, ProviderID, WorkName);
                }
                string strItem = string.Empty;
                foreach (DataRowView selecteditemRow in FProviderID_listBox.SelectedItems) //Find the Providers
                {
                    strItem = selecteditemRow.Row["P_ID"].ToString();
                    ProviderID = Int32.Parse(strItem);
                    insertFamilyPersonDetails(FamilyID, PersonID, activeInFamily, relationInFamily, ProviderID, WorkName);
                }

                l.Insert_Log("insert the family member" + FPersonFName_textBox.Text + " " + FPersonLName_textBox.Text, " ", username, DateTime.Now);
                bindPersonIntoProvider();
                //FamilySection_Load(sender, e);
                FamilyMember_bind("", "");
                clearBoxes();
                n = getCurrentPersonID();
                FPersonID_textBox.Text = (++n).ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("اختر العائلة التي تريد إضافة أفراد عليها"))
                    MessageBox.Show("اختر العائلة التي تريد إضافة أفراد عليها");
                else MessageBox.Show(ex.Message);
            }
        }

        #endregion Add buttons

        #region update buttons

        private void UpdateFamily_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (FamilyNum_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                //int n = 0;
                //Int32.TryParse(FamilyID_textBox1.Text,out n);
                updateFamily(FamilyID);

                l.Insert_Log("update the family " + FamilyFName_textBox.Text + " " + FamilyLName_textBox.Text, "Family", username, DateTime.Now);
                DialogResult r = MessageBox.Show("هل تريد إضافة الشخص المستفيد من المشروع إلى العائلة قبل إضافة باقي الأفراد", "", MessageBoxButtons.OKCancel);
                if (r == DialogResult.OK)
                {
                    AllBeneficiaries AllBeneficiaries = new AllBeneficiaries();
                    MicroProjectOwnerDataRow = AllBeneficiaries.showSelectedPersonRow();

                    if (MicroProjectOwnerDataRow != null)
                    {
                        MicroProjectOwner_ID = (int)MicroProjectOwnerDataRow["رقم الشخص"];
                        //FamilyID = Int32.Parse(SelectedDataRow["رقم العائلة"].ToString());
                        getCurrentFamilyID();
                        //ADD the Micro Project Owner to his Family
                        insertFamilyPersonDetails(FamilyID, MicroProjectOwner_ID, "YES", "لا يوجد", MicroProjectOwner_ID, "لا يوجد");
                        r = MessageBox.Show("تم إضافة الفرد المستفيد بنجاح");
                        l.Insert_Log("insert the family member" + FPersonFName_textBox.Text + " " + FPersonLName_textBox.Text, " ", username, DateTime.Now);

                        FamilyMember_bind("","");
                    }
                }
                FamilyFName_textBox.Clear();
                FamilyLName_textBox.Clear();
                FamilyFaName_textBox.Clear();
                FamilyNum_textBox.Clear();
                //Family_bind("", "", "");
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("اختر العائلة التي تريد التعديل عليها");
            }
        }

        private void UpdateFamilyMember_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                MP_Owner = (FMicroProjectOwner_checkBox.Checked ? "Yes" : "No");
                //    if (FPersonFamilyStatus_comboBox.SelectedIndex != -1)
                updateFamilyMember(PersonID);

                //relationInFamily = WorkName = "لا يوجد";
                //if (FamilyID == -1)
                //{
                //    throw new Exception("اختر العائلة التي تريد إضافة أفراد عليها");
                //}
                //PersonID = Int32.Parse(FPersonID_textBox.Text);

                //activeInFamily = (FPersonActiveInFamily_radioButton.Checked ? "Yes" : "No");
                
                //relationInFamily = FPersonFamilyStatus_comboBox.Text;
                //if (FPersonWorkDescribtion_textBox.Text != "")
                //    WorkName = FPersonWorkDescribtion_textBox.Text;
                //if (FPersonIsProvider_checkBox.Checked) //if the current person is the provider for himself
                //{
                //    ProviderID = PersonID;
                //    updateFamilyPersonDetails(FamilyID, PersonID, activeInFamily, relationInFamily, ProviderID, WorkName,P);
                //}
                //string strItem = string.Empty;
                //foreach (DataRowView selecteditemRow in FProviderID_listBox.SelectedItems) //Find the Providers
                //{
                //    strItem = selecteditemRow.Row["P_ID"].ToString();
                //    ProviderID = Int32.Parse(strItem);
                //    updateFamilyPersonDetails(FamilyID, PersonID, activeInFamily, relationInFamily, ProviderID, WorkName);
                //}

                l.Insert_Log("Update " + FPersonFName_textBox.Text + " " + FPersonLName_textBox.Text, " Family Member ", username, DateTime.Now);

                //AddFamilyMember_Form_Load(sender, e);
                bindPersonIntoProvider();
                FamilyMember_bind("", "");
                clearBoxes();
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("اختر العائلة التي تريد إضافة أفراد عليها"))
                    MessageBox.Show("اختر العائلة التي تريد إضافة أفراد عليها");
                else MessageBox.Show(ex.Message);
            }
        }

        #endregion update buttons

        #region delete buttons

        private void DeleteFamily_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (FamilyNum_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                //int n = 0;
                //Int32.TryParse(FamilyID_textBox1.Text, out n);
                deleteFamily(FamilyID);

                l.Insert_Log("delete the family " + FamilyFName_textBox.Text + " " + FamilyLName_textBox.Text, "Family", username, DateTime.Now);

                FamilyFName_textBox.Clear();
                FamilyLName_textBox.Clear();
                FamilyFaName_textBox.Clear();
                FamilyNum_textBox.Clear();
                // Family_bind("", "", "");
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("اختر العائلة التي تريد حذفها");
            }
        }

        private void DeleteFamilyMember_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                deleteFamilyMember(PersonID);

                l.Insert_Log("Delete " + FPersonFName_textBox.Text + " " + FPersonLName_textBox.Text, " Family Member ", username, DateTime.Now);

                //AddFamilyMember_Form_Load(sender, e);
                bindPersonIntoProvider();
                FamilyMember_bind("", "");
                clearBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemovePersonFamily_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (FP_SelectedDataRow == null || FamilyPerson_ID == -1)
                {
                    throw new Exception("اختر السطر الذي تريد حذفه");
                }
                deleteFamilyPerson(FamilyPerson_ID);

                l.Insert_Log("Delete a family relation ", " Family Member ", username, DateTime.Now);

                Family_FamilyMember_bind("");
                FamilyPerson_ID = -1;
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("اختر السطر الذي تريد حذفه"))
                    MessageBox.Show("اختر السطر الذي تريد حذفه");
                MessageBox.Show(ex.Message);
            }
        }

        #endregion delete buttons

        #region datagridview select
        private void PersonDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)PersonDataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                PersonID = Int32.Parse(SelectedDataRow["رقم الشخص"].ToString());
                FPersonID_textBox.Text = PersonID.ToString();
                FPersonFName_textBox.Text = (string)SelectedDataRow["الاسم"];
                FPersonLName_textBox.Text = (string)SelectedDataRow["الكنية"];
                FPersonFatherName_textBox.Text = (string)SelectedDataRow["اسم الأب"];

                string P_sex = (string)SelectedDataRow["الجنس"];
                if (P_sex.Contains(@"ذكر"))
                    FPersonSexMale_radioButton.Checked = true;
                else
                    FPersonSexFemale_radioButton.Checked = true;

                FPersonDOB_dateTimePicker.Value = (DateTime)SelectedDataRow["تاريخ الولادة"];
                FPersonState_comboBox.Text = (string)SelectedDataRow["الحالة الاجتماعية"];

                string MicroProjectOwner = (string)SelectedDataRow["مستفيد من مشروع"];
                if (MicroProjectOwner.Equals("YES"))
                    FMicroProjectOwner_checkBox.Checked = true;
                else
                    FMicroProjectOwner_checkBox.Checked = true;

                //string IsInNow = (string)SelectedDataRow["فعال"];
                //if (IsInNow.Contains("Y"))
                //    FPersonActiveInFamily_radioButton.Checked = true;
                //else
                //    FPersonNotActiveInFamily_radioButton.Checked = true;
                //FPersonState_comboBox.Text = (string)SelectedDataRow["علاقته مع المستفيد"];
                //FPersonWorkDescribtion_textBox.Text = (string)SelectedDataRow["العمل"];
                //FamilyID = Int32.Parse(SelectedDataRow["رقم دفتر العائلة"].ToString());
            }
        }

        private void FamilyPerson_dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FP_SelectedDataRow = ((DataRowView)FamilyPerson_dataGridView.CurrentRow.DataBoundItem).Row;

            FamilyPerson_ID = Int32.Parse(FP_SelectedDataRow["رقم العائلة"].ToString());
        }
        private void FamilyPersonRelation_dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)FamilyPersonRelation_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                FamilyPerson_ID = Int32.Parse(SelectedDataRow["رقم السجل"].ToString());

                FamilyNum_txtBox1.Text = (string)SelectedDataRow["رقم دفتر العائلة"];
                FamilyID = Int32.Parse(SelectedDataRow["رقم العائلة"].ToString());
                
                FamilyNum_txtBox1.Text = FamilyID.ToString();

                PersonID = Int32.Parse(SelectedDataRow["رقم الشخص"].ToString());
                FPersonID_textBox1.Text = PersonID.ToString();

                FPersonFamilyStatus_comboBox1.Text = (string)SelectedDataRow["علاقته مع المستفيد"];

                FPersonWorkDescribtion_textBox1.Text = (string)SelectedDataRow["عمله الحالي"];

                ProviderID = Int32.Parse(SelectedDataRow["رقم المعيل"].ToString());
                if (ProviderID == PersonID)
                    FPersonIsProvider_checkBox1.Checked = true;
                else
                    FPersonIsProvider_checkBox1.Checked = false;

                string IsInNow = (string)SelectedDataRow["فعال"];
                if (IsInNow.Contains("Y"))
                    FPersonActiveInFamily_radioButton1.Checked = true;
                else
                    FPersonNotActiveInFamily_radioButton1.Checked = true;

            }
        }

        #endregion datagridview select

        #region filters (textbox change)

        private void FamilyNum_textBox1_TextChanged(object sender, EventArgs e)
        {
            Family_FamilyMember_bind(FamilyNum_textBox1.Text);
        }
        private void fnameTxtBox_TextChanged(object sender, EventArgs e)
        {
            FamilyMember_bind(fnameTxtBox.Text, lNameTxtBox.Text);
        }

        private void lNameTxtBox_TextChanged(object sender, EventArgs e)
        {
            FamilyMember_bind(fnameTxtBox.Text, lNameTxtBox.Text);
        }

        private void nationalNumberTxtBox_TextChanged(object sender, EventArgs e)
        {
            FamilyMember_bind(fnameTxtBox.Text, lNameTxtBox.Text);
        }

        private void FPersonSexMale_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            PersonState_bind();
        }

        private void FPersonSexFemale_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            PersonState_bind();
        }

        #endregion filters (textbox change)

        #region tabcontrol buttons

        private void Family_button_Click(object sender, EventArgs e)
        {
            FamilySection_tabControl.SelectedIndex = 0;
        }

        private void Members_button_Click(object sender, EventArgs e)
        {
            FamilySection_tabControl.SelectedIndex = 1;
        }

        private void FamilyAndMembers_button_Click(object sender, EventArgs e)
        {
            //FamilySection_tabControl.SelectedIndex = 2;
            ////family person bind
            //Family_FamilyMember_bind(FamilyNum_textBox1.Text);

            FamilySection_tabControl.SelectedIndex = 3;
            Family_bind();
            Person_bind();
            Family_FamilyMember_bind("");
            FProviderID_listBox1.SelectedIndex = -1;
        }

        #endregion tabcontrol buttons

        private void clearBoxes()
        {
            FPersonFName_textBox.Text = FPersonLName_textBox.Text = FPersonFatherName_textBox.Text = FPersonWorkDescribtion_textBox.Text = FPersonWorkDescribtion_textBox1.Text = "";
            FMicroProjectOwner_checkBox.Checked = FPersonIsProvider_checkBox.Checked = FPersonIsProvider_checkBox1.Checked = false;
            FPersonDOB_dateTimePicker.Value = DateTime.Now;
            FPersonSexMale_radioButton.Checked = FPersonSexFemale_radioButton.Checked = false;
            PersonState_bind();
            FProviderID_listBox.SelectedIndex = FProviderID_listBox1.SelectedIndex = -1;
            
            FamilyPerson_ID = -1;
            ProviderID = -1;
        }

        #region save - delete

        private void AddSave_button_MouseEnter(object sender, EventArgs e)
        {
            InsertFamily_button.BackgroundImage = Properties.Resources.save;
        }
        private void AddSave_button_MouseLeave(object sender, EventArgs e)
        {
            InsertFamily_button.BackgroundImage = InsertFamilyMember_button.BackgroundImage = Properties.Resources.save0;
        }
        private void UpdateSave_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateFamily_button.BackgroundImage =Properties.Resources.save;
        }
        private void UpdateSave_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateFamily_button.BackgroundImage = UpdateFamilyMember_button.BackgroundImage = Properties.Resources.save0;
        }
        private void delete_button_MouseEnter(object sender, EventArgs e)
        {
            RemovePersonFamily_button.BackgroundImage = DeleteFamilyMember_button.BackgroundImage = Properties.Resources.delete;
        }

        private void delete_button_MouseLeave(object sender, EventArgs e)
        {
            RemovePersonFamily_button.BackgroundImage = DeleteFamilyMember_button.BackgroundImage = Properties.Resources.delete0;
        }
        
        private void add_button_MouseEnter(object sender, EventArgs e)
        {
            InsertFamilyMember_button.BackgroundImage = Properties.Resources.add;
        }

        private void add_button_MouseLeave(object sender, EventArgs e)
        {
            InsertFamilyMember_button.BackgroundImage = Properties.Resources.add0;
        }
         private void update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateFamilyMember_button.BackgroundImage = Properties.Resources.update;
        }
        private void update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateFamilyMember_button.BackgroundImage = Properties.Resources.update0;
        }
        #endregion save - delete

        #region change btn backgrounds

        private void FamilyAndMembers_button_MouseEnter(object sender, EventArgs e)
        {
            FamilyAndMembers_button.BackgroundImage = Properties.Resources.family3;
        }

        private void FamilyAndMembers_button_MouseLeave(object sender, EventArgs e)
        {
            FamilyAndMembers_button.BackgroundImage = null;
        }

        private void Members_button_MouseEnter(object sender, EventArgs e)
        {
            Members_button.BackgroundImage = Properties.Resources.family2;
        }

        private void Members_button_MouseLeave(object sender, EventArgs e)
        {
            Members_button.BackgroundImage = null;
        }

        private void Family_button_MouseEnter(object sender, EventArgs e)
        {
            Family_button.BackgroundImage = Properties.Resources.family1;
        }

        private void Family_button_MouseLeave(object sender, EventArgs e)
        {
            Family_button.BackgroundImage = null;
        }

        #endregion change btn backgrounds

        private void AddFamilyRelation_button_Click(object sender, EventArgs e)
        {
            //string IsInNow = "";
            //if (FPersonActiveInFamily_radioButton1.Checked)
            //    IsInNow = "YES";
            //else
            //    IsInNow = "NO";

            //if (FPersonIsProvider_checkBox1.Checked) //if the current person is the provider for himself
            //{
            //    ProviderID = PersonID;
            //    insertFamilyPersonDetails(FamilyID, Int32.Parse(FPersonID_textBox1.Text), IsInNow, FPersonFamilyStatus_comboBox1.Text, ProviderID, FPersonWorkDescribtion_textBox1.Text);
            //}
            //string strItem = string.Empty;
            //foreach (DataRowView selecteditemRow in FProviderID_listBox1.SelectedItems) //Find the Providers
            //{
            //    strItem = selecteditemRow.Row["P_ID"].ToString();
            //    ProviderID = Int32.Parse(strItem);
            //    insertFamilyPersonDetails(FamilyID, Int32.Parse(FPersonID_textBox1.Text), IsInNow, FPersonFamilyStatus_comboBox1.Text, ProviderID, FPersonWorkDescribtion_textBox1.Text);
            //}

            //l.Insert_Log("insert the family member" + FPersonFName_textBox.Text + " " + FPersonLName_textBox.Text, " ", username, DateTime.Now);

            //Family_FamilyMember_bind("");

            //clearBoxes();
        }

        private void UpdateFamilyRelation_button_Click(object sender, EventArgs e)
        {
            PersonID = Int32.Parse(FPersonID_textBox1.Text);

            string IsInNow = (FPersonActiveInFamily_radioButton1.Checked ? "Yes" : "No");

            relationInFamily = FPersonFamilyStatus_comboBox1.Text;
            if (FPersonWorkDescribtion_textBox.Text != "")
                WorkName = FPersonWorkDescribtion_textBox1.Text;
            if (FPersonIsProvider_checkBox1.Checked) //if the current person is the provider for himself
            {
                ProviderID = PersonID;
                updateFamilyPersonDetails(FamilyID, Int32.Parse(FPersonID_textBox1.Text), IsInNow, FPersonFamilyStatus_comboBox1.Text, ProviderID, FPersonWorkDescribtion_textBox1.Text, FamilyPerson_ID);
            }
            string strItem = string.Empty;
            foreach (DataRowView selecteditemRow in FProviderID_listBox1.SelectedItems) //Find the Providers
            {
                strItem = selecteditemRow.Row["P_ID"].ToString();
                ProviderID = Int32.Parse(strItem);
                updateFamilyPersonDetails(FamilyID, Int32.Parse(FPersonID_textBox1.Text), IsInNow, FPersonFamilyStatus_comboBox1.Text, ProviderID, FPersonWorkDescribtion_textBox1.Text, FamilyPerson_ID);
            }

         //   l.Insert_Log("Update " + FPersonFName_textBox.Text + " " + FPersonLName_textBox.Text, " Family Member ", username, DateTime.Now);
            
            Family_FamilyMember_bind("");
            clearBoxes();
        }

        private void DeleteFamilyRelation_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (FamilyPerson_ID == -1)
                {
                    throw new Exception("اختر السطر الذي تريد حذفه");
                }
                deleteFamilyPerson(FamilyPerson_ID);

                l.Insert_Log("Delete a family relation ", " Family Member ", username, DateTime.Now);

                Family_FamilyMember_bind("");
                FamilyPerson_ID = -1;
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("اختر السطر الذي تريد حذفه"))
                    MessageBox.Show("اختر السطر الذي تريد حذفه");
                MessageBox.Show(ex.Message);
            }
        }

        private void Family_dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
        private void MainBack0_button_Click(object sender, EventArgs e)
        {
            AllFamilies = new AllFamilies(username);
            AllFamilies.Show();
            this.Close();
        }



    }
}