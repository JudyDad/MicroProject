using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization; 
using ClosedXML.Excel;
using System.Windows.Forms;
using MyWorkApplication.Classes;
using MySql.Data.MySqlClient;
using System.Linq;
using MyWorkApplication.Visit_Forms;
using MyWorkApplication.Properties;
using System.Text;
using System.Text.RegularExpressions;

namespace MyWorkApplication
{
    public partial class Search_Form2 : Form
    {
        public Search_Form2(MainForm main_form)
        {
            InitializeComponent();

            this.main_form = main_form;
            //this.SearchFor = SearchFor;
            bs.ListChanged += bs_ListChanged;
            Search_DataGridView.DoubleBuffered(true);
            Hide_MenuItem.DropDown.Closing += Hide_MenuItem_Closing;
            Show_MenuItem.DropDown.Closing += Show_MenuItem_Closing;
        }

        //  string SearchFor;
        bool Additional_Filters_Applied = false, Additional_Filters_Opened = false;
        NewTheme NewTheme;
        Select s;
        Log l;
        Street st;
        SubCategory sub;
        List<string> searchBy_list;
        MainForm main_form;
        DataRow SelectedDataRow;
        BindingSource bs = new BindingSource();
        int tick1 = 0;
        bool UserMode = true;
        private bool Hided;
        private int PW;

        private string ApplyDate_condition = "";
        private string FundedAmount_condition = "";
        private string FundedDate_condition = "";
        private string RequestedAmount_condition = "";
        private string visitDate_condition = "";
        private string mevisitDate_condition = "";

        string Filter1_where_in = "", Filter2_where_in = "", Filter3_where_in = "", Filter4_where_in = "";
        string Original_Filter_String = "";

        bool show_images;
        private int MicroProject_ID, Person_ID, PMP_ID, Family_ID, Image_ID, ExeFile_ID;

        private void Search_Form2_Load(object sender, EventArgs e)
        {
            try
            {
                l = new Log();
                s = new Select();
                st = new Street();
                sub = new SubCategory();
                NewTheme = new NewTheme();
                if (Settings.Default.theme == "Light") NewTheme.Search_ToLight(this, false);
                else NewTheme.Search_ToNight(this, false);

                PW = Search_panel.Height;
                Hided = true;
                Search_panel.Height = 0;

                UserMode = false;
                searchBy_list = new List<string>();
                searchBy_list.Add("Applications");
                searchBy_list.Add("Families");
                searchBy_list.Add("Loans and Payments");
                searchBy_list.Add("Attachments");
                searchBy_list.Add("Executive Files");
                searchBy_list.Add("M&E Visits");
                searchBy_list.Add("Monitoring Visits(1 to 5)");
                searchBy_list.Add("Check Lists");

                for (int i = 0; i < searchBy_list.Count; i++)
                    SearchBy_comboBox.Items.Add(searchBy_list.ElementAt<string>(i));

                CheckUserPermission();

                //Category_bind();
                //SubCategory_bind(""); //to select the subCategory when Fill MicroProject//
                //Street_bind();
                //Donor_bind();

                SearchBy_comboBox.SelectedIndex = 0;
                Bind_Search_DataGridView();
                

                //Main_TableLayoutPanel.Size = new Size(this.Width, 1006);

                //Filter1_ComboBox.MouseWheel += new MouseEventHandler(ComboBox_MouseWheel);
                //Filter2_ComboBox.MouseWheel += new MouseEventHandler(ComboBox_MouseWheel);
                //Filter3_ComboBox.MouseWheel += new MouseEventHandler(ComboBox_MouseWheel);
                //Filter4_ComboBox.MouseWheel += new MouseEventHandler(ComboBox_MouseWheel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                MessageBox.Show("لن يتم فتح هذه الصفحة بسبب خطأ في جلب البيانات، يرجى محاولة إعادة فتحها من جديد", "Error");
                this.Close();
            }
        }

        private void CheckUserPermission()
        {
            switch (Settings.Default.role)
            {
                case 1: //admin 
                case 8: //admin l0
                case 3: //Financial
                case 5: //manager 
                    {
                        //SearchBy_comboBox.Items.Insert(2,"Loans and Payments");
                    }
                    break;
                case 2: //Data
                case 4: //Guest
                case 6: //Out Of Service
                case 7: //Lawful
                    {
                        SearchBy_comboBox.Items.Remove("Loans and Payments");
                    }
                    break;
            }
        }

        private void Search_Form_Shown(object sender, EventArgs e)
        {
            Search_DataGridView.SetFilterAndSortEnabled(Search_DataGridView.Columns["Logo"], false); 
        }

        #region queries binding
        private void Application_bind(string Age, string ApplyYear, string FundYear, bool showImages)
        {
            string query = @"select 
 PMP.PMP_ID as 'ID'
 , P.P_ID as 'رقم المستفيد'
 , MP_ID as 'رقم المشروع'
 , CONCAT(P.P_FirstName, ' ', P.P_LastName) as 'المستفيد'
 , P.P_FatherName as 'اسم الأب'
 , P.P_MotherName as 'اسم الأم'
 , (SELECT CONCAT(p.P_FirstName, ' ', p.P_FatherName, ' ', p.P_LastName) From `person` p join `person_family` pf on p.P_ID = pf.Person_ID where pf.Relation like N'زوج/ة' and pf.P_Provider_ID = PMP.Person_ID limit 1) as 'الزوج / الزوجة'
 , MP.MP_Name as 'اسم المشروع'
 , C_Name 'الصنف'
 , Sub.Name as 'المهنة'
 , P.P_Parish as 'الطائفة'
 , IFNULL(Priest_Name, '') as 'الكاهن'
 , h_street.Name as 'منطقة السكن'
 , P.P_HomeAddress as 'العنوان'
 , P.P_RegistrationPlace as 'القيد'
 , P.P_NationalNumber as 'الرقم الوطني'
 , P.P_Mobile as 'الموبايل'
 , P.P_NumAtHome as 'عدد الأفراد'
 , MP.MP_DateOfRequest as 'تاريخ التقديم'
 , fundtype.Name as 'نوع التمويل'
 , microprojecttype.Name as 'نوع المشروع'
 , microprojectsubtype.Name as 'تفصيل نوع المشروع'
 , state.Name_Ar as 'حالة المشروع'
 , IFNULL(MP.MP_StateDate, '') as 'تاريخ تغيير الحالة'
 , IFNULL(L.Loan_Amount, '') as 'مبلغ التمويل'
 , IFNULL(L.Loan_DateTaken, '') as 'تاريخ التمويل'
 , IFNULL(donor.Name, '') as 'الجهة الممولة'
 , IFNULL(donorgroup.Name, '') as 'المجموعة'
 , IFNULL(w_street.Name, '') 'منطقة المشروع'
 , IFNULL(MP_AddressAfterFund, '') as 'موقع المشروع'
 , (CASE MP.MP_Visited WHEN 0 THEN N'غير مزار' ELSE N'مزار' End) as 'الزيارة'
 , (CASE MP.MP_Message WHEN 1 THEN N'تم الإرسال' ELSE 'لا يوجد' End) as 'رسالة الرفض'
 , (CASE MP.IsContentUpdated WHEN 1 THEN N'نعم' ELSE 'لا' End) as 'تبديل المشروع'
 , PMP.PersonType as 'صفة مقدم الطلب'";

            if (show_images) query += " ,P_Picture as '#',P_PicturePath as 'Path'";
            var condition = " where 1 ";
            var from = @" From `microproject` MP
 LEFT OUTER JOIN `person_microproject` PMP on PMP.MicroProject_ID = MP.MP_ID 
 LEFT OUTER JOIN `person` P on P.P_ID = PMP.Person_ID   
 LEFT OUTER JOIN `priest` on Priest_ID = P.P_Priest_ID
 LEFT OUTER JOIN `category` C on C.C_ID = MP.MP_Category_ID
 LEFT OUTER JOIN `subcategory` Sub on Sub.ID =  MP.SubCategory_ID 
 LEFT OUTER JOIN `state` on state.ID = MP.MP_State 
 LEFT OUTER JOIN `donor` on donor.ID = MP.MP_Donor
 LEFT OUTER JOIN `street` w_street on w_street.ID = MP.MP_Street_ID
 LEFT OUTER JOIN `street` h_street on h_street.ID = P.Street_ID  
 LEFT OUTER JOIN `loan` L on L.MicroProject_ID = MP.MP_ID
 LEFT OUTER JOIN `donorgroup` on donorgroup.ID = MP.DonorGroup_ID  
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID ";
            

            ////Age 
            if (Age != "")
            {
                if (Age.Contains("<16")) condition += " and  TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) < 16 ";
                else if (Age.Contains("16-26"))
                    condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 16 and 26 ";
                else if (Age.Contains("27-35"))
                    condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 27 and 35 ";
                else if (Age.Contains("36-45"))
                    condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 36 and 45 ";
                else if (Age.Contains("46-60"))
                    condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 46 and 60 ";
                else if (Age.Contains(">60")) condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) > 60 ";
            }
            
