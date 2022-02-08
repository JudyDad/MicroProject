using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;
using Application = Microsoft.Office.Interop.Excel.Application;
using Button = System.Windows.Forms.Button;
using DataTable = System.Data.DataTable;

namespace MyWorkApplication
{
    public partial class Payments_Form : Form
    {
        private Log l; 
        private readonly MainForm main_form;
        private NewTheme NewTheme;
        private UserNotification noti;
        private Select s;
        private bool user_mode;

        public Payments_Form(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void Payments_Form_Load(object sender, EventArgs e)
        {
            try
            {
                NewTheme = new NewTheme();
                l = new Log();
                s = new Select();
                noti = new UserNotification();

                if (Settings.Default.theme == "Light") NewTheme.Search_ToLight(this, false);
                else NewTheme.Search_ToNight(this, false);

                //Default view pending//
                Pending_button.BackColor = Color.FromArgb(104, 157, 202);
                Paid_button.BackColor = Color.Transparent;
                ////////////////////////

                user_mode = false;
                Year_comboBox.Items.Clear();
                for (int i = 2018; i <= DateTime.Today.Year + 2; i++)
                {
                    Year_comboBox.Items.Add(i.ToString());
                }
                //Default view This Year//
                Year_comboBox.SelectedItem = DateTime.Now.Year.ToString();
                //////////////////////////

                Group_bind();

                bind_PendingPayments("", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                user_mode = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bind_PendingPayments(string month, string year, string Group)
        {
            var paid = "Not Paid";
            if (Paid_button.BackColor == Color.FromArgb(104, 157, 202)) paid = "Paid";
            else paid = "Not Paid";

            var dt = s.Payments_bind(month, year, Group, paid);
            dataGridView.ColumnHeadersVisible = false;
            dataGridView.DataSource = null;
            dataGridView.Columns.Clear();
            dataGridView.DataSource = dt;
            dataGridView.Columns["تاريخ الاستحقاق"].DefaultCellStyle.Format =
                dataGridView.Columns["تاريخ الدفع الفعلي"].DefaultCellStyle.Format = "yyyy/MM/dd";
            dataGridView.Columns["القسط"].DefaultCellStyle.Format = "#,##0";

            DataGridViewColumn dgC1;
            if (dataGridView.RowCount > 0 && dataGridView.CurrentRow != null)
            {
                dgC1 = dataGridView.Columns["Pay_ID"];
                dgC1.Visible = false;
            }

            dataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                dataGridView.Columns["تاريخ الاستحقاق"].DefaultCellStyle.Alignment =
                    dataGridView.Columns["تاريخ الدفع الفعلي"].DefaultCellStyle.Alignment = 
                        dataGridView.Columns["المجموعة"].DefaultCellStyle.Alignment =
                            dataGridView.Columns["القسط"].DefaultCellStyle.Alignment =
                                DataGridViewContentAlignment.MiddleCenter;

            //count rows
            Count_label.Text = "All: " + dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible);

            var sum = 0;
            if (dt.Rows.Count > 0) sum = Convert.ToInt32(dt.Compute("SUM(القسط)", string.Empty));

            Sum_label.Text = "Sum: " + sum.ToString("#,##0");
            dataGridView.ColumnHeadersVisible = true;
        }

        private void SearchBy_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in dataGridView.Rows)
                {
                    var found = 0;
                    for (var i = 0; i < dataGridView.ColumnCount; i++)
                    {
                        if (r.Cells[i].Value == null) continue;

                        if (ExactSearch_checkBox.Checked)
                        {
                            if (dataGridView.Columns[i].Visible)
                                if (r.Cells[i].Value.ToString().ToLower().Equals(SearchBy_textBox.Text.ToLower()))
                                {
                                    dataGridView.Rows[r.Index].Visible = true;
                                    found = 1;
                                }
                        }
                        else
                        {
                            if (r.Cells[i].Value.ToString().ToLower().Contains(SearchBy_textBox.Text.ToLower()))
                            {
                                dataGridView.Rows[r.Index].Visible = true;
                                found = 1;
                            }
                        }
                    }

                    if (found == 0)
                    {
                        dataGridView.CurrentCell = null;
                        dataGridView.Rows[r.Index].Visible = false;
                    }
                }

                //count rows
                Count_label.Text = "All: " + dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible);
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

        private void Year_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(user_mode)
                Refresh_button_Click(sender, e);
        }

        private void Refresh_button_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        btn.BackColor = Color.Transparent;
                    }

                SearchBy_textBox_TextChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var SelectedRowCount = dataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
                Selected_label.Text = "Selected: " + SelectedRowCount;

