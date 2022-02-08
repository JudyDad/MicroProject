using MyWorkApplication.Properties;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using MyWorkApplication.Classes;
using System.Drawing;
using MySql.Data.MySqlClient;

namespace MyWorkApplication.Visit_Forms
{
    public partial class Monitoring_Visit_Results_Form : Form
    {
        private M_and_E me;
        SubCategory sub;
        private MainForm main_form;

        public Monitoring_Visit_Results_Form(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void Monitoring_Visit_Results_Form_Load(object sender, EventArgs e)
        {
            try
            {
                Check_Theme();

                me = new M_and_E();
                sub = new SubCategory();

                Category_bind();

                Answers_dataGridView.DoubleBuffered(true);
                Benficiaries_dataGridView.DoubleBuffered(true);

                DateFrom = DateFrom_dateTimePicker.Value.ToString("yyyy/MM/dd");
                DateTo = DateTo_dateTimePicker.Value.ToString("yyyy/MM/dd");

                AllV_button.BackColor = Color.FromArgb(104, 157, 202);
                FirstV_button.BackColor = LastV_button.BackColor = Color.Transparent;
                beneficiary = "all";

                Visits_comboBox.SelectedIndex = 0; //all
                VisitNumber_comboBox.SelectedIndex = 0; //all
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Check_Theme()
        {
            var newTheme = new NewTheme();
            if (Settings.Default.theme == "Dark")
                newTheme.Visit_ToNight(this);
            else
                newTheme.Visit_ToLight(this);
        }

        private void Category_bind()
        {
            DataTable cat_st = sub.Category_Select();
            Category_comboBox.DataSource = null;
            Category_comboBox.DisplayMember = "C_Name";
            Category_comboBox.ValueMember = "C_ID";
            Category_comboBox.DataSource = cat_st;
            Category_comboBox.SelectedIndex = -1;
        }
        private void SubCategory_bind(string Category_ID)
        {
            try
            {
                DataTable sub_st = sub.Select(Category_ID, "");
                SubCategory_comboBox.DataSource = null;
                SubCategory_comboBox.DisplayMember = "Name";
                SubCategory_comboBox.ValueMember = "ID";
                SubCategory_comboBox.DataSource = sub_st;
                SubCategory_comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Category_comboBox_TextChanged(object sender, EventArgs e)
        {
            if(Category_comboBox.Text != "")
                SubCategory_bind(Category_comboBox.SelectedValue.ToString()); 
        }

        private void Fill_Answers(string Visit_Type, string visit_Num, string DateFrom, string DateTo, string beneficiary, string category, string subCategory)
        {
            try { 
            Answers_dataGridView.Rows.Clear();

            var dt = me.Get_Questions_Answers_SideBySide(Visit_Type, visit_Num, DateFrom, DateTo, beneficiary,Category_comboBox.Text,SubCategory_comboBox.Text); 

            if (dt != null || dt.Rows.Count > 0)
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    Answers_dataGridView.Rows.Add();

                    var Q_ID = dt.Rows[i].Field<int>("ID");
                    Answers_dataGridView.Rows[i].Cells["Q_ID"].Value = Q_ID;

                    Answers_dataGridView.Rows[i].Cells["Q_Name"].Value = dt.Rows[i].Field<string>("Name");

                    Answers_dataGridView.Rows[i].Cells["Ans1"].Value = dt.Rows[i].Field<string>("ans1");
                    Answers_dataGridView.Rows[i].Cells["Ans2"].Value = dt.Rows[i].Field<string>("ans3");
                    Answers_dataGridView.Rows[i].Cells["Ans3"].Value = dt.Rows[i].Field<string>("ans2");
                    /// put ans3 in ans2 column لحتى نعبي الفراغ
                    Answers_dataGridView.Rows[i].Cells["Ans1_ID"].Value = dt.Rows[i].Field<int>("ans1_ID").ToString();
                    Answers_dataGridView.Rows[i].Cells["Ans2_ID"].Value = dt.Rows[i].Field<int>("ans3_ID").ToString();
                    Answers_dataGridView.Rows[i].Cells["Ans3_ID"].Value = dt.Rows[i].Field<string>("ans2_ID");

                    Answers_dataGridView.Rows[i].Cells["Ans1_count"].Value = dt.Rows[i].Field<long>("ans1_count").ToString();
                    Answers_dataGridView.Rows[i].Cells["Ans2_count"].Value = dt.Rows[i].Field<long>("ans3_count").ToString();

                    Answers_dataGridView.Rows[i].Cells["Ans3_count"].Value
                                                = (dt.Rows[i].Field<long>("ans2_count").ToString() == "0" ? "" : dt.Rows[i].Field<long>("ans2_count").ToString());
                        
                    Answers_dataGridView.Rows[i].Cells["all_ans_count"].Value =
                        dt.Rows[i].Field<long>("all_ans_count").ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Answers_dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void Answers_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedCellCount = Answers_dataGridView.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    var sb = new StringBuilder();

                    for (var i = 0; i < selectedCellCount; i++)
                    {
                        var selected_row = Answers_dataGridView.SelectedCells[i].RowIndex;
                        var selected_column = Answers_dataGridView.SelectedCells[i].ColumnIndex;

                        if (selected_column != 2 && selected_column != 5 && selected_column != 8) continue;

                        //if answer 2 is empty//
                        if (Answers_dataGridView.SelectedCells[i].Value == DBNull.Value
                            || Answers_dataGridView.SelectedCells[i].Value.ToString() == "") continue;

                        sb.Append("Answer_ID = ");
                        sb.Append(Answers_dataGridView.Rows[selected_row].Cells[selected_column + 2].Value);
                        sb.Append(" AND ");
                    }

                    if (sb.ToString() != "")
                    {
                        sb.Remove(sb.Length - 4, 4); // to remove last 'AND ' //
                        
                        //Add Same Conditions to beneficiaries..//
                        var dt2 = me.Get_Beneficaies(sb.ToString(),visitNum, DateFrom, DateTo, beneficiary, Category_comboBox.Text, SubCategory_comboBox.Text);

                        Benficiaries_dataGridView.DataSource = dt2;
                        Count_label.Text = dt2.Rows.Count.ToString();

                        Benficiaries_dataGridView.Columns["رقم الزيارة"].Width = 50;
                        Benficiaries_dataGridView.Columns["رقم المشروع"].Width = 80;
                    }
                    else
                    {
                        Benficiaries_dataGridView.DataSource = null;
                        Count_label.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
        private void SearchBy_textBox_Enter(object sender, EventArgs e)
        {
            if (SearchBy_textBox.Text == "بحث...") SearchBy_textBox.Text = "";
        }

        private void SearchBy_textBox_Leave(object sender, EventArgs e)
        {
            if (SearchBy_textBox.Text == "") SearchBy_textBox.Text = "بحث...";
        }

        private void SearchBy_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = (SearchBy_textBox.Text == "بحث..." ? "" : SearchBy_textBox.Text);

                foreach (DataGridViewRow r in Benficiaries_dataGridView.Rows)
                {
                    var found = 0;
                    for (var i = 0; i < Benficiaries_dataGridView.ColumnCount; i++)
                    {
                        if (search == "")
                        {
                            Benficiaries_dataGridView.Rows[r.Index].Visible = true;
                            found = 1; 
                        }
                        if(search != "" && r.Cells[i].Value.ToString().ToLower().Contains(search.ToLower()))
                        {
                            Benficiaries_dataGridView.Rows[r.Index].Visible = true;
                            found = 1;
                        }
                    } 
                    if (found == 0)
                    {
                        Benficiaries_dataGridView.CurrentCell = null;
                        Benficiaries_dataGridView.Rows[r.Index].Visible = false;
                    }
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
                //CHECK PERMISSION//

                _Application app = new Application();
                // creating new WorkBook within Excel application  
                _Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                _Worksheet worksheet = app.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                // see the excel sheet behind the program  
                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.
                // store its reference to worksheet  
                worksheet = workbook.Sheets[1];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "M&E Visit Answers";

                // storing header part in Excel  
                for (var i = 1; i < Answers_dataGridView.Columns.Count + 1; i++)
                    worksheet.Cells[1, i] = Answers_dataGridView.Columns[i - 1].HeaderText;
                // storing Each row and column value to excel sheet  
                for (var i = 0; i < Answers_dataGridView.Rows.Count-1; i++)
                    for (var j = 0; j < Answers_dataGridView.Columns.Count; j++)
                        if (Answers_dataGridView.Rows[i].Visible)
                            worksheet.Cells[i + 2, j + 1] = Answers_dataGridView.Rows[i].Cells[j].Value.ToString();

                ////////////////////////////////////////////
                ////////////////////////////////////////////
                ////////////////////////////////////////////
                _Worksheet worksheet2 = worksheet2 = workbook.Sheets[2];

                // changing the name of active sheet  
                worksheet2.Name = "Beneficiaries";

                // storing header part in Excel  
                for (var i = 1; i < Benficiaries_dataGridView.Columns.Count + 1; i++)
                    worksheet2.Cells[1, i] = Benficiaries_dataGridView.Columns[i - 1].HeaderText;
                // storing Each row and column value to excel sheet  
                for (var i = 0; i < Benficiaries_dataGridView.Rows.Count; i++)
                    for (var j = 0; j < Benficiaries_dataGridView.Columns.Count; j++)
                        if (Benficiaries_dataGridView.Rows[i].Visible)
                            worksheet2.Cells[i + 2, j + 1] = Benficiaries_dataGridView.Rows[i].Cells[j].Value.ToString();
                

                //workbook.Worksheets.Add(worksheet);
                //workbook.Worksheets.Add(worksheet2);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string visitType = "";
        string visitNum = "";
        string beneficiary = "";
        string DateFrom = "", DateTo = "";

        private void Visits_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                if (Visits_comboBox.SelectedIndex != -1)
                {
                    if (Visits_comboBox.SelectedItem.ToString() == "مركبات")
                        visitType = "v";
                    else if (Visits_comboBox.SelectedItem.ToString() == "مشاريع أخرى")
                        visitType = "o";
                    else visitType = "";
                }
                if (VisitNumber_comboBox.SelectedIndex == -1 || VisitNumber_comboBox.SelectedIndex == 0)
                    visitNum = "";
                else visitNum = VisitNumber_comboBox.SelectedItem.ToString();

                Fill_Answers(visitType, visitNum, DateFrom, DateTo, beneficiary, Category_comboBox.Text, SubCategory_comboBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void AllV_button_Click(object sender, EventArgs e)
        {
            AllV_button.BackColor = Color.FromArgb(104, 157, 202); 
            FirstV_button.BackColor = LastV_button.BackColor = Color.Transparent; 
            beneficiary = "all";

            Fill_Answers(visitType, visitNum, DateFrom, DateTo, beneficiary, Category_comboBox.Text, SubCategory_comboBox.Text);
        }

        private void FirstV_button_Click(object sender, EventArgs e)
        {
            FirstV_button.BackColor = Color.FromArgb(104, 157, 202);
            AllV_button.BackColor = LastV_button.BackColor = Color.Transparent;

            beneficiary = "first";

            Fill_Answers(visitType, visitNum, DateFrom, DateTo, beneficiary, Category_comboBox.Text, SubCategory_comboBox.Text);
        }

        private void LastV_button_Click(object sender, EventArgs e)
        {
            LastV_button.BackColor = Color.FromArgb(104, 157, 202);
            AllV_button.BackColor = FirstV_button.BackColor = Color.Transparent;

            beneficiary = "last";

            Fill_Answers(visitType, visitNum, DateFrom, DateTo, beneficiary, Category_comboBox.Text, SubCategory_comboBox.Text);
        }

        private void SubCategory_comboBox_TextChanged(object sender, EventArgs e)
        {
            Fill_Answers(visitType, visitNum, DateFrom, DateTo, beneficiary, Category_comboBox.Text, SubCategory_comboBox.Text);
        }

        private void DateFrom_bcDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateFrom = DateFrom_dateTimePicker.Value.ToString("yyyy/MM/dd");
            DateTo = DateTo_dateTimePicker.Value.ToString("yyyy/MM/dd"); 
            Fill_Answers(visitType, visitNum, DateFrom, DateTo, beneficiary, Category_comboBox.Text, SubCategory_comboBox.Text);
        }
    }
}