            //Apply Date
            if (ApplyYear != "") condition += " and Year(MP.MP_DateOfRequest) like '" + ApplyYear + "'";

            //Fund Date
            if (FundYear != "") condition += " and Year(L.Loan_DateTaken) like '" + FundYear + "'";

            if (ApplyDate_condition != "") condition += ApplyDate_condition;
            if (FundedDate_condition != "") condition += FundedDate_condition;
            if (RequestedAmount_condition != "") condition += RequestedAmount_condition;
            if (FundedAmount_condition != "") condition += FundedAmount_condition;

            string order_by = " order by MP_ID DESC ";

            query += from + condition + order_by + ";";
             
            Program.buildConnection();
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            bs.DataSource = dt;
            Search_DataGridView.ColumnHeadersVisible = false;
            Search_DataGridView.DataSource = null;
            Search_DataGridView.Columns.Clear();
            Search_DataGridView.DataSource = bs;
            Search_DataGridView.ColumnHeadersVisible = true;

            Search_DataGridView.Columns["تاريخ التقديم"].DefaultCellStyle.Format
                = Search_DataGridView.Columns["تاريخ تغيير الحالة"].DefaultCellStyle.Format = "yyyy/MM/dd";
            Search_DataGridView.Columns["مبلغ التمويل"].DefaultCellStyle.Format = "#,##0";
            DataGridViewColumn dgC1;
            if (Search_DataGridView.RowCount > 0 && Search_DataGridView.CurrentRow != null)
            {
                dgC1 = Search_DataGridView.Columns["رقم المستفيد"];
                dgC1.Visible = false;
                dgC1 = Search_DataGridView.Columns["ID"];
                dgC1.Visible = false;
            }
            Search_DataGridView.Columns["رقم المشروع"].Width = 70;
            Search_DataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                Search_DataGridView.Columns["الزيارة"].DefaultCellStyle.Alignment =
                    Search_DataGridView.Columns["تاريخ التقديم"].DefaultCellStyle.Alignment =
                        Search_DataGridView.Columns["الصنف"].DefaultCellStyle.Alignment =
                            Search_DataGridView.Columns["نوع المشروع"].DefaultCellStyle.Alignment =
                                Search_DataGridView.Columns["الطائفة"].DefaultCellStyle.Alignment =
                                    Search_DataGridView.Columns["الموبايل"].DefaultCellStyle.Alignment =
                                        Search_DataGridView.Columns["الرقم الوطني"].DefaultCellStyle.Alignment =
                                            Search_DataGridView.Columns["حالة المشروع"].DefaultCellStyle.Alignment =
                                                Search_DataGridView.Columns["الجهة الممولة"].DefaultCellStyle.Alignment =
                                                    Search_DataGridView.Columns["عدد الأفراد"].DefaultCellStyle.Alignment =
                                                    DataGridViewContentAlignment.MiddleCenter;
            if (show_images)
            {
                var column = Search_DataGridView.Columns["#"];
                column.Width = 70;
                ((DataGridViewImageColumn)Search_DataGridView.Columns["#"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            Search_DataGridView.ColumnHeadersVisible = true;
        }

        private void FamilyMember_bind(string family_ID)
        {
            //check connection//
            Program.buildConnection();
            string query = @" SELECT p.P_ID as 'ID' 
  ,pf.Family_ID as 'رقم العائلة' 
  ,f.F_Number as 'رقم دفتر العائلة' 
  ,CONCAT(p.P_FirstName, ' ', p.P_FatherName, ' ', p.P_LastName) as 'المستفيد' 
  ,p.P_MotherName as 'اسم الأم' 
  ,p.P_MaritalStatus as 'الحالة الاجتماعية' 
  ,pf.IsInNow as 'تابع لدفتر عائلة المستفيد' 
  ,pf.Relation as 'العلاقة مغ المستفيد' 
  ,pf.Work_Name as 'العمل' 
  ,p.P_DOB as 'تاريخ الولادة' 
  ,pf.P_Provider_ID as 'رقم المستفيد' ";
                         
            string from = @" From `person` p  
 left outer join `person_family` pf on p.P_ID = pf.Person_ID  
 left outer join `family` f on f.F_ID = pf.Family_ID ";

            string condition = "";
             
            query += from + condition;
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            /////////////////////////////
            bs.DataSource = dt;
            Search_DataGridView.ColumnHeadersVisible = false;
            Search_DataGridView.DataSource = null;
            Search_DataGridView.Columns.Clear();
            Search_DataGridView.DataSource = bs;
            Search_DataGridView.ColumnHeadersVisible = true;
            //////////////////////////////

            var dgC1 = Search_DataGridView.Columns["ID"];
            var dgC2 = Search_DataGridView.Columns["رقم المستفيد"];
            Search_DataGridView.Columns["تاريخ الولادة"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgC1.Visible = dgC2.Visible = false;
             
            //count rows
            var sel = "select count(p.P_ID) " + from + condition;
            sc = new MySqlCommand(sel, Program.MyConn);
            decimal count = Convert.ToDecimal(sc.ExecuteScalar());
            Count_label.Text = "All:" + count.ToString("#,##0");
            Program.MyConn.Close();

            Search_DataGridView.Columns["رقم العائلة"].Width = Search_DataGridView.Columns["تابع لدفتر عائلة المستفيد"].Width =
                Search_DataGridView.Columns["تاريخ الولادة"].Width = 60;
            Search_DataGridView.Columns["رقم دفتر العائلة"].Width = Search_DataGridView.Columns["اسم الأم"].Width =
                Search_DataGridView.Columns["العلاقة مغ المستفيد"].Width =
                    Search_DataGridView.Columns["الحالة الاجتماعية"].Width = 100;

            Search_DataGridView.Columns["رقم العائلة"].DefaultCellStyle.Alignment =
                Search_DataGridView.Columns["رقم دفتر العائلة"].DefaultCellStyle.Alignment =
                    Search_DataGridView.Columns["تاريخ الولادة"].DefaultCellStyle.Alignment =
                        Search_DataGridView.Columns["الحالة الاجتماعية"].DefaultCellStyle.Alignment =
                            Search_DataGridView.Columns["العلاقة مغ المستفيد"].DefaultCellStyle.Alignment =
                                Search_DataGridView.Columns["تابع لدفتر عائلة المستفيد"].DefaultCellStyle.Alignment =
                                    DataGridViewContentAlignment.MiddleCenter;
        }

        private void Loan_bind(string Type, string State, string Category, string Donor, string DonorGroup, string Partners, string ApplyYear, string FundYear)
        {
            DataTable dt = s.Select_Loan_All_Data("", "", Type, "", State, Partners, Donor, DonorGroup, Category, "", ApplyYear, FundYear);

            bs.DataSource = dt;
            Search_DataGridView.ColumnHeadersVisible = false;
            Search_DataGridView.DataSource = null;
            Search_DataGridView.Columns.Clear();
            Search_DataGridView.DataSource = bs;
          
             
            Search_DataGridView.Columns["تاريخ استلام القرض"].DefaultCellStyle.Format
                = Search_DataGridView.Columns["تاريخ آخر دفعة مدفوعة"].DefaultCellStyle.Format
                    = Search_DataGridView.Columns["تاريخ الدفعة المستحقة"].DefaultCellStyle.Format = "dd/MM/yyyy";

            Search_DataGridView.Columns["القرض"].DefaultCellStyle.Format
                = Search_DataGridView.Columns["القرض ($)"].DefaultCellStyle.Format
                    = Search_DataGridView.Columns["القسط"].DefaultCellStyle.Format
                        = Search_DataGridView.Columns["المبلغ المدفوع"].DefaultCellStyle.Format
                            = Search_DataGridView.Columns["المبلغ المتبقي"].DefaultCellStyle.Format
                                = Search_DataGridView.Columns["المبلغ المطلوب استرداده"].DefaultCellStyle.Format = "#,##0";

            Search_DataGridView.Columns["عدد الدفعات المتبقية"].DefaultCellStyle.Format = "N1";

            var dgc1 = Search_DataGridView.Columns["ID"];
            dgc1.Visible = false;

            decimal count = Convert.ToDecimal(s.Count_Distinct_Loan_IDs());
            Count_label.Text = "All:" + count.ToString("#,##0");

            Search_DataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                Search_DataGridView.Columns["القسط"].DefaultCellStyle.Alignment =
                    Search_DataGridView.Columns["عدد الدفعات المتبقية"].DefaultCellStyle.Alignment =
            Search_DataGridView.Columns["عدد الدفعات المدفوعة"].DefaultCellStyle.Alignment =
                Search_DataGridView.Columns["عدد الدفعات الكلي"].DefaultCellStyle.Alignment =
                    Search_DataGridView.Columns["تاريخ استلام القرض"].DefaultCellStyle.Alignment =
                        Search_DataGridView.Columns["القرض"].DefaultCellStyle.Alignment =
            Search_DataGridView.Columns["القرض ($)"].DefaultCellStyle.Alignment =
                Search_DataGridView.Columns["النسبة"].DefaultCellStyle.Alignment =
                    Search_DataGridView.Columns["المجموعة"].DefaultCellStyle.Alignment =
            Search_DataGridView.Columns["رقم الإيصال"].DefaultCellStyle.Alignment =
                Search_DataGridView.Columns["المبلغ المطلوب استرداده"].DefaultCellStyle.Alignment =
                    Search_DataGridView.Columns["المبلغ المدفوع"].DefaultCellStyle.Alignment =
                        Search_DataGridView.Columns["تاريخ آخر دفعة مدفوعة"].DefaultCellStyle.Alignment =
                            Search_DataGridView.Columns["تاريخ الدفعة المستحقة"].DefaultCellStyle.Alignment =
                                Search_DataGridView.Columns["المبلغ المتبقي"].DefaultCellStyle.Alignment =
                                Search_DataGridView.Columns["أشهر التأخير"].DefaultCellStyle.Alignment =
                                Search_DataGridView.Columns["الدفعات المستحقة والغير مدفوعة"].DefaultCellStyle.Alignment =
             DataGridViewContentAlignment.MiddleCenter;

            Search_DataGridView.ColumnHeadersVisible = true;
        }

        private void bind_Images(string Type, string State, string Category, string Donor, string DonorGroup, bool showImages)
        {
            //check connection//
            Program.buildConnection();

            var from = @" from `image` 
    left join `microproject` MP on image.MicroProject_ID = MP.MP_ID  
    LEFT join `person_microproject` PMP on PMP.MicroProject_ID = MP.MP_ID 
    LEFT join `category` C on MP.MP_Category_ID = C.C_ID  
    LEFT join `state` on state.ID = MP.MP_State 
    Left join `donor` on donor.ID = MP.MP_Donor
    LEFT join `loan` on loan.MicroProject_ID = MP.MP_ID
    LEFT join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID ";

            string query = "SELECT Image_ID as 'ID'" +
                         ",image.MicroProject_ID as 'رقم المشروع'" +
                         ",Image_Path as 'المسار'" +
                         ",Image_Type as 'النوع'";

            if (show_images) query += " ,Image_Content as '#'";

            var condition = " where 1 ";
 

            //MP State
            if (State != "")
            {
                if (State == "ممول+منتهي")
                    condition += " and state.ID in (4,5) ";
                else if (State == "ممول+منتهي+ملغى")
                    condition += " and state.ID in (4,5,7) ";
                else
                    condition += " and state.Name_Ar like N'" + State + "' ";
            }

            //Donor
            if (Donor != "")
                condition += " and donor.Name like N'" + Donor + "'";

            //Donor Group
            if (DonorGroup != "")
                condition += " and donorgroup.Name like '" + DonorGroup + "'";

            //Project Category
            if (Category != "")
                condition += " and C.C_Name like N'" + Category + "'";
              
            query += from + condition;
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            bs.DataSource = dt;
            Search_DataGridView.ColumnHeadersVisible = false;
            Search_DataGridView.DataSource = null;
            Search_DataGridView.Columns.Clear();
            Search_DataGridView.DataSource = bs; 
             
            var dgC2 = Search_DataGridView.Columns["ID"];
            dgC2.Visible = false;
             
            //count rows
            var sel = "select count(DISTINCT MP_ID) " + from + condition;
            sc = new MySqlCommand(sel, Program.MyConn);
            decimal count = Convert.ToDecimal(sc.ExecuteScalar());
            Count_label.Text = "All:" + count.ToString("#,##0");
            Program.MyConn.Close();
             
            Search_DataGridView.Columns["رقم المشروع"].Width = Search_DataGridView.Columns["النوع"].Width = 80;

            Search_DataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                Search_DataGridView.Columns["النوع"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (show_images)
            {
                var column = Search_DataGridView.Columns["#"];
                column.Width = 70;
                ((DataGridViewImageColumn)Search_DataGridView.Columns["#"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            Search_DataGridView.ColumnHeadersVisible = true;
        }

        private void bind_ExeFile(string Type, string State, string Category, string Donor, string DonorGroup, string Partners)
        {
            //check connection//
            Program.buildConnection();
            var from = @" from `exefile` 
    left join `microproject` MP on exefile.MicroProject_ID = MP.MP_ID  
    LEFT join `person_microproject` PMP on PMP.MicroProject_ID = MP.MP_ID  
    LEFT join `person` P on P.P_ID = PMP.Person_ID  
    LEFT join `category` C on MP.MP_Category_ID = C.C_ID  
    Left join `donor` on donor.ID = MP.MP_Donor
    LEFT join `state` on state.ID = MP.MP_State
    LEFT join `loan` on loan.MicroProject_ID = MP.MP_ID
    LEFT join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID ";

            string query = " select PMP.MicroProject_ID as 'رقم المشروع'"
                         + " ,CONCAT(P.P_FirstName,' ', P.P_LastName) as 'المستفيد'"
                         + " ,P.P_FatherName as 'اسم الأب'"
                         + ",`ExeF_No` as 'رقم الملف'"
                         + ",`ExeF_BeginDate` as 'تاريخ البداية'"
                         + ",`ExeF_CurrentDate` as 'التاريخ الحالي'"
                         + ",`ExeF_NumOfMonths` as 'عدد الأشهر'"
                         + ", DATE_ADD(ExeF_CurrentDate, INTERVAL `ExeF_NumOfMonths` Month) as 'تاريخ الاستحقاق'"
                         + ",`ExeF_ImpoundDate` as 'تاريخ الحجز'"
                         + ",`ExeF_ImpoundType` as 'نوع الحجز' "
                         + ",`ExeF_ID` as 'ID'" + from;
            var condition = " where 1 ";

           

            //MP State
            if (State != "")
            {
                if (State == "ممول+منتهي")
                    condition += " and state.ID in (4,5) ";
                else if (State == "ممول+منتهي+ملغى")
                    condition += " and state.ID in (4,5,7) ";
                else
                    condition += " and state.Name_Ar like N'" + State + "' ";
            }

            //Donor
            if (Donor != "")
                condition += " and donor.Name like N'" + Donor + "'";

            //Donor Group
            if (DonorGroup != "")
                condition += " and donorgroup.Name like '" + DonorGroup + "'";

            //Project Category
            if (Category != "")
                condition += " and C.C_Name like N'" + Category + "'";

            //Partners
            if (Partners != "")
            {
                if (Partners.Contains("شراكة"))
                    condition += " and Partnership = 2 ";
                else if (Partners.Contains("فردي")) condition += " and Partnership = 1 ";
                else condition += "";
            }

            query += from + condition;
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            bs.DataSource = dt;
            Search_DataGridView.ColumnHeadersVisible = false;
            Search_DataGridView.DataSource = null;
            Search_DataGridView.Columns.Clear();
            Search_DataGridView.DataSource = bs; 

            Search_DataGridView.Columns["تاريخ البداية"].DefaultCellStyle.Format = "dd/MM/yyyy";
            Search_DataGridView.Columns["تاريخ الاستحقاق"].DefaultCellStyle.Format = "dd/MM/yyyy";
            Search_DataGridView.Columns["تاريخ الحجز"].DefaultCellStyle.Format = "dd/MM/yyyy";
            Search_DataGridView.Columns["التاريخ الحالي"].DefaultCellStyle.Format = "dd/MM/yyyy";

            var dgc1 = Search_DataGridView.Columns["ID"];
            dgc1.Visible = false;

            Program.MyConn.Close();
            //check connection//
            Program.buildConnection();
            //count rows
            var sel = "select count(DISTINCT MP_ID) " + from + condition;
            sc = new MySqlCommand(sel, Program.MyConn);
            decimal count = Convert.ToDecimal(sc.ExecuteScalar());
            Count_label.Text = "All:" + count.ToString("#,##0");
            Program.MyConn.Close();

            Search_DataGridView.Columns["رقم المشروع"].Width = Search_DataGridView.Columns["نوع الحجز"].Width =
                Search_DataGridView.Columns["رقم الملف"].Width = 80;

            Search_DataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                Search_DataGridView.Columns["تاريخ البداية"].DefaultCellStyle.Alignment
                    = Search_DataGridView.Columns["التاريخ الحالي"].DefaultCellStyle.Alignment =
                        Search_DataGridView.Columns["تاريخ الاستحقاق"].DefaultCellStyle.Alignment
                            = Search_DataGridView.Columns["رقم الملف"].DefaultCellStyle.Alignment =
                                Search_DataGridView.Columns["عدد الأشهر"].DefaultCellStyle.Alignment
                                    = Search_DataGridView.Columns["تاريخ الحجز"].DefaultCellStyle.Alignment =
                                        Search_DataGridView.Columns["نوع الحجز"].DefaultCellStyle.Alignment =
                                            DataGridViewContentAlignment.MiddleCenter;

            Search_DataGridView.ColumnHeadersVisible = true;
        }

        private void bind_Visits(string kind, string State, string Donor, string DonorGroup, string Partners, string ApplyYear, string FundYear)
        {
            try
            {
                //check connection//
                Program.buildConnection(); 
                var from =
                    @" from `microproject` MP 
    left join `person_microproject` PMP  on PMP.MicroProject_ID = MP.MP_ID
    left join `person` P on P.P_ID = PMP.Person_ID 
    left join `category` C on MP.MP_Category_ID = C.C_ID 
    LEFT join `state` on state.ID = MP.MP_State  
    Left join `donor` on donor.ID = MP.MP_Donor
    LEFT join `loan` L on L.MicroProject_ID = MP.MP_ID
    LEFT join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID ";

                string condition = " where 1 ";

                string query = @"SELECT  MP.MP_ID as 'رقم المشروع'
, Group_Concat(CONCAT(P.P_FirstName, ' ' , P.P_LastName , ' ابن/ة ', P.P_FatherName)) as 'المستفيد'
, (select Date from visit where visit.MicroProject_ID = PMP.MicroProject_ID and Kind like '%op' " + visitDate_condition + " limit 1) as 'Opening'"
+ ", (select Date from mevisit where mevisit.Person_ID = PMP.Person_ID and Number like '1' " + mevisitDate_condition + " limit 1) as 'M1'"
+ ", (select Date from mevisit where mevisit.Person_ID = PMP.Person_ID and Number like '2' " + mevisitDate_condition + " limit 1) as 'M2'"
+ ", (select Date from mevisit where mevisit.Person_ID = PMP.Person_ID and Number like '3' " + mevisitDate_condition + " limit 1) as 'M3'"
+ ", (select Date from mevisit where mevisit.Person_ID = PMP.Person_ID and Number like '4' " + mevisitDate_condition + " limit 1) as 'M4'"
+ ", (select Date from mevisit where mevisit.Person_ID = PMP.Person_ID and Number like '5' " + mevisitDate_condition + " limit 1) as 'M5'"
+ ", (select Date from visit where visit.MicroProject_ID = PMP.MicroProject_ID and Kind like '%cl' " + visitDate_condition + " limit 1) as 'Closing' "
+ from;

                if (kind != "")
                {
                    if (kind == "Vehicles") condition += " and MP_Category_ID in (1,2) ";
                    else condition += " and MP_Category_ID not in (1,2) ";
                }

                //MP State
                if (State != "")
                {
                    if (State == "ممول+منتهي")
                        condition += " and state.ID in (4,5) ";
                    else if (State == "ممول+منتهي+ملغى")
                        condition += " and state.ID in (4,5,7) ";
                    else
                        condition += " and state.Name_Ar like N'" + State + "' ";
                }

                //Donor
                if (Donor != "")
                    condition += " and donor.Name like '" + Donor + "'";

                //Donor Group
                if (DonorGroup != "")
                    condition += " and donorgroup.Name like '" + DonorGroup + "'";

                //Partners
                if (Partners != "")
                {
                    if (Partners.Contains("شراكة"))
                        condition += " and Partnership = 2 ";
                    else if (Partners.Contains("فردي")) condition += " and Partnership = 1 ";
                    else condition += "";
                }

                //if (ApplyDate_condition != "") condition += ApplyDate_condition;
                if (FundedDate_condition != "") condition += FundedDate_condition;

                //Apply Date
                if (ApplyYear != "") condition += " and Year(MP.MP_DateOfRequest) like '" + ApplyYear + "'";

                //Fund Date
                if (FundYear != "") condition += " and Year(L.Loan_DateTaken) like '" + FundYear + "'";

                string visit_query = @"SELECT  
      IFNULL(COUNT(case when visit.`Kind` like '%op' then 1 end ),0) as 'sum op' 
    , IFNULL(COUNT(case when visit.`Kind` like '%cl' then 1 end),0) as 'sum cl' 
    , IFNULL(COUNT(visit.ID),0) as 'total visit' 
   
    from visit
	LEFT join `microproject` MP on visit.MicroProject_ID = MP.MP_ID
    LEFT join `category` C on MP.MP_Category_ID = C.C_ID 
    LEFT join `state` on state.ID = MP.MP_State  
    LEFT join `donor` on donor.ID = MP.MP_Donor 
    LEFT join `loan` L on L.MicroProject_ID = MP.MP_ID
    LEFT join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID ";


                string mevisit_query = @" SELECT
      IFNULL(COUNT(CASE WHEN `Number`= '1' THEN 1 end), 0) as 'sum m1'
    , IFNULL(COUNT(CASE WHEN `Number`= '2' THEN 1 end), 0) as 'sum m2'
    , IFNULL(COUNT(CASE WHEN `Number`= '3' THEN 1 end), 0) as 'sum m3'
    , IFNULL(COUNT(CASE WHEN `Number`= '4' THEN 1 end), 0) as 'sum m4'
    , IFNULL(COUNT(CASE WHEN `Number`= '5' THEN 1 end), 0) as 'sum m5'
    , IFNULL(COUNT(mevisit.ID), 0) as 'total mevisit' 

    from `microproject` MP
    LEFT join `person_microproject` PMP on MP.MP_ID = PMP.MicroProject_ID 
    LEFT join `mevisit` on PMP.Person_ID = mevisit.Person_ID 
    LEFT join `category` C on MP.MP_Category_ID = C.C_ID 
    LEFT join `state` on state.ID = MP.MP_State  
    LEFT join `donor` on donor.ID = MP.MP_Donor  
    LEFT join `loan` L on L.MicroProject_ID = MP.MP_ID
    LEFT join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID ";

                query += condition + " Group By MP_ID; ";
                visit_query += condition + visitDate_condition + ";";
                mevisit_query += condition + mevisitDate_condition + " and PMP.PersonType like 'مستفيد' ;";

                string all_query = query + visit_query + mevisit_query;

                MySqlCommand sc = new MySqlCommand(all_query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataSet ds = new DataSet();
                da.Fill(ds);

                bs.DataSource = ds;
                Search_DataGridView.ColumnHeadersVisible = false;
                Search_DataGridView.DataSource = null;
                Search_DataGridView.Columns.Clear();
                Search_DataGridView.DataSource = bs[0];

                Search_DataGridView.Columns["Opening"].DefaultCellStyle.Format =
                    Search_DataGridView.Columns["M1"].DefaultCellStyle.Format =
                    Search_DataGridView.Columns["M2"].DefaultCellStyle.Format =
                    Search_DataGridView.Columns["M3"].DefaultCellStyle.Format =
                    Search_DataGridView.Columns["M4"].DefaultCellStyle.Format =
                    Search_DataGridView.Columns["M5"].DefaultCellStyle.Format =
                    Search_DataGridView.Columns["Closing"].DefaultCellStyle.Format = "dd/MM/yyyy";


                //count rows
                var sel = "select count(DISTINCT MP_ID) " + from + condition;
                MySqlCommand sc1 = new MySqlCommand(sel, Program.MyConn);
                decimal count = Convert.ToDecimal(sc1.ExecuteScalar());
                Count_label.Text = "All:" + count.ToString("#,##0");
                Program.MyConn.Close();

                Search_DataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                    Search_DataGridView.Columns["Opening"].DefaultCellStyle.Alignment
                        = Search_DataGridView.Columns["M1"].DefaultCellStyle.Alignment
                        = Search_DataGridView.Columns["M2"].DefaultCellStyle.Alignment
                        = Search_DataGridView.Columns["M3"].DefaultCellStyle.Alignment
                        = Search_DataGridView.Columns["M4"].DefaultCellStyle.Alignment
                        = Search_DataGridView.Columns["M5"].DefaultCellStyle.Alignment
                        = Search_DataGridView.Columns["Closing"].DefaultCellStyle.Alignment
                        = DataGridViewContentAlignment.MiddleCenter;
                 
                sumOp_label.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["sum op"]).ToString("#,##0");
                sumCl_label.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["sum cl"]).ToString("#,##0");
                sumM1_label.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["sum m1"]).ToString("#,##0");
                sumM2_label.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["sum m2"]).ToString("#,##0");
                sumM3_label.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["sum m3"]).ToString("#,##0");
                sumM4_label.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["sum m4"]).ToString("#,##0");
                sumM5_label.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["sum m5"]).ToString("#,##0");

                decimal total = Convert.ToDecimal(ds.Tables[1].Rows[0]["total visit"]) +
                    Convert.ToDecimal(ds.Tables[2].Rows[0]["total mevisit"]);
                sumAll_label.Text = total.ToString("#,##0");

                Search_DataGridView.ColumnHeadersVisible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bind_NewVisits(string kind, string State, string Category, string Donor, string DonorGroup, string Age
            , string Sex, string MaritalStatus
            , string ProjectReason, string HasExperience, string Partners)
        {
            try
            {
                Program.buildConnection();

                var from = @" FROM mevisit v 
    left join `person` P on P.P_ID = v.Person_ID 
    left join `person_microproject` PMP on PMP.Person_ID = P.P_ID  
    left join `microproject` MP on MP.MP_ID = PMP.MicroProject_ID 
    left join `category` C on MP.MP_Category_ID = C.C_ID 
    LEFT join `state` on state.ID = MP.MP_State
    Left join `donor` on donor.ID = MP.MP_Donor
    Left join `loan` l on l.MicroProject_ID = MP.MP_ID
    Left join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID ";

                string query = @"SELECT v.`ID` as 'V_ID'
, MP.MP_ID as 'رقم المشروع'
, CONCAT(P.P_FirstName, ' ' , P.P_LastName , ' ابن/ة ', P.P_FatherName) as 'المستفيد'
, `Date` as 'تاريخ الزيارة'
, `Number` as 'رقم الزيارة'
, `Kind` as 'نوع الزيارة'
, `Profit` as 'الربح الصافي التقريبي' 
, `Result` as 'النتيجة'
, donor.Name as 'الجهة الممولة' 
, `MP_Category_ID` as 'Category_ID'
, v.Person_ID as 'رقم المستفيد'";

                var from3 = "";
                var condition = " where 1 ";
                if (kind != "")
                {
                    if (kind == "Vehicles") condition += " and Kind like 'v' ";
                    else condition += " and Kind like 'o' ";
                }

                //State
                if (State != "")
                {
                    if (State == "ممول+منتهي")
                        condition += " and state.ID in (4,5) ";
                    else if (State == "ممول+منتهي+ملغى")
                        condition += " and state.ID in (4,5,7) ";
                    else
                        condition += " and state.Name_Ar like N'" + State + "' ";
                }

                //Donor
                if (Donor != "")
                    condition += " and donor.Name like '" + Donor + "'";

                //Donor Group
                if (DonorGroup != "")
                    condition += " and donorgroup.Name like '" + DonorGroup + "'";

                //Category
                if (Category != "")
                    condition += " and C.C_Name like N'" + Category + "'";

                ////Age 
                if (Age != "")
                {
                    if (Age.Contains("<16")) condition += " and  TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) < 16 ";
                    else if (Age.Contains("16-26"))
                        condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 16 and 26 ";
                    else if (Age.Contains("27-35"))
                        condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 27 and 35 ";
                    else if (Age.Contains("36-45"))
                        condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 36 and 45 ";
                    else if (Age.Contains("46-60"))
                        condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) BETWEEN 46 and 60 ";
                    else if (Age.Contains(">60"))
                        condition += " and TIMESTAMPDIFF(YEAR, `P_DOB`, MP_DateOfRequest) > 60 ";
                }

                //Marital Status
                if (MaritalStatus != "")
                {
                    if (MaritalStatus.Contains("متزوج")) condition += " and P.P_MaritalStatus like N'%متزوج%'";
                    else if (MaritalStatus.Contains("عازب")) condition += " and P.P_MaritalStatus like N'%عازب%'";
                    else if (MaritalStatus.Contains("مطلق")) condition += " and P.P_MaritalStatus like N'%مطلق%'";
                    else if (MaritalStatus.Contains("أرمل")) condition += " and P.P_MaritalStatus like N'%أرمل%'";
                    else if (MaritalStatus.Contains("مخطوب")) condition += " and P.P_MaritalStatus like N'%مخطوب%'";
                }

                //Sex
                if (Sex != "")
                {
                    if (Sex.Contains("ذكر")) condition += " and P.P_Sex like N'%ذكر%'";
                    else if (Sex.Contains("أنثى")) condition += " and P.P_Sex like N'%أنثى%'";
                }

                //Kind
                if (ProjectReason != "")
                {
                    if (ProjectReason.Contains("توسيع/تطوير مشروع حالي"))
                        condition += " and MP.MP_ProjectKind like 'Expand'";
                    else if (ProjectReason.Contains("إنشاء مشروع جديد"))
                        condition += " and MP.MP_ProjectKind like 'New'";
                    else condition += " and MP.MP_ProjectKind like ''";
                }

                //Experience
                if (HasExperience != "")
                {
                    from3 = " left join microproject_score ms on MP.MP_ID = ms.MicroProject_ID ";
                    from += from3;

                    if (HasExperience == "يوجد") condition += " and ms.Score_ID = 3 and ms.value = 1";
                    else if (HasExperience == "لا يوجد") condition += " and ms.Score_ID = 3 and ms.value = 0";
                    else condition += " and ms.Score_ID = 3 and ms.value = -1 ";
                }
                //Partners
                if (Partners != "")
                {
                    if (Partners.Contains("شراكة"))
                        condition += " and Partnership = 2 ";
                    else if (Partners.Contains("فردي")) condition += " and Partnership = 1 ";
                    else condition += "";
                }

                query += from + condition;

                MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataTable dt = new DataTable();
                da.Fill(dt);

                bs.DataSource = dt;
                Search_DataGridView.ColumnHeadersVisible = false;
                Search_DataGridView.DataSource = null;
                Search_DataGridView.Columns.Clear();
                Search_DataGridView.DataSource = bs; 
                 
                Search_DataGridView.Columns["تاريخ الزيارة"].DefaultCellStyle.Format = "dd/MM/yyyy";
                Search_DataGridView.Columns["الربح الصافي التقريبي"].DefaultCellStyle.Format = "#,##0";

                var dgC1 = Search_DataGridView.Columns["V_ID"];
                dgC1.Visible = false;
                var dgC2 = Search_DataGridView.Columns["رقم المستفيد"];
                dgC2.Visible = false;
                var dgC3 = Search_DataGridView.Columns["Category_ID"];
                dgC3.Visible = false;

                //count rows
                var sel = "select count(*) " + from + condition;
                sc = new MySqlCommand(sel, Program.MyConn);
                decimal count = Convert.ToDecimal(sc.ExecuteScalar());
                Count_label.Text = "All:" + count.ToString("#,##0");
                Program.MyConn.Close();

                Search_DataGridView.Columns["رقم المشروع"].Width = 80;

                Search_DataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                    Search_DataGridView.Columns["الربح الصافي التقريبي"].DefaultCellStyle.Alignment
                        = Search_DataGridView.Columns["تاريخ الزيارة"].DefaultCellStyle.Alignment =
                            Search_DataGridView.Columns["النتيجة"].DefaultCellStyle.Alignment
                                = Search_DataGridView.Columns["رقم الزيارة"].DefaultCellStyle.Alignment =
                                    Search_DataGridView.Columns["نوع الزيارة"].DefaultCellStyle.Alignment
                                        = DataGridViewContentAlignment.MiddleCenter;

                Search_DataGridView.ColumnHeadersVisible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bind_CheckList()
        {
            try
            {
                Program.buildConnection();

                var from = @"FROM  `microproject` MP  
    left outer join `person_microproject` PMP on MP.MP_ID = PMP.MicroProject_ID 
    left outer join `person` P on PMP.Person_ID = P.P_ID
    left outer join `category` C on MP.MP_Category_ID = C.C_ID
    LEFT outer join `state` on state.ID = MP.MP_State ";

                string query = @"SELECT PMP.`MicroProject_ID` as 'رقم المشروع'
 , CONCAT(P.P_FirstName, ' ', P.P_LastName) as 'المستفيد' 
 , P.P_FatherName as 'اسم الأب' 
 , MP.MP_Name as 'اسم المشروع' 
 , C_Name 'الصنف' 
 ,(SELECT COUNT(ID) 
FROM `microproject_checklist` 
WHERE microproject_checklist.MicroProject_ID = MP.MP_ID AND Location like 'k') as 'عدد آراء الأشخاص المفتاحيين'

,(SELECT COUNT(ID) 
FROM `microproject_checklist` 
WHERE microproject_checklist.MicroProject_ID = MP.MP_ID AND Location like 't') as 'عدد آراء الفريق'

,(SELECT COUNT(ID) 
FROM `microproject_checklist` 
WHERE microproject_checklist.MicroProject_ID = MP.MP_ID AND Location like 'v') as 'عدد آراء الزائرون'

,(SELECT COUNT(ID) 
FROM `microproject_checklist` 
WHERE microproject_checklist.MicroProject_ID = MP.MP_ID AND Location like 'c') as 'عدد آراء اللجنة'";


                var condition = " where 1 ";
                 
                query += from + condition;

                MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataTable dt = new DataTable();
                da.Fill(dt);

                bs.DataSource = dt;
                Search_DataGridView.ColumnHeadersVisible = false;
                Search_DataGridView.DataSource = null;
                Search_DataGridView.Columns.Clear();
                Search_DataGridView.DataSource = bs; 

                //////////////////////////////////////////////////////////////////

                //count rows
                var sel = "select count(DISTINCT MP_ID) " + from + condition;
                sc = new MySqlCommand(sel, Program.MyConn);
                decimal count = Convert.ToDecimal(sc.ExecuteScalar());
                Count_label.Text = "All:" + count.ToString("#,##0");
                //////////////////////////////////////////////////////////////////

                Search_DataGridView.Columns["رقم المشروع"].Width = 80;

                Search_DataGridView.Columns["عدد آراء الأشخاص المفتاحيين"].DefaultCellStyle.Alignment
                    = Search_DataGridView.Columns["عدد آراء الفريق"].DefaultCellStyle.Alignment
                    = Search_DataGridView.Columns["عدد آراء الزائرون"].DefaultCellStyle.Alignment
                    = Search_DataGridView.Columns["عدد آراء اللجنة"].DefaultCellStyle.Alignment
                    = DataGridViewContentAlignment.MiddleCenter;

                Search_DataGridView.ColumnHeadersVisible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        public void Bind_Search_DataGridView()
        {
            bs.Filter = Original_Filter_String = bs.Sort = "";
            Search_DataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            
            //Check_Visibility();
            // applications //
            if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(0))
            {
                if (show_images) Search_DataGridView.RowTemplate.Height = 80;

                Application_bind(Age_comboBox.Text,ApplyDate_comboBox.Text,FundDate_comboBox.Text,false);

           
            }

            // families //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(1))
            {
                FamilyMember_bind("");


            }
            // Loans //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(2))
            {
                Loan_bind("","","","","","", ApplyDate_comboBox.Text, FundDate_comboBox.Text);
                //Color_Cells();
            }
            // Attachments //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(3))
            {
                if (show_images) Search_DataGridView.RowTemplate.Height = 80;

                bind_Images("","","","","", show_images);
            }
            //Executive Files //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(4))
            {
                bind_ExeFile("","","","","","");
            }
            // Visits //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(5))
            {
                bind_Visits("","","","",""
                    , ApplyDate_comboBox.Text, FundDate_comboBox.Text);
            }
            // New Visit Forms //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(6))
            {
                bind_NewVisits("", "","","","" , Age_comboBox.Text,"","","","","");
            }
            // Check Lists //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(7))
            {
                bind_CheckList();
            }

            Search_DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Bind_Show_Hide_ToolStrip();
            NumOfRows1.Text = Search_DataGridView.Rows.Count.ToString();
            Count_label.Text = Search_DataGridView.Rows.Count.ToString();
            SearchToolBar.SetColumns(Search_DataGridView.Columns); 
        }

        private void SearchBy_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Search_DataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ProjectStatus_comboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (user_mode)
                //{
                Bind_Search_DataGridView();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Search DatagridView Events/ Functions
        private void Search_DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // Applications //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(0))
                    if (Search_DataGridView.RowCount > 0 && Search_DataGridView.CurrentRow != null)
                    {
                        SelectedDataRow = ((DataRowView)Search_DataGridView.CurrentRow.DataBoundItem).Row;
                        if (SelectedDataRow != null)
                        {
                            if (SelectedDataRow["رقم المشروع"] == null ||
                                SelectedDataRow["رقم المشروع"] == DBNull.Value)
                                MicroProject_ID = -1;
                            else MicroProject_ID = int.Parse(SelectedDataRow["رقم المشروع"].ToString());

                            if (SelectedDataRow["رقم المستفيد"] == null ||
                                SelectedDataRow["رقم المستفيد"] == DBNull.Value)
                                Person_ID = -1;
                            else Person_ID = int.Parse(SelectedDataRow["رقم المستفيد"].ToString());

                            if (SelectedDataRow["ID"] == null || SelectedDataRow["ID"] == DBNull.Value)
                                PMP_ID = -1;
                            else PMP_ID = int.Parse(SelectedDataRow["ID"].ToString());
                        }

                        Form application_Form = new Application_Form(Person_ID, MicroProject_ID, main_form);
                        main_form.showNewTab(application_Form, "Application", 0);
                    }

                // families //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(1))
                {
                    SelectedDataRow = ((DataRowView)Search_DataGridView.CurrentRow.DataBoundItem).Row;
                    if (SelectedDataRow != null)
                    {
                        if (SelectedDataRow["رقم العائلة"].ToString() == "" ||
                            SelectedDataRow["رقم العائلة"].ToString() == null || SelectedDataRow["رقم العائلة"] == null)
                            throw new Exception("This Person Doesn't belong to a family");
                        Family_ID = int.Parse(SelectedDataRow["رقم العائلة"].ToString());
                        //t P_ID = Int32.Parse(SelectedDataRow["ID"].ToString()); 
                        var P_ID = int.Parse(SelectedDataRow["رقم المستفيد"].ToString());

                        using (var ShowFamilyMembers = new ShowFamilyMembers(Family_ID.ToString(), P_ID))
                        {
                            ShowFamilyMembers.ShowDialog();
                        }
                    }
                }

                // Attachments //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(3))
                    if (Search_DataGridView.RowCount > 0 && Search_DataGridView.CurrentRow != null)
                    {
                        SelectedDataRow = ((DataRowView)Search_DataGridView.CurrentRow.DataBoundItem).Row;
                        if (SelectedDataRow != null)
                        {
                            if (SelectedDataRow["رقم المشروع"] == null ||
                                SelectedDataRow["رقم المشروع"] == DBNull.Value)
                                MicroProject_ID = -1;
                            else MicroProject_ID = int.Parse(SelectedDataRow["رقم المشروع"].ToString());

                            if (SelectedDataRow["ID"] == null || SelectedDataRow["ID"] == DBNull.Value)
                                Image_ID = -1;
                            else Image_ID = int.Parse(SelectedDataRow["ID"].ToString());
                        }

                        Form attachments_Form = new Attachments_Form(MicroProject_ID, Image_ID, main_form);
                        main_form.showNewTab(attachments_Form, "Attachments", 0);
                    }

                //Executive Files
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(4))
                    if (Search_DataGridView.RowCount > 0 && Search_DataGridView.CurrentRow != null)
                    {
                        SelectedDataRow = ((DataRowView)Search_DataGridView.CurrentRow.DataBoundItem).Row;
                        if (SelectedDataRow != null)
                        {
                            if (SelectedDataRow["رقم المشروع"] == null ||
                                SelectedDataRow["رقم المشروع"] == DBNull.Value)
                                MicroProject_ID = -1;
                            else MicroProject_ID = int.Parse(SelectedDataRow["رقم المشروع"].ToString());

                            if (SelectedDataRow["ID"] == null || SelectedDataRow["ID"] == DBNull.Value)
                                ExeFile_ID = -1;
                            else ExeFile_ID = int.Parse(SelectedDataRow["ID"].ToString());
                        }

                        Form ExecutiveFiles_Form = new ExecutiveFiles_Form(ExeFile_ID, main_form);
                        main_form.showNewTab(ExecutiveFiles_Form, "Executive File", 0);
                    }

                // Visits //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(5))
                    if (Search_DataGridView.RowCount > 0 && Search_DataGridView.CurrentRow != null)
                    {
                        SelectedDataRow = ((DataRowView)Search_DataGridView.CurrentRow.DataBoundItem).Row;
                        if (SelectedDataRow != null)
                        {
                            if (SelectedDataRow["رقم المشروع"] == null ||
                                SelectedDataRow["رقم المشروع"] == DBNull.Value)
                                MicroProject_ID = -1;
                            else MicroProject_ID = Convert.ToInt32(SelectedDataRow["رقم المشروع"].ToString());


                            var _Form = new ChooseVisitKind_Form(MicroProject_ID, main_form);
                            _Form.ShowDialog();
                        }
                    }

                //New visit forms
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(6))
                {
                    SelectedDataRow = ((DataRowView)Search_DataGridView.CurrentRow.DataBoundItem).Row;
                    if (SelectedDataRow != null)
                    {
                        var V_ID = int.Parse(SelectedDataRow["V_ID"].ToString());
                        var Beneficiary_ID = int.Parse(SelectedDataRow["رقم المستفيد"].ToString());
                        var Category_ID = int.Parse(SelectedDataRow["Category_ID"].ToString());
                        var V_Num = SelectedDataRow["رقم الزيارة"].ToString();

                        string V_NAME_TO_SHOW;
                        if (V_Num == "2") V_NAME_TO_SHOW = "Second visit";
                        else if (V_Num == "3*") V_NAME_TO_SHOW = "Third * visit";
                        else if (V_Num == "3") V_NAME_TO_SHOW = "Third visit";
                        else V_NAME_TO_SHOW = "";

                        if (Category_ID == 1 || Category_ID == 2)
                        {
                            Form Visit_V_Form = new V_ME_Taxi_Form(V_ID, main_form);
                            main_form.showNewTab(Visit_V_Form, V_NAME_TO_SHOW, 0);
                        }
                        else
                        {
                            Form Visit_O_Form = new V_ME_Other_Form(V_ID, main_form);
                            main_form.showNewTab(Visit_O_Form, V_NAME_TO_SHOW, 0);
                        }
                    }
                }

                // Checklists //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(7))
                    if (Search_DataGridView.RowCount > 0 && Search_DataGridView.CurrentRow != null)
                    {
                        SelectedDataRow = ((DataRowView)Search_DataGridView.CurrentRow.DataBoundItem).Row;
                        if (SelectedDataRow != null)
                        {
                            if (SelectedDataRow["رقم المشروع"] == null ||
                                SelectedDataRow["رقم المشروع"] == DBNull.Value)
                                MicroProject_ID = -1;
                            else MicroProject_ID = int.Parse(SelectedDataRow["رقم المشروع"].ToString());
                        }

                        Form Form = new CheckList_Form(MicroProject_ID, main_form);
                        main_form.showNewTab(Form, "Check List", 0);
                    }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Search_DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        
        private void Search_DataGridView_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            try
            {
                bs.Sort = Search_DataGridView.SortString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Search_DataGridView_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
            try
            {
                Original_Filter_String = Search_DataGridView.FilterString;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("MySql") || ex.Message.Contains("MySQL")) { MessageBox.Show("please check your network and try again", "Error"); } else if (ex.Message.Contains("host")) { MessageBox.Show("please check the server or your network connection and try again", "Error"); } else { MessageBox.Show(ex.Message, "Error"); }
            }
        }

        private void Search_DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            NumOfSelected1.Text = Search_DataGridView.SelectedRows.Count.ToString();
            Selected_label.Text = Search_DataGridView.SelectedRows.Count.ToString();
        }

        private void Search_DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (SearchFor == "Distribution")
            //{
            //    if (e.RowIndex < 0)
            //        return; 
            //    if (e.ColumnIndex == Search_DataGridView.Columns["المبلغ المطلوب من قبل الكنيسة"].Index
            //        || e.ColumnIndex == Search_DataGridView.Columns["مجموع المبالغ الموزعة للعائلات"].Index
            //        || e.ColumnIndex == Search_DataGridView.Columns["المبالغ الإدارية"].Index
            //        || e.ColumnIndex == Search_DataGridView.Columns["المبلغ المتبقي"].Index
            //        || e.ColumnIndex == Search_DataGridView.Columns["المبلغ المسلم للكنيسة"].Index)
            //    {
            //        if (e.Value != null && e.Value != DBNull.Value && e.Value != "")
            //        {
            //            decimal value = decimal.Parse(e.Value.ToString());

            //            e.Value = value.ToString("N0");

            //            e.FormattingApplied = true;
            //        }
            //    }
            //}
        }
        #endregion
         
