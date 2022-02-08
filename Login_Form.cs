using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;

namespace MyWorkApplication
{
    public partial class Login_Form : Form
    {
        private Log l;
        private User u;
        private MySqlComponents MySS;
        private byte[] PersonPicArr;
        private char selected_hope; 

        bool connection = false; //EndUserSeach = false; //ADDED BY SARY
        List<string> Databases_Of_the_User = new List<string>(); //ADDED BY SARY - UPDATED BY ME
        DataTable User_info = new DataTable(); //ADDED BY SARY 
        //ADD ANY NEW CENTER HERE AFTER CREATE ITS DATABAE
        List<string> Centers = new List<string> { "Aleppo", "Homs", "Damascus", "Lebanon" }; //ADDED BY SARY 
         
        public Login_Form()
        {
            InitializeComponent();
            Version_label.Text +=  Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {
            try
            {
                MySS = new MySqlComponents();
                l = new Log();
                u = new User();
                connection = false;
                Databases_Of_the_User.Clear();

                //if (Properties.Settings.Default.first_login)
                //{
                //    Properties.Settings.Default.Admin_Visible_arr = "";
                //    Properties.Settings.Default.Search0_Visible_arr = "";
                //    Properties.Settings.Default.Search1_Visible_arr = "";
                //    Properties.Settings.Default.Search2_Visible_arr = "";
                //    Properties.Settings.Default.Search3_Visible_arr = "";
                //    Properties.Settings.Default.Search4_Visible_arr = "";
                //    Properties.Settings.Default.Search5_Visible_arr = "";
                //    Properties.Settings.Default.Search6_Visible_arr = "";
                //    Properties.Settings.Default.Search7_Visible_arr = "";
                //    Properties.Settings.Default.Task_Visible_arr = "";
                //    Properties.Settings.Default.RememberMe = false;
                //    Properties.Settings.Default.first_login = false;
                //    Properties.Settings.Default.Save();
                //}

                //if (Settings.Default.update_version)
                //{
                //    Settings.Default.Upgrade();
                //    Settings.Default.update_version = false;
                //    Settings.Default.Save();
                //}

                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "Micro Projects", "Team");

                if (Directory.Exists(specificFolder).Equals(false))
                {
                    Directory.CreateDirectory(specificFolder);
                }

                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Micro Projects\Team\" + Properties.Settings.Default.username + ".png";
                FileInfo file = new FileInfo(path);
                 
                if (Settings.Default.RememberMe)
                {
                    Remember_checkBox.Checked = true;
                    if (Settings.Default.username != "" && Settings.Default.password != "")
                    {
                        UserName_textBox.Text = Settings.Default.username;
                        Password_textBox.Text = Settings.Default.password;
                        
                        if (file.Exists.Equals(true))
                        { 
                            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                ProfilePicture_pictureBox.BackgroundImage = Image.FromStream(stream);
                            } 
                        }
                        else
                        {
                            ProfilePicture_pictureBox.BackgroundImage = Properties.Resources.Unknown_User;
                        } 
                    }
                }
                else
                { 
                    UserName_textBox.Text = "username";
                    Password_textBox.Text = "password";
                    
                    //DELETE IMAGE FILE
                    file = new FileInfo(path);
                    if (file.Exists.Equals(true) && !Properties.Settings.Default.RememberMe)
                    {
                        file.Delete();
                    }
                    ProfilePicture_pictureBox.BackgroundImage = Properties.Resources.Unknown_User;
                }
                UserName_textBox.Select(UserName_textBox.Text.Length, UserName_textBox.Text.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Connect_button_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                if (!Online_checkBox.Checked) //offline
                    Properties.Settings.Default.Online = false;
                else if (Online_checkBox.Checked) //online
                    Properties.Settings.Default.Online = true; 
                Properties.Settings.Default.Save();

                for (int i = 0; i < Centers.Count; i++)
                {
                    Properties.Settings.Default.selected_hope = Centers[i].ToString();
                    Properties.Settings.Default.Save();
                    Program.set_ConnectionString();

                    if (Program.firstbuildConnection())
                    {
                        connection = true;

                        dt = u.Get_Version();

                        var version1 = new Version(Application.ProductVersion.ToString());
                        var version2 = new Version(dt.Rows[0].Field<string>("l_version"));

                        var result = version1.CompareTo(version2);
                        if (result > 0)
                        {
                            u.Update_Version(Application.ProductVersion.ToString());
                        }
                        else if (result < 0)
                        {
                            // Using old version
                            MessageBox.Show("أنت تستخدم نسخة قديمة من البرنامج" + Environment.NewLine + "الرجاء التحديث إلى النسخة ذات الرقم" + Environment.NewLine + version2.ToString(), "Error");
                            return;
                        }
                        else
                        {
                            // Versions are equal - Do nothing
                        }
                         
                        dt = Check_User(); 
                        if (dt.Rows.Count == 1) //user exsist
                        {
                            Databases_Of_the_User.Add(Centers[i].ToString());
                            User_info = dt;
                        }
                    }

                }

                if (!connection)
                {
                    Properties.Settings.Default.selected_hope = "";
                    Properties.Settings.Default.Save(); 
                    Program.set_ConnectionString();

                    MessageBox.Show("please check your network connection and try again", "Login Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (Databases_Of_the_User.Count < 1)
                {
                    Properties.Settings.Default.selected_hope = "";
                    Properties.Settings.Default.Save();

                    Program.set_ConnectionString();
                    MessageBox.Show("please check username or password", "Login Faild", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (Databases_Of_the_User.Count >= 1)
                { 
                    Program.set_ConnectionString();
                    Program.User_In_Database = Databases_Of_the_User;
                     
                    Set_Properties();
                    Set_User_Image();
                } 
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to connect to the server"))
                    MessageBox.Show("Unable to connect to the server please try again or check your connection",
                        "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Check_Version()
        {
            DataTable dt = u.Get_Version();

            var version1 = new Version(Application.ProductVersion.ToString());
            var version2 = new Version(dt.Rows[0].Field<string>("l_version"));

            var result = version1.CompareTo(version2);
            if (result > 0)
            {
                u.Update_Version(Application.ProductVersion.ToString());
            }
            else if (result < 0)
            {
                // Using old version
                MessageBox.Show("أنت تستخدم نسخة قديمة من البرنامج" + Environment.NewLine + "الرجاء التحديث إلى النسخة ذات الرقم" + Environment.NewLine + version2.ToString(), "Error");
                return;
            }
            else
            {
                // Versions are equal - Do nothing
            }
        }

        private DataTable Check_User()
        { 
            string query = "select * from `user` where UserName like '" + UserName_textBox.Text +
                         "' and Password like '" + Password_textBox.Text + "' ";
            MySqlDataAdapter da = new MySqlDataAdapter(query, Program.MyFirstConn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Program.MyFirstConn.Close();
            return dt;
            
        }

        private void Set_Properties()
        {
            Properties.Settings.Default.selected_hope = Databases_Of_the_User[0];
            Properties.Settings.Default.Save();
            Program.set_ConnectionString();
            
            ////////////////////////////////////////////////////
            //Select userID from selected database and save it in properties//
            User u = new User();
            DataTable user_info_dt = u.Select_Users(UserName_textBox.Text,"","");
            int User_ID = user_info_dt.Rows[0].Field<int>(0);  
            int role = User_info.Rows[0].Field<int>("UserRoleID");

            Properties.Settings.Default.username = UserName_textBox.Text;
            Properties.Settings.Default.password = Password_textBox.Text;
            Properties.Settings.Default.role = role;
            Properties.Settings.Default.userID = User_ID;
            Properties.Settings.Default.Save();

            if (Remember_checkBox.Checked)
            { 
                Properties.Settings.Default.RememberMe = true; 
                Properties.Settings.Default.Save(); 
            }
            else
            {
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
            } 
        }

        private void Select_User_Profile()
        {
            PersonPicArr = null;
            Program.buildConnection();
            var query = "select ProfilePicture from `user` where UserName like '" + UserName_textBox.Text +
                        "' and Password like '" + Password_textBox.Text + "' ";
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            byte[] query_array = dt.Rows[0].Field<byte[]>(0);
            if (query_array != null && query_array.Length > 0)
            {
                MySqlDataReader reader = sc.ExecuteReader();
                reader.Read();
                PersonPicArr = (byte[])reader[0];

                reader.Close();
            }
            Program.MyConn.Close();
        }
         

        private void Close_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Close_button_MouseEnter(object sender, EventArgs e)
        {
            Close_button.BackgroundImage = Resources.Exit_D;
        }

        private void Close_button_MouseLeave(object sender, EventArgs e)
        {
            Close_button.BackgroundImage = Resources.Exit_L;
        }
        
        private void Connect_button_MouseEnter(object sender, EventArgs e)
        {
            Connect_button.BackgroundImage = Resources.login_L;
        }

        private void Connect_button_MouseLeave(object sender, EventArgs e)
        {
            Connect_button.BackgroundImage = Resources.login_D;
        }

        private void Set_User_Image()
        {
            FileInfo file;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Micro Projects\Team\" + Properties.Settings.Default.username + ".png";

            //GET IMAGE IF NOT EXIST
            file = new FileInfo(path);
            if (file.Exists.Equals(false))
            {
                //PersonPicArr = u.Select_ProfilePicture(dt.Rows[0].Field<int>("ID"));
                Select_User_Profile();

                if (PersonPicArr != null && PersonPicArr.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(PersonPicArr);

                    using (FileStream file2 = new FileStream(path, FileMode.Create, System.IO.FileAccess.Write))
                        ms.CopyTo(file2);
                }
            }

            //SET IMAGE
            file = new FileInfo(path);
            if (file.Exists.Equals(true))
            {

                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    ProfilePicture_pictureBox.BackgroundImage = Image.FromStream(stream);
                }

            }
            else
                ProfilePicture_pictureBox.BackgroundImage = Properties.Resources.Unknown_User;

            //////DELETE IMAGE FILE
            ////file = new FileInfo(path);
            ////if (file.Exists.Equals(true) && !Properties.Settings.Default.RememberMe)
            ////{
            ////    file.Delete();
            ////}
        }

        private void ShowPassword_button_Click(object sender, EventArgs e)
        {
            if (Password_textBox.PasswordChar == '*')
            {
                Password_textBox.PasswordChar = '\0';
                string Pass = Password_textBox.Text;
                Password_textBox.Clear();
                Password_textBox.Text = Pass;
                ShowPassword_button.BackgroundImage = Properties.Resources.show_password_true;
                toolTip1.SetToolTip(ShowPassword_button, "Hide Password");
            }
            else if (Password_textBox.PasswordChar == '\0')
            {
                Password_textBox.PasswordChar = '*'; //•
                string Pass = Password_textBox.Text;
                Password_textBox.Clear();
                Password_textBox.Text = Pass;
                ShowPassword_button.BackgroundImage = Properties.Resources.show_password_false;
                toolTip1.SetToolTip(ShowPassword_button, "Show Password");
            }
        }
    }
}