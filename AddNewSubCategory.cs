using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using System.Data;
using ClosedXML.Excel;
using System.Diagnostics;
using System.Threading;

namespace MyWorkApplication
{
    public partial class AddNewSubCategory : Form
    {
        public AddNewSubCategory()
        {
            InitializeComponent();
        }

        private int ID;
        private Log l;
        private SubCategory subC;  
        System.Data.DataTable SubCategory_dt,Category_dt;

        private void AddNewSubCategory_Load(object sender, EventArgs e)
        {
            try
            {
                var newTheme = new NewTheme();
                if (Settings.Default.theme == "Dark")
                    newTheme.Category_ToNight(this);
                else
                    newTheme.Category_ToLight(this);
                 
                l = new Log();
                subC = new SubCategory();

                SubCategory_dt = new System.Data.DataTable();
                Category_dt = new System.Data.DataTable();

                Category_bind();
                SubCategory_bind("", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Category_bind()
        {
            Category_dt = subC.Category_Select();
            Category_comboBox.DisplayMember = "C_Name";
            Category_comboBox.ValueMember = "C_ID";
            Category_comboBox.DataSource = Category_dt;

        }

        public void SubCategory_bind(string Category_ID, string Name)
        {
            if (SubCategory_dt.Rows.Count == 0)
            {
                SubCategory_dt = subC.Select("", "");
                dataGridView.DataSource = null;
                dataGridView.Columns.Clear();
                dataGridView.DataSource = SubCategory_dt; 
            }
            else
            {
                DataRow[] rows = null;
                string condition = "";
                if (Name != "")
                {
                    condition += "Name='" + Name + "'";
                    if (Category_ID != "")
                        condition += " AND Category_ID=" + Category_ID + " ";
                }
                else if (Category_ID != "")
                {
                    condition += "Category_ID=" + Category_ID + " ";
                    if (Name != "")
                        condition += " AND Name='" + Name + "'";
                }

                rows = SubCategory_dt.Select(condition);
                dataGridView.DataSource = null;
                dataGridView.Columns.Clear();

                System.Data.DataTable selected_rows_dt =new System.Data.DataTable();
                
                selected_rows_dt = SubCategory_dt.Copy();
                selected_rows_dt.Rows.Clear();
                foreach (DataRow row in rows)
                    selected_rows_dt.ImportRow(row);

                dataGridView.DataSource = selected_rows_dt;
            }

            var dgC2 = dataGridView.Columns["ID"]; dgC2.Visible = false;
            dgC2 = dataGridView.Columns["Category_ID"]; dgC2.Visible = false;
            dataGridView.Columns["Category"].ReadOnly = true;

            var colCB = new DataGridViewButtonColumn();
            colCB.Name = "DeleteRow";
            colCB.HeaderText = "";
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCB.FlatStyle = FlatStyle.Flat;
            dataGridView.Columns.Add(colCB);
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["DeleteRow"].Width = 40;

            Count_label.Text = "All:" + dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible);
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Name_textBox.Text == "" || Category_comboBox.SelectedIndex == -1)
                    throw new Exception("لا يمكن ترك بعض الخلايا فارغة!");
                subC.Insert_SubCategory(Name_textBox.Text, Convert.ToInt32( Category_comboBox.SelectedValue.ToString())); 
                l.Insert_Log("Insert " + Name_textBox.Text, "SubCategory", Properties.Settings.Default.username, DateTime.Now);

                Name_textBox.Clear();
                ID = -1; 

                SubCategory_dt.Rows.Clear();
                SubCategory_bind("", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
          
        private void Close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Category_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Category_comboBox.SelectedIndex != -1)
                    SubCategory_bind(Category_comboBox.SelectedValue.ToString(), Name_textBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Category_comboBox.SelectedIndex != -1)
                    SubCategory_bind(Category_comboBox.SelectedValue.ToString(), Name_textBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region mouse move
        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                Save_button.BackgroundImage = Resources.Save_CL;
            else Save_button.BackgroundImage = Resources.Save_CD;
        }

        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                Save_button.BackgroundImage = Resources.Save_CD;
            else Save_button.BackgroundImage = Resources.Save_CL;
        }

        private void Close_button_MouseEnter(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                Close_button.BackgroundImage = Resources.Exit_L;
            else Close_button.BackgroundImage = Resources.Exit_D;
        }

        private void Close_button_MouseLeave(object sender, EventArgs e)
        {
            if (Settings.Default.theme == "Light")
                Close_button.BackgroundImage = Resources.Exit_D;
            else Close_button.BackgroundImage = Resources.Exit_L;
        }

        #endregion
         

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dialogResult = MessageBox.Show("هل تريد حفظ التعديلات التي قمت بها؟", "Confirm before save",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var r = e.RowIndex;
                    ID = Convert.ToInt32(dataGridView.Rows[r].Cells["ID"].Value);

                    if (e.ColumnIndex == 1 || e.ColumnIndex == 2) // name - category_id
                    {
                        int CategoryID = Convert.ToInt32(dataGridView.Rows[r].Cells["Category_ID"].Value.ToString());
                        string Name = dataGridView.Rows[r].Cells["Name"].Value.ToString();

                        subC.Update_SubCategory(ID, Name, CategoryID);
                        l.Insert_Log("Update SubCategory:" + Name + ":" + CategoryID,
                            "SubCategory", Settings.Default.username, DateTime.Now);
                    }
                    dataGridView.EndEdit();
                }
                else
                {
                    dataGridView.CancelEdit(); // Set cell value back to what it was prior to user's change 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Check if click is on specific column 
                if (e.ColumnIndex == dataGridView.Columns["DeleteRow"].Index)
                {
                    var dialogResult = MessageBox.Show("هل أنت متأكد أنك تريد حذف هذه المجموعة؟", "Delete",
                        MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var ID = Convert.ToInt32( dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                        subC.Delete_SubCategory(ID);
                        l.Insert_Log("Delete SubCategory: " + dataGridView.Rows[e.RowIndex].Cells["Name"].Value,
                            "SubCategory", Settings.Default.username, DateTime.Now);
                        dataGridView.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex == dataGridView.NewRowIndex || e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == dataGridView.Columns["DeleteRow"].Index)
                {
                    Image image = null;
                    if (Settings.Default.theme == "Light")
                        image = Resources.KAKA_Alii;
                    else image = Resources.KAKA_Alii_D;

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                    var size = image.Size;
                    var location = new System.Drawing.Point((e.CellBounds.Width - size.Width) / 2,
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
                    System.Data.DataTable ex_dt = ((System.Data.DataTable)dataGridView.DataSource).DefaultView.ToTable();

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

    }
}
