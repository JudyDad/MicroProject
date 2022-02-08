using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class AddNewProjectCategory : Form
    {
        public AddNewProjectCategory()
        {
            InitializeComponent();
        }
        public AddNewProjectCategory(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        MySqlComponents MySS;
        private int Category_ID;
        private string username;
        private Log l;
        private DataRow SelectedDataRow;

        private void AddNewProjectCategory_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.AddNewForm_ToLight(this);
                else
                    myTheme.AddNewForm_ToNight(this);

                MySS = new MySqlComponents();
                l = new Log();
                Category_bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Category_bind()
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "select `C_ID` as 'ID' ,`C_Name` as 'Name' from `category`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Category_dataGridView.DataSource = MySS.dt;
            DataGridViewColumn dgC2 = Category_dataGridView.Columns["ID"];
            dgC2.Visible = false;
        }

        private void Category_dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Category_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    Category_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                    CategoryName_textBox.Text = SelectedDataRow["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertCategory_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (CategoryName_textBox.Text == "")
                {
                    throw new Exception("You can't leave empty fields");
                }
                insertCategory();

                l.Insert_Log("Insert " + CategoryName_textBox.Text, " Category ", username, DateTime.Now);

                CategoryName_textBox.Clear();
                Category_bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteCategory_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (CategoryName_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }

                deleteCategory(Category_ID);
                l.Insert_Log("Delete " + CategoryName_textBox.Text, " Category ", username, DateTime.Now);

                CategoryName_textBox.Clear();
                Category_bind();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("Please select the field you wnat to delete");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void UpdateCategory_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (CategoryName_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                updateCategory(Category_ID);
                l.Insert_Log("Update " + CategoryName_textBox.Text, " Category ", username, DateTime.Now);
                CategoryName_textBox.Clear();
                Category_bind();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("You can't leave empty fields");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region mouse move
        private void delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteCategory_button.BackgroundImage = Properties.Resources.delete;
        }
        private void delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteCategory_button.BackgroundImage = Properties.Resources.delete0;
        }
        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
          //  UpdateCategory_button.BackgroundImage = Properties.Resources.update;
        }
        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateCategory_button.BackgroundImage = Properties.Resources.update0;
        }
        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            InsertCategory_button.BackgroundImage = Properties.Resources.add;
        }
        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            InsertCategory_button.BackgroundImage = Properties.Resources.add0;
        }
        #endregion

        #region insert update delete Category
        private void insertCategory()
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Insert Into `category`(`C_Name`) values(N'"
                                    + CategoryName_textBox.Text + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void updateCategory(int CID)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Update `category` set "
                    + "`C_Name` = N'" + CategoryName_textBox.Text + "'"
                    + "where `C_ID` =" + CID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void deleteCategory(int CID)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "delete From `category` where `C_ID` =" + CID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        #endregion insert update delete Category
    }
}
