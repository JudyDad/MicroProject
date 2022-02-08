using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MyWorkApplication
{
    internal static class Program
    {
        public static MySqlConnection MyConn;

        //CHARSET=utf8 for recognizing Arabic in c#
        public static string ConStr_Online = ""; 
        public static string ConStr_Offline = "";
        public static string database = "";
        public static string ConnectionString = "";

        public static MySqlConnection MyFirstConn; //ADDED BY SARY
        public static List<string> User_In_Database; //ADDED BY SARY
        
        public static void set_ConnectionString() //CHANGED BY SARY
        {
            if (Properties.Settings.Default.selected_hope != "")
            {
                if (Properties.Settings.Default.selected_hope == "Aleppo")
                    database = "hcsyria_micro";
                else if (Properties.Settings.Default.selected_hope == "Homs")
                    database = "hcsyria_micro_h";
                else if (Properties.Settings.Default.selected_hope == "Damascus")
                    database = "hcsyria_micro_d";
                else if (Properties.Settings.Default.selected_hope == "Lebanon")
                    database = "hcsyria_micro_lb";
            }
            else database = ""; //datasource=PC-ADMIN //UID=localhost

            ConStr_Online = @"Server=mysql.s807.sureserver.com;Port=3306;Database=" + database +
                            ";Uid=judy;Password=DAda1994@" +
                            ";Convert Zero Datetime=True;CHARSET=utf8;Connect Timeout=21600;";
            ConStr_Offline = @"datasource=PC-ADMIN;username=jj;password=pp;database=" + database +
                             ";Convert Zero Datetime=True;CHARSET=utf8;Connect Timeout=21600;";

            if (Properties.Settings.Default.Online)  //online
                ConnectionString = ConStr_Online;
            else //offline
                ConnectionString = ConStr_Offline; 
        }

        public static bool get_ConnectionString()
        {
            var status = false;
            if (ConnectionString == ConStr_Online)
                status = true; //online
            else
                status = false; //offline
            return status;
        }

        public static bool buildConnection()
        {
            try
            {
                //System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() && 
                MyConn = new MySqlConnection(ConnectionString);
                if (MyConn.State == ConnectionState.Closed)
                {
                    MyConn.Open();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("MySql") || ex.Message.Contains("MySQL"))
                { MessageBox.Show("please check your network and try again", "Login Faild", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                else if (ex.Message.Contains("host"))
                { MessageBox.Show("please check the server or your network connection and try again", "Login Faild", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                else
                {
                    MessageBox.Show(ex.Message, "Login Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool firstbuildConnection()
        {
            try
            {
                MyFirstConn = new MySqlConnection(ConnectionString);
                if (MyFirstConn.State == System.Data.ConnectionState.Closed)
                {
                    MyFirstConn.Open();
                    return true;
                }
                else return false;
            }
            catch
            { 
                return false;
            }
        }

        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            var dgvType = dgv.GetType();
            var pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            Application.Run(new MainForm());
        }
    }
}