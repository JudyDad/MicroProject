using MySql.Data.MySqlClient;

namespace MyWorkApplication.Classes
{
    internal class UpdateQueries
    {
        private string query;

        public UpdateQueries()
        {
            query = "";
        }

        public void Update_Material(int M_ID, string name, int amount, double price, double local, string comment)
        {
            //check connection//
            Program.buildConnection();
            query = "UPDATE `material` SET "
                    + " `Name`= N'" + name + "'"
                    + ",`Amount`= " + amount + ""
                    + ",`Price`= " + price + ""
                    + ",`LocalContribution`= " + local + ""
                    + ",`Comments`= N'" + comment + "'"
                    + " WHERE `ID`= " + M_ID + "";
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Update_Person_Education(int PEdu_ID, int Education_ID)
        {
            //check connection//
            Program.buildConnection();
            query = "UPDATE `person_education` SET "
                    + " `Education_ID`= " + Education_ID + ""
                    + " WHERE `PEdu_ID`= " + PEdu_ID + "";
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Update_Person_WorkExp(int PW_ID, int Work_ID, string W_Description, string W_BeginDate,
            string W_EndDate, string W_Place, string W_PlaceState, string W_CauseOfLoseDescription, double W_Salary)
        {
            //check connection//
            Program.buildConnection();
            query = "UPDATE `person_workexp` SET "
                    + " `Work_ID`= " + Work_ID + ""
                    + ",`W_Description`= N'" + W_Description + "'"
                    + ",`W_BeginDate`= N'" + W_BeginDate + "'"
                    + ",`W_EndDate`= N'" + W_EndDate + "'"
                    + ",`W_Place`= N'" + W_Place + "'"
                    + ",`W_PlaceState`= N'" + W_PlaceState + "'"
                    + ",`W_CauseOfLoseDescription`= N'" + W_CauseOfLoseDescription + "'"
                    + ",`W_Salary`= " + W_Salary + ""
                    + " WHERE `PW_ID`= " + PW_ID + "";
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Update_Person_FMember(int P_ID, string P_FirstName, string P_LastName, string P_DOB)
        {
            //check connection//
            Program.buildConnection();
            query = "UPDATE `person` SET "
                    + " `P_FirstName`= N'" + P_FirstName + "'"
                    + ",`P_LastName`= N'" + (P_LastName != "" ? P_LastName : "") + "'"
                    + ",`P_DOB` = N'" + P_DOB + "/" + 01 + "/" + 01 + "'"
                    + " WHERE `P_ID`= " + P_ID + "";
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Update_Person_FMember_Details(int Member_ID, int F_ID, string Relation, string Work_Name)
        {
            //check connection//
            Program.buildConnection();
            query = "UPDATE `person_family` SET "
                    + " `Relation`= N'" + Relation + "'"
                    + ",`Work_Name`= N'" + Work_Name + "'"
                    + " WHERE `Person_ID` = " + Member_ID + " and `Family_ID` = " + F_ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }
    }
}