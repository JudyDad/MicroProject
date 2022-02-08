using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyWorkApplication
{
    public partial class AddNewCourse : Form
    {
        public AddNewCourse(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private SqlCommand sc;
        private SqlDataAdapter da;
        private SqlDataReader reader;
        private DataTable dt;
        private int Course_ID;
        private string username;
        private Log l;
        private DataRow SelectedDataRow;

        public void Course_bind()
        {
            string strCmd = "select C_ID as 'رقم الدورة',C_Name as 'اسم الدورة' from Course";
            sc = new SqlCommand(strCmd, Program.MyConn);
            sc.ExecuteNonQuery();
            da = new SqlDataAdapter(sc);
            dt = new DataTable();
            da.Fill(dt);
            Course_dataGridView.DataSource = dt;
            DataGridViewColumn dgC2 = Course_dataGridView.Columns["رقم الدورة"];
            dgC2.Visible = false;
        }

        #region insert update delete Course

        private void insertCourse()
        {
            string insCourseQuery = "Insert Into dbo.Course values(N'" + CourseName_textBox.Text + "' )";
            sc = new SqlCommand(insCourseQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void updateCourse(int CID)
        {
            string updCourseQuery = "Update Course set "
                    + "C_Name = N'" + CourseName_textBox.Text + "'"
                    + "where C_ID =" + CID;
            sc = new SqlCommand(updCourseQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }

        private void deleteCourse(int CID)
        {
            string delCourseQuery = "delete From Course where C_ID =" + CID;
            sc = new SqlCommand(delCourseQuery, Program.MyConn);
            sc.ExecuteNonQuery();
        }
        #endregion

        private void AddNewCourse_Load(object sender, EventArgs e)
        {
            l = new Log();
            Course_bind();
        }

        private void InsertCourse_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (CourseName_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                insertCourse();
                l.Insert_Log("Insert " + CourseName_textBox.Text, " Category ", username, DateTime.Now);
                CourseName_textBox.Clear();
                Course_bind();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("لا يمكن ترك بعض الحقول فارغة");
            }
        }

        private void DeleteCourse_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (CourseName_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                deleteCourse(Course_ID);
                l.Insert_Log("Delete " + CourseName_textBox.Text, " Category ", username, DateTime.Now);

                CourseName_textBox.Clear();
                Course_bind();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("اختر أولاً الاسم الذي تريد حذفه");
            }
        }

        private void UpdateCourse_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (CourseName_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                updateCourse(Course_ID);
                l.Insert_Log("Update " + CourseName_textBox.Text, " Category ", username, DateTime.Now);

                CourseName_textBox.Clear();
                Course_bind();
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("لا يمكن ترك بعض الحقول فارغة");
            }
        }

        private void Course_dataGridView_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Course_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    Course_ID = Int32.Parse(SelectedDataRow["رقم الدورة"].ToString());

                    CourseName_textBox.Text = SelectedDataRow["اسم الدورة"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region mouse move
        private void delete_button_MouseEnter(object sender, EventArgs e)
        {
            //var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string filePath = Path.Combine(projectPath, "Resources");
            DeleteCourse_button.BackgroundImage = Properties.Resources.delete;
        }
        private void delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeleteCourse_button.BackgroundImage = Properties.Resources.delete0;
        }
        private void Update_button_MouseEnter(object sender, EventArgs e)
        {
            //var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string filePath = Path.Combine(projectPath, "Resources");
            UpdateCourse_button.BackgroundImage = Properties.Resources.update;
        }
        private void Update_button_MouseLeave(object sender, EventArgs e)
        {
            //var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string filePath = Path.Combine(projectPath, "Resources");
            //UpdateContract_button.BackgroundImage = UpdateExeFile_button.BackgroundImage =
            //    UpdatePayment_button.BackgroundImage = Image.FromFile(filePath + "\\update0.png");
            UpdateCourse_button.BackgroundImage = Properties.Resources.update0;
        }
        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            //var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string filePath = Path.Combine(projectPath, "Resources");
            //AddExeFile_button.BackgroundImage = AddPayment_button.BackgroundImage = Image.FromFile(filePath + "\\add.png");
            InsertCourse_button.BackgroundImage = Properties.Resources.add;
        }
        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            //var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string filePath = Path.Combine(projectPath, "Resources");
            //AddExeFile_button.BackgroundImage = AddPayment_button.BackgroundImage = Image.FromFile(filePath + "\\add0.png");
            InsertCourse_button.BackgroundImage = Properties.Resources.add0;
        }

        #endregion
    }
}
