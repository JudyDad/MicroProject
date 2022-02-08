using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;

namespace MyWorkApplication
{
    public partial class ShowFamilyMembers : Form
    {
        private readonly string family_ID;
        private readonly int Person_ID;

        public ShowFamilyMembers()
        {
            InitializeComponent();
        }

        public ShowFamilyMembers(string family_ID, int Person_ID)
        {
            InitializeComponent();
            this.family_ID = family_ID;
            this.Person_ID = Person_ID;
        }

        public void FamilyMember_bind()
        {
            //check connection//
            Program.buildConnection();

            var query = "select p.P_ID as 'ID'"
                        + ",pf.Family_ID as 'رقم العائلة'"
                        + ",f.F_Number as 'رقم دفتر العائلة'"
                        + ",CONCAT(p.P_FirstName, ' ', p.P_FatherName, ' ', p.P_LastName) as 'الاسم الثلاثي'"
                        + ",p.P_MotherName as 'الأم'"
                        + ",p.P_MaritalStatus as 'الحالة الاجتماعية'"
                        + ",pf.IsInNow as 'من دفتر العائلة'"
                        + ",pf.Relation as 'علاقته مع المستفيد'"
                        + ",pf.Work_Name as 'العمل'"
                        + ",p.P_DOB as 'تاريخ الولادة'"
                        + "\n From `person` p right outer join `person_family` pf on p.P_ID = pf.Person_ID left outer join `family` f on f.F_ID = pf.Family_ID ";
            var condition = "";
            if (family_ID != "")
                condition = " where pf.Family_ID like CAST('" + family_ID + "' AS CHAR) and pf.P_Provider_ID = " +
                            Person_ID + " ";
            query += condition;
            var sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            var da = new MySqlDataAdapter(sc);
            var dt = new DataTable();
            da.Fill(dt);
            PersonDataGridView.DataSource = dt;
            var dgC1 = PersonDataGridView.Columns["ID"];
            PersonDataGridView.Columns["تاريخ الولادة"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgC1.Visible = false;
            Program.MyConn.Close();

            PersonDataGridView.Columns[1].DefaultCellStyle.Alignment =
                PersonDataGridView.Columns[2].DefaultCellStyle.Alignment =
                    PersonDataGridView.Columns[9].DefaultCellStyle.Alignment =
                        PersonDataGridView.Columns[5].DefaultCellStyle.Alignment =
                            PersonDataGridView.Columns[7].DefaultCellStyle.Alignment =
                                PersonDataGridView.Columns[6].DefaultCellStyle.Alignment =
                                    DataGridViewContentAlignment.MiddleCenter;

            PersonDataGridView.Columns[6].Width = PersonDataGridView.Columns[5].Width =
                PersonDataGridView.Columns[4].Width = PersonDataGridView.Columns[7].Width = 70;
            PersonDataGridView.Columns[1].Width = 90;
            PersonDataGridView.Columns[1].Width = PersonDataGridView.Columns[9].Width = 120;
            PersonDataGridView.Columns[3].Width = 150;
        }

        private void ShowFamilyMembers_Load(object sender, EventArgs e)
        {
            try
            {
                var newTheme = new NewTheme();
                if (Settings.Default.theme == "Dark")
                    newTheme.FamilyMembers_ToNight(this);
                else
                    newTheme.FamilyMembers_ToLight(this);


                FamilyMember_bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Close_button_MouseEnter(object sender, EventArgs e)
        {
            Close_button.BackgroundImage = Resources.Exit_D;
        }

        private void Close_button_MouseLeave(object sender, EventArgs e)
        {
            Close_button.BackgroundImage = Resources.Exit_L;
        }
    }
}