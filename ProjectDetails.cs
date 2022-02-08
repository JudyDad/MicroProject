using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyWorkApplication.Classes;

namespace MyWorkApplication
{
    public partial class ProjectDetails : Form
    {
        public ProjectDetails()
        {
            InitializeComponent();
        }
        public ProjectDetails(string username,int MicroProject_ID)
        {
            InitializeComponent();
            this.MicroProject_ID = MicroProject_ID;
            this.username = username;
        }

        MySqlComponents MySS;
        string username;
        DataRow SelectedDataRow;
        private ImageConverter converter;
        private Log l;
        private int MicroProject_ID, Person_ID, PMP_ID;
        private int Intermidate_Priest_ID;
        private int Budget_ID, Item_ID;
        private int Plan_ID, Activity_ID, image_ID;
        private int Level_ID;
        private Double sumOfItemsPrice_SYR;
        private string image_type, imageName, Image_Path;
        private int File_ID;
        private string full_file_name, file_extention, file_name_without_extention, file_name, destinationFolderPath, file_Path;
        private OpenFileDialog open;
        private Thread myTh;
        private byte[] arr;
        FtpConnector c = new FtpConnector();

        private void ProjectDetails_Load(object sender, EventArgs e)
        {
            try
            {
                MyTheme myTheme = new MyTheme();
                if (Properties.Settings.Default.theme == "Light")
                    myTheme.ProjectDetails_ToLight(Main_panel);
                else
                    myTheme.ProjectDetails_ToNight(Main_panel);


                MySS = new MySqlComponents();
                l = new Log();
                sumOfItemsPrice_SYR = 0;

                MicroProject_Intermidiate_bind();
                Budget_Item_bind();
                // Plan_Activity_bind();
                Person_MicroProject_bind();
                // Image_bind(MicroProject_ID.ToString());
                File_bind(MicroProject_ID.ToString());
                MicroProjectID_textBox.Text = MicroProject_ID.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region inserts
        private void insertIntermediarySide(bool isPriest, int mpNo)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `intermediaryside`(`IS_Name`, `IS_Job`, `IS_Phone`, `IS_Email`, `IS_Address`, `IS_isPriest`, `MicroProject_ID`) values(N'"
            + Name_textBox.Text + "',N'"
            + Job_textBox.Text + "',N'"
            + Phone_textBox.Text + "',N'"
            + "  " + "',N'"
            + "  " + "','"
            + (isPriest == true ? "Yes" : "NO" ) + "',"
            + mpNo + ")" ;
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void insertPerson_MicroProject(int personID, int microProjectID, string type)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "insert into `person_microproject`(`Person_ID`, `MicroProject_ID`, `PersonType`) values ("
                            + personID + ","
                            + microProjectID + ",N'"
                            + type + "' " +
                            ")";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void insertProjectPlan(int microProjectID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `projectplan`(`PP_Name`, `MicroProject_ID`) values(" + "null" + "," + microProjectID + ")";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void insertActivity(string Aname, string AM1, string AM2, string AM3, string AM4)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `activity`(`A_Name`, `A_Month1`, `A_Month2`, `A_Month3`, `A_Month4`) values(N'"
                                      + Aname + "',N'"
                                      + AM1 + "',N'"
                                      + AM2 + "',N'"
                                      + AM3 + "',N'"
                                      + AM4 + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void insertActivityPlan(int ANo, int PPNo)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `activity_plan`(`Activity_ID`, `ProjectPlan_ID`) values("
                                      + "(select `A_ID` from `activity` where `A_ID` = " + ANo + "),"
                                      + "(select `PP_ID` from `projectplan` where `PP_ID` = " + PPNo + "))";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void insertItem(string Iname, string IlocalCont, double Lprice, string Lcomment)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `item`(`I_Name`, `I_LocalContribution`, `I_Price`, `I_Comment`) values(N'"
                                      + Iname + "',N'"
                                      + IlocalCont + "',"
                                      + Lprice + ",N'"
                                      + Lcomment + "')";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void insertBudget()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `budget`(`B_EuroToSyrPrice`, `B_DateOfExchange`, `MicroProject_ID`) values("
                                      + 500 + ",'"
                                      + DateTime.Now.Month.ToString() + "/"
                                      + DateTime.Now.Day.ToString() + "/"
                                      + DateTime.Now.Year.ToString() + "',"
                                      + MicroProject_ID + ")";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        private void insertBudgetItem(int bNo, int iNo, Double amount)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "Insert Into `budget_item`(`Budget_ID`, `Item_ID`, `Item_Amount`) values("
                                      + "(select `B_ID` from `budget` where `B_ID` = " + bNo + "),"
                                      + "(select `I_ID` from `item` where `I_ID` = " + iNo + "),"
                                      + amount
                                      + ")";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }

        public void insertImage(int MP_ID, string path, byte[] array, string type)
        {
            //MySS.sc = new MySqlCommand("select Max(Image_ID) from `image`", Program.MyConn);
            //Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out image_ID);
            //string destinationFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //check connection//
            Program.buildConnection();

            string insertImg = "Insert Into `image`(`Image_Content`, `Image_Path`, `Image_Type`, `MicroProject_ID`) values("
                                + "@Image_Content" + ",N'"
                                + path + "',N'"
                                + type + "',"
                                + MP_ID + ")";

            MySS.sc = new MySqlCommand(insertImg, Program.MyConn);
            MySS.sc.Parameters.Add(new MySqlParameter("@Image_Content", array));
            MySS.sc.ExecuteNonQuery();
        }
        public void insertFile(int MP_ID, string path, byte[] array)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "insert into `file`(File_Name,File_Content,MicroProject_ID) values(@file_name,@file_content,@MicroProject_ID)";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySqlParameter param = MySS.sc.Parameters.Add("@file_name", MySqlDbType.LongText);
            param.Value = path;
            param = MySS.sc.Parameters.Add("@file_content", MySqlDbType.Blob);
            param.Value = array;
            param = MySS.sc.Parameters.Add("@MicroProject_ID", MySqlDbType.Int32);
            param.Value = MP_ID;
            MySS.sc.ExecuteNonQuery();
        }
        #endregion

        #region deletes
        private void Delete_Intermidiary(int IS_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `intermediaryside` where IS_ID = " + IS_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void Delete_Item(int I_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `item` where I_ID = " + I_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void Delete_Activity(int A_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query= "delete from `activity` where A_ID = " + A_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void Delete_PersonFromMicroProject(int PMP)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `person_microproject` where PMP_ID =" + PMP + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void deleteImage(int image_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query= "delete from `image` where `Image_ID` = " + image_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        private void deleteFile(int file_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "delete from `file` where `File_ID` = " + file_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
        }
        #endregion

        #region deletes button click
        private void DeleteIntermidate_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDataRow == null)
                    throw new Exception("please choose the Intermidate you what to update");
                Delete_Intermidiary(Intermidate_Priest_ID);
                MicroProject_Intermidiate_bind();
                Name_textBox.Text = Job_textBox.Text = Phone_textBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void DeleteItem_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                Delete_Item(Item_ID);
                Budget_Item_bind();

                ItemName_textBox.Text = ItemAmount_textBox.Text = ItemPrice_textBox.Text =
                    ItemLocalContribution_textBox.Text = ItemComment_textBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeletePersonProject_button_Click(object sender, EventArgs e)
        {
            try
            {
                Delete_PersonFromMicroProject(PMP_ID);
                Person_MicroProject_bind();
                PersonName_textBox.Text = "";
                MP_Owner_radioButton.Checked = MP_Partner_radioButton.Checked = false;
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }
        private void ImageDelete_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (image_ID == -1 || SelectedDataRow == null)
                {
                    throw new Exception("please choose the Image you what to delete");
                }
                deleteImage(image_ID);
                
                l.Insert_Log("Delete Image of : " + MicroProject_ID + " ", "Image", username, DateTime.Now);

                //delete from server//
                c.Delete(Image_Path);
                /////////////////////

                //Image_bind(MicroProject_ID.ToString());
                ImageLocation_textBox.Text = "";
                pictureBox.Image = null;
                image_ID = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FileDelete_button_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (File_ID == -1 || SelectedDataRow == null)
                {
                    throw new Exception("please choose the File you what to delete");
                }
                deleteFile(File_ID);

                l.Insert_Log("Delete File of : " + MicroProject_ID + " ", "File", username, DateTime.Now);

                //delete from server//
                c.Delete(file_Path);
                /////////////////////

                File_bind(MicroProject_ID.ToString());
                ImageLocation_textBox.Text = "";
                //pictureBox.Image = null;
                File_ID = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion deletes button click

        #region inserts button click
        private void InsertIntermidate_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID == -1)
                    throw new Exception("Please choose the project you want to add to it");
                insertIntermediarySide(false, MicroProject_ID);
                
                MicroProject_Intermidiate_bind();
                Name_textBox.Text = Job_textBox.Text = Phone_textBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void AddItem_button_Click(object sender, EventArgs e)
        {
            try
            {

                if (ItemName_textBox.Text == "" || ItemAmount_textBox.Text == "" || ItemLocalContribution_textBox.Text == "" || ItemPrice_textBox.Text == "")
                {
                    throw new NoNullAllowedException();
                }
                Double itemOverAllsyr = 0;
                Double itemOverAlleuro = 0;

                Double realAmount = Double.Parse(ItemAmount_textBox.Text) - Double.Parse(ItemLocalContribution_textBox.Text);
                string I_Comment_string = "..";

                itemOverAllsyr = realAmount * Double.Parse(ItemPrice_textBox.Text);

                //save the overAll for each item
                sumOfItemsPrice_SYR += itemOverAllsyr;

                //ItemOverallSyrian_label.Text += itemOverAllsyr + "\n";
                itemOverAlleuro = itemOverAllsyr / 500.00;
                //ItemOverallEuro_label.Text += itemOverAlleuro + "\n";

                OverallSyrian_label.Text = sumOfItemsPrice_SYR.ToString();
                OverallEuro_label.Text = (sumOfItemsPrice_SYR / 500.00).ToString();

                if (ItemComment_textBox.Text != "")
                    I_Comment_string = ItemComment_textBox.Text;

                insertItem(ItemName_textBox.Text, ItemLocalContribution_textBox.Text, Double.Parse(ItemPrice_textBox.Text.ToString()), I_Comment_string);
                GetCurrentItemId();
                GetCurrentBudgetId();
                insertBudgetItem(Budget_ID, Item_ID, Double.Parse(ItemAmount_textBox.Text.ToString()));

                Budget_Item_bind();

                ItemName_textBox.Text = ItemAmount_textBox.Text = ItemPrice_textBox.Text =
                    ItemLocalContribution_textBox.Text = ItemComment_textBox.Text = "";
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("You can't leave empty fields");
            }
        }
        private void InsertPersonProject_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Person_ID == -1)
                {
                    throw new Exception("Please choose the beneficiary of this project");
                }
                if (MicroProject_ID == -1)
                    throw new Exception("Please choose the project you want to add to it");
                if (MP_Owner_radioButton.Checked)
                {
                    insertPerson_MicroProject(Person_ID, MicroProject_ID, "مستفيد");
                }
                else
                    insertPerson_MicroProject(Person_ID, MicroProject_ID, "شريك");

                Person_MicroProject_bind();

                PersonName_textBox.Text = "";
                MP_Owner_radioButton.Checked = MP_Partner_radioButton.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FileSave_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (MicroProject_ID == -1)
                {
                    throw new Exception("Please choose the project you want to add to it");
                }
                //if (Files_radioButton.Checked)          //insert file//
                //{
                    //File.Copy(open.FileName, destinationFolderPath + "\\Micro_Project_File " + File_ID + file_extention);
                    string ftpPath = ImageLocation_textBox.Text;
                    string fullFtpPath = "ftp://judy@hcsyria.org" + ftpPath;
                    
                    byte[] file_content = File.ReadAllBytes(full_file_name);

                    insertFile(MicroProject_ID, fullFtpPath, file_content);
                    l.Insert_Log("Insert a file to the project :" + MicroProjectID_textBox.Text, "Files", username, DateTime.Now);

                    //upload image to the server
                    c.Upload(fullFtpPath, full_file_name);
                    l.Insert_Log("Upload file " + ImageLocation_textBox.Text + " to the server: ", "Files", username, DateTime.Now);
                    
                    ImageLocation_textBox.Clear();
                    file_name = "";
                    File_bind(MicroProject_ID.ToString());
                //}
                //else                               //insert image//
                //{
                //    if (pictureBox1.Image == null)
                //    {
                //        throw new Exception("Please choose the image you want to add");
                //    }
                //    Convert_Picture();
                //    if (image_type == "")
                //    {
                //        throw new Exception("Please choose the image type you want");
                //    }
                    
                //    if (Priest_radioButton.Checked)
                //    { image_type = "Priest"; }
                //    else if (EconomicFeasibility_radioButton.Checked)
                //    { image_type = "EconomicFeasibility"; }
                //    else
                //    { image_type = "EconomicFeasibility"; }

                //    string ftpPath = ImageLocation_textBox.Text;
                //    string fullFtpPath = "ftp://judy@hcsyria.org" + ftpPath;
                    
                //    insertImage(MicroProject_ID, fullFtpPath, arr, image_type);
                //    l.Insert_Log("insert image to: " + MicroProject_ID + " ", "Images", username, DateTime.Now);

                //    //upload image to the server
                //    c.Upload(fullFtpPath, full_file_name);
                //    l.Insert_Log("Upload image " + ImageLocation_textBox.Text + " to the server: ", "Images", username, DateTime.Now);

                //    //insertImage(MicroProject_ID, full_file_name, arr, image_type);
                //    //l.Insert_Log("insert image to: " + MicroProject_ID + " ", "Images", username, DateTime.Now);

                //    ImageLocation_textBox.Text = "";
                //    pictureBox1.Image = null;
                //    imageName = "";
                //    Image_bind(MicroProject_ID.ToString());

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion inserts button click

        #region bind
        
        private void MicroProject_Intermidiate_bind()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select IS_ID as 'ID'" +
                ",IS_Name as 'Name'" +
                ",IS_Job as 'Job'" +
                ",IS_Phone as 'Phone'" +
                "\n from `intermediaryside` where MicroProject_ID = " + MicroProject_ID + " ";

            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            ProjectIntermidate_dataGridView.ColumnHeadersVisible = false;
            ProjectIntermidate_dataGridView.DataSource = MySS.dt;
            ProjectIntermidate_dataGridView.ColumnHeadersVisible = true;
            
            DataGridViewColumn dgC5 = ProjectIntermidate_dataGridView.Columns["ID"];
            dgC5.Visible = false;
        }
        private void Budget_Item_bind()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select BI.Item_ID as 'Item_ID'" +
                        ",I.I_Name as 'Item Name'" +
                        ",I.I_Price as 'Item Price'" +
                        ",BI.Item_Amount as 'Amount'" +
                        ",I.I_LocalContribution as 'Local Contribution'" +
                        ",I.I_Comment as 'Other Comments'" +
                        ",BI.Budget_ID as 'ID'" +
                        "\n from `item` I right outer join `budget_item` BI on BI.Item_ID = I.I_ID " +
                        "left outer join `budget` B on BI.Budget_ID = B.B_ID" +
                        "\n where B.MicroProject_ID = " + MicroProject_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            MP_Item_dataGridView.ColumnHeadersVisible = false;
            MP_Item_dataGridView.DataSource = MySS.dt;
            MP_Item_dataGridView.ColumnHeadersVisible = true;

            DataGridViewColumn dgC3 = MP_Item_dataGridView.Columns["Item_ID"];
            dgC3.Visible = false;
        }
        
        private void Person_MicroProject_bind()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select PMP.PMP_ID as 'ID'"
                + ",PMP.MicroProject_ID as 'MicroProject_ID'"
                + ",PMP.Person_ID as 'Person_ID'"
                + ",CONCAT(P1.P_FirstName, ' ', P1.P_FatherName, ' ', P1.P_LastName) as 'Beneficiary Name'"
                //+ ",(P1.P_FirstName + ' ' + P1.P_FatherName + ' ' + P1.P_LastName) '"
                + ",PMP.PersonType as 'Type'"

                + "\n from `person_microproject` PMP left outer join `person` P1 on P1.P_ID = PMP.Person_ID "
                + "\n where PMP.MicroProject_ID = " + MicroProject_ID + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);

            PersonDataGridView.ColumnHeadersVisible = false;
            PersonDataGridView.DataSource = MySS.dt;
            PersonDataGridView.ColumnHeadersVisible = true;

            DataGridViewColumn dgC1 = PersonDataGridView.Columns["ID"];
            dgC1.Visible = false;
            DataGridViewColumn dgC2 = PersonDataGridView.Columns["Person_ID"];
            dgC2.Visible = false;
        }
        //private void Image_bind(string MP_ID)
        //{
        //    MySS.query = "SELECT Image_ID as 'ID'" +
        //            ",Image_Path as 'Image Path'" +
        //            ",Image_Type as 'Image Type'" +
        //            ",MicroProject_ID as 'MicroProject_ID'" +
        //            " \n FROM `image` " +
        //            " \n Where MicroProject_ID = " + Int32.Parse(MP_ID) + " ";
        //    MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
        //    MySS.sc.ExecuteNonQuery();
        //    MySS.da = new MySqlDataAdapter(MySS.sc);
        //    MySS.dt = new DataTable();
        //    MySS.da.Fill(MySS.dt);
        //    Images_dataGridView.DataSource = MySS.dt;
        //    DataGridViewColumn dgC2 = Images_dataGridView.Columns["ID"];
        //    dgC2.Visible = false;
        //}
        private void File_bind(string MP_ID)
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "SELECT File_ID as 'ID'" +
                    ",File_Name as 'File Path'" +
                    ",MicroProject_ID as 'MicroProject_ID'" +
                    " \n FROM `file` " +
                    " \n Where MicroProject_ID = " + Int32.Parse(MP_ID) + " ";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            MySS.sc.ExecuteNonQuery();
            MySS.da = new MySqlDataAdapter(MySS.sc);
            MySS.dt = new DataTable();
            MySS.da.Fill(MySS.dt);
            Files_dataGridView.DataSource = MySS.dt;
            DataGridViewColumn dgC2 = Files_dataGridView.Columns["ID"];
            dgC2.Visible = false;
            DataGridViewColumn dgC1 = Files_dataGridView.Columns["MicroProject_ID"];
            dgC1.Visible = false;
        }
        #endregion

