using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace MyWorkApplication.Classes
{
    public class UserNotification
    {
        private readonly MySqlComponents MySS;

        public UserNotification()
        {
            MySS = new MySqlComponents();
        }

        public void Insert_UserNotification(DateTime Date, string Body, string P_Name, int MicroProject_ID, int User_ID,
            int Sender_ID, int Pay_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query =
                "Insert Into `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`,`Pay_ID`) values(N'"
                + Date.Year + "/" + Date.Month + "/" + Date.Day + "',N'"
                + Body + "',"
                + 0 + ",N'"
                + P_Name + "',"
                + MicroProject_ID + ","
                + User_ID + ","
                + Sender_ID + ","
                + Pay_ID + ")";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        public void Update_MicroUsers_Notification(int MicroProject_ID, string Date, string Body, string P_Name,string Seen)
        {
            //check connection//
            Program.buildConnection();
            string condition = " where 1 ";
            MySS.query = "update `user_notification` set `Seen` = " + Seen;
            if (MicroProject_ID != -1)
                condition += " and `MicroProject_ID` = " + MicroProject_ID;
            if(Date != "")
                condition +=" and `Date` = '" + Date + "'";
            if (Body != "")
                condition += " and `Body` like '" + Body + "'";
            if (P_Name != "")
                condition += " and `P_Name` like '" + P_Name + "'";

            MySS.query += condition;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        public void Update_NotificationWithPaymentID(int Pay_ID,  string Body, string Date, string Seen)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "update `user_notification` set `Seen` = " + Seen;
            if (Date != "") MySS.query += " ,`Date` = '" + Date + "'";
            if (Body != "") MySS.query += " ,`Body` = '" + Body + "'";

            MySS.query += " where `Pay_ID` = " + Pay_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        public void Update_NotificationW_ExpiredDateOfExeFile(int ExeFileID, DateTime Date, int Sender_ID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "update `user_notification` set " +
                         " Date = '" + Date.Year + "/" + Date.Month + "/" + Date.Day + "' " +
                         ",Sender_ID = " + Sender_ID +
                         " where `Body` like N'الملف التنفيذي' and `Pay_ID` = " + ExeFileID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        public void Update_UserNotification(int N_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "update `user_notification` set `Seen` = 1 where `ID` = " + N_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        public void Update_UserNotification(int MicroProject_ID, int User_ID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = " update `user_notification` set `Seen` = 1 " 
                + " where `MicroProject_ID` = " +  MicroProject_ID;
            if (User_ID != -1)
                MySS.query += " and `User_ID` = " + User_ID;

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

        public DataTable Select_VisitNotifications(string month, string year, string V_Kind, string V_number,string Street_ID, string seen)
        {
            //check connection//
            Program.buildConnection();
            var q = "select user_notification.`ID` as 'ID'" +
                    ", user_notification.`MicroProject_ID` as 'رقم المشروع' " +
                    ", `P_Name` as 'المستفيد'" +
                    ", `Date` as 'التاريخ'" +
                    ", `Body` as 'نوع الزيارة'" +
                    ", w_street.Name 'منطقة المشروع'" +
                    ", (select P_Mobile from person where person.P_ID = PMP.Person_ID limit 1) 'الموبايل'" +
                    @" from `user_notification` 
 left join `microproject` MP on MP.MP_ID = user_notification.MicroProject_ID
 left join `person_microproject` PMP on MP.MP_ID = PMP.MicroProject_ID
 LEFT join `street` w_street on w_street.ID = MP.MP_Street_ID ";

            var condition =
                " where `Pay_ID` = -21 and `User_ID` =  (select UserID from user where IsME = 1 limit 1) "; //28
            if (month != "") condition += " and Month(`Date`) like " + month;
            if (year != "") condition += " and Year(`Date`) like " + year;
            if (V_Kind != "") condition += " and Body like '%-" + V_Kind + "'"; 
            if (V_number != "") condition += " and Body like 'Visit " + V_number + "%'";
            if (Street_ID != "") condition += " and w_street.ID = " + Street_ID + " ";
            if (seen != "") condition += " and Seen = " + seen;
            q += condition + " order by Date asc ";

            var sc = new MySqlCommand(q, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }
    }
}