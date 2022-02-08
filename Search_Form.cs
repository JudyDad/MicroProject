using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;
using MyWorkApplication.Visit_Forms;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.Threading;
using System.Diagnostics;

namespace MyWorkApplication
{
    public partial class Search_Form : Form
    {
        private DataGridViewColumnSelector cs;
        private string ApplyDate_condition = "";
        private string FundedAmount_condition = "";
        private string FundedDate_condition = "";
        private string RequestedAmount_condition = "";
        private string visitDate_condition = "";
        private string mevisitDate_condition = "";
         
        private bool Hided;
        private int[] IDs_array, Installment_IDs_array, PIDs_array;
        private int MicroProject_ID, Person_ID, PMP_ID, Family_ID, Image_ID, ExeFile_ID;
        private string[] P_Names_array; 
        private bool partners = false; 
        private int PW;
        
        private DataRow SelectedDataRow, LoanSelectedDataRow, PersonSelectedDataRow;
        private bool show_images;
        private List<string> searchBy_list;
        private int total_bind_count;

        private MainForm main_form;
        MySqlComponents MySS;
        NewTheme NewTheme;
        Select s; Log l;
        Street st; SubCategory sub;
        DataTable donor_group_dt, sub_category_dt;
        bool user_mode;
        DataSet ds;
         
        public Search_Form(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;

            Bind_All_ComboBoxes();
        }

        public Search_Form(MainForm main_form
            , string FundType, string Type, string SubType
            , string Partners, string State
            , string Donor, string DonorGroup
            , string Category, string SubCategory
            , string Parish, string Age, string Sex, string MaritalStatus )
        {
            InitializeComponent();
            this.main_form = main_form;

            Bind_All_ComboBoxes();

            user_mode = false;
            FundType_comboBox.Text = FundType;
            Type_comboBox.Text = Type;
            SubType_comboBox.Text = SubType;

            MP_Status_comboBox.Text = State;
            Partnership_comboBox.Text = Partners;

            Donor_comboBox.Text = Donor;
            DonorGroup_comboBox.Text = DonorGroup;

            MP_Category_comboBox.Text = Category;
            SubCategory_comboBox.Text = SubCategory;

            P_Parish_comboBox.Text = Parish;
            Age_comboBox.Text = Age;
            Gender_comboBox.Text = Sex;
            MaritalStatus_comboBox.Text = MaritalStatus;
            user_mode = true;
        }
         
