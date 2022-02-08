using System.Data;
using MySql.Data.MySqlClient;

namespace MyWorkApplication.Classes
{
    public class SubCategory
    {
        private string query;

        public void Insert_SubCategory(string Name, int Category_ID)
        {
            //check connection//
            Program.buildConnection();
            query = "INSERT INTO `subcategory` (`Name`, `Category_ID`) VALUES ("
                        + "'" + Name + "',"
                        + Category_ID + ")";
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Update_SubCategory(int ID, string Name, int Category_ID)
        {
            //check connection//
            Program.buildConnection();
            query = "Update `subcategory` set "
                         + " Name = N'" + Name + "',"
                         + " Category_ID = " + Category_ID + " "
                         + " where `ID` =" + ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Delete_SubCategory(int ID)
        {
            //check connection//
            Program.buildConnection();
            query = "delete From `subcategory` where `ID` =" + ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }
        
        public DataTable Select(string Category_ID, string Name)
        {
            //check connection//
            Program.buildConnection();
            query = "SELECT ID, Name, Category_ID " +
                ",(select C_Name from category where C_ID = subcategory.Category_ID) as 'Category' from `subcategory`";

            string condition = " where 1 ";
            if (Category_ID != "") condition += " and Category_ID = " + Category_ID;
            else if (Name != "") condition += " and Name like '"+Name+"' ";

            query += condition + " order by Name asc ";
            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            Program.MyConn.Close();
            return dt;
        }

        public DataTable Category_Select()
        {
            //check connection//
            Program.buildConnection();
            query = "select C_ID,C_Name from `category` ORDER BY C_Name ASC ";
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