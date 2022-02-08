using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class SearchSection : Form
    {
        public SearchSection()
        {
            InitializeComponent();
        }
        public SearchSection(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        string username;
        MySqlComponents MySS;
        private int PersonID;
        DataRow SelectedDataRow;

        #region bind commands
        private void bind(string P_Name ,string MP_ID, string MP_Name,string intermidiarySide, string priest,string parish)
        {
            //check connection//
            Program.buildConnection();
            string from = "  from `person_microproject` PMP left outer join `person` P1 on P1.P_ID = PMP.Person_ID"
                        + "  left outer join `microproject` MP on PMP.MicroProject_ID = MP.MP_ID"
                        + "  left outer join `level` L on MP.MP_Level_ID = L.Level_ID"
                        + "  left outer join intermediaryside Iss1 on Iss1.MicroProject_ID = MP.MP_ID and Iss1.IS_isPriest like N'%No'"
                        + "  left outer join priest Pri on MP.MP_Priest_ID = Pri.Priest_ID "
                        + "  left outer join `category` C on MP.MP_Category_ID = C.C_ID  ";

            MySS.query = "select PMP.MicroProject_ID as 'MicroProject_ID'"
                       + " ,PMP.Person_ID as 'Beneficiary_ID'"
                       + " ,CONCAT(P1.P_FirstName, ' ', P1.P_FatherName, ' ', P1.P_LastName) as 'Beneficiary Name'"
                       + " ,MP.MP_Name as 'Project Name'"
                       + " ,P1.P_Parish as 'Parish'"
                       + " ,CASE MP.MP_State WHEN 0 THEN N'Rejected' WHEN 1 THEN N'Accepted' WHEN 2 THEN N'Delayed' WHEN 4 THEN N'Financed' WHEN 5 THEN N'Closed' ELSE N'On Hold' End as 'Project State'"
                       + " ,MP.MP_ResonOfPlace as 'Donor'"
                       + " ,C.C_Name as 'Category'"
                       + " ,L.Level_Symbol as 'Level'"

                       + " ,Iss1.IS_Name as 'Intermidate Side'"
                       + " ,Pri.Priest_Name as 'Priest'"
                       + from;
            
            string condition = "";
            if (P_Name != "")
            {
                condition += " where (P1.P_FirstName like N'%" + P_Name + "%' or P1.P_FatherName like N'%" + P_Name + "%' or P1.P_LastName like N'%" + P_Name + "%' )";
                if (MP_Name != "")
                    condition += " and MP.MP_Name like N'" + MP_Name + "%'";
                if (intermidiarySide != "")
                    condition += " and Iss1.IS_Name like N'" + intermidiarySide + "%' and Iss1.IS_Name IS NOT NULL";
                if (priest != "")
                    condition += " and Pri.Priest_Name like N'" + priest + "%'";
                if (parish != "")
                    condition = " and P1.P_Parish like  N'%"+ parish + "%'";
            }
            else if (MP_Name != "")
            {
                condition += " where MP.MP_Name like N'" + MP_Name + "%'";
                if (intermidiarySide != "")
                    condition += " and Iss1.IS_Name like N'" + intermidiarySide + "%' and Iss1.IS_Name IS NOT NULL";
                if (priest != "")
                    condition += " and Pri.Priest_Name like N'" + priest + "%'";
                if (parish != "")
                    condition = " and P1.P_Parish like  N'%" + parish + "%'";
            }
            else if (intermidiarySide != "")
            { 
                condition += " where Iss1.IS_Name like N'" + intermidiarySide + "%' and Iss1.IS_Name IS NOT NULL";
                if (priest != "")
                    condition += " and Pri.Priest_Name like N'" + priest + "%'";
                if (parish != "")
                    condition = " and P1.P_Parish like  N'%" + parish + "%'";
            }
            else if (priest != "")
            { 
                condition += " where Pri.Priest_Name like N'" + priest + "%'";
                if (parish != "")
                    condition = " and P1.P_Parish like  N'%" + parish + "%'";
            }
            else if (parish != "")
                condition = " where P1.P_Parish like  N'%" + parish + "%'";
            else if (MP_ID != "")
            {
                condition= " where CAST(PMP.MicroProject_ID AS nvarchar(Max)) LIKE '" + MP_ID + "%'";
            }

            //Project State
            if (MP_Accepted_radioButton.Checked)
                condition += " where MP.MP_State = 1";
            else if (MP_NotFinanced_radioButton.Checked)
                condition += " where MP.MP_State = 0";
            else if (MP_Delayed_radioButton.Checked)
                condition += " where MP.MP_State = 2";
            else if (MP_Hold_radioButton.Checked)
                condition += " where MP.MP_State = 3";
            else if (MP_Financed_radioButton.Checked)
                condition += " where MP.MP_State = 4";
            else if (MP_Closed_radioButton.Checked)
                condition += " where MP.MP_State = 5";
            //////Project Need Level
            ////else if (MP_StateA_radioButton.Checked)
            ////    condition += " where L.Level_Symbol like 'A'";
            ////else if (MP_StateBF_radioButton.Checked)
            ////    condition += " where L.Level_Symbol like 'BF'";
            ////else if (MP_StateBP_radioButton.Checked)
            ////    condition += " where L.Level_Symbol like 'BP'";
            ////else if (MP_StateC_radioButton.Checked)
            ////    condition += " where L.Level_Symbol like 'C'";
            ////// License
            ////else if (MP_NeedLicense_radioButton.Checked)
            ////    condition += " where MP.MP_IsNeedLicense like N'Yes'";
            ////else if (MP_NoNeedLicense_radioButton.Checked)
            ////    condition += " where MP.MP_IsNeedLicense like N'No'";
            //Marital Status
            else if (P_Married_radioButton.Checked)
                condition += " where P1.P_MaritalStatus like N'%متزوج%'";
            else if (P_NotMarried_radioButton.Checked)
                condition += " where P1.P_MaritalStatus like N'%عازب%'";
            //Sex
            else if (P_Male_radioButton.Checked)
                condition += " where P1.P_Sex like N'%ذكر%'";
            else if (P_Female_radioButton.Checked)
                condition += " where P1.P_Sex like N'%أنثى%'";
            //lives with another family
            else if (P_Male_radioButton.Checked)
                condition += " where P1.P_IsLivingWithFamily like N'%Yes%'";
            else if (P_Female_radioButton.Checked)
                condition += " where P1.P_IsLivingWithFamilylike N'%No%'";
            //Cost
            else if (MP_CostFrom_textBox.Text != "")
                if (MP_CostTo_textBox.Text !="")
                    condition += " where between '" + MP_CostFrom_textBox.Text + "' and '" + MP_CostTo_textBox.Text + "'";
                else
                    condition += " where between '" + MP_CostFrom_textBox.Text + "' and '" + MP_CostFrom_textBox.Text + "'";
            
            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            MP_dataGridView.ColumnHeadersVisible = false;
            MP_dataGridView.DataSource = MySS.dt;
            MP_dataGridView.ColumnHeadersVisible = true;
            DataGridViewColumn dgC1;
            if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
            {
                dgC1 = MP_dataGridView.Columns["Beneficiary_ID"];
                dgC1.Visible = false;
            }
            //check connection//
            Program.buildConnection();
            
            /////////////////////////////////
            StringBuilder s = new StringBuilder();
            s.Append("select MP_ID from microproject where MP_ID in ");
            s.Append("(");
            s.Append("select PMP.MicroProject_ID as 'MicroProject_ID'" + from + condition);
            s.Append(")");
            /////////////////////////////////

            //count rows
            string sel = "select count(*) from (" + s + ") as count";
            MySS.sc = new MySqlCommand(sel, Program.MyConn);
            Counter_textBox.Text = MySS.sc.ExecuteScalar().ToString();
        }
        private void bind(string category)
        {
            //check connection//
            Program.buildConnection();

            string from = "  from `person_microproject` PMP left outer join `person` P1 on P1.P_ID = PMP.Person_ID"
                        + "  left outer join `microproject` MP on PMP.MicroProject_ID = MP.MP_ID"
                        + "  left outer join `level` L on MP.MP_Level_ID = L.Level_ID"
                        + "  left outer join `category` C on MP.MP_Category_ID = C.C_ID";

            MySS.query = "select PMP.MicroProject_ID as 'MicroProject_ID'"
                       + " ,PMP.Person_ID as 'Beneficiary_ID'"
                       + " ,CONCAT(P1.P_FirstName, ' ', P1.P_FatherName, ' ', P1.P_LastName) as 'Beneficiary Name'"
                       + " ,MP.MP_Name as 'Project Name'"
                       + " ,P1.P_Parish as 'Parish'"
                       + " ,CASE MP.MP_State WHEN 0 THEN N'Rejected' WHEN 1 THEN N'Accepted' WHEN 2 THEN N'Delayed' WHEN 4 THEN N'Financed' WHEN 5 THEN N'Closed' ELSE N'On Hold' End as 'Project State'"
                                             + " ,MP.MP_ResonOfPlace as 'Donor'"
                       + " ,C.C_Name as 'Category'"
                       + " ,L.Level_Symbol as 'Level'"
                       + from;
            string condition = "";
            if (category != "")
            {
                condition += " where C.C_Name like N'%" + category + "%'";
                //Project State
                if (MP_Accepted_radioButton.Checked)
                    condition += " and MP.MP_State = 1";
                else if (MP_NotFinanced_radioButton.Checked)
                    condition += " and MP.MP_State = 0";
                else if (MP_Delayed_radioButton.Checked)
                    condition += " and MP.MP_State = 2";
                else if (MP_Hold_radioButton.Checked)
                    condition += " and MP.MP_State = 3";
                else if (MP_Financed_radioButton.Checked)
                    condition += " and MP.MP_State = 4";
                else if (MP_Closed_radioButton.Checked)
                    condition += " and MP.MP_State = 5";
            }
            else
            {
                if (MP_Accepted_radioButton.Checked)
                    condition += " where MP.MP_State = 1";
                else if (MP_NotFinanced_radioButton.Checked)
                    condition += " where MP.MP_State = 0";
                else if (MP_Delayed_radioButton.Checked)
                    condition += " where MP.MP_State = 2";
                else if (MP_Hold_radioButton.Checked)
                    condition += " where MP.MP_State = 3";
                else if (MP_Financed_radioButton.Checked)
                    condition += " where MP.MP_State = 4";
                else if (MP_Closed_radioButton.Checked)
                    condition += " where MP.MP_State = 5";
            }
            ////////Project Need Level
            //////if (condition != "" && MP_StateA_radioButton.Checked)
            //////    condition += " and L.Level_Symbol like 'A'";
            //////else if (condition != "" && MP_StateBF_radioButton.Checked)
            //////    condition += " and L.Level_Symbol like 'BF'";
            //////else if (condition != "" && MP_StateBP_radioButton.Checked)
            //////    condition += " and L.Level_Symbol like 'BP'";
            //////else if (condition != "" && MP_StateC_radioButton.Checked)
            //////    condition += " and L.Level_Symbol like 'C'";
            //////else if(condition == "" && MP_StateA_radioButton.Checked)
            //////    condition += " where L.Level_Symbol like 'A'";
            //////else if (condition == "" && MP_StateBF_radioButton.Checked)
            //////    condition += " where L.Level_Symbol like 'BF'";
            //////else if (condition == "" && MP_StateBP_radioButton.Checked)
            //////    condition += " where L.Level_Symbol like 'BP'";
            //////else if (condition == "" && MP_StateC_radioButton.Checked)
            //////    condition += " where L.Level_Symbol like 'C'";


            MySS.query += condition;
            
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            MP_dataGridView.ColumnHeadersVisible = false;
            MP_dataGridView.DataSource = MySS.dt;
            MP_dataGridView.ColumnHeadersVisible = true;
            DataGridViewColumn dgC1;
            if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
            {
                dgC1 = MP_dataGridView.Columns["Beneficiary_ID"];
                dgC1.Visible = false;
            }
            //check connection//
            Program.buildConnection();

            /////////////////////////////////
            StringBuilder s = new StringBuilder();
            s.Append("select MP_ID from microproject where MP_ID in ");
            s.Append("(");
            s.Append("select PMP.MicroProject_ID as 'MicroProject_ID'" + from + condition);
            s.Append(")");
            /////////////////////////////////

            //count rows
            string sel = "select count(*) from (" + MySS.query + ") as count";
            MySS.sc = new MySqlCommand(sel, Program.MyConn);
            Counter_textBox.Text = MySS.sc.ExecuteScalar().ToString();
        }

        private void Person_bind(string P_Name, string NationalNum,string birthDate)
        {
            //check connection//
            Program.buildConnection();

            string from = "  from person_microproject PMP left outer join person P1 on P1.P_ID = PMP.Person_ID "
                       + "  left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID "
                       + "  left outer join `level` L on MP.MP_Level_ID = L.Level_ID "
                       + "  left outer join category C on MP.MP_Category_ID = C.C_ID ";

            MySS.query = "select PMP.MicroProject_ID as 'MicroProject_ID'"
                       + " ,PMP.Person_ID as 'Beneficiary_ID'"
                       + " ,CONCAT(P1.P_FirstName, ' ', P1.P_FatherName, ' ', P1.P_LastName) as 'Beneficiary Name'"
                       + " ,MP.MP_Name as 'Project Name'"
                       + " ,P1.P_Parish as 'Parish'"
                       + " ,CASE MP.MP_State WHEN 0 THEN N'Rejected' WHEN 1 THEN N'Accepted' WHEN 2 THEN N'Delayed' WHEN 4 THEN N'Financed' WHEN 5 THEN N'Closed' ELSE N'On Hold' End as 'Project State'"
                                              + " ,MP.MP_ResonOfPlace as 'Donor'"
                       + " ,C.C_Name as 'Category'"
                       + " ,L.Level_Symbol as 'Level'"
                       + from;

            string condition = " where P1.IsProjectOwner like N'YES' ";
            if(P_Name != "")
                condition += " and ( P1.P_FirstName like N'%" + P_Name + "%' or P1.P_FatherName like N'%" + P_Name + "%' or P1.P_LastName like N'%" + P_Name + "%' )";
            else if (NationalNum != "")
                condition += " and P1.P_NationalNumber like N'%" + NationalNum + "%'";
            else if (birthDate != "")
            {
                string now = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "";

                condition += " and ( P1.P_DOB BETWEEN '" + birthDate + "' and '"+ now + "') ";
            }
            
            //////Military Services
            ////if (YesMilitary_radioButton.Checked)
            ////    condition += " and P1.P_MilitaryService like N'Yes'";
            ////else if (NoMilitary_radioButton.Checked)
            ////    condition += " and P1.P_MilitaryService like N'No'";
            //Marital Status
            if (P_Married_radioButton.Checked)
                condition += " and P1.P_MaritalStatus like N'%متزوج%'";
            else if (P_NotMarried_radioButton.Checked)
                condition += " and P1.P_MaritalStatus like N'%عازب%'";
            else if (P_Divorced_radioButton.Checked)
                condition += " and ( P1.P_MaritalStatus like N'%مطلق%' or P1.P_MaritalStatus like N'%منفصل%' )";
            else if (P_Widow_radioButton.Checked)
                condition += " and P1.P_MaritalStatus like N'%أرمل%'";

            
            //Sex
            if (P_Male_radioButton.Checked)
                condition += " and P1.P_Sex like N'%ذكر%'";
            else if (P_Female_radioButton.Checked)
                condition += " and P1.P_Sex like N'%أنثى%'";
            //////lives with another family
            ////if (P_IsLivingWithFamily_radioButton.Checked)
            ////    condition += " and P1.P_IsLivingWithFamily like N'%Yes%'";
            ////else if (P_IsNotLivingWithFamily_radioButton.Checked)
            ////    condition += " and P1.P_IsLivingWithFamily like N'%No%'";

            //Project State
            if (MP_Accepted_radioButton.Checked)
                condition += " and MP.MP_State = 1";
            else if (MP_NotFinanced_radioButton.Checked)
                condition += " and MP.MP_State = 0";
            else if (MP_Delayed_radioButton.Checked)
                condition += " and MP.MP_State = 2";
            else if (MP_Hold_radioButton.Checked)
                condition += " and MP.MP_State = 3";
            else if (MP_Financed_radioButton.Checked)
                condition += " and MP.MP_State = 4";
            else if (MP_Closed_radioButton.Checked)
                condition += " and MP.MP_State = 5";

            //////Project Need Level
            ////if (MP_StateA_radioButton.Checked)
            ////    condition += " and L.Level_Symbol  like 'A'";
            ////else if (MP_StateBF_radioButton.Checked)
            ////    condition += " and L.Level_Symbol  like 'BF'";
            ////else if (MP_StateBP_radioButton.Checked)
            ////    condition += " and L.Level_Symbol  like 'BP'";
            ////else if (MP_StateC_radioButton.Checked)
            ////    condition += " and L.Level_Symbol  like 'C'";
            
            MySS.query += condition;
            
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            MP_dataGridView.ColumnHeadersVisible = false;
            MP_dataGridView.DataSource = MySS.dt;
            MP_dataGridView.ColumnHeadersVisible = true;
            DataGridViewColumn dgC1;
            if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
            {
                dgC1 = MP_dataGridView.Columns["Beneficiary_ID"] ;
                dgC1.Visible = false;
            }
            //check connection//
            Program.buildConnection();

            /////////////////////////////////
            StringBuilder s = new StringBuilder();
            s.Append("select MP_ID from microproject where MP_ID in ");
            s.Append("(");
            s.Append("select PMP.MicroProject_ID as 'MicroProject_ID'" + from + condition);
            s.Append(")");
            /////////////////////////////////

            //count rows
            string sel = "select count(*) from (" + MySS.query + ") as count";
            MySS.sc = new MySqlCommand(sel, Program.MyConn);
            Counter_textBox.Text = MySS.sc.ExecuteScalar().ToString();
            
        }
        private void MicroProject_bind(string category,string MP_ID,string MP_Name,string Parish, string intermidiarySide, string priest,string fundedBy)
        {
            //check connection//
            Program.buildConnection();
            string from = " from person_microproject PMP left outer join person P1 on P1.P_ID = PMP.Person_ID "
           + "  left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID "
           + "  left outer join intermediaryside Iss1 on Iss1.MicroProject_ID = MP.MP_ID and Iss1.IS_isPriest like N'%No'"
           + "  left outer join priest Pri on MP.MP_Priest_ID = Pri.Priest_ID "
           + "  left outer join level L on MP.MP_Level_ID = L.Level_ID "
           + "  left outer join category C on MP.MP_Category_ID = C.C_ID ";

            MySS.query = " select PMP.MicroProject_ID as 'MicroProject_ID'"
                       + " ,PMP.Person_ID as 'Beneficiary_ID'"
                       + " ,CONCAT(P1.P_FirstName, ' ', P1.P_FatherName, ' ', P1.P_LastName) as 'Beneficiary Name'"
                       + " ,MP.MP_Name as 'Project Name'"
                       + " ,P1.P_Parish as 'Parish'"
                       + " ,CASE MP.MP_State WHEN 0 THEN N'Rejected' WHEN 1 THEN N'Accepted' WHEN 2 THEN N'Delayed' WHEN 4 THEN N'Financed' WHEN 5 THEN N'Closed' ELSE N'On Hold' End as 'Project State'"
                       + " ,MP.MP_ResonOfPlace as 'Donor'"
                       + " ,C.C_Name as 'Category'"
                       + " ,L.Level_Symbol as 'Level'"
                       + " ,Iss1.IS_Name as 'Intermidate Side'"
                       + " ,Pri.Priest_Name as 'Priest'"
                       + from ;


            string condition = " where P1.IsProjectOwner like N'YES' ";
            //Project ID
            if (MP_ID != "")
                // condition += " and CAST(PMP.MicroProject_ID AS nvarchar(Max)) LIKE '" + MP_ID + "%'";
                condition += " and PMP.MicroProject_ID like CAST('" + MP_ID + "%' AS CHAR)";

            //Funded By
            if (fundedBy !="")
                condition += " and MP.MP_ResonOfPlace like '" + fundedBy + "'";
            //Project Category
            if (category != "")
                condition += " and C.C_Name like N'%" + category + "%'";
            //Project State
            if ( MP_Accepted_radioButton.Checked)
                condition += " and MP.MP_State = 1";
            else if (MP_NotFinanced_radioButton.Checked)
                condition += " and MP.MP_State = 0";
            else if (MP_Delayed_radioButton.Checked)
                condition += " and MP.MP_State = 2";
            else if (MP_Hold_radioButton.Checked)
                condition += " and MP.MP_State = 3";
            else if (MP_Financed_radioButton.Checked)
                condition += " and MP.MP_State = 4";
            else if (MP_Closed_radioButton.Checked)
                condition += " and MP.MP_State = 5";

            if (MP_CostFrom_textBox.Text != "")
                if (MP_CostTo_textBox.Text != "")
                    condition += " and (MP.MP_AllPriceNeeded between '" + MP_CostFrom_textBox.Text + "' and '" + MP_CostTo_textBox.Text + "') ";
                else
                    condition += " and (MP.MP_AllPriceNeeded between '" + MP_CostFrom_textBox.Text + "' and '" + MP_CostFrom_textBox.Text + "' )";

            
            if (MP_Name != "")
                condition += " and MP.MP_Name like N'" + MP_Name + "%'";
            if (Parish != "")
                condition += " and P1.P_Parish like  N'%" + Parish + "%'";
            if (intermidiarySide != "")
                condition += " and Iss1.IS_Name like N'" + intermidiarySide + "%' and Iss1.IS_Name IS NOT NULL";
            if (priest != "")
                condition += " and Pri.Priest_Name like N'" + priest + "%'";

            MySS.query += condition;
            
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            MP_dataGridView.ColumnHeadersVisible = false;
            MP_dataGridView.DataSource = MySS.dt;
            MP_dataGridView.ColumnHeadersVisible = true;
            DataGridViewColumn dgC1;
            if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
            {
                dgC1 = MP_dataGridView.Columns["Beneficiary_ID"];
                dgC1.Visible = false;
            }
            //check connection//
            Program.buildConnection();

            /////////////////////////////////
            StringBuilder s = new StringBuilder();
            s.Append("select MP_ID from microproject where MP_ID in ");
            s.Append("(");
            s.Append("select PMP.MicroProject_ID as 'MicroProject_ID'" + from + condition);
            s.Append(")");
            /////////////////////////////////

            //count rows
            string sel = "select count(*) from (" + s + ") as count";
            MySS.sc = new MySqlCommand(sel, Program.MyConn);
            Counter_textBox.Text = MySS.sc.ExecuteScalar().ToString();
        }

        private void Category_bind()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select C_ID,C_Name from category";
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
            MP_Priest_comboBox.DisplayMember = "Priest_Name";
            MP_Priest_comboBox.ValueMember = "Priest_ID";
            MP_Priest_comboBox.DataSource = MySS.dt;
        }
        #endregion

        private void FilterAndMonitor_Load(object sender, EventArgs e)
        {
            try {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.Search_ToLight(this);
                else
                    myTheme.Search_ToNight(this);

                MySS = new MySqlComponents();
                bind("", "", "", "", "", "");
                SelectedDataRow = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MP_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)MP_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    PersonID = Int32.Parse(SelectedDataRow["Beneficiary_ID"].ToString());
                    Details Details = new Details(PersonID);
                    Details.Show();
                }
                else
                    throw new Exception("Please Select a row first");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Please Select a row first"))
                    MessageBox.Show("Please Select a row first");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        #region person data
        private void PersonDOB_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            string date = PersonDOB_dateTimePicker.Value.Year.ToString() + "-" + PersonDOB_dateTimePicker.Value.Month.ToString() + "-" + PersonDOB_dateTimePicker.Value.Day.ToString() + "";
            Person_bind(PersonFirstName_textBox.Text, PersonNationalNum_textBox.Text, date);

        }
        private void PersonFirstName_textBox_TextChanged(object sender, EventArgs e)
        {
            Person_bind(PersonFirstName_textBox.Text, PersonNationalNum_textBox.Text, "");
            //  bind(PersonFirstName_textBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, MP_Parish_comboBox.Text);
        }
        private void AllPersonSex_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            Person_bind(PersonFirstName_textBox.Text, PersonNationalNum_textBox.Text, "");
            //bind(PersonFirstName_textBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, MP_Parish_comboBox.Text);
        }
        private void AllPersonMarital_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            Person_bind(PersonFirstName_textBox.Text, PersonNationalNum_textBox.Text, "");
            // bind(PersonFirstName_textBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, MP_Parish_comboBox.Text);
        }
        private void AllPersonLiveWithFamily_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            Person_bind(PersonFirstName_textBox.Text, PersonNationalNum_textBox.Text, "");
            //bind(PersonFirstName_textBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, MP_Parish_comboBox.Text);
        }
        private void YesMilitary_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            Person_bind(PersonFirstName_textBox.Text, PersonNationalNum_textBox.Text, "");
        }
        #endregion

        #region MicroProject data
        private void MP_StateALL_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            MicroProject_bind(MP_Category_comboBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, P_Parish_comboBox.Text, MP_Intermidary_textBox.Text, MP_Priest_comboBox.Text, replaceQuotation(FundedBy_comboBox.Text));
            //bind(PersonFirstName_textBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, MP_Parish_comboBox.Text);
        }
        private void MP_AllFinanced_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            MicroProject_bind(MP_Category_comboBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, P_Parish_comboBox.Text, MP_Intermidary_textBox.Text, MP_Priest_comboBox.Text, replaceQuotation(FundedBy_comboBox.Text));
            //bind(PersonFirstName_textBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, MP_Parish_comboBox.Text);
        }
        private void MP_AllNeedLicense_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            MicroProject_bind(MP_Category_comboBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, P_Parish_comboBox.Text, MP_Intermidary_textBox.Text, MP_Priest_comboBox.Text, replaceQuotation(FundedBy_comboBox.Text));
            //bind(PersonFirstName_textBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, MP_Parish_comboBox.Text);
        }
        private void MP_CostFrom_textBox_TextChanged(object sender, EventArgs e)
        {
            MicroProject_bind(MP_Category_comboBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, P_Parish_comboBox.Text, MP_Intermidary_textBox.Text, MP_Priest_comboBox.Text, replaceQuotation(FundedBy_comboBox.Text));
            //bind(PersonFirstName_textBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, MP_Parish_comboBox.Text);
        }
        private void MP_Category_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MicroProject_bind(MP_Category_comboBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, P_Parish_comboBox.Text, MP_Intermidary_textBox.Text, MP_Priest_comboBox.Text, replaceQuotation(FundedBy_comboBox.Text));
            //bind(MP_Category_comboBox.Text.ToString());
        }
        private void MP_Parish_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MicroProject_bind(MP_Category_comboBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, P_Parish_comboBox.Text, MP_Intermidary_textBox.Text, MP_Priest_comboBox.Text, replaceQuotation(FundedBy_comboBox.Text));
            //bind(PersonFirstName_textBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, MP_Intermidary_textBox.Text, MP_Priest_textBox.Text, MP_Parish_comboBox.Text);
        }
        private void MP_Priest_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MicroProject_bind(MP_Category_comboBox.Text, MP_ID_textBox.Text, MP_Name_textBox.Text, P_Parish_comboBox.Text, MP_Intermidary_textBox.Text, MP_Priest_comboBox.Text, replaceQuotation(FundedBy_comboBox.Text));
        }
        #endregion

        #region btn clicks
        private void MoreDetails_button_Click(object sender, EventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)MP_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    PersonID = Int32.Parse(SelectedDataRow["Beneficiary_ID"].ToString());
                    Details Details = new Details(PersonID);
                    Details.Show();
                }
                else
                    throw new Exception("Please Select a row first");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Please Select a row first"))
                    MessageBox.Show("Please Select a row first");
                else
                    MessageBox.Show(ex.Message);
            }
        }
        private void ExportToExcel_button_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Exported from gridview";
            // storing header part in Excel  
            for (int i = 1; i < MP_dataGridView.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = MP_dataGridView.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < MP_dataGridView.Rows.Count - 1; i++)
            {
                for (int j = 0; j < MP_dataGridView.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = MP_dataGridView.Rows[i].Cells[j].Value.ToString();
                }
            }
        }
        private void statistics_button_Click(object sender, EventArgs e)
        {
            Statistics Statistics = new Statistics();
            Statistics.Show();
        }
        private void MainBack_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void MP_Category_comboBox_Enter(object sender, EventArgs e)
        {
            Category_bind();
        }

        private void MP_Priest_comboBox_Enter(object sender, EventArgs e)
        {
            Priest_bind();
        }

        private string replaceQuotation(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FilterAndMonitor_Load(sender, e);
        }
    }
}
