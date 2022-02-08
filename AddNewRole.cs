using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class AddNewRole : Form
    {
        public AddNewRole()
        {
            InitializeComponent();
        }
        public AddNewRole(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        MySqlComponents MySS;
        private DataRow SelectedDataRow;
        private int RoleID;
        private Log l;
        private string username;

        #region btn clicks
        private void InsertRole_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (RoleName_textBox.Text == "" || RoleDescrib_textBox.Text == "" || RoleCode_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                insertRole(RoleName_textBox.Text, Int32.Parse(RoleCode_textBox.Text), RoleDescrib_textBox.Text);

                l.Insert_Log("insert the role " + RoleName_textBox.Text, "Role", username, DateTime.Now);

                role_bind();
                clear_role_boxes();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("You can't leave empty fields");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateRole_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (RoleName_textBox.Text == "" || RoleDescrib_textBox.Text == "" || RoleCode_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }

                UpdateRole(RoleID, RoleName_textBox.Text, Int32.Parse(RoleCode_textBox.Text), RoleDescrib_textBox.Text);

                l.Insert_Log("update the role " + RoleName_textBox.Text, "Role", username, DateTime.Now);

                role_bind();
                clear_role_boxes();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("You can't leave empty fields");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeleteRole_button_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (SelectedDataRow != null && RoleID != -1)
            {
                deleteRole(RoleID);

                l.Insert_Log("delete the role " + RoleName_textBox.Text, "Role", username, DateTime.Now);

                role_bind();
                clear_role_boxes();
            }
             }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("You can't leave empty fields");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region insert - update - delete - bind
        private void insertRole(string rolename, int rolecode, string roledescrib)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Insert Into `role`(`Role_Name`,`Role_Code`,`Role_Describtion`) values(N'"
                + rolename + "',"
                + rolecode + ",N'"
                + roledescrib + "' )";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void UpdateRole(int roleID, string rolename, int rolecode, string roledescrib)
        {
            //check connection//
           Program.buildConnection();
            
            MySS.query = "Update `role` set "
                + "`Role_Name` =N'" + rolename + "'"
                + ",`Role_Code` =" + rolecode + " "
                + ",`Role_Describtion` =N'" + roledescrib + "'"
                + "\n where `Role_ID` = " + roleID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void deleteRole(int roleID)
        {
            //check connection//
            Program.buildConnection();
            
            MySS.query = "Delete from `role` where `Role_ID` = " + roleID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void role_bind()
        {
            MySS.query = "select `Role_ID` as 'ID'" +
                ",`Role_Name` as 'Name'" +
                ",`Role_Code` as 'Code'" +
                ",`Role_Describtion` as 'Description'" +

                "\n from `role` ";
            //check connection//
            Program.buildConnection();
            
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            RoleDataGridView.DataSource = MySS.dt;

            DataGridViewColumn dgC1 = RoleDataGridView.Columns["ID"];
            dgC1.Visible = false;
        }
        #endregion

        #region mouse hover
        private void AddSave_button_MouseEnter(object sender, EventArgs e)
        {
            InsertRole_button.BackgroundImage = Properties.Resources.save;
        }
        private void AddSave_button_MouseLeave(object sender, EventArgs e)
        {
            InsertRole_button.BackgroundImage = Properties.Resources.save0;
        }
        private void UpdateSave_button_MouseEnter(object sender, EventArgs e)
        {
          //  UpdateRole_button.BackgroundImage = Properties.Resources.update;
        }
        private void UpdateSave_button_MouseLeave(object sender, EventArgs e)
        {
            UpdateRole_button.BackgroundImage = Properties.Resources.update0;
        }
        private void delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeleteRole_button.BackgroundImage = Properties.Resources.delete;
        }
        private void delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteRole_button.BackgroundImage = Properties.Resources.delete0;
        }
        #endregion

        private void RoleDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedDataRow = ((DataRowView)RoleDataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                RoleID = Int32.Parse(SelectedDataRow["ID"].ToString());
                RoleName_textBox.Text = (string)SelectedDataRow["Name"];
                int RoleCode = Int32.Parse(SelectedDataRow["Code"].ToString());
                RoleCode_textBox.Text = RoleCode.ToString();
                RoleDescrib_textBox.Text = (string)SelectedDataRow["Description"];
            }
        }
        private void clear_role_boxes()
        {
            RoleID = -1;
            RoleName_textBox.Text = RoleCode_textBox.Text = RoleDescrib_textBox.Text = "";
        }

        private void AddNewRole_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.AddNewForm_ToLight(this);
                else
                    myTheme.AddNewForm_ToNight(this);

                MySS = new MySqlComponents();
                l = new Log();
                role_bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
