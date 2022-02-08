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
    public partial class AddNewLevel : Form
    {
        public AddNewLevel()
        {
            InitializeComponent();
        }
        public AddNewLevel(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        MySqlComponents MySS;
        private int Level_ID;
        private string username;
        private Log l;
        private DataRow SelectedDataRow;

        #region insert update delete Level

        private void insertLevel()
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Insert Into `level`(`Level_Symbol`,`Level_Description`) values(N'"
                                    + Level_Symbol_textBox.Text + "',N'"
                                    + Level_Description_textBox.Text + "' )";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void updateLevel(int Level_ID)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Update `level` set "
                    + "`Level_Symbol` = N'" + Level_Symbol_textBox.Text + "',"
                    + "`Level_Description` = N'" + Level_Description_textBox.Text + "'"
                    + "where `Level_ID` =" + Level_ID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void deleteLevel(int Level_ID)
        {
            //check connection//
             Program.buildConnection();
            
            MySS.query = "delete From `level` where `Level_ID` =" + Level_ID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        #endregion

        public void Level_bind()
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "select `Level_ID` as 'ID' ,`Level_Symbol` as 'Symbol',`Level_Description` as 'Description' from `level`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Level_dataGridView.DataSource = MySS.dt;
            DataGridViewColumn dgC2 = Level_dataGridView.Columns["Level_ID"];
            dgC2.Visible = false;
        }

        private void InsertLevel_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Level_Symbol_textBox.Text == "" || Level_Description_textBox.Text == "")
                {
                    throw new Exception("You can't leave empty fields");
                }
                insertLevel();

                l.Insert_Log("Insert " + Level_Symbol_textBox.Text, " Level ", username, DateTime.Now);

                Level_Symbol_textBox.Clear();
                Level_Description_textBox.Clear();
                Level_bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteLevel_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Level_Symbol_textBox.Text == "" || Level_Description_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }

                deleteLevel(Level_ID);
                l.Insert_Log("Delete " + Level_Symbol_textBox.Text, " Level ", username, DateTime.Now);

                Level_Symbol_textBox.Clear();
                Level_Description_textBox.Clear();
                Level_bind();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("Please select the field you want to delete");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateLevel_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Level_Symbol_textBox.Text == "" || Level_Description_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }

                updateLevel(Level_ID);
                l.Insert_Log("Update " + Level_Symbol_textBox.Text, " Level ", username, DateTime.Now);

                Level_Symbol_textBox.Clear();
                Level_Description_textBox.Clear();
                Level_bind();
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

        private void AddNewLevel_Load(object sender, EventArgs e)
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
                Level_bind(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #region mouse move
        private void delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteLevel_button.BackgroundImage = Properties.Resources.delete;
        }
        private void delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteLevel_button.BackgroundImage = Properties.Resources.delete0;
        }
        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateLevel_button.BackgroundImage = Properties.Resources.update;
        }
        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateLevel_button.BackgroundImage = Properties.Resources.update0;
        }
        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            InsertLevel_button.BackgroundImage = Properties.Resources.add;
        }
        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            InsertLevel_button.BackgroundImage = Properties.Resources.add0;
        }

        #endregion

        private void Level_dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Level_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    Level_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                    Level_Symbol_textBox.Text = (string)SelectedDataRow["Symbol"];
                    Level_Description_textBox.Text = SelectedDataRow["Description"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
