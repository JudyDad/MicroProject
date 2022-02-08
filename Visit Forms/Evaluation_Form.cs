using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MyWorkApplication
{
    public partial class Evaluation_Form : Form
    {
        public Evaluation_Form(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            Evaluation_ID = MicroProject_ID= Beneficiary_ID = - 1;
            kind = "";
        }
        //public Evaluation_Form(DataRow DRow, MainForm mainForm)
        //{
        //    InitializeComponent();
        //    this.mainForm = mainForm;
        //    if (DRow != null)
        //    {
        //        MicroProject_ID = Int32.Parse(DRow["رقم المشروع"].ToString());
        //        MicroProject_ID_textBox1.Text = MicroProject_ID.ToString();

        //        Beneficiary_ID = Int32.Parse(DRow["رقم المستفيد"].ToString());

        //        Person_Name_textBox1.Text = DRow["المستفيد"].ToString();

        //        if (DRow["ID"] == null || DRow["ID"] == DBNull.Value)
        //            Evaluation_ID = -1;
        //        else Evaluation_ID = Int32.Parse(DRow["ID"].ToString());

        //        if (DRow["تاريخ الزيارة"] != DBNull.Value)
        //        {
        //            DateTime date = (DateTime)DRow["تاريخ الزيارة"];
        //            VisitDate_dateTimePicker.Value = date;
        //        }
        //        if (DRow["علامة التقييم"] != DBNull.Value)
        //        {
        //            Double dd = Convert.ToDouble(DRow["علامة التقييم"].ToString());
        //            E = dd;
        //            Evaluation_Result(E);
        //        }
        //        if (DRow["الربح الصافي التقريبي"] != DBNull.Value)
        //        {
        //            SimpleProfit_textBox.Text = DRow["الربح الصافي التقريبي"].ToString();
        //        }
        //        if (DRow["Category_ID"] != DBNull.Value )
        //        {
        //            if(Int32.Parse(DRow["Category_ID"].ToString()) == 1 || Int32.Parse(DRow["Category_ID"].ToString()) == 2)
        //            {
        //                kind = "T";
        //            }
        //            else  { kind = "O"; }
        //        }
        //    }
        //}
        public Evaluation_Form(int Evaluation_ID , int Person_ID, string kind, MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            this.Evaluation_ID = Evaluation_ID;
            this.Beneficiary_ID = Person_ID;
            this.kind = kind;
        }
        public Evaluation_Form(int MP_ID,int Person_ID,string P_Name,DateTime V_Date,string kind, MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.kind = kind;

            this.MicroProject_ID = MP_ID;
            MicroProject_ID_textBox1.Text = MP_ID.ToString();

            this.Beneficiary_ID = Person_ID;
            Person_Name_textBox1.Text = P_Name;

            this.Evaluation_ID = -1;

            VisitDate_dateTimePicker.Value = V_Date;

            select_EvaluationForms_OfBeneficiary(Beneficiary_ID);
        }
        
        DataRow SelectedDataRow;
        int MicroProject_ID, Factor_ID, User_ID, Beneficiary_ID, Evaluation_ID;
        NewTheme NewTheme; Log l; MainForm mainForm; Select s;
        int Positive_Count, Negative_Count, All_Count;
        int Positive_Sum, Negative_Sum;
        Double P1, P2, N1, N2, P, N, E, S;
        string query,kind, Eval_str, V1_str, V2_str;
        int mark, mark_type;
        int tick = 0;

        private void evaluation_Form1_Load(object sender, EventArgs e)
        {
            try
            {
                NewTheme = new NewTheme();
                if (Properties.Settings.Default.theme == "Light")
                {
                    NewTheme.Evaluation_ToLight(this);
                }
                else
                {
                    NewTheme.Evaluation_ToNight(this);
                }

                mainForm.Project_label.Visible = mainForm.TabName_label.Visible = true;
                mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = false;
                mainForm.TabName_label.Text = "Evaluation Form";

                l = new Log(); s = new Select();

                Person_Name_textBox1.AutoCompleteCustomSource = s.select_beneficiaries("", "Yes");
                MicroProject_ID_textBox1.AutoCompleteCustomSource = s.select_project_IDs("", "Yes");

                users_bind(V1_comboBox); users_bind(V2_comboBox);
                select_Factors(kind);

                Factors_dataGridView.Visible = M_Header_label.Visible = false;

                if (Evaluation_ID != -1)
                    timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        #region sql statments
        private void insert_factor_mark(int Evaluation_ID, int Factor_ID, int Mark, int MarkType)
        {
            //check connection//
                           Program.buildConnection();
            query = "INSERT INTO `evaluation_factor`(`Evaluation_ID`, `Factor_ID`, `Mark`, `MarkType`) VALUES ("
                        + Evaluation_ID + "," + Factor_ID + "," + Mark + "," + MarkType + ")";
            using (MySqlCommand sc = new MySqlCommand(query, Program.MyConn)) { sc.ExecuteNonQuery(); }
            Program.MyConn.Close();
        }
        private void delete_factor_mark(int Evaluation_ID)
        {
            //check connection//
                           Program.buildConnection();
            query = "delete from `evaluation_factor` where `Evaluation_ID` = " + Evaluation_ID;
            using (MySqlCommand sc = new MySqlCommand(query, Program.MyConn)) { sc.ExecuteNonQuery(); }
            Program.MyConn.Close();
        }
        private void insert_Evaluation_form(int Beneficiary_ID, DateTime Date, Double result, int profit)
        {
            //check connection//
                           Program.buildConnection();
            query = "INSERT INTO `evaluation`(`Person_ID`, `Date`, `Result` , `Profit`) VALUES ("
                        + Beneficiary_ID + ",N'"
                        + Date.Year.ToString() + "/" + Date.Month.ToString() + "/" + Date.Day.ToString() + "'"
                        + "," + result
                        + "," + profit + ")";
            using (MySqlCommand sc = new MySqlCommand(query, Program.MyConn)) { sc.ExecuteNonQuery(); }
            Program.MyConn.Close();
        }
        private void update_Evaluation_form(int Evaluation_ID, Double E, int profit)
        {
            //check connection//
                           Program.buildConnection();
            query = "UPDATE `evaluation` SET " +
                //   "`Person_ID`= " + Beneficiary_ID +
                "`Result`= " + E +
                ",`Profit`= " + profit +
                " WHERE ID = " + Evaluation_ID;
            using (MySqlCommand sc = new MySqlCommand(query, Program.MyConn)) { sc.ExecuteNonQuery(); }
            Program.MyConn.Close();
        }
        private void delete_Evaluation_form(int Evaluation_ID)
        {
            //check connection//
                           Program.buildConnection();
            query = "delete from `evaluation` where ID = " + Evaluation_ID + " ";
            using (MySqlCommand sc = new MySqlCommand(query, Program.MyConn)) { sc.ExecuteNonQuery(); }
            Program.MyConn.Close();
        }
        private void Get_Last_Evaluation_ID()
        {
            //check connection//
                           Program.buildConnection();
            query = "select MAX(ID) from `evaluation`";
            using (MySqlCommand sc = new MySqlCommand(query, Program.MyConn))
            {
                Int32.TryParse((sc.ExecuteScalar()).ToString(), out Evaluation_ID);
                //Evaluation_ID_textBox.Text = Evaluation_ID.ToString();
            }

            Program.MyConn.Close();
        }
        private void insert_Evaluation_User(int Evaluation_ID, int User_ID, double mark)
        {
            query = "INSERT INTO `evaluation_user`(`Evaluation_ID`, `User_ID`, `Mark`) values("
                + Evaluation_ID + ","
                + User_ID + ","
                + mark+ ")";

            //check connection//
                           Program.buildConnection();
            using (MySqlCommand sc = new MySqlCommand(query, Program.MyConn)) { sc.ExecuteNonQuery(); }
            Program.MyConn.Close();
        }
        private void delete_User_marks(int Evaluation_ID)
        {
            //check connection//
                           Program.buildConnection();
            query = "DELETE FROM `evaluation_user` where `Evaluation_ID` = " + Evaluation_ID + " ";
            using (MySqlCommand sc = new MySqlCommand(query, Program.MyConn)) { sc.ExecuteNonQuery(); }
            Program.MyConn.Close();
        }
        private void select_User_Marks(int Evaluation_ID)
        {
            //check connection//
                           Program.buildConnection();
            query = "SELECT `User_ID`,`UserName`,`Mark` FROM `evaluation_user` join `user` on user.UserID = evaluation_user.User_ID"
                + " WHERE `Evaluation_ID` = " + Evaluation_ID + "";
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int u_id = dt.Rows[i].Field<int>(0);
                    string V = "V";
                    string VV = "_comboBox";
                    string Tr = "_trackBar";
                    string L = "_str";
                    string fullName = V + (i + 1) + VV;
                    ComboBox cbx = this.Controls.Find(fullName, true).FirstOrDefault() as ComboBox;
                    cbx.SelectedValue = u_id;

                    string track_fullname = V + (i + 1) + Tr;
                    string lable_fullname = V + (i + 1) + L;

                    TrackBar trackBar = this.Controls.Find(track_fullname, true).FirstOrDefault() as TrackBar;
                    //Label lble = this.Controls.Find(lable_fullname, true).FirstOrDefault() as Label;

                    double mark = dt.Rows[i].Field<double>(2);
                    //lble.Text = mark.ToString();
                    lable_fullname = mark.ToString();
                    trackBar.Value = Convert.ToInt32(mark * 10.0);
                }
            }
            else
            {
                V1_comboBox.Text = V2_comboBox.Text = "";
                V1_comboBox.SelectedIndex = V2_comboBox.SelectedIndex = -1;
                V1_str= V2_str = "0";
                V1_trackBar.Value = V2_trackBar.Value = 0;
            }
        }
        private void select_Factors_Marks(int Evaluation_ID)
        {
            //check connection//
                           Program.buildConnection();
            string query = @" select factor.ID as 'ID'
 , factor.Type as 'Type'
 , evaluation_factor.Mark as 'Mark'
 , evaluation_factor.MarkType as 'Mark_Type'
 FROM `factor` LEFT outer join evaluation_factor on factor.ID = evaluation_factor.Factor_ID ";
            string condition = " where evaluation_factor.Evaluation_ID = " + Evaluation_ID;

            query += condition;
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int mark, mark_type, f_type;
            ///// clear all N & P columns /////
            for (int i = 0; i < Factors_dataGridView.Rows.Count; i++)
                Factors_dataGridView.Rows[i].Cells["N"].Value = Factors_dataGridView.Rows[i].Cells["P"].Value = null;
            /////////////////////////////////////
            if (dt != null && dt.Rows.Count > 0)
            {
                mark = mark_type = f_type = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Factor_ID = dt.Rows[i].Field<int>("ID");
                    mark = dt.Rows[i].Field<int>("Mark");
                    mark_type = dt.Rows[i].Field<int>("Mark_Type");
                    f_type = dt.Rows[i].Field<int>("Type");

                    for (int j = 0; j < Factors_dataGridView.Rows.Count; j++)
                    {
                        if (Factors_dataGridView.Rows[j].Cells["#"].Value.ToString() == Factor_ID.ToString())
                        {
                            if (mark_type == 0)    // Negative 
                                Factors_dataGridView.Rows[j].Cells["N"].Value = mark;
                            else                    // Positive 
                                Factors_dataGridView.Rows[j].Cells["P"].Value = mark;
                            break;
                        }
                    }
                }
            }
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

                //DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                //col.Name = "E_DeleteRow";
                //col.HeaderText = " ";
                //col.FlatStyle = FlatStyle.Flat;

                //Visits_DataGridView.Columns.Add(col);

                Visits_DataGridView.Columns["النتيجة الحسابية"].DefaultCellStyle.Alignment = Visits_DataGridView.Columns["الربح الصافي"].DefaultCellStyle.Alignment =
                    Visits_DataGridView.Columns["تاريخ التقييم"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //Visits_DataGridView.Columns["E_DeleteRow"].Width = 50;
                Factors_dataGridView.Visible = M_Header_label.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete ??", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (Evaluation_ID != -1 && SimpleProfit_textBox.Text != "" )
                    {
                       // Evaluation_ID = Convert.ToInt32(Visits_DataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                        delete_Evaluation_form(Evaluation_ID);

                        l.Insert_Log("Delete evaluation Form of MicroProject_ID : " + Person_Name_textBox1.Text + " ", " evaluation Form ", Properties.Settings.Default.username, DateTime.Now);
                        //Visits_DataGridView.Rows.RemoveAt(e.RowIndex);
                        //clear_boxes();
                        MicroProject_ID_textBox1_Leave(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Make_ComboBox(DataGridView gridGridView)
        {
            DataGridViewComboBoxColumn colCB = new DataGridViewComboBoxColumn();
            colCB.Name = "P";
            colCB.HeaderText = "P";
            colCB.ValueType = typeof(int);

            colCB.Items.AddRange(string.Empty, 1, 2, 3);
            colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCB.DefaultCellStyle.ForeColor = Color.Black;
            colCB.FlatStyle = FlatStyle.Flat;
            gridGridView.Columns.Add(colCB);

            DataGridViewComboBoxColumn colCA = new DataGridViewComboBoxColumn();
            colCA.Name = "N";
            colCA.HeaderText = "N";
            colCA.ValueType = typeof(int);

            colCA.Items.AddRange(string.Empty, 1, 2, 3);
            colCA.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCA.DefaultCellStyle.ForeColor = Color.Black;
            colCA.FlatStyle = FlatStyle.Flat;
            gridGridView.Columns.Add(colCA);

        }
        private void select_Factors(string kind)
        {
            string where = "";
            if (kind.Contains("T"))
                where = " or Kind like 'T'";
            else
                where = " or Kind like 'O'";
            //check connection//
                           Program.buildConnection();
            string query = @" select factor.ID as '#'
,factor.`Name` as 'العامل'" +
" FROM `factor` where Kind like 'OT' " + where + " order by ID asc ";
            MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
            sc.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Factors_dataGridView.DataSource = null;
            Factors_dataGridView.Columns.Clear();
            Factors_dataGridView.Rows.Clear();

            Factors_dataGridView.ColumnHeadersVisible = false;
            Factors_dataGridView.DataSource = dt;
            Factors_dataGridView.ColumnHeadersVisible = true;

            DataGridViewColumn dgc1 = Factors_dataGridView.Columns["#"];
            dgc1.Visible = false;

            Factors_dataGridView.Columns["العامل"].ReadOnly = true;

            Program.MyConn.Close();

            // Add 2 columns N & P //
            Make_ComboBox(Factors_dataGridView);
            
            Factors_dataGridView.Columns["P"].MinimumWidth = 20;
            Factors_dataGridView.Columns["العامل"].MinimumWidth = 70;
            Factors_dataGridView.Columns["العامل"].FillWeight = 60;
            Factors_dataGridView.Columns["P"].Width = Factors_dataGridView.Columns["N"].Width = 30;
            Factors_dataGridView.Columns["P"].MinimumWidth = Factors_dataGridView.Columns["N"].MinimumWidth = 30;
            Factors_dataGridView.Columns["P"].FillWeight = Factors_dataGridView.Columns["N"].FillWeight = 20;

        }
        
        private void MicroProject_ID_textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID_textBox1.Text != "")
                {
                    //check connection//
                                   Program.buildConnection();
                    string query = "select PMP.MicroProject_ID as 'MicroProject_ID'"
                        + ", CONCAT(P_FirstName, ' ', P_LastName, ' ابن/ة ', P_FatherName) as 'Beneficiary Name'"
                        + " ,MP_Category_ID as 'Category_ID'"
                        + " ,P1.P_ID 'Beneficiary_ID'"
                        + " ,M.ID as 'Evaluation_ID'"
                        + " from person_microproject PMP left outer join person P1 on P1.P_ID = PMP.Person_ID "
                        + " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID"
                        + " left outer join evaluation M on M.Person_ID = P1.P_ID "
                    + " where PMP.MicroProject_ID = " + Convert.ToInt32(MicroProject_ID_textBox1.Text);

                    MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
                    sc.ExecuteNonQuery();
                    MySqlDataAdapter da = new MySqlDataAdapter(sc);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Program.MyConn.Close();

                    clear_boxes();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            MicroProject_ID = Int32.Parse(dt.Rows[0]["MicroProject_ID"].ToString());
                            Beneficiary_ID = Int32.Parse(dt.Rows[0]["Beneficiary_ID"].ToString());
                            MicroProject_ID_textBox1.Text = MicroProject_ID.ToString();
                            Person_Name_textBox1.Text = dt.Rows[0]["Beneficiary Name"].ToString();

                            if (dt.Rows[0]["Category_ID"] != DBNull.Value)
                            {
                                if (Int32.Parse(dt.Rows[0]["Category_ID"].ToString()) == 1 || Int32.Parse(dt.Rows[0]["Category_ID"].ToString()) == 2)
                                {
                                    kind = "T";
                                }
                                else { kind = "O"; }
                                select_Factors(kind);
                            }
                            
                            select_EvaluationForms_OfBeneficiary(Beneficiary_ID);

                            mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                            mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Person_Name_textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Person_Name_textBox1.Text != "")
                {
                    //check connection//
                                   Program.buildConnection();
                    string query = "select PMP.MicroProject_ID as 'MicroProject_ID'" +
                        ", CONCAT(P_FirstName, ' ', P_LastName, ' ابن/ة ', P_FatherName) as 'Beneficiary Name'"
                        + " ,P1.P_ID 'Beneficiary_ID'"
                        + " ,M.ID as 'Evaluation_ID'"
                        + ", M.Date as 'M_Date'"
                        + ", M.Result as 'M_Result'"
                        + ", M.Profit as 'M_Profit'"
                        + ", MP_Category_ID as 'Category_ID'"
                        + " from person_microproject PMP left outer join person P1 on P1.P_ID = PMP.Person_ID "
                        + " left outer join microproject MP on PMP.MicroProject_ID = MP.MP_ID"
                        + " left outer join evaluation M on M.Person_ID = P1.P_ID "
                + " WHERE CONCAT(TRIM(P1.P_FirstName),' ', TRIM(P1.P_LastName),' ابن/ة ',TRIM(P1.P_FatherName)) LIKE '%" + Person_Name_textBox1.Text + "%'";

                    MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
                    sc.ExecuteNonQuery();
                    MySqlDataAdapter da = new MySqlDataAdapter(sc);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    Program.MyConn.Close();

                    clear_boxes();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            MicroProject_ID = Int32.Parse(dt.Rows[0]["MicroProject_ID"].ToString());
                            Beneficiary_ID = Int32.Parse(dt.Rows[0]["Beneficiary_ID"].ToString());
                            MicroProject_ID_textBox1.Text = MicroProject_ID.ToString();
                            Person_Name_textBox1.Text = dt.Rows[0]["Beneficiary Name"].ToString();

                            if (dt.Rows[0]["Category_ID"] != DBNull.Value)
                            {
                                if (Int32.Parse(dt.Rows[0]["Category_ID"].ToString()) == 1 || Int32.Parse(dt.Rows[0]["Category_ID"].ToString()) == 2)
                                {
                                    kind = "T";
                                }
                                else { kind = "O"; }
                                select_Factors(kind);
                            }
                            
                            select_EvaluationForms_OfBeneficiary(Beneficiary_ID);

                            mainForm.ProjectNumber_label.Visible = mainForm.MP_ID_label.Visible = true;
                            mainForm.MP_ID_label.Text = MicroProject_ID.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void clear_boxes()
        {
            Evaluation_ID = Factor_ID = -1;
            Beneficiary_ID = MicroProject_ID = -1;
            MicroProject_ID_textBox1.Text = Person_Name_textBox1.Text = SimpleProfit_textBox.Text = "";
            V1_str = V2_str = "";
            V1_comboBox.Text = V2_comboBox.Text = "";
            V1_comboBox.SelectedIndex = V2_comboBox.SelectedIndex = -1;
            V1_trackBar.Value = V2_trackBar.Value =Evaluation_trackBar.Value= 0;
            
            SimpleProfit_textBox.Text = Eval_str = "";
            ///// clear all N & P columns /////
            for (int i = 0; i < Factors_dataGridView.Rows.Count; i++)
                Factors_dataGridView.Rows[i].Cells["N"].Value = Factors_dataGridView.Rows[i].Cells["P"].Value = null;
            /////////////////////////////////////
            if (Visits_DataGridView.Rows.Count > 1)
                Visits_DataGridView.DataSource = null;
            kind = "";

            Factors_dataGridView.Visible = M_Header_label.Visible = false;

        }
        private void Evaluation_Result(double E)
        {
            if (E < -2f) Eval_str = "Fail";
            else if (E >= -2f && E < -1f) Eval_str = "Poor-Fail";
            else if (E >= -1f && E < 0f) Eval_str = "Average-Poor";
            else if (E == 0f) Eval_str = "Average";
            else if (E >= 0f && E < +1f) Eval_str = "Average-Good";
            else if (E >= 1f && E < 2f) Eval_str = "Good-Success";
            else if (E >= 2f) Eval_str = "Success";

            E = Math.Round(E, 3);

            //show result on track bar //
            //E_lable.Text = E.ToString();
            double trackbar_value = E * 10.0;
            if (trackbar_value > 30) trackbar_value = 30;
            else if(trackbar_value < -30) trackbar_value = -30;
            else trackbar_value = E * 10.0;
            Evaluation_trackBar.Value = Convert.ToInt32(trackbar_value);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            V1_str = (V1_trackBar.Value / 10f).ToString();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            V2_str = (V2_trackBar.Value / 10f).ToString();
        }

        private void SimpleProfit_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (SimpleProfit_textBox.Text.ToString() != "")
                {
                    SimpleProfit_textBox.Text =
                        Regex.Replace(String.Format("{0:n" + 4 + "}", Convert.ToDecimal(SimpleProfit_textBox.Text.ToString())),
                            @"[" + System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

                    SimpleProfit_textBox.SelectionStart = SimpleProfit_textBox.Text.Length;
                    SimpleProfit_textBox.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SimpleProfit_textBox.Text == "")
                { 
                    throw new Exception("Please Enter a valid profit and try again!");
                }
                if (V1_comboBox.Text == "" || V2_comboBox.Text == "")
                {
                    throw new Exception("Please Enter a visitors names and marks !");
                }
                if (Person_Name_textBox1.Text == "" || MicroProject_ID_textBox1.Text == "")
                {
                    throw new Exception("Please choose a Beneficiary first !");
                }

                Positive_Count = Negative_Count = Positive_Sum = Negative_Sum = 0;
                All_Count = Factors_dataGridView.Rows.Count;
                DateTime Evaluation_Date = VisitDate_dateTimePicker.Value;
                int Profit = Convert.ToInt32(SimpleProfit_textBox.Text.Replace(",", ""));

                if (Evaluation_ID == -1 || Evaluation_ID == 0)
                {
                    insert_Evaluation_form(Beneficiary_ID, Evaluation_Date, -10f, Profit);
                    l.Insert_Log("Insert evaluation Form to " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " ", " evaluation Form ", Properties.Settings.Default.username, DateTime.Now);
                    Get_Last_Evaluation_ID();
                }
                else delete_factor_mark(Evaluation_ID);

                mark = 0;
                for (int i = 0; i < Factors_dataGridView.Rows.Count; i++)
                {
                    Factor_ID = Convert.ToInt32(Factors_dataGridView.Rows[i].Cells["#"].Value);
                    if (Factors_dataGridView.Rows[i].Cells["P"].Value != null && Factors_dataGridView.Rows[i].Cells["P"].Value != DBNull.Value && Factors_dataGridView.Rows[i].Cells["P"].Value.ToString() != "0")
                    {
                        mark = Convert.ToInt32(Factors_dataGridView.Rows[i].Cells["P"].Value);
                        mark_type = 1;
                        Positive_Count++;
                        Positive_Sum += Convert.ToInt32(Factors_dataGridView.Rows[i].Cells["P"].Value);
                        //insert into database//
                        insert_factor_mark(Evaluation_ID, Factor_ID, mark, mark_type);
                    }
                    else if (Factors_dataGridView.Rows[i].Cells["N"].Value != null && Factors_dataGridView.Rows[i].Cells["N"].Value != DBNull.Value &&  Factors_dataGridView.Rows[i].Cells["N"].Value.ToString() != "0")
                    {
                        mark = Convert.ToInt32(Factors_dataGridView.Rows[i].Cells["N"].Value);
                        mark_type = 0;
                        Negative_Count++;
                        Negative_Sum += Convert.ToInt32(Factors_dataGridView.Rows[i].Cells["N"].Value);
                        //insert into database//
                        insert_factor_mark(Evaluation_ID, Factor_ID, mark, mark_type);
                    }
                }
                l.Insert_Log("Insert Factors Marks of evaluation Form:"+Evaluation_ID+ " Of " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " ", " Factor-evaluation ", Properties.Settings.Default.username, DateTime.Now);

                if (Profit < 50000) S = 0;
                else if (Profit >= 50000 && Profit < 100000) S = 0.5;
                else if (Profit >= 100000 && Profit < 150000) S = 1;
                else if (Profit >= 150000 && Profit < 200000) S = 1.5;
                else if (Profit >= 200000) S = 2;

                if (Positive_Sum == 0 && Positive_Count == 0)
                    P1 = 0;
                else P1 = (Double)Positive_Sum / (Double)Positive_Count;
                P2 = (Double)Positive_Sum / (Double)(All_Count - Positive_Count);

                if (Negative_Sum == 0 && Negative_Count == 0)
                    N1 = 0;
                else N1 = (Double)Negative_Sum / (Double)Negative_Count;
                N2 = (Double)Negative_Sum / (Double)(All_Count - Negative_Count);

                P = (Double)(P1 * P2);
                N = (Double)(N1 * N2);
                E = P - N + S;

                Evaluation_Result(E);

                //insert evaluation result into this evaluation page in database//
                update_Evaluation_form(Evaluation_ID, E, Profit);

                //link user Evalution to it//
                double user_mark = -10;
                //delete old marks to add the new ones
                delete_User_marks(Evaluation_ID);
                
                //check user comboBoxs
                if (V1_comboBox.Text != "")
                {
                    User_ID = Convert.ToInt32(V1_comboBox.SelectedValue);

                    V1_str = (V1_trackBar.Value / 10f).ToString();
                    user_mark = Convert.ToDouble(V1_str);

                    insert_Evaluation_User(Evaluation_ID, User_ID, user_mark);
                    l.Insert_Log("Insert Mark="+ V1_str + "of User:"+ V1_comboBox .Text + " to evaluation Form:" + Evaluation_ID + " Of " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " "
                        , " User-evaluation ", Properties.Settings.Default.username, DateTime.Now);

                }
                if (V2_comboBox.Text != "")
                {
                    User_ID = Convert.ToInt32(V2_comboBox.SelectedValue);

                    V2_str = (V2_trackBar.Value / 10f).ToString();
                    user_mark = Convert.ToDouble(V2_str);

                    insert_Evaluation_User(Evaluation_ID, User_ID, user_mark);
                    l.Insert_Log("Insert Mark=" + V2_str + "of User:" + V2_comboBox.Text + " to evaluation Form:" + Evaluation_ID + " Of " + MicroProject_ID + " : " + Person_Name_textBox1.Text + " "
                        , " User-evaluation ", Properties.Settings.Default.username, DateTime.Now);
                }
                select_EvaluationForms_OfBeneficiary(Beneficiary_ID);

                //  create Evaluation form of this beneficiary //
                DialogResult dialogResult = MessageBox.Show("Do you want to add Evaluation Form to this beneficiary visit ? ", " ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    clear_boxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        #region datagridview tools
        private void Factor_dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        private void Visits_DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Visits_DataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    Factors_dataGridView.Visible = M_Header_label.Visible = true;
                    
                    Evaluation_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                    select_Factors_Marks(Evaluation_ID);
                    V1_comboBox.Text = V2_comboBox.Text = "";
                    V1_comboBox.SelectedIndex = V2_comboBox.SelectedIndex = -1;
                    select_User_Marks(Evaluation_ID);
                    
                    if (SelectedDataRow["تاريخ التقييم"] != DBNull.Value)
                    {
                        DateTime date = (DateTime)SelectedDataRow["تاريخ التقييم"];
                        VisitDate_dateTimePicker.Value = date;
                    }
                    if (SelectedDataRow["النتيجة الحسابية"] != DBNull.Value)
                    {
                        Double dd = Convert.ToDouble(SelectedDataRow["النتيجة الحسابية"].ToString());
                        E = dd;
                        Evaluation_Result(E);
                    }
                    if (SelectedDataRow["الربح الصافي"] != DBNull.Value)
                    {
                        SimpleProfit_textBox.Text = SelectedDataRow["الربح الصافي"].ToString();
                    }

                }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        #endregion

        private void fill_selected_evaluation_data()
        {
            try
            {
                if (Evaluation_ID != -1 && Beneficiary_ID != -1)
                {
                    select_EvaluationForms_OfBeneficiary(Beneficiary_ID);
                    Factors_dataGridView.Visible = M_Header_label.Visible = true;
                    select_Factors_Marks(Evaluation_ID);
                    V1_comboBox.Text = V2_comboBox.Text = "";
                    V1_comboBox.SelectedIndex = V2_comboBox.SelectedIndex = -1;
                    select_User_Marks(Evaluation_ID);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tick == 0)
            {
                fill_selected_evaluation_data();
                tick++;
            }
            else
                timer1.Stop();
        }

        private void Save_button_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.theme == "Light")
                Save_button.BackgroundImage = Properties.Resources.Save2_L;
            else
                Save_button.BackgroundImage = Properties.Resources.Save2_D;
        }
        private void Save_button_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.theme == "Light")
                Save_button.BackgroundImage = Properties.Resources.Save2_D;
            else
                Save_button.BackgroundImage = Properties.Resources.Save2_L;
        }

    }
}
