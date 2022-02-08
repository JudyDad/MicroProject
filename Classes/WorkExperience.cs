﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkApplication.Classes
{
    public class WorkExperience
    {

        private string query;

        public void Insert(string Name)
        {
            //check connection//
            Program.buildConnection();
            query = "INSERT INTO `work` (`W_Name`) VALUES ('" + Name + "')";
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
            query = "Update `work` set W_Name = N'" + Name + "'"
                         + " where `W_ID` =" + ID;
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
            query = "delete From `work` where `W_ID` =" + ID;
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
            query = "SELECT W_ID as 'ID', W_Name as 'Name' from `work` order by W_Name asc ";
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
