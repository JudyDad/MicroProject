using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Chilkat;
using MySql.Data.MySqlClient;
using MyWorkApplication.Classes;
using MyWorkApplication.Properties;
using MyWorkApplication.Visit_Forms;
using Tulpep.NotificationWindow;

namespace MyWorkApplication
{
    public partial class MainForm : Form
    {
        private readonly List<Form> _childForms = new List<Form>();
        private bool Have_Notifications;
        private Point image_HitArea = new Point(20, 2);
        private Point image_Location = new Point(22, 7);
        //private Point lastPoint;
        private bool Maximized;
        private MySqlComponents MySS;
        private int tabCount;
        private NewTheme theme;
        private int Old_notifications_count;
        private int tick_index;

        public MainForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true); 
            Maximized = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {  
                var newTheme = new NewTheme();

                if (Settings.Default.theme == "Dark")
                {
                    newTheme.Main_ToNight(this, false);
                    ToDark_AllChidForms();
                }
                else
                {
                    newTheme.Main_ToLight(this, false);
                    ToLight_AllChidForms();
                }

                MySS = new MySqlComponents();
                TabName_label.Text = MP_ID_label.Text = "";
                MP_ID_label.Visible = ProjectNumber_label.Visible 
                    = Project_label.Visible = TabName_label.Visible = false;

                Old_notifications_count = tick_index = 0;

                using (var login_Form = new Login_Form())
                {
                    var res = login_Form.ShowDialog();
                    if (res == DialogResult.OK)
                    { 
                        Username_label.Text = Settings.Default.username;
                        //if(Username_label.Text == "Hope" || Username_label.Text == "Judy")
                        //oneTimeClickToolStripMenuItem.Visible = true;

                        Get_User_Profile();

                        Check_Notifications();
                        if (Program.get_ConnectionString())
                            Online_checkBox.Checked = true;
                        else
                            Online_checkBox.Checked = false;
                    }
                }

                Check_User_Role();
                 
                Text = Settings.Default.selected_hope + " Hope Center - Micro Projects ";
                
                WindowState = FormWindowState.Maximized;
                if(!Online_checkBox.Checked) SetPanelBackground();
                else {
                    byte[] arr  = Convert.FromBase64String(Settings.Default.Background);
                    if (arr != null)
                    {
                        var ms = new MemoryStream(arr);
                        Home_panel.BackgroundImage = Image.FromStream(ms);
                        Home_panel.BackgroundImageLayout = ImageLayout.Zoom; 
                    }
                }
                timer1.Start(); 
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to connect to the server"))
                    MessageBox.Show("Unable to connect to the server please try again or check your connection",
                        "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetPanelBackground()
        {
            try
            {
                //get user image
                Program.buildConnection();
                byte[] arr = null;
                var query = "SELECT `Image` FROM `backgrounds` where `Name` = 'home'";
                MySqlCommand sc = new MySqlCommand(query, Program.MyConn);
                var da = new MySqlDataAdapter(sc);
                var dt = new DataTable();
                da.Fill(dt);
                byte[] query_array = dt.Rows[0].Field<byte[]>(0); 
                if (query_array != null && query_array.Length > 0)
                {
                    MySqlDataReader reader = sc.ExecuteReader();
                    reader.Read(); 
                    arr = (byte[])reader[0];

                    var ms = new MemoryStream(arr);
                    Home_panel.BackgroundImage = Image.FromStream(ms);
                    Home_panel.BackgroundImageLayout = ImageLayout.Zoom; 
                    Settings.Default.Background = Convert.ToBase64String(arr);
                    Settings.Default.Save(); 
                    reader.Close();
                }
                else
                {
                    Home_panel.BackgroundImage = null;
                    Home_panel.BackColor = Color.White;
                } 
                Program.MyConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Get_User_Profile()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Micro Projects\Team\" + Properties.Settings.Default.username + ".png";

            FileInfo file = new FileInfo(path);

            if (file.Exists.Equals(true))
            { 
                using (FileStream stream = new FileStream(path, FileMode.Open, System.IO.FileAccess.Read))
                {
                    ProfilePicture_pictureBox.BackgroundImage = Image.FromStream(stream);
                } 
            }
            else
            {
                ProfilePicture_pictureBox.BackgroundImage = Properties.Resources.Unknown_User;
            }
        }

        //Method to generate tabs//
        public void showNewTab(Form cform, string HeaderText, int tag)
        {
            try
            {
                tabCount = tabCount + 1;
                cform.Text = HeaderText;
                cform.TopLevel = false;
                cform.ControlBox = false;
                cform.FormBorderStyle = FormBorderStyle.None;
                cform.Dock = DockStyle.Fill;

                cform.Tag = tag; //Micro

                tabControl1.TabPages.Add(cform);
                AddForm(cform);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddForm(Form toAdd)
        {
            _childForms.Add(toAdd);
            Home_panel.Visible = false;

            toAdd.Closed += (sender, e) =>
            {
                _childForms.Remove(toAdd);
                if (_childForms.Count == 0)
                    Home_panel.Visible = true;
            }; 

            
        }

        private void ToDark_AllChidForms()
        {
            try
            {
                theme = new NewTheme();
                foreach (var form in _childForms)
                {
                    if (form.Name == "Application_Form" || form.Name == "CheckList_Form")
                        theme.Application_ToNight(form);
                    if (form.Name == "AdminPanel_From") theme.AdminPanel_ToNight(form, false);

                    if (form.Name == "VOpening_Other_Form" || form.Name == "VOpening_Taxi_Form" ||
                        form.Name == "VClosing_Form" || form.Name == "Monitoring_Visit_Results_Form")
                        theme.Visit_ToNight(form);

                    if (form.Name == "V_ME_Other_Form" || form.Name == "VOpening_Taxi_Form")
                        theme.ME_Visit_ToNight(form);

                    if (form.Name == "Visits_Statistics") theme.Visit_User_ToNight(form);

                    if (form.Name == "Attachments_Form") theme.Attachment_ToNight(form);
                    if (form.Name == "Loans_Form") theme.Loan_ToNight(form);
                    if (form.Name == "ExecutiveFiles_Form") theme.Loan_ToNight(form);

                    if (form.Name == "Timeline_Form") theme.Timeline_ToNight(form);
                    if (form.Name == "Search_Form") theme.Search_ToNight(form, false);
                    if (form.Name == "Statistics_Form") theme.Statistics_ToNight(form, false);
                    if (form.Name == "TaskBoard_Form") theme.Tasks_ToNight(form);

                    if (form.Name == "VisitNotifications_Form" || form.Name == "Payments_Form")
                        theme.Search_ToNight(form, false);
                    if (form.Name == "Notification_Box") theme.NotificationBox_ToNight(form);

                    if (form.Name == "ShowFamilyMembers") theme.FamilyMembers_ToNight(form);

                    if (form.Name == "AddNewEducation" || form.Name == "AddNewExperience" ||
                        form.Name == "AddNewLanguage" || form.Name == "AddNewPriest" || form.Name == "AddNewSkill")
                        theme.Category_ToNight(form);

                    if (form.Name == "AboutUs_Form") theme.About_ToNight(form);
                    if (form.Name == "User_Form") theme.User_ToNight(form);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToLight_AllChidForms()
        {
            try
            {
                theme = new NewTheme();
                foreach (var form in _childForms)
                {
                    if (form.Name == "Application_Form" || form.Name == "CheckList_Form")
                        theme.Application_ToLight(form);
                    if (form.Name == "AdminPanel_From") theme.AdminPanel_ToLight(form, false);

                    if (form.Name == "VOpening_Other_Form" || form.Name == "VOpening_Taxi_Form" ||
                        form.Name == "VClosing_Form" || form.Name == "Monitoring_Visit_Results_Form")
                        theme.Visit_ToLight(form);

                    if (form.Name == "Visits_Statistics") theme.Visit_User_ToLight(form);

                    if (form.Name == "V_ME_Other_Form" || form.Name == "VOpening_Taxi_Form")
                        theme.ME_Visit_ToLight(form);

                    if (form.Name == "Attachments_Form") theme.Attachment_ToLight(form);
                    if (form.Name == "Loans_Form") theme.Loan_ToLight(form);
                    if (form.Name == "ExecutiveFiles_Form") theme.Loan_ToLight(form);

                    if (form.Name == "Timeline_Form") theme.Timeline_ToLight(form);
                    if (form.Name == "Search_Form") theme.Search_ToLight(form, false);
                    if (form.Name == "Statistics_Form") theme.Statistics_ToLight(form, false);
                    if (form.Name == "TaskBoard_Form") theme.Tasks_ToLight(form);

                    if (form.Name == "VisitNotifications_Form" || form.Name == "Payments_Form")
                        theme.Search_ToLight(form, false);
                    if (form.Name == "Notification_Box") theme.NotificationBox_ToLight(form);

                    if (form.Name == "ShowFamilyMembers") theme.FamilyMembers_ToLight(form);

                    if (form.Name == "AddNewEducation" || form.Name == "AddNewExperience" ||
                        form.Name == "AddNewLanguage" || form.Name == "AddNewPriest" || form.Name == "AddNewSkill")
                        theme.Category_ToLight(form);

                    if (form.Name == "AboutUs_Form") theme.About_ToLight(form);
                    if (form.Name == "User_Form") theme.User_ToLight(form);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ToolStripItem[] GetAllChildren(ToolStripItem item)
        {
            var Items = new List<ToolStripItem> {item};
            if (item is ToolStripMenuItem)
                foreach (ToolStripItem i in ((ToolStripMenuItem) item).DropDownItems)
                    Items.AddRange(GetAllChildren(i));
            else if (item is ToolStripSplitButton)
                foreach (ToolStripItem i in ((ToolStripSplitButton) item).DropDownItems)
                    Items.AddRange(GetAllChildren(i));
            else if (item is ToolStripDropDownButton)
                foreach (ToolStripItem i in ((ToolStripDropDownButton) item).DropDownItems)
                    Items.AddRange(GetAllChildren(i));
            return Items.ToArray();
        }

        private void Check_User_Role()
        {
            try
            {
                //check the role of this user
                Program.buildConnection();
                //default user if there isn't any user
                if (Settings.Default.username == "admin" && Settings.Default.password == "4602543")
                {
                    for (var i = 0; i < menuStrip1.Items.Count; i++) menuStrip1.Items[i].Enabled = true;
                    return;
                }

                var applicationItems = GetAllChildren(menuStrip1.Items["applicationToolStripMenuItem"]);
                var financialItems = GetAllChildren(menuStrip1.Items["financialToolStripMenuItem"]);
                var mEItems = GetAllChildren(menuStrip1.Items["mEToolStripMenuItem"]);
                var toolsItems = GetAllChildren(menuStrip1.Items["toolsToolStripMenuItem"]);

                DataBase_comboBox.Visible = false;   

                switch (Settings.Default.role)
                {
                    //ADDED BY SARY
                    case 8: // L0_Admin
                        {
                            DataBase_comboBox.Visible = true;
                            Bind_DataBase_comboBox(); 

                            for (var i = 0; i < menuStrip1.Items.Count; i++)
                                menuStrip1.Items[i].Enabled = true;
                        }
                        break; 
                    case 1: //admin
                    {
                        for (var i = 0; i < menuStrip1.Items.Count; i++)
                            menuStrip1.Items[i].Enabled = true;
                    }
                        break;
                    case 2: //Data
                    {
                        applicationItems[0].Enabled = true;
                        applicationItems[1].Enabled = true;
                        applicationItems[2].Enabled = false; //Admin
                        applicationItems[3].Enabled = false; //Admin Tools
                        applicationItems[4].Enabled = true; //checklist

                        financialItems[0].Enabled = false; //Close the head item => close all the list 

                        mEItems[0].Enabled = true; // enable all the list except the user statistics
                        mEItems[1].Enabled = true;
                        mEItems[2].Enabled = true;
                        mEItems[3].Enabled = true;
                        mEItems[4].Enabled = false;

                        toolsItems[0].Enabled = true; //title Tools
                        toolsItems[1].Enabled = true; //search
                        toolsItems[2].Enabled = true; //statistics
                        toolsItems[3].Enabled = true; //taskboard
                        toolsItems[4].Enabled = false; //log
                        toolsItems[5].Enabled = true; //theme
                        toolsItems[6].Enabled = true; //Dark
                        toolsItems[7].Enabled = true; //Light
                        toolsItems[8].Enabled = true; //message 
                        toolsItems[9].Enabled = false; //data backup
                        toolsItems[10].Enabled = false; //backup
                        toolsItems[11].Enabled = false; //restor
                        toolsItems[12].Enabled = false; //users
                    }
                        break;
                    case 3: //Financial
                    case 7: //Lawful
                    {
                        applicationItems[0].Enabled = false; //Close the head item => close all the list 

                        financialItems[0].Enabled = true; //enable the head item => enable all the list  

                        mEItems[0].Enabled = false; //Close the head item => close all the list  

                        toolsItems[0].Enabled = true; // title Tools
                        toolsItems[1].Enabled = true; //search
                        toolsItems[2].Enabled = true; //statistics
                        toolsItems[3].Enabled = true; //taskboard
                        toolsItems[4].Enabled = false; //log
                        toolsItems[5].Enabled = true; //theme
                        toolsItems[6].Enabled = true; //Dark
                        toolsItems[7].Enabled = true; //Light
                        toolsItems[8].Enabled = true; //message 
                        toolsItems[9].Enabled = false; //data backup
                        toolsItems[10].Enabled = false; //backup
                        toolsItems[11].Enabled = false; //restor
                        toolsItems[12].Enabled = false; //users
                    }
                        break;
                    case 4: //Guest
                    case 6: //Out Of Service
                    {
                        applicationItems[0].Enabled = false; //Close the head item => close all the list  

                        financialItems[0].Enabled = false; //Close the head item => close all the list 

                        mEItems[0].Enabled = false; //Close the head item => close all the list 

                        toolsItems[0].Enabled = true; // title Tools
                        toolsItems[1].Enabled = true; //search
                        toolsItems[2].Enabled = true; //statistics
                        toolsItems[3].Enabled = true; //taskboard
                        toolsItems[4].Enabled = false; //log
                        toolsItems[5].Enabled = true; //theme
                        toolsItems[6].Enabled = true; //Dark
                        toolsItems[7].Enabled = true; //Light
                        toolsItems[8].Enabled = true; //message 
                        toolsItems[9].Enabled = false; //data backup
                        toolsItems[10].Enabled = false; //backup
                        toolsItems[11].Enabled = false; //restor
                        toolsItems[12].Enabled = false; //users
                    }
                        break;
                    case 5: //manager ???
                    {
                        applicationItems[0].Enabled = true; //enable the head item => enable all the list  

                        financialItems[0].Enabled = true; //enable the head item => enable all the list  

                        mEItems[0].Enabled = true; //enable the head item => enable all the list 

                        toolsItems[0].Enabled = true; // title Tools
                        toolsItems[1].Enabled = true; //search
                        toolsItems[2].Enabled = true; //statistics
                        toolsItems[3].Enabled = true; //taskboard 
                        toolsItems[4].Enabled = false; //log 
                        toolsItems[5].Enabled = true; //theme
                        toolsItems[6].Enabled = true; //Dark
                        toolsItems[7].Enabled = true; //Light
                        toolsItems[8].Enabled = true; //message 
                        toolsItems[9].Enabled = true; //data backup
                        toolsItems[10].Enabled = true; //backup
                        toolsItems[11].Enabled = true; //restor 
                        toolsItems[12].Enabled = false; //users
                    }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProfilePicture_button_Click(object sender, EventArgs e)
        {
            try
            {
                var login_Form = new Login_Form();
                if (login_Form.ShowDialog() == DialogResult.OK)
                {
                    //Close all the opened forms
                    tabControl1.TabPages.Clear();
                    Home_panel.Visible = true;

                    Get_User_Profile();

                    Check_Notifications();
                    Username_label.Text = Settings.Default.username;
                     
                    //ADDED BY SARY TO APPLY THE NEW ROLES AFTER CHANGING THE USER
                    Check_User_Role(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Online_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!Online_checkBox.Checked) //offline
            {
                Properties.Settings.Default.Online = false;
            }
            else if (Online_checkBox.Checked) //online
            {
                Properties.Settings.Default.Online = true;
            }
            Program.set_ConnectionString();
             
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to close the Application ?", "Close Application",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // continue closing;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void X_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void R_button_Click(object sender, EventArgs e)
        {
            showOnScreen();
        }

        private void M_button_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void showOnScreen()
        {
            //showOnScreen(Screen.AllScreens.Length);
            //if (!Maximized)
            //{
            //    //Screen screen = Screen.FromControl(this); //this is the Form class
            //    //Screen screen = Screen.FromPoint(Cursor.Position);
            //    //int height = screen.Bounds.Height;
            //    //int width = screen.Bounds.Width;
            //    //this.Size = new Size(height, width);

            //    Screen screen = Screen.PrimaryScreen;
            //    int intX = screen.Bounds.Width;
            //    int intY = screen.Bounds.Height;
            //    if (intX < this.Width)
            //        this.Width = intX;
            //    if (intY < this.Height)
            //        this.Height = intY;

            //    this.WindowState = FormWindowState.Maximized;
            //    this.FormBorderStyle = FormBorderStyle.None;
            //    Maximized = true;

            //}
            //else
            //{
            //    this.WindowState = FormWindowState.Normal;
            //    this.Size = new Size(1000, 600);
            //    this.FormBorderStyle = FormBorderStyle.Sizable;
            //    Maximized = false;
            //}
        }

        //private void Header_panel_MouseDown(object sender, MouseEventArgs e)
        //{
        //    lastPoint = new Point(e.X, e.Y);
        //    // Maximized = false;
        //    if (Maximized)
        //    {
        //        WindowState = FormWindowState.Normal;
        //        Size = new Size(1000, 600);
        //        FormBorderStyle = FormBorderStyle.Sizable;
        //        Maximized = false;
        //    }
        //}

        //private void Header_panel_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        Left += e.X - lastPoint.X;
        //        Top += e.Y - lastPoint.Y;
        //    }
        //}

       

        #region ToolStripMenuItem_Click

        private void adminPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form AllProjectsLikeExcel = new AdminPanel_From(this);
                showNewTab(AllProjectsLikeExcel, "Admin Panel",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Application_Form = new Application_Form(this);
                showNewTab(Application_Form, "Application",1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void newCheckListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form CheckList_Form = new CheckList_Form(this);
                showNewTab(CheckList_Form, "Check List", 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void createProjectSubCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form AddNewSubCategory = new AddNewSubCategory();
                AddNewSubCategory.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editProjectStreetNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form AddNewStreet = new AddNewStreet();
                AddNewStreet.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Education_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form AddNewEducation = new AddNewEducation();
                AddNewEducation.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void workExperience_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form AddNewExperience = new AddNewExperience();
                AddNewExperience.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void rejectionReasonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
        private void monitoringVisitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form ChooseVisitKind_Form = new ChooseVisitKind_Form(this);
                ChooseVisitKind_Form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
          
        private void monitorVisitResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Monitoring_Visit_Results_Form = new Monitoring_Visit_Results_Form(this);
                showNewTab(Monitoring_Visit_Results_Form, "Monitoring",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void visitDatesSchedualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form form = new VisitNotifications_Form(this);
                showNewTab(form, "Visit Dates",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loansPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Loans_Form = new Loans_Form(this);
                showNewTab(Loans_Form, "Loans",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void paymentsSchedualToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                Form form = new Payments_Form(this);
                showNewTab(form, "Payment Dates",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void newAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Attachments_Form = new Attachments_Form(this);
                showNewTab(Attachments_Form, "Attachments",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newExecutiveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form ExecutiveFiles_Form = new ExecutiveFiles_Form(this);
                showNewTab(ExecutiveFiles_Form, "Executive Files",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Statistics_Form = new Statistics_Form(this);
                showNewTab(Statistics_Form, "Statistics",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void taskBoardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                var projectsTasks = new TaskBoard_Form(this);
                showNewTab(projectsTasks, "Task Board",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Search_Form = new Search_Form(this);
                showNewTab(Search_Form, "Search",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void projectLifeCycleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Timeline_Form = new Timeline_Form(this);
                showNewTab(Timeline_Form, "Timeline",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Log_Form = new Log_Form();
                showNewTab(Log_Form, "Log",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void darkToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //save theme settings
            Settings.Default.theme = "Dark";
            Settings.Default.Save();

            var myTheme = new NewTheme();
            myTheme.Main_ToNight(this, true);

            ToDark_AllChidForms();
        }

        private void lightToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //save theme settings
            Settings.Default.theme = "Light";
            Settings.Default.Save();

            var myTheme = new NewTheme();
            myTheme.Main_ToLight(this, true);

            ToLight_AllChidForms();
        }

        private void chatGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var chat_Form = new Chat_Form();
                chat_Form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void aboutHopeCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form About_US = new AboutUs_Form();
                About_US.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sendAMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form SendMessage_Box = new SendMessage_Box();
                SendMessage_Box.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form User_Form = new User_Form(this);
                showNewTab(User_Form, "User Control",0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //export from offline
                var myTh = new Thread(SaveCallDialog);
                myTh.SetApartmentState(ApartmentState.STA);
                myTh.Start();
                myTh.Join();

                // string CmdString = "SELECT table_name FROM information_schema.tables WHERE table_schema='hcsyria_micro';";
                //string CmdString = "SELECT * from user;";   
                //using (MySqlConnection con = new MySqlConnection(Program.ConnectionString))
                //{
                //    MySqlCommand cmd = new MySqlCommand(CmdString, con);
                //    con.Open();
                //    DataTable dt = new DataTable();
                //    dt = new DataTable("user");
                //    MySqlDataAdapter sda = new MySqlDataAdapter(cmd); 
                //    sda.Fill(dt);
                //    dt.WriteXml(destpath);//"user.xml"
                //    con.Close();

                //    Gzip gzip = new Chilkat.Gzip();

                //    // File-to-file GZip:
                //    // Compress "hamlet.xml" to create "hamlet.xml.gz"
                //    bool success = gzip.CompressFile(destpath, destpath+".gz");
                //    if (success)
                //    {
                //        MessageBox.Show("Backup succeeded", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}

                using (var conn = new MySqlConnection(Program.ConnectionString))
                {
                    using (var cmd = new MySqlCommand())
                    {
                        using (var mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            //mb.ExportInfo.TablesToBeExportedList = new List<string> {
                            ////    "perosn",
                            ////    "microproject",
                            ////    "person_microproject"
                            //};
                            mb.ExportToFile(destpath);
                            conn.Close(); 
                            MessageBox.Show("Backup succeeded", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void restoreFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //export from offline
                var myTh = new Thread(OpenCallDialog);
                myTh.SetApartmentState(ApartmentState.STA);
                myTh.Start();
                myTh.Join();

                // File-to-string ungzip
                // Decompress the contents of a .gz directly to a string variable:
                // The 2nd argument indicates the charset of the character
                // data after it is decompressed.

                //Gzip gzip = new Chilkat.Gzip();
                //bool success = gzip.UncompressFile(destpath, destpath.Substring(0, destpath.Length - 3));
                //if (success)
                //{

                //    MessageBox.Show("Restore succeeded", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}


                //string file = @"C:\Program Files (x86)\Hope Center\Micro Project Setup\backup.sql";
                using (var conn = new MySqlConnection(Program.ConnectionString))
                {
                    using (var cmd = new MySqlCommand())
                    {
                        using (var mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ImportFromFile(destpath);
                            conn.Close();
                            MessageBox.Show("Restore succeeded", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NewIdea_Form NewIdea_Form = new NewIdea_Form("New Idea","");
                NewIdea_Form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void readTheManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Help_Form Help_Form = new Help_Form();
                Help_Form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private string destpath = "";

        private void SaveCallDialog()
        {
            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Backup file|*.xml";
            saveFileDialog1.Title = "Save a backup for database:";
            var res = saveFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
                // If the file name is not an empty string open it for saving.  
                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.  
                    var fs = (FileStream) saveFileDialog1.OpenFile();
                    destpath = saveFileDialog1.FileName;
                    fs.Close();
                }
        }

        private void OpenCallDialog()
        {
            var open = new OpenFileDialog();
            open.Title = "Select a backup file:";
            //open.Filter = "Backup file|*.xml";
            var res = open.ShowDialog();
            if (res == DialogResult.OK) destpath = open.FileName;
        }

        #endregion

        #region notifications

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                tick_index++;
                Check_Notifications();
            }
            catch (Exception ex)
            {
                timer1.Stop();
                MessageBox.Show(ex.Message);
            }
        }

        private void Check_Notifications()
        {
            try
            {
                if (noti_wasOpened)
                {
                    if (notification_Box.IsDisposed)
                    {
                        if (Notification_Box.makeSeens) Old_notifications_count = 0;
                        noti_wasOpened = false;
                    }
                }

                Program.buildConnection();
                //check if the user has notifications to set the icon
                var q = "";
                if (Settings.Default.role != 3 || Settings.Default.role != 7) //financial & legal
                    q = "SELECT count(*) FROM `user_notification`  where `User_ID` = " + Settings.Default.userID +
                        " and `Seen` = 0 and Date <= CURDATE() + INTERVAL 4 DAY";
                else
                    q = "SELECT count(*) FROM `user_notification`  where `User_ID` = " + Settings.Default.userID +
                        " and `Seen` = 0 and Date <= CURDATE()";
                MySS.sc = new MySqlCommand(q, Program.MyConn);
                var Noti_count = Convert.ToInt32(MySS.sc.ExecuteScalar().ToString());
                Program.MyConn.Close();
                if (Noti_count != 0)
                {
                    if (Settings.Default.theme == "Dark")
                        Notifications_button.BackgroundImage = Resources.Red_Bell_DL;
                    else Notifications_button.BackgroundImage = Resources.Bell_Red_L;
                    Have_Notifications = true;

                    if (tick_index == 0 || tick_index == 1)
                        Old_notifications_count = Noti_count;

                    if (Old_notifications_count != Noti_count || tick_index == 0 || tick_index == 1)
                    {
                        //////////////////// have a windows popup of notifications /////////////////////
                        PopupNotifier popup = new PopupNotifier();
                        popup.Image = Resources.Logo_Small_L;
                        popup.ImageSize = new Size(30, 30);
                        popup.TitleText = "Micro Projects App - Notifications";
                        popup.ContentText = "You got " + Noti_count + " notification!";
                        popup.Popup();// show  
                    }
                }
                else
                {
                    if (Settings.Default.theme == "Dark")
                        Notifications_button.BackgroundImage = Resources.Bell_DL;
                    else Notifications_button.BackgroundImage = Resources.Bell_L;
                    Have_Notifications = false;
                }
            }
            catch (Exception ex)
            {
                timer1.Stop();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                timer1.Start();
            }
        }
        Form notification_Box = new Notification_Box();
        bool noti_wasOpened = false;
        private void Notifications_button_Click(object sender, EventArgs e)
        {
            try
            {
                notification_Box = new Notification_Box();
                notification_Box.Show();
                noti_wasOpened = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Notifications_button_MouseEnter(object sender, EventArgs e)
        {
            if (!Have_Notifications)
            {
                if (Settings.Default.theme == "Light")
                    Notifications_button.BackgroundImage = Resources.Bell_N;
                else Notifications_button.BackgroundImage = Resources.Bell_DL;
            }
            else
            {
                if (Settings.Default.theme == "Light")
                    Notifications_button.BackgroundImage = Resources.Bell_Red_N;
                else Notifications_button.BackgroundImage = Resources.Red_Bell_DL;
            }
        }

        private void Notifications_button_MouseLeave(object sender, EventArgs e)
        {
            if (!Have_Notifications)
            {
                if (Settings.Default.theme == "Light")
                    Notifications_button.BackgroundImage = Resources.Bell_L;
                else Notifications_button.BackgroundImage = Resources.Bell_DD;
            }
            else
            {
                if (Settings.Default.theme == "Light")
                    Notifications_button.BackgroundImage = Resources.Bell_Red_L;
                else Notifications_button.BackgroundImage = Resources.Red_Bell_DD;
            }
        }
         
        #endregion
         
        private void MainForm_Resize(object sender, EventArgs e)
        {
            // To solve the problem of tabControl (hide the first tab when minimizing the form)
            if (WindowState == FormWindowState.Minimized || WindowState != FormWindowState.Maximized)
                tabControl1.SuspendLayout();
            else
                tabControl1.ResumeLayout(true); 
            ////////////////////////////////////////////////////////////////////////////////////
        }

        private void tabControl1_SelectedTabChanged(object sender, EventArgs e)
        {
            Form form = (Form)tabControl1.SelectedForm;
            if(form.Tag.Equals(1))//application
            {
                form = (Application_Form)form;

                
                ////////////////////////////////
                //  I don't know what to do  //

                ///////////////////////////////
            }
        }

        private void DataBase_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selected_hope = DataBase_comboBox.GetItemText(DataBase_comboBox.SelectedItem);
            Properties.Settings.Default.selected_hope = selected_hope;
            Properties.Settings.Default.Save();

            Program.set_ConnectionString();

            //Select userID from selected database and save it in properties//
            User u = new User();
            DataTable user_info_dt = u.Select_Users(Username_label.Text,"","");
            Properties.Settings.Default.userID = user_info_dt.Rows[0].Field<int>(0);
            Properties.Settings.Default.Save();

            Text = Settings.Default.selected_hope + " Hope Center - Micro Projects ";
            MessageBox.Show("Database has successfuly changed to: " + selected_hope, "تغيير قاعدة البيانات");

        }

        private void Bind_DataBase_comboBox() //ADDED BY SARY
        {
            DataBase_comboBox.Items.Clear();
            DataBase_comboBox.Items.AddRange(Program.User_In_Database.ToArray());

            DataBase_comboBox.SelectedItem = Properties.Settings.Default.selected_hope;
        }

//        private void gergiToRodaToolStripMenuItem_Click(object sender, EventArgs e)
//        {


////            string query = @"SELECT `Pay_ID`, `Pay_Amount`, `Pay_DueDate`, `Pay_IsPaid`
////,loan.MicroProject_ID as 'MicroProject_ID', loan.`Loan_ID`, `Pay_RecievedOnDate`, `Pay_Note`
////, CONCAT(P_FirstName, ' ', P_LastName) as 'P_Name'
//// FROM `payment` left join loan on  payment.Loan_ID = loan.Loan_ID 
//// join person_microproject on loan.MicroProject_ID = person_microproject.MicroProject_ID
//// join person on person.P_ID = person_microproject.Person_ID
//// WHERE Pay_IsPaid like 'Not Paid' ";
////            Program.buildConnection();
////            var da = new MySqlDataAdapter(query, Program.MyConn);
////            var dt = new DataTable();
////            da.Fill(dt);
////            string insert_query = ""; 
////            for (int i = 0; i < dt.Rows.Count; i++)
////            {
////                string Date = dt.Rows[i].Field<DateTime>("Pay_DueDate").ToString("yyyy/MM/dd");
////                float number = dt.Rows[i].Field<float>("Pay_Amount");
////                string Body = number.ToString();
////                string P_Name = dt.Rows[i].Field<string>("P_Name");
////                int MicroProject_ID = (int)dt.Rows[i].Field<int>("MicroProject_ID");
////               // int User_ID = 25;
////                int Sender_ID = 4;
////                int Pay_ID = dt.Rows[i].Field<int>("Pay_ID");

////                insert_query += "Insert Into `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`,`Pay_ID`) values(N'"
////                + Date + "',N'"
////                + Body + "',"
////                + 0 + ",N'"
////                + P_Name + "',"
////                + MicroProject_ID + ","
////                + 25 + ","
////                + Sender_ID + ","
////                + Pay_ID + ");";

////                insert_query += "Insert Into `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`,`Pay_ID`) values(N'"
////                + Date + "',N'"
////                + Body + "',"
////                + 0 + ",N'"
////                + P_Name + "',"
////                + MicroProject_ID + ","
////                + 29 + ","
////                + Sender_ID + ","
////                + Pay_ID + ");";
////            }
////            using (var sc = new MySqlCommand(insert_query, Program.MyConn))
////            {
////                sc.ExecuteNonQuery();
////            }
             

//            //Program.buildConnection();
//            //string query = @"select * from user_notification where `User_ID` = 17 and Seen = 0;";
//            //var da = new MySqlDataAdapter(query, Program.MyConn);
//            //var dt = new DataTable();
//            //da.Fill(dt);

//            //string insert_query = "";
//            //for (int i = 0; i < dt.Rows.Count; i++)
//            //{
//            //    string Date = dt.Rows[i].Field<DateTime>("Date").ToString("yyyy/MM/dd");
//            //    string Body = dt.Rows[i].Field<string>("Body").ToString(); 
//            //    string P_Name = dt.Rows[i].Field<string>("P_Name").ToString();
//            //    int MicroProject_ID = dt.Rows[i].Field<int>("MicroProject_ID");
//            //    int User_ID = 34;
//            //    int Sender_ID = 4;
//            //    int Pay_ID = dt.Rows[i].Field<int>("Pay_ID");

//            //    insert_query += "Insert Into `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`,`Pay_ID`) values(N'"
//            //    + Date + "',N'"
//            //    + Body + "',"
//            //    + 0 + ",N'"
//            //    + P_Name + "',"
//            //    + MicroProject_ID + ","
//            //    + User_ID + ","
//            //    + Sender_ID + ","
//            //    + Pay_ID + ");";
//            //}
//            //using (var sc = new MySqlCommand(insert_query, Program.MyConn))
//            //{
//            //    sc.ExecuteNonQuery();
//            //}
//        }

//        private void clickMeToolStripMenuItem_Click(object sender, EventArgs e)
//        {
////            try
////            {
////                Properties.Settings.Default.clicked_me = false;
////                Properties.Settings.Default.Save();

////                //&& Properties.Settings.Default.username == "Hope"
////                if (Properties.Settings.Default.clicked_me == false)
////                {
////                    List<int> User_IDs = new List<int>();
////                    Select s = new Classes.Select();
////                    var user_db = s.select_M_E();
////                    if (user_db != null)
////                        for (var i = 0; i < user_db.Rows.Count; i++)
////                            User_IDs.Add(user_db.Rows[i].Field<int>(0));

////                    /// code ///
////                    Program.buildConnection();
////                    string query = @"select MP_ID from microproject where MP_State in (1,4)"; //مقبول - ممول
////                    var da = new MySqlDataAdapter(query, Program.MyConn);
////                    var dt = new DataTable();
////                    da.Fill(dt);

////                    //check connection//
////                    Program.buildConnection();
////                    string insert_query22 = "";
////                    for (int i = 0; i < dt.Rows.Count; i++)
////                    {
////                        //insert_query += "INSERT INTO `task_microproject`(`MicroProject_ID`, `Task_ID`, `State`, `Date`) VALUES ("
////                        //            + dt.Rows[i].Field<int>(0) + ","
////                        //            + 29 + ","
////                        //            + 0 + ",'"
////                        //            + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "' );"; 
////                        //string query22 = @"SELECT * FROM `user_notification` WHERE MicroProject_ID = " + dt.Rows[i].Field<int>(0) + " and ((`Body` like 'Visit%' and seen = 1) or (`Body` like 'Visit Opening%' and seen = 0)) order by Date desc limit 1";

////                        string query22 = @"SELECT `ID`, CONCAT(P_FirstName, ' ', P_LastName, ' ابن/ة ', P_FatherName) as 'name'
////,person_microproject.MicroProject_ID, `Number`, `Kind`,  `Date` 
////FROM `mevisit` 
////left join person_microproject on mevisit.Person_ID = person_microproject.Person_ID  
////left join person on person_microproject.Person_ID = person.P_ID 
////WHERE person_microproject.MicroProject_ID = " + dt.Rows[i].Field<int>(0) + " order by Number desc limit 1";
////                        var da22 = new MySqlDataAdapter(query22, Program.MyConn);
////                        var dt22 = new DataTable();
////                        da22.Fill(dt22);

////                        if (dt22.Rows.Count != 0)
////                        {
////                            string P_Name = dt22.Rows[0].Field<string>(1);
////                            int mp_id = dt22.Rows[0].Field<int>(2);
////                            string Number = dt22.Rows[0].Field<string>(3);
////                            string Kind = dt22.Rows[0].Field<string>(4);
////                            DateTime date = dt22.Rows[0].Field<DateTime>(5);

////                            string category_from_body = "";
////                            if (Kind == "v")
////                                category_from_body = "-V";
////                            else if (Kind == "o")
////                                category_from_body = "-O";

////                            int visit_interval = 4;

////                            if (date <= DateTime.Today)  //تغيير التاريخ وزلقه الى الشهر السادس والسابع
////                            {
////                                DateTime newDateToBe;
////                                if (date.Year == 2019)
////                                {
////                                    if (date.Day > 28) newDateToBe = new DateTime(date.Year + 2, 02, 28);
////                                    else newDateToBe = new DateTime(date.Year + 2, 02, date.Day);
////                                    date = newDateToBe;
////                                }
////                                else if (date.Year == 2020 && date.Month <= 11)
////                                {
////                                    if (date.Day > 28) newDateToBe = new DateTime(date.Year + 1, 02, 28);
////                                    else newDateToBe = new DateTime(date.Year + 1, 02, date.Day);
////                                    date = newDateToBe;
////                                }
////                                else if (date.Year == 2020 && date.Month > 11)
////                                {
////                                    newDateToBe = new DateTime(date.Year + 1, 03, date.Day);
////                                    date = newDateToBe;
////                                }
////                                else if (date.Year == 2021 && date.Month == 01)
////                                {
////                                    newDateToBe = new DateTime(date.Year, 03, date.Day);
////                                    date = newDateToBe;
////                                }
////                            }

////                            //last visit number check
////                            if (Number == "4")
////                            {
////                                DateTime M5_Visit_date;
////                                M5_Visit_date = date;
////                                M5_Visit_date = M5_Visit_date.AddMonths(visit_interval);

////                                #region insert M5 
////                                foreach (var u in User_IDs)
////                                    insert_query22 += "INSERT INTO `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`, `Pay_ID`) VALUES ("
////                                    + "'" + M5_Visit_date.ToString("yyyy/MM/dd") + "',"
////                                    + "N'" + "Visit M5" + category_from_body + "',"
////                                    + 0 + ","
////                                    + "N'" + P_Name + "',"
////                                    + "" + mp_id + ","
////                                    + "" + u + ","
////                                    + "" + Properties.Settings.Default.userID + ","
////                                    + "" + -21 + ");";
////                                #endregion
////                            }

////                            else if (Number == "3")
////                            {
////                                DateTime M4_Visit_date, M5_Visit_date;
////                                M4_Visit_date = M5_Visit_date = DateTime.Now;
////                                M4_Visit_date = date;
////                                M4_Visit_date = M4_Visit_date.AddMonths(visit_interval);
////                                M5_Visit_date = M4_Visit_date.AddMonths(visit_interval);

////                                #region insert M4-M5
////                                foreach (var u in User_IDs)
////                                {
////                                    insert_query22 += "INSERT INTO `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`, `Pay_ID`) VALUES ("
////                                        + "'" + M4_Visit_date.ToString("yyyy/MM/dd") + "',"
////                                        + "N'" + "Visit M4" + category_from_body + "',"
////                                        + 0 + ","
////                                        + "N'" + P_Name + "',"
////                                        + "" + mp_id + ","
////                                        + "" + u + ","
////                                        + "" + Properties.Settings.Default.userID + ","
////                                        + "" + -21 + ");";
////                                    insert_query22 += "INSERT INTO `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`, `Pay_ID`) VALUES ("
////                                        + "'" + M5_Visit_date.ToString("yyyy/MM/dd") + "',"
////                                        + "N'" + "Visit M5" + category_from_body + "',"
////                                        + 0 + ","
////                                        + "N'" + P_Name + "',"
////                                        + "" + mp_id + ","
////                                        + "" + u + ","
////                                        + "" + Properties.Settings.Default.userID + ","
////                                        + "" + -21 + ");";
////                                }
////                                #endregion
////                            }

////                            else if (Number == "2")
////                            {
////                                DateTime M3_Visit_date, M4_Visit_date, M5_Visit_date;
////                                M3_Visit_date = M4_Visit_date = M5_Visit_date = DateTime.Now;
////                                M3_Visit_date = date;
////                                M3_Visit_date = M3_Visit_date.AddMonths(visit_interval);
////                                M4_Visit_date = M3_Visit_date.AddMonths(visit_interval);
////                                M5_Visit_date = M4_Visit_date.AddMonths(visit_interval);


////                                #region insert M3-M4-M5
////                                foreach (var u in User_IDs)
////                                {
////                                    insert_query22 += "INSERT INTO `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`, `Pay_ID`) VALUES ("
////                                        + "'" + M3_Visit_date.ToString("yyyy/MM/dd") + "',"
////                                        + "N'" + "Visit M3" + category_from_body + "',"
////                                        + 0 + ","
////                                        + "N'" + P_Name + "',"
////                                        + "" + mp_id + ","
////                                        + "" + u + ","
////                                        + "" + Properties.Settings.Default.userID + ","
////                                        + "" + -21 + ");";
////                                    insert_query22 += "INSERT INTO `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`, `Pay_ID`) VALUES ("
////                                        + "'" + M4_Visit_date.ToString("yyyy/MM/dd") + "',"
////                                        + "N'" + "Visit M4" + category_from_body + "',"
////                                        + 0 + ","
////                                        + "N'" + P_Name + "',"
////                                        + "" + mp_id + ","
////                                        + "" + u + ","
////                                        + "" + Properties.Settings.Default.userID + ","
////                                        + "" + -21 + ");";
////                                    insert_query22 += "INSERT INTO `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`, `Pay_ID`) VALUES ("
////                                        + "'" + M5_Visit_date.ToString("yyyy/MM/dd") + "',"
////                                        + "N'" + "Visit M5" + category_from_body + "',"
////                                        + 0 + ","
////                                        + "N'" + P_Name + "',"
////                                        + "" + mp_id + ","
////                                        + "" + u + ","
////                                        + "" + Properties.Settings.Default.userID + ","
////                                        + "" + -21 + ");";
////                                }
////                                #endregion
////                            }

////                            else if (Number == "1")
////                            {
////                                DateTime M2_Visit_date, M3_Visit_date, M4_Visit_date, M5_Visit_date;
////                                M2_Visit_date = M3_Visit_date = M4_Visit_date = M5_Visit_date = DateTime.Now;

////                                M2_Visit_date = date;
////                                M2_Visit_date = M2_Visit_date.AddMonths(visit_interval);
////                                M3_Visit_date = M2_Visit_date.AddMonths(visit_interval);
////                                M4_Visit_date = M3_Visit_date.AddMonths(visit_interval);
////                                M5_Visit_date = M4_Visit_date.AddMonths(visit_interval);

////                                #region insert M2_M3-M4-M5
////                                foreach (var u in User_IDs)
////                                {
////                                    insert_query22 += "INSERT INTO `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`, `Pay_ID`) VALUES ("
////                                        + "'" + M2_Visit_date.ToString("yyyy/MM/dd") + "',"
////                                        + "N'" + "Visit M2" + category_from_body + "',"
////                                        + 0 + ","
////                                        + "N'" + P_Name + "',"
////                                        + "" + mp_id + ","
////                                        + "" + u + ","
////                                        + "" + Properties.Settings.Default.userID + ","
////                                        + "" + -21 + ");";
////                                    insert_query22 += "INSERT INTO `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`, `Pay_ID`) VALUES ("
////                                        + "'" + M3_Visit_date.ToString("yyyy/MM/dd") + "',"
////                                        + "N'" + "Visit M3" + category_from_body + "',"
////                                        + 0 + ","
////                                        + "N'" + P_Name + "',"
////                                        + "" + mp_id + ","
////                                        + "" + u + ","
////                                        + "" + Properties.Settings.Default.userID + ","
////                                        + "" + -21 + ");";
////                                    insert_query22 += "INSERT INTO `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`, `Pay_ID`) VALUES ("
////                                        + "'" + M4_Visit_date.ToString("yyyy/MM/dd") + "',"
////                                        + "N'" + "Visit M4" + category_from_body + "',"
////                                        + 0 + ","
////                                        + "N'" + P_Name + "',"
////                                        + "" + mp_id + ","
////                                        + "" + u + ","
////                                        + "" + Properties.Settings.Default.userID + ","
////                                        + "" + -21 + ");";
////                                    insert_query22 += "INSERT INTO `user_notification`(`Date`, `Body`, `Seen`, `P_Name`, `MicroProject_ID`, `User_ID`, `Sender_ID`, `Pay_ID`) VALUES ("
////                                        + "'" + M5_Visit_date.ToString("yyyy/MM/dd") + "',"
////                                        + "N'" + "Visit M5" + category_from_body + "',"
////                                        + 0 + ","
////                                        + "N'" + P_Name + "',"
////                                        + "" + mp_id + ","
////                                        + "" + u + ","
////                                        + "" + Properties.Settings.Default.userID + ","
////                                        + "" + -21 + ");";
////                                }
////                                #endregion
////                            }
////                        }
////                    }

////                    string end_query = "Delete from `user_notification` WHERE `Body` like 'Visit%' and `Body` not like '%Visit Opening%' and `Body` not like '%Visit Closing%' and seen = 0;";
////                    using (MySqlCommand sc3 = new MySqlCommand(end_query, Program.MyConn))
////                    {
////                        sc3.ExecuteNonQuery();
////                    }

////                    using (MySqlCommand sc1 = new MySqlCommand(insert_query22, Program.MyConn))
////                    {
////                        sc1.ExecuteNonQuery();
////                    }
////                    //string end_query = "Update `user_notification` set seen = 1 WHERE `Body` like 'Visit%' and Year(Date) < 2020 and seen = 0;";
////                    //end_query += "Update `user_notification` set seen = 1 WHERE MicroProject_ID in (SELECT `MP_ID` FROM `microproject` WHERE `MP_State` in (5,6,7));";

////                    Program.MyConn.Close();

////                    Properties.Settings.Default.clicked_me = true;
////                    Properties.Settings.Default.Save();
////                    oneTimeClickToolStripMenuItem.Visible = false;
////                }
////                else
////                { oneTimeClickToolStripMenuItem.Visible = false; }
////            }
////            catch (Exception ex)
////            { MessageBox.Show(ex.Message); }
//        }

 
        private void moveVisitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var UserNotification = new UserNotification();

                Program.buildConnection();
                var qqq = @"select `Pay_ID`,`Pay_Amount`,`Pay_DueDate`, loan.MicroProject_ID as 'MicroProject_ID'
 ,CONCAT(P_FirstName,' ', P_LastName,' ابن/ة ',P_FatherName) as 'المستفيد'
 from `payment` 
 left join `loan` on payment.Loan_ID = loan.Loan_ID  
 left join `microproject` on microproject.MP_ID = loan.MicroProject_ID  
 left join `donorgroup` on donorgroup.ID = microproject.DonorGroup_ID  
 left join `person_microproject` on person_microproject.MicroProject_ID = loan.MicroProject_ID 
 left join `person` on person.P_ID = person_microproject.Person_ID 
 Where Month(`Pay_DueDate`) like 12 and Year(`Pay_DueDate`) like 2020 and Pay_IsPaid Like 'Not Paid' order by Pay_DueDate asc";
                var da = new MySqlDataAdapter(qqq, Program.MyConn);
                var my_dt = new DataTable();
                da.Fill(my_dt);
                if (my_dt != null || my_dt.Rows.Count > 0)
                    for (var i = 0; i < my_dt.Rows.Count; i++)
                    {
                        var Pay_ID = int.Parse(my_dt.Rows[i]["Pay_ID"].ToString());
                        var Pay_Amount = float.Parse(my_dt.Rows[i]["Pay_Amount"].ToString());
                        var date1 = (DateTime)my_dt.Rows[i]["Pay_DueDate"];
                        var MP_ID = int.Parse(my_dt.Rows[i]["MicroProject_ID"].ToString());
                        var name = my_dt.Rows[i]["المستفيد"].ToString();

                        UserNotification.Insert_UserNotification(date1, Pay_Amount.ToString(), name, MP_ID, 25,
                            Settings.Default.userID, Pay_ID);
                        UserNotification.Insert_UserNotification(date1, Pay_Amount.ToString(), name, MP_ID, 5,
                            Settings.Default.userID, Pay_ID);
                    }

                Program.MyConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void أسبابعدمالتمويلRejectReasonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewStateReasons Form = new AddNewStateReasons();
                Form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void restoreImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // micro images // homs images/profile
            //#region update profiles
            //// update profiles
            //DirectoryInfo d = new DirectoryInfo(@"D:\homs_micro\profile");
            //FileInfo[] Files = d.GetFiles("*.jpg"); //Getting image files *.jpg , *.png
            //string imageName = "";
            //foreach (FileInfo file in Files)
            //{
            //    imageName = file.Name;
            //    string ftpPath = "/homs_micro/profile/" + imageName;
            //    string fullFtpPath = "ftp://judy@hcsyria.org" + ftpPath;

            //    Program.buildConnection();
            //    string qqq = " SELECT `P_ID` FROM `person` WHERE `P_PicturePath` like '" + fullFtpPath + "'";
            //    MySqlCommand sc = new MySqlCommand(qqq, Program.MyConn);
            //    int P_ID = Convert.ToInt32(sc.ExecuteScalar());

            //    string imageFilePath = file.FullName;
            //    FileStream fs = new FileStream(imageFilePath, FileMode.Open, System.IO.FileAccess.Read);
            //    BinaryReader br = new BinaryReader(fs);
            //    byte[] PersonPicArr = null;
            //    PersonPicArr = br.ReadBytes((int)fs.Length);

            //    string u_query = "Update `person` set "
            //        + " P_Picture = @PersonPicArr"
            //        + " where P_ID =" + P_ID;

            //    MySS.sc = new MySqlCommand(u_query, Program.MyConn);
            //    MySS.sc.Parameters.Add(new MySqlParameter("@PersonPicArr", PersonPicArr));
            //    MySS.sc.ExecuteNonQuery();
            //    Program.MyConn.Close();
            //}
            //#endregion

            //#region udate attachments
            //// update attachments
            //d = new DirectoryInfo(@"D:\homs_micro");
            //Files = d.GetFiles("*.jpg"); //Getting image files *.jpg , *.png
            //imageName = "";
            //foreach (FileInfo file in Files)
            //{
            //    imageName = file.Name;
            //    string ftpPath = "/homs_micro/" + imageName;
            //    string fullFtpPath = "ftp://judy@hcsyria.org" + ftpPath;

            //    Program.buildConnection();
            //    string qqq = " SELECT `Image_ID` FROM `image` WHERE `Image_Path` like '" + fullFtpPath + "'";
            //    MySqlCommand sc = new MySqlCommand(qqq, Program.MyConn);
            //    int I_ID = Convert.ToInt32(sc.ExecuteScalar());

            //    string imageFilePath = file.FullName;
            //    FileStream fs = new FileStream(imageFilePath, FileMode.Open, System.IO.FileAccess.Read);
            //    BinaryReader br = new BinaryReader(fs);
            //    byte[] PersonPicArr = null;
            //    PersonPicArr = br.ReadBytes((int)fs.Length);

            //    string u_query = "Update `image` set "
            //        + " Image_Content = @PersonPicArr"
            //        + " where Image_ID =" + I_ID;
            //    MySS.sc = new MySqlCommand(u_query, Program.MyConn);
            //    MySS.sc.Parameters.Add(new MySqlParameter("@PersonPicArr", PersonPicArr));
            //    MySS.sc.ExecuteNonQuery();
            //    Program.MyConn.Close();
            //}
            //#endregion

            //MessageBox.Show("DONE ^_^");
        }

        private void newSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form form = new Search_Form2(this);
                showNewTab(form, "Search_Form2", 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void donorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewDonor Form = new AddNewDonor();
                Form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void visitorsStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form form = new Visits_Statistics(this);
                showNewTab(form, "Visitors Statistics", 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rehabAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form form = new ApplicationRehab_Form(this,1);
                //showNewTab(form, "ملحق", 0);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    } 
}