                if (SelectedRowCount >= 1)
                {
                    var sum = 0;

                    //IDs_array = new int[SelectedRowCount];
                    for (var i = 0; i < SelectedRowCount; i++)
                    {
                        var SelectedDataRow = ((DataRowView) dataGridView.SelectedRows[i].DataBoundItem).Row;
                        sum += int.Parse(SelectedDataRow["القسط"].ToString());
                    }

                    Sum_label.Text = "Sum: " + sum.ToString("#,##0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Group_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (user_mode)
            {
                var month = "";
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.BackColor == Color.FromArgb(104, 157, 202)) month = btn.Name.Substring(6);
                    }

                bind_PendingPayments(month, (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
            }
        }

        private void Paid_button_Click(object sender, EventArgs e)
        {
            var month = "";
            foreach (Control c in panel1.Controls)
                if (c.GetType() == typeof(Button))
                {
                    var btn = c as Button;
                    if (btn.BackColor == Color.FromArgb(104, 157, 202)) month = btn.Name.Substring(6);
                }

            Paid_button.BackColor = Color.FromArgb(104, 157, 202);
            Pending_button.BackColor = Color.Transparent;

            bind_PendingPayments(month, (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
            SearchBy_textBox_TextChanged(sender, e);
        }

        private void Pending_button_Click(object sender, EventArgs e)
        {
            var month = "";
            foreach (Control c in panel1.Controls)
                if (c.GetType() == typeof(Button))
                {
                    var btn = c as Button;
                    if (btn.BackColor == Color.FromArgb(104, 157, 202)) month = btn.Name.Substring(6);
                }

            Pending_button.BackColor = Color.FromArgb(104, 157, 202);
            Paid_button.BackColor = Color.Transparent;

            bind_PendingPayments(month, (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
            SearchBy_textBox_TextChanged(sender, e);
        }

        private void Group_bind()
        {
            var donorGroup = new DonorGroup();
            var dt_Group = donorGroup.Select("");
            Group_comboBox.DisplayMember = "Name";
            Group_comboBox.ValueMember = "ID";
            Group_comboBox.DataSource = dt_Group;
            Group_comboBox.SelectedIndex = -1;
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

                //_Application app = new Application();
                //// creating new WorkBook within Excel application  
                //_Workbook workbook = app.Workbooks.Add(Type.Missing);
                //// creating new Excelsheet in workbook  
                //_Worksheet worksheet = null;
                //// see the excel sheet behind the program  
                //app.Visible = true;
                //// get the reference of first sheet. By default its name is Sheet1.  
                //// store its reference to worksheet  


                //worksheet = workbook.Sheets[1];
                //worksheet = workbook.ActiveSheet;
                //// changing the name of active sheet  
                //worksheet.Name = "Exported from App";

                //// storing header part in Excel  
                //for (var i = 1; i < MP_dataGridView.Columns.Count + 1; i++)
                //    worksheet.Cells[1, i] = MP_dataGridView.Columns[i - 1].HeaderText;
                //// storing Each row and column value to excel sheet  
                //for (var i = 0; i < MP_dataGridView.Rows.Count; i++)
                //for (var j = 0; j < MP_dataGridView.Columns.Count; j++)
                //    if (MP_dataGridView.Rows[i].Visible)
                //        worksheet.Cells[i + 2, j + 1] = MP_dataGridView.Rows[i].Cells[j].Value.ToString();
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
                    DataTable ex_dt = ((DataTable)dataGridView.DataSource).DefaultView.ToTable();

                    //Remove invisible columns//
                    for (var j = dataGridView.ColumnCount - 1; j >= 0; j--)
                        if (dataGridView.Columns[j].Visible == false)
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
        private void dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dataGridView.RowCount > 0 && dataGridView.CurrentRow != null)
                {
                    var SelectedDataRow = ((DataRowView) dataGridView.CurrentRow.DataBoundItem).Row;
                    var MicroProject_ID = -1;

                    if (SelectedDataRow != null)
                    {
                        if (SelectedDataRow["رقم المشروع"] == null || SelectedDataRow["رقم المشروع"] == DBNull.Value)
                            MicroProject_ID = -1;
                        else MicroProject_ID = int.Parse(SelectedDataRow["رقم المشروع"].ToString());
                    }

                    Form Loan_Form = new Loans_Form(MicroProject_ID, main_form);
                    main_form.showNewTab(Loan_Form, "Loans & Payments",0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region months button clicks

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("01", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button1") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("02", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button2") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("03", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button3") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("04", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button4") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("05", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button5") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("06", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button6") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("07", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button7") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("08", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button8") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("09", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button9") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("10", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button10") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("11", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button11") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                bind_PendingPayments("12", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
                SearchBy_textBox_TextChanged(sender, e);
                foreach (Control c in panel1.Controls)
                    if (c.GetType() == typeof(Button))
                    {
                        var btn = c as Button;
                        if (btn.Name == "button12") btn.BackColor = Color.FromArgb(104, 157, 202);
                        else btn.BackColor = Color.Transparent;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void Year_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Year_checkBox.Checked)
                Year_comboBox.Enabled = true;
            else Year_comboBox.Enabled = false;

            bind_PendingPayments("", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), Group_comboBox.Text.ToLower());
            SearchBy_textBox_TextChanged(sender, e);
        }
    }
}