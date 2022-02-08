using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MyWorkApplication.Classes
{
    internal class Select
    {
        private byte[] arr;
        private string Loan_from, Loan_condition;
        private readonly MySqlComponents MySS;

        public Select()
        {
            MySS = new MySqlComponents();
        }

        #region family

        //public string Family_Num_bind(int Person_ID)
        //{
        //    //check connection//
        //                   Program.buildConnection();

        //    MySS.query = "select F.F_Number "
        //            + " From `person` p left outer join `person_family` pf on p.P_ID = pf.Person_ID "
        //   + " left outer join `family` F on F.F_ID = pf.Family_ID "
        //            + " where p.P_ID = " + Person_ID + " ";
        //    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
        //    string n = MySS.sc.ExecuteScalar().ToString() ;
        //    Program.MyConn.Close();
        //    return n;
        //}
        //public DataTable FamilyMembers_bind(int Family_ID)
        //{
        //    //check connection//
        //    Program.buildConnection();
        //    string query = "select p.P_ID as 'ID'" +
        //            ",p.P_FirstName as 'FirstName'" +
        //            ",p.P_LastName as 'LastName'" +
        //            ",p.P_MotherName as 'MotherName'" +
        //            ",p.P_FatherName as 'FatherName'" +
        //            ",p.P_NationalNumber as 'National Number'" +
        //            ",p.P_MaritalStatus as 'Marital Status'" +
        //            ",pf.IsInNow as 'IsInNow'" +
        //            ",pf.Relation as 'Relation'" +
        //            ",p.P_Sex as 'Gender'" +
        //            ",p.P_MaritalStatus as 'Marital Status'" +
        //            ",pf.Work_Name as 'Work'" +
        //            ",pf.P_Provider_ID as 'Provider_ID'" +
        //            ",p.P_DOB as 'Birth Date'" +
        //            " from `person` p left outer join `person_family` pf on p.P_ID = pf.Person_ID" +
        //            " where pf.Family_ID = " + Family_ID + " ";
        //    MySqlCommand sc1 = new MySqlCommand(query, Program.MyConn);
        //    sc1.ExecuteNonQuery();
        //    MySqlDataAdapter da = new MySqlDataAdapter(sc1);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    Program.MyConn.Close();
        //    return dt;
        //}
        public DataTable FamilyMembers_bind(int Family_ID, int P_ID)
        {
            //check connection//
            Program.buildConnection();
            var query = "select p.P_ID as 'ID'" +
                        ",p.P_FirstName as 'FirstName'" +
                        ",p.P_LastName as 'LastName'" +
                        ",p.P_MotherName as 'MotherName'" +
                        ",p.P_FatherName as 'FatherName'" +
                        ",p.P_NationalNumber as 'National Number'" +
                        ",p.P_MaritalStatus as 'Marital Status'" +
                        ",pf.IsInNow as 'IsInNow'" +
                        ",pf.Relation as 'Relation'" +
                        ",p.P_Sex as 'Gender'" +
                        ",p.P_MaritalStatus as 'Marital Status'" +
                        ",pf.Work_Name as 'Work'" +
                        ",pf.P_Provider_ID as 'Provider_ID'" +
                        ",p.P_DOB as 'Birth Date'" +
                        ",F.F_Number as 'Family Number' " +
                        ",pf.Family_ID as 'Family_ID'" +
                        " from `person` p " +
                        " left outer join `person_family` pf on p.P_ID = pf.Person_ID " +
                        " left outer join `family` F on F.F_ID = pf.Family_ID ";

            var condition = " where pf.P_Provider_ID = " + P_ID;
            if (Family_ID != -1)
                condition += " and pf.Family_ID = " + Family_ID;
            query += condition;

            var sc1 = new MySqlCommand(query, Program.MyConn);
            sc1.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc1);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        #endregion

        #region ExeFiles

        public DataTable ExeFile_bind(int ExeFile_ID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "select PMP.MicroProject_ID as 'MicroProject_ID'"
                         + " ,CONCAT(P_FirstName,' ', P_LastName,' ابن/ة ',P_FatherName) as 'Beneficiary Name'"
                         + " ,`ExeF_ID` as 'ID'"
                         + ",`ExeF_No` as 'Number'"
                         + ",`ExeF_BeginDate` as 'Begin Date'"
                         + ",`ExeF_CurrentDate` as 'Current Date'"
                         + ",`ExeF_ImpoundDate` as 'Impound Date'"
                         + ",`ExeF_ImpoundType` as 'Impound Type'"
                         + ",`ExeF_NumOfMonths` as 'Months'"
                         + " FROM `exefile` E right outer join  person_microproject PMP on E.MicroProject_ID = PMP.MicroProject_ID "
                         + " right outer join person P1 on P1.P_ID = PMP.Person_ID "
                         + " where ExeF_ID = " + ExeFile_ID + " and PMP.MicroProject_ID is not NULL ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Program.MyConn.Close();
            return MySS.dt;
        }

        #endregion

        public AutoCompleteStringCollection select_beneficiaries(string category, string Financed)
        {
            //check connection//
            Program.buildConnection();
            var where = "";
            if (category == "")
            {
                where = "";
                if (Financed == "Yes")
                    where += " where `MP_State` in (1,4,5) ";
            }
            else if (category.Contains("T"))
            {
                where = " where microproject.MP_Category_ID in (1, 2) ";
                if (Financed == "Yes")
                    where += " and `MP_State` in (1,4,5) ";
            }
            else
            {
                where = " where microproject.MP_Category_ID not in (1, 2) ";
                if (Financed == "Yes")
                    where += " and `MP_State` in (1,4,5) ";
            }


            var query = @"select CONCAT(P_FirstName,' ', P_LastName,' ابن/ة ',P_FatherName) as 'Beneficiary Name' 
 from microproject 
 join person_microproject on person_microproject.MicroProject_ID = microproject.MP_ID  
 join person on person.P_ID = person_microproject.Person_ID " + where;

            MySqlCommand mySqlCommand = new MySqlCommand(query, Program.MyConn);
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            while (reader.Read()) collection.Add(reader.GetString(0));
            return collection;
        }

        public AutoCompleteStringCollection select_project_IDs(string category, string Financed)
        {
            //check connection//
            Program.buildConnection();
            var where = "";
            if (category == "")
            {
                where = "";
                if (Financed == "Yes")
                    where += " where `MP_State` in (1,4,5) ";
            }
            else if (category.Contains("T"))
            {
                where = " where microproject.MP_Category_ID in (1, 2) ";
                if (Financed == "Yes")
                    where += " and `MP_State` in (1,4,5) ";
            }
            else
            {
                where = " where microproject.MP_Category_ID not in (1, 2) ";
                if (Financed == "Yes")
                    where += " and `MP_State` in (1,4,5) ";
            }

            var query = @"select MP_ID as 'MicroProject_ID' from microproject " + where;

            MySqlCommand mySqlCommand = new MySqlCommand(query, Program.MyConn);
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            while (reader.Read()) collection.Add(reader.GetString(0));
            return collection;
        }

        public DataTable select_visitors()
        {
            //check connection//
            Program.buildConnection();
            var query =
                @"select UserID as 'U_ID',UserName as 'Visitors' from `user` where IsVisitor = 1 order by UserName ASC";
            var da = new MySqlDataAdapter(query, Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable select_users()
        {
            //check connection//
            Program.buildConnection();
            var da = new MySqlDataAdapter(
                "select UserID as 'U_ID',UserName as 'Users' from `user` where IsVisitor != -1 order by UserName ASC",
                Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable select_M_E()
        {
            //check connection//
            Program.buildConnection();
            var da = new MySqlDataAdapter(" select UserID as 'U_ID',UserName as 'Users' from `user` where IsME = 1 ",
                Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable select_Communication()
        {
            //check connection//
            Program.buildConnection();
            var da = new MySqlDataAdapter(
                " select UserID as 'U_ID',UserName as 'Users' from `user` where IsCommunication = 1 ", Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        // to send the payments notifications must be with role financial OR isCashier = 1 //
        public DataTable select_Financial()
        {
            //check connection//
            Program.buildConnection();
            var da = new MySqlDataAdapter(
                " select UserID as 'U_ID',UserName as 'Users' from `user` where UserRoleID = 3 or IsCashier = 1 ", Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable select_Lawful()
        {
            //check connection//
            Program.buildConnection();
            var da = new MySqlDataAdapter(
                " select UserID as 'U_ID',UserName as 'Users' from `user` where UserRoleID = 7 ", Program.MyConn);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable select_Beneficiaries_of_project(string MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();
            string query = @"select Person_ID as 'ID'
 ,CONCAT(P_FirstName,' ', P_LastName,' ابن/ة ',P_FatherName) as 'Name'
  from person_microproject left join person on person.P_ID = person_microproject.Person_ID ";
            string condition = "";
            if (MicroProject_ID != "")
                condition += " where person_microproject.MicroProject_ID = " + MicroProject_ID;

            query += condition;

            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Program.MyConn.Close();
            return dt;
        }

        public int HasPartner(int MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "SELECT count(Person_ID) FROM `person_microproject` WHERE MicroProject_ID = " +
                         MicroProject_ID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            var count = Convert.ToInt32(MySS.sc.ExecuteScalar());
            Program.MyConn.Close();
            return count;
        }

        #region images

        public DataTable Image_bind(int MP_ID, string type)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "SELECT `Image_ID` " +
                         ",`Image_Path`" +
                         " FROM `image` " +
                         " Where `MicroProject_ID` = " + MP_ID + " and `Image_Type` like '" + type + "'";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);

            MySS.da = new MySqlDataAdapter(MySS.query, Program.MyConn);
            MySS.reader = MySS.sc.ExecuteReader();
            MySS.dt = new DataTable();
            MySS.dt.Load(MySS.reader);
            Program.MyConn.Close();

            return MySS.dt;
        }

        public byte[] Show_image(int image_ID)
        {
            //check connection//
            Program.buildConnection();

            byte[] arr = null;
            MySS.query = "select `Image_Content` from `image` where `Image_ID` = " + image_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.reader = MySS.sc.ExecuteReader();
            MySS.reader.Read();
            if (MySS.reader.HasRows) arr = (byte[]) MySS.reader[0];
            MySS.reader.Close();
            Program.MyConn.Close();
            return arr;
        }

        #endregion

        #region loan

        public DataTable Loan_bind(int MP_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select `Loan_ID` as 'ID'" +
                         ",`Loan_Amount` as 'Loan Amount'" +
                         ",`Loan_DateTaken` as 'Receive Date'" +
                         ",`Loan_PaymentsCount` as 'Payments Count'" +
                         "from `loan` where `MicroProject_ID` = " + MP_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            Program.MyConn.Close();
            return MySS.dt;
        }

        public DataTable Payments_bind(string month, string year, string group, string paid)
        { 
            var q = @"select 
 MP_ID as 'رقم المشروع' 
 ,(select CONCAT(P_FirstName,' ', P_LastName,' ابن/ة ',P_FatherName) from person 
 left join `person_microproject` on person_microproject.Person_ID = person.P_ID 
 where person_microproject.PersonType like 'مستفيد' AND loan.MicroProject_ID = person_microproject.MicroProject_ID LIMIT 1) as 'المستفيد' 

 ,IFNULL((select CONCAT(P_FirstName,' ', P_LastName,' ابن/ة ',P_FatherName) from person 
 left join `person_microproject` on person_microproject.Person_ID = person.P_ID 
 where person_microproject.PersonType like 'شريك' AND loan.MicroProject_ID = person_microproject.MicroProject_ID LIMIT 1),'') as 'الشريك'

,`Pay_Amount` as 'القسط' 
,`Pay_DueDate` as 'تاريخ الاستحقاق' 
,`Pay_RecievedOnDate` as 'تاريخ الدفع الفعلي' 
, donorgroup.Name as 'المجموعة'
,`Pay_ID` as 'Pay_ID'

 from `payment` left join `loan` on payment.Loan_ID = loan.Loan_ID  
 left join `microproject` on microproject.MP_ID = loan.MicroProject_ID  
 left join `donorgroup` on donorgroup.ID = microproject.DonorGroup_ID";

            var filtered_col = "Pay_DueDate"; 
            if (paid == "Paid") filtered_col = "Pay_RecievedOnDate";
            else filtered_col = "Pay_DueDate";

            var condition = "  Where 1 ";
            if (month != "") condition += " and Month("+ filtered_col + ") like " + month;
            if (year != "") condition += " and Year(" + filtered_col + ") like " + year;
            if (group != "") condition += " and lower(donorgroup.Name) Like lower('" + @group + "') ";
            if (paid != "") condition += " and Pay_IsPaid Like '" + paid + "' ";
            q += condition + " order by " + filtered_col + " asc "; 

            //check connection//
            Program.buildConnection();
            var sc = new MySqlCommand(q, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable Select_Loan_All_Data(string Loan_ID, string FundType, string Type, string SubType, string State, string Partners, string Donor, string DonorGroup, string Category,string SubCategory, string ApplyYear, string FundYear)
        {
            var from = @" from `loan` L 
 LEFT OUTER join `microproject` MP on L.MicroProject_ID = MP.MP_ID  
 LEFT OUTER join `person_microproject` PMP on PMP.MicroProject_ID = MP.MP_ID  
 LEFT OUTER join `person` P on P.P_ID = PMP.Person_ID  
 LEFT OUTER join `category` C on MP.MP_Category_ID = C.C_ID  
 LEFT OUTER join `subcategory` sub on MP.SubCategory_ID = sub.ID  
 LEFT OUTER join `state` on state.ID = MP.MP_State  
 LEFT OUTER join `donor` on donor.ID = MP.MP_Donor  
 LEFT OUTER join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID 
 LEFT OUTER JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
 LEFT OUTER JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
 LEFT OUTER JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID ";

            // IF(a-b<0,0,a-b) //
            //,(SELECT COUNT(DATEDIFF(`Pay_RecievedOnDate`,`Pay_DueDate`)>10) from `payment` where `Loan_ID` = L.Loan_ID) as 'عدد تأخيرات المستفيد'"
            //+ ",L.Loan_PackageName as 'المجموعة'"
            //+ ",((L.Loan_PaymentsCount) - (select count(Pay_Amount) from `payment` where `Loan_ID` = L.Loan_ID and `Pay_IsPaid` like N'Paid')) as 'عدد الدفعات المتبقية'"
            //,(L.Loan_Amount * L.Loan_Rate / 100) as 'المبلغ المطلوب استرداده'
            //,CONCAT(P.P_FirstName, ' ',P.P_FatherName, ' ', P.P_LastName) as 'المستفيد'

            var query = @"select L.Loan_ID as 'ID'
,L.MicroProject_ID as 'رقم المشروع' 
, Group_Concat(CONCAT(P.P_FirstName, ' ' , P.P_LastName , ' ابن/ة ', P.P_FatherName)) as 'المستفيد'
,L.Loan_DateTaken as 'تاريخ استلام القرض'
,L.Loan_Amount as 'القرض'
,Round( ( Loan_Amount / (SELECT Rate from donorgroup where donorgroup.ID = MP.DonorGroup_ID)) , 2 ) as 'القرض ($)'
,Round((L.Loan_ReturnedAmount * 100)/L.Loan_Amount, 2 ) as 'النسبة' 
,L.Loan_ReturnedAmount as 'المبلغ المطلوب استرداده'
,L.Loan_PaymentsAmount as 'القسط'

,(select sum(`Pay_Amount`) from `payment` where `Loan_ID` = L.Loan_ID and `Pay_IsPaid` like N'Paid') as 'المبلغ المدفوع'

,IF((L.Loan_ReturnedAmount - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID))<0
    ,0,(L.Loan_ReturnedAmount - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID))) as 'المبلغ المتبقي'

,Round( (L.Loan_ReturnedAmount / L.Loan_PaymentsAmount),2) as 'عدد الدفعات الكلي'

,(select count(Pay_Amount) from `payment` where `Loan_ID` = L.Loan_ID and `Pay_IsPaid` like N'Paid') as 'عدد الدفعات المدفوعة'

,((IF(((L.Loan_Amount * L.Loan_Rate / 100) - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID))<0 
    ,0,((L.Loan_Amount * L.Loan_Rate / 100) - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID))) )
    / L.Loan_PaymentsAmount ) as 'عدد الدفعات المتبقية'

,(SELECT `Pay_RecievedOnDate` FROM `payment` WHERE `Loan_ID` = L.Loan_ID and `Pay_IsPaid` like 'Paid' ORDER BY `payment`.`Pay_RecievedOnDate` DESC LIMIT 1) as 'تاريخ آخر دفعة مدفوعة'

,(SELECT `Pay_DueDate` FROM `payment` WHERE `Loan_ID` = L.Loan_ID and `Pay_IsPaid` like 'Not Paid'  AND state.Name_ar like 'ممول' ORDER BY `payment`.`Pay_DueDate` DESC LIMIT 1) as 'تاريخ الدفعة المستحقة'

,IF( 
    IF((L.Loan_ReturnedAmount - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID)) < 0
    ,0,(L.Loan_ReturnedAmount - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID))) = 0 
    ,0,
    (SELECT TIMESTAMPDIFF(MONTH, payment.Pay_DueDate , DATE_ADD( Now(),INTERVAL 1 MONTH)) from payment where payment.Loan_ID = L.Loan_ID and payment.Pay_IsPaid = 'Not Paid' and  state.ID != 7 ORDER by Pay_DueDate desc LIMIT 1 ))as 'أشهر التأخير'

,(select count(Pay_Amount) from `payment` where `Loan_ID` = L.Loan_ID and `Pay_IsPaid` like N'Not Paid') as 'الدفعات المستحقة والغير مدفوعة'

,donorgroup.Name as 'المجموعة'
,L.Loan_ReceiptID as 'رقم الإيصال'";


            var condition = " where 1 ";
            if (Loan_ID != "") condition += " and L.Loan_ID = " + Loan_ID;
            
            //Fund Type
            if (FundType != "")
            {
                condition += " and fundtype.Name like '" + FundType + "'";
            }
            //Type
            if (Type != "")
            {
                condition += " and microprojecttype.Name like '" + Type + "'";
            }
            //Sub Type
            if (SubType != "")
            {
                condition += " and microprojectsubtype.Name like '" + SubType + "'";
            }

            //MP State
            if (State != "")
            {
                if (State == "ممول+منتهي")
                    condition += " and state.ID in (4,5) ";
                else if (State == "ممول+منتهي+ملغى")
                    condition += " and state.ID in (4,5,7) ";
                else
                    condition += " and state.Name_Ar like N'" + State + "' ";
            }

            //Funded By
            if (Donor != "")
                condition += " and donor.Name like N'" + Donor + "'";

            //Donor Group
            if (DonorGroup != "")
                condition += " and donorgroup.Name like N'" + DonorGroup + "'";

            //Project Category
            if (Category != "")
                condition += " and C.C_Name like N'" + Category + "'";

            //Sub Category
            if (SubCategory != "")
                condition += " and sub.Name like N'" + SubCategory + "'";

            //Partners
            if (Partners != "")
            {
                if (Partners.Contains("شراكة"))
                    condition += " and Partnership = 2 ";
                else if (Partners.Contains("فردي")) condition += " and Partnership = 1 ";
                else condition += "";
            }

            //Apply Date
            if (ApplyYear != "") condition += " and Year(MP.MP_DateOfRequest) like '" + ApplyYear + "'";

            //Fund Date
            if (FundYear != "") condition += " and Year(L.Loan_DateTaken) like '" + FundYear + "'";

            //for group_concat//
            string groupBy = " Group By MP_ID ";

            query += from + condition + groupBy;

            Loan_from = from;
            Loan_condition = condition;

            //check connection//
            Program.buildConnection();
            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable Select_Loan_Sum(string id_loan_str,string donor)
        {
            var net_query = ""; string query = "";

            if (id_loan_str == "")
                net_query = @" ,IFNULL((select sum(`Pay_Amount`) from `payment` where Pay_Amount >= 0 and `Pay_IsPaid` = N'Paid') ,0)";
            else
                net_query = @" ,IFNULL((select sum(`Pay_Amount`) from `payment` where payment.Loan_ID in " + id_loan_str + " and Pay_Amount < 0 and `Pay_IsPaid` = N'Paid') ,0) + IFNULL((select sum(`Pay_Amount`) from `payment` where payment.Loan_ID in " + id_loan_str + " and Pay_Amount >= 0 and `Pay_IsPaid` = N'Paid') ,0)";
             
            if (donor != "")  net_query+= @"-(select SUM(lulu.Loan_Amount) from loan lulu join microproject m on lulu.MicroProject_ID = m.MP_ID left join donor on donor.ID = m.MP_Donor left join donorgroup on donorgroup.ID = m.DonorGroup_ID where donor.Name like '" + donor + "' and donorgroup.Name like '%Installment%') ";
            net_query += " as 'Remaining'";
          

            if (id_loan_str == "")
                query = @"select SUM(`Loan_Amount`) as 'Export'
 ,IFNULL((select sum(`Pay_Amount`) from `payment` where Pay_Amount >= 0 and `Pay_IsPaid` = N'Paid') ,0) as 'Import' " +
",IFNULL((select sum(`Pay_Amount`) from `payment` where Pay_Amount < 0 and `Pay_IsPaid` = N'Paid') ,0) as 'Returned' " +
",IFNULL((select sum(`Pay_Amount`) from `payment` where Pay_Amount < 0 and `Pay_IsPaid` = N'Paid') ,0) + IFNULL((select sum(`Pay_Amount`) from `payment` where Pay_Amount >= 0 and `Pay_IsPaid` = N'Paid') ,0) as 'Net'" +
",IFNULL( SUM( ( Loan_Amount / (SELECT Rate from donorgroup where donorgroup.ID = microproject.DonorGroup_ID))) ,0) as 'Export($)'"
 + net_query
 + " from `loan` left join `microproject` on loan.MicroProject_ID = microproject.MP_ID ";
            else
                query = @"select SUM(`Loan_Amount`) as 'Export'
 ,IFNULL((select sum(`Pay_Amount`) from `payment` where payment.Loan_ID in " + id_loan_str + " and Pay_Amount >= 0 and `Pay_IsPaid` = N'Paid') ,0) as 'Import' " +
",IFNULL((select sum(`Pay_Amount`) from `payment` where payment.Loan_ID in " + id_loan_str + " and Pay_Amount < 0 and `Pay_IsPaid` = N'Paid') ,0) as 'Returned' " +
",IFNULL((select sum(`Pay_Amount`) from `payment` where payment.Loan_ID in " + id_loan_str + " and Pay_Amount < 0 and `Pay_IsPaid` = N'Paid') ,0) + IFNULL((select sum(`Pay_Amount`) from `payment` where payment.Loan_ID in " + id_loan_str + " and Pay_Amount >= 0 and `Pay_IsPaid` = N'Paid') ,0) as 'Net'" +
",IFNULL( SUM( ( Loan_Amount / (SELECT Rate from donorgroup where donorgroup.ID = microproject.DonorGroup_ID))) ,0) as 'Export($)'"
  + net_query
  + " from `loan` left join `microproject` on loan.MicroProject_ID = microproject.MP_ID where `Loan_ID` in " + id_loan_str + " ";

    //        ,SUM(IF(((L.Loan_Amount * L.Loan_Rate / 100) - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID)) < 0
    //,0,((L.Loan_Amount * L.Loan_Rate / 100) - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID)))) as 'Remaining'
     
            //check connection//
            Program.buildConnection();
            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public int Count_Distinct_Loan_IDs()
        {
            //check connection//
            Program.buildConnection();
            //count rows
            var sel = "select count(DISTINCT MP_ID) " + Loan_from + Loan_condition;
            var sc = new MySqlCommand(sel, Program.MyConn);
            var count = Convert.ToInt32(sc.ExecuteScalar());
            Program.MyConn.Close();
            return count;
        }

        #endregion

        #region project

        //micro project bind//
        public DataTable MicroProject_bind(int Person_ID, int MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();

            var query = "select MP.MP_ID as 'MicroProject_ID'"
                + ", MP_Name as 'Project Name'"
                + ", MP_RequestedAmount as 'Requested Amount'"
                + ", MP_PeriodOfExecution as 'Requested Time'"
                + ", MP_DateOfRequest as 'Application Date'"
                + ", MP_Description as 'Description'"

                //+ ",MP.MP_ProjectKind as 'Project Kind'"
                //+ ",MP.MP_Type as 'Project Type'"

                + ", MP_FundType_ID as 'FundType_ID'"
                + ", MP_Type_ID as 'Type_ID'"
                + ", MP_SubType_ID as 'SubType_ID'"
                 
                + ", MP_Category_ID as 'Category_ID'"
                + ", SubCategory_ID as 'SubCategory_ID'"
                + ", (select C_Name from `category` where C_ID = MP.MP_Category_ID) as 'Category'"
                + ", (select Name from `subcategory` where ID = MP.SubCategory_ID) as 'SubCategory'"
                          
                + ", MP_HasPreviousDonors as 'Has Previous Donors'"
                + ", MP_OtherDonors as 'Other Donors'"
                + ", MP_IsNeedLicense as 'needs a license'"
                + ", MP_LicenseSide as 'License Side'"
                + ", MP_HasCompetitors as 'Has Competitors'"
                + ", MP_Competitors as 'Competitors'"
                + ", MP_SuppliesInsurance as 'Supplies Insurance'"
                + ", MP_Suppliers as 'Suppliers'"
                + ", MP_Risk as 'Risk'"
                + ", MP_Protection as 'Protection'"
                + ", MP_OtherNotes as 'Other Notes'"
                + ", MP_Marketing as 'Marketing'"
                + ", MP_SimpleProfit as 'Simple Profit'"
                + ", MP_Profit_Description as 'Profit Comment'"
                + ", MP_IncomeKind as 'Income Kind'"
                + ", MP_IncomeKind_Note as 'Income Kind Note'"
                + ", MP_HasLedger as 'Has Ledger'" 
                + ", MP_StateReason as 'Project status reason'"
                
                + ", MP_Donor as 'Donor_ID'" 
                + ", (select donor.Name from donor where donor.ID = MP_Donor) as 'Donor'"
                       
                + ", (select Name_ar from state where ID = MP_State) as 'Project State'"
                + ", MP_State as 'State_ID'"
                       
                + ", MP_Visited as 'Visited'"
                + ", MP_Message as 'Message'"
                + ", IsContentUpdated as 'IsContentUpdated'"
                + ", MP_StateDate as 'Message Date'"

                + ", MP_PaperUser_ID as    'PaperUser_ID'"
                + ", MP_ProgramUser_ID as  'ProgramUser_ID'"
                + ", MP_EditedByUser_ID as 'EditedByUser_ID'"

                + ", (select UserName from user where user.UserID = MP_PaperUser_ID limit 1) as 'PaperUser'"
                + ", (select UserName from user where user.UserID = MP_ProgramUser_ID limit 1) as 'ProgramUser'"
                + ", (select UserName from user where user.UserID = MP_EditedByUser_ID limit 1) as 'EditedByUser'"
                         
                + " from `microproject` MP"
                + " where MP.MP_ID = " + MicroProject_ID; 
            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        //micro project details bind// 
        public DataTable MicroProject_Priest_bind(int MicroProject_ID)
        {
            //check connection//
            Program.buildConnection(); 
            MySS.query =
                "select Priest_Name as 'Name' from `priest` right outer join `microproject` on priest.Priest_ID = microproject.MP_Priest_ID "
                + " where microproject.MP_ID = " + MicroProject_ID + " "; 
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Program.MyConn.Close();
            return MySS.dt;
        }

        public DataTable Budget_Item_bind(int MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select I.I_Name as 'Item Name'" +
                         ",I.I_Price as 'Item Price'" +
                         ",BI.Item_Amount as 'Amount'" +
                         "\n from `item` I right outer join `budget_item` BI on BI.Item_ID = I.I_ID " +
                         "left outer join `budget` B on BI.Budget_ID = B.B_ID" +
                         "\n where B.MicroProject_ID = " + MicroProject_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Program.MyConn.Close();
            return MySS.dt;
        }

        public DataTable Materials_bind(int MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select `ID` as 'ID'" +
                         ",`Name` as 'Name'" +
                         ",`Amount` as 'Amount'" +
                         ",`Price` as 'Price'" +
                         ",`LocalContribution` as 'LocalContribution'" +
                         ",`Comments` as 'Comments'" +
                         ",`MicroProject_ID` as 'MicroProject_ID'" +
                         " FROM `material` " +
                         " WHERE MicroProject_ID = " + MicroProject_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Program.MyConn.Close();
            return MySS.dt;
        }

        public DataTable Risks_bind(int MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "select risk.Name as 'Name'" +
                         ",`Risk_ID` as 'Risk_ID'" +
                         ",`MicroProject_ID` as 'MicroProject_ID'" +
                         " FROM `microproject_risk` left join `risk` on microproject_risk.Risk_ID = risk.ID " +
                         " WHERE MicroProject_ID = " + MicroProject_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Program.MyConn.Close();
            return MySS.dt;
        }

        public DataTable Score_bind(int MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();
            MySS.query = "select score.Name as 'Name'" +
                         ",`Score_ID` as 'Score_ID'" +
                         ",`value` as 'value'" +
                         ",`notes` as 'notes'" +
                         ",`MicroProject_ID` as 'MicroProject_ID'" +
                         " FROM `microproject_score` left join `score` on microproject_score.Score_ID = score.ID " +
                         " WHERE MicroProject_ID = " + MicroProject_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Program.MyConn.Close();
            return MySS.dt;
        }

        #endregion

        #region beneficiary

        //benefeciary bind//
        public DataTable Person_bind(int Person_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select P_ID as 'ID'"
                         + ",P_FirstName as 'First Name'"
                         + ",P_LastName as 'Last Name'"
                         + ",P_FatherName as 'Father Name'"
                         + ",P_MotherName as 'Mother Name'"
                         + ",P_DOB as 'Birth Date'"
                         + ",P_MilitaryService as 'Military Services'"
                         + ",P_MilitaryService_Note as 'Military Services Note'"
                         + ",P_Mobile as 'Mobile'"
                         + ",P_HomeTel as 'Land Line'"
                         + ",Street_ID as 'Street_ID'"
                         + ",P_HomeAddress as 'Home Address'"
                         + ",P_NationalNumber as 'National Number'"
                         + ",P_RegistrationPlace as 'Nationality'"
                         + ",P_Sex as 'Gender'"
                         + ",P_MaritalStatus as 'Marital Status'"
                         + ",P_IsLivingWithFamily as 'Is Living With Family'"
                         + ",P_LiveWithFamily_Note as 'Living With Family Note'"
                         + ",P_NumAtHome as 'Family members at home'"
                         + ",IsProjectOwner as 'Project Owner'"
                         + ",IsProvider as 'Provider'"
                         + ",P_SourceOfIncome as 'Source Of Income'"
                         + ",P_MedicalCond as 'Medical Conditions'"
                         + ",P_MedicalHelp as 'Medical Help'"
                         + ",P_MedicalHelp_Note as 'Medical Help Note'"
                         + ",P_Loss as 'Loss'"
                         + ",P_IntermidiarySide as 'Intermidiary Side'"
                         + ",P_HomeState as 'Home State'"
                         + ",P_HomeState_Note as 'Home State Note'"
                         + ",P_OtherProperties as 'Other Properties'"
                         + ",P_OtherProperties_Note as 'Other Properties Note'"
                         + ",P_OtherIncomeSources as 'Other Income Sources'"
                         + ",P_OtherIncomeSources_Note as 'Other Income Sources Note'"
                         + ",P_Courses_Note as 'Other Courses'"
                         + ",P_MaristesCourse as 'Maristes Course'"
                         + ",P_OtherCourses as 'Other Courses'"
                         + ",P_Parish as 'Parish'"

                         + ",P_Priest_ID as 'Priest_ID'"
                         + ",Priest_Name as 'Priest'"
                         + ",P_PicturePath as 'Picture Path'"
                         + ",person_microproject.PersonType as 'PersonType'"

                         + " From `person` " 
                         + "  left outer join `priest` on priest.Priest_ID = person.P_Priest_ID "
                         + "  left outer join `person_microproject` on person_microproject.Person_ID = person.P_ID "

                         + " Where P_ID= " + Person_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Program.MyConn.Close();
            return MySS.dt;
        }

        public MySqlDataReader Person_Image(int Person_ID)
        {
            //check connection//
            Program.buildConnection();

            MySqlDataReader reader;
            MySS.query = "select P_Picture, P_PicturePath from `person` where P_ID = " + Person_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            reader = MySS.sc.ExecuteReader();

            return reader;
        }

        //benefeciary details bind//
        public DataTable Person_Education_bind(int Person_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select PEdu_ID as 'ID'" +
                         ",E.E_Type as 'Level'" +
                         ",E.E_Name as 'Name'" +
                         "\n from `person_education` PE left outer join `education` E on PE.Education_ID = E.E_ID" +
                         "\n where PE.Person_ID=" + Person_ID + "";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            Program.MyConn.Close();
            return MySS.dt;
        }

        public DataTable Person_Experience_bind(int Person_ID, int MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select PW_ID as 'ID'" +
                         ",W.W_Name as 'Name'" +
                         ",PWE.W_Description as 'Description'" +
                         ",PWE.W_BeginDate as 'Begin Date'" +
                         ",PWE.W_EndDate as 'End Date'" +
                         ",PWE.W_Place as 'Place'" +
                         ",PWE.W_PlaceState as 'Place State'" +
                         ",PWE.W_Status as 'Status'" +
                         ",PWE.W_CauseOfLose as 'Reason Of Lose'" +
                         ",PWE.W_CauseOfLoseDescription as 'Description Reason Of Lose'" +
                         ",PWE.W_Salary as 'Salary'" +
                         "\n from `person_workexp` PWE left outer join `work` W on PWE.Work_ID = W.W_ID " +
                         "\n where PWE.Person_ID=" + Person_ID + " and PWE.MicroProject_ID=" + MicroProject_ID +
                         " and PWE.W_Status='No'";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            Program.MyConn.Close();
            return MySS.dt;
        }

        public DataTable Person_Work_bind(int Person_ID, int MicroProject_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select PW_ID as 'ID'" +
                         ",W.W_Name as 'Name'" +
                         ",PWE.W_Description as 'Description'" +
                         ",PWE.W_BeginDate as 'Begin Date'" +
                         ",PWE.W_EndDate as 'End Date'" +
                         ",PWE.W_Place as 'Place'" +
                         ",PWE.W_PlaceState as 'Place State'" +
                         ",PWE.W_Status as 'Status'" +
                         ",PWE.W_CauseOfLose as 'Reason Of Lose'" +
                         ",PWE.W_CauseOfLoseDescription as 'Description Reason Of Lose'" +
                         ",PWE.W_Salary as 'Salary'" +
                         "\n from `person_workexp` PWE left outer join `work` W on PWE.Work_ID = W.W_ID " +
                         "\n where PWE.Person_ID=" + Person_ID + " and PWE.MicroProject_ID=" + MicroProject_ID +
                         " and PWE.W_Status='Yes' ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            Program.MyConn.Close();
            return MySS.dt;
        }

        public DataTable Person_Courses_bind(int Person_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select PC.Course_ID as 'ID' " +
                         " ,C.Name as 'Name'" +
                         " from `person_course` PC left outer join `course` C on PC.Course_ID = C.ID " +
                         " where PC.Person_ID = " + Person_ID;

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            Program.MyConn.Close();
            return MySS.dt;
        }

        public DataTable Person_Loss_bind(int Person_ID)
        {
            //check connection//
            Program.buildConnection();

            var query = "select PL.Loss_ID as 'ID' " +
                        " ,L.Name as 'Name'" +
                        " from `person_loss` PL left outer join `loss` L on PL.Loss_ID = L.ID " +
                        " where PL.Person_ID = " + Person_ID;

            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);

            Program.MyConn.Close();
            return dt;
        }

        //public DataTable Person_Language_bind(int Person_ID)
        //{
        //    //check connection//
        //                   Program.buildConnection();

        //    MySS.query = "select PL_ID as 'ID'" +
        //       ",L.L_Name as 'Language'" +
        //       ",PL.L_Level as 'Level'" +
        //       "\n from `person_language` PL left outer join `language` L on PL.Language_ID = L.L_ID " +
        //       "\n where PL.Person_ID=" + Person_ID + "";

        //    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
        //    MySS.sc.ExecuteNonQuery();
        //    MySS.da = new MySqlDataAdapter(MySS.sc);
        //    MySS.dt = new DataTable();
        //    MySS.da.Fill(MySS.dt);

        //    Program.MyConn.Close();
        //    return MySS.dt;
        //}
        //public DataTable Person_Skill_bind(int Person_ID)
        //{
        //    //check connection//
        //                   Program.buildConnection();

        //    string strCmd = "select PS_ID as 'ID'" +
        //       ",S.S_Name as 'Skill'" +
        //       ",PS.S_Describtion as 'Description'" +
        //       "\n from `person_skill` PS left outer join `skill` S on PS.Skill_ID = S.S_ID " +
        //       "\n where PS.Person_ID=" + Person_ID + "";

        //    MySS.sc = new MySqlCommand(strCmd, Program.MyConn);
        //    MySS.sc.ExecuteNonQuery();
        //    MySS.da = new MySqlDataAdapter(MySS.sc);
        //    MySS.dt = new DataTable();
        //    MySS.da.Fill(MySS.dt);

        //    Program.MyConn.Close();
        //    return MySS.dt;
        //}

        #endregion

        #region visits

        public DataTable InitialVisit_bind(int V_ID)
        {
            Program.buildConnection();

            MySS.query = "SELECT `ID`"
                         + ", visitinitial.`MicroProject_ID`"
                         + ", CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                         + ", `Date`, `Type`, `Reseption`, `Waiting`, `Visit`, `Lawyer`, `Purchasing`, `Photography`, `OtherComments`"
                         + ", `StartInTime`, `StartInTimeReason`, `PurchaseAllItemsOfBudget`, `PurchaseAllItemsOfBudgetReason`"
                         + ", `SamePlace`, `SamePlaceReason`, `SameQualityAndQuantity`, `SameQualityAndQuantityReason`, `Marketing` "
                         + " FROM visitinitial left outer join person_microproject on visitinitial.MicroProject_ID = person_microproject.MicroProject_ID "
                         + " join person P on P.P_ID = person_microproject.Person_ID "
                         + " where ID = " + V_ID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            Program.MyConn.Close();
            return MySS.dt;
        }

        public DataTable FinancialVisit_bind(int V_ID)
        {
            Program.buildConnection();
            MySS.query = "SELECT `ID`"
                         + ", visitfinancial.`MicroProject_ID`"
                         + ", CONCAT(P.P_FirstName, ' ', P.P_FatherName, ' ', P.P_LastName) as 'Beneficiary Name'"
                         + ", `Date`, `Type`, `Continuance`, `Ledger`, `LedgerReason`, `ProfitRatio`, `AverageSales`"
                         + ", `AveragePrice`, `Indicators` "
                         + " FROM visitfinancial left outer join person_microproject on visitfinancial.MicroProject_ID = person_microproject.MicroProject_ID "
                         + " join person P on P.P_ID = person_microproject.Person_ID "
                         + " where ID = " + V_ID;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            Program.MyConn.Close();
            return MySS.dt;
        }

        #endregion
    }
}