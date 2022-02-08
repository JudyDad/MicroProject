namespace MyWorkApplication.Visit_Forms
{
    partial class Monitoring_Visit_Results_Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Benficiaries_dataGridView = new System.Windows.Forms.DataGridView();
            this.Answers_dataGridView = new System.Windows.Forms.DataGridView();
            this.Q_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Q_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ans1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ans1_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ans1_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ans2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ans2_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ans2_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ans3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ans3_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ans3_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.all_ans_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Selected_label = new System.Windows.Forms.Label();
            this.Count_label = new System.Windows.Forms.Label();
            this.ExportToExcel_button = new System.Windows.Forms.Button();
            this.SearchBy_textBox = new System.Windows.Forms.TextBox();
            this.Visits_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.AllV_button = new System.Windows.Forms.Button();
            this.LastV_button = new System.Windows.Forms.Button();
            this.FirstV_button = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.DateTo_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SubCategory_comboBox = new System.Windows.Forms.ComboBox();
            this.SubCategory_label = new System.Windows.Forms.Label();
            this.FundedDateFrom_label = new System.Windows.Forms.Label();
            this.FundedDateTo_label = new System.Windows.Forms.Label();
            this.Visits_label = new System.Windows.Forms.Label();
            this.Visits_comboBox = new System.Windows.Forms.ComboBox();
            this.VisitNumber_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Category_comboBox = new System.Windows.Forms.ComboBox();
            this.MP_Category_label = new System.Windows.Forms.Label();
            this.DateFrom_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Benficiaries_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Answers_dataGridView)).BeginInit();
            this.Visits_tableLayoutPanel.SuspendLayout();
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
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1262, 673);
            this.panel1.TabIndex = 417;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.741497F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.93033F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.653356F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.34311F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.34311F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Controls.Add(this.Benficiaries_dataGridView, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.Answers_dataGridView, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Selected_label, 6, 4);
            this.tableLayoutPanel1.Controls.Add(this.Count_label, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.ExportToExcel_button, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.SearchBy_textBox, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.Visits_tableLayoutPanel, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.578938F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.42942F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.42942F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.36256F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.9691F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1262, 673);
            this.tableLayoutPanel1.TabIndex = 497;
            // 
            // Benficiaries_dataGridView
            // 
            this.Benficiaries_dataGridView.AllowUserToAddRows = false;
            this.Benficiaries_dataGridView.AllowUserToDeleteRows = false;
            this.Benficiaries_dataGridView.AllowUserToOrderColumns = true;
            this.Benficiaries_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Benficiaries_dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.Benficiaries_dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Benficiaries_dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Benficiaries_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Benficiaries_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.Benficiaries_dataGridView, 2);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Benficiaries_dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.Benficiaries_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Benficiaries_dataGridView.EnableHeadersVisualStyles = false;
            this.Benficiaries_dataGridView.GridColor = System.Drawing.Color.Gray;
            this.Benficiaries_dataGridView.Location = new System.Drawing.Point(32, 148);
            this.Benficiaries_dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.Benficiaries_dataGridView.Name = "Benficiaries_dataGridView";
            this.Benficiaries_dataGridView.ReadOnly = true;
            this.Benficiaries_dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Benficiaries_dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Benficiaries_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Benficiaries_dataGridView.RowTemplate.Height = 28;
            this.Benficiaries_dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Benficiaries_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Benficiaries_dataGridView.Size = new System.Drawing.Size(310, 480);
            this.Benficiaries_dataGridView.TabIndex = 407;
            this.Benficiaries_dataGridView.VirtualMode = true;
            this.Benficiaries_dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Answers_dataGridView_DataError);
            // 
            // Answers_dataGridView
            // 
            this.Answers_dataGridView.AllowUserToResizeRows = false;
            this.Answers_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Answers_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.Answers_dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.Answers_dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Answers_dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Answers_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Answers_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Answers_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Q_ID,
            this.Q_Name,
            this.Ans1,
            this.Ans1_count,
            this.Ans1_ID,
            this.Ans2,
            this.Ans2_count,
            this.Ans2_ID,
            this.Ans3,
            this.Ans3_count,
            this.Ans3_ID,
            this.all_ans_count});
            this.tableLayoutPanel1.SetColumnSpan(this.Answers_dataGridView, 3);
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Answers_dataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.Answers_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Answers_dataGridView.EnableHeadersVisualStyles = false;
            this.Answers_dataGridView.GridColor = System.Drawing.Color.DarkGray;
            this.Answers_dataGridView.Location = new System.Drawing.Point(369, 147);
            this.Answers_dataGridView.Name = "Answers_dataGridView";
            this.Answers_dataGridView.ReadOnly = true;
            this.Answers_dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Answers_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Answers_dataGridView.RowTemplate.Height = 35;
            this.Answers_dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Answers_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Answers_dataGridView.Size = new System.Drawing.Size(870, 482);
            this.Answers_dataGridView.StandardTab = true;
            this.Answers_dataGridView.TabIndex = 498;
            this.Answers_dataGridView.SelectionChanged += new System.EventHandler(this.Answers_dataGridView_SelectionChanged);
            // 
            // Q_ID
            // 
            this.Q_ID.FillWeight = 20F;
            this.Q_ID.HeaderText = "ID";
            this.Q_ID.Name = "Q_ID";
            this.Q_ID.ReadOnly = true;
            // 
            // Q_Name
            // 
            this.Q_Name.FillWeight = 125F;
            this.Q_Name.HeaderText = "السؤال";
            this.Q_Name.Name = "Q_Name";
            this.Q_Name.ReadOnly = true;
            // 
            // Ans1
            // 
            this.Ans1.FillWeight = 40F;
            this.Ans1.HeaderText = "إجابة1";
            this.Ans1.Name = "Ans1";
            this.Ans1.ReadOnly = true;
            // 
            // Ans1_count
            // 
            this.Ans1_count.FillWeight = 25F;
            this.Ans1_count.HeaderText = "العدد";
            this.Ans1_count.Name = "Ans1_count";
            this.Ans1_count.ReadOnly = true;
            // 
            // Ans1_ID
            // 
            this.Ans1_ID.FillWeight = 15F;
            this.Ans1_ID.HeaderText = "Ans1_ID";
            this.Ans1_ID.Name = "Ans1_ID";
            this.Ans1_ID.ReadOnly = true;
            this.Ans1_ID.Visible = false;
            // 
            // Ans2
            // 
            this.Ans2.FillWeight = 40F;
            this.Ans2.HeaderText = "إجابة2";
            this.Ans2.Name = "Ans2";
            this.Ans2.ReadOnly = true;
            // 
            // Ans2_count
            // 
            this.Ans2_count.FillWeight = 25F;
            this.Ans2_count.HeaderText = "العدد";
            this.Ans2_count.Name = "Ans2_count";
            this.Ans2_count.ReadOnly = true;
            // 
            // Ans2_ID
            // 
            this.Ans2_ID.FillWeight = 15F;
            this.Ans2_ID.HeaderText = "Ans2_ID";
            this.Ans2_ID.Name = "Ans2_ID";
            this.Ans2_ID.ReadOnly = true;
            this.Ans2_ID.Visible = false;
            // 
            // Ans3
            // 
            this.Ans3.FillWeight = 40F;
            this.Ans3.HeaderText = "إجابة3";
            this.Ans3.Name = "Ans3";
            this.Ans3.ReadOnly = true;
            // 
            // Ans3_count
            // 
            this.Ans3_count.FillWeight = 25F;
            this.Ans3_count.HeaderText = "العدد";
            this.Ans3_count.Name = "Ans3_count";
            this.Ans3_count.ReadOnly = true;
            // 
            // Ans3_ID
            // 
            this.Ans3_ID.FillWeight = 15F;
            this.Ans3_ID.HeaderText = "Ans3_ID";
            this.Ans3_ID.Name = "Ans3_ID";
            this.Ans3_ID.ReadOnly = true;
            this.Ans3_ID.Visible = false;
            // 
            // all_ans_count
            // 
            this.all_ans_count.FillWeight = 35F;
            this.all_ans_count.HeaderText = "العدد الكلي";
            this.all_ans_count.Name = "all_ans_count";
            this.all_ans_count.ReadOnly = true;
            // 
            // Selected_label
            // 
            this.Selected_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Selected_label.AutoSize = true;
            this.Selected_label.BackColor = System.Drawing.Color.Transparent;
            this.Selected_label.Font = new System.Drawing.Font("Janna LT", 10F);
            this.Selected_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Selected_label.Location = new System.Drawing.Point(28, 639);
            this.Selected_label.Margin = new System.Windows.Forms.Padding(0);
            this.Selected_label.Name = "Selected_label";
            this.Selected_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Selected_label.Size = new System.Drawing.Size(159, 26);
            this.Selected_label.TabIndex = 476;
            this.Selected_label.Text = "Selected";
            this.Selected_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Count_label
            // 
            this.Count_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Count_label.AutoSize = true;
            this.Count_label.BackColor = System.Drawing.Color.Transparent;
            this.Count_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Count_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Count_label.Location = new System.Drawing.Point(187, 639);
            this.Count_label.Margin = new System.Windows.Forms.Padding(0);
            this.Count_label.Name = "Count_label";
            this.Count_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Count_label.Size = new System.Drawing.Size(159, 26);
            this.Count_label.TabIndex = 499;
            this.Count_label.Text = "Count";
            this.Count_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ExportToExcel_button
            // 
            this.ExportToExcel_button.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ExportToExcel_button.AutoSize = true;
            this.ExportToExcel_button.BackColor = System.Drawing.Color.Transparent;
            this.ExportToExcel_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.Excel_D;
            this.ExportToExcel_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ExportToExcel_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ExportToExcel_button.FlatAppearance.BorderSize = 0;
            this.ExportToExcel_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ExportToExcel_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ExportToExcel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportToExcel_button.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportToExcel_button.ForeColor = System.Drawing.Color.White;
            this.ExportToExcel_button.Location = new System.Drawing.Point(1208, 637);
            this.ExportToExcel_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ExportToExcel_button.MaximumSize = new System.Drawing.Size(30, 30);
            this.ExportToExcel_button.MinimumSize = new System.Drawing.Size(30, 30);
            this.ExportToExcel_button.Name = "ExportToExcel_button";
            this.ExportToExcel_button.Size = new System.Drawing.Size(30, 30);
            this.ExportToExcel_button.TabIndex = 411;
            this.ExportToExcel_button.UseVisualStyleBackColor = false;
            this.ExportToExcel_button.Click += new System.EventHandler(this.ExportToExcel_button_Click);
            // 
            // SearchBy_textBox
            // 
            this.SearchBy_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBy_textBox.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.SearchBy_textBox, 2);
            this.SearchBy_textBox.Font = new System.Drawing.Font("Janna LT", 10.2F);
            this.SearchBy_textBox.Location = new System.Drawing.Point(31, 102);
            this.SearchBy_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchBy_textBox.Name = "SearchBy_textBox";
            this.SearchBy_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SearchBy_textBox.Size = new System.Drawing.Size(312, 33);
            this.SearchBy_textBox.TabIndex = 500;
            this.SearchBy_textBox.Text = "بحث...";
            this.SearchBy_textBox.TextChanged += new System.EventHandler(this.SearchBy_textBox_TextChanged);
            this.SearchBy_textBox.Enter += new System.EventHandler(this.SearchBy_textBox_Enter);
            this.SearchBy_textBox.Leave += new System.EventHandler(this.SearchBy_textBox_Leave);
            // 
            // Visits_tableLayoutPanel
            // 
            this.Visits_tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Visits_tableLayoutPanel.ColumnCount = 4;
            this.Visits_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.58053F));
            this.Visits_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.13982F));
            this.Visits_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.13982F));
            this.Visits_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.13982F));
            this.Visits_tableLayoutPanel.Controls.Add(this.AllV_button, 1, 0);
            this.Visits_tableLayoutPanel.Controls.Add(this.LastV_button, 3, 0);
            this.Visits_tableLayoutPanel.Controls.Add(this.FirstV_button, 2, 0);
            this.Visits_tableLayoutPanel.Location = new System.Drawing.Point(412, 97);
            this.Visits_tableLayoutPanel.Name = "Visits_tableLayoutPanel";
            this.Visits_tableLayoutPanel.RowCount = 1;
            this.Visits_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Visits_tableLayoutPanel.Size = new System.Drawing.Size(783, 44);
            this.Visits_tableLayoutPanel.TabIndex = 505;
            // 
            // AllV_button
            // 
            this.AllV_button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AllV_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.AllV_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.AllV_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AllV_button.Location = new System.Drawing.Point(460, 3);
            this.AllV_button.Name = "AllV_button";
            this.AllV_button.Size = new System.Drawing.Size(222, 38);
            this.AllV_button.TabIndex = 506;
            this.AllV_button.Text = "كل الزيارات";
            this.AllV_button.UseVisualStyleBackColor = true;
            this.AllV_button.Click += new System.EventHandler(this.AllV_button_Click);
            // 
            // LastV_button
            // 
            this.LastV_button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LastV_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.LastV_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.LastV_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LastV_button.Location = new System.Drawing.Point(3, 3);
            this.LastV_button.Name = "LastV_button";
            this.LastV_button.Size = new System.Drawing.Size(223, 38);
            this.LastV_button.TabIndex = 504;
            this.LastV_button.Text = "آخر زيارة لكل مستفيد";
            this.LastV_button.UseVisualStyleBackColor = true;
            this.LastV_button.Click += new System.EventHandler(this.LastV_button_Click);
            // 
            // FirstV_button
            // 
            this.FirstV_button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FirstV_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.FirstV_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.FirstV_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FirstV_button.Location = new System.Drawing.Point(232, 3);
            this.FirstV_button.Name = "FirstV_button";
            this.FirstV_button.Size = new System.Drawing.Size(222, 38);
            this.FirstV_button.TabIndex = 503;
            this.FirstV_button.Text = "أول زيارة لكل مستفيد";
            this.FirstV_button.UseVisualStyleBackColor = true;
            this.FirstV_button.Click += new System.EventHandler(this.FirstV_button_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.63786F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.24417F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.91221F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.06721F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.12894F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Controls.Add(this.DateTo_dateTimePicker, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.SubCategory_comboBox, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.SubCategory_label, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.FundedDateFrom_label, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.FundedDateTo_label, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.Visits_label, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.Visits_comboBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.VisitNumber_comboBox, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.Category_comboBox, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.MP_Category_label, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.DateFrom_dateTimePicker, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(412, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(783, 88);
            this.tableLayoutPanel3.TabIndex = 562;
            // 
            // DateTo_dateTimePicker
            // 
            this.DateTo_dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DateTo_dateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.DateTo_dateTimePicker.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTo_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTo_dateTimePicker.Location = new System.Drawing.Point(247, 10);
            this.DateTo_dateTimePicker.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.DateTo_dateTimePicker.Name = "DateTo_dateTimePicker";
            this.DateTo_dateTimePicker.Size = new System.Drawing.Size(143, 24);
            this.DateTo_dateTimePicker.TabIndex = 572;
            this.DateTo_dateTimePicker.ValueChanged += new System.EventHandler(this.DateFrom_bcDateTimePicker_ValueChanged);
            // 
            // SubCategory_comboBox
            // 
            this.SubCategory_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SubCategory_comboBox.BackColor = System.Drawing.Color.White;
            this.SubCategory_comboBox.Font = new System.Drawing.Font("Janna LT", 10F);
            this.SubCategory_comboBox.ForeColor = System.Drawing.Color.Black;
            this.SubCategory_comboBox.FormattingEnabled = true;
            this.SubCategory_comboBox.Location = new System.Drawing.Point(3, 49);
            this.SubCategory_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SubCategory_comboBox.Name = "SubCategory_comboBox";
            this.SubCategory_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SubCategory_comboBox.Size = new System.Drawing.Size(127, 32);
            this.SubCategory_comboBox.TabIndex = 570;
            this.SubCategory_comboBox.TextChanged += new System.EventHandler(this.SubCategory_comboBox_TextChanged);
            // 
            // SubCategory_label
            // 
            this.SubCategory_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SubCategory_label.AutoSize = true;
            this.SubCategory_label.BackColor = System.Drawing.Color.Transparent;
            this.SubCategory_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubCategory_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.SubCategory_label.Location = new System.Drawing.Point(133, 53);
            this.SubCategory_label.Margin = new System.Windows.Forms.Padding(0);
            this.SubCategory_label.Name = "SubCategory_label";
            this.SubCategory_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SubCategory_label.Size = new System.Drawing.Size(51, 26);
            this.SubCategory_label.TabIndex = 569;
            this.SubCategory_label.Text = "المهنة:";
            // 
            // FundedDateFrom_label
            // 
            this.FundedDateFrom_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FundedDateFrom_label.AutoSize = true;
            this.FundedDateFrom_label.Font = new System.Drawing.Font("Janna LT", 10.2F);
            this.FundedDateFrom_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FundedDateFrom_label.Location = new System.Drawing.Point(663, 9);
            this.FundedDateFrom_label.Margin = new System.Windows.Forms.Padding(2);
            this.FundedDateFrom_label.Name = "FundedDateFrom_label";
            this.FundedDateFrom_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundedDateFrom_label.Size = new System.Drawing.Size(104, 26);
            this.FundedDateFrom_label.TabIndex = 559;
            this.FundedDateFrom_label.Text = "تاريخ الزيارات من:";
            // 
            // FundedDateTo_label
            // 
            this.FundedDateTo_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FundedDateTo_label.AutoSize = true;
            this.FundedDateTo_label.Font = new System.Drawing.Font("Janna LT", 10.2F);
            this.FundedDateTo_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FundedDateTo_label.Location = new System.Drawing.Point(395, 9);
            this.FundedDateTo_label.Margin = new System.Windows.Forms.Padding(2);
            this.FundedDateTo_label.Name = "FundedDateTo_label";
            this.FundedDateTo_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundedDateTo_label.Size = new System.Drawing.Size(36, 26);
            this.FundedDateTo_label.TabIndex = 558;
            this.FundedDateTo_label.Text = "إلى:";
            // 
            // Visits_label
            // 
            this.Visits_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Visits_label.AutoSize = true;
            this.Visits_label.BackColor = System.Drawing.Color.Transparent;
            this.Visits_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Visits_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Visits_label.Location = new System.Drawing.Point(661, 53);
            this.Visits_label.Margin = new System.Windows.Forms.Padding(0);
            this.Visits_label.Name = "Visits_label";
            this.Visits_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Visits_label.Size = new System.Drawing.Size(87, 26);
            this.Visits_label.TabIndex = 475;
            this.Visits_label.Text = "نوع المشاريع:";
            // 
            // Visits_comboBox
            // 
            this.Visits_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Visits_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Visits_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Visits_comboBox.Font = new System.Drawing.Font("Janna LT", 10F);
            this.Visits_comboBox.FormattingEnabled = true;
            this.Visits_comboBox.Items.AddRange(new object[] {
            "الكل",
            "مركبات",
            "مشاريع أخرى"});
            this.Visits_comboBox.Location = new System.Drawing.Point(521, 48);
            this.Visits_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Visits_comboBox.Name = "Visits_comboBox";
            this.Visits_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Visits_comboBox.Size = new System.Drawing.Size(137, 32);
            this.Visits_comboBox.TabIndex = 474;
            this.Visits_comboBox.SelectedIndexChanged += new System.EventHandler(this.Visits_comboBox_SelectedIndexChanged);
            // 
            // VisitNumber_comboBox
            // 
            this.VisitNumber_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VisitNumber_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VisitNumber_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VisitNumber_comboBox.Font = new System.Drawing.Font("Janna LT", 10F);
            this.VisitNumber_comboBox.FormattingEnabled = true;
            this.VisitNumber_comboBox.Items.AddRange(new object[] {
            "الكل",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.VisitNumber_comboBox.Location = new System.Drawing.Point(3, 3);
            this.VisitNumber_comboBox.Name = "VisitNumber_comboBox";
            this.VisitNumber_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.VisitNumber_comboBox.Size = new System.Drawing.Size(127, 32);
            this.VisitNumber_comboBox.TabIndex = 502;
            this.VisitNumber_comboBox.SelectedIndexChanged += new System.EventHandler(this.Visits_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label1.Location = new System.Drawing.Point(133, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(74, 26);
            this.label1.TabIndex = 501;
            this.label1.Text = "رقم الزيارة:";
            // 
            // Category_comboBox
            // 
            this.Category_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Category_comboBox.BackColor = System.Drawing.Color.White;
            this.Category_comboBox.Font = new System.Drawing.Font("Janna LT", 10F);
            this.Category_comboBox.ForeColor = System.Drawing.Color.Black;
            this.Category_comboBox.FormattingEnabled = true;
            this.Category_comboBox.Location = new System.Drawing.Point(247, 49);
            this.Category_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Category_comboBox.Name = "Category_comboBox";
            this.Category_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Category_comboBox.Size = new System.Drawing.Size(143, 32);
            this.Category_comboBox.TabIndex = 567;
            this.Category_comboBox.TextChanged += new System.EventHandler(this.Category_comboBox_TextChanged);
            // 
            // MP_Category_label
            // 
            this.MP_Category_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.MP_Category_label.AutoSize = true;
            this.MP_Category_label.BackColor = System.Drawing.Color.Transparent;
            this.MP_Category_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MP_Category_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MP_Category_label.Location = new System.Drawing.Point(393, 53);
            this.MP_Category_label.Margin = new System.Windows.Forms.Padding(0);
            this.MP_Category_label.Name = "MP_Category_label";
            this.MP_Category_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MP_Category_label.Size = new System.Drawing.Size(98, 26);
            this.MP_Category_label.TabIndex = 568;
            this.MP_Category_label.Text = "صنف المشروع:";
            // 
            // DateFrom_dateTimePicker
            // 
            this.DateFrom_dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DateFrom_dateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.DateFrom_dateTimePicker.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateFrom_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateFrom_dateTimePicker.Location = new System.Drawing.Point(521, 10);
            this.DateFrom_dateTimePicker.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.DateFrom_dateTimePicker.Name = "DateFrom_dateTimePicker";
            this.DateFrom_dateTimePicker.Size = new System.Drawing.Size(137, 24);
            this.DateFrom_dateTimePicker.TabIndex = 571;
            this.DateFrom_dateTimePicker.Value = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.DateFrom_dateTimePicker.ValueChanged += new System.EventHandler(this.DateFrom_bcDateTimePicker_ValueChanged);
            // 
            // Monitoring_Visit_Results_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Monitoring_Visit_Results_Form";
            this.Text = "Monitoring_Visit_Results_Form";
            this.Load += new System.EventHandler(this.Monitoring_Visit_Results_Form_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Benficiaries_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Answers_dataGridView)).EndInit();
            this.Visits_tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView Benficiaries_dataGridView;
        private System.Windows.Forms.ComboBox Visits_comboBox;
        private System.Windows.Forms.Label Visits_label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView Answers_dataGridView;
        private System.Windows.Forms.Label Count_label;
        private System.Windows.Forms.TextBox SearchBy_textBox;
        private System.Windows.Forms.Button ExportToExcel_button;
        private System.Windows.Forms.Label Selected_label;
        private System.Windows.Forms.ComboBox VisitNumber_comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FirstV_button;
        private System.Windows.Forms.Button LastV_button;
        private System.Windows.Forms.TableLayoutPanel Visits_tableLayoutPanel;
        private System.Windows.Forms.Button AllV_button;
        private System.Windows.Forms.Label FundedDateFrom_label;
        private System.Windows.Forms.Label FundedDateTo_label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Q_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Q_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ans1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ans1_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ans1_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ans2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ans2_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ans2_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ans3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ans3_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ans3_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn all_ans_count;
        private System.Windows.Forms.ComboBox SubCategory_comboBox;
        private System.Windows.Forms.Label SubCategory_label;
        private System.Windows.Forms.ComboBox Category_comboBox;
        private System.Windows.Forms.Label MP_Category_label;
        private System.Windows.Forms.DateTimePicker DateTo_dateTimePicker;
        private System.Windows.Forms.DateTimePicker DateFrom_dateTimePicker;
    }
}