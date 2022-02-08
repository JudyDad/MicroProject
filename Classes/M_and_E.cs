using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace MyWorkApplication.Visit_Forms
{
    public class M_and_E
    {
        private string query;

        public M_and_E()
        {
            query = "";
        }

        public void Insert_Visit(int Person_ID, string Number, string Kind, string Type, DateTime Date, double Profit,
            string Ans1, string Ans2, string Indicators, string Notes)
        {
            Program.buildConnection();
            query =
                "INSERT INTO `mevisit`(`Person_ID`,`Number`, `Kind`, `Type`, `Date`, `Profit`, `Ans1`, `Ans2`" +
                ", `Indicators`, `Notes`,`Result`, Created_By) VALUES  ("
                + Person_ID + ",'"
                + Number + "',N'"
                + Kind + "',N'"
                + Type + "',N'"
                + Date.Year + "/" + Date.Month + "/" + Date.Day + "',"
                + Profit + ",N'"
                + Ans1 + "',N'"
                + Ans2 + "',N'"
                + Indicators + "',N'"
                + Notes + "',"
                + 0 + ",'"+ Properties.Settings.Default.username + "')";
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Update_Visit(int ID, string Type, DateTime Date, int Profit,
            string Ans1, string Ans2, string Indicators, string Notes)
        {
            Program.buildConnection();
            query = "update `mevisit` set " +
                    " `Type`=N'" + Type + "'" +
                    ",`Profit`= " + Profit + " " +
                    ",`Date`=N'" + Date.Year + "/" + Date.Month + "/" + Date.Day + "'" +
                    ",`Ans1`=N'" + Ans1 + "'" +
                    ",`Ans2`=N'" + Ans2 + "'" +
                    ",`Indicators`=N'" + Indicators + "'" +
                    ",`Notes`=N'" + Notes + "'" +
                    ",Edited_By = N'"+ Properties.Settings.Default.username + "'" +
                    " where `ID` = " + ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Update_Visit(DateTime Date, int Person_ID, int Profit)
        {
            Program.buildConnection();
            query = "update `mevisit` set  `Profit`= " + Profit + " " +
                    " where Person_ID = " + Person_ID + " and `Date`=N'" + Date.Year + "/" + Date.Month + "/" +
                    Date.Day + "'";
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Update_VisitResult(int ID, int Result)
        {
            Program.buildConnection();
            query = "update `mevisit` set Result = " + Result + " where `ID` = " + ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Delete_Visit(int ID)
        {
            Program.buildConnection();
            query = "delete from `mevisit` where `ID` = " + ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public int Get_Current_Visit(string Kind, string Number)
        {
            var V_ID = -1;
            Program.buildConnection();
            query = "select MAX(ID) from `mevisit` where `Kind` like '" + Kind + "' and Number = '" + Number + "'";
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                int.TryParse(sc.ExecuteScalar().ToString(), out V_ID);
                Program.MyConn.Close();
            }

            return V_ID;
        }

        public void Insert_Visit_User(int V_ID, int User_ID, string Mark)
        {
            Program.buildConnection();
            query = "INSERT INTO `mevisit_user`(`Visit_ID`, `User_ID`,`Mark`) values("
                    + V_ID + ","
                    + User_ID + ",N'"
                    + Mark + "' )";

            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Delete_Visit_Users(int V_ID)
        {
            Program.buildConnection();
            query = "delete from `mevisit_user` where `Visit_ID` = " + V_ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public DataTable Get_Visit_Users(int V_ID)
        {
            Program.buildConnection();
            query =
                "SELECT `User_ID`,`UserName`,`Mark` FROM `mevisit_user` join `user` on user.UserID = mevisit_user.User_ID"
                + " WHERE `Visit_ID` = " + V_ID + "";
            var da = new MySqlDataAdapter(query, Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable Get_Visit(int V_ID)
        {
            Program.buildConnection();
            query = @"SELECT v.Person_ID as 'Beneficiary_ID'
, person_microproject.MicroProject_ID as 'MicroProject_ID'
, CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'
, Loan_Amount as 'Loan Amount' 
, Loan_DateTaken as 'Loan Date'
, microproject.MP_Name as 'Project Name'
, P_Mobile as 'Mobile'
, MP_Street_ID 
, (case when (street.Name like 'متنقل') then
     street.Name
     else CONCAT(street.Name,' - ',MP_AddressAfterFund) END ) as 'Address'
, `Type`
, `Number`
, `Profit`
, `Date`
, `Kind`
, `Ans1`, `Ans2`, `Indicators` , `Notes` 
, Created_By , Edited_By

 FROM mevisit v
 left join person_microproject on person_microproject.Person_ID = v.Person_ID
 left join microproject on person_microproject.MicroProject_ID = microproject.MP_ID
 left join person P on P.P_ID = person_microproject.Person_ID
 left join loan on loan.MicroProject_ID = person_microproject.MicroProject_ID
 left join street on street.ID =  microproject.MP_Street_ID
 where v.ID = " + V_ID + "";
            var da = new MySqlDataAdapter(query, Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable SearchByName(string name)
        {
            //check connection// 
            Program.buildConnection();
            var query = @"select PMP.MicroProject_ID as 'MicroProject_ID'
, CONCAT(P_FirstName, ' ', P_LastName, ' ابن/ة ', P_FatherName) as 'Beneficiary Name'
, PMP.Person_ID as 'Beneficiary_ID'
, L.Loan_Amount as 'Loan Amount'
, L.Loan_DateTaken as 'Loan Date'
, V.ID as 'ID'
,`Kind`, `Profit`, `Number`
,`Type`, `Date`, `Ans1`, `Ans2`, `Indicators` , `Notes`";

            var from = @" from person_microproject PMP left outer join person P1 on P1.P_ID = PMP.Person_ID
    left outer join loan L on PMP.MicroProject_ID = L.MicroProject_ID
    left outer join mevisit V on PMP.Person_ID = V.Person_ID";

            var Condition =
                " WHERE CONCAT(TRIM(P1.P_FirstName),' ', TRIM(P1.P_LastName),' ابن/ة ',TRIM(P1.P_FatherName)) LIKE '%" +
                name + "%'";
            //if (V_Num != "")
            //    Condition += " and Number like '" + V_Num + "' ";

            query += from + Condition;
            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable SearchByID(string MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();
            var query = @"select PMP.MicroProject_ID as 'MicroProject_ID'
, CONCAT(P_FirstName, ' ', P_LastName, ' ابن/ة ', P_FatherName) as 'Beneficiary Name'
, PMP.Person_ID as 'Beneficiary_ID'
,L.Loan_Amount as 'Loan Amount'
,L.Loan_DateTaken as 'Loan Date'
, V.ID as 'ID' 
,`Kind`, `Profit`, `Number`
,`Type`, `Date`, `Ans1`, `Ans2`, `Indicators` , `Notes` ";

            var from = @" from person_microproject PMP left outer join person P1 on P1.P_ID = PMP.Person_ID
    left outer join loan L on PMP.MicroProject_ID = L.MicroProject_ID
    left outer join mevisit V on PMP.Person_ID = V.Person_ID ";


            var Condition = " where PMP.MicroProject_ID = " + MicroProject_ID;
            //if (V_Num != "")
            //    Condition += " and Number like '" + V_Num + "' ";

            query += from + Condition;

            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        public DataTable Get_Questions_Answers(string type)
        {
            Program.buildConnection();
            query = @"SELECT mequestion.`Name` as 'Q_Name'
, mequestion.`Type` as 'Q_Type' 
, meanswer.`ID` as 'A_ID'
, meanswer.`Name` as 'A_Name'
, meanswer.`Mark` as 'A_Mark'
, `Question_ID` 
 from `mequestion` left join `meanswer` on mequestion.ID = meanswer.Question_ID ";

            var condition = "";
            if (type != "") condition = " where mequestion.`Type` like '%" + type + "%' ";
            query += condition + " order by A_ID";

            var da = new MySqlDataAdapter(query, Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable Get_Questions_Answers_SideBySide(string type,string visit_Num, string DateFrom, string DateTo, string beneficiary, string category,string subCategory)
        {
            
            //   Get the (last-first) visit of each funded beneficiary  
            string beneficiary_condtition = "";
            string orderBY = "";
            if (beneficiary == "all") { beneficiary_condtition = ""; orderBY = ""; }
            else
            {
                if (beneficiary == "last") orderBY = " ORDER BY mevisit.Date desc ";
                if (beneficiary == "first") orderBY = " ORDER BY mevisit.Date asc ";

                beneficiary_condtition =
                    " and mevisit_answer.Visit_ID in ( " +
                    " SELECT (SELECT mevisit.ID FROM mevisit WHERE mevisit.Person_ID = person_microproject.Person_ID " +
                    orderBY + " LIMIT 1) as 'V_ID' " +
                    " FROM microproject JOIN person_microproject on microproject.MP_ID = person_microproject.MicroProject_ID " +
                    " WHERE microproject.MP_State in (1, 4, 5) ) ";
            }
             
            string category_condtition = "";
            string subCategory_condtition = "";
            string visit_Number_condition = "";
            string Date_condition = "";

            if (category != "")
                category_condtition =
                   " and mevisit_answer.Visit_ID in ( " +
                   " SELECT (SELECT mevisit.ID FROM mevisit WHERE mevisit.Person_ID = person_microproject.Person_ID LIMIT 1) as 'V_ID' " +
                   " FROM microproject JOIN person_microproject on microproject.MP_ID = person_microproject.MicroProject_ID " +
                   " WHERE microproject.MP_Category_ID = (select C_ID from category where C_Name like '" + category + "' limit 1)) ";

            if (subCategory != "")
                subCategory_condtition =
                   " and mevisit_answer.Visit_ID in ( " +
                   " SELECT (SELECT mevisit.ID FROM mevisit WHERE mevisit.Person_ID = person_microproject.Person_ID LIMIT 1) as 'V_ID' " +
                   " FROM microproject JOIN person_microproject on microproject.MP_ID = person_microproject.MicroProject_ID " +
                   " WHERE microproject.SubCategory_ID = (select ID from subcategory where Name like '" + subCategory + "' limit 1)) ";
               
            if (visit_Num != "")
                visit_Number_condition = @" and mevisit.Number like '" + visit_Num + "' ";
             
            if(DateFrom != "" && DateTo != "")
                 Date_condition = @" and mevisit.Date between '" + DateFrom + "' and '" + DateTo + "' ";
            
            Program.buildConnection();
            //            query = @"SELECT mequestion.ID,Name
            //, (SELECT Name from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID ASC LIMIT 1) as 'ans1'
            //, IFNULL( (SELECT Name from meanswer WHERE meanswer.Question_ID = mequestion.ID 
            //   and meanswer.ID >  (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID ASC LIMIT 1) 
            //   and meanswer.ID <  (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID DESC LIMIT 1)  
            //   LIMIT 1) ,'') as 'ans2'
            //, (SELECT Name from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID DESC LIMIT 1) as 'ans3'

            //, (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID ASC LIMIT 1) as 'ans1_ID'
            //, IFNULL( (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID
            //   and meanswer.ID > (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID ASC LIMIT 1)
            //   and meanswer.ID < (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID DESC LIMIT 1)  
            //   LIMIT 1) ,'' ) as 'ans2_ID'
            //, (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID DESC LIMIT 1) as 'ans3_ID'" +

            //",(SELECT count(*) from `mevisit_answer` left join `mevisit` on mevisit.ID = mevisit_answer.Visit_ID where mevisit_answer.Question_ID = mequestion.ID and mevisit_answer.Answer_ID = ans1_ID " + Date_condition + beneficiary_condtition + visit_Number_condition + category_condtition + subCategory_condtition + " ) as 'ans1_count'" +
            //",(SELECT count(*) from `mevisit_answer` left join `mevisit` on mevisit.ID = mevisit_answer.Visit_ID where mevisit_answer.Question_ID = mequestion.ID and mevisit_answer.Answer_ID = ans2_ID " + Date_condition + beneficiary_condtition + visit_Number_condition + category_condtition + subCategory_condtition + " ) as 'ans2_count'" +
            //",(SELECT count(*) from `mevisit_answer` left join `mevisit` on mevisit.ID = mevisit_answer.Visit_ID where mevisit_answer.Question_ID = mequestion.ID and mevisit_answer.Answer_ID = ans3_ID " + Date_condition + beneficiary_condtition + visit_Number_condition + category_condtition + subCategory_condtition + " ) as 'ans3_count'" +

            //",(SELECT count(*) from mevisit_answer left join mevisit on mevisit.ID = mevisit_answer.Visit_ID where mevisit_answer.Question_ID = mequestion.ID " + Date_condition + beneficiary_condtition + visit_Number_condition+  category_condtition + subCategory_condtition +" ) as 'all_ans_count'";

            //            var from = " FROM `mequestion` ";
            //            var condition = " where 1 ";
            //            if (type != "") condition += " and mequestion.`Type` like '%" + type + "%' ";  
            //            query += from + condition + " order by ID";

            //var da = new MySqlDataAdapter(query, Program.MyConn);
            //var dt = new DataTable();
            //da.Fill(dt);
            //Program.MyConn.Close();
            //return dt;

            string all_conditions = "";
            all_conditions = Date_condition + beneficiary_condtition + visit_Number_condition + category_condtition + subCategory_condtition;
 
            MySqlCommand sc = new MySqlCommand("mevisit_results", Program.MyConn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.AddWithValue("@MyCondition", all_conditions);
            sc.Parameters.AddWithValue("@MyType",type); 
            using (MySqlDataAdapter sda = new MySqlDataAdapter(sc))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt); 
                Program.MyConn.Close();
                return dt;
            }
        }

        public DataTable Get_Beneficaies(string Ans_IDs, string visitNumber, string DateFrom, string DateTo, string beneficiary, string category, string subCategory)
        {
            //TODO
            //   Get the (last-first) visit of each funded beneficiary  
            string beneficiary_condtition = "";
            string orderBY = "";
            if (beneficiary == "all") { beneficiary_condtition = ""; orderBY = ""; }
            else
            {
                if (beneficiary == "last") orderBY = " ORDER BY mevisit.Date desc ";
                if (beneficiary == "first") orderBY = " ORDER BY mevisit.Date asc ";

                beneficiary_condtition =
                    " and mevisit_answer.Visit_ID in ( " +
                    " SELECT (SELECT mevisit.ID FROM mevisit WHERE mevisit.Person_ID = person_microproject.Person_ID " +
                    orderBY + " LIMIT 1) as 'V_ID' " +
                    " FROM microproject JOIN person_microproject on microproject.MP_ID = person_microproject.MicroProject_ID " +
                    " WHERE microproject.MP_State in (1, 4, 5) ) ";
            }
              
            Program.buildConnection();
            query = @"SELECT person_microproject.MicroProject_ID as 'رقم المشروع'
, CONCAT(P_FirstName, ' ', P_LastName) as 'المستفيد'
, mevisit.Number as 'رقم الزيارة'
 FROM mevisit_answer 
 join `mevisit` on mevisit.ID = mevisit_answer.Visit_ID
 join person on person.P_ID = mevisit.Person_ID
 join person_microproject on person_microproject.Person_ID =  mevisit.Person_ID
 join microproject on person_microproject.MicroProject_ID =  microproject.MP_ID ";

            string condition = " WHERE " + Ans_IDs ;
            if (visitNumber != "")
                condition += " and mevisit.Number like '" + visitNumber + "' ";

            if (DateFrom != "" && DateTo != "")
                condition += @" and mevisit.Date between '" + DateFrom + "' and '" + DateTo + "' ";

            if (category != "")
                condition += " and microproject.MP_Category_ID = (select C_ID from category where C_Name like '" + category + "' limit 1) ";

            if (subCategory != "")
                condition += " and microproject.SubCategory_ID = (select ID from subcategory where Name like '" + subCategory + "' limit 1) ";

            string orderBy = " order by MicroProject_ID ";

            query += condition + orderBy;

            var da = new MySqlDataAdapter(query, Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }
         
        public void Insert_Visit_Answers(int Visit_ID, int Question_ID, int Answer_ID, string Notes)
        {
            Program.buildConnection();
            query = "INSERT INTO `mevisit_answer`(`Visit_ID`, `Question_ID`, `Answer_ID`,`Notes`) VALUES ("
                    + Visit_ID + ","
                    + Question_ID + ","
                    + Answer_ID + ",N'"
                    + Notes + "' )";

            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Delete_Visit_Answers(int V_ID)
        {
            Program.buildConnection();
            query = "delete from `mevisit_answer` where `Visit_ID` = " + V_ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public DataTable Get_Visit_Answers(int V_ID, string Type)
        {
            Program.buildConnection();
            query = @"SELECT `Visit_ID`, `Question_ID`, `Answer_ID` , `Notes`
 FROM mevisit_answer left join mequestion on mevisit_answer.Question_ID = mequestion.ID ";

            var condition = " where Visit_ID = " + V_ID + " ";
            if (Type != "")
                condition += " and Type like '" + Type + "' ";

            query += condition;
            var da = new MySqlDataAdapter(query, Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable GetVisitsOfBeneficiary(string MP_ID, string Name)
        {
            Program.buildConnection();
            var query = @"select MP.MP_ID as 'MicroProject_ID'
, CONCAT(P_FirstName, ' ', P_LastName, ' ابن/ة ', P_FatherName) as 'Beneficiary Name'
, PMP.Person_ID as 'Person_ID'
, MP.MP_Category_ID as 'Category_ID'
, V.Kind as 'Kind' 
, V.Date as 'Date'
, V.Number as 'Number'
, V.ID as 'V_ID'";

            var from = @"from microproject MP
left outer join person_microproject PMP on MP.MP_ID = PMP.MicroProject_ID
left outer join person P1 on P1.P_ID = PMP.Person_ID
left outer join mevisit V on PMP.Person_ID = V.Person_ID";

            var condition = " where 1 ";
            if (Name != "")
                condition +=
                    " and CONCAT(TRIM(P1.P_FirstName),' ', TRIM(P1.P_LastName),' ابن/ة ',TRIM(P1.P_FatherName)) LIKE '%" +
                    Name + "%'";
            if (MP_ID != "")
                condition += " and MP.MP_ID = " + Convert.ToInt32(MP_ID);
            query += from + condition + " order by V.Date asc ";

            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }
    }
}