        private void Search_Form_Load(object sender, EventArgs e)
        {
            try
            {
                MySS = new MySqlComponents();
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
                MP_dataGridView.DoubleBuffered(true);

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
                 
                SearchBy_comboBox.SelectedIndex = 0; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void Check_Visibility()
        {
            ApplyDateFrom_label.Text = "تاريخ التقديم: (من)";
            if(Sum_FamilyMembers_label.Visible) Sum_FamilyMembers_label.Visible = false;
            if(visit_statistics_tableLayoutPanel.Visible) visit_statistics_tableLayoutPanel.Visible = false;
            if(Financial_tableLayoutPanel.Visible) Financial_tableLayoutPanel.Visible = false;
            if(Export_dolar_label.Visible) Export_dolar_label.Visible = false;
            if(showHideProfileImagesToolStripMenuItem.Visible) showHideProfileImagesToolStripMenuItem.Visible = false;
            if(Evaluation_button.Visible) Evaluation_button.Visible = false;
            if(Delete_button.Visible) Delete_button.Visible = false; 

            if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(0))
            { 
                FundType_comboBox.Visible = Type_comboBox.Visible = SubType_comboBox.Visible = Partnership_comboBox.Visible =
                    MP_Status_comboBox.Visible = Donor_comboBox.Visible = DonorGroup_comboBox.Visible = Street_comboBox.Visible =
                    MP_Category_comboBox.Visible = SubCategory_comboBox.Visible = FundDate_comboBox.Visible = ApplyDate_comboBox.Visible =
                    P_Parish_comboBox.Visible = Age_comboBox.Visible = Gender_comboBox.Visible = MaritalStatus_comboBox.Visible 
                    =  true;
                
                FundType_label.Visible = Type_label.Visible = SubType_label.Visible = Partnership_label.Visible
                    = MP_Status_label.Visible = Donor_label.Visible = DonorGroup_label.Visible = Street_label.Visible 
                    = MP_Category_label.Visible = SubCategory_label.Visible = FundDate_lable.Visible = ApplyDate_lable.Visible  
                    = P_Parish_label.Visible = Age_label.Visible = Gender_label.Visible = MaritalStatus_label.Visible
                    =  true; 

                RequestedFrom_textBox.Visible = RequestedTo_textBox.Visible =
                    RequestedFrom_label.Visible = RequestedTo_label.Visible =  
                    FundedFrom_textBox.Visible = FundedTo_textBox.Visible = 
                    FundedFrom_label.Visible = FundedTo_label.Visible =  
                    ApplyDateFrom_label.Visible = ApplyDateFrom_dateTimePicker.Visible =
                    ApplyDateTo_label.Visible = ApplyDateTo_DateTimePicker.Visible =  
                    FundedDateFrom_label.Visible = FundedDateFrom_DateTimePicker.Visible =
                    FundedDateTo_label.Visible = FundedDateTo_DateTimePicker.Visible = 
                    ApplyDate_checkBox.Visible = FundedDate_checkBox.Visible =
                    Requested_checkBox.Visible = Funded_checkBox.Visible = true;
                  
                showHideProfileImagesToolStripMenuItem.Visible = true;
                Sum_FamilyMembers_label.Visible = true;
            }
            // families //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(1))
            {
                FundType_comboBox.Visible = Type_comboBox.Visible = SubType_comboBox.Visible = Partnership_comboBox.Visible =
                    MP_Status_comboBox.Visible = Donor_comboBox.Visible = DonorGroup_comboBox.Visible = Street_comboBox.Visible =
                    MP_Category_comboBox.Visible = SubCategory_comboBox.Visible = FundDate_comboBox.Visible = ApplyDate_comboBox.Visible =
                    P_Parish_comboBox.Visible = Age_comboBox.Visible = Gender_comboBox.Visible = MaritalStatus_comboBox.Visible
                    = false;

                FundType_label.Visible = Type_label.Visible = SubType_label.Visible = Partnership_label.Visible
                    = MP_Status_label.Visible = Donor_label.Visible = DonorGroup_label.Visible = Street_label.Visible
                    = MP_Category_label.Visible = SubCategory_label.Visible = FundDate_lable.Visible = ApplyDate_lable.Visible
                    = P_Parish_label.Visible = Age_label.Visible = Gender_label.Visible = MaritalStatus_label.Visible
                    = false;

                RequestedFrom_textBox.Visible = RequestedTo_textBox.Visible =
                    RequestedFrom_label.Visible = RequestedTo_label.Visible =
                    FundedFrom_textBox.Visible = FundedTo_textBox.Visible =
                    FundedFrom_label.Visible = FundedTo_label.Visible =
                    ApplyDateFrom_label.Visible = ApplyDateFrom_dateTimePicker.Visible =
                    ApplyDateTo_label.Visible = ApplyDateTo_DateTimePicker.Visible =
                    FundedDateFrom_label.Visible = FundedDateFrom_DateTimePicker.Visible =
                    FundedDateTo_label.Visible = FundedDateTo_DateTimePicker.Visible =
                    ApplyDate_checkBox.Visible = FundedDate_checkBox.Visible =
                    Requested_checkBox.Visible = Funded_checkBox.Visible = false;

                //delete button//
                if (Settings.Default.role == 1 || Settings.Default.role == 8)
                    Delete_button.Visible = true;
                else Delete_button.Visible = false;
            }
            // Loans //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(2))
            {  
                FundType_comboBox.Visible = Type_comboBox.Visible = SubType_comboBox.Visible = Partnership_comboBox.Visible =
                    MP_Status_comboBox.Visible = Donor_comboBox.Visible = DonorGroup_comboBox.Visible = Street_comboBox.Visible =
                    MP_Category_comboBox.Visible = SubCategory_comboBox.Visible = FundDate_comboBox.Visible = ApplyDate_comboBox.Visible 
                    = true;
                P_Parish_comboBox.Visible = Age_comboBox.Visible = Gender_comboBox.Visible = MaritalStatus_comboBox.Visible = false;

                FundType_label.Visible = Type_label.Visible = SubType_label.Visible = Partnership_label.Visible
                    = MP_Status_label.Visible = Donor_label.Visible = DonorGroup_label.Visible = Street_label.Visible
                    = MP_Category_label.Visible = SubCategory_label.Visible = FundDate_lable.Visible = ApplyDate_lable.Visible 
                    = true;
                P_Parish_label.Visible = Age_label.Visible = Gender_label.Visible = MaritalStatus_label.Visible = false;

                RequestedFrom_textBox.Visible = RequestedTo_textBox.Visible =
                    RequestedFrom_label.Visible = RequestedTo_label.Visible =
                    FundedFrom_textBox.Visible = FundedTo_textBox.Visible =
                    FundedFrom_label.Visible = FundedTo_label.Visible =
                    ApplyDateFrom_label.Visible = ApplyDateFrom_dateTimePicker.Visible =
                    ApplyDateTo_label.Visible = ApplyDateTo_DateTimePicker.Visible =
                    FundedDateFrom_label.Visible = FundedDateFrom_DateTimePicker.Visible =
                    FundedDateTo_label.Visible = FundedDateTo_DateTimePicker.Visible =
                    ApplyDate_checkBox.Visible = FundedDate_checkBox.Visible =
                    Requested_checkBox.Visible = Funded_checkBox.Visible = true;
                 
                Export_dolar_label.Visible  = Export_val_label.Visible =
                Remaining_val_label.Visible = Import_val_label.Visible =
                Returned_val_label.Visible  = Net_val_label.Visible = true;

                Financial_tableLayoutPanel.Visible = true;
                Evaluation_button.Visible = false; 
            }
            // Attachments //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(3))
            {
                FundType_comboBox.Visible = Type_comboBox.Visible = SubType_comboBox.Visible = Partnership_comboBox.Visible =
                    MP_Status_comboBox.Visible = Donor_comboBox.Visible = DonorGroup_comboBox.Visible = Street_comboBox.Visible =
                    MP_Category_comboBox.Visible = SubCategory_comboBox.Visible = FundDate_comboBox.Visible = ApplyDate_comboBox.Visible
                    = true;
                P_Parish_comboBox.Visible = Age_comboBox.Visible = Gender_comboBox.Visible = MaritalStatus_comboBox.Visible = false;

                FundType_label.Visible = Type_label.Visible = SubType_label.Visible = Partnership_label.Visible
                    = MP_Status_label.Visible = Donor_label.Visible = DonorGroup_label.Visible = Street_label.Visible
                    = MP_Category_label.Visible = SubCategory_label.Visible = FundDate_lable.Visible = ApplyDate_lable.Visible
                    = true;
                P_Parish_label.Visible = Age_label.Visible = Gender_label.Visible = MaritalStatus_label.Visible = false;

                RequestedFrom_textBox.Visible = RequestedTo_textBox.Visible =
                    RequestedFrom_label.Visible = RequestedTo_label.Visible =
                    FundedFrom_textBox.Visible = FundedTo_textBox.Visible =
                    FundedFrom_label.Visible = FundedTo_label.Visible =
                    ApplyDateFrom_label.Visible = ApplyDateFrom_dateTimePicker.Visible =
                    ApplyDateTo_label.Visible = ApplyDateTo_DateTimePicker.Visible =
                    FundedDateFrom_label.Visible = FundedDateFrom_DateTimePicker.Visible =
                    FundedDateTo_label.Visible = FundedDateTo_DateTimePicker.Visible =
                    ApplyDate_checkBox.Visible = FundedDate_checkBox.Visible =
                    Requested_checkBox.Visible = Funded_checkBox.Visible = false;
                 
                showHideProfileImagesToolStripMenuItem.Visible = true;
            }
            //Executive Files //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(4))
            {
                FundType_comboBox.Visible = Type_comboBox.Visible = SubType_comboBox.Visible = Partnership_comboBox.Visible =
                    MP_Status_comboBox.Visible = Donor_comboBox.Visible = DonorGroup_comboBox.Visible = Street_comboBox.Visible =
                    MP_Category_comboBox.Visible = SubCategory_comboBox.Visible = FundDate_comboBox.Visible = ApplyDate_comboBox.Visible
                    = true;
                P_Parish_comboBox.Visible = Age_comboBox.Visible = Gender_comboBox.Visible = MaritalStatus_comboBox.Visible = false;

                FundType_label.Visible = Type_label.Visible = SubType_label.Visible = Partnership_label.Visible
                    = MP_Status_label.Visible = Donor_label.Visible = DonorGroup_label.Visible = Street_label.Visible
                    = MP_Category_label.Visible = SubCategory_label.Visible = FundDate_lable.Visible = ApplyDate_lable.Visible
                    = true;
                P_Parish_label.Visible = Age_label.Visible = Gender_label.Visible = MaritalStatus_label.Visible = false;

                RequestedFrom_textBox.Visible = RequestedTo_textBox.Visible =
                    RequestedFrom_label.Visible = RequestedTo_label.Visible =
                    FundedFrom_textBox.Visible = FundedTo_textBox.Visible =
                    FundedFrom_label.Visible = FundedTo_label.Visible =
                    ApplyDateFrom_label.Visible = ApplyDateFrom_dateTimePicker.Visible =
                    ApplyDateTo_label.Visible = ApplyDateTo_DateTimePicker.Visible =
                    FundedDateFrom_label.Visible = FundedDateFrom_DateTimePicker.Visible =
                    FundedDateTo_label.Visible = FundedDateTo_DateTimePicker.Visible =
                    ApplyDate_checkBox.Visible = FundedDate_checkBox.Visible =
                    Requested_checkBox.Visible = Funded_checkBox.Visible = true;

                Export_dolar_label.Visible  = Export_val_label.Visible = false;
                Remaining_val_label.Visible = Import_val_label.Visible = false;
            }
            // Visits //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(5))
            {
                //statistics table
                visit_statistics_tableLayoutPanel.Visible = true;

                FundType_comboBox.Visible = Type_comboBox.Visible = SubType_comboBox.Visible = Partnership_comboBox.Visible =
                MP_Status_comboBox.Visible = Donor_comboBox.Visible = DonorGroup_comboBox.Visible = Street_comboBox.Visible =
                MP_Category_comboBox.Visible = SubCategory_comboBox.Visible = FundDate_comboBox.Visible = ApplyDate_comboBox.Visible 
                 = true;
                P_Parish_comboBox.Visible = Age_comboBox.Visible = Gender_comboBox.Visible = MaritalStatus_comboBox.Visible = false;

                FundType_label.Visible = Type_label.Visible = SubType_label.Visible = Partnership_label.Visible
                    = MP_Status_label.Visible = Donor_label.Visible = DonorGroup_label.Visible = Street_label.Visible
                    = MP_Category_label.Visible = SubCategory_label.Visible = FundDate_lable.Visible = ApplyDate_lable.Visible 
                    = true;
                P_Parish_label.Visible = Age_label.Visible = Gender_label.Visible = MaritalStatus_label.Visible = false;

                ApplyDateFrom_label.Text = "تاريخ الزيارة: (من)";
                ApplyDateFrom_label.Visible   = ApplyDateFrom_dateTimePicker.Visible =
                    ApplyDateTo_label.Visible = ApplyDateTo_DateTimePicker.Visible =
                    FundedDateFrom_label.Visible = FundedDateFrom_DateTimePicker.Visible =
                    FundedDateTo_label.Visible = FundedDateTo_DateTimePicker.Visible =
                    ApplyDate_checkBox.Visible = FundedDate_checkBox.Visible = true;
                
                 
                RequestedFrom_textBox.Visible   = RequestedTo_textBox.Visible = 
                    RequestedFrom_label.Visible = RequestedTo_label.Visible =
                    FundedFrom_textBox.Visible  = FundedTo_textBox.Visible =
                    FundedFrom_label.Visible    = FundedTo_label.Visible = 
                    Requested_checkBox.Visible  = Funded_checkBox.Visible = false;  

            }
            // New Visit Forms //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(6))
            {
                FundType_comboBox.Visible = Type_comboBox.Visible = SubType_comboBox.Visible = Partnership_comboBox.Visible =
                    MP_Status_comboBox.Visible = Donor_comboBox.Visible = DonorGroup_comboBox.Visible = Street_comboBox.Visible =
                    MP_Category_comboBox.Visible = SubCategory_comboBox.Visible = FundDate_comboBox.Visible = ApplyDate_comboBox.Visible =
                    P_Parish_comboBox.Visible = Age_comboBox.Visible = Gender_comboBox.Visible = MaritalStatus_comboBox.Visible
                    = true;

                FundType_label.Visible = Type_label.Visible = SubType_label.Visible = Partnership_label.Visible
                    = MP_Status_label.Visible = Donor_label.Visible = DonorGroup_label.Visible = Street_label.Visible
                    = MP_Category_label.Visible = SubCategory_label.Visible = FundDate_lable.Visible = ApplyDate_lable.Visible
                    = P_Parish_label.Visible = Age_label.Visible = Gender_label.Visible = MaritalStatus_label.Visible
                    = true; 

                RequestedFrom_textBox.Visible = RequestedTo_textBox.Visible =
                    RequestedFrom_label.Visible = RequestedTo_label.Visible =
                    FundedFrom_textBox.Visible = FundedTo_textBox.Visible =
                    FundedFrom_label.Visible = FundedTo_label.Visible =
                    ApplyDateFrom_label.Visible = ApplyDateFrom_dateTimePicker.Visible =
                    ApplyDateTo_label.Visible = ApplyDateTo_DateTimePicker.Visible =
                    FundedDateFrom_label.Visible = FundedDateFrom_DateTimePicker.Visible =
                    FundedDateTo_label.Visible = FundedDateTo_DateTimePicker.Visible =
                    ApplyDate_checkBox.Visible = FundedDate_checkBox.Visible =
                    Requested_checkBox.Visible = Funded_checkBox.Visible = false;
            }
            // Checklists //
            else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(7))
            {
                FundType_comboBox.Visible = Type_comboBox.Visible = SubType_comboBox.Visible = Partnership_comboBox.Visible =
                    MP_Status_comboBox.Visible = Donor_comboBox.Visible = DonorGroup_comboBox.Visible = Street_comboBox.Visible =
                    MP_Category_comboBox.Visible = SubCategory_comboBox.Visible = FundDate_comboBox.Visible = ApplyDate_comboBox.Visible =
                    P_Parish_comboBox.Visible = Age_comboBox.Visible = Gender_comboBox.Visible = MaritalStatus_comboBox.Visible = false;

                FundType_label.Visible = Type_label.Visible = SubType_label.Visible = Partnership_label.Visible
                    = MP_Status_label.Visible = Donor_label.Visible = DonorGroup_label.Visible = Street_label.Visible
                    = MP_Category_label.Visible = SubCategory_label.Visible = FundDate_lable.Visible = ApplyDate_lable.Visible
                    = P_Parish_label.Visible = Age_label.Visible = Gender_label.Visible = MaritalStatus_label.Visible = false;

                RequestedFrom_textBox.Visible = RequestedTo_textBox.Visible = RequestedFrom_label.Visible =
                    RequestedTo_label.Visible =
                        FundedFrom_textBox.Visible = FundedTo_textBox.Visible = FundedFrom_label.Visible =
                            FundedTo_label.Visible =
                                ApplyDateFrom_label.Visible = ApplyDateFrom_dateTimePicker.Visible =
                                    ApplyDateTo_label.Visible = ApplyDateTo_DateTimePicker.Visible =
                                        FundedDateFrom_label.Visible = FundedDateFrom_DateTimePicker.Visible =
                                            FundedDateTo_label.Visible = FundedDateTo_DateTimePicker.Visible =
                                                ApplyDate_checkBox.Visible = FundedDate_checkBox.Visible =
                                                    Requested_checkBox.Visible = Funded_checkBox.Visible = false;
            }
        }

