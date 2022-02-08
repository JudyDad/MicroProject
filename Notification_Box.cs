using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;

namespace MyWorkApplication
{
    public partial class Notification_Box : Form
    {
        private DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool dragging;
        private Log l;

        private MySqlComponents MySS;
        private int seen;
        private UserNotification userNotification;
        public static bool makeSeens;

        public Notification_Box()
        {
            InitializeComponent();
        }

        private void bind(int User_ID, string Role_ID)
        {
            if (Role_ID == "3") //financial
                MySS.query = @"select MicroProject_ID as 'ID'"
                             + ",P_Name as 'Title'"
                             + ",IF(CAST(Body AS UNSIGNED) = 0,Body,CAST(Body AS UNSIGNED)) as 'Body'"
                             + ",Date as 'Date'"
                             + ",User_ID as 'User_ID'"
                             + ",ID as 'N_ID'"
                             + ",user_notification.Pay_ID as 'Details'"
                             + ",user.UserName as 'Sent By'"
                             + " from `user_notification` left join user on  user_notification.User_ID = user.UserID "
                             + " where user.UserRoleID = " + Role_ID + " and Seen = 0 and Date <= CURDATE() "
                             + " order by Date desc";
            else
                MySS.query = "select MicroProject_ID as 'ID'" +
                             ",P_Name as 'Title'" +
                             ",Body as 'Body'" +
                             ",Date as 'Date'" +
                             ",User_ID as 'User_ID'" +
                             ",ID as 'N_ID'" +
                             ",user_notification.Pay_ID as 'Details'" +
                             ",(select UserName from user where user.UserID = user_notification.Sender_ID)  as 'Sent By'" +
                             " from `user_notification` " +
                             " where User_ID = " + User_ID + " and Seen = 0 and Date < CURDATE() + INTERVAL 4 DAY " +
                             " order by Date desc ";
            //// Date from just 3 Months till NOW-4 days //// Date > CURDATE() + INTERVAL -3 MONTH and 

            //check connection//
            Program.buildConnection();
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            var ds = new DataSet();
            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.da.Fill(ds, "projects");
            Program.MyConn.Close();

            Notification_dataGridView.ColumnHeadersVisible = false;
            Notification_dataGridView.DataSource = ds.Tables[0];
            Notification_dataGridView.Columns["Date"].DefaultCellStyle.Format = "dd-MM-yyyy";

            //body
            Notification_dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (Role_ID == "3") //financial
            {
                Notification_dataGridView.Columns["Body"].DefaultCellStyle.Format = "#,#";
                Notification_dataGridView.Columns["Body"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                var dgc3 = Notification_dataGridView.Columns["Details"];
                dgc3.Visible = false;
            }

            Notification_dataGridView.ColumnHeadersVisible = true;
            var dgc1 = Notification_dataGridView.Columns["N_ID"];
            dgc1.Visible = false;
            var dgc2 = Notification_dataGridView.Columns["User_ID"];
            dgc2.Visible = false;

            //count rows
            Counter_textBox.Text =
                Notification_dataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void add_checkBox_Column()
        {
            foreach (DataGridViewColumn col in Notification_dataGridView.Columns) col.ReadOnly = true;
            var colCB = new DataGridViewCheckBoxColumn();
            colCB.Name = "Seen";
            colCB.HeaderText = "Seen";
            colCB.FalseValue = false;
            colCB.TrueValue = true;
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Notification_dataGridView.Columns.Add(colCB);

            Notification_dataGridView.Columns["ID"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            Notification_dataGridView.Columns["Date"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            Notification_dataGridView.Columns["Sent By"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            Notification_dataGridView.Columns["Details"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            Notification_dataGridView.Columns["Seen"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            Notification_dataGridView.Columns["Seen"].Width = 30;
            Notification_dataGridView.Columns["ID"].Width = 40;
            Notification_dataGridView.Columns["Sent By"].Width = 80;
            Notification_dataGridView.Columns["Date"].Width = 100;
            Notification_dataGridView.Columns["Title"].Width = 160;
        }

        private void Notification_Box_Load(object sender, EventArgs e)
        {
            try
            {
                var newTheme = new NewTheme();
                if (Settings.Default.theme == "Dark")
                    newTheme.NotificationBox_ToNight(this);
                else
                    newTheme.NotificationBox_ToLight(this);


                MySS = new MySqlComponents();
                userNotification = new UserNotification();
                l = new Log();
                makeSeens = false;

                // make financial //
                if (Settings.Default.role == 3)
                    bind(Settings.Default.userID, "3");
                else
                    bind(Settings.Default.userID, "");

                add_checkBox_Column();
                SeenCounter_textBox.Text = seen.ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Notification_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Notification_dataGridView.CurrentRow.Index != null)
                    if (e.ColumnIndex == 8)
                    {
                        var r_index = Notification_dataGridView.CurrentRow.Index;

                        ch1 = Notification_dataGridView.Rows[r_index].Cells["Seen"] as DataGridViewCheckBoxCell;
                        ch1.ValueType = typeof(bool);
                        if (ch1.Value == null)
                        {
                            ch1.Value = true;
                            seen++;
                        }
                        else
                        {
                            ch1.Value = null;
                            seen--;
                        }

                        SeenCounter_textBox.Text = seen.ToString();
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Close_button_MouseEnter(object sender, EventArgs e)
        {
            Close_button.BackgroundImage = Resources.Exit_L;
        }

        private void Close_button_MouseLeave(object sender, EventArgs e)
        {
            Close_button.BackgroundImage = Resources.Exit_D;
        }

        private void Notification_dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void Notification_Box_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in Notification_dataGridView.Rows)
                {
                    var ch2 = row.Cells["Seen"] as DataGridViewCheckBoxCell;
                    ch2.ValueType = typeof(bool);
                    if (ch2.Value == null) continue;

                    if ((bool) ch2.Value)
                    {
                        var N_ID = row.Cells["N_ID"].Value.ToString();
                        var P_Name = row.Cells["Title"].Value.ToString();
                        var N_Body = row.Cells["Body"].Value.ToString();

                        N_Body = replaceQuotation(N_Body);
                        P_Name = replaceQuotation(P_Name);

                        if (Settings.Default.role == 3) //financial
                        {
                            var N_Pay_ID = row.Cells["Details"].Value.ToString();
                            userNotification.Update_NotificationWithPaymentID(int.Parse(N_Pay_ID),"","","1");
                        }
                        else
                        {
                            var N_Date = row.Cells["Date"].Value.ToString();
                            var N_MicroProject_ID = row.Cells["ID"].Value.ToString();

                            var oDate = Convert.ToDateTime(N_Date);


                            // clear this notification from other micro users //
                            userNotification.Update_MicroUsers_Notification(int.Parse(N_MicroProject_ID), oDate.ToString("yyyy/MM/dd"), N_Body,P_Name,"1"); 
                        }
                        makeSeens = true;
                        l.Insert_Log("The Notification[" + N_ID + "]:" + P_Name + "-" + N_Body + ")",
                            "user_notification", Settings.Default.username, DateTime.Now);
                    }
                    else
                    {
                    }
                }
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Notification_Box_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = Location;
        }

        private void Notification_Box_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                var dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Notification_Box_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Project_label_Click(object sender, EventArgs e)
        {
        }

        private string replaceQuotation(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }
    }
}