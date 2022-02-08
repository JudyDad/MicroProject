﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using System.Threading;
using System.Diagnostics;
using ClosedXML.Excel;

namespace MyWorkApplication
{
    public partial class AddNewEducation : Form
    {
        private int Education_ID;
        private Log l; private Education ed;
        private MySqlComponents MySS; 
        private readonly string username;
        System.Data.DataTable Education_dt;

        public AddNewEducation()
        {
            InitializeComponent();
        }

        public AddNewEducation(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void AddNewEducation_Load(object sender, EventArgs e)
        {
            try
            {
                var newTheme = new NewTheme();
                if (Settings.Default.theme == "Dark")
                    newTheme.Category_ToNight(this);
                else
                    newTheme.Category_ToLight(this);
                 
                Education_dt = new System.Data.DataTable();

                MySS = new MySqlComponents();
                l = new Log();
                ed = new Education();
                Education_bind("", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Education_bind(string Edu_type, string Name)
        { 
                Education_dt = ed.Select(Edu_type, Name);
                dataGridView.DataSource = null;
                dataGridView.Columns.Clear();
                dataGridView.DataSource = Education_dt;
          
            var dgC2 = dataGridView.Columns["ID"]; dgC2.Visible = false;
            dataGridView.Columns["Type"].ReadOnly = true;

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
                if (EducationName_textBox.Text == "" || EducationType_comboBox.Text == "")
                    throw new Exception("You can't leave empty fields");

                ed.Insert(EducationName_textBox.Text, EducationType_comboBox.Text); 
                l.Insert_Log("Insert " + EducationName_textBox.Text, " Education ", username, DateTime.Now);

                EducationName_textBox.Clear();
                Education_ID = -1; 

                Education_dt.Rows.Clear(); 
                Education_bind("", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EducationType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(EducationType_comboBox.SelectedIndex != -1)
                    Education_bind(EducationType_comboBox.SelectedItem.ToString(), "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void EducationName_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    foreach (DataGridViewRow r in dataGridView.Rows)
                    {
                        var found = 0;
                                 //columns -1 without delete button
                        for (var i = 0; i < dataGridView.ColumnCount-1; i++) 
                            if (r.Cells[i].Value.ToString().ToLower().Contains(EducationName_textBox.Text.ToLower()))
                            {
                                dataGridView.Rows[r.Index].Visible = true;
                                found = 1;
                            }  
                        if (found == 0)
                        {
                            dataGridView.CurrentCell = null;
                            dataGridView.Rows[r.Index].Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
         
        private void Education_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dialogResult = MessageBox.Show("هل تريد حفظ التعديلات التي قمت بها؟", "Confirm before save",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var r = e.RowIndex;
                    Education_ID = Convert.ToInt32(dataGridView.Rows[r].Cells["ID"].Value);

                    if (e.ColumnIndex == 1 || e.ColumnIndex == 2) // name - category_id
                    {
                        string Name = dataGridView.Rows[r].Cells["Name"].Value.ToString();
                        string Type = dataGridView.Rows[r].Cells["Type"].Value.ToString();

                        ed.Update(Education_ID, Name, Type);
                        l.Insert_Log("Update Education:" + Type + ":" + Name, "Education", Settings.Default.username, DateTime.Now);
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

        private void Education_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                        var ID = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                        ed.Delete(ID);
                        l.Insert_Log("Delete Education: " + dataGridView.Rows[e.RowIndex].Cells["Name"].Value,
                            "Education", Settings.Default.username, DateTime.Now);
                        dataGridView.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot delete or update a parent row"))
                    MessageBox.Show("لا يمكنك حذف هذا الخيار ! بسبب وجود معلومات متعلقة به..");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void Education_dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void Education_dataGridView_SelectionChanged(object sender, EventArgs e)
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