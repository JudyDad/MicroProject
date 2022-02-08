namespace MyWorkApplication
{
    partial class Financial_Visit_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Financial_Visit_Form));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Visits_DataGridView = new System.Windows.Forms.DataGridView();
            this.Person_Name_textBox2 = new System.Windows.Forms.TextBox();
            this.MicroProject_ID_textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.LoanAmount_textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.VisitType_comboBox2 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Ledger_label = new System.Windows.Forms.Label();
            this.Beneficiary_FullName_label = new System.Windows.Forms.Label();
            this.Project_Number_label = new System.Windows.Forms.Label();
            this.OtherNotes_lable = new System.Windows.Forms.Label();
            this.Indicators_textBox = new System.Windows.Forms.TextBox();
            this.Indicators_label = new System.Windows.Forms.Label();
            this.AverageItemPrice_textBox = new System.Windows.Forms.TextBox();
            this.AverageItemPrice_label = new System.Windows.Forms.Label();
            this.AverageSales_textBox = new System.Windows.Forms.TextBox();
            this.AverageSales_label = new System.Windows.Forms.Label();
            this.ProfitRatio_textBox = new System.Windows.Forms.TextBox();
            this.ProfitRatio_label = new System.Windows.Forms.Label();
            this.LedgerComments_textBox = new System.Windows.Forms.TextBox();
            this.Continuance_textBox = new System.Windows.Forms.TextBox();
            this.NoLedger_radioButton = new System.Windows.Forms.RadioButton();
            this.YesLedger_radioButton = new System.Windows.Forms.RadioButton();
            this.IF_Datagrid_lable = new System.Windows.Forms.Label();
            this.Vistors_lable = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.V4_comboBox = new System.Windows.Forms.ComboBox();
            this.V3_comboBox = new System.Windows.Forms.ComboBox();
            this.V2_comboBox = new System.Windows.Forms.ComboBox();
            this.V1_comboBox = new System.Windows.Forms.ComboBox();
            this.Continuance_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.InsertVisit_button = new System.Windows.Forms.Button();
            this.VisitDate_dateTimePicker = new MyWorkApplication.Classes.BCDateTimePicker();
            this.LoanDate_dateTimePicker = new MyWorkApplication.Classes.BCDateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Visits_DataGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 681);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 276F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 316F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Visits_DataGridView, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.Person_Name_textBox2, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.MicroProject_ID_textBox2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.LoanAmount_textBox2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.VisitType_comboBox2, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.Ledger_label, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.Beneficiary_FullName_label, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.Project_Number_label, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.OtherNotes_lable, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.Indicators_textBox, 1, 24);
            this.tableLayoutPanel1.Controls.Add(this.Indicators_label, 1, 23);
            this.tableLayoutPanel1.Controls.Add(this.AverageItemPrice_textBox, 1, 22);
            this.tableLayoutPanel1.Controls.Add(this.AverageItemPrice_label, 1, 21);
            this.tableLayoutPanel1.Controls.Add(this.AverageSales_textBox, 1, 20);
            this.tableLayoutPanel1.Controls.Add(this.AverageSales_label, 1, 19);
            this.tableLayoutPanel1.Controls.Add(this.ProfitRatio_textBox, 1, 17);
            this.tableLayoutPanel1.Controls.Add(this.ProfitRatio_label, 1, 16);
            this.tableLayoutPanel1.Controls.Add(this.LedgerComments_textBox, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.Continuance_textBox, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.NoLedger_radioButton, 4, 12);
            this.tableLayoutPanel1.Controls.Add(this.YesLedger_radioButton, 3, 12);
            this.tableLayoutPanel1.Controls.Add(this.IF_Datagrid_lable, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.Vistors_lable, 1, 28);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 29);
            this.tableLayoutPanel1.Controls.Add(this.Continuance_label, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 30);
            this.tableLayoutPanel1.Controls.Add(this.VisitDate_dateTimePicker, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.LoanDate_dateTimePicker, 4, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.RowCount = 32;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.050834F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.765687F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.429706F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1247, 1259);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // Visits_DataGridView
            // 
            this.Visits_DataGridView.AllowUserToAddRows = false;
            this.Visits_DataGridView.AllowUserToDeleteRows = false;
            this.Visits_DataGridView.AllowUserToOrderColumns = true;
            this.Visits_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Visits_DataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.Visits_DataGridView.BackgroundColor = System.Drawing.Color.White;
            this.Visits_DataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Visits_DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Visits_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.Visits_DataGridView, 4);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Visits_DataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.Visits_DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Visits_DataGridView.EnableHeadersVisualStyles = false;
            this.Visits_DataGridView.GridColor = System.Drawing.Color.Gray;
            this.Visits_DataGridView.Location = new System.Drawing.Point(187, 198);
            this.Visits_DataGridView.Name = "Visits_DataGridView";
            this.Visits_DataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Visits_DataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Visits_DataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Janna LT", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Visits_DataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.tableLayoutPanel1.SetRowSpan(this.Visits_DataGridView, 3);
            this.Visits_DataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Janna LT", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Visits_DataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Visits_DataGridView.RowTemplate.Height = 26;
            this.Visits_DataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Visits_DataGridView.Size = new System.Drawing.Size(874, 111);
            this.Visits_DataGridView.TabIndex = 455;
            this.Visits_DataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Visits_DataGridView_CellClick);
            this.Visits_DataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Visits_DataGridView_CellContentClick);
            this.Visits_DataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.Visits_DataGridView_CellPainting);
            this.Visits_DataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Visits_DataGridView_RowHeaderMouseClick);
            // 
            // Person_Name_textBox2
            // 
            this.Person_Name_textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Person_Name_textBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Person_Name_textBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Person_Name_textBox2.BackColor = System.Drawing.Color.White;
            this.Person_Name_textBox2.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Person_Name_textBox2.Location = new System.Drawing.Point(214, 42);
            this.Person_Name_textBox2.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.Person_Name_textBox2.Name = "Person_Name_textBox2";
            this.Person_Name_textBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Person_Name_textBox2.Size = new System.Drawing.Size(283, 33);
            this.Person_Name_textBox2.TabIndex = 101;
            this.Person_Name_textBox2.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            this.Person_Name_textBox2.Leave += new System.EventHandler(this.Person_Name_textBox2_Leave);
            // 
            // MicroProject_ID_textBox2
            // 
            this.MicroProject_ID_textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MicroProject_ID_textBox2.BackColor = System.Drawing.Color.White;
            this.MicroProject_ID_textBox2.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MicroProject_ID_textBox2.Location = new System.Drawing.Point(667, 42);
            this.MicroProject_ID_textBox2.Name = "MicroProject_ID_textBox2";
            this.MicroProject_ID_textBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MicroProject_ID_textBox2.Size = new System.Drawing.Size(270, 33);
            this.MicroProject_ID_textBox2.TabIndex = 100;
            this.MicroProject_ID_textBox2.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            this.MicroProject_ID_textBox2.Leave += new System.EventHandler(this.MicroProject_ID_textBox2_Leave);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(943, 83);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(100, 29);
            this.label9.TabIndex = 260;
            this.label9.Text = "المبلغ الممول:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(503, 83);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(147, 29);
            this.label10.TabIndex = 261;
            this.label10.Text = "تاريخ انطلاق المشروع:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoanAmount_textBox2
            // 
            this.LoanAmount_textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LoanAmount_textBox2.BackColor = System.Drawing.Color.White;
            this.LoanAmount_textBox2.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoanAmount_textBox2.Location = new System.Drawing.Point(667, 81);
            this.LoanAmount_textBox2.Name = "LoanAmount_textBox2";
            this.LoanAmount_textBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LoanAmount_textBox2.Size = new System.Drawing.Size(270, 33);
            this.LoanAmount_textBox2.TabIndex = 102;
            this.LoanAmount_textBox2.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(943, 122);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label11.Size = new System.Drawing.Size(76, 29);
            this.label11.TabIndex = 259;
            this.label11.Text = "نوع الزيارة:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VisitType_comboBox2
            // 
            this.VisitType_comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.VisitType_comboBox2.BackColor = System.Drawing.Color.White;
            this.VisitType_comboBox2.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisitType_comboBox2.FormattingEnabled = true;
            this.VisitType_comboBox2.Items.AddRange(new object[] {
            "MC",
            "MSC",
            "MCSC",
            "CSC",
            "M",
            "C",
            "SC"});
            this.VisitType_comboBox2.Location = new System.Drawing.Point(667, 120);
            this.VisitType_comboBox2.Name = "VisitType_comboBox2";
            this.VisitType_comboBox2.Size = new System.Drawing.Size(270, 34);
            this.VisitType_comboBox2.TabIndex = 10;
            this.VisitType_comboBox2.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(503, 122);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label12.Size = new System.Drawing.Size(81, 29);
            this.label12.TabIndex = 258;
            this.label12.Text = "تاريخ الزيارة:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ledger_label
            // 
            this.Ledger_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Ledger_label.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.Ledger_label, 2);
            this.Ledger_label.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ledger_label.ForeColor = System.Drawing.Color.Black;
            this.Ledger_label.Location = new System.Drawing.Point(855, 473);
            this.Ledger_label.Name = "Ledger_label";
            this.Ledger_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ledger_label.Size = new System.Drawing.Size(206, 29);
            this.Ledger_label.TabIndex = 263;
            this.Ledger_label.Text = "هل يتم الالتزام بدفتر الحسابات؟";
            // 
            // Beneficiary_FullName_label
            // 
            this.Beneficiary_FullName_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Beneficiary_FullName_label.AutoSize = true;
            this.Beneficiary_FullName_label.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Beneficiary_FullName_label.ForeColor = System.Drawing.Color.Black;
            this.Beneficiary_FullName_label.Location = new System.Drawing.Point(503, 44);
            this.Beneficiary_FullName_label.Name = "Beneficiary_FullName_label";
            this.Beneficiary_FullName_label.Size = new System.Drawing.Size(109, 29);
            this.Beneficiary_FullName_label.TabIndex = 15;
            this.Beneficiary_FullName_label.Text = "اسم المستفيد:";
            this.Beneficiary_FullName_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Project_Number_label
            // 
            this.Project_Number_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Project_Number_label.AutoSize = true;
            this.Project_Number_label.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Project_Number_label.ForeColor = System.Drawing.Color.Black;
            this.Project_Number_label.Location = new System.Drawing.Point(943, 44);
            this.Project_Number_label.Name = "Project_Number_label";
            this.Project_Number_label.Size = new System.Drawing.Size(98, 29);
            this.Project_Number_label.TabIndex = 0;
            this.Project_Number_label.Text = "رقم المشروع:";
            this.Project_Number_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OtherNotes_lable
            // 
            this.OtherNotes_lable.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.OtherNotes_lable.AutoSize = true;
            this.OtherNotes_lable.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OtherNotes_lable.ForeColor = System.Drawing.Color.Black;
            this.OtherNotes_lable.Location = new System.Drawing.Point(958, 512);
            this.OtherNotes_lable.Name = "OtherNotes_lable";
            this.OtherNotes_lable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.OtherNotes_lable.Size = new System.Drawing.Size(103, 29);
            this.OtherNotes_lable.TabIndex = 276;
            this.OtherNotes_lable.Text = "ملاحظات أخرى:";
            // 
            // Indicators_textBox
            // 
            this.Indicators_textBox.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.Indicators_textBox, 4);
            this.Indicators_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Indicators_textBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Indicators_textBox.Location = new System.Drawing.Point(187, 939);
            this.Indicators_textBox.Multiline = true;
            this.Indicators_textBox.Name = "Indicators_textBox";
            this.Indicators_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.SetRowSpan(this.Indicators_textBox, 4);
            this.Indicators_textBox.Size = new System.Drawing.Size(874, 150);
            this.Indicators_textBox.TabIndex = 19;
            this.Indicators_textBox.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // Indicators_label
            // 
            this.Indicators_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Indicators_label.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.Indicators_label, 4);
            this.Indicators_label.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Indicators_label.ForeColor = System.Drawing.Color.Black;
            this.Indicators_label.Location = new System.Drawing.Point(825, 902);
            this.Indicators_label.Name = "Indicators_label";
            this.Indicators_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Indicators_label.Size = new System.Drawing.Size(236, 29);
            this.Indicators_label.TabIndex = 293;
            this.Indicators_label.Text = "مؤشرات خاصة لاحظها فريق الـ M&&E :";
            // 
            // AverageItemPrice_textBox
            // 
            this.AverageItemPrice_textBox.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.AverageItemPrice_textBox, 4);
            this.AverageItemPrice_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AverageItemPrice_textBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AverageItemPrice_textBox.Location = new System.Drawing.Point(187, 861);
            this.AverageItemPrice_textBox.Name = "AverageItemPrice_textBox";
            this.AverageItemPrice_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.AverageItemPrice_textBox.Size = new System.Drawing.Size(874, 33);
            this.AverageItemPrice_textBox.TabIndex = 18;
            this.AverageItemPrice_textBox.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // AverageItemPrice_label
            // 
            this.AverageItemPrice_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AverageItemPrice_label.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.AverageItemPrice_label, 3);
            this.AverageItemPrice_label.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AverageItemPrice_label.ForeColor = System.Drawing.Color.Black;
            this.AverageItemPrice_label.Location = new System.Drawing.Point(830, 824);
            this.AverageItemPrice_label.Name = "AverageItemPrice_label";
            this.AverageItemPrice_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.AverageItemPrice_label.Size = new System.Drawing.Size(231, 29);
            this.AverageItemPrice_label.TabIndex = 272;
            this.AverageItemPrice_label.Text = "ما هو متوسط سعر القطعة/الخدمة؟";
            // 
            // AverageSales_textBox
            // 
            this.AverageSales_textBox.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.AverageSales_textBox, 4);
            this.AverageSales_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AverageSales_textBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AverageSales_textBox.Location = new System.Drawing.Point(187, 783);
            this.AverageSales_textBox.Name = "AverageSales_textBox";
            this.AverageSales_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.AverageSales_textBox.Size = new System.Drawing.Size(874, 33);
            this.AverageSales_textBox.TabIndex = 17;
            this.AverageSales_textBox.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // AverageSales_label
            // 
            this.AverageSales_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AverageSales_label.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.AverageSales_label, 4);
            this.AverageSales_label.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AverageSales_label.ForeColor = System.Drawing.Color.Black;
            this.AverageSales_label.Location = new System.Drawing.Point(763, 746);
            this.AverageSales_label.Name = "AverageSales_label";
            this.AverageSales_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.AverageSales_label.Size = new System.Drawing.Size(298, 29);
            this.AverageSales_label.TabIndex = 274;
            this.AverageSales_label.Text = "ما هو متوسط المبيعات الشهرية (قطعة/خدمة)؟";
            // 
            // ProfitRatio_textBox
            // 
            this.ProfitRatio_textBox.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.ProfitRatio_textBox, 4);
            this.ProfitRatio_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProfitRatio_textBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProfitRatio_textBox.Location = new System.Drawing.Point(187, 666);
            this.ProfitRatio_textBox.Multiline = true;
            this.ProfitRatio_textBox.Name = "ProfitRatio_textBox";
            this.ProfitRatio_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.SetRowSpan(this.ProfitRatio_textBox, 2);
            this.ProfitRatio_textBox.Size = new System.Drawing.Size(874, 72);
            this.ProfitRatio_textBox.TabIndex = 16;
            this.ProfitRatio_textBox.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // ProfitRatio_label
            // 
            this.ProfitRatio_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ProfitRatio_label.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.ProfitRatio_label, 4);
            this.ProfitRatio_label.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProfitRatio_label.ForeColor = System.Drawing.Color.Black;
            this.ProfitRatio_label.Location = new System.Drawing.Point(634, 629);
            this.ProfitRatio_label.Name = "ProfitRatio_label";
            this.ProfitRatio_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ProfitRatio_label.Size = new System.Drawing.Size(427, 29);
            this.ProfitRatio_label.TabIndex = 286;
            this.ProfitRatio_label.Text = "هل تتوقع زيادة أو انخفاض في نسبة الربح في الفترة المقبلة؟ ولماذا؟";
            this.ProfitRatio_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LedgerComments_textBox
            // 
            this.LedgerComments_textBox.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.LedgerComments_textBox, 4);
            this.LedgerComments_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LedgerComments_textBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LedgerComments_textBox.Location = new System.Drawing.Point(187, 549);
            this.LedgerComments_textBox.Multiline = true;
            this.LedgerComments_textBox.Name = "LedgerComments_textBox";
            this.LedgerComments_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.SetRowSpan(this.LedgerComments_textBox, 2);
            this.LedgerComments_textBox.Size = new System.Drawing.Size(874, 72);
            this.LedgerComments_textBox.TabIndex = 15;
            this.LedgerComments_textBox.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // Continuance_textBox
            // 
            this.Continuance_textBox.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.Continuance_textBox, 4);
            this.Continuance_textBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Continuance_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Continuance_textBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Continuance_textBox.Location = new System.Drawing.Point(187, 393);
            this.Continuance_textBox.Multiline = true;
            this.Continuance_textBox.Name = "Continuance_textBox";
            this.Continuance_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.SetRowSpan(this.Continuance_textBox, 2);
            this.Continuance_textBox.Size = new System.Drawing.Size(874, 72);
            this.Continuance_textBox.TabIndex = 11;
            this.Continuance_textBox.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // NoLedger_radioButton
            // 
            this.NoLedger_radioButton.AutoSize = true;
            this.NoLedger_radioButton.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoLedger_radioButton.ForeColor = System.Drawing.Color.Black;
            this.NoLedger_radioButton.Location = new System.Drawing.Point(458, 470);
            this.NoLedger_radioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NoLedger_radioButton.Name = "NoLedger_radioButton";
            this.NoLedger_radioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NoLedger_radioButton.Size = new System.Drawing.Size(39, 30);
            this.NoLedger_radioButton.TabIndex = 14;
            this.NoLedger_radioButton.TabStop = true;
            this.NoLedger_radioButton.Text = "لا";
            this.NoLedger_radioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NoLedger_radioButton.UseVisualStyleBackColor = true;
            // 
            // YesLedger_radioButton
            // 
            this.YesLedger_radioButton.AutoSize = true;
            this.YesLedger_radioButton.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YesLedger_radioButton.ForeColor = System.Drawing.Color.Black;
            this.YesLedger_radioButton.Location = new System.Drawing.Point(608, 470);
            this.YesLedger_radioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.YesLedger_radioButton.Name = "YesLedger_radioButton";
            this.YesLedger_radioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.YesLedger_radioButton.Size = new System.Drawing.Size(53, 30);
            this.YesLedger_radioButton.TabIndex = 13;
            this.YesLedger_radioButton.TabStop = true;
            this.YesLedger_radioButton.Text = "نعم";
            this.YesLedger_radioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.YesLedger_radioButton.UseVisualStyleBackColor = true;
            // 
            // IF_Datagrid_lable
            // 
            this.IF_Datagrid_lable.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.IF_Datagrid_lable.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.IF_Datagrid_lable, 2);
            this.IF_Datagrid_lable.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IF_Datagrid_lable.ForeColor = System.Drawing.Color.Black;
            this.IF_Datagrid_lable.Location = new System.Drawing.Point(869, 161);
            this.IF_Datagrid_lable.Name = "IF_Datagrid_lable";
            this.IF_Datagrid_lable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.IF_Datagrid_lable.Size = new System.Drawing.Size(192, 29);
            this.IF_Datagrid_lable.TabIndex = 456;
            this.IF_Datagrid_lable.Text = "الزيارات المالية لهذا المستفيد:";
            this.IF_Datagrid_lable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Vistors_lable
            // 
            this.Vistors_lable.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Vistors_lable.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.Vistors_lable, 2);
            this.Vistors_lable.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Vistors_lable.ForeColor = System.Drawing.Color.Black;
            this.Vistors_lable.Location = new System.Drawing.Point(930, 1097);
            this.Vistors_lable.Name = "Vistors_lable";
            this.Vistors_lable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Vistors_lable.Size = new System.Drawing.Size(131, 29);
            this.Vistors_lable.TabIndex = 396;
            this.Vistors_lable.Text = "تمت الزيارة من قبل:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 4);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.V4_comboBox, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.V3_comboBox, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.V2_comboBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.V1_comboBox, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(187, 1134);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(874, 44);
            this.tableLayoutPanel2.TabIndex = 20;
            this.tableLayoutPanel2.TabStop = true;
            // 
            // V4_comboBox
            // 
            this.V4_comboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.V4_comboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.V4_comboBox.BackColor = System.Drawing.Color.White;
            this.V4_comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.V4_comboBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.V4_comboBox.FormattingEnabled = true;
            this.V4_comboBox.Items.AddRange(new object[] {
            "MC",
            "MSC",
            "MCSC",
            "CSC",
            "M",
            "C",
            "SC"});
            this.V4_comboBox.Location = new System.Drawing.Point(3, 3);
            this.V4_comboBox.Name = "V4_comboBox";
            this.V4_comboBox.Size = new System.Drawing.Size(214, 34);
            this.V4_comboBox.TabIndex = 24;
            this.V4_comboBox.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // V3_comboBox
            // 
            this.V3_comboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.V3_comboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.V3_comboBox.BackColor = System.Drawing.Color.White;
            this.V3_comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.V3_comboBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.V3_comboBox.FormattingEnabled = true;
            this.V3_comboBox.Items.AddRange(new object[] {
            "MC",
            "MSC",
            "MCSC",
            "CSC",
            "M",
            "C",
            "SC"});
            this.V3_comboBox.Location = new System.Drawing.Point(223, 3);
            this.V3_comboBox.Name = "V3_comboBox";
            this.V3_comboBox.Size = new System.Drawing.Size(212, 34);
            this.V3_comboBox.TabIndex = 23;
            this.V3_comboBox.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // V2_comboBox
            // 
            this.V2_comboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.V2_comboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.V2_comboBox.BackColor = System.Drawing.Color.White;
            this.V2_comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.V2_comboBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.V2_comboBox.FormattingEnabled = true;
            this.V2_comboBox.Items.AddRange(new object[] {
            "MC",
            "MSC",
            "MCSC",
            "CSC",
            "M",
            "C",
            "SC"});
            this.V2_comboBox.Location = new System.Drawing.Point(441, 3);
            this.V2_comboBox.Name = "V2_comboBox";
            this.V2_comboBox.Size = new System.Drawing.Size(212, 34);
            this.V2_comboBox.TabIndex = 22;
            this.V2_comboBox.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // V1_comboBox
            // 
            this.V1_comboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.V1_comboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.V1_comboBox.BackColor = System.Drawing.Color.White;
            this.V1_comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.V1_comboBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.V1_comboBox.FormattingEnabled = true;
            this.V1_comboBox.Items.AddRange(new object[] {
            "MC",
            "MSC",
            "MCSC",
            "CSC",
            "M",
            "C",
            "SC"});
            this.V1_comboBox.Location = new System.Drawing.Point(659, 3);
            this.V1_comboBox.Name = "V1_comboBox";
            this.V1_comboBox.Size = new System.Drawing.Size(212, 34);
            this.V1_comboBox.TabIndex = 21;
            this.V1_comboBox.TextChanged += new System.EventHandler(this.V1_comboBox_TextChanged);
            // 
            // Continuance_label
            // 
            this.Continuance_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Continuance_label.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.Continuance_label, 4);
            this.Continuance_label.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Continuance_label.ForeColor = System.Drawing.Color.Black;
            this.Continuance_label.Location = new System.Drawing.Point(497, 356);
            this.Continuance_label.Name = "Continuance_label";
            this.Continuance_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Continuance_label.Size = new System.Drawing.Size(564, 29);
            this.Continuance_label.TabIndex = 262;
            this.Continuance_label.Text = "هل يكفي الربح الشهري لتسديد الأقساط ولإدخار مبلغ من المال لتحقيق استمرارية المشرو" +
    "ع؟";
            this.Continuance_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 6);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.InsertVisit_button, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 1184);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1241, 53);
            this.tableLayoutPanel3.TabIndex = 462;
            // 
            // InsertVisit_button
            // 
            this.InsertVisit_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.InsertVisit_button.BackColor = System.Drawing.Color.Transparent;
            this.InsertVisit_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.Save2_D;
            this.InsertVisit_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.InsertVisit_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.InsertVisit_button.FlatAppearance.BorderSize = 0;
            this.InsertVisit_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.InsertVisit_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.InsertVisit_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.InsertVisit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InsertVisit_button.Font = new System.Drawing.Font("Janna LT", 10F);
            this.InsertVisit_button.ForeColor = System.Drawing.Color.Black;
            this.InsertVisit_button.Location = new System.Drawing.Point(562, 11);
            this.InsertVisit_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InsertVisit_button.Name = "InsertVisit_button";
            this.InsertVisit_button.Size = new System.Drawing.Size(120, 40);
            this.InsertVisit_button.TabIndex = 25;
            this.toolTip1.SetToolTip(this.InsertVisit_button, "حفظ الزيارة");
            this.InsertVisit_button.UseVisualStyleBackColor = false;
            this.InsertVisit_button.Click += new System.EventHandler(this.InsertVisit_button_Click);
            this.InsertVisit_button.MouseEnter += new System.EventHandler(this.InsertVisit_button_MouseEnter);
            this.InsertVisit_button.MouseLeave += new System.EventHandler(this.InsertVisit_button_MouseLeave);
            // 
            // VisitDate_dateTimePicker
            // 
            this.VisitDate_dateTimePicker.BackColor = System.Drawing.Color.White;
            this.VisitDate_dateTimePicker.CalendarForeColor = System.Drawing.Color.Black;
            this.VisitDate_dateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.VisitDate_dateTimePicker.ForeColor = System.Drawing.Color.Black;
            this.VisitDate_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VisitDate_dateTimePicker.Location = new System.Drawing.Point(214, 120);
            this.VisitDate_dateTimePicker.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.VisitDate_dateTimePicker.Name = "VisitDate_dateTimePicker";
            this.VisitDate_dateTimePicker.RightToLeftLayout = true;
            this.VisitDate_dateTimePicker.Size = new System.Drawing.Size(283, 33);
            this.VisitDate_dateTimePicker.TabIndex = 466;
            // 
            // LoanDate_dateTimePicker
            // 
            this.LoanDate_dateTimePicker.BackColor = System.Drawing.Color.White;
            this.LoanDate_dateTimePicker.CalendarForeColor = System.Drawing.Color.Black;
            this.LoanDate_dateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.LoanDate_dateTimePicker.ForeColor = System.Drawing.Color.Black;
            this.LoanDate_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.LoanDate_dateTimePicker.Location = new System.Drawing.Point(214, 81);
            this.LoanDate_dateTimePicker.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.LoanDate_dateTimePicker.Name = "LoanDate_dateTimePicker";
            this.LoanDate_dateTimePicker.RightToLeftLayout = true;
            this.LoanDate_dateTimePicker.Size = new System.Drawing.Size(283, 33);
            this.LoanDate_dateTimePicker.TabIndex = 465;
            // 
            // Financial_Visit_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Financial_Visit_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Financial_Visit_Form";
            this.Load += new System.EventHandler(this.Financial_Visit_Form_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Visits_DataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView Visits_DataGridView;
        private System.Windows.Forms.TextBox Person_Name_textBox2;
        private System.Windows.Forms.TextBox MicroProject_ID_textBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox LoanAmount_textBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox VisitType_comboBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label Ledger_label;
        private System.Windows.Forms.Label Beneficiary_FullName_label;
        private System.Windows.Forms.Label Project_Number_label;
        private System.Windows.Forms.Label OtherNotes_lable;
        private System.Windows.Forms.Label Vistors_lable;
        private System.Windows.Forms.TextBox Indicators_textBox;
        private System.Windows.Forms.Label Indicators_label;
        private System.Windows.Forms.TextBox AverageItemPrice_textBox;
        private System.Windows.Forms.Label AverageItemPrice_label;
        private System.Windows.Forms.TextBox AverageSales_textBox;
        private System.Windows.Forms.Label AverageSales_label;
        private System.Windows.Forms.TextBox ProfitRatio_textBox;
        private System.Windows.Forms.Label ProfitRatio_label;
        private System.Windows.Forms.TextBox LedgerComments_textBox;
        private System.Windows.Forms.TextBox Continuance_textBox;
        private System.Windows.Forms.RadioButton NoLedger_radioButton;
        private System.Windows.Forms.RadioButton YesLedger_radioButton;
        private System.Windows.Forms.Label Continuance_label;
        private System.Windows.Forms.Label IF_Datagrid_lable;
        private System.Windows.Forms.Button InsertVisit_button;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox V4_comboBox;
        private System.Windows.Forms.ComboBox V3_comboBox;
        private System.Windows.Forms.ComboBox V2_comboBox;
        private System.Windows.Forms.ComboBox V1_comboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ToolTip toolTip1;
        private Classes.BCDateTimePicker VisitDate_dateTimePicker;
        private Classes.BCDateTimePicker LoanDate_dateTimePicker;
    }
}