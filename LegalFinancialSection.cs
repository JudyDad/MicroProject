using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using word = Microsoft.Office.Interop.Word;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class LegalFinancialSection : Form
    {
        public LegalFinancialSection()
        {
            InitializeComponent();
        }
        public LegalFinancialSection(int i, string username)
        {
            InitializeComponent();
            this.username = username;
            pageType = i;
            if (i == 1)    //add
            {
                LegalFinancial_tabControl.Visible = true;
            }
            LegalFinancial_tabControl.SelectedIndex = 1;
        }
        public LegalFinancialSection(DataRow dr, string username)       //update
        {
            InitializeComponent();
            this.username = username;
            pageType = 2;
            LegalFinancial_tabControl.Visible = true;
            LegalFinancial_tabControl.SelectedIndex = 1;
            SelectedDataRow = dr;
        }

        private int pageType;
        private string username;
        private Log l; MySqlComponents MySS;
        private Notification noti;
        private ShowImages si;
        private int MicroProject_ID, Loan_ID, Pay_ID, ExeFile_ID, image_ID, contract_ID, Person_ID;
        private int SimpleProfit;
        private double rate, ReclameMoney;
        private DataRow SelectedDataRow, SelectedDataRow_P, ContrctDataRow, MicroProjectDataRow;
        private string image_type;
        private SoundPlayer Pay_Notification_SoundPlayer, Exe_Notification_SoundPlayer;
        private Double Loan_Amount = -1;
        private Thread myTh;
        private string imageFilePath, imageName, Image_Path;
        private byte[] arr;
        FtpConnector c = new FtpConnector();
        AllLoansPayments AllLoansPayments;
        SelectPersonAndProject SelectPersonAndProject;
        string pay;
        TasksOfProjects TasksOfProjects;
        UserNotification userNotification;

        #region tab control buttons
        
        private void ExecutiveFile_button_Click(object sender, EventArgs e)
        {
            ExecutiveFile_button.BackColor = Color.Maroon;
            Attachments_button.BackColor = Notifications_button.BackColor =
        LoansPayements_button.BackColor = Color.Transparent;

            LegalFinancial_tabControl.SelectedIndex = 0;
        }

        private void LoansPayements_button_Click(object sender, EventArgs e)
        {
            LegalFinancial_tabControl.SelectedIndex = 1;

            LoansPayements_button.BackColor = Color.Maroon;
            ExecutiveFile_button.BackColor = Notifications_button.BackColor =
        Attachments_button.BackColor = Color.Transparent;
        }

        private void Attachments_button_Click(object sender, EventArgs e)
        {
            try
            {
                LegalFinancial_tabControl.SelectedIndex = 2;

                Attachments_button.BackColor = Color.Maroon;
                ExecutiveFile_button.BackColor = Notifications_button.BackColor =
            LoansPayements_button.BackColor = Color.Transparent; ;
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Notifications_button_Click(object sender, EventArgs e)
        {
            LegalFinancial_tabControl.SelectedIndex = 3;

            Notifications_button.BackColor = Color.Maroon;
            ExecutiveFile_button.BackColor = Attachments_button.BackColor =
            LoansPayements_button.BackColor = Color.Transparent;

            DateTime dDate = DateTime.Now;
            //String.Format("{0:yyyy-MM-dd}", dDate);
            //if (Payment_Notification_bind1(string.Format("{0:yyyy-MM-dd}", dDate)))
            if (Payment_Notification_bind1(DateTime.Now))
            {
                Pay_Notification_SoundPlayer.Play();
            }
            if (ExeFile_Notification_bind1(DateTime.Now))
            {
                Exe_Notification_SoundPlayer.Play();
            }
        }

        #endregion tab control buttons

        #region inserts
        
        private void insertLoan(int MP_ID)
        {
            //check connection//
            Program.buildConnection();

            string Loan = Loan_Amount_textBox.Text.Replace(",", "");
            MySS.query = "Insert Into `loan`(`Loan_Amount`, `Loan_DateTaken`, `Loan_PaymentsCount`, `MicroProject_ID`) values("
                                      + Loan + ",N'"
                                      + Loan_DateTaken_dateTimePicker.Value.Year.ToString() + "/"
                                      + Loan_DateTaken_dateTimePicker.Value.Month.ToString() + "/"
                                      + Loan_DateTaken_dateTimePicker.Value.Day.ToString() + "',"
                                      + Int32.Parse(Loan_PaymentsCount_textBox.Text) + ","
                                      + MP_ID + " )";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void insertPayment(int Loan_ID)
        {
            //check connection//
            Program.buildConnection();

            pay = Pay_Amount_textBox.Text.Replace(",", "");
            MySS.query = "Insert Into `payment`(`Pay_Amount`, `Pay_DueDate`, `Pay_IsPaid`, `Loan_ID`, `Pay_RecievedOnDate`) values("
                                      + pay + ",N'"
                                      + Pay_DueDate_dateTimePicker.Value.Year.ToString() + "/"
                                      + Pay_DueDate_dateTimePicker.Value.Month.ToString() + "/"
                                      + Pay_DueDate_dateTimePicker.Value.Day.ToString() + "',N'"
                                      + (Pay_IsPaid_checkBox.Checked ? "Paid" : "Not Paid") + "',"
                                      + Loan_ID + ",N'"
                                      + (!Pay_IsPaid_checkBox.Checked ? Pay_DueDate_dateTimePicker.Value.Year.ToString() + "/"
                                                                     + Pay_DueDate_dateTimePicker.Value.Month.ToString() + "/"
                                                                     + Pay_DueDate_dateTimePicker.Value.Day.ToString() : Pay_RecievedOnDate_dateTimePicker.Value.Year.ToString() + "/"
                                                                                                                        + Pay_RecievedOnDate_dateTimePicker.Value.Month.ToString() + "/"
                                                                                                                        + Pay_RecievedOnDate_dateTimePicker.Value.Day.ToString()
                                      ) + "') ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void insertExeFile(int MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `exefile`(`ExeF_No`, `ExeF_BeginDate`, `ExeF_CurrentDate`, `ExeF_ImpoundDate`, `ExeF_ImpoundType`, `MicroProject_ID`) values(N'"
                                      + ExeF_No_textBox.Text + "',N'"
                                      //+ ExeF_BeginDate_dateTimePicker.Value.Month.ToString() + "/"
                                      //+ ExeF_BeginDate_dateTimePicker.Value.Day.ToString() + "/"
                                      //+ ExeF_BeginDate_dateTimePicker.Value.Year.ToString() + "',N'"
                                      + ExeF_BeginDate_dateTimePicker.Value.Year.ToString() + "/"
                                      + ExeF_BeginDate_dateTimePicker.Value.Month.ToString() + "/"
                                      + ExeF_BeginDate_dateTimePicker.Value.Day.ToString() + "',N'"

                                      + ExeF_CurrentDate_dateTimePicker.Value.Year.ToString() + "/"
                                      + ExeF_CurrentDate_dateTimePicker.Value.Month.ToString() + "/"
                                      + ExeF_CurrentDate_dateTimePicker.Value.Day.ToString() + "',N'"

                                      + ExeF_ImpoundDate_dateTimePicker.Value.Year.ToString() + "/"
                                      + ExeF_ImpoundDate_dateTimePicker.Value.Month.ToString() + "/"
                                      + ExeF_ImpoundDate_dateTimePicker.Value.Day.ToString() + "',N'"

                                      + ExeF_ImpoundType_textBox.Text  + "',"

                                      + MicroProject_ID + " ) ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        public void insertImage(int MP_ID, string path, byte[] array, string type)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `image`(`Image_Content`, `Image_Path`, `Image_Type`, `MicroProject_ID`) values("
                                + "@Image_Content" + ",N'"
                            //  + destinationFolderPath + "\\Micro_Project " + image_ID + ".jpg' ,N'"
                                + path + "',N'"
                                + type + "',"
                                + MP_ID + ")";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.Parameters.Add("@Image_Content", MySqlDbType.Blob);
            MySS.sc.Parameters["@Image_Content"].Value = array;
            MySS.sc.ExecuteNonQuery();
        }

        #endregion inserts
        #region updates

        private void updateLoan(int L_ID)
        {
            //check connection//
            Program.buildConnection();

            string Loan = Loan_Amount_textBox.Text.Replace(",", "");
            MySS.query = "update `loan` set " +
            "`Loan_Amount` = " + Double.Parse(Loan) + "," +
            //"`Loan_DateTaken` = N'" + Loan_DateTaken_dateTimePicker.Value.Month.ToString() + "/"
            //                        + Loan_DateTaken_dateTimePicker.Value.Day.ToString() + "/"
            //                        + Loan_DateTaken_dateTimePicker.Value.Year.ToString() + "'," +
            "`Loan_DateTaken` = N'" + Loan_DateTaken_dateTimePicker.Value.Year.ToString() + "/"
                                    + Loan_DateTaken_dateTimePicker.Value.Month.ToString() + "/"
                                    + Loan_DateTaken_dateTimePicker.Value.Day.ToString() + "'," +
            "`Loan_PaymentsCount` = " + Int32.Parse(Loan_PaymentsCount_textBox.Text) + "," +
            "`MicroProject_ID` = " + Int32.Parse(MP_ID_TextBox.Text) + " " +
            "\n where `Loan_ID` = " + L_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void updatePayment(int L_ID, int P_ID)
        {
            //check connection//
            Program.buildConnection();

            string pay = Pay_Amount_textBox.Text.Replace(",", "");
            MySS.query = "update `payment` set " +
            "Pay_Amount = " + Double.Parse(pay) + "," +
            "Pay_DueDate = N'" + Pay_DueDate_dateTimePicker.Value.Year.ToString() + "/"
                                 + Pay_DueDate_dateTimePicker.Value.Month.ToString() + "/"
                                 + Pay_DueDate_dateTimePicker.Value.Day.ToString() + "'," +

            "Pay_IsPaid = N'" + (Pay_IsPaid_checkBox.Checked ? "Paid" : "Not Paid") + "'," +
            "Loan_ID = " + L_ID + "," +
            "Pay_RecievedOnDate =N' " + (!Pay_IsPaid_checkBox.Checked ? Pay_DueDate_dateTimePicker.Value.Year.ToString() + "/"
                                                                       + Pay_DueDate_dateTimePicker.Value.Month.ToString() + "/"
                                                                       + Pay_DueDate_dateTimePicker.Value.Day.ToString() :  Pay_RecievedOnDate_dateTimePicker.Value.Year.ToString() + "/"
                                                                                                                         + Pay_RecievedOnDate_dateTimePicker.Value.Month.ToString() + "/"
                                                                                                                         + Pay_RecievedOnDate_dateTimePicker.Value.Day.ToString()
                                          ) + "' " +
            "\n where Pay_ID = " + P_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void updateExeFile(int ExeF_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "update `exefile` set " +
            "ExeF_No = N'" + ExeF_No_textBox.Text + "'," +
            //"ExeF_BeginDate = N'" + ExeF_BeginDate_dateTimePicker.Value.Month.ToString() + "/"
            //                        + ExeF_BeginDate_dateTimePicker.Value.Day.ToString() + "/"
            //                        + ExeF_BeginDate_dateTimePicker.Value.Year.ToString() + "'," +
            "ExeF_BeginDate = N'" + ExeF_BeginDate_dateTimePicker.Value.Year.ToString() + "/"
                                    + ExeF_BeginDate_dateTimePicker.Value.Month.ToString() + "/"
                                    + ExeF_BeginDate_dateTimePicker.Value.Day.ToString() + "'," +
            "ExeF_CurrentDate = N'" + ExeF_CurrentDate_dateTimePicker.Value.Year.ToString() + "/"
                                      + ExeF_CurrentDate_dateTimePicker.Value.Month.ToString() + "/"
                                      + ExeF_CurrentDate_dateTimePicker.Value.Day.ToString() + "'," +

            "ExeF_ImpoundDate = N'" + ExeF_ImpoundDate_dateTimePicker.Value.Year.ToString() + "/"
                                      + ExeF_ImpoundDate_dateTimePicker.Value.Month.ToString() + "/"
                                      + ExeF_ImpoundDate_dateTimePicker.Value.Day.ToString() + "'," +

           "ExeF_ImpoundType = N'"  + ExeF_ImpoundType_textBox.Text  + "'," +

            "MicroProject_ID = " + MicroProject_ID + " " +
            "\n where ExeF_ID = " + ExeF_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        //private void updateContract(int c_ID)
        //{
        //    //check connection//
        //    Program.buildConnection();

        //    MySS.query = "update `contract` set " +
        //        "Co_FirstGroup = N'" + C_FirstGroup_textBox.Text + "'," +
        //        "Co_FirstGroupPlace = N'" + C_FirstGroupPlace_textBox.Text + "'," +
        //        "Co_SecondGroup = N'" + C_SecondGroup_textBox.Text + "'," +
        //        "Co_SecondGroupPlace = N'" + C_SecondGroupPlace_textBox.Text + "'," +
        //        "Co_Rate = " + Double.Parse(C_Rate_textBox.Text) + "," +
        //        "Co_BeginParagraph = N'" + C_BeginParagraph_textBox.Text + "'," +
        //        "Co_Paragraph = N'" + C_Text_richTextBox.Text + "'," +
        //        "Co_EndParagraph = N'" + C_EndParagraph_textBox.Text + "'," +
        //        "MicroProject_ID = " + MicroProject_ID + " " +
        //        "\n where Co_ID = " + c_ID + " ";
        //    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
        //    MySS.sc.ExecuteNonQuery();
        //}

        #endregion updates
        #region deletes

        private void deleteLoan(int L_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `loan` where Loan_ID = " + L_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void deletePayment(int P_ID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "delete from `payment` where Pay_ID = " + P_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void deleteExeFile(int ExeF_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `exefile` where ExeF_ID = " + ExeF_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void deleteImage(int image_ID)
        {            
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `image` where Image_ID = " + image_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void deleteContract(int c_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `contract` where Co_ID = " + c_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        #endregion deletes

        #region binds
        private void Payment_bind(string Loan_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select Pay_ID as 'ID'" +
            ",Pay_Amount as 'Amount'" +
            ",Pay_DueDate as 'Pay Date'" +
            ",Pay_IsPaid as 'State'" +
            ",Pay_RecievedOnDate as 'Actual Pay Date'" +
            ",Loan_ID as 'Loan_ID'" +
            "\n from `payment` ";
            string condition = "\n";
            if (Loan_ID != "")
            {
                condition = " where Loan_ID = " + Int32.Parse(Loan_ID) + " ";
            }
            MySS.query += condition;

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            Payment_dataGridView.DataSource = MySS.dt;
            Payment_dataGridView.Columns[1].DefaultCellStyle.Format = "#,##0";
            Payment_dataGridView.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            Payment_dataGridView.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";

            DataGridViewColumn dgc1 = Payment_dataGridView.Columns["ID"];
            dgc1.Visible = false;
        }
        private bool Payment_Notification_bind1(DateTime date)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "select N_ID as 'ID'" +
                      ",N_Date  as 'Pay Date'" +
                      ",N_PName_Amount as 'Beneficiary'" +
                      ",N_Type as 'MicroProject_ID'" + 
                      ",N_ExeMP_ID as 'Pay Amount'" +
                      ",N_PaymentID as 'Payment_ID'" +
                      " from `notification`" +
                      " where N_Type != -7 and N_Seen = 0 ";
            string condition = "";
            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            DataSet ds = new DataSet();
            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.da.Fill(ds);
            Payment_Notification_listView.Items.Clear();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime rowDate = Convert.ToDateTime(row["Pay Date"]);
                if(rowDate.Date <= date.Date)
                {
                    ListViewItem item = new ListViewItem(row["Beneficiary"].ToString(), Payment_Notification_listView.Groups[0]);
                    item.SubItems.Add(row["MicroProject_ID"].ToString());
                    item.SubItems.Add(row["Pay Amount"].ToString());
                    item.SubItems.Add(row["Pay Date"].ToString());
                    item.SubItems.Add(row["ID"].ToString());
                    item.SubItems.Add(row["Payment_ID"].ToString());
                    Payment_Notification_listView.Items.Add(item);
                }
                
            }
            if (Payment_Notification_listView.Items.Count == 0)
                return false;
            return true;
        }
        private bool ExeFile_Notification_bind1(DateTime date)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select N_ID as 'ID'" +
                                  ",N_Date  as 'Date'" +
                                  ",N_PName_Amount as 'Beneficiary'" +
                                  ",N_ExeMP_ID as 'MicroProject_ID'" +
                                  " from `notification`" +
                                  " where N_Type = -7 and N_Seen = 0 ";
            string condition = "";
            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            DataSet ds = new DataSet();
            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.da.Fill(ds);
            ExeFile_Notification_listView.Items.Clear();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime rowDate = Convert.ToDateTime(row["Date"]);
                if (rowDate.Date <= date.Date)
                {
                    ListViewItem item = new ListViewItem(row["Beneficiary"].ToString(), ExeFile_Notification_listView.Groups[0]);
                    item.SubItems.Add(row["MicroProject_ID"].ToString());
                    item.SubItems.Add(row["Date"].ToString());
                    item.SubItems.Add(row["ID"].ToString());
                    ExeFile_Notification_listView.Items.Add(item);
                }
            }
            if (ExeFile_Notification_listView.Items.Count == 0)
                return false;
            return true;
        }
        private void Image_bind(string MP_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "SELECT Image_ID as 'ID'" +
                    ",Image_Path as 'Image Path'" +
                    ",Image_Type as 'Image Type'" +
                    ",MicroProject_ID as 'MicroProject_ID'" +
                    " \n FROM `image` " +
                    " \n Where MicroProject_ID = " + Int32.Parse(MP_ID) + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Images_dataGridView.DataSource = MySS.dt;
            DataGridViewColumn dgC2 = Images_dataGridView.Columns["ID"];
            dgC2.Visible = false;
        }
        #endregion binds

        #region add buttons
        private void AddLoan_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Loan_Amount_textBox.Text == "")
                {
                    throw new Exception("Loan Amount");
                }
                MicroProject_ID = Int32.Parse(MP_ID_TextBox.Text);
                insertLoan(MicroProject_ID);
                l.Insert_Log("insert Loan to: " + MicroProject_ID + " ", " Loan", username, DateTime.Now);
                
                //add user notification " VISITS " for micro users
                DateTime first_visit_date = Loan_DateTaken_dateTimePicker.Value.AddDays(27);

                DateTime second_visit_date = first_visit_date.AddMonths(2);
                second_visit_date = second_visit_date.AddDays(27);

                DateTime third_visit_date = second_visit_date.AddMonths(3);
                third_visit_date = third_visit_date.AddDays(27);

                DateTime forth_visit_date = third_visit_date.AddMonths(3);
                forth_visit_date = forth_visit_date.AddDays(27);

                List<int> User_IDs = new List<int>() { 23 ,4,8,20,16};
                //User_IDs.Add(23);   //bassil
                //User_IDs.Add(4);    //judy
                //User_IDs.Add(8);    //myriam
                //User_IDs.Add(20);   //ola
                //User_IDs.Add(16);   //subhi
                foreach (int u in User_IDs)
                {
                    userNotification.Insert_UserNotification(first_visit_date, "الزيارة الأولى - مطابقة", Person_Name_textBox.Text, MicroProject_ID,u,-5);
                    userNotification.Insert_UserNotification(second_visit_date, "الزيارة الثانية - مالي", Person_Name_textBox.Text, MicroProject_ID, u, -10);
                    userNotification.Insert_UserNotification(third_visit_date, "الزيارة الثالثة - مالي", Person_Name_textBox.Text, MicroProject_ID, u, -15);
                    userNotification.Insert_UserNotification(forth_visit_date, "الزيارة الرابعة - مالي", Person_Name_textBox.Text, MicroProject_ID, u, -20);
                }

                //update lana task ^_^
                ///////////////////////////   Task IDs = 17   ////////////////////////////////////////////
                TasksOfProjects = new TasksOfProjects();
                TasksOfProjects.Update_Task_MicroProject(MicroProject_ID, 17, true, DateTime.Now);

                getCurrentLoanID();

                Payment_bind(Loan_ID.ToString());
                calculate_RemainedAmount(Loan_ID);
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("Please choose the Project you want to add the Loan to it");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Loan Amount"))
                    MessageBox.Show("You can't leave empty fields, Please enter the Loan Amount.");
                else
                    MessageBox.Show(ex.Message);
            }
        }
        private void AddPayment_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pay_Amount_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                if (Loan_ID == -1)
                {
                    throw new Exception("Please choose the Loan you want to add Payments to it");
                }

                insertPayment(Loan_ID);

                
                l.Insert_Log("insert Payment to: " + MicroProject_ID + " ", " Payment", username, DateTime.Now);
                getCurrentPaymentID();

                if (!Pay_IsPaid_checkBox.Checked)
                {
                    noti.Insert_Notification(Pay_DueDate_dateTimePicker.Value, Convert.ToInt32(pay), Pay_ID, Person_Name_textBox.Text, MicroProject_ID); //Payment note
                    userNotification.Insert_UserNotification(Pay_DueDate_dateTimePicker.Value, pay, Person_Name_textBox.Text, MicroProject_ID, 5, Pay_ID);                                                                                                                            //  noti.Insert_Notification(Pay_DueDate_dateTimePicker.Value, MicroProject_ID, Pay_ID, Pay_Amount_textBox.Text, 0); //Payment note
                }
                Payment_bind(Loan_ID.ToString());
                calculate_RemainedAmount(Loan_ID);
                Pay_DueDate_dateTimePicker.Value = Pay_NextDueDate_dateTimePicker.Value;
               
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("You can't leave empty fields, Please enter the Payment Amount. ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddExeFile_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExeF_No_textBox.Text == "")
                {
                    throw new NoNullAllowedException("ExeFile_ID");
                }
                if (MicroProject_ID_textBox.Text == "" || MicroProject_ID == -1)
                {
                    throw new NoNullAllowedException("MicroProject_ID");
                }
                insertExeFile(MicroProject_ID);
                l.Insert_Log("insert Executional file to: " + MicroProject_ID + " ", " Executional file", username, DateTime.Now);

                getCurrentExeFileID();

                if (ExeF_ImpoundDate_checkBox.Checked)
                {
                    noti.Insert_Notification(ExeF_ExpiredDate_dateTimePicker.Value, ExeFile_ID, -7, Person_Name_textBox.Text, -7); //Exe file note
                    noti.Update_NotificationWithExeFileID(ExeFile_ID);

                    userNotification.Insert_UserNotification(ExeF_ExpiredDate_dateTimePicker.Value, "الملف التنفيذي", Person_Name_textBox.Text, MicroProject_ID, 6, ExeFile_ID);
                    userNotification.Update_NotificationWithPaymentID(ExeFile_ID);
                }
                else
                {
                    noti.Insert_Notification(ExeF_ExpiredDate_dateTimePicker.Value, ExeFile_ID, -7, Person_Name_textBox.Text, -7); //Exe file note
                    userNotification.Update_NotificationWithPaymentID(ExeFile_ID);
                }
                clear_ExeFile_boxes();
            }
            catch (NoNullAllowedException NO)
            {
                if (NO.Message == "ExeFile_ID")
                {
                    MessageBox.Show("You Can't leave empty fields");
                }
                if (NO.Message == "MicroProject_ID")
                {
                    MessageBox.Show("Please choose the Project you want to add Executive File to it");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ImageSave_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (ImageLocation_textBox.Text == "")
                {
                    throw new Exception("Please choose the image you want to add");
                }
                
                if (image_type == "")
                {
                    throw new Exception("Please choose the image type you want");
                }

                foreach (String file in FileNames)
                {
                    imageFilePath = file;
                    imageName = Path.GetFileName(file);
                    ImageLocation_textBox.Text = "/micro_images/" + MicroProject_ID + "_" + imageName;

                    Convert_Picture();

                    string ftpPath = "/micro_images/" + MicroProject_ID + "_" + imageName;
                    string fullFtpPath = "ftp://judy@hcsyria.org" + ftpPath;

                    insertImage(MicroProject_ID, fullFtpPath, arr, image_type);
                    l.Insert_Log("insert image to: " + MicroProject_ID + " ", "Images", username, DateTime.Now);

                    //upload image to the server

                    c.Upload(fullFtpPath, imageFilePath);
                    l.Insert_Log("upload image " + ImageLocation_textBox.Text + " to the server: ", "Images", username, DateTime.Now);
                    clear_Images_boxes();
                    Image_bind(MicroProject_ID.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion add buttons
        #region update buttons

        private void UpdateLoan_button_Click(object sender, EventArgs e)
        {
            try
            {
                updateLoan(Loan_ID);

                l.Insert_Log("Update Loan of : " + MicroProject_ID + " ", "Loan", username, DateTime.Now);
                
                Payment_bind(Loan_ID.ToString());
                calculate_RemainedAmount(Loan_ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdatePayment_button_Click(object sender, EventArgs e)
        {
            try
            {
                updatePayment(Loan_ID, Pay_ID);
                l.Insert_Log("Update payment of: " + MicroProject_ID + " ", "Payment", username, DateTime.Now);
                if (!Pay_IsPaid_checkBox.Checked)
                {
                    noti.Insert_Notification(Pay_DueDate_dateTimePicker.Value, Convert.ToInt32(pay), Pay_ID, Person_Name_textBox.Text, MicroProject_ID); //Payment note
                    userNotification.Insert_UserNotification(Pay_DueDate_dateTimePicker.Value, pay, Person_Name_textBox.Text, MicroProject_ID, 5, Pay_ID);                                                                                                                            //  noti.Insert_Notification(Pay_DueDate_dateTimePicker.Value, MicroProject_ID, Pay_ID, Pay_Amount_textBox.Text, 0); //Payment note

                }
                else
                {
                    userNotification.Update_NotificationWithPaymentID(Pay_ID);
                    noti.Update_NotificationWithPaymentID(Pay_ID);
                }

                Payment_bind(Loan_ID.ToString());
                calculate_RemainedAmount(Loan_ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateExeFile_button_Click(object sender, EventArgs e)
        {
            try
            {
                updateExeFile(ExeFile_ID);
                l.Insert_Log("Update Executional file of : " + MicroProject_ID + " ", "Executional file", username, DateTime.Now);

                if (ExeF_ImpoundDate_checkBox.Checked)
                {
                    noti.Insert_Notification(ExeF_ExpiredDate_dateTimePicker.Value, ExeFile_ID, -7, Person_Name_textBox.Text,-7); //Exe file note
                    noti.Update_NotificationWithExeFileID(ExeFile_ID);

                    userNotification.Insert_UserNotification(ExeF_ExpiredDate_dateTimePicker.Value, "الملف التنفيذي", Person_Name_textBox.Text, MicroProject_ID, 6, ExeFile_ID);                   
                    userNotification.Update_NotificationWithPaymentID(ExeFile_ID);
                }
                else
                {
                    noti.Insert_Notification(ExeF_ExpiredDate_dateTimePicker.Value, ExeFile_ID, -7, Person_Name_textBox.Text, -7); //Exe file note
                    userNotification.Insert_UserNotification(ExeF_ExpiredDate_dateTimePicker.Value, "الملف التنفيذي", Person_Name_textBox.Text, MicroProject_ID, 6, ExeFile_ID);
                }
                clear_ExeFile_boxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        #endregion update buttons
        #region delete buttons

        private void DeleteLoan_button_Click(object sender, EventArgs e)
        {
            try
            {
                deleteLoan(Loan_ID);
                l.Insert_Log("Delete Loan of : " + MicroProject_ID + " ", "Loan", username, DateTime.Now);
                
                Loan_Amount_textBox.Text = Loan_RemainAmount_textBox.Text = Loan_PaymentsCount_textBox.Text = MP_ID_TextBox.Text = "";
                Loan_DateTaken_dateTimePicker.Value = DateTime.Now;
                Loan_ID = -1;

                Payment_bind("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeletePayment_button_Click(object sender, EventArgs e)
        {
            try
            {
                deletePayment(Pay_ID);
                l.Insert_Log("Delete Payment of : " + MicroProject_ID + " ", "Payment", username, DateTime.Now);
                Pay_Amount_textBox.Text = "";
                Pay_RecievedOnDate_dateTimePicker.Value = Pay_NextDueDate_dateTimePicker.Value = Pay_DueDate_dateTimePicker.Value = DateTime.Now;
                Pay_IsPaid_checkBox.Checked = false;
                
                noti.Update_NotificationWithPaymentID(Pay_ID);

                Pay_ID = -1;
                Payment_bind(Loan_ID.ToString());
                calculate_RemainedAmount(Loan_ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteExeFile_button_Click(object sender, EventArgs e)
        {
            try
            {
                deleteExeFile(ExeFile_ID);
                l.Insert_Log("Delete Executional file of : " + MicroProject_ID + " ", "Executional file", username, DateTime.Now);
                ExeF_No_textBox.Text = "";
                ExeF_BeginDate_dateTimePicker.Value = ExeF_CurrentDate_dateTimePicker.Value = ExeF_ExpiredDate_dateTimePicker.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImageDelete_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (image_ID == -1 || SelectedDataRow == null)
                {
                    throw new Exception("Please choose the Image you want to delete");
                }
                deleteImage(image_ID);
                l.Insert_Log("Delete Image of : " + MicroProject_ID + " ", "Image", username, DateTime.Now);

                //delete from server//
                c.Delete(Image_Path);
                /////////////////////

                Image_bind(MicroProject_ID.ToString());
                ImageLocation_textBox.Text = "";
                pictureBox1.Image = null;
                image_ID = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteContract_button_Click(object sender, EventArgs e)
        {
            try
            {
                deleteContract(contract_ID);
                l.Insert_Log("Delete contract of the project: " + MicroProject_ID + " ", " Contract", username, DateTime.Now);
                MessageBox.Show("The contract has deleted successfully");
                clear_boxes();
                getCurrentContractID();
               // CID_textBox.Text = (++contract_ID).ToString();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("Please choose the contract you want to delete");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion delete buttons

        #region datagridviews select
        private void Payment_dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectedDataRow_P = ((DataRowView)Payment_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow_P != null)
                {
                    Pay_ID = Int32.Parse(SelectedDataRow_P["ID"].ToString());
                    Double Pay_Amount = Double.Parse(SelectedDataRow_P["Amount"].ToString());
                    Pay_Amount_textBox.Text = Pay_Amount.ToString();

                    Pay_DueDate_dateTimePicker.Value = (DateTime)SelectedDataRow_P["Pay Date"];
                    string Pay_IsPaid = (string)SelectedDataRow_P["State"];
                    if (Pay_IsPaid.Contains("Not"))
                        Pay_IsPaid_checkBox.Checked = false;
                    else
                        Pay_IsPaid_checkBox.Checked = true;
                    Pay_RecievedOnDate_dateTimePicker.Value = (DateTime)SelectedDataRow_P["Actual Pay Date"];
                    Loan_ID = (int)SelectedDataRow_P["Loan_ID"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Images_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Images_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    image_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                    Image_Path = SelectedDataRow["Image Path"].ToString();
                    arr = null;
                    //check connection//
                    Program.buildConnection();

                    string strCmd = "select Image_Content from `image` where Image_ID = " + image_ID + " ";
                    MySS.sc = new MySqlCommand(strCmd, Program.MyConn);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion datagridviews select

        #region text & value & radiobutton & checkbox Changed

        //private void C_Rate_textBox_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (C_Rate_textBox.Text != "")
        //        {
        //            rate = Double.Parse(C_Rate_textBox.Text);
        //            if (MP_SimpleProfit_textBox.Text != "")
        //            {
        //                SimpleProfit = Int32.Parse(MP_SimpleProfit_textBox.Text);
        //                ReclameMoney = (rate * SimpleProfit) / 100;
        //                C_ReclameMoney_textBox.Text = ReclameMoney.ToString();
        //            }
        //        }
        //        else
        //        {
        //            rate = 0;
        //            C_ReclameMoney_textBox.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void Pay_IsPaid_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Pay_IsPaid_checkBox.Checked)
            {
                Pay_RecievedOnDate_dateTimePicker.Enabled = true;
            }
            else
                Pay_RecievedOnDate_dateTimePicker.Enabled = false;
        }
        
        private void ExeF_BeginDate_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ExeF_CurrentDate_dateTimePicker.Value = ExeF_BeginDate_dateTimePicker.Value;
        }
        private void ExeF_CurrentDate_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ExeF_ExpiredDate_dateTimePicker.Value = ExeF_CurrentDate_dateTimePicker.Value.AddMonths(5);
            ExeF_ExpiredDate_dateTimePicker.Value = ExeF_ExpiredDate_dateTimePicker.Value.AddDays(20);
        }
        private void ExeF_ImpoundDate_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ExeF_ExpiredDate_dateTimePicker.Value = ExeF_ImpoundDate_dateTimePicker.Value.AddMonths(5);
            ExeF_ExpiredDate_dateTimePicker.Value = ExeF_ExpiredDate_dateTimePicker.Value.AddDays(20);
        }

        private void Pay_DueDate_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Pay_NextDueDate_dateTimePicker.Value = Pay_DueDate_dateTimePicker.Value.AddMonths(1);
        }

        private void Detention_radioButton_CheckedChanged(object sender, EventArgs e)
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
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            try
            {
                Payment_Notification_bind1(monthCalendar1.SelectionEnd);
                ExeFile_Notification_bind1(monthCalendar1.SelectionEnd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Payment_Notification_listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                string N_ID;
                foreach (ListViewItem item in Payment_Notification_listView.CheckedItems)
                {
                    N_ID = item.SubItems[4].Text;
                    noti.Update_Notification(Int32.Parse(N_ID));
                    item.Remove();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExeFile_Notification_listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {

                string N_ID;
                foreach (ListViewItem item in ExeFile_Notification_listView.CheckedItems)
                {
                    N_ID = item.SubItems[3].Text;
                    noti.Update_Notification(Int32.Parse(N_ID));
                    item.Remove();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion text & value & radiobutton & checkbox Changed

        #region select project - beneficiary - contact - person ID

        //private void ShowContract_button_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ShowContracts = new ShowContracts();

        //        ContrctDataRow = ShowContracts.showSelectedContractRow();
        //        if (ContrctDataRow != null)
        //        {
        //            contract_ID = ShowContracts.Contract_ID;
        //      //      CID_textBox.Text = contract_ID.ToString();
        //            C_Rate_textBox.Text = (string)ShowContracts.rate.ToString();
        //            C_FirstGroup_textBox.Text = (string)ShowContracts.firstGroup.ToString();
        //            C_SecondGroup_textBox.Text = (string)ShowContracts.secondGroup.ToString();
        //            C_FirstGroupPlace_textBox.Text = (string)ShowContracts.FPlace.ToString();
        //            C_SecondGroupPlace_textBox.Text = (string)ShowContracts.SPlace.ToString();
        //            C_BeginParagraph_textBox.Text = (string)ShowContracts.Begin.ToString();
        //            C_Text_richTextBox.Text = (string)ShowContracts.Paragraph.ToString();
        //            C_EndParagraph_textBox.Text = (string)ShowContracts.End.ToString();
        //            C_LawyerName_textBox.Text = "المحامي مجد زريق";
        //            MicroProject_ID = ShowContracts.MicroProject_ID;
        //            //ShowContracts.Close();
        //            AddContract_button.Visible = false;
        //            UpdateContract_button.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {

        //    }
        //}

        private void Select_PersonPic_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                si = new ShowImages();
                si.Show();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void SelectPerson_button_Click(object sender, EventArgs e)
        {
            try
            {
                SelectPersonAndProject = new SelectPersonAndProject();
                MicroProjectDataRow = SelectPersonAndProject.showSelectedMPRow();
                if (MicroProjectDataRow != null)
                {
                    Person_ID = (int)MicroProjectDataRow["Beneficiary_ID"];
                    Person_Name_textBox.Text = (string)MicroProjectDataRow["Beneficiary Name"];
                    MicroProject_ID = (int)MicroProjectDataRow["MicroProject_ID"];
                    MicroProject_ID_textBox.Text = MicroProject_ID.ToString();
                    MP_ID_TextBox.Text = MicroProject_ID.ToString();

                    SimpleProfit = (int)MicroProjectDataRow["Minimal Profit"];
                  //  MP_SimpleProfit_textBox.Text = SimpleProfit.ToString();
                }
                if(LegalFinancial_tabControl.SelectedIndex == 2)
                    Image_bind(MicroProject_ID.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Person_ID = -1;
                MicroProject_ID = -1;
            }
            finally
            {
                SelectPersonAndProject = null;
                MicroProjectDataRow = null;
            }
        }

        #endregion select project - beneficiary - contact - person ID

        #region fill & clear boxes
        private void fill_ExeFiles_boxes(DataRow DRow)
        {
            if (DRow != null)
            {
                ExeFile_ID = Int32.Parse(DRow["ID"].ToString());

                ExeF_No_textBox.Text = DRow["Number"].ToString();

                ExeF_BeginDate_dateTimePicker.Value = (DateTime)DRow["Begin Date"];
                ExeF_CurrentDate_dateTimePicker.Value = (DateTime)DRow["Current Date"];

                ExeF_ImpoundDate_dateTimePicker.Value = (DateTime)DRow["Impound Date"];
                ExeF_ImpoundType_textBox.Text = DRow["Impound Type"].ToString();

                MicroProject_ID = (int)DRow["MicroProject_ID"];
                MicroProject_ID_textBox.Text = MicroProject_ID.ToString();
                Person_Name_textBox.Text = getPersonNameFromProjectID(MicroProject_ID);

                UpdateExeFile_button.Visible = true;
                AddExeFile_button.Visible = false;
            }
            else
            {
                clear_ExeFile_boxes();
            }
        }
        private void clear_ExeFile_boxes()
        {
            ExeF_No_textBox.Text = "";
            ExeF_BeginDate_dateTimePicker.Value = ExeF_CurrentDate_dateTimePicker.Value
                = ExeF_ExpiredDate_dateTimePicker.Value = DateTime.Now;
            ExeF_ImpoundDate_dateTimePicker.Enabled = false;
            ExeF_ImpoundDate_checkBox.Checked = false;
            ExeFile_ID = -1;

            UpdateExeFile_button.Visible = false;
            AddExeFile_button.Visible = true;
        }
        private void clear_Images_boxes()
        {
            ImageLocation_textBox.Text = "";
            pictureBox1.Image = null;
        }
        private void fill_LoansPayments_boxes(DataRow DRow)
        {
            if (DRow != null)
            {
                Loan_ID = Int32.Parse(DRow["ID"].ToString());
                Loan_Amount = Double.Parse(DRow["Loan Amount"].ToString());
                Loan_Amount_textBox.Text = Loan_Amount.ToString();
                Loan_DateTaken_dateTimePicker.Value = (DateTime)DRow["Receive Date"];
                int Loan_PaymentsCount = Int32.Parse(DRow["Payments Count"].ToString());
                Loan_PaymentsCount_textBox.Text = Loan_PaymentsCount.ToString();

                MicroProject_ID = (int)DRow["MicroProject_ID"];
                MP_ID_TextBox.Text = MicroProject_ID.ToString();
                calculate_RemainedAmount(Loan_ID);
                Payment_bind(Loan_ID.ToString());

                MicroProject_ID_textBox.Text = MicroProject_ID.ToString();
                Person_Name_textBox.Text = getPersonNameFromProjectID(MicroProject_ID);

                UpdateLoan_button.Visible = true;
                AddLoan_button.Visible = false;
            }
        }
        private void clear_LoansPayments_boxes()
        {
            Loan_Amount_textBox.Clear();
            Loan_DateTaken_dateTimePicker.Value = DateTime.Now;
            Loan_PaymentsCount_textBox.Clear();
            Loan_RemainAmount_textBox.Clear();
            MP_ID_TextBox.Clear();

            Pay_Amount_textBox.Clear();
            Pay_DueDate_dateTimePicker.Value = Pay_DueDate_dateTimePicker.Value =
                Pay_RecievedOnDate_dateTimePicker.Value = DateTime.Now;
            Pay_IsPaid_checkBox.Checked = false;
            Loan_ID = Pay_ID = -1;

            UpdateLoan_button.Visible = false;
            AddLoan_button.Visible =true ;
        }
        private void clear_boxes()
        {
            Pay_RecievedOnDate_dateTimePicker.Enabled = false;
            MP_ID_TextBox.Text = Loan_Amount_textBox.Text = Loan_RemainAmount_textBox.Text = Pay_Amount_textBox.Text = "";
            Loan_DateTaken_dateTimePicker.Value = Pay_RecievedOnDate_dateTimePicker.Value = Pay_DueDate_dateTimePicker.Value = Pay_NextDueDate_dateTimePicker.Value = DateTime.Now;
            Loan_ID = Pay_ID = -1;
            MicroProject_ID = image_ID = -1;
            contract_ID = -1;
            
            ExeF_ImpoundDate_dateTimePicker.Enabled = false;
            ExeF_ImpoundDate_checkBox.Checked = false;
        }
        #endregion

        private void LegalFinancialSection_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.Legal_ToLight(this.LegalFinancial_tabControl, Header_panel);
                else
                    myTheme.Legal_ToNight(this.LegalFinancial_tabControl, Header_panel);
                
                MySS = new MySqlComponents();
                Pay_Notification_SoundPlayer = new SoundPlayer(Properties.Resources.alert1);
                Exe_Notification_SoundPlayer = new SoundPlayer(Properties.Resources.alert2);

                clear_boxes();
                l = new Log();
                noti = new Notification();
                userNotification = new UserNotification();
                clear_ExeFile_boxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintContract_button_Click(object sender, EventArgs e)
        {
            try
            {
        //create the application
        word.Application word_Obj = new word.Application();
        word_Obj.Visible = true;
        word_Obj.WindowState = word.WdWindowState.wdWindowStateNormal;

        //create the document
        word.Document word_Doc = word_Obj.Documents.Add();

        //add paragraph
        word.Paragraph word_Paragraph;
        word_Paragraph = word_Doc.Paragraphs.Add();
        word.Range rng = word_Paragraph.Range;
        rng.Font.Size = 34;
        rng.Font.Name = "Arial";
                rng.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
                rng.Text = "\t\t\t\t\t\tعقد شركة محاصة" + "\n";

                word.Range rng2 = word_Paragraph.Range;
                rng2.Font.Size = 14;
                rng2.Font.Name = "Arial";
                rng2.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphRight;
                //word_Paragraph.Range.Font.Size = 14;
                //word_Paragraph.Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphRight;

                //rng2.Text = C_FirstGroup_Lable.Text + C_FirstGroup_textBox.Text + "\n";
                //rng2.Text += C_FirstGroupPlace_Lable.Text + C_FirstGroupPlace_textBox.Text + "\n";
                //rng2.Text += C_SecondGroup_Lable.Text + C_SecondGroup_textBox.Text + "\n";
                //rng2.Text += C_SecondGroupPlace_Lable.Text + C_SecondGroupPlace_textBox.Text + "\n\n";
                //rng2.Text += C_BeginParagraph_textBox.Text + "\n";
                //rng2.Text += C_Text_richTextBox.Text + "\n";

                //rng2.Text += "الفريق الأول" + "\t\t\t" + "الفريق الثاني" + "\t\t\t" + "الشاهد" + "\t\t\t" + "الشاهد" + "\t\t\t";
                //rng2.Text += "\n\n\n" + C_EndParagraph_textBox.Text + "\n\n";

                //word.Range rng3 = word_Paragraph.Range;
                //rng3.Font.Size = 14;
                //rng3.Font.Name = "Arial";
                //rng3.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphLeft;
                //rng3.Text = C_LawyerName_textBox.Text + "\n";

                getCurrentContractID();

                string destinationFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                word_Doc.SaveAs2(destinationFolderPath + "\\HopeCenter" + contract_ID.ToString() + ".docx");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            {
                //     word_Doc.Close();
                //     word_Obj.Quit();
            }
        }
        private void RefreshListView_button_Click(object sender, EventArgs e)
        {
            try
            {
                Payment_Notification_bind1(DateTime.Parse("01/01/3000"));
                ExeFile_Notification_bind1(DateTime.Parse("01/01/3000"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region select and convert image
        private void FileOpen_button_Click(object sender, EventArgs e)
        {
            try
            {
                myTh = new Thread(new ThreadStart(CallDialog));
                myTh.SetApartmentState(ApartmentState.STA);
                myTh.Start();
                myTh.Join();
                ImageLocation_textBox.Text = "";

                foreach (String fileName in FileNames)
                {
                    ImageLocation_textBox.Text += fileName + " , ";
                    pictureBox1.ImageLocation = fileName;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        OpenFileDialog open;
        string[] FileNames;
        private void CallDialog()
        {
            open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            open.Multiselect = true;
            DialogResult res = open.ShowDialog();
            if (res == DialogResult.OK)
            {
                FileNames = open.FileNames;
            }
        }
        private void Convert_Picture()
        {
            arr = null;
            FileStream fs = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            arr = br.ReadBytes((int)fs.Length);
        }
        #endregion

        private void calculate_RemainedAmount(int LoanID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select count(Pay_Amount) from `payment` where `Loan_ID` = " + LoanID + " and `Pay_IsPaid` like N'Paid'";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            int count = Convert.ToInt32(MySS.sc.ExecuteScalar());

            if (count != 0)
            {
                MySS.query = "select sum(`Pay_Amount`) from `payment` where `Loan_ID` = " + LoanID + " and `Pay_IsPaid` like N'Paid'";
                MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                Double Paid = Convert.ToDouble(MySS.sc.ExecuteScalar());
                Loan_RemainAmount_textBox.Text = (Loan_Amount - Paid).ToString();
            }
            else
            {
                Loan_RemainAmount_textBox.Text = Loan_Amount.ToString();
            }
            //set commas to the number
            Loan_RemainAmount_textBox.Text = Convert.ToDecimal(Loan_RemainAmount_textBox.Text).ToString("#,##0");
        }
        private void getCurrentContractID()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select MAX(`Co_ID`) from `contract` ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out contract_ID);
        }
        private void getCurrentPaymentID()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select MAX(`Pay_ID`) from `payment`  ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out Pay_ID);
        }
        private void getCurrentLoanID()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select MAX(`Loan_ID`) from `loan` ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out Loan_ID);
        }
        private void getCurrentExeFileID()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select MAX(`ExeF_ID`) from `exefile` ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out ExeFile_ID);
        }
        private string getPersonNameFromProjectID(int MP_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = " select CONCAT(P1.P_FirstName, ' ', P1.P_FatherName, ' ', P1.P_LastName) as 'Beneficiary Name'"
                            + " from person_microproject PMP left outer join `person` P1 on P1.P_ID = PMP.Person_ID "
                            + " where PMP.MicroProject_ID = " + MP_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            return MySS.sc.ExecuteScalar().ToString();
        }
        
        #region save - delete
        private void AddSave_button_MouseEnter(object sender, EventArgs e)
        {
            AddLoan_button.BackgroundImage = AddExeFile_button.BackgroundImage = 
                ImageSave_button.BackgroundImage = Properties.Resources.save;
            UpdateLoan_button.BackgroundImage =
                UpdateExeFile_button.BackgroundImage = Properties.Resources.save;
        }
        private void AddSave_button_MouseLeave(object sender, EventArgs e)
        {
            AddLoan_button.BackgroundImage = AddExeFile_button.BackgroundImage = 
                ImageSave_button.BackgroundImage = Properties.Resources.save0;
            UpdateLoan_button.BackgroundImage =
                UpdateExeFile_button.BackgroundImage = Properties.Resources.save0;
        }
        private void delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeletePayment_button.BackgroundImage = Properties.Resources.delete;
        }
        private void delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeletePayment_button.BackgroundImage =Properties.Resources.delete0;
        }
        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
        UpdatePayment_button.BackgroundImage = Properties.Resources.update;
        }
        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
           UpdatePayment_button.BackgroundImage = Properties.Resources.update0;
        }

        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            AddPayment_button.BackgroundImage = Properties.Resources.add;
        }
        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            AddPayment_button.BackgroundImage = Properties.Resources.add0;
        }
        private void Refresh_button_MouseEnter(object sender, EventArgs e)
        {
            RefreshAllNotification_button.BackgroundImage = Properties.Resources.refresh;
        }
        private void Refresh_button_MouseLeave(object sender, EventArgs e)
        {
            RefreshAllNotification_button.BackgroundImage = Properties.Resources.refresh0;
        }
        //tab btn clicks//
        private void LoansPayements_button_Enter(object sender, EventArgs e)
        {
            LoansPayements_button.BackgroundImage = Properties.Resources.blank;
        }
        private void LoansPayements_button_Leave(object sender, EventArgs e)
        {
            LoansPayements_button.BackgroundImage = null;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            LegalFinancialSection_Load(sender, e);
        }
        private void ExecutiveFile_button_MouseEnter(object sender, EventArgs e)
        {
            ExecutiveFile_button.BackgroundImage = Properties.Resources.blank;
        }
        private void ExecutiveFile_button_MouseLeave(object sender, EventArgs e)
        {
            ExecutiveFile_button.BackgroundImage = null;
        }
        private void Attachments_button_MouseEnter(object sender, EventArgs e)
        {
            Attachments_button.BackgroundImage = Properties.Resources.blank;
        }
        private void Attachments_button_MouseLeave(object sender, EventArgs e)
        {
            Attachments_button.BackgroundImage = null;
        }
        private void Notifications_button_MouseEnter(object sender, EventArgs e)
        {
            Notifications_button.BackgroundImage = Properties.Resources.blank;
        }
        private void Notifications_button_MouseLeave(object sender, EventArgs e)
        {
            Notifications_button.BackgroundImage = null;
        }
        #endregion save - delete

        private void ExeF_ImpoundDate_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ExeF_ImpoundDate_checkBox.Checked)
                ExeF_ImpoundDate_dateTimePicker.Enabled = true;
            else
                ExeF_ImpoundDate_dateTimePicker.Enabled = false;
        }

        private void MainBack0_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AllExeFiles_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Form 1
                using (AllExeFiles AllExeFiles = new AllExeFiles(username))
                {
                    if (AllExeFiles.ShowDialog() == DialogResult.OK)
                    {
                        fill_ExeFiles_boxes(AllExeFiles.SelectedDataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AllAttachments_button_Click(object sender, EventArgs e)
        {
            try
            {

                using (AllAttachemnts AllAttachemnts = new AllAttachemnts(username))
                {
                    if (AllAttachemnts.ShowDialog() == DialogResult.OK)
                    {
                        //fill_Image_boxes(AllAttachemnts.SelectedDataRow);
                        clear_Images_boxes();
                        Image_bind(MicroProject_ID.ToString());
                    }
                    Image_bind(MicroProject_ID.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AllLoansAndPayments_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (AllLoansPayments AllLoansPayments = new AllLoansPayments(username))
                {
                    if (AllLoansPayments.ShowDialog() == DialogResult.OK)
                    {
                        fill_LoansPayments_boxes(AllLoansPayments.SelectedDataRow);
                        UpdateLoan_button.Visible = true;
                        AddLoan_button.Visible = false;
                    }
                    else
                    {
                        clear_LoansPayments_boxes();
                        UpdateLoan_button.Visible = false;
                        AddLoan_button.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Loan_Amount_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Loan_Amount_textBox.Text.ToString() != "")
                {
                    //Loan_Amount_textBox.Text = Convert.ToDecimal(Loan_Amount_textBox.Text.ToString()).ToString("#,##0");
                    Loan_Amount_textBox.Text =
                     Regex.Replace(String.Format("{0:n" + 4 + "}", Convert.ToDecimal(Loan_Amount_textBox.Text.ToString())),
                         @"[" + System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    Loan_Amount_textBox.SelectionStart = Loan_Amount_textBox.Text.Length;
                    Loan_Amount_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Pay_Amount_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Pay_Amount_textBox.Text.ToString() != "")
                {
                    Pay_Amount_textBox.Text =
                     Regex.Replace(String.Format("{0:n" + 4 + "}", Convert.ToDecimal(Pay_Amount_textBox.Text.ToString())),
                         @"[" + System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    Pay_Amount_textBox.SelectionStart = Pay_Amount_textBox.Text.Length;
                    Pay_Amount_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}