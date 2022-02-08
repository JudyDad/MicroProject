using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWorkApplication.Visit_Forms
{
    public partial class Other_Visit2_Form : Form
    {
        public Other_Visit2_Form(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            V_ID = -1;
        }
        public Other_Visit2_Form(int V_ID,MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.V_ID = V_ID;
        }
        public Other_Visit2_Form(MainForm mainForm,int MicroProject_ID)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.MicroProject_ID = MicroProject_ID;
            this.V_ID = -1;
        }
        private const string _KIND = "O2";
        DataRow SelectedDataRow;
        int MicroProject_ID, V_ID, User_ID , Person_ID, Office_Monitoring_ID;
        MySqlComponents MySS;
        NewTheme NewTheme;
        Log l; Select s;
        Visit v; Office_Monitoring O;
        DataTable Visit_Users_dt;
        TasksOfProjects TasksOfProjects;
        bool Update_Mood;
        MainForm mainForm;

        private void Other_Visit2_Form_Load(object sender, EventArgs e)
        {
            try
            {
                NewTheme = new NewTheme();
                if (Properties.Settings.Default.theme == "Light")
                {
                    NewTheme.Visit_ToLight(this);
                }
                else
                {
                    NewTheme.Visit_ToNight(this);
                }
                mainForm.Project_label.Visible = mainForm.TabName_label.Visible = true;
                mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = false;
                mainForm.TabName_label.Text = "Second Visit of Other Project";

                MySS = new MySqlComponents();
                l = new Log(); s = new Select();
                v = new Visit();
                //O = new Office_Monitoring();

                users_bind(V1_comboBox); users_bind(V2_comboBox); users_bind(V3_comboBox); users_bind(V4_comboBox);
                Person_Name_textBox1.AutoCompleteCustomSource = s.select_beneficiaries(_KIND, "Yes");
                MicroProject_ID_textBox1.AutoCompleteCustomSource = s.select_project_IDs(_KIND, "Yes");
                if (V_ID != -1)
                {
                    fill_visit2_boxes(V_ID);

                    mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                    mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
                }
                else if (MicroProject_ID != -1)
                {
                    MicroProject_ID_textBox1.Text = MicroProject_ID.ToString();
                    MicroProject_ID_textBox1_Leave(sender, e);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void users_bind(ComboBox comboBox)
        {
            try
            {
                DataTable dt = s.select_visitors();
                comboBox.DataSource = dt;
                comboBox.DisplayMember = "Visitors";
                comboBox.ValueMember = "U_ID";
                comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void fill_visit2_boxes(int V_ID)
        {
            try
            {
                DataTable dt = v.Get_Visit(V_ID);
                if (dt != null)
                {
                    MicroProject_ID = Int32.Parse(dt.Rows[0]["MicroProject_ID"].ToString());
                    MicroProject_ID_textBox1.Text = MicroProject_ID.ToString();
                    Person_Name_textBox1.Text = (string)dt.Rows[0]["Beneficiary Name"];

                    Person_ID = Int32.Parse(dt.Rows[0]["Beneficiary_ID"].ToString());

                    VisitType_comboBox1.Text = (string)dt.Rows[0]["Type"];
                    DateTime date1 = (DateTime)dt.Rows[0]["Date"];
                    VisitDate_dateTimePicker.Value = date1;

                    select_EvaluationForms_OfBeneficiary(Person_ID);

                    Ans1_textBox.Text = (string)dt.Rows[0]["Ans1"];
                    Ans2_textBox.Text = (string)dt.Rows[0]["Ans2"];
                    Ans3_textBox.Text = (string)dt.Rows[0]["Ans3"];
                    Ans4_textBox.Text = (string)dt.Rows[0]["Ans4"];
                   // Ans5_textBox.Text = (string)dt.Rows[0]["Ans5"];
                    Indicatores_textBox.Text = (string)dt.Rows[0]["Indicators"];
                    //DataTable office_dt = O.Get_Office_Monitoring(V_ID);
                    //if (office_dt != null)
                    //{
                    //    Office_Monitoring_ID = Int32.Parse(office_dt.Rows[0]["ID"].ToString());
                    //    //Office1_textBox.Text = (string)office_dt.Rows[0]["Ans1"];
                    //    //Office2_textBox.Text = (string)office_dt.Rows[0]["Ans2"];
                    //    //Office3_textBox.Text = (string)office_dt.Rows[0]["Ans3"];
                    //    //Office4_textBox.Text = (string)office_dt.Rows[0]["Ans4"];
                    //    //Office5_textBox.Text = (string)office_dt.Rows[0]["Ans5"];
                    //    //Office6_textBox.Text = (string)office_dt.Rows[0]["Ans5"];
                    //    //OtherComments_textBox.Text = (string)office_dt.Rows[0]["OtherComments"];
                    //}
                    
                                   Program.buildConnection();

                    MySS.query = "select Loan_Amount as 'Loan Amount'"
                                    + ",Loan_DateTaken as 'Loan Date'"
                                    + " from loan where MicroProject_ID = " + MicroProject_ID + " ";
                    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
                    MySS.sc.ExecuteNonQuery();
                    MySS.da = new MySqlDataAdapter(MySS.sc);
                    MySS.dt = new DataTable();
                    MySS.da.Fill(MySS.dt);

                    Program.MyConn.Close();

                    Double dd = Convert.ToDouble(MySS.dt.Rows[0][0].ToString());
                    LoanAmount_textBox1.Text = dd.ToString();
                    DateTime date = (DateTime)MySS.dt.Rows[0][1];
                    LoanDate_dateTimePicker.Value = date;

                    V1_comboBox.Text = V2_comboBox.Text = V3_comboBox.Text = V4_comboBox.Text = "";
                    V1_comboBox.SelectedIndex = V2_comboBox.SelectedIndex = V3_comboBox.SelectedIndex = V4_comboBox.SelectedIndex = -1;

                    Visit_Users_dt = v.Get_Visit_Users(V_ID);
                    if (Visit_Users_dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < Visit_Users_dt.Rows.Count; i++)
                        {
                            int u_id = Visit_Users_dt.Rows[i].Field<int>(0);
                            string V = "V";
                            string VV = "_comboBox";
                            string fullName = V + (i + 1) + VV;
                            ComboBox cbx = this.Controls.Find(fullName, true).FirstOrDefault() as ComboBox;
                            cbx.SelectedValue = u_id;
                        }
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void MicroProject_ID_textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID_textBox1.Text != "")
            {
                    DataTable dt = v.SearchByID(MicroProject_ID_textBox1.Text,true);

                    clear_Visit_boxes(0);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MicroProject_ID = Int32.Parse(dt.Rows[i]["MicroProject_ID"].ToString());

                            MicroProject_ID_textBox1.Text = MicroProject_ID.ToString();
                            Person_Name_textBox1.Text = dt.Rows[i]["Beneficiary Name"].ToString();
                            Person_ID = Int32.Parse(dt.Rows[i]["Beneficiary_ID"].ToString());
                            if (dt.Rows[i]["Loan Amount"] != DBNull.Value)
                            {
                                Double dd = Convert.ToDouble(dt.Rows[i]["Loan Amount"].ToString());
                                LoanAmount_textBox1.Text = dd.ToString();
                            }
                            if (dt.Rows[i]["Loan Date"] != DBNull.Value)
                            {
                                DateTime date = Convert.ToDateTime(dt.Rows[i]["Loan Date"].ToString());
                                LoanDate_dateTimePicker.Value = date;
                            }
                            if (dt.Rows[i]["ID"] != DBNull.Value)
                            {
                                if (dt.Rows[i]["Kind"].Equals(_KIND))
                                {
                                    V_ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
                                    fill_visit2_boxes(V_ID);
                                    
                                        //VisitType_comboBox1.Text = (string)dt.Rows[i]["Type"];
                                    //DateTime date1 = (DateTime)dt.Rows[i]["Date"];
                                    //VisitDate_dateTimePicker.Value = date1;

                                    //Ans1_textBox.Text = (string)dt.Rows[i]["Ans1"];
                                    //Ans2_textBox.Text = (string)dt.Rows[i]["Ans2"];
                                    //Ans3_textBox.Text = (string)dt.Rows[i]["Ans3"];
                                    //Ans4_textBox.Text = (string)dt.Rows[i]["Ans4"];
                                    ////Ans5_textBox.Text = (string)dt.Rows[i]["Ans5"];
                                    ////    Ans6_textBox.Text = (string)dt.Rows[0]["Ans6"];
                                    //Indicatores_textBox.Text = (string)dt.Rows[i]["Indicators"];

                                    ////Office1_textBox.Text = (string)dt.Rows[i]["o_Ans1"];
                                    ////Office2_textBox.Text = (string)dt.Rows[i]["o_Ans2"];
                                    ////Office3_textBox.Text = (string)dt.Rows[i]["o_Ans3"];
                                    ////Office4_textBox.Text = (string)dt.Rows[i]["o_Ans4"];
                                    ////Office5_textBox.Text = (string)dt.Rows[i]["o_Ans5"];
                                    ////Office6_textBox.Text = (string)dt.Rows[i]["o_Ans6"];
                                    ////OtherComments_textBox.Text = (string)dt.Rows[i]["OtherComments"];

                                    //Visit_Users_dt = v.Get_Visit_Users(V_ID);
                                    //if (Visit_Users_dt.Rows.Count != 0)
                                    //{
                                    //    for (int j = 0; j < Visit_Users_dt.Rows.Count; j++)
                                    //    {
                                    //        int u_id = Visit_Users_dt.Rows[j].Field<int>(0);
                                    //        string V = "V";
                                    //        string VV = "_comboBox";
                                    //        string fullName = V + (j + 1) + VV;
                                    //        ComboBox cbx = this.Controls.Find(fullName, true).FirstOrDefault() as ComboBox;
                                    //        cbx.SelectedValue = u_id;
                                    //    }
                                    //}
                                }
                                mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                                mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
                            }
                        }
                    }
                }
            }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        private void Person_Name_textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Person_Name_textBox1.Text != "")
            {
                    DataTable dt = v.SearchByName(Person_Name_textBox1.Text, true);
                    clear_Visit_boxes(0);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MicroProject_ID = Int32.Parse(dt.Rows[i]["MicroProject_ID"].ToString());
                            MicroProject_ID_textBox1.Text = MicroProject_ID.ToString();
                            Person_Name_textBox1.Text = dt.Rows[i]["Beneficiary Name"].ToString();
                            Person_ID = Int32.Parse(dt.Rows[i]["Beneficiary_ID"].ToString());
                            if (dt.Rows[i]["Loan Amount"] != DBNull.Value)
                            {
                                Double dd = Convert.ToDouble(dt.Rows[i]["Loan Amount"].ToString());
                                LoanAmount_textBox1.Text = dd.ToString();
                            }
                            if (dt.Rows[i]["Loan Date"] != DBNull.Value)
                            {
                                DateTime date = Convert.ToDateTime(dt.Rows[i]["Loan Date"].ToString());
                                LoanDate_dateTimePicker.Value = date;
                            }
                            if (dt.Rows[i]["ID"] != DBNull.Value)
                            {
                                if (dt.Rows[i]["Kind"].Equals(_KIND))
                                {
                                    V_ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
                                        fill_visit2_boxes(V_ID);
                               //     VisitType_comboBox1.Text = (string)dt.Rows[i]["Type"];
                               //     DateTime date1 = (DateTime)dt.Rows[i]["Date"];
                               //     VisitDate_dateTimePicker.Value = date1;

                               //     Ans1_textBox.Text = (string)dt.Rows[i]["Ans1"];
                               //     Ans2_textBox.Text = (string)dt.Rows[i]["Ans2"];
                               //     Ans3_textBox.Text = (string)dt.Rows[i]["Ans3"];
                               //     Ans4_textBox.Text = (string)dt.Rows[i]["Ans4"];
                               ////    Ans5_textBox.Text = (string)dt.Rows[i]["Ans5"];
                               //     Indicatores_textBox.Text = (string)dt.Rows[i]["Indicators"];

                               //     //Office1_textBox.Text = (string)dt.Rows[i]["o_Ans1"];
                               //     //Office2_textBox.Text = (string)dt.Rows[i]["o_Ans2"];
                               //     //Office3_textBox.Text = (string)dt.Rows[i]["o_Ans3"];
                               //     //Office4_textBox.Text = (string)dt.Rows[i]["o_Ans4"];
                               //     //Office5_textBox.Text = (string)dt.Rows[i]["o_Ans5"];
                               //     //Office6_textBox.Text = (string)dt.Rows[i]["o_Ans6"];
                               //     //OtherComments_textBox.Text = (string)dt.Rows[i]["OtherComments"];

                               //     Visit_Users_dt = v.Get_Visit_Users(V_ID);
                               //     if (Visit_Users_dt.Rows.Count != 0)
                               //     {
                               //         for (int j = 0; j < Visit_Users_dt.Rows.Count; j++)
                               //         {
                               //             int u_id = Visit_Users_dt.Rows[j].Field<int>(0);
                               //             string V = "V";
                               //             string VV = "_comboBox";
                               //             string fullName = V + (j + 1) + VV;
                               //             ComboBox cbx = this.Controls.Find(fullName, true).FirstOrDefault() as ComboBox;
                               //             cbx.SelectedValue = u_id;
                               //         }
                               //     }
                                }
                                mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                                mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
                            }
                        }
                    }
                }
            }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            try
            {
                CheckUserPermission();
                if (MicroProject_ID_textBox1.Text == "" || Person_Name_textBox1.Text == "" || MicroProject_ID == -1)
                {
                    throw new Exception("You should choose a project first ..!!");
                }
                if (VisitType_comboBox1.Text == "")
                {
                    throw new Exception("You can't leave empty fields ..!!");
                }
                ////insert Section////
                if (SelectedDataRow == null && Update_Mood == false && V_ID == -1)
                {
                    v.Insert_Visit(MicroProject_ID, _KIND, VisitType_comboBox1.Text, VisitDate_dateTimePicker.Value,
                        Ans1_textBox.Text, Ans2_textBox.Text, Ans3_textBox.Text,
                        Ans4_textBox.Text,"", "", Indicatores_textBox.Text);

                    ////////////// GET current V_ID //////////////////
                    V_ID = v.Get_Current_Visit(_KIND);
                    l.Insert_Log("Insert visit-" + _KIND + " to " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " ", " Visit ", Properties.Settings.Default.username, DateTime.Now);

                    ///////////// insert office monitoring ////////////////
                    //O.Insert_Office_Monitoring(V_ID, Office1_textBox.Text, Office2_textBox.Text,
                    //    Office3_textBox.Text, Office4_textBox.Text, Office5_textBox.Text, Office6_textBox.Text, OtherComments_textBox.Text);
                    //l.Insert_Log("Insert office-monitoring to " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " ", " Office-Monitoring ", Properties.Settings.Default.username, DateTime.Now);
                    
                    //make the visit 1 task of this project ==> checked
                    //update task ^_^
                   
                    ///////////////////////////   Task IDs = 23   ////////////////////////////////////////////
                    TasksOfProjects = new TasksOfProjects();
                    TasksOfProjects.Update_Task_MicroProject(MicroProject_ID, 23, true, DateTime.Now);
                    //////////////////////////////////////////////////////////////////////////////////////////

                }
                else          ////update section ////
                {
                    v.Update_Visit(V_ID, VisitType_comboBox1.Text, VisitDate_dateTimePicker.Value,
                        Ans1_textBox.Text, Ans2_textBox.Text, Ans3_textBox.Text,
                        Ans4_textBox.Text, "", "", Indicatores_textBox.Text);
                    l.Insert_Log("Update visit-" + _KIND + " of " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " ", " Visit ", Properties.Settings.Default.username, DateTime.Now);

                    ///////////// update office monitoring ////////////////
                    //O.Update_Office_Monitoring(V_ID, Office1_textBox.Text, Office2_textBox.Text,
                    //    Office3_textBox.Text, Office4_textBox.Text, Office5_textBox.Text, Office6_textBox.Text, OtherComments_textBox.Text);
                    //l.Insert_Log("Update office-monitoring of " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " ", " Office-Monitoring ", Properties.Settings.Default.username, DateTime.Now);

                    //remove all users of this visit from database
                    v.Delete_Visit_Users(V_ID);
                }

                string users_of_visit = "";
                //check user comboBoxs and insert
                if (V1_comboBox.Text != "")
                {
                    User_ID = Convert.ToInt32(V1_comboBox.SelectedValue);
                    v.Insert_Visit_User(V_ID, User_ID);
                    users_of_visit += V1_comboBox.Text + " ";
                }
                if (V2_comboBox.Text != "")
                {
                    User_ID = Convert.ToInt32(V2_comboBox.SelectedValue);
                    v.Insert_Visit_User(V_ID, User_ID);
                    users_of_visit += V2_comboBox.Text + " ";
                }
                if (V3_comboBox.Text != "")
                {
                    User_ID = Convert.ToInt32(V3_comboBox.SelectedValue);
                    v.Insert_Visit_User(V_ID, User_ID);
                    users_of_visit += V3_comboBox.Text + " ";
                }
                if (V4_comboBox.Text != "")
                {
                    User_ID = Convert.ToInt32(V4_comboBox.SelectedValue);
                    v.Insert_Visit_User(V_ID, User_ID);
                    users_of_visit += V4_comboBox.Text + " ";
                }
                l.Insert_Log("Insert visitors-" + users_of_visit + " of the visit-" + _KIND + " of " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " ", " Visit ", Properties.Settings.Default.username, DateTime.Now);

                //  create Evaluation form of this beneficiary //
                DialogResult dialogResult = MessageBox.Show("Do you want to add Evaluation Form to this beneficiary visit ? ", " ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        Form Evaluation_form = new Evaluation_Form(MicroProject_ID, Person_ID, Person_Name_textBox1.Text, VisitDate_dateTimePicker.Value.Date, _KIND, mainForm);
                        mainForm.showNewTab(Evaluation_form, "Evaluation Form");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                clear_Visit_boxes(1);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (V_ID == -1)
                {
                    throw new Exception("Please choose a vosot first to delete !");
                }
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this visit?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    v.Delete_Visit(V_ID);

                    ///////////////////////////   Task IDs = 23   ////////////////////////////////////////////
                    TasksOfProjects = new TasksOfProjects();
                    TasksOfProjects.Update_Task_MicroProject(MicroProject_ID, 23, false, DateTime.Now);
                    //////////////////////////////////////////////////////////////////////////////////////////


                    l.Insert_Log("Delete visit-" + _KIND + " to " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " ", " Visit ", Properties.Settings.Default.username, DateTime.Now);
                    clear_Visit_boxes(1);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        private void select_EvaluationForms_OfBeneficiary(int Beneficiary_ID)
        {
            try
            {//check connection//
                               Program.buildConnection();
                string query = "SELECT `ID`" +
                        ",FORMAT(Result,3) as 'النتيجة الحسابية'" +
                        @",CASE WHEN Result < -2 THEN N'Fail'
WHEN Result >= -2 and Result < -1 THEN N'Poor-Fail'
WHEN Result >= -1 and Result < 0 THEN N'Average-Poor'
WHEN Result = 0 THEN 'Average' 
WHEN Result > 0 and Result < +1 THEN N'Average-Good'
WHEN Result >= 1 and Result < 2 THEN N'Good-Success'
ELSE N'Success' End as 'التقييم'" +
                        ",`Profit` as 'الربح الصافي'" +
                        ",`Date` as 'تاريخ التقييم'" +
                        ",`Person_ID` FROM `evaluation`" +
                        " where Person_ID = " + Beneficiary_ID;
                //" and Date like N'" + visit_date.Year.ToString() + " / " + visit_date.Month.ToString() + " / " + visit_date.Day.ToString() + "'";

                MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
                sc.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Visits_DataGridView.Columns.Clear();
                Visits_DataGridView.ColumnHeadersVisible = false;
                Visits_DataGridView.DataSource = dt;
                Visits_DataGridView.ColumnHeadersVisible = true;

                Program.MyConn.Close();
                DataGridViewColumn dgc1 = Visits_DataGridView.Columns["ID"];
                dgc1.Visible = false;
                DataGridViewColumn dgc2 = Visits_DataGridView.Columns["Person_ID"];
                dgc2.Visible = false;

                Visits_DataGridView.Columns["تاريخ التقييم"].DefaultCellStyle.Format = "dd/MM/yyyy";
                Visits_DataGridView.Columns["الربح الصافي"].DefaultCellStyle.Format = "#,##0";

                Visits_DataGridView.Columns["النتيجة الحسابية"].DefaultCellStyle.Alignment = Visits_DataGridView.Columns["الربح الصافي"].DefaultCellStyle.Alignment =
                    Visits_DataGridView.Columns["تاريخ التقييم"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CheckUserPermission()
        {
            switch (Properties.Settings.Default.role)
            {

                case 1: { } break;  //admin
                case 2: { } break;  //Data
                case 3:             //Financial_Lawful
                    {
                        throw new Exception(" You don't have the permission for this action.");
                    }
                case 4:             //Guest
                    {
                        throw new Exception(" You don't have the permission for this action.");
                    }
                case 5: { } break;  //manager ???
            }
        }
        private void clear_Visit_boxes(int j)
        {
            V_ID = -1;
            SelectedDataRow = null;
            V1_comboBox.Text = V2_comboBox.Text = V3_comboBox.Text = V4_comboBox.Text = "";
            V1_comboBox.SelectedIndex = V2_comboBox.SelectedIndex = V3_comboBox.SelectedIndex = V4_comboBox.SelectedIndex = -1;
            Update_Mood = false;

            LoanDate_dateTimePicker.Value = VisitDate_dateTimePicker.Value = DateTime.Now;
            Ans1_textBox.Text = Ans2_textBox.Text = Ans3_textBox.Text =
                Ans4_textBox.Text =  Indicatores_textBox.Text = "";

            //Office1_textBox.Text = Office2_textBox.Text = Office3_textBox.Text =Ans5_textBox.Text =
            //    Office4_textBox.Text = Office5_textBox.Text = Office6_textBox.Text = OtherComments_textBox.Text = "";
            LoanAmount_textBox1.Text = "";
            if (j != 0)
            {
              MicroProject_ID_textBox1.Text = Person_Name_textBox1.Text = "";
                MicroProject_ID = -1;
            }

        }
        private void Visits_DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Visits_DataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    int Evaluation_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                    int Beneficiary_ID = Int32.Parse(SelectedDataRow["Person_ID"].ToString());

                    Form Evaluation_Form1 = new Evaluation_Form(Evaluation_ID, Beneficiary_ID, "O2", mainForm);
                    mainForm.showNewTab(Evaluation_Form1, "Evaluation Form");
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        #region mouse hover
        private void Save_button_MouseEnter(object sender, EventArgs e)
        {
            Image image = null;
            if (Properties.Settings.Default.theme == "Light")
                image = Properties.Resources.Save2_L;
            else image = Properties.Resources.Save2_D;

            Save_button.BackgroundImage = image;
        }
        private void Save_button_button_MouseLeave(object sender, EventArgs e)
        {
            Image image = null;
            if (Properties.Settings.Default.theme == "Light")
                image = Properties.Resources.Save2_D;
            else image = Properties.Resources.Save2_L;

            Save_button.BackgroundImage = image;
        }
        private void Delete_button_MouseEnter(object sender, EventArgs e)
        {
            Image image = null;
            if (Properties.Settings.Default.theme == "Light")
                image = Properties.Resources.Delete2_L;
            else image = Properties.Resources.Delete2_D;

            Delete_button.BackgroundImage = image;
        }
        private void Delete_button_MouseLeave(object sender, EventArgs e)
        {
            Image image = null;
            if (Properties.Settings.Default.theme == "Light")
                image = Properties.Resources.Delete2_D;
            else image = Properties.Resources.Delete2_L;

            Delete_button.BackgroundImage = image;
        }
        #endregion
    }
}