        private void Bind_Show_Hide_ToolStrip()
        {
            ToolStripMenuItem Hide_Columns = Show_Hide_ToolStrip.DropDownItems[0] as ToolStripMenuItem;
            ToolStripMenuItem Show_Columns = Show_Hide_ToolStrip.DropDownItems[1] as ToolStripMenuItem;

            Show_Columns.DropDownItems.Clear();
            Hide_Columns.DropDownItems.Clear();

            foreach (DataGridViewColumn C in Search_DataGridView.Columns)
            {
                if (!C.Name.Contains("ID"))
                    if (C.Visible)
                        Hide_Columns.DropDownItems.Add(C.Name);
                    else
                        Show_Columns.DropDownItems.Add(C.Name);
            }
        }

        private void toolStripMenuItem2_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Search_DataGridView.Columns[e.ClickedItem.Text].Visible = false;
                Bind_Show_Hide_ToolStrip();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
         
        private void toolStripMenuItem3_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Search_DataGridView.Columns[e.ClickedItem.Text].Visible = true;
                Bind_Show_Hide_ToolStrip();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
         
        private void Show_Hide_ToolStrip_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                Show_Hide_ToolStrip.ShowDropDown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
         
        private void Hide_MenuItem_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            try
            {
                if (Hide_MenuItem.DropDownItems.Count > 0)
                {
                    if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                    {
                        e.Cancel = true;
                    }
                }
                SearchToolBar.SetColumns(Search_DataGridView.Columns);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            } 
        }
        private void Show_MenuItem_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            try
            {
                if (Show_MenuItem.DropDownItems.Count > 0)
                {
                    if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                    {
                        e.Cancel = true;
                    }
                }
                SearchToolBar.SetColumns(Search_DataGridView.Columns);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }
 
