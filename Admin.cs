using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MyWorkApplication.Classes;
using System.Collections.Generic;

namespace MyWorkApplication
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        public Admin(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        MySqlComponents MySS;
        private DataRow SelectedDataRow;
        private int UserID, RoleID, Log_ID;
        private Log l; int[] Log_IDs;
        DataRow LogSelectedDataRow;
        private string username;
        private string srcpath = "";
        private string destpath = "";


        #region insert update delete bind
        private void insertUser(string username, string password, string roleID)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Insert Into `user`(`UserName`,`Password`,`UserRoleID`) values(N'"
                + username + "',N'"
                + password + "',N'"
                + Int32.Parse(roleID) + "' )";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void UpdateUser(int userID, string username, string password, string roleID)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Update `user` set "
                + "`UserName` =N'" + username + "'"
                + ",`Password` =N'" + password + "'"
                + ",`UserRoleID` =N'" + Int32.Parse(roleID) + "'"
                + "\n where `UserID` = " + userID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void deleteUser(int userID)
        {
            //check connection//
             Program.buildConnection();
            
            MySS.query = "Delete from `user` where UserID = " + userID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void user_bind()
        {
            //check connection//
             Program.buildConnection();
            
            MySS.query = "select u.UserID as 'UserID'" +
                ",u.UserName as 'UserName'" +
                ",u.Password as 'Password'" +
                ",UserRoleID as 'UserRoleID'" +
                ",r.Role_Name as 'RoleName'" +
                " from `user` u join `role` r on r.Role_ID = u.UserRoleID";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            UserDataGridView.DataSource = MySS.dt;

            DataGridViewColumn dgC1 = UserDataGridView.Columns["UserID"];
            dgC1.Visible = false;
        }
        private void RoleInUser_bind()
        {
            //check connection//
             Program.buildConnection();
            
            MySS.query = "select `Role_ID`,`Role_Name` from `role`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.reader = MySS.sc.ExecuteReader();
            MySS.dt = new DataTable();
            MySS.dt.Columns.Add("Role_ID", typeof(string));
            MySS.dt.Columns.Add("Role_Name", typeof(string));
            MySS.dt.Load(MySS.reader);
            Role_comboBox.DisplayMember = "Role_Name";
            Role_comboBox.ValueMember = "Role_ID";
            Role_comboBox.DataSource = MySS.dt;
        }
        private void log_bind()
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "select `Log_ID`,`Log_Type`,`Log_User`,`Log_Date`,`Log_OnTable` from `log` where `Log_Seen` = 0";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Log_dataGridView.DataSource = MySS.dt;

            DataGridViewColumn dgC1 = Log_dataGridView.Columns["Log_ID"];
            dgC1.Visible = false;
        }
        private void log_bind(string type,string onTable,string user)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select `Log_ID`,`Log_Type`,`Log_User`,`Log_Date`,`Log_OnTable` from `log` where `Log_Seen` = 0 ";
            string condition = "";
            if(type!="")
                condition += " and Log_Type like '%"+ type + "%'";
            if (onTable != "")
                condition += " and Log_OnTable like '%" + onTable + "%'";
            if (user != "")
                condition += " and Log_User like '%" + user + "%'";
            MySS.query += condition;

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Log_dataGridView.DataSource = MySS.dt;

            DataGridViewColumn dgC1 = Log_dataGridView.Columns["Log_ID"];
            dgC1.Visible = false;
        }
        #endregion insert update delete bind

        #region users & roles
        private void AddUser_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Username_textBox.Text == "" || Password_textBox.Text == "" || ConfirmPassword_textBox.Text == ""
                    || Role_comboBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }

                insertUser(Username_textBox.Text, Password_textBox.Text, Role_comboBox.SelectedValue.ToString());

                l.Insert_Log("insert the user " + Username_textBox.Text, "User", username, DateTime.Now);

                user_bind();
                clear_user_boxes();
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
        private void UpdateUser_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Username_textBox.Text == "" || Password_textBox.Text == "" || ConfirmPassword_textBox.Text == ""
                    || Role_comboBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }

                UpdateUser(UserID, Username_textBox.Text, Password_textBox.Text, Role_comboBox.SelectedValue.ToString());

                l.Insert_Log("update the user " + Username_textBox.Text, "User", username, DateTime.Now);

                user_bind();
                clear_user_boxes();
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
        private void DeleteUser_button_Click(object sender, EventArgs e)
        {
            if (SelectedDataRow != null)
            {
                deleteUser(UserID);

                l.Insert_Log("delete the user " + Username_textBox.Text, "User", username, DateTime.Now);

                user_bind();
                clear_user_boxes();
            }
        }

        private void UserDataGridView_RowHeaderMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)UserDataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                UserID = Int32.Parse(SelectedDataRow["UserID"].ToString());
                Username_textBox.Text = (string)SelectedDataRow["UserName"];
                Password_textBox.Text = (string)SelectedDataRow["Password"];
                ConfirmPassword_textBox.Text = (string)SelectedDataRow["Password"];
                int role_ID = (int)SelectedDataRow["UserRoleID"];
                Role_comboBox.Text = role_ID.ToString();
            }
        }
        private void Role_comboBox_Enter(object sender, EventArgs e)
        {
            RoleInUser_bind();
        }
        private void clear_user_boxes()
        {
            UserID = -1;
            Username_textBox.Text = Password_textBox.Text = Role_comboBox.Text = ConfirmPassword_textBox.Text = "";
        }
        private void AddNewRole_button_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewRole AddNewRole = new AddNewRole(username);
                AddNewRole.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region tab control btn change
        private void Users_button_Click(object sender, EventArgs e)
        {
            User_TabControl.SelectedIndex = 0;
            Users_button.BackColor = Color.Maroon;
            Log_button.BackColor = Color.Transparent;
        }
        private void Data_button_Click(object sender, EventArgs e)
        {
            User_TabControl.SelectedIndex = 2;
            Users_button.BackColor = Color.Transparent;
            Log_button.BackColor = Color.Transparent;
            //SourceServer_comboBox.Items.Clear();
            //DestinationServer_comboBox.Items.Clear();
            //SourceServer_comboBox.Items.Add("(local)");
            //SourceServer_comboBox.Items.Add("HC-JUDY");
            //DestinationServer_comboBox.Items.Add("(local)");
        }
        private void Log_button_Click(object sender, EventArgs e)
        {
            User_TabControl.SelectedIndex = 1;
            Users_button.BackColor = Color.Transparent;
            Log_button.BackColor = Color.Maroon;
        }
        #endregion

        #region log
        private void RefreshLog_button_Click(object sender, EventArgs e)
        {
            log_bind();
        }
        private void ClearLog_button_Click(object sender, EventArgs e)
        {
            //select specific rows
            int SelectedRowCount = Log_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (SelectedRowCount > 0)
            {
                Log_IDs = new int[SelectedRowCount];
                for (int i = 0; i < SelectedRowCount; i++)
                {
                    LogSelectedDataRow = ((DataRowView)Log_dataGridView.SelectedRows[i].DataBoundItem).Row;
                    Log_IDs[i] = Int32.Parse(LogSelectedDataRow["Log_ID"].ToString());
                }
                l.Update_Log(Log_IDs);
            }
            else
                l.Update_Log();

            log_bind();
        }
        #endregion

        #region save - delete - refresh

        private void AddSave_button_MouseEnter(object sender, EventArgs e)
        {
            AddUser_button.BackgroundImage = Properties.Resources.save;
        }
        private void AddSave_button_MouseLeave(object sender, EventArgs e)
        {
            AddUser_button.BackgroundImage = Properties.Resources.save0;
        }
        private void UpdateSave_button_MouseEnter(object sender, EventArgs e)
        {
            UpdateUser_button.BackgroundImage = Properties.Resources.update;
        }
        private void UpdateSave_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateUser_button.BackgroundImage = Properties.Resources.update0;
        }
        private void delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteUser_button.BackgroundImage = ClearLog_button.BackgroundImage = Properties.Resources.delete;
        }
        private void delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteUser_button.BackgroundImage = ClearLog_button.BackgroundImage = Properties.Resources.delete0;
        }
        private void refresh_button_MouseEnter(object sender, EventArgs e)
        {
            RefreshLog_button.BackgroundImage = Properties.Resources.refresh;
        }
        private void refresh_button_MouseLeave(object sender, EventArgs e)
        {
            RefreshLog_button.BackgroundImage = Properties.Resources.refresh0;
        }
        private void Plus_button_MouseEnter(object sender, EventArgs e)
        {
            AddNewRole_button.BackgroundImage = Properties.Resources.plus;
        }
        private void Plus_button_MouseLeave(object sender, EventArgs e)
        {
            AddNewRole_button.BackgroundImage = Properties.Resources.plus0b;
        }
        #endregion save - delete - refresh

        //private void CallDialog()
        //{
        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        //    saveFileDialog1.Filter = "Backup file|*.bak";
        //    saveFileDialog1.Title = "Save a backup for database:";
        //    DialogResult res = saveFileDialog1.ShowDialog();
        //    if (res == DialogResult.OK)
        //    {
        //        // If the file name is not an empty string open it for saving.  
        //        if (saveFileDialog1.FileName != "")
        //        {
        //            // Saves the Image via a FileStream created by the OpenFile method.  
        //            System.IO.FileStream fs =
        //                (System.IO.FileStream)saveFileDialog1.OpenFile();
        //            destpath = saveFileDialog1.FileName;
        //            fs.Close();
        //        }
        //    }

        //}
        //private void CallDialog2()
        //{
        //    OpenFileDialog theDialog = new OpenFileDialog();
        //    theDialog.Title = "Select the backup file that you want to restore:";
        //    theDialog.Filter = "Backup file|*.bak";
        //    theDialog.InitialDirectory = @"C:\";
        //    DialogResult res = theDialog.ShowDialog();
        //    if (res == DialogResult.OK)
        //    {
        //        srcpath = theDialog.FileName.ToString();
        //        //BuckupFileLocation_textBox.Text = srcpath;
        //    }

        //}

        //private bool checkIfConnected()
        //{
        //    var ping = new Ping();
        //    var options = new PingOptions { DontFragment = true };

        //    //just need some data. this sends 10 bytes.
        //    var buffer = Encoding.ASCII.GetBytes(new string('z', 10));
        //    var host = "192.168.1.1";
        //    var host2 = "192.168.0.1";
        //    try
        //    {
        //        var reply = ping.Send(host, 1000, buffer, options);
        //        var reply2 = ping.Send(host2, 1000, buffer, options);

        //        if (reply == null && reply2 == null )
        //        {
        //            MessageBox.Show("There isn't a network connectivity.You can only enter by local connection or check your connection.", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return false;
        //        }

        //        if (reply.Status == IPStatus.Success || reply2.Status == IPStatus.Success)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            MessageBox.Show("There isn't a network connectivity.You can only enter by local connection or check your connection.", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("An exception occurred during a Ping request"))
        //            MessageBox.Show("There isn't a network connectivity.You can only enter by local connection or check your connection.", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        else
        //            MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}
        //private void GetIP()
        //{
        //    Process netUtility = new Process();
        //    netUtility.StartInfo.FileName = "net.exe";
        //    netUtility.StartInfo.CreateNoWindow = true;
        //    netUtility.StartInfo.Arguments = "view";
        //    netUtility.StartInfo.RedirectStandardOutput = true;
        //    netUtility.StartInfo.UseShellExecute = false;
        //    netUtility.StartInfo.RedirectStandardError = true;
        //    netUtility.Start();

        //    StreamReader streamReader = new StreamReader(netUtility.StandardOutput.BaseStream, netUtility.StandardOutput.CurrentEncoding);

        //    string line = "";
        //    while ((line = streamReader.ReadLine()) != null)
        //    {
        //        if (line.StartsWith("\\"))
        //        {
        //            //Server_comboBox.Items.Add(line.Substring(2).Substring(0, line.Substring(2).IndexOf(" ")).ToUpper());
        //            //SourceServer_comboBox.Items.Add(line.Substring(2).Substring(0, line.Substring(2).IndexOf(" ")).ToUpper());
        //            //DestinationServer_comboBox.Items.Add(line.Substring(2).Substring(0, line.Substring(2).IndexOf(" ")).ToUpper());
        //        }
        //    }
        //    streamReader.Close();
        //    netUtility.WaitForExit(1000);
        //}

        //private void SourceServer_comboBox_Enter(object sender, EventArgs e)
        //{
        //    if (checkIfConnected())
        //    {
        //        GetIP();
        //    }
        //}
        //private void SelectLocation1_button_Click(object sender, EventArgs e)
        //{
        //    Thread myTh = new Thread(new ThreadStart(CallDialog2));
        //    myTh.SetApartmentState(ApartmentState.STA);
        //    myTh.Start();
        //    myTh.Join();
        //    TextBox.CheckForIllegalCrossThreadCalls = false;
        //    //BuckupFileLocation_textBox.Text = srcpath;
        //}
        
        private void Admin_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.Application_ToLight(this.User_TabControl);
                else
                    myTheme.Application_ToNight(this.User_TabControl);

                MySS = new MySqlComponents();
                l = new Log();
                user_bind();
                log_bind();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Admin_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int U_ID;
            string text = Message_textBox.Text;

            UserNotification userNotification = new UserNotification();
            foreach (DataGridViewRow r in UserDataGridView.Rows)
            {
                U_ID = Convert.ToInt32(r.Cells["UserID"].Value.ToString());

                userNotification.Insert_UserNotification(DateTime.Now.Date, text, " ", 0 , U_ID,-1);
            }
        }

        private void MainBack0_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LogType_TxtBox_TextChanged(object sender, EventArgs e)
        {
            log_bind(LogType_TxtBox.Text, LogOnTable_TxtBox.Text, LogUser_TxtBox.Text);
        }
    }

}