        private void calculate_ImportExportMoney(string str)
        {
            string donor="";
            if(Donor_comboBox.Text != "")
            {
                donor = replaceQuotation(Donor_comboBox.Text); 
            }

            var dt2 = s.Select_Loan_Sum(str, donor);
            if (dt2.Rows.Count != 0)
            {
                Export_val_label.Text = "Exports:" + (dt2.Rows[0]["Export"] == DBNull.Value
                    ? "0"
                    : Convert.ToDecimal(dt2.Rows[0]["Export"]).ToString("#,##0"));
                Export_dolar_label.Text = "Exports($):" + (dt2.Rows[0]["Export($)"] == DBNull.Value
                    ? "0"
                    : Convert.ToDecimal(dt2.Rows[0]["Export($)"]).ToString("#,##0"));
                Import_val_label.Text = "Imports:" + (dt2.Rows[0]["Import"] == DBNull.Value
                    ? "0"
                    : Convert.ToDecimal(dt2.Rows[0]["Import"]).ToString("#,##0"));
                Remaining_val_label.Text = "Remaining:" + (dt2.Rows[0]["Remaining"] == DBNull.Value
                    ? "0"
                    : Convert.ToDecimal(dt2.Rows[0]["Remaining"]).ToString("#,##0"));

                Returned_val_label.Text = "Returned:" + (dt2.Rows[0]["Returned"] == DBNull.Value
                   ? "0"
                   : Convert.ToDecimal(dt2.Rows[0]["Returned"]).ToString("#,##0"));

                Net_val_label.Text = "Net:" + (dt2.Rows[0]["Net"] == DBNull.Value
                   ? "0"
                   : Convert.ToDecimal(dt2.Rows[0]["Net"]).ToString("#,##0"));
            }
        }
         
        private void Refresh_button_Click(object sender, EventArgs e)
        {
            save_visible_columns(SearchBy_comboBox.SelectedIndex);
            SearchBy_comboBox_SelectedIndexChanged(sender, e);
        }
         
        private string replaceQuotation(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }

        private void MP_dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void showHideProfileImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (show_images)
                    show_images = false;
                else show_images = true;
                SearchBy_comboBox_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
                visitDate_condition = " and ( visit.Date between '" + beg  + "' and '" + end  + "' )";
                mevisitDate_condition = " and ( mevisit.Date between '" + beg  + "' and '" + end  + "')";
                 
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

        private void ShowHideColumns_button_MouseClick(object sender, MouseEventArgs e)
        {
            cs.mDataGridView_MouseClick(sender, e);
        }