        #region datagridviews
        private void ProjectIntermidate_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedDataRow = ((DataRowView)ProjectIntermidate_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                Intermidate_Priest_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                Name_textBox.Text = SelectedDataRow["Name"].ToString();
                Job_textBox.Text = SelectedDataRow["Job"].ToString();
                Phone_textBox.Text = SelectedDataRow["Phone"].ToString();
            }
        }
        private void PersonDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedDataRow = ((DataRowView)PersonDataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                PMP_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                PersonName_textBox.Text = SelectedDataRow["Beneficiary Name"].ToString();
                string Type = (string)SelectedDataRow["Type"];
                if (Type.Contains(@"م"))
                    MP_Owner_radioButton.Checked = true;
                else
                    MP_Partner_radioButton.Checked = true;
                SelectedDataRow = null;
            }
        }
       
        private void Files_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectedDataRow = ((DataRowView)Files_dataGridView.CurrentRow.DataBoundItem).Row;
                if (SelectedDataRow != null)
                {
                    File_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                    file_Path = SelectedDataRow["File Path"].ToString();
                    
                    ImageLocation_textBox.Text = file_Path;
                   // Files_radioButton.Checked = true;

                    // open the file in windows !!
                    c.DownloadFileAsync(file_Path);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void MP_Item_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedDataRow = ((DataRowView)MP_Item_dataGridView.CurrentRow.DataBoundItem).Row;
            if (SelectedDataRow != null)
            {
                Budget_ID = Int32.Parse(SelectedDataRow["ID"].ToString());
                Item_ID = Int32.Parse(SelectedDataRow["Item_ID"].ToString());
                ItemName_textBox.Text = SelectedDataRow["Item Name"].ToString();
                ItemPrice_textBox.Text = SelectedDataRow["Item Price"].ToString();
                ItemAmount_textBox.Text = SelectedDataRow["Amount"].ToString();
                ItemLocalContribution_textBox.Text = SelectedDataRow["Local Contribution"].ToString();
                ItemComment_textBox.Text = SelectedDataRow["Other Comments"].ToString();
                SelectedDataRow = null;
            }
        }
        #endregion

        #region get IDs
        private void GetCurrentProjectPlanId()
        {
            //check connection//
            Program.buildConnection();

            // MySS.sc = new MySqlCommand("select IDENT_CURRENT('ProjectPlan')", Program.MyConn);
            MySS.sc = new MySqlCommand("select max(PP_ID) from `projectplan`", Program.MyConn);
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out Plan_ID);
        }
        private void GetCurrentActivityId()
        {
            //check connection//
            Program.buildConnection();

            MySS.sc = new MySqlCommand("select max(A_ID) from `activity`", Program.MyConn);
            Int32.TryParse(MySS.sc.ExecuteScalar().ToString(), out Activity_ID);
        }
        private void GetCurrentItemId()
        {
            //check connection//
            Program.buildConnection();

            MySS.sc = new MySqlCommand("select max(I_ID) from `item`", Program.MyConn);
            Int32.TryParse(MySS.sc.ExecuteScalar().ToString(), out Item_ID);
        }
        private void GetCurrentBudgetId()
        {
            //check connection//
            Program.buildConnection();

            MySS.query = "select max(B_ID) from `budget`";
            MySS.sc = new MySqlCommand(MySS.query, Program.MyConn);
            Int32.TryParse((MySS.sc.ExecuteScalar()).ToString(), out Budget_ID);
        }
        #endregion get IDs

        #region save - delete - plus
        private void Save_button_MouseEnter(object sender, EventArgs e)
        {
            Save_button.BackgroundImage = Properties.Resources.save;
        }
        private void Save_button_MouseLeave(object sender, EventArgs e)
        {
            Save_button.BackgroundImage = Properties.Resources.save0;
        }
        private void delete_button_MouseEnter(object sender, EventArgs e)
        {
            DeletePersonProject_button.BackgroundImage = Properties.Resources.delete;
        }
        private void delete_button1_MouseEnter(object sender, EventArgs e)
        {
            DeleteIntermidate_button.BackgroundImage = Properties.Resources.delete;
        }
        private void delete_button3_MouseEnter(object sender, EventArgs e)
        {
            DeleteItem_button.BackgroundImage = Properties.Resources.delete;
        }
        private void delete_button4_MouseEnter(object sender, EventArgs e)
        {
            FileDelete_button.BackgroundImage = Properties.Resources.delete;
        }
        private void delete_button_MouseLeave(object sender, EventArgs e)
        {
            DeletePersonProject_button.BackgroundImage = Properties.Resources.delete0b;
        }
        private void delete_button1_MouseLeave(object sender, EventArgs e)
        {
            DeleteIntermidate_button.BackgroundImage = Properties.Resources.delete0b;
        }
        private void delete_button3_MouseLeave(object sender, EventArgs e)
        {
            DeleteItem_button.BackgroundImage = Properties.Resources.delete0b;
        }
        private void delete_button4_MouseLeave(object sender, EventArgs e)
        {
            FileDelete_button.BackgroundImage = Properties.Resources.delete0b;
        }
        private void add_button_MouseEnter(object sender, EventArgs e)
        {
            InsertPersonProject_button.BackgroundImage =  Properties.Resources.add;
        }
        private void add_button1_MouseEnter(object sender, EventArgs e)
        {
            AddIntermidate_button.BackgroundImage = Properties.Resources.add;
        }
        private void add_button3_MouseEnter(object sender, EventArgs e)
        {
            AddItem_button.BackgroundImage = Properties.Resources.add;
        }
        private void add_button4_MouseEnter(object sender, EventArgs e)
        {
            FileSave_button.BackgroundImage = Properties.Resources.add; 
        }
        private void add_button_MouseLeave(object sender, EventArgs e)
        {
            InsertPersonProject_button.BackgroundImage = Properties.Resources.add0b;
        }
        private void add_button1_MouseLeave(object sender, EventArgs e)
        {
            AddIntermidate_button.BackgroundImage = Properties.Resources.add0b;
        }
        private void add_button3_MouseLeave(object sender, EventArgs e)
        {
            AddItem_button.BackgroundImage = Properties.Resources.add0b;
        }

        private void add_button4_MouseLeave(object sender, EventArgs e)
        {
            FileSave_button.BackgroundImage = Properties.Resources.add0b;
            
        }
        #endregion save - delete

        private void SelectPerson_button_Click(object sender, EventArgs e)
        {
            try
            {
                SelectPerson SelectPerson = new SelectPerson();
                SelectedDataRow = SelectPerson.showSelectedMPRow();

                if (SelectedDataRow != null)
                {
                    Person_ID = (int)SelectedDataRow["ID"];
                    PersonName_textBox.Text = (string)SelectedDataRow["Beneficiary Name"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Person_ID = -1;
            }
        }
        private void AddNewBudget_radioButton_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (AddNewBudget_radioButton.Checked)
                {
                    //Budget inserting Part
                    if (MicroProject_ID == -1)
                        throw new Exception("Please choose the project you want to add to");
                    insertBudget();
                    GetCurrentBudgetId();
                }
                else
                {
                    Budget_Item_bind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        #region image & file
        private void ImageOpen_button_Click(object sender, EventArgs e)
        {
                myTh = new Thread(new ThreadStart(CallBrowseFileDialog));
                myTh.SetApartmentState(ApartmentState.STA);
                myTh.Start();
                myTh.Join();
                ImageLocation_textBox.Text = "/micro_files/" + MicroProject_ID + "_" + file_name;
           
        }
        //private void CallDialog()
        //{
        //    open = new OpenFileDialog();
        //    open.Filter = "Image Files| *.jpg; *.jpeg; *.png; ";
        //    DialogResult res = open.ShowDialog();
        //    if (res == DialogResult.OK)
        //    {
        //        full_file_name = open.FileName;
        //        imageName = Path.GetFileName(open.FileName);
        //        pictureBox1.ImageLocation = full_file_name;
        //    }
        //}
        //private void Convert_Picture()
        //{
        //    arr = null;
        //    FileStream fs = new FileStream(full_file_name, FileMode.Open, FileAccess.Read);
        //    BinaryReader br = new BinaryReader(fs);
        //    arr = br.ReadBytes((int)fs.Length);
        //}

        //private void Files_radioButton_CheckedChanged(object sender, EventArgs e)
        //{
        //    pictureBox1.ImageLocation = "/micro_files/";
        //}
        private void CallBrowseFileDialog()
        {
            open = new OpenFileDialog();
            open.Filter = @"All Files|*.txt;*.docx;*.pdf;*.xls;*.xlsx|Text File (.txt)|*.txt|Word File (.docx,.doc)|*.docx;*.doc
                                |PDF (.pdf)|*.pdf|Spreadsheet (.xls,.xlsx)| *.xls;*.xlsx";
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (open.CheckFileExists)
                {
                    full_file_name = open.FileName;
                    file_name = Path.GetFileName(open.FileName);
                }
            }
        }
        
        #endregion

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