        private void bs_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            NumOfRows1.Text = Search_DataGridView.Rows.Count.ToString();
            Count_label.Text = Search_DataGridView.Rows.Count.ToString();
        }
        
        private void SearchToolBar_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            try
            {
                #region search by enter
                //bool restartsearch = true;
                //int startColumn = 0;
                //int startRow = 0;
                //if (!e.FromBegin)
                //{
                //    bool endcol; bool endrow;

                //    endcol = Search_DataGridView.CurrentCell.ColumnIndex + 1 >= Search_DataGridView.ColumnCount;
                //    endrow = Search_DataGridView.CurrentCell.RowIndex + 1 >= Search_DataGridView.RowCount;

                //    if (endcol && endrow)
                //    {
                //        startColumn = Search_DataGridView.CurrentCell.ColumnIndex;
                //        startRow = Search_DataGridView.CurrentCell.RowIndex;
                //    }
                //    else
                //    {
                //        startColumn = endcol ? 0 : Search_DataGridView.CurrentCell.ColumnIndex + 1;
                //        startRow = Search_DataGridView.CurrentCell.RowIndex + (endcol ? 1 : 0);
                //    }
                //}
                //DataGridViewCell c = Search_DataGridView.FindCell(
                //    e.ValueToSearch,
                //    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                //    startRow,
                //    startColumn,
                //    e.WholeWord,
                //    e.CaseSensitive);
                //if (c == null && restartsearch)
                //{
                //    c = Search_DataGridView.FindCell(
                //    e.ValueToSearch,
                //    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                //    0,
                //    0,
                //    e.WholeWord,
                //   e.CaseSensitive);
                //}

                //if (c != null)
                //{
                //    Search_DataGridView.CurrentCell = c;
                //    if (e.FromBegin)
                //        SearchToolBar.Items["button_frombegin"].PerformClick();
                //}
                //else
                //    MessageBox.Show("لا يوجد نتائج لعرضها");
                #endregion

                if (String.IsNullOrWhiteSpace(e.ValueToSearch))
                {
                    bs.Filter = Original_Filter_String;
                }
                else
                {
                    string NewFilter = "";


                    if (e.ColumnToSearch != null)
                    {
                        //convert(JobNumber, 'System.String')
                        if (e.WholeWord)
                            NewFilter += "convert(`" + e.ColumnToSearch.Name + "`,'System.String') = '" + e.ValueToSearch + "'";
                        else
                            NewFilter += "convert(`" + e.ColumnToSearch.Name + "`,'System.String') like '%" + e.ValueToSearch + "%'";
                    }
                    else
                    {
                        foreach (DataGridViewColumn C in Search_DataGridView.Columns)
                        {
                            if (C.Visible == true)
                            {
                                if (e.WholeWord)
                                    NewFilter += "convert(`" + C.Name + "`,'System.String') = '" + e.ValueToSearch + "'";
                                else
                                    NewFilter += "convert(`" + C.Name + "`,'System.String') like '%" + e.ValueToSearch + "%'";

                                if (C.Index < Search_DataGridView.Columns.Count - 1)
                                    NewFilter += " or ";
                            }
                        }
                    }
                    string All_Filters = Original_Filter_String == "" ? NewFilter : Original_Filter_String + " and (" + NewFilter + ")";
                    bs.Filter = All_Filters;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
         
        protected override Point ScrollToControl(Control activeControl) //STOP AUTO SCROLL
        {
            Point pt = this.AutoScrollPosition;
            return pt;
        }

        void ComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        
        private void Search_Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }

        private void Export_Exel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel |*.xlsx";
                saveFileDialog1.Title = "Export as Excel";
                DialogResult res = saveFileDialog1.ShowDialog();
                string filepath = "";

                if (res == DialogResult.OK)
                {
                    // If the file name is not an empty string open it for saving.  
                    if (!string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
                    {
                        System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                        filepath = saveFileDialog1.FileName;
                        fs.Close();
                    }
                }

                if (!string.IsNullOrWhiteSpace(filepath))
                {
                    XLWorkbook wb = new XLWorkbook();

                    DataTable dt = ((DataTable)bs.DataSource).DefaultView.ToTable();

                    foreach (ToolStripItem tsi in (Show_Hide_ToolStrip.DropDownItems[1] as ToolStripMenuItem).DropDownItems)
                    {
                        dt.Columns.RemoveAt(dt.Columns.IndexOf(dt.Columns[tsi.Text]));
                    }

                    wb.Worksheets.Add(dt, "Extrcted Data");
                    wb.SaveAs(filepath);
                    wb.Dispose();
                    Process.Start(filepath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
         
        #region date - amount filters
        ///////////////////////////////////////////////////////////////////////////
        private void Requested_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Requested_checkBox.Checked)
            {
                RequestedFrom_label.Enabled = RequestedFrom_textBox.Enabled
                    = RequestedTo_label.Enabled = RequestedTo_textBox.Enabled = true;
                RequestedFrom_textBox_Leave(sender, e);
            }
            else
            {
                RequestedAmount_condition = "";
                RequestedFrom_label.Enabled = RequestedFrom_textBox.Enabled
                    = RequestedTo_label.Enabled = RequestedTo_textBox.Enabled = false;
                SearchBy_comboBox_SelectedIndexChanged(sender, e);
            }
        }

        private void Funded_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Funded_checkBox.Checked)
            {
                FundedFrom_label.Enabled = FundedFrom_textBox.Enabled
                    = FundedTo_label.Enabled = FundedTo_textBox.Enabled = true;
                FundedFrom_textBox_Leave(sender, e);
            }
            else
            {
                FundedAmount_condition = "";
                FundedFrom_label.Enabled = FundedFrom_textBox.Enabled
                    = FundedTo_label.Enabled = FundedTo_textBox.Enabled = false;
                SearchBy_comboBox_SelectedIndexChanged(sender, e);
            }
        }

        private void RequestedFrom_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (RequestedFrom_textBox.Text != "")
                {
                    RequestedFrom_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(RequestedFrom_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    RequestedFrom_textBox.SelectionStart = RequestedFrom_textBox.Text.Length;
                    RequestedFrom_textBox.SelectionLength = 0;
                }

                if (RequestedTo_textBox.Text != "")
                {
                    RequestedTo_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(RequestedTo_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    RequestedTo_textBox.SelectionStart = RequestedTo_textBox.Text.Length;
                    RequestedTo_textBox.SelectionLength = 0;
                }

                if (FundedFrom_textBox.Text != "")
                {
                    FundedFrom_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(FundedFrom_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    FundedFrom_textBox.SelectionStart = FundedFrom_textBox.Text.Length;
                    FundedFrom_textBox.SelectionLength = 0;
                }

                if (FundedTo_textBox.Text != "")
                {
                    FundedTo_textBox.Text =
                        Regex.Replace(string.Format("{0:n" + 4 + "}", Convert.ToDecimal(FundedTo_textBox.Text)),
                            @"[" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    FundedTo_textBox.SelectionStart = FundedTo_textBox.Text.Length;
                    FundedTo_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RequestedFrom_textBox_Leave(object sender, EventArgs e)
        {
            try
            {
                RequestedAmount_condition = "";
                if (RequestedFrom_textBox.Text != "" || RequestedTo_textBox.Text != "")
                {
                    var NeededFrom = RequestedFrom_textBox.Text.Replace(",", "");
                    var NeededTo = RequestedTo_textBox.Text.Replace(",", "");

                    double Needed_from_d, Needed_to_d;
                    if (NeededFrom == "")
                        Needed_from_d = 0;
                    else Needed_from_d = Convert.ToDouble(NeededFrom);

                    if (NeededTo == "")
                        Needed_to_d = 0;
                    else Needed_to_d = Convert.ToDouble(NeededTo);
                    RequestedAmount_condition = " and (`MP_RequestedAmount` BETWEEN " + Needed_from_d + " and " +
                                                Needed_to_d + ") ";

                    SearchBy_comboBox_SelectedIndexChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FundedFrom_textBox_Leave(object sender, EventArgs e)
        {
            FundedAmount_condition = "";
            if (FundedFrom_textBox.Text != "" || FundedTo_textBox.Text != "")
            {
                var FinancedFrom = FundedFrom_textBox.Text.Replace(",", "");
                var FinancedTo = FundedTo_textBox.Text.Replace(",", "");

                double Financed_from_d, Financed_To_d;
                if (FinancedFrom == "")
                    Financed_from_d = 0;
                else Financed_from_d = Convert.ToDouble(FinancedFrom);

                if (FinancedTo == "")
                    Financed_To_d = 0;
                else Financed_To_d = Convert.ToDouble(FinancedTo);
                FundedAmount_condition +=
                    " and (L.Loan_Amount BETWEEN " + Financed_from_d + " and " + Financed_To_d + ") ";

                SearchBy_comboBox_SelectedIndexChanged(sender, e);
            }
        }
        ///////////////////////////////////////////////////////////////////////////

        private void ApplyDate_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ApplyDate_checkBox.Checked)
            {
                ApplyDateFrom_dateTimePicker.Enabled = ApplyDateFrom_label.Enabled
                    = ApplyDateTo_DateTimePicker.Enabled = ApplyDateTo_label.Enabled = true;
                ApplyDateFrom_dateTimePicker_ValueChanged(sender, e);
            }
            else
            {
                ApplyDate_condition = visitDate_condition = mevisitDate_condition = "";
                ApplyDateFrom_dateTimePicker.Enabled = ApplyDateFrom_label.Enabled
                    = ApplyDateTo_DateTimePicker.Enabled = ApplyDateTo_label.Enabled = false;
                SearchBy_comboBox_SelectedIndexChanged(sender, e);
            }
        }

        private void ShowHide_button_Click(object sender, EventArgs e)
        {
            if (Hided)
            {
                Hided = false;
                if (Settings.Default.theme == "Light")
                    ShowHide_button.BackgroundImage = Resources.Hide2_L;
                else ShowHide_button.BackgroundImage = Resources.Hide2_D;
                Search_panel.Height = PW;
            }
            else
            {
                Hided = true;
                if (Settings.Default.theme == "Light")
                    ShowHide_button.BackgroundImage = Resources.Show2_L;
                else ShowHide_button.BackgroundImage = Resources.Show2_D;
                Search_panel.Height = 0;
            }
        }

        private void FundedDate_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FundedDate_checkBox.Checked)
            {
                FundedDateFrom_DateTimePicker.Enabled = FundedDateFrom_label.Enabled
                    = FundedDateTo_DateTimePicker.Enabled = FundedDateTo_label.Enabled = true;
                FundedDateFrom_bcDateTimePicker_ValueChanged(sender, e);
            }
            else
            {
                FundedDate_condition = "";
                FundedDateFrom_DateTimePicker.Enabled = FundedDateFrom_label.Enabled
                    = FundedDateTo_DateTimePicker.Enabled = FundedDateTo_label.Enabled = false;
                SearchBy_comboBox_SelectedIndexChanged(sender, e);
            }
        }

        /////////////////  #############  /////////////////
        private void ApplyDateFrom_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ApplyDateFrom_dateTimePicker.Value > ApplyDateTo_DateTimePicker.Value)
                {
                    ApplyDate_condition = visitDate_condition = mevisitDate_condition = "";
                    throw new Exception("لا يمكن أن يكون تاريخ البداية أكبر من تاريخ النهاية");
                }

                string beg, end;
                beg = ApplyDateFrom_dateTimePicker.Value.ToString("yyyy/MM/dd");
                end = ApplyDateTo_DateTimePicker.Value.ToString("yyyy/MM/dd");

                ApplyDate_condition = " and ( MP_DateOfRequest between '" + beg + "' and '" + end + "' )";
                visitDate_condition = " and ( visit.Date between '" + beg + "' and '" + end + "' )";
                mevisitDate_condition = " and ( mevisit.Date between '" + beg + "' and '" + end + "')";

                SearchBy_comboBox_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FundedDateFrom_bcDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (FundedDateFrom_DateTimePicker.Value > FundedDateTo_DateTimePicker.Value)
                    throw new Exception("لا يمكن أن يكون تاريخ البداية أكبر من تاريخ النهاية");

                DateTime beg, end;
                beg = FundedDateFrom_DateTimePicker.Value;
                end = FundedDateTo_DateTimePicker.Value;

                FundedDate_condition = " and MP_ID in (select MicroProject_ID from loan where " +
                                       " Loan_DateTaken between '" + beg.Year + "/" + beg.Month + "/" + beg.Day + "' " +
                                       " and '" + end.Year + "/" + end.Month + "/" + end.Day + "' )";

                SearchBy_comboBox_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region right click menu 
        private void refreshPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bind_Search_DataGridView();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Search_Form = new Search_Form(main_form);
                main_form.showNewTab(Search_Form, "Search", 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Statistics_Form = new Statistics_Form(main_form);
                main_form.showNewTab(Statistics_Form, "Statistics", 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void taskBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var projectsTasks = new TaskBoard_Form(main_form);
                main_form.showNewTab(projectsTasks, "Task Board", 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showHideFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHide_button_Click(sender, e);
        }

        private void clearAllFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            ApplyDate_comboBox.Text = FundDate_comboBox.Text = "";
            RequestedFrom_textBox.Text = RequestedTo_textBox.Text = FundedFrom_textBox.Text = FundedTo_textBox.Text = "";
             
            SearchToolBar.Text = " ";
            Bind_Search_DataGridView();
        }
        #endregion

    }
}
