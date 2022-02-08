using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace MyWorkApplication.Visit_Forms
{
    public class Visit
    {
        private string query;

        public Visit()
        {
            query = "";
        }

        public void Insert_Visit(int MicroProject_ID, string Kind, string Type, DateTime Date,
            string Ans1, string Ans2, string Ans3, string Ans4, string Ans5, string Ans6, string Ans7, string Indicators
            , int Ans1_Value, int Ans2_Value, int Ans3_Value
            , int Ans4_Value, int Ans5_Value, int Ans6_Value, int Ans7_Value )
        {
            Program.buildConnection();
            query =
                "INSERT INTO `Visit`(`MicroProject_ID`, `Kind`, `Type`, `Date`" +
                ", `Ans1`, `Ans2`, `Ans3`, `Ans4`, `Ans5`, `Ans6`,`Ans7`, `Indicators`" +
                ", `Ans1_Value`, `Ans2_Value`, `Ans3_Value`, `Ans4_Value`, `Ans5_Value`, `Ans6_Value`, `Ans7_Value`" +
                ", `Created_By` ) VALUES ("
                + MicroProject_ID + "," +
                "N'" + Kind + "'," +
                "N'" + Type + "'," +
                "N'" + Date.Year + "/" + Date.Month + "/" + Date.Day + "'," +
                "N'" + Ans1 + "'," +
                "N'" + Ans2 + "'," +
                "N'" + Ans3 + "'," +
                "N'" + Ans4 + "'," +
                "N'" + Ans5 + "'," +
                "N'" + Ans6 + "'," +
                "N'" + Ans7 + "'," +
                "N'" + Indicators + "',"
                + Ans1_Value + "," + Ans2_Value + "," + Ans3_Value + ","
                + Ans4_Value + "," + Ans5_Value + "," + Ans6_Value + "," + Ans7_Value + ","
                + "'" + Properties.Settings.Default.username + "' )";

            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }
        
        

        public void Update_Visit(int ID, string Type, DateTime Date,
            string Ans1, string Ans2, string Ans3, string Ans4, string Ans5, string Ans6,string Ans7, string Indicators
            , int Ans1_Value, int Ans2_Value, int Ans3_Value
            , int Ans4_Value, int Ans5_Value, int Ans6_Value, int Ans7_Value)
        {
            Program.buildConnection();
            query = "update `Visit` set " +
                    " `Type`=N'" + Type + "'" +
                    ",`Date`=N'" + Date.Year + "/" + Date.Month + "/" + Date.Day + "'" +
                    ",`Ans1`=N'" + Ans1 + "'" + ",`Ans2`=N'" + Ans2 + "'" +
                    ",`Ans3`=N'" + Ans3 + "'" + ",`Ans4`=N'" + Ans4 + "'" +
                    ",`Ans5`=N'" + Ans5 + "'" + ",`Ans6`=N'" + Ans6 + "'" +
                    ",`Ans7`=N'" + Ans7 + "'" +
                    ",`Indicators`=N'" + Indicators + "'" +
                    ",`Ans1_Value`= " + Ans1_Value + ",`Ans2_Value`= " + Ans2_Value +
                    ",`Ans3_Value`= " + Ans3_Value + ",`Ans4_Value`= " + Ans4_Value +
                    ",`Ans5_Value`= " + Ans5_Value + ",`Ans6_Value`= " + Ans6_Value +
                    ",`Ans7_Value`= " + Ans7_Value +
                    ",`Edited_By`='" + Properties.Settings.Default.username + "'" +
                    " where `ID` = " + ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Delete_Visit(int ID)
        {
            Program.buildConnection();
            query = "delete from `Visit` where `ID` = " + ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public int Get_Current_Visit(string Kind)
        {
            var V_ID = -1;
            Program.buildConnection();
            query = "select MAX(ID) from `Visit` where `Kind` like '" + Kind + "'";
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                int.TryParse(sc.ExecuteScalar().ToString(), out V_ID);
                Program.MyConn.Close();
            }

            return V_ID;
        }

        public void Insert_Visit_User(int V_ID, int User_ID)
        {
            Program.buildConnection();
            query = "INSERT INTO `visit_user`(`Visit_ID`, `User_ID`) values("
                    + V_ID + ","
                    + User_ID + ")";

            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Delete_Visit_Users(int V_ID)
        {
            Program.buildConnection();
            query = "delete from `visit_user` where `Visit_ID` = " + V_ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public DataTable Get_Visit_Users(int V_ID)
        {
            Program.buildConnection();
            query = "SELECT `User_ID`,`UserName` FROM `visit_user` join `user` on user.UserID = visit_user.User_ID"
                    + " WHERE `Visit_ID` = " + V_ID + "";
            var da = new MySqlDataAdapter(query, Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable Get_Visit(string V_ID, string name, string MicroProject_ID,string _Kind)
        {
            Program.buildConnection();
            query = @"SELECT PMP.`MicroProject_ID` as 'MicroProject_ID'
, PMP.Person_ID as 'Beneficiary_ID'
, CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'
, microproject.MP_Name as 'Project Name'
, P_Mobile as 'Mobile'
, L.Loan_Amount as 'Loan Amount'
, L.Loan_DateTaken as 'Loan Date'
, v.ID as 'ID'
, MP_Street_ID 
, (case when (street.Name like 'متنقل') then
     street.Name
     else CONCAT(street.Name,' - ',MP_AddressAfterFund) END ) as 'Address'
 
, `Type` , `Date` , `Kind`
, `Ans1`, `Ans2`, `Ans3`, `Ans4`, `Ans5`, `Ans6`, `Ans7`
, `Indicators`
, `Ans1_Value`, `Ans2_Value`, `Ans3_Value`, `Ans4_Value`, `Ans5_Value`, `Ans6_Value`, `Ans7_Value`
, Created_By, Edited_By ";

            string condition = " where 1 ";
            string str = "";
            string from = @" FROM person_microproject PMP 
 left join microproject on PMP.MicroProject_ID = microproject.MP_ID
 left join person P on P.P_ID = PMP.Person_ID
 left join loan L on L.MicroProject_ID = microproject.MP_ID 
 left join visit v on PMP.MicroProject_ID = v.MicroProject_ID 
 left join street on street.ID =  microproject.MP_Street_ID ";

            if (V_ID != "") condition += " and v.ID = " + V_ID + ""; 
            if (name != "") condition+= " and CONCAT(TRIM(P_FirstName),' ', TRIM(P_LastName),' ابن/ة ',TRIM(P_FatherName)) LIKE '%" + name + "%'";
            if (MicroProject_ID != "") condition += " and PMP.MicroProject_ID = " + MicroProject_ID;
            if (_Kind != "") condition += " and Kind like '" + _Kind + "' ";
             
            query += from + condition;

            var da = new MySqlDataAdapter(query, Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        } 
         
        public bool Has_Visit(string name, string MicroProject_ID, string _Kind)
        {
            var V_count = 0; 

            Program.buildConnection();
            query = @"SELECT count(*) ";

            string condition = " where 1 ";
            string str = "";
            string from = @" FROM visit v 
 left join person_microproject PMP on PMP.MicroProject_ID = v.MicroProject_ID
 left join microproject on PMP.MicroProject_ID = microproject.MP_ID
 left join person P on P.P_ID = PMP.Person_ID ";
             
            if (name != "") condition += " and CONCAT(TRIM(P_FirstName),' ', TRIM(P_LastName),' ابن/ة ',TRIM(P_FatherName)) LIKE '%" + name + "%'";
            if (MicroProject_ID != "") condition += " and PMP.MicroProject_ID = " + MicroProject_ID;
            if (_Kind != "") condition += " and Kind like '" + _Kind + "' ";

            query += from + condition;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                int.TryParse(sc.ExecuteScalar().ToString(), out V_count);
                Program.MyConn.Close();
            }
            if (V_count == 0) return false;
            else return true;
        }

        public DataTable GetVisitsOfBeneficiary(string MP_ID, string Name)
        {
            Program.buildConnection();
            var query = @"select MP.MP_ID as 'MicroProject_ID'
, CONCAT(P_FirstName, ' ', P_LastName, ' ابن/ة ', P_FatherName) as 'Beneficiary Name'
, PMP.Person_ID as 'Person_ID'
, MP.MP_Category_ID as 'Category_ID' 
, V.Date as 'Date'
, V.Kind as 'Kind'
, V.ID as 'V_ID'";

            var from = @"from microproject MP
left outer join person_microproject PMP on MP.MP_ID = PMP.MicroProject_ID
left outer join person P1 on P1.P_ID = PMP.Person_ID
left outer join visit V on PMP.MicroProject_ID = V.MicroProject_ID";

            var condition = "";
            if (Name != "")
                condition +=
                    " Where CONCAT(TRIM(P1.P_FirstName),' ', TRIM(P1.P_LastName),' ابن/ة ',TRIM(P1.P_FatherName)) LIKE '%" +
                    Name + "%'";
            else if (MP_ID != "")
                condition += " Where PersonType like 'مستفيد' and MP.MP_ID = " + Convert.ToInt32(MP_ID);
            query += from + condition;

            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataSet Get_Visits_Statistics(string FromDate, string ToDate)
        {
            Program.buildConnection();
            MySqlCommand sc = new MySqlCommand();
            sc.Parameters.AddWithValue("@FromDate", FromDate);
            sc.Parameters.AddWithValue("@ToDate", ToDate);
            string query = " CALL `visit_user_statistics`(@FromDate, @ToDate); ";
            sc.CommandText = query;
            sc.Connection = Program.MyConn;
            MySqlDataAdapter da = new MySqlDataAdapter(sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Program.MyFirstConn.Close();
            return ds;
        }
         
    }
}