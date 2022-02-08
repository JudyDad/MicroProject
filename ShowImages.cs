using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class ShowImages : Form
    {
        public ShowImages()
        {
            InitializeComponent();
        }

        MySqlComponents MySS;
        private string str;
        private int Person_ID;
        private byte[] arr;
        FtpConnector c;

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // Do not use MouseEventArgs.X, Y because they are relative!
            Point pt_MouseAbs = Control.MousePosition;
            Control i_Ctrl = pictureBox;
            do
            {
                Rectangle r_Ctrl = i_Ctrl.RectangleToScreen(i_Ctrl.ClientRectangle);
                if (!r_Ctrl.Contains(pt_MouseAbs))
                {
                    base.OnMouseWheel(e);
                    return; // mouse position is outside the picturebox or it's parents
                }
                i_Ctrl = i_Ctrl.Parent;
            }
            while (i_Ctrl != null && i_Ctrl != this);

            // here you have the mouse position relative to the pictureBox if you need it
            Point pt_MouseRel = pictureBox.PointToClient(pt_MouseAbs);

            // Do your work here
            if (pictureBox.Image != null)
            {
                // If the mouse wheel is moved forward (Zoom in)
                if (e.Delta > 0)
                {
                    // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
                    if ((pictureBox.Width < (15 * this.Width)) && (pictureBox.Height < (15 * this.Height)))
                    {
                        // Change the size of the picturebox, multiply it by the ZOOMFACTOR
                        pictureBox.Width = (int)(pictureBox.Width * 1.25);
                        pictureBox.Height = (int)(pictureBox.Height * 1.25);

                        // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
                        pictureBox.Top = (int)(e.Y - 1.25 * (e.Y - pictureBox.Top));
                        pictureBox.Left = (int)(e.X - 1.25 * (e.X - pictureBox.Left));
                    }
                }
                else
                {
                    // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
                    if ((pictureBox.Width > (this.Width / 15)) && (pictureBox.Height > (this.Height / 15)))
                    {
                        // Change the size of the picturebox, divide it by the ZOOMFACTOR
                        pictureBox.Width = (int)(pictureBox.Width / 1.25);
                        pictureBox.Height = (int)(pictureBox.Height / 1.25);

                        // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
                        pictureBox.Top = (int)(e.Y - 0.80 * (e.Y - pictureBox.Top));
                        pictureBox.Left = (int)(e.X - 0.80 * (e.X - pictureBox.Left));
                    }
                }
            }
        }

        private void PersonName_bind()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select `P_ID` , CONCAT(P_FirstName, ' ', P_FatherName, ' ', P_LastName) as PNAME from `person` where IsProjectOwner like 'YES'";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.reader = MySS.sc.ExecuteReader();
            MySS.dt = new DataTable();
            MySS.dt.Columns.Add("P_ID", typeof(string));
            MySS.dt.Columns.Add("PNAME", typeof(string));
            MySS.dt.Load(MySS.reader);
            MySS.reader.Close();

            listBox.DisplayMember = "PNAME";
            listBox.ValueMember = "P_ID";
            listBox.DataSource = MySS.dt;
        }

        public int Get_Person_ID()
        {
            return Person_ID;
        }

        private void ShowImages_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.ShowAllForm_ToLight(this);
                else
                    myTheme.ShowAllForm_ToNight(this);

                MySS = new MySqlComponents();
                listBox.SelectedIndex = -1;
                listBox.SelectedValue = 0;
                PersonName_bind();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            arr = null;
            str = listBox.SelectedValue.ToString();
            Person_ID = Int32.Parse(str);
            //check connection//
            Program.buildConnection();

            MySS.query = "select `P_Picture` from `person` where `P_ID` = " + Person_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            //sc1.ExecuteNonQuery();

            MySS.reader = MySS.sc.ExecuteReader();
            MySS.reader.Read();
            if (MySS.reader.HasRows)
            {
                arr = (byte[])(MySS.reader[0]);
                MySS.reader.Close();

                if (arr == null || arr.Length == 0)
                {
                    pictureBox.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(arr);
                    pictureBox.Image = Image.FromStream(ms);
                }
            }
            else
            {
                MessageBox.Show("data not available");
                MySS.reader.Close();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //       pictureBox.Size = new Size(trackBar1.Value, trackBar1.Value);
        }

        //   bool selected = false;
        private Point MouseDownLocation;

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            //     selected = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            //   selected = false;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //if (selected == true)
            //{
            //    pictureBox.Location = e.Location;
            //}
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox.Left = e.X + pictureBox.Left - MouseDownLocation.X;
                pictureBox.Top = e.Y + pictureBox.Top - MouseDownLocation.Y;
            }
        }


        public string imageFilePath = "";
        private void CallDialog()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            DialogResult res = open.ShowDialog();
            if (res == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(open.FileName);
                imageFilePath = open.FileName;
            }
            else
                imageFilePath = "";
        }
    }
}