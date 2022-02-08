using System;
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
using Point = System.Drawing.Point;
using DataTable = System.Data.DataTable;

namespace MyWorkApplication
{
    public partial class VisitNotifications_Form : Form
    {
        private Log l;
        private Street st;
        private MainForm main_form;
        private NewTheme NewTheme;
        private UserNotification noti;
        private Select s;
        private bool user_mode;
        string V_number = "", V_Kind = ""; 
        string Street = "";
        string Seen = "0";

        public VisitNotifications_Form(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void VisitNotifications_Form_Load(object sender, EventArgs e)
        {
            try
            {
                NewTheme = new NewTheme();
                l = new Log();
                s = new Select();
                st = new Street();
                noti = new UserNotification();
                if (Settings.Default.theme == "Light") NewTheme.Search_ToLight(this, false);
                else NewTheme.Search_ToNight(this, false);

                user_mode = false;
                Year_comboBox.Items.Clear(); 
                for (int i = 2018; i <= DateTime.Today.Year + 2; i++)
                {
                    Year_comboBox.Items.Add(i.ToString()); 
                }
                Street_bind();
                Year_comboBox.SelectedItem = DateTime.Now.Year.ToString();
                 
                bind_VisitNotifications("", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
                user_mode = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Street_bind()
        {
            DataTable st_st = st.Select("");
            Street_comboBox.DataSource = null;
            Street_comboBox.DisplayMember = "Name";
            Street_comboBox.ValueMember = "ID";
            Street_comboBox.DataSource = st_st;
            Street_comboBox.SelectedIndex = -1;
        }

        private void bind_VisitNotifications(string month, string year, string V_Kind ,string V_number, string Street, string Seen)
        {
            var dt = noti.Select_VisitNotifications(month, year, V_Kind, V_number, Street, Seen);
            MP_dataGridView.ColumnHeadersVisible = false;
            MP_dataGridView.DataSource = null;
            MP_dataGridView.Columns.Clear();
            MP_dataGridView.DataSource = dt;
            MP_dataGridView.Columns["التاريخ"].DefaultCellStyle.Format = "yyyy/MM/dd";

            DataGridViewColumn dgC1;
            if (MP_dataGridView.RowCount > 0 && MP_dataGridView.CurrentRow != null)
            {
                dgC1 = MP_dataGridView.Columns["ID"];
                dgC1.Visible = false;
            }

            MP_dataGridView.Columns["رقم المشروع"].DefaultCellStyle.Alignment =
                MP_dataGridView.Columns["التاريخ"].DefaultCellStyle.Alignment = 
                MP_dataGridView.Columns["نوع الزيارة"].DefaultCellStyle.Alignment =
                MP_dataGridView.Columns["منطقة المشروع"].DefaultCellStyle.Alignment =
                MP_dataGridView.Columns["الموبايل"].DefaultCellStyle.Alignment
                = DataGridViewContentAlignment.MiddleCenter;

            //count rows
            Count_label.Text = "All: " + MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible);

            var colCB = new DataGridViewButtonColumn();
            colCB.Name = "DeleteRow";
            colCB.HeaderText = "";
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCB.FlatStyle = FlatStyle.Flat;
            MP_dataGridView.Columns.Add(colCB);

            MP_dataGridView.Columns["DeleteRow"].Width = 45;

            MP_dataGridView.ColumnHeadersVisible = true; 
        }


        private void refreshPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_button_Click(sender, e);
        }

        private void clearAllFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control c in panel1.Controls)
                if (c.GetType() == typeof(Button))
                {
                    var btn = c as Button;
                    btn.BackColor = Color.Transparent;
                }
            SearchBy_textBox.Text = "";
            VisitNumber_comboBox.SelectedIndex = Visits_comboBox.SelectedIndex = -1;
            Refresh_button_Click(sender, e);
        }

