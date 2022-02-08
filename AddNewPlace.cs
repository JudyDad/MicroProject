using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class AddNewPlace : Form
    {
        public AddNewPlace()
        {
            InitializeComponent();
        }
        public AddNewPlace(int type,string username)
        {
            InitializeComponent();
            typeOfPlace = type;
            this.username = username;
        }

        MySqlComponents MySS;
        public static int typeOfPlace;
        string username;
        Log l;
        DataRow SelectedDataRow;
        int Place_ID;

        public void Place_bind()
        {
            //check connection//
             Program.buildConnection();
            
            string strCmd;
            if (typeOfPlace == 0)
                strCmd = "select `CPlace_ID` as 'ID',`CPlace_Name` as 'Name',`CPlace_type` as 'Type' from `centerplace` where `CPlace_type` = 0";
            else
                strCmd = "select `CPlace_ID` as 'ID',`CPlace_Name` as 'Name',`CPlace_type` as 'Type' from `centerplace` where `CPlace_type` = 1";
            MySS.sc = new MySqlCommand(strCmd, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Place_dataGridView.DataSource = MySS.dt;
            DataGridViewColumn dgC1 = Place_dataGridView.Columns["Type"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = Place_dataGridView.Columns["ID"];
            dgC2.Visible = false;
        }

        #region insert update delete Place
        private void insertPlace(int typeOfPlace)
        {
            //check connection//
             Program.buildConnection();
            
            MySS.query = "Insert Into `centerplace`(`CPlace_Name`,'Type') values(N'"
                                        + PlaceName_textBox.Text + "',N'"
                                        + typeOfPlace
                                        + "' )";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();

        }
        private void updatePlace(int CPID, int typeOfPlace)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Update `centerplace` set "
                    + "`CPlace_Name` = N'" + PlaceName_textBox.Text + "',"
                    + "`CPlace_type` = N'" + typeOfPlace + "' "
                    + "where `CPlace_ID` =" + CPID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void deletePlace(int CPID)
        {
            //check connection//
             Program.buildConnection();
            
            MySS.query = "delete From `centerplace` where `CPlace_ID` =" + CPID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        #endregion

        #region mouse hover

        private void Delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeletePlace_button.BackgroundImage = Properties.Resources.delete;
        }
        private void Delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeletePlace_button.BackgroundImage = Properties.Resources.delete0;
        }
        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdatePlace_button.BackgroundImage = Properties.Resources.update;
        }
        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdatePlace_button.BackgroundImage = Properties.Resources.update0;
        }
        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            InsertPlace_button.BackgroundImage = Properties.Resources.add;
        }
        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            InsertPlace_button.BackgroundImage = Properties.Resources.add0;
        }
        #endregion

        private void AddNewPlace_Load(object sender, EventArgs e)
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
                Place_bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertPlace_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (PlaceName_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                insertPlace(typeOfPlace);
                l.Insert_Log("Insert " + PlaceName_textBox.Text, " Category ", username, DateTime.Now);
                PlaceName_textBox.Clear();
                Place_bind();
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

        private void UpdatePlace_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (PlaceName_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                updatePlace(Place_ID, typeOfPlace);
                l.Insert_Log("Update " + PlaceName_textBox.Text, " Category ", username, DateTime.Now);

                PlaceName_textBox.Clear();
                Place_bind();
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

        private void DeletePlace_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (PlaceName_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                deletePlace(Place_ID);
                l.Insert_Log("Delete " + PlaceName_textBox.Text, " Category ", username, DateTime.Now);

                PlaceName_textBox.Clear();
                Place_bind();
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
        private void Place_dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Place_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    Place_ID = Int32.Parse(SelectedDataRow["ID"].ToString());

                    PlaceName_textBox.Text = SelectedDataRow["Name"].ToString();
                    typeOfPlace = Int32.Parse(SelectedDataRow["Type"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