        private void Evaluation_button_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in MP_dataGridView.Rows)
                {
                    var found = 0;
                    if (r.DefaultCellStyle.BackColor == Evaluation_button.BackColor)
                    {
                        MP_dataGridView.Rows[r.Index].Visible = true;
                        found = 1;
                    }
                    else
                    {
                        MP_dataGridView.CurrentCell = null;
                        MP_dataGridView.Rows[r.Index].Visible = false;
                    }
                } 
                Decimal count = MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible);
                Count_label.Text = "All:" + count.ToString("#,##0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //private void Middle_button_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (DataGridViewRow r in MP_dataGridView.Rows)
        //        {
        //            int found = 0;
        //            if (r.DefaultCellStyle.BackColor == Middle_button.BackColor)
        //            {
        //                MP_dataGridView.Rows[r.Index].Visible = true;
        //                found = 1;
        //            }
        //            else
        //            {
        //                MP_dataGridView.CurrentCell = null;
        //                MP_dataGridView.Rows[r.Index].Visible = false;
        //            }
        //        }
        //        Count_label.Text = "All:" + MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.Message); }
        //} 
        //private void Bad_button_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (DataGridViewRow r in MP_dataGridView.Rows)
        //        {
        //            int found = 0;
        //            if (r.DefaultCellStyle.BackColor == Bad_button.BackColor)
        //            {
        //                MP_dataGridView.Rows[r.Index].Visible = true;
        //                found = 1;
        //            }
        //            else
        //            {
        //                MP_dataGridView.CurrentCell = null;
        //                MP_dataGridView.Rows[r.Index].Visible = false;
        //            }
        //        }
        //        Count_label.Text = "All:" + MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.Message); }
        //}

        //private void checkComboBox1_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (sender is CheckComboBoxItem)
        //    {
        //        CheckComboBoxItem item = (CheckComboBoxItem)sender;
        //        switch (item.Text)
        //        {
        //            case "One":
        //                checkBox1.Checked = item.CheckState;
        //                break;
        //            case "Two":
        //                checkBox2.Checked = item.CheckState;
        //                break;
        //            case "Three":
        //                checkBox3.Checked = item.CheckState;
        //                break;
        //        }
        //    }
        //}

        //#region checkbox combobox
        //private StatusList _StatusList;

        //private ListSelectionWrapper<Status> StatusSelections;

        //private void PopulateManualCombo()
        //{
        //    cmbManual.Items.Add("Item 1");
        //    cmbManual.Items.Add("Item 2");
        //    cmbManual.Items.Add("Item 3");
        //    cmbManual.Items.Add("Item 4");
        //    cmbManual.Items.Add("Item 5");
        //    cmbManual.Items.Add("Item 6");
        //    cmbManual.Items.Add("Item 7");
        //    cmbManual.Items.Add("Item 8");

        //    cmbManual.CheckBoxItems[1].Checked = true;
        //}
        //#endregion
        
        private void MP_dataGridView_Sorted(object sender, EventArgs e)
        {
            //if (SearchBy_comboBox.SelectedIndex == 2)
            //{
            //    Color_Cells();
            //} 
        }

        private void Search_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            save_visible_columns(SearchBy_comboBox.SelectedIndex);
        }

        #region bind queries

        private void bind_Applications(string FundType, string Type, string SubType, string State, string Partners
            , string Donor, string DonorGroup
            , string Category, string SubCategory
            , string Street, string Parish, string Age, string Sex, string MaritalStatus 
            , string ApplyYear, string FundYear, bool showImages)
        {
            try
            { 
                string from = @"  From `microproject` MP
 LEFT OUTER JOIN `person_microproject` PMP on PMP.MicroProject_ID = MP.MP_ID 
 LEFT OUTER JOIN `person` P on P.P_ID = PMP.Person_ID   
 LEFT OUTER JOIN `priest` on Priest_ID = P.P_Priest_ID
 LEFT OUTER JOIN `category` C on C.C_ID = MP.MP_Category_ID
 LEFT OUTER JOIN `subcategory` sub on sub.ID =  MP.SubCategory_ID 
 LEFT OUTER JOIN `state` on state.ID = MP.MP_State 
 LEFT OUTER JOIN `donor` on donor.ID = MP.MP_Donor
 LEFT OUTER JOIN `street` w_street on w_street.ID = MP.MP_Street_ID
 LEFT OUTER JOIN `street` h_street on h_street.ID = P.Street_ID  
 LEFT OUTER JOIN `loan` L on L.MicroProject_ID = MP.MP_ID
 LEFT OUTER JOIN `donorgroup` on donorgroup.ID = MP.DonorGroup_ID  
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID  ";


                //" , (CASE MP.MP_Type WHEN 0 THEN N'Loan' ELSE N'Grant' End) as 'نوع المشروع'"
                string query1 = @"select 
 PMP.PMP_ID as 'ID' 
 , P.P_ID as 'رقم المستفيد' 
 , MP_ID as 'رقم المشروع' 
 , CONCAT(P.P_FirstName, ' ', P.P_LastName) as 'المستفيد' 
 , P.P_FatherName as 'اسم الأب' 
 , P.P_MotherName as 'اسم الأم' 
 , (SELECT CONCAT(p.P_FirstName, ' ', p.P_FatherName, ' ', p.P_LastName) 
 From `person` p join `person_family` pf on p.P_ID = pf.Person_ID 
 WHERE pf.Relation like N'زوج/ة' and pf.P_Provider_ID = PMP.Person_ID limit 1) as 'الزوج / الزوجة' 
 , MP.MP_Name as 'اسم المشروع' 
 , C_Name 'الصنف' 
 , sub.Name as 'المهنة'  
 , P.P_Parish as 'الطائفة' 
 , IFNULL(Priest_Name,'') as 'الكاهن' 
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
 , MP.MP_StateDate as 'تاريخ تغيير الحالة' 
 , L.Loan_Amount as 'مبلغ التمويل' 
 , L.Loan_DateTaken as 'تاريخ التمويل'  
 , donor.Name as 'الجهة الممولة' 
 , donorgroup.Name as 'المجموعة'  
 , w_street.Name 'منطقة المشروع'
 , MP_AddressAfterFund  as 'موقع المشروع' 
 , (CASE MP.MP_Visited WHEN 0 THEN N'غير مزار' ELSE N'مزار' End) as 'الزيارة'  
 , (CASE MP.MP_Message WHEN 1 THEN N'تم الإرسال' ELSE 'لا يوجد' End) as 'رسالة الرفض'  
 , (CASE MP.IsContentUpdated WHEN 1 THEN N'نعم' ELSE 'لا' End) as 'تبديل المشروع'  
 , PMP.PersonType as 'صفة مقدم الطلب'  ";
                 
                if (show_images) query1 += " ,P_Picture as '#' ,P_PicturePath as 'Path' ";

                var condition = " where 1 "; 

                //Fund Type
                if (FundType != "")
                {
                    condition += " and fundtype.Name like '"+ FundType + "'";
                }
                //Type
                if (Type != "")
                {
                    condition += " and microprojecttype.Name like '" + Type + "'";
                }
                //Sub Type
                if (SubType != "")
                {
                    condition += " and microprojectsubtype.Name like '" + SubType + "'";
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

                //Project SUB Category
                if (SubCategory != "")
                    condition += " and sub.Name like N'" + SubCategory + "'";

                //Home Street
                if (Street != "")
                    condition += " and h_street.Name like N'" + Street + "'";
                 
                //Parish
                if (Parish != "")
                {
                    if (Parish == "Christian")
                        condition += " and P.P_Parish not like  N'Muslim'";
                    else condition += " and P.P_Parish like  N'" + Parish + "'";
                }

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

                //Partners
                if (Partners != "")
                {
                    if (Partners.Contains("شراكة"))
                        condition += " and Partnership = 2 ";
                    else if (Partners.Contains("فردي")) condition += " and Partnership = 1 ";
                    else condition += "";
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

                query1 += from + condition + order_by+ ";";  

                //count rows
                string query2 = "select IFNULL(count(DISTINCT MP_ID),0) " + from + condition + ";";

                //calculate sum of family members for all projects// 
                string query3 = "select IFNULL(sum(P_NumAtHome),0) " + from + condition + ";";

                //////////////////////////////////////////////////////////////////
                string query = "";
                query += query1 + query2 + query3;

                    //" START TRANSACTION; " +
                    //" COMMIT; ";
                //////////////////////////////////////////////////////////////////

                //check connection//
                Program.buildConnection();
                MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Program.MyConn.Close();

                total_bind_count = 0;
                //total_bind_count = int.Parse(sc.ExecuteScalar().ToString());
                total_bind_count = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
                Count_label.Text = "All:" + total_bind_count.ToString("#,##0");

                //////calculate sum of family members for all projects// 
                ////if (dt.Rows.Count != 0 && dt != null)
                ////{
                Decimal sumObject = 0;
                ////    sumObject = Convert.ToDecimal(dt.Compute("sum([" + "عدد الأفراد" + "])", string.Empty));
                sumObject = Convert.ToDecimal(ds.Tables[2].Rows[0][0].ToString());
                Sum_FamilyMembers_label.Text = "مجموع الأفراد: " + sumObject.ToString("#,##0");
                ////}

                //MP_dataGridView.RowHeadersVisible = false;
                MP_dataGridView.ColumnHeadersVisible = false; 
                MP_dataGridView.DataSource = ds.Tables[0];

                MP_dataGridView.Columns["تاريخ التقديم"].DefaultCellStyle.Format
                    = MP_dataGridView.Columns["تاريخ تغيير الحالة"].DefaultCellStyle.Format = "yyyy/MM/dd";
                MP_dataGridView.Columns["مبلغ التمويل"].DefaultCellStyle.Format = "#,##0";
                
                DataGridViewColumn dgC1;
                if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
                {
                    dgC1 = MP_dataGridView.Columns["رقم المستفيد"];
                    dgC1.Visible = false;
                    dgC1 = MP_dataGridView.Columns["ID"];
                    dgC1.Visible = false;
                }
                MP_dataGridView.Columns["رقم المشروع"].Width = 70;
                MP_dataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                    MP_dataGridView.Columns["الزيارة"].DefaultCellStyle.Alignment =
                        MP_dataGridView.Columns["تاريخ التقديم"].DefaultCellStyle.Alignment =
                            MP_dataGridView.Columns["الصنف"].DefaultCellStyle.Alignment =
                                MP_dataGridView.Columns["نوع المشروع"].DefaultCellStyle.Alignment =
                                    MP_dataGridView.Columns["الطائفة"].DefaultCellStyle.Alignment =
                                        MP_dataGridView.Columns["الموبايل"].DefaultCellStyle.Alignment =
                                            MP_dataGridView.Columns["الرقم الوطني"].DefaultCellStyle.Alignment =
                                                MP_dataGridView.Columns["حالة المشروع"].DefaultCellStyle.Alignment =
                                                    MP_dataGridView.Columns["الجهة الممولة"].DefaultCellStyle.Alignment =
                                                        MP_dataGridView.Columns["المجموعة"].DefaultCellStyle.Alignment =
                                                            MP_dataGridView.Columns["عدد الأفراد"].DefaultCellStyle.Alignment =
                                                        DataGridViewContentAlignment.MiddleCenter;

                if (show_images)
                {
                    var column = MP_dataGridView.Columns["#"];
                    column.Width = 70;
                    ((DataGridViewImageColumn)MP_dataGridView.Columns["#"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                }

                MP_dataGridView.ColumnHeadersVisible = true;
                //MP_dataGridView.RowHeadersVisible = true;
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void FamilyMember_bind(string family_ID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "select p.P_ID as 'ID'"
                         + ",pf.Family_ID as 'رقم العائلة'"
                         + ",f.F_Number as 'رقم دفتر العائلة'"
                         + ",CONCAT(p.P_FirstName, ' ', p.P_FatherName, ' ', p.P_LastName) as 'المستفيد'"
                         + ",p.P_MotherName as 'اسم الأم'"
                         + ",p.P_MaritalStatus as 'الحالة الاجتماعية'"
                         + ",pf.IsInNow as 'تابع لدفتر عائلة المستفيد'"
                         + ",pf.Relation as 'العلاقة مغ المستفيد'"
                         + ",pf.Work_Name as 'العمل'"
                         + ",p.P_DOB as 'تاريخ الولادة'"
                         + ",pf.P_Provider_ID as 'رقم المستفيد'"
                         + "\n From `person` p left outer join `person_family` pf on p.P_ID = pf.Person_ID left outer join `family` f on f.F_ID = pf.Family_ID  ";
            var condition = "";
            if (family_ID != "") condition = " where pf.Family_ID like CAST('" + family_ID + "' AS CHAR)";
            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            MP_dataGridView.DataSource = MySS.dt;
            Program.MyConn.Close();

            var dgC1 = MP_dataGridView.Columns["ID"];
            var dgC2 = MP_dataGridView.Columns["رقم المستفيد"];
            MP_dataGridView.Columns["تاريخ الولادة"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgC1.Visible = dgC2.Visible = false;
            Program.MyConn.Close();
            //check connection//
            Program.buildConnection();
            //count rows
            var sel = "select count(*) FROM `person`";
            MySS.sc = new MySqlCommand(sel, Program.MyConn);
            decimal count = Convert.ToDecimal(MySS.sc.ExecuteScalar());
            Count_label.Text = "All:" + count.ToString("#,##0");
            Program.MyConn.Close();

            MP_dataGridView.Columns["رقم العائلة"].Width = MP_dataGridView.Columns["تابع لدفتر عائلة المستفيد"].Width =
                MP_dataGridView.Columns["تاريخ الولادة"].Width = 60;
            MP_dataGridView.Columns["رقم دفتر العائلة"].Width = MP_dataGridView.Columns["اسم الأم"].Width =
                MP_dataGridView.Columns["العلاقة مغ المستفيد"].Width =
                    MP_dataGridView.Columns["الحالة الاجتماعية"].Width = 100;

            MP_dataGridView.Columns["رقم العائلة"].DefaultCellStyle.Alignment =
                MP_dataGridView.Columns["رقم دفتر العائلة"].DefaultCellStyle.Alignment =
                    MP_dataGridView.Columns["تاريخ الولادة"].DefaultCellStyle.Alignment =
                        MP_dataGridView.Columns["الحالة الاجتماعية"].DefaultCellStyle.Alignment =
                            MP_dataGridView.Columns["العلاقة مغ المستفيد"].DefaultCellStyle.Alignment =
                                MP_dataGridView.Columns["تابع لدفتر عائلة المستفيد"].DefaultCellStyle.Alignment =
                                    DataGridViewContentAlignment.MiddleCenter;
        }

        private void Loan_bind(string FundType, string Type, string SubType, string State, string Partners
            , string Donor, string DonorGroup
            , string Category, string SubCategory
            , string ApplyYear, string FundYear)
        {
            MP_dataGridView.ColumnHeadersVisible = false;
            MP_dataGridView.DataSource = null;
            MP_dataGridView.DataSource = s.Select_Loan_All_Data("", FundType, Type, SubType, State, Partners,  Donor, DonorGroup, Category, SubCategory, ApplyYear, FundYear);

            MP_dataGridView.Columns["تاريخ استلام القرض"].DefaultCellStyle.Format
                = MP_dataGridView.Columns["تاريخ آخر دفعة مدفوعة"].DefaultCellStyle.Format
                    = MP_dataGridView.Columns["تاريخ الدفعة المستحقة"].DefaultCellStyle.Format = "dd/MM/yyyy";
            
            MP_dataGridView.Columns["القرض"].DefaultCellStyle.Format
                = MP_dataGridView.Columns["القرض ($)"].DefaultCellStyle.Format
                    = MP_dataGridView.Columns["القسط"].DefaultCellStyle.Format
                        = MP_dataGridView.Columns["المبلغ المدفوع"].DefaultCellStyle.Format
                            = MP_dataGridView.Columns["المبلغ المتبقي"].DefaultCellStyle.Format
                                = MP_dataGridView.Columns["المبلغ المطلوب استرداده"].DefaultCellStyle.Format = "#,##0";

            MP_dataGridView.Columns["عدد الدفعات المتبقية"].DefaultCellStyle.Format = "N1";

            var dgc1 = MP_dataGridView.Columns["ID"];
            dgc1.Visible = false;

            decimal count = Convert.ToDecimal(s.Count_Distinct_Loan_IDs());
            Count_label.Text = "All:" + count.ToString("#,##0");
             
            MP_dataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                MP_dataGridView.Columns["القسط"].DefaultCellStyle.Alignment =
                    MP_dataGridView.Columns["عدد الدفعات المتبقية"].DefaultCellStyle.Alignment =
            MP_dataGridView.Columns["عدد الدفعات المدفوعة"].DefaultCellStyle.Alignment =
                MP_dataGridView.Columns["عدد الدفعات الكلي"].DefaultCellStyle.Alignment =
                    MP_dataGridView.Columns["تاريخ استلام القرض"].DefaultCellStyle.Alignment =
                        MP_dataGridView.Columns["القرض"].DefaultCellStyle.Alignment =
            MP_dataGridView.Columns["القرض ($)"].DefaultCellStyle.Alignment =
                MP_dataGridView.Columns["النسبة"].DefaultCellStyle.Alignment =
                    MP_dataGridView.Columns["المجموعة"].DefaultCellStyle.Alignment =
            MP_dataGridView.Columns["رقم الإيصال"].DefaultCellStyle.Alignment =
                MP_dataGridView.Columns["المبلغ المطلوب استرداده"].DefaultCellStyle.Alignment =
                    MP_dataGridView.Columns["المبلغ المدفوع"].DefaultCellStyle.Alignment =
                        MP_dataGridView.Columns["تاريخ آخر دفعة مدفوعة"].DefaultCellStyle.Alignment =
                            MP_dataGridView.Columns["تاريخ الدفعة المستحقة"].DefaultCellStyle.Alignment =
                                MP_dataGridView.Columns["المبلغ المتبقي"].DefaultCellStyle.Alignment =
                                MP_dataGridView.Columns["أشهر التأخير"].DefaultCellStyle.Alignment =
                                MP_dataGridView.Columns["الدفعات المستحقة والغير مدفوعة"].DefaultCellStyle.Alignment =
             DataGridViewContentAlignment.MiddleCenter;

            MP_dataGridView.ColumnHeadersVisible = true;
        }

        private void bind_Images(string FundType, string Type, string SubType, string State
            , string Donor, string DonorGroup
            , string Category, string SubCategory
            , bool showImages)
        {
            //check connection//
            Program.buildConnection();

            var from = @" from `image` 
 left OUTER join `microproject` MP on image.MicroProject_ID = MP.MP_ID  
 LEFT OUTER join `person_microproject` PMP on PMP.MicroProject_ID = MP.MP_ID 
 LEFT OUTER join `category` C on MP.MP_Category_ID = C.C_ID  
 LEFT OUTER join `subcategory` sub on MP.SubCategory_ID = sub.ID  
 LEFT OUTER join `state` on state.ID = MP.MP_State 
 Left OUTER join `donor` on donor.ID = MP.MP_Donor
 LEFT OUTER join `loan` on loan.MicroProject_ID = MP.MP_ID
 LEFT OUTER join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID  ";

            MySS.query = "SELECT Image_ID as 'ID'" +
                         ",image.MicroProject_ID as 'رقم المشروع'" +
                         ",Image_Path as 'المسار'" +
                         ",Image_Type as 'النوع'";

            if (show_images) MySS.query += " ,Image_Content as '#'";

            var condition = " where 1 ";
             
            //Fund Type
            if (FundType != "")
            {
                condition += " and fundtype.Name like '" + FundType + "'";
            }
            //Type
            if (Type != "")
            {
                condition += " and microprojecttype.Name like '" + Type + "'";
            }
            //Sub Type
            if (SubType != "")
            {
                condition += " and microprojectsubtype.Name like '" + SubType + "'";
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
                condition += " and donor.Name like N'" + Donor + "'";

            //Donor Group
            if (DonorGroup != "")
                condition += " and donorgroup.Name like '" + DonorGroup + "'";

            //Project Category
            if (Category != "")
                condition += " and C.C_Name like N'" + Category + "'";

            //Sub Category
            if (SubCategory != "")
                condition += " and sub.Name like N'" + SubCategory + "'";

            MP_dataGridView.ColumnHeadersVisible = false;

            MySS.query += from + condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            MP_dataGridView.DataSource = MySS.dt;
            var dgC2 = MP_dataGridView.Columns["ID"];
            dgC2.Visible = false;


            Program.MyConn.Close();
            //check connection//
            Program.buildConnection();
            //count rows
            var sel = "select count(DISTINCT MP_ID) " + from + condition;
            MySS.sc = new MySqlCommand(sel, Program.MyConn);
            decimal count = Convert.ToDecimal(MySS.sc.ExecuteScalar());
            Count_label.Text = "All:" + count.ToString("#,##0");
            Program.MyConn.Close();


            MP_dataGridView.Columns["رقم المشروع"].Width = MP_dataGridView.Columns["النوع"].Width = 80;

            MP_dataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                MP_dataGridView.Columns["النوع"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (show_images)
            {
                var column = MP_dataGridView.Columns["#"];
                column.Width = 70;
                ((DataGridViewImageColumn)MP_dataGridView.Columns["#"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            MP_dataGridView.ColumnHeadersVisible = true;
        }

        private void bind_ExeFile(string FundType, string Type, string SubType, string State, string Partners
             , string Donor, string DonorGroup
             , string Category, string SubCategory)
        {
            //check connection//
            Program.buildConnection();
            var from = @" from `exefile` 
 left OUTER join `microproject` MP on exefile.MicroProject_ID = MP.MP_ID  
 LEFT OUTER join `person_microproject` PMP on PMP.MicroProject_ID = MP.MP_ID  
 LEFT OUTER join `person` P on P.P_ID = PMP.Person_ID  
 LEFT OUTER join `category` C on MP.MP_Category_ID = C.C_ID  
 LEFT OUTER join `subcategory` sub on MP.SubCategory_ID = sub.ID  
 Left OUTER join `donor` on donor.ID = MP.MP_Donor
 LEFT OUTER join `state` on state.ID = MP.MP_State
 LEFT OUTER join `loan` on loan.MicroProject_ID = MP.MP_ID
 LEFT OUTER join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID  ";

            MySS.query = " select PMP.MicroProject_ID as 'رقم المشروع'"
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
             
            //Fund Type
            if (FundType != "")
            {
                condition += " and fundtype.Name like '" + FundType + "'";
            }
            //Type
            if (Type != "")
            {
                condition += " and microprojecttype.Name like '" + Type + "'";
            }
            //Sub Type
            if (SubType != "")
            {
                condition += " and microprojectsubtype.Name like '" + SubType + "'";
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
                condition += " and donor.Name like N'" + Donor + "'";

            //Donor Group
            if (DonorGroup != "")
                condition += " and donorgroup.Name like '" + DonorGroup + "'";

            //Project Category
            if (Category != "")
                condition += " and C.C_Name like N'" + Category + "'";

            //Sub Category
            if (SubCategory != "")
                condition += " and sub.Name like N'" + SubCategory + "'";

            //Partners
            if (Partners != "")
            {
                if (Partners.Contains("شراكة"))
                    condition += " and Partnership = 2 ";
                else if (Partners.Contains("فردي")) condition += " and Partnership = 1 ";
                else condition += "";
            }


            MP_dataGridView.ColumnHeadersVisible = false;
            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            MP_dataGridView.DataSource = MySS.dt;
            MP_dataGridView.Columns["تاريخ البداية"].DefaultCellStyle.Format = "dd/MM/yyyy";
            MP_dataGridView.Columns["تاريخ الاستحقاق"].DefaultCellStyle.Format = "dd/MM/yyyy";
            MP_dataGridView.Columns["تاريخ الحجز"].DefaultCellStyle.Format = "dd/MM/yyyy";
            MP_dataGridView.Columns["التاريخ الحالي"].DefaultCellStyle.Format = "dd/MM/yyyy";

            var dgc1 = MP_dataGridView.Columns["ID"];
            dgc1.Visible = false;

            Program.MyConn.Close();
            //check connection//
            Program.buildConnection();
            //count rows
            var sel = "select count(DISTINCT MP_ID) " + from + condition;
            MySS.sc = new MySqlCommand(sel, Program.MyConn);
            decimal count = Convert.ToDecimal(MySS.sc.ExecuteScalar());
            Count_label.Text = "All:" + count.ToString("#,##0");
            Program.MyConn.Close();

            MP_dataGridView.Columns["رقم المشروع"].Width = MP_dataGridView.Columns["نوع الحجز"].Width =
                MP_dataGridView.Columns["رقم الملف"].Width = 80;

            MP_dataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                MP_dataGridView.Columns["تاريخ البداية"].DefaultCellStyle.Alignment
                    = MP_dataGridView.Columns["التاريخ الحالي"].DefaultCellStyle.Alignment =
                        MP_dataGridView.Columns["تاريخ الاستحقاق"].DefaultCellStyle.Alignment
                            = MP_dataGridView.Columns["رقم الملف"].DefaultCellStyle.Alignment =
                                MP_dataGridView.Columns["عدد الأشهر"].DefaultCellStyle.Alignment
                                    = MP_dataGridView.Columns["تاريخ الحجز"].DefaultCellStyle.Alignment =
                                        MP_dataGridView.Columns["نوع الحجز"].DefaultCellStyle.Alignment =
                                            DataGridViewContentAlignment.MiddleCenter;
            MP_dataGridView.ColumnHeadersVisible = true;
        }

        private void bind_Visits(string FundType, string Type, string SubType
            , string kind, string State, string Partners
            , string Donor, string DonorGroup
            , string Category, string SubCategory
            ,string ApplyYear, string FundYear)
        {
            try
            {
                //check connection//
                Program.buildConnection();

                var from =
                    @" from `microproject` MP 
 left OUTER join `person_microproject` PMP  on PMP.MicroProject_ID = MP.MP_ID
 left OUTER join `person` P on P.P_ID = PMP.Person_ID 
 left OUTER join `category` C on MP.MP_Category_ID = C.C_ID 
 LEFT OUTER join `subcategory` sub on MP.SubCategory_ID = sub.ID  
 LEFT OUTER join `state` on state.ID = MP.MP_State  
 Left OUTER join `donor` on donor.ID = MP.MP_Donor
 LEFT OUTER join `loan` L on L.MicroProject_ID = MP.MP_ID
 LEFT OUTER join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID 
";
                 
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


                //Fund Type
                if (FundType != "")
                {
                    condition += " and fundtype.Name like '" + FundType + "'";
                }
                //Type
                if (Type != "")
                {
                    condition += " and microprojecttype.Name like '" + Type + "'";
                }
                //Sub Type
                if (SubType != "")
                {
                    condition += " and microprojectsubtype.Name like '" + SubType + "'";
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
 LEFT OUTER join `microproject` MP on visit.MicroProject_ID = MP.MP_ID
 LEFT OUTER join `category` C on MP.MP_Category_ID = C.C_ID 
 LEFT OUTER join `subcategory` sub on MP.SubCategory_ID = sub.ID  
 LEFT OUTER join `state` on state.ID = MP.MP_State  
 LEFT OUTER join `donor` on donor.ID = MP.MP_Donor 
 LEFT OUTER join `loan` L on L.MicroProject_ID = MP.MP_ID
 LEFT OUTER join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID 
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID 
";


                string mevisit_query = @" SELECT
      IFNULL(COUNT(CASE WHEN `Number`= '1' THEN 1 end), 0) as 'sum m1'
    , IFNULL(COUNT(CASE WHEN `Number`= '2' THEN 1 end), 0) as 'sum m2'
    , IFNULL(COUNT(CASE WHEN `Number`= '3' THEN 1 end), 0) as 'sum m3'
    , IFNULL(COUNT(CASE WHEN `Number`= '4' THEN 1 end), 0) as 'sum m4'
    , IFNULL(COUNT(CASE WHEN `Number`= '5' THEN 1 end), 0) as 'sum m5'
    , IFNULL(COUNT(mevisit.ID), 0) as 'total mevisit' 

    from `microproject` MP
 LEFT OUTER join `person_microproject` PMP on MP.MP_ID = PMP.MicroProject_ID 
 LEFT OUTER join `mevisit` on PMP.Person_ID = mevisit.Person_ID 
 LEFT OUTER join `category` C on MP.MP_Category_ID = C.C_ID 
 LEFT OUTER join `subcategory` sub on MP.SubCategory_ID = sub.ID  
 LEFT OUTER join `state` on state.ID = MP.MP_State  
 LEFT OUTER join `donor` on donor.ID = MP.MP_Donor  
 LEFT OUTER join `loan` L on L.MicroProject_ID = MP.MP_ID
 LEFT OUTER join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID 
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID  ";  
                 
                query += condition + " Group By MP_ID; " ;
                visit_query += condition + visitDate_condition + ";";
                mevisit_query += condition + mevisitDate_condition + " and PMP.PersonType like 'مستفيد' ;";

                string all_query = query + visit_query + mevisit_query;
                
                MySqlCommand sc = new MySqlCommand(all_query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataSet ds = new DataSet();
                da.Fill(ds);

                MP_dataGridView.ColumnHeadersVisible = false;
                MP_dataGridView.DataSource = ds.Tables[0]; 
                MP_dataGridView.Columns["Opening"].DefaultCellStyle.Format =
                    MP_dataGridView.Columns["M1"].DefaultCellStyle.Format =
                    MP_dataGridView.Columns["M2"].DefaultCellStyle.Format =
                    MP_dataGridView.Columns["M3"].DefaultCellStyle.Format =
                    MP_dataGridView.Columns["M4"].DefaultCellStyle.Format =
                    MP_dataGridView.Columns["M5"].DefaultCellStyle.Format =
                    MP_dataGridView.Columns["Closing"].DefaultCellStyle.Format = "dd/MM/yyyy";


                //count rows
                var sel = "select count(DISTINCT MP_ID) " + from + condition;
                MySqlCommand sc1 = new MySqlCommand(sel, Program.MyConn);
                decimal count = Convert.ToDecimal(sc1.ExecuteScalar());
                Count_label.Text = "All:" + count.ToString("#,##0");
                Program.MyConn.Close();

                MP_dataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                    MP_dataGridView.Columns["Opening"].DefaultCellStyle.Alignment
                        = MP_dataGridView.Columns["M1"].DefaultCellStyle.Alignment  
                        = MP_dataGridView.Columns["M2"].DefaultCellStyle.Alignment  
                        = MP_dataGridView.Columns["M3"].DefaultCellStyle.Alignment  
                        = MP_dataGridView.Columns["M4"].DefaultCellStyle.Alignment  
                        = MP_dataGridView.Columns["M5"].DefaultCellStyle.Alignment 
                        = MP_dataGridView.Columns["Closing"].DefaultCellStyle.Alignment 
                        = DataGridViewContentAlignment.MiddleCenter;
                MP_dataGridView.ColumnHeadersVisible = true;

                sumOp_label.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["sum op"]).ToString("#,##0");
                sumCl_label.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["sum cl"]).ToString("#,##0");
                sumM1_label.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["sum m1"]).ToString("#,##0");
                sumM2_label.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["sum m2"]).ToString("#,##0");
                sumM3_label.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["sum m3"]).ToString("#,##0");
                sumM4_label.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["sum m4"]).ToString("#,##0");
                sumM5_label.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["sum m5"]).ToString("#,##0");

                decimal total = Convert.ToDecimal(ds.Tables[1].Rows[0]["total visit"]) +
                    Convert.ToDecimal(ds.Tables[2].Rows[0]["total mevisit"]) ;
                sumAll_label.Text = total.ToString("#,##0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bind_NewVisits(string FundType, string Type, string SubType
            , string kind, string State, string Partners
             , string Donor, string DonorGroup
            , string Category, string SubCategory
            , string Age, string Sex, string MaritalStatus )
        {
            try
            {
                Program.buildConnection();

                var from = @" FROM mevisit v 
 left OUTER join `person` P on P.P_ID = v.Person_ID 
 left OUTER join `person_microproject` PMP on PMP.Person_ID = P.P_ID  
 left OUTER join `microproject` MP on MP.MP_ID = PMP.MicroProject_ID 
 left OUTER join `category` C on MP.MP_Category_ID = C.C_ID 
 LEFT OUTER join `subcategory` sub on MP.SubCategory_ID = sub.ID  
 LEFT OUTER join `state` on state.ID = MP.MP_State
 Left OUTER join `donor` on donor.ID = MP.MP_Donor
 Left OUTER join `loan` l on l.MicroProject_ID = MP.MP_ID
 Left OUTER join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID 
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID  ";

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
                 
                var condition = " where 1 ";
                if (kind != "")
                {
                    if (kind == "Vehicles") condition += " and Kind like 'v' ";
                    else condition += " and Kind like 'o' ";
                }
                 
                //Fund Type
                if (FundType != "")
                {
                    condition += " and fundtype.Name like '" + FundType + "'";
                }
                //Type
                if (Type != "")
                {
                    condition += " and microprojecttype.Name like '" + Type + "'";
                }
                //Sub Type
                if (SubType != "")
                {
                    condition += " and microprojectsubtype.Name like '" + SubType + "'";
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
                //Sub Category
                if (SubCategory != "")
                    condition += " and sub.Name like N'" + SubCategory + "'";

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
                  
                //Partners
                if (Partners != "")
                {
                    if (Partners.Contains("شراكة"))
                        condition += " and Partnership = 2 ";
                    else if (Partners.Contains("فردي")) condition += " and Partnership = 1 ";
                    else condition += "";
                }

                query += from + condition ;

                MySqlCommand sc = new MySqlCommand( query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataTable dt = new DataTable();
                da.Fill(dt);

                MP_dataGridView.ColumnHeadersVisible = false;
                MP_dataGridView.DataSource = dt; 
                MP_dataGridView.Columns["تاريخ الزيارة"].DefaultCellStyle.Format = "dd/MM/yyyy";
                MP_dataGridView.Columns["الربح الصافي التقريبي"].DefaultCellStyle.Format = "#,##0";

                var dgC1 = MP_dataGridView.Columns["V_ID"];
                dgC1.Visible = false;
                var dgC2 = MP_dataGridView.Columns["رقم المستفيد"];
                dgC2.Visible = false;
                var dgC3 = MP_dataGridView.Columns["Category_ID"];
                dgC3.Visible = false;

                //count rows
                var sel = "select count(*) " + from + condition;
                MySS.sc = new MySqlCommand(sel, Program.MyConn);
                decimal count = Convert.ToDecimal(MySS.sc.ExecuteScalar());
                Count_label.Text = "All:" + count.ToString("#,##0");
                Program.MyConn.Close();

                MP_dataGridView.Columns["رقم المشروع"].Width = 80;

                MP_dataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                    MP_dataGridView.Columns["الربح الصافي التقريبي"].DefaultCellStyle.Alignment
                        = MP_dataGridView.Columns["تاريخ الزيارة"].DefaultCellStyle.Alignment =
                            MP_dataGridView.Columns["النتيجة"].DefaultCellStyle.Alignment
                                = MP_dataGridView.Columns["رقم الزيارة"].DefaultCellStyle.Alignment =
                                    MP_dataGridView.Columns["نوع الزيارة"].DefaultCellStyle.Alignment
                                        = DataGridViewContentAlignment.MiddleCenter;
                MP_dataGridView.ColumnHeadersVisible = true;
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

                MySS.query = @"SELECT PMP.`MicroProject_ID` as 'رقم المشروع'
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

                
                var condition = " "; 
    
                MySS.query += from + condition;

                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                MySS.sc.ExecuteNonQuery();
                MySS.da = new MySqlDataAdapter(MySS.sc);
                MySS.dt = new DataTable();
                MySS.da.Fill(MySS.dt);

                MP_dataGridView.ColumnHeadersVisible = false;
                MP_dataGridView.DataSource = MySS.dt; 

                //////////////////////////////////////////////////////////////////
                var s = new StringBuilder();
                s.Append("select MP_ID from microproject where MP_ID in ");
                s.Append("(");
                s.Append("select PMP.MicroProject_ID as 'MicroProject_ID'" + from + condition);
                s.Append(")");
                //count rows
                var sel = "select count(DISTINCT MP_ID) " + from + condition;
                var sc = new MySqlCommand(sel, Program.MyConn);
                decimal count = Convert.ToDecimal(sc.ExecuteScalar());
                Count_label.Text = "All:" + count.ToString("#,##0");
                //////////////////////////////////////////////////////////////////

                MP_dataGridView.Columns["رقم المشروع"].Width = 80;

                MP_dataGridView.Columns["عدد آراء الأشخاص المفتاحيين"].DefaultCellStyle.Alignment  
                    = MP_dataGridView.Columns["عدد آراء الفريق"].DefaultCellStyle.Alignment
                    = MP_dataGridView.Columns["عدد آراء الزائرون"].DefaultCellStyle.Alignment
                    = MP_dataGridView.Columns["عدد آراء اللجنة"].DefaultCellStyle.Alignment 
                    = DataGridViewContentAlignment.MiddleCenter;

                MP_dataGridView.ColumnHeadersVisible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         //private void Category_bind()
        //{
        //    //check connection//
        //    Program.buildConnection(); 
        //    MySS.query = "select C_ID,C_Name from category ORDER BY C_Name ASC";
        //    var sc = new MySqlCommand(MySS.query, Program.MyConn);
        //    var da = new MySqlDataAdapter(MySS.query, Program.MyConn);
        //    var reader = sc.ExecuteReader();
        //    var dt = new DataTable();
        //    dt.Columns.Add("C_ID", typeof(string));
        //    dt.Columns.Add("C_Name", typeof(string));
        //    dt.Load(reader);
        //    MP_Category_comboBox.DisplayMember = "C_Name";
        //    MP_Category_comboBox.ValueMember = "C_ID";
        //    MP_Category_comboBox.DataSource = dt;
        //    //MP_Category_comboBox.Text = ""; 
        //    Program.MyConn.Close();
        //}
        private void Bind_All_ComboBoxes()
        {
            user_mode = false;

            string categroy_query = "SELECT C_ID as 'ID',C_Name as 'Name' from category ORDER BY C_Name ASC;";
            string sub_category_query = "SELECT ID, Name, Category_ID from `subcategory` ORDER BY Name ASC;";
            string street_query = "SELECT ID, Name from `street` order by Name ASC;";
            string donor_query = "SELECT ID, Name from `donor` ORDER BY Name ASC;";
            string donor_group_query = "SELECT ID, Name, Donor_ID from `donorgroup` ORDER BY Name ASC;";
            string fundType_query = "SELECT ID, Name from `fundtype` ORDER BY ID ASC;";
            string microprojecttype_query = "SELECT ID, Name from `microprojecttype` ORDER BY ID ASC;";
            string microprojectsubtype_query = "SELECT ID, Name,Type_ID from `microprojectsubtype` ORDER BY ID ASC;";

            string query = categroy_query + sub_category_query + street_query + donor_query + donor_group_query
                +fundType_query + microprojecttype_query + microprojectsubtype_query;
            Program.buildConnection();
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            ds = new DataSet();
            da.Fill(ds);
            Program.MyConn.Close();

            Set_comboBox_datasource(MP_Category_comboBox, ds.Tables[0]);
            Set_comboBox_datasource(SubCategory_comboBox, ds.Tables[1]);
            Set_comboBox_datasource(Street_comboBox, ds.Tables[2]);
            Set_comboBox_datasource(Donor_comboBox, ds.Tables[3]);
            Set_comboBox_datasource(DonorGroup_comboBox, ds.Tables[4]);
            Set_comboBox_datasource(FundType_comboBox, ds.Tables[5]);
            Set_comboBox_datasource(Type_comboBox, ds.Tables[6]);
            Set_comboBox_datasource(SubType_comboBox, ds.Tables[7]);


            ApplyDate_comboBox.Items.Clear();
            FundDate_comboBox.Items.Clear();
            for (int i = 2018; i <= DateTime.Today.Year; i++)
            {
                ApplyDate_comboBox.Items.Add(i.ToString());
                FundDate_comboBox.Items.Add(i.ToString());
            }

            user_mode = true;
        }

        private void Set_comboBox_datasource(ComboBox cBox, DataTable c_dt)
        {
            cBox.DataSource = null;
            cBox.DisplayMember = "Name";
            cBox.ValueMember = "ID";
            cBox.DataSource = c_dt;
            cBox.SelectedIndex = -1;
        }

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
                
            }
            SubCategory_comboBox.SelectedIndex = -1;
            user_mode = true; 
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
                rows = ds.Tables[7].Select("Type_ID=" + Type_ID);

                DataTable selected_rows_dt = new DataTable();
                selected_rows_dt = ds.Tables[7].Copy();
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
                SubType_comboBox.DataSource = ds.Tables[7];
                
            }
            SubType_comboBox.SelectedIndex = -1;
            user_mode = true;
        }
        private void DonorGroup_bind(string Donor_ID)
            {
            user_mode = false;
            DonorGroup_comboBox.DataSource = null;
            DonorGroup_comboBox.DisplayMember = "Name";
            DonorGroup_comboBox.ValueMember = "ID";

            if (Donor_ID != "")
            {
                DataRow[] rows = null;
                rows = ds.Tables[4].Select("Donor_ID=" + Donor_ID);

                DataTable selected_rows_dt = new DataTable();
                selected_rows_dt = ds.Tables[4].Copy();
                selected_rows_dt.Rows.Clear();
                foreach (DataRow row in rows)
                    selected_rows_dt.ImportRow(row);

                if (selected_rows_dt.Rows.Count != 0 || selected_rows_dt != null)
                    DonorGroup_comboBox.DataSource = selected_rows_dt;
                else
                {

                }
            }
            else
            {
                DonorGroup_comboBox.DataSource = ds.Tables[4];
                
            }
            DonorGroup_comboBox.SelectedIndex = -1;
            user_mode = true;
        }
        #endregion

        #region search and filter functions
        private void SearchBy_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Check_Visibility();
                MP_dataGridView.RowTemplate.Height = 28;

                // applications //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(0))
                {
                    if (show_images) MP_dataGridView.RowTemplate.Height = 80;

                    bind_Applications(FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text
                        , MP_Status_comboBox.Text, Partnership_comboBox.Text
                        , replaceQuotation(Donor_comboBox.Text), replaceQuotation(DonorGroup_comboBox.Text)
                        , MP_Category_comboBox.Text, SubCategory_comboBox.Text
                        , Street_comboBox.Text
                        , P_Parish_comboBox.Text, Age_comboBox.Text
                        , Gender_comboBox.Text, MaritalStatus_comboBox.Text 
                        , ApplyDate_comboBox.Text, FundDate_comboBox.Text, show_images);
                }
                // families //
                else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(1))
                {
                    FamilyMember_bind("");
                }
                // Loans //
                else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(2))
                {
                    Loan_bind(FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text
                        , MP_Status_comboBox.Text , Partnership_comboBox.Text 
                        ,replaceQuotation(Donor_comboBox.Text), replaceQuotation(DonorGroup_comboBox.Text)
                        , MP_Category_comboBox.Text , SubCategory_comboBox.Text
                        , ApplyDate_comboBox.Text, FundDate_comboBox.Text);

                }
                // Attachments //
                else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(3))
                {
                    if (show_images) MP_dataGridView.RowTemplate.Height = 80;

                    bind_Images(FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text, MP_Status_comboBox.Text
                        , replaceQuotation(Donor_comboBox.Text), replaceQuotation(DonorGroup_comboBox.Text)
                        , MP_Category_comboBox.Text, SubCategory_comboBox.Text
                        , show_images);
                }
                //Executive Files //
                else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(4))
                {
                    bind_ExeFile(FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text, MP_Status_comboBox.Text, Partnership_comboBox.Text
                        , replaceQuotation(Donor_comboBox.Text), replaceQuotation(DonorGroup_comboBox.Text)
                        , MP_Category_comboBox.Text, SubCategory_comboBox.Text  );
                }
                // Visits //
                else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(5))
                {
                    bind_Visits(FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text,""
                        , MP_Status_comboBox.Text, Partnership_comboBox.Text

                        , replaceQuotation(Donor_comboBox.Text), replaceQuotation(DonorGroup_comboBox.Text)
                        , MP_Category_comboBox.Text, SubCategory_comboBox.Text

                        , ApplyDate_comboBox.Text, FundDate_comboBox.Text); 
                }
                // New Visit Forms //
                else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(6))
                {
                    bind_NewVisits(FundType_comboBox.Text, Type_comboBox.Text, SubType_comboBox.Text, ""
                        , MP_Status_comboBox.Text, Partnership_comboBox.Text
                        
                        , replaceQuotation(Donor_comboBox.Text), replaceQuotation(DonorGroup_comboBox.Text)
                        , MP_Category_comboBox.Text, SubCategory_comboBox.Text

                        , Age_comboBox.Text, Gender_comboBox.Text, MaritalStatus_comboBox.Text);
                }
                // Check Lists //
                else if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(7))
                {
                    bind_CheckList();
                }
                
                // Use of the DataGridViewColumnSelector
                cs = new DataGridViewColumnSelector(MP_dataGridView);
                cs.MaxHeight = 100;
                cs.Width = 110;
                restore_visible_columns(SearchBy_comboBox.SelectedIndex);
                
                //SearchBy_textBox_TextChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Color_Cells()
        {
            try
            {
                //for (int i = 0; i < MP_dataGridView.RowCount; i++)
                //{
                //color
                //int late_num = Convert.ToInt32(MP_dataGridView.Rows[i].Cells["عدد تأخيرات المستفيد"].Value.ToString());
                //if (late_num > 3)
                //{
                //    MP_dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(23, 63, 95);
                //    MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                //}
                //else if (late_num > 0 && late_num <= 3)
                //{
                //    MP_dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(236, 214, 133);
                //    MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                //}
                //else if (late_num == 0)
                //{
                //    MP_dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                //    MP_dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                //}  
                //}
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
                if (user_mode)
                {
                    SearchBy_textBox_TextChanged(sender, e);
                    SearchBy_comboBox_SelectedIndexChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchBy_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchBy_textBox.Text))
                {
                    foreach (DataGridViewRow r in MP_dataGridView.Rows)
                        r.Visible = true;

                    Count_label.Text = "All:" + total_bind_count.ToString("#,##0"); 
                }
                else
                {
                    foreach (DataGridViewRow r in MP_dataGridView.Rows)
                    {
                        var found = 0;
                        for (var i = 0; i < MP_dataGridView.ColumnCount; i++)
                            if (ExactSearch_checkBox.Checked)
                            {
                                if (MP_dataGridView.Columns[i].Visible)
                                    if (r.Cells[i].Value.ToString().ToLower().Equals(SearchBy_textBox.Text.ToLower()))
                                    {
                                        MP_dataGridView.Rows[r.Index].Visible = true;
                                        found = 1;
                                    }
                            }
                            else
                            {
                                if (r.Cells[i].Value.ToString().ToLower().Contains(SearchBy_textBox.Text.ToLower()))
                                {
                                    MP_dataGridView.Rows[r.Index].Visible = true;
                                    found = 1;
                                }
                            }

                        if (found == 0)
                        {
                            MP_dataGridView.CurrentCell = null;
                            MP_dataGridView.Rows[r.Index].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ExactSearch_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SearchBy_textBox_TextChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void MP_Category_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //add the type_comboBox_selectedValue_changed to fill the data before binding subType  
            try
            {
                if (user_mode)
                    SubCategory_bind(MP_Category_comboBox.SelectedValue.ToString());
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
        private void Donor_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //add the type_comboBox_selectedValue_changed to fill the data before binding subType  
            try
            {
                if (user_mode)
                   DonorGroup_bind(Donor_comboBox.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region datagridview functions

        private void MP_dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // Applications //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(0))
                    if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
                    {
                        SelectedDataRow = ((DataRowView) MP_dataGridView.CurrentRow.DataBoundItem).Row;
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
                        main_form.showNewTab(application_Form, "Application",0);
                    }

                // families //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(1))
                {
                    SelectedDataRow = ((DataRowView) MP_dataGridView.CurrentRow.DataBoundItem).Row;
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
                    if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
                    {
                        SelectedDataRow = ((DataRowView) MP_dataGridView.CurrentRow.DataBoundItem).Row;
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
                        main_form.showNewTab(attachments_Form, "Attachments",0);
                    }

                //Executive Files
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(4))
                    if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
                    {
                        SelectedDataRow = ((DataRowView) MP_dataGridView.CurrentRow.DataBoundItem).Row;
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
                        main_form.showNewTab(ExecutiveFiles_Form, "Executive File",0);
                    }

                // Visits //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(5))
                    if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
                    {
                        SelectedDataRow = ((DataRowView) MP_dataGridView.CurrentRow.DataBoundItem).Row;
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
                    SelectedDataRow = ((DataRowView) MP_dataGridView.CurrentRow.DataBoundItem).Row;
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
                            main_form.showNewTab(Visit_V_Form, V_NAME_TO_SHOW,0);
                        }
                        else
                        {
                            Form Visit_O_Form = new V_ME_Other_Form(V_ID, main_form);
                            main_form.showNewTab(Visit_O_Form, V_NAME_TO_SHOW,0);
                        }
                    }
                }

                // Checklists //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(7))
                    if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
                    {
                        SelectedDataRow = ((DataRowView)MP_dataGridView.CurrentRow.DataBoundItem).Row;
                        if (SelectedDataRow != null)
                        {
                            if (SelectedDataRow["رقم المشروع"] == null ||
                                SelectedDataRow["رقم المشروع"] == DBNull.Value)
                                MicroProject_ID = -1;
                            else MicroProject_ID = int.Parse(SelectedDataRow["رقم المشروع"].ToString()); 
                        }

                        Form  Form = new CheckList_Form(MicroProject_ID, main_form);
                        main_form.showNewTab(Form, "Check List", 0);
                    }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MP_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var SelectedRowCount = MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
                Selected_label.Text = "Selected: " + SelectedRowCount;
                // Loans //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(2))
                {
                    if (SelectedRowCount >= 1)
                    {
                        IDs_array = new int[SelectedRowCount];
                        Installment_IDs_array = new int[SelectedRowCount];

                        for (var i = 0; i < SelectedRowCount; i++)
                        {
                            LoanSelectedDataRow = ((DataRowView) MP_dataGridView.SelectedRows[i].DataBoundItem).Row;
                            IDs_array[i] = int.Parse(LoanSelectedDataRow["ID"].ToString());
                        }

                        var str = "(" + string.Join(", ",
                            Array.ConvertAll(IDs_array, v => v.ToString(CultureInfo.InvariantCulture))) + ")";
                        
                        calculate_ImportExportMoney(str);
                    }
                    else
                    {
                        calculate_ImportExportMoney("");
                    }
                }

                // Families //
                if (SearchBy_comboBox.SelectedItem.ToString() == searchBy_list.ElementAt<string>(1))
                    if (SelectedRowCount >= 1)
                    {
                        PIDs_array = new int[SelectedRowCount];
                        P_Names_array = new string[SelectedRowCount];

                        for (var i = 0; i < SelectedRowCount; i++)
                        {
                            PersonSelectedDataRow = ((DataRowView) MP_dataGridView.SelectedRows[i].DataBoundItem).Row;
                            PIDs_array[i] = int.Parse(PersonSelectedDataRow["ID"].ToString());
                            P_Names_array[i] = PersonSelectedDataRow["المستفيد"].ToString();
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region button clicks

        private void Delete_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Settings.Default.role != 1 && Settings.Default.role != 8) throw new Exception("You don't have permession for this action.");
                var dialogResult = MessageBox.Show("Are you sure you want to delete ??", "Delete",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //check connection//
                    Program.buildConnection();
                    var str = "(" + string.Join(", ",
                        Array.ConvertAll(PIDs_array, v => v.ToString(CultureInfo.InvariantCulture))) + ")";
                    var name_str = string.Join(", ",
                        Array.ConvertAll(P_Names_array, v => v.ToString(CultureInfo.InvariantCulture)));

                    var query = "DELETE FROM `person` WHERE `P_ID` in" + str;
                    var sc = new MySqlCommand(query, Program.MyConn);
                    sc.ExecuteNonQuery();
                    Program.MyConn.Close();
                    l.Insert_Log("Delete from Persons: " + name_str, "Person", Settings.Default.username, DateTime.Now);
                    FamilyMember_bind("");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportToExcel_button_Click(object sender, EventArgs e)
        {
            try
            {
                //if Guest or Out of service or lawful//
                if (Settings.Default.role == 4 ||
                    Settings.Default.role == 6 ||
                    Settings.Default.role == 7)
                    throw new Exception("Sorry ! You Don't have the permission for this action.");
                 
                Thread myTh = new Thread(SaveCallDialog);
                myTh.SetApartmentState(ApartmentState.STA);
                myTh.Start();
                myTh.Join(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
          
        private void SaveCallDialog()
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save Excel File";
                saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.ValidateNames = true;

                var res = saveFileDialog1.ShowDialog();
                if (res == DialogResult.OK && saveFileDialog1.FileName != "")
                {
                    string filename = filename = saveFileDialog1.FileName;
                    XLWorkbook wb = new XLWorkbook { RightToLeft = true };
                    //defaultView gets the visible rows only//
                    DataTable ex_dt = ((DataTable)MP_dataGridView.DataSource).DefaultView.ToTable();
                     
                    //Remove invisible columns//
                    for (var j = MP_dataGridView.ColumnCount-1; j >=0; j--)
                        if (MP_dataGridView.Columns[j].Visible == false)
                            ex_dt.Columns.RemoveAt(j);

                    wb.Worksheets.Add(ex_dt, "Exported from App");
                    
                    wb.SaveAs(filename);
                    wb.Dispose();

                    Process.Start(filename);
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
        private void show_hide_button_Click(object sender, EventArgs e)
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
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (partners)
        //    {
        //        partners = false;
        //        button2.BackgroundImage = Properties.Resources.group_grey;


        //        /////// select where NOOOOO partners //////
        //    }
        //    else
        //    {
        //        partners = true;
        //        button2.BackgroundImage = Properties.Resources.group_red;


        //        /////// select where partners //////
        //    }
        //}

        #endregion

        #region mouse hover

        private void ExportToExcel_button_MouseEnter(object sender, EventArgs e)
        {
            ExportToExcel_button.BackgroundImage = Resources.Excel_L;
        }

        private void ExportToExcel_button_MouseLeave(object sender, EventArgs e)
        {
            ExportToExcel_button.BackgroundImage = Resources.Excel_D;
        }

        private void Refresh_button_MouseEnter(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                Refresh_button.BackgroundImage = Resources.Refresh2_L;
            else Refresh_button.BackgroundImage = Resources.Refresh2_D;
        }

        private void Refresh_button_MouseLeave(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                Refresh_button.BackgroundImage = Resources.Refresh2_D;
            else Refresh_button.BackgroundImage = Resources.Refresh2_L;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            if (Hided)
            {
                if (Settings.Default.theme == "Light")
                    ShowHide_button.BackgroundImage = Resources.Show2_D;
                else ShowHide_button.BackgroundImage = Resources.Show2_L;
            }
            else
            {
                if (Settings.Default.theme == "Light")
                    ShowHide_button.BackgroundImage = Resources.Hide2_D;
                else ShowHide_button.BackgroundImage = Resources.Hide2_L;
            }
        }
         
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            if (Hided)
            {
                if (Settings.Default.theme == "Light")
                    ShowHide_button.BackgroundImage = Resources.Show2_L;
                else ShowHide_button.BackgroundImage = Resources.Show2_D;
            }
            else
            {
                if (Settings.Default.theme == "Light")
                    ShowHide_button.BackgroundImage = Resources.Hide2_L;
                else ShowHide_button.BackgroundImage = Resources.Hide2_D;
            }
        }
          
        #endregion
          
        #region right click menu 
        private void refreshPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_button_Click(sender, e);
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Search_Form = new Search_Form(main_form);
                main_form.showNewTab(Search_Form, "Search",0);
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
                main_form.showNewTab(Statistics_Form, "Statistics",0);
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
                main_form.showNewTab(projectsTasks, "Task Board",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showHideFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            show_hide_button_Click(sender, e);
        }
         
        private void clearAllFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Visits_comboBox.Text 
            SubCategory_comboBox.Text = Street_comboBox.Text = Partnership_comboBox.Text=
            FundType_comboBox.Text= Type_comboBox.Text= SubType_comboBox.Text 
            = MP_Status_comboBox.Text = Donor_comboBox.Text = DonorGroup_comboBox.Text = MP_Category_comboBox.Text
            = P_Parish_comboBox.Text = Age_comboBox.Text = Gender_comboBox.Text = MaritalStatus_comboBox.Text
                    = SubType_comboBox.Text 
                    = ApplyDate_comboBox.Text = FundDate_comboBox.Text = "";
            RequestedFrom_textBox.Text = RequestedTo_textBox.Text = FundedFrom_textBox.Text = FundedTo_textBox.Text = "";
             
            SearchBy_textBox.Text = "";
            Refresh_button_Click(sender, e);
        }
        #endregion
         
        #region save visible arrays for each tab in search

        private int[] arr;

        private void save_visible_columns(int index)
        {
            arr = new int[MP_dataGridView.ColumnCount];
            for (var j = 0; j < MP_dataGridView.ColumnCount; j++)
                if (MP_dataGridView.Columns[j].Visible)
                    arr[j] = 1;
                else arr[j] = 0;
            var value = string.Join(",", arr.Select(i => i.ToString()).ToArray());

            if (index == 0) Settings.Default.Search0_Visible_arr = value;
            else if (index == 1) Settings.Default.Search1_Visible_arr = value;
            else if (index == 2) Settings.Default.Search2_Visible_arr = value;
            else if (index == 3) Settings.Default.Search3_Visible_arr = value;
            else if (index == 4) Settings.Default.Search4_Visible_arr = value;
            else if (index == 5) Settings.Default.Search5_Visible_arr = value;
            else if (index == 6) Settings.Default.Search6_Visible_arr = value;
            else if (index == 7) Settings.Default.Search7_Visible_arr = value;

            Settings.Default.Save();
        }

        private void restore_visible_columns(int index)
        {
            try
            {
                if (index == 0)
                    arr = Settings.Default.Search0_Visible_arr.Split(',').Select(s => int.Parse(s)).ToArray();
                else if (index == 1)
                    arr = Settings.Default.Search1_Visible_arr.Split(',').Select(s => int.Parse(s)).ToArray();
                else if (index == 2)
                    arr = Settings.Default.Search2_Visible_arr.Split(',').Select(s => int.Parse(s)).ToArray();
                else if (index == 3)
                    arr = Settings.Default.Search3_Visible_arr.Split(',').Select(s => int.Parse(s)).ToArray();
                else if (index == 4)
                    arr = Settings.Default.Search4_Visible_arr.Split(',').Select(s => int.Parse(s)).ToArray();
                else if (index == 5)
                    arr = Settings.Default.Search5_Visible_arr.Split(',').Select(s => int.Parse(s)).ToArray();
                else if (index == 6)
                    arr = Settings.Default.Search6_Visible_arr.Split(',').Select(s => int.Parse(s)).ToArray();
                else if (index == 7)
                    arr = Settings.Default.Search7_Visible_arr.Split(',').Select(s => int.Parse(s)).ToArray();

                for (var i = 0; i < MP_dataGridView.ColumnCount; i++)
                    if (arr[i] == 1) MP_dataGridView.Columns[i].Visible = true;
                    else MP_dataGridView.Columns[i].Visible = false;
              
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                save_visible_columns(index);
            }
        }

        #endregion


          
    }
}