        private void Refresh_button_Click(object sender, EventArgs e)
        {
            try
            {
                bind_VisitNotifications("", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
                
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

        private void SearchBy_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in MP_dataGridView.Rows)
                {
                    var found = 0;
                    for (var i = 0; i < MP_dataGridView.ColumnCount; i++)
                    {
                        if (r.Cells[i].Value == null) continue;

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
                    }

                    if (found == 0)
                    {
                        MP_dataGridView.CurrentCell = null;
                        MP_dataGridView.Rows[r.Index].Visible = false;
                    }
                }

                //count rows
                Count_label.Text = "All: " + MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible);
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

        private void MP_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var SelectedRowCount = MP_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
                Selected_label.Text = "Selected: " + SelectedRowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MP_dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void MP_dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex == MP_dataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == MP_dataGridView.Columns["DeleteRow"].Index)
                {
                    Image image = null;
                    if (Deleted_checkBox.Checked)
                        image = Resources.Undo24;
                    else image = Resources.OK24;

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

        private void MP_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Check if click is on specific column 
                if (e.ColumnIndex == MP_dataGridView.Columns["DeleteRow"].Index)
                {
                    string message = "";
                    string seen = "";

                    if (Deleted_checkBox.Checked)
                    {
                        message = "هل أنت متأكد أنك تريد استعادة هذا الموعد؟";
                        seen = "0";
                    }
                    else
                    {
                        message = "هل أنت متأكد أنك تريد حذف هذا الموعد؟";
                        seen = "1";
                    }

                    var dialogResult = MessageBox.Show(message, "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        int ID = Convert.ToInt32(MP_dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                        string P_Name = MP_dataGridView.Rows[e.RowIndex].Cells["المستفيد"].Value.ToString();
                        string Visit = MP_dataGridView.Rows[e.RowIndex].Cells["نوع الزيارة"].Value.ToString();
                        string Date = MP_dataGridView.Rows[e.RowIndex].Cells["التاريخ"].Value.ToString();
                        int MP_ID = Convert.ToInt32(MP_dataGridView.Rows[e.RowIndex].Cells["رقم المشروع"].Value.ToString());

                        DateTime Date_2 = Convert.ToDateTime(Date);


                        noti.Update_MicroUsers_Notification(MP_ID, Date_2.ToString("yyyy/MM/dd"), Visit, P_Name,seen);
                        l.Insert_Log(
                            "Set seen="+seen+" to Notification[" + ID + "] of Visit[" + Visit + "]:" + P_Name + "-" + MP_ID + ")",
                            "user_notification", Settings.Default.username, DateTime.Now);

                        Refresh_button_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void v1_button_Click(object sender, EventArgs e)
        {
            try
            {
                bind_VisitNotifications("01", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void v2_button_Click(object sender, EventArgs e)
        {
        }

        private void v3s_button_Click(object sender, EventArgs e)
        {
        }

        private void v3_button_Click(object sender, EventArgs e)
        {
        }

        private void v4_button_Click(object sender, EventArgs e)
        {
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
                    DataTable ex_dt = ((DataTable)MP_dataGridView.DataSource).DefaultView.ToTable();

                    //Remove invisible columns//
                    for (var j = MP_dataGridView.ColumnCount - 1; j >= 0; j--)
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

        private void Year_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refresh_button_Click(sender, e);
        }

        #region months button clicks

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bind_VisitNotifications("01", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("02", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("03", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("04", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("05", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("06", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("07", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("08", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("09", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("10", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("11", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
                bind_VisitNotifications("12", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
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
            if (user_mode)
            {
                if (Year_checkBox.Checked)
                    Year_comboBox.Enabled = true;
                else Year_comboBox.Enabled = false;

                bind_VisitNotifications("", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number, Street, Seen);
                SearchBy_textBox_TextChanged(sender, e);
            }
        }
         
        private void Deleted_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Deleted_checkBox.Checked) Seen = "1";
            else Seen = "0";

            bind_VisitNotifications("", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number,Street, Seen);
            SearchBy_textBox_TextChanged(sender, e);
        }

        private void ClearPlace_button_Click(object sender, EventArgs e)
        {
            Street_comboBox.SelectedIndex = -1;
        }

        private void VisitNumber_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (user_mode)
                {
                    // Check V_Number //
                    if (VisitNumber_comboBox.SelectedIndex == -1 || VisitNumber_comboBox.SelectedIndex == 0)
                        V_number = "";
                    else V_number = VisitNumber_comboBox.SelectedItem.ToString();

                    // Check V_Kind //
                    if (Visits_comboBox.SelectedIndex == -1 || Visits_comboBox.SelectedIndex == 0)
                        V_Kind = "";
                    else
                    {
                        if (Visits_comboBox.SelectedItem.ToString() == "مركبات")
                            V_Kind = "V";
                        else V_Kind = "O";
                    }

                    // Check Street //
                    if (Street_comboBox.SelectedIndex == -1)
                        Street = "";
                    else Street = Street_comboBox.SelectedValue.ToString();

                    bind_VisitNotifications("", (Year_checkBox.Checked ? Year_comboBox.SelectedItem.ToString() : ""), V_Kind, V_number, Street, Seen);
                    SearchBy_textBox_TextChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}