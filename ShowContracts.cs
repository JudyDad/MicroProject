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

namespace MyWorkApplication
{
    public partial class ShowContracts : Form
    {
        public ShowContracts()
        {
            InitializeComponent();
        }
        public ShowContracts(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        MySqlComponents MySS;
        DataRow SelectedDataRow;
        Log l;
        string username;
        private void deleteContract(int c_ID)
        {
            //check connection//
            if (Program.MyConn.State != ConnectionState.Open)
            {
                Program.buildConnection();
            }
            MySS.query = "delete from `contract` where `Co_ID` = " + c_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        public void Contract_bind(string mp_ID)
        {
            //check connection//
            if (Program.MyConn.State != ConnectionState.Open)
            {
                Program.buildConnection();
            }
            MySS.query = " SELECT Co_ID as 'ID'"
                         + " ,Co_FirstGroup as 'First Group'"
                         + " ,Co_FirstGroupPlace as 'First Group Address'"
                         + " ,Co_SecondGroup as 'Second Group'"
                         + " ,Co_SecondGroupPlace as 'Second Group Address'"
                         + " ,Co_Rate as 'Rate'"
                         + " ,Co_BeginParagraph as '.'"
                         + " ,Co_Paragraph as '..'"
                         + " ,Co_EndParagraph as '...'"
                         + " ,MicroProject_ID as 'MicroProject_ID'"
                         + " FROM `contract` ";

            string condition = "";
            if (mp_ID != "")
            {
                condition = " where MicroProject_ID = " + Int32.Parse(mp_ID) + " ";
            }
            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Contract_DataGridView.DataSource = MySS.dt;
            DataGridViewColumn dgC1 = Contract_DataGridView.Columns["ID"];
            dgC1.Visible = false;
        }
        public void ContractImages_bind(string mp_ID)
        {
            //check connection//
            if (Program.MyConn.State != ConnectionState.Open)
            {
                Program.buildConnection();
            }
            MySS.query = "SELECT Image_ID as 'ID'" +
                    ",Image_Path as 'Image Path'" +
                    ",Image_Type as 'Image Type'" +
                    ",MicroProject_ID as 'MicroProject_ID'" +
                    " \n FROM `image` " +
                    " \n Where Image_Type like 'Contract' ";

            string condition = "";
            if (mp_ID != "")
            {
                //condition = " and MicroProject_ID = " + Int32.Parse(mp_ID) + " ";
                condition = " and MicroProject_ID like CAST('" + mp_ID + "%' AS CHAR)";
            }
            MySS.query += condition;

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Images_dataGridView.DataSource = MySS.dt;
            DataGridViewColumn dgC2 = Images_dataGridView.Columns["ID"];
            dgC2.Visible = false;
        }
        private void ShowContracts_Load(object sender, EventArgs e)
        {
            MySS = new MySqlComponents();
            l = new Log();
            Contract_bind("");
            ContractImages_bind("");
        }
        public int Contract_ID, MicroProject_ID, image_ID;
        public string firstGroup, secondGroup, FPlace, SPlace, Begin, Paragraph, End;
        public Double rate;
        byte[] arr;
       
        private void Contract_DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)Contract_DataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                Contract_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                firstGroup = SelectedDataRow["First Group"].ToString();
                secondGroup = SelectedDataRow["First Group Address"].ToString();
                FPlace = SelectedDataRow["Second Group"].ToString();
                SPlace = SelectedDataRow["Second Group Address"].ToString();
                rate = Double.Parse(SelectedDataRow["Rate"].ToString());
                Begin = SelectedDataRow["."].ToString();
                Paragraph = SelectedDataRow[".."].ToString();
                End = SelectedDataRow["..."].ToString();
                MicroProject_ID = Int32.Parse(SelectedDataRow["MicroProject_ID"].ToString());
            }
        }
        private void Images_dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)Images_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                image_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                arr = null;
                //check connection//
                if (Program.MyConn.State != ConnectionState.Open)
                {
                    Program.buildConnection();
                }
                MySS.query = "select Image_Content from `image` where Image_ID = " + image_ID + " ";
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);

                MySS.reader = MySS.sc.ExecuteReader();
                MySS.reader.Read();
                if (MySS.reader.HasRows)
                {
                    arr = (byte[])(MySS.reader[0]);
                    MySS.reader.Close();

                    if (arr == null || arr.Length == 0)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(arr);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    MessageBox.Show("data not available");
                    MySS.reader.Close();
                }
                MicroProject_ID = (int)SelectedDataRow["MicroProject_ID"];
            }
        }

        public DataRow showSelectedContractRow()
        {
            this.Contract_DataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Contract_DataGridView_RowHeaderMouseDoubleClick);
            if (this.ShowDialog() == DialogResult.OK)
            {
                return SelectedDataRow;
            }
            else
            {
                return null;
            }
        }
        private void MicroProjectID_textBox_TextChanged(object sender, EventArgs e)
        {
            Contract_bind(MicroProjectID_textBox.Text);
        }
        private void DeleteContract_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Contract_ID == -1 || SelectedDataRow == null)
                {
                    throw new NoNullAllowedException();
                }

                deleteContract(Contract_ID);
                l.Insert_Log("Delete contract of the project: " + MicroProject_ID + " ", " Contract", username, DateTime.Now);

                Contract_bind("");
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("Please choose the Contract you want to delete");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddContract_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateContract_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null || Contract_ID == -1)
                    throw new Exception("Please choose the Contract you want to update");
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region save - delete

        private void delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteContract_button.BackgroundImage =Properties.Resources.delete;
        }

        private void delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteContract_button.BackgroundImage = Properties.Resources.delete0;
        }

        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateContract_button.BackgroundImage =  Properties.Resources.update;
        }

        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateContract_button.BackgroundImage = Properties.Resources.update0;
        }

        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            AddContract_button.BackgroundImage = Properties.Resources.add;
        }

        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            AddContract_button.BackgroundImage =Properties.Resources.add0;
        }

        #endregion save - delete

        private void C_Back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
