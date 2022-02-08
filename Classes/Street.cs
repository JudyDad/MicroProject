﻿using System.Data;
using MySql.Data.MySqlClient;

namespace MyWorkApplication.Classes
{
    public class Street
    {
        private string query;

        public void Insert(string Name)
        {
            //check connection//
            Program.buildConnection();
            query = "INSERT INTO `street` (`Name`) VALUES ('" + Name + "')";
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Update(int ID, string Name)
        {
            //check connection//
            Program.buildConnection();
            query = "Update `street` set Name = N'" + Name + "'"
                         + " where `ID` =" + ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }

        public void Delete(int ID)
        {
            //check connection//
            Program.buildConnection();
            query = "delete From `street` where `ID` =" + ID;
            using (var sc = new MySqlCommand(query, Program.MyConn))
            {
                sc.ExecuteNonQuery();
                Program.MyConn.Close();
            }
        }
        
        public DataTable Select(string name)
        {
            //check connection//
            Program.buildConnection();
            query = "SELECT ID, Name from `street` order by Name asc ";
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