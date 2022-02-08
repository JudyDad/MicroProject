using MySql.Data.MySqlClient;
using System;
using System.Data.SqlTypes;

namespace MyWorkApplication.Classes
{
    public class MicroProject
    {
        private string query;


        public MicroProject()
        {
            query = "";
        }

        public void Update_Project_State(int MicroProject_ID, string State, string MP_StateDate)
        { 
            query = " Update `microproject` set " +
                    " MP_State = (select ID from `state` where Name_ar like N'" + State + "')" +
                    ",MP_StateDate = '" + MP_StateDate + "' " +
                    " where MP_ID = " + MicroProject_ID + ";";

            if (State == "منتهي" || State == "منسحب" || State == "ملغى")
                query += "UPDATE user_notification set `Seen` = 1 WHERE user_notification.MicroProject_ID = " + MicroProject_ID + ";";
             
           Program.buildConnection();
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }
          
        public void Update_Project(int MicroProject_ID,string property, string value)
        {
            query = "Update `microproject` set " + property + " = " + value + " where MP_ID = " + MicroProject_ID;
            if (value == "-1") //insert null
                query = "Update `microproject` set " + property + " = " + SqlInt32.Null + " where MP_ID = " + MicroProject_ID; 

            Program.buildConnection();
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Update_Micro_VisitDetails(int MicroProject_ID, string isVisited, string Visit_Date, string Visit_Time
            , string Visit_Notes, string MP_ParishNotes, string MP_KeyPersonNotes, string Team_Date
            , int ch_HouseTechCondition, int ch_ProjectExperience, int ch_ExpectedProjectSuccess, int ch_WorkAbility)
        {
            query = "Update `microproject` set " +
                " MP_ParishNotes = '" + MP_ParishNotes + "' " +
                ",MP_KeyPersonNotes = '" + MP_KeyPersonNotes + "' " +
                ",MP_TeamDate =  " + (Team_Date == "" ? "NULL" : "'" + Team_Date + "' ") +
                ",MP_VisitDate = " + (Visit_Date == "" ? "NULL" : "'" + Visit_Date + "' ") +
                ",MP_VisitTime = '" + Visit_Time + "' " +
                ",MP_VisitNotes = N'" + Visit_Notes + "' " +

                ",ch_HouseTechCondition = " + ch_HouseTechCondition + " " +
                ",ch_ProjectExperience =  " + ch_ProjectExperience + " " +
                ",ch_ExpectedProjectSuccess = " + ch_ExpectedProjectSuccess + " " +
                ",ch_WorkAbility = " + ch_WorkAbility + " "  ;

            if (isVisited != "")
                query += ",MP_Visited = " + isVisited + " ";

            query += " where MP_ID = " + MicroProject_ID;

            Program.buildConnection();
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }


        public void Update_MP_Place(int MP_ID, int MP_Street_ID,string MP_AddressAfterFund)
        {
            var query = " Update `microproject` set " 
                + " MP_Street_ID = " + (MP_Street_ID == -1 ? SqlInt32.Null : MP_Street_ID)
                + ",MP_AddressAfterFund = N'" + MP_AddressAfterFund + "'" 
                + " where MP_ID = " + MP_ID;

            //check connection//
            Program.buildConnection();
            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            Program.MyConn.Close();
        }

    }
}