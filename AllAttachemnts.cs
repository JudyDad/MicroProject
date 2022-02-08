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
    public partial class AllAttachemnts : Form
    {
        public AllAttachemnts()
        {
            InitializeComponent();
        }
        public AllAttachemnts(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        MySqlComponents MySS;
        public DataRow SelectedDataRow;
        string username;
        private Log l;
        private int MicroProject_ID, Image_ID;
        private string image_type,Image_Path;
        private byte[] arr;
        FtpConnector c = new FtpConnector();

        #region delete and bind
        public void Images_bind(string mp_ID,string type)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "SELECT Image_ID as 'ID'" +
                    ",MicroProject_ID as 'MicroProject_ID'" +
                    ",Image_Path as 'Image Path'" +
                    ",Image_Type as 'Image Type'" +
                    " \n FROM `image` ";
            string condition = "";
            if (mp_ID != "")
            {
                //condition = " where CAST(MicroProject_ID AS nvarchar(Max)) LIKE '" + mp_ID + "%'";
                condition = " where MicroProject_ID like CAST('" + mp_ID + "%' AS CHAR)";
                if (type != "")
                {
                     condition += " and Image_Type like '"+type+"' ";
                }
            }
            else if (type != "")
            {
                condition = " where Image_Type like '" + type + "' ";
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
        private void deleteImage(int image_ID)
        {
            //check connection//
            Program.buildConnection();
            
            string strCmd = "delete from `image` where `Image_ID` = " + image_ID + " ";

            MySS.sc = new MySqlCommand(strCmd, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        #endregion

        private void AllAttachemnts_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.ShowAllForm_ToLight(this);
                else
                    myTheme.ShowAllForm_ToNight(this);

                MySS = new MySqlComponents();
                l = new Log();
                Images_bind("", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void Images_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Images_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    Image_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                    MicroProject_ID = Int32.Parse(SelectedDataRow["MicroProject_ID"].ToString());
                    Image_Path = SelectedDataRow["Image Path"].ToString();

                    arr = null;
                    MySS.query = "select `Image_Content` from `image` where `Image_ID` = " + Image_ID + " ";
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    MySS.reader = MySS.sc.ExecuteReader();
                    MySS.reader.Read();
                    if (MySS.reader.HasRows)
                    {
                        arr = (byte[])(MySS.reader[0]);
                        MySS.reader.Close();
                        if (arr == null || arr.Length == 0)     //if content = null
                        {
                            pictureBox1.Image = null;
                            //try to download it from online server
                            pictureBox1.Image = c.Download(Image_Path);
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
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region search
        private void MicroProjectID_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Contract_radioButton.Checked)
            { image_type = "Contract"; }
            else if (Detention_radioButton.Checked)
            { image_type = "Detention"; }
            else if (InsurancePolicy_radioButton.Checked)
            { image_type = "InsurancePolicy"; }
            else if (Invoice_radioButton.Checked)
            { image_type = "Invoice"; }
            else if (Reciept_radioButton.Checked)
            { image_type = "Reciept"; }
            else if (Rent_radioButton.Checked)
            { image_type = "Rent"; }
            else if (EconomicFeasibility_radioButton.Checked)
            { image_type = "EconomicFeasibility"; }
            else if (Priest_radioButton.Checked)
            { image_type = "Priest"; }
            else
            { image_type = ""; }
            Images_bind(MicroProjectID_textBox.Text, image_type);
        }
        private void Reciept_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Contract_radioButton.Checked)
            { image_type = "Contract"; }
            else if (Detention_radioButton.Checked)
            { image_type = "Detention"; }
            else if (InsurancePolicy_radioButton.Checked)
            { image_type = "InsurancePolicy"; }
            else if (Invoice_radioButton.Checked)
            { image_type = "Invoice"; }
            else if (Reciept_radioButton.Checked)
            { image_type = "Reciept"; }
            else if (Rent_radioButton.Checked)
            { image_type = "Rent"; }
            else if (EconomicFeasibility_radioButton.Checked)
            { image_type = "EconomicFeasibility"; }
            else if (Priest_radioButton.Checked)
            { image_type = "Priest"; }

            Images_bind(MicroProjectID_textBox.Text, image_type);
        }
        #endregion

        #region btn clicks
        private void DeleteImage_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Image_ID == -1 || SelectedDataRow == null)
                {
                    throw new Exception("Please choose the image you want to delete");
                }
                deleteImage(Image_ID);
                l.Insert_Log("Delete Image of : " + MicroProject_ID + " ", "Image", username, DateTime.Now);

                //delete from server//
                c.Delete(Image_Path);
                /////////////////////
                pictureBox1.Image = null;
                Image_ID = MicroProject_ID = -1;

                Images_bind("","");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddImage_button_Click(object sender, EventArgs e)
        {
            try
            {
                SelectedDataRow = null;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void C_Back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region mouse hover
        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            AddImage_button.BackgroundImage = Properties.Resources.add;
        }
        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            AddImage_button.BackgroundImage = Properties.Resources.add0;
        }

        private void Delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteImage_button.BackgroundImage = Properties.Resources.delete;
        }
        private void Delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteImage_button.BackgroundImage = Properties.Resources.delete0;
        }
        #endregion mouse hover

    }
}
