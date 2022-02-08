namespace MyWorkApplication
{
    partial class AllVisits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllVisits));
            this.Visits_DataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Users_comboBox = new System.Windows.Forms.ComboBox();
            this.Story_radioButton = new System.Windows.Forms.RadioButton();
            this.Financial_radioButton = new System.Windows.Forms.RadioButton();
            this.Initial_radioButton = new System.Windows.Forms.RadioButton();
            this.Search_TxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.Counter_textBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AddVisit_button = new System.Windows.Forms.Button();
            this.UpdateVisit_button = new System.Windows.Forms.Button();
            this.DeleteVisit_button = new System.Windows.Forms.Button();
            this.MainBack_button = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Project_label = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Visits_DataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Visits_DataGridView
            // 
            this.Visits_DataGridView.AllowUserToAddRows = false;
            this.Visits_DataGridView.AllowUserToDeleteRows = false;
            this.Visits_DataGridView.AllowUserToOrderColumns = true;
            this.Visits_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.Visits_DataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Proxima Nova Rg", 10.8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Visits_DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Visits_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Proxima Nova Rg", 10.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Visits_DataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.Visits_DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Visits_DataGridView.Location = new System.Drawing.Point(0, 127);
            this.Visits_DataGridView.Name = "Visits_DataGridView";
            this.Visits_DataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Proxima Nova Rg", 10.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Visits_DataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Visits_DataGridView.RowTemplate.Height = 26;
            this.Visits_DataGridView.Size = new System.Drawing.Size(1262, 502);
            this.Visits_DataGridView.TabIndex = 453;
            this.Visits_DataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Visits_DataGridView_RowHeaderMouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightGray;
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1262, 56);
            this.groupBox1.TabIndex = 452;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.711076F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.26806F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.47673F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.29856F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.006421F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.39961F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.39961F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.39961F));
            this.tableLayoutPanel2.Controls.Add(this.Users_comboBox, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.Story_radioButton, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.Financial_radioButton, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.Initial_radioButton, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.Search_TxtBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 10);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1256, 36);
            this.tableLayoutPanel2.TabIndex = 321;
            // 
            // Users_comboBox
            // 
            this.Users_comboBox.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.Users_comboBox.FormattingEnabled = true;
            this.Users_comboBox.Location = new System.Drawing.Point(536, 3);
            this.Users_comboBox.Name = "Users_comboBox";
            this.Users_comboBox.Size = new System.Drawing.Size(222, 29);
            this.Users_comboBox.TabIndex = 412;
            this.Users_comboBox.TextChanged += new System.EventHandler(this.Users_comboBox_TextChanged);
            // 
            // Story_radioButton
            // 
            this.Story_radioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Story_radioButton.AutoSize = true;
            this.Story_radioButton.Font = new System.Drawing.Font("Proxima Nova Rg", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Story_radioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Story_radioButton.Location = new System.Drawing.Point(1100, 5);
            this.Story_radioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Story_radioButton.Name = "Story_radioButton";
            this.Story_radioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Story_radioButton.Size = new System.Drawing.Size(72, 25);
            this.Story_radioButton.TabIndex = 27;
            this.Story_radioButton.TabStop = true;
            this.Story_radioButton.Text = "Story";
            this.Story_radioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Story_radioButton.UseVisualStyleBackColor = true;
            this.Story_radioButton.CheckedChanged += new System.EventHandler(this.Initial_radioButton_CheckedChanged);
            // 
            // Financial_radioButton
            // 
            this.Financial_radioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Financial_radioButton.AutoSize = true;
            this.Financial_radioButton.Font = new System.Drawing.Font("Proxima Nova Rg", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Financial_radioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Financial_radioButton.Location = new System.Drawing.Point(945, 5);
            this.Financial_radioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Financial_radioButton.Name = "Financial_radioButton";
            this.Financial_radioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Financial_radioButton.Size = new System.Drawing.Size(100, 25);
            this.Financial_radioButton.TabIndex = 24;
            this.Financial_radioButton.TabStop = true;
            this.Financial_radioButton.Text = "Financial";
            this.Financial_radioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Financial_radioButton.UseVisualStyleBackColor = true;
            this.Financial_radioButton.CheckedChanged += new System.EventHandler(this.Initial_radioButton_CheckedChanged);
            // 
            // Initial_radioButton
            // 
            this.Initial_radioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Initial_radioButton.AutoSize = true;
            this.Initial_radioButton.Font = new System.Drawing.Font("Proxima Nova Rg", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Initial_radioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Initial_radioButton.Location = new System.Drawing.Point(790, 5);
            this.Initial_radioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Initial_radioButton.Name = "Initial_radioButton";
            this.Initial_radioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Initial_radioButton.Size = new System.Drawing.Size(80, 25);
            this.Initial_radioButton.TabIndex = 26;
            this.Initial_radioButton.TabStop = true;
            this.Initial_radioButton.Text = "Initial";
            this.Initial_radioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Initial_radioButton.UseVisualStyleBackColor = true;
            this.Initial_radioButton.CheckedChanged += new System.EventHandler(this.Initial_radioButton_CheckedChanged);
            // 
            // Search_TxtBox
            // 
            this.Search_TxtBox.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.SetColumnSpan(this.Search_TxtBox, 2);
            this.Search_TxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Search_TxtBox.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.Search_TxtBox.Location = new System.Drawing.Point(125, 3);
            this.Search_TxtBox.Name = "Search_TxtBox";
            this.Search_TxtBox.Size = new System.Drawing.Size(405, 28);
            this.Search_TxtBox.TabIndex = 15;
            this.Search_TxtBox.TextChanged += new System.EventHandler(this.Search_TxtBox_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label5.Location = new System.Drawing.Point(45, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Search:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Black;
            this.panel7.Controls.Add(this.Counter_textBox);
            this.panel7.Controls.Add(this.tableLayoutPanel1);
            this.panel7.Controls.Add(this.MainBack_button);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 629);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1262, 44);
            this.panel7.TabIndex = 451;
            // 
            // Counter_textBox
            // 
            this.Counter_textBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Counter_textBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Counter_textBox.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.Counter_textBox.ForeColor = System.Drawing.Color.Black;
            this.Counter_textBox.Location = new System.Drawing.Point(1124, 8);
            this.Counter_textBox.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.Counter_textBox.Name = "Counter_textBox";
            this.Counter_textBox.ReadOnly = true;
            this.Counter_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Counter_textBox.Size = new System.Drawing.Size(129, 28);
            this.Counter_textBox.TabIndex = 410;
            this.Counter_textBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.AddVisit_button, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.UpdateVisit_button, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.DeleteVisit_button, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(471, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(336, 30);
            this.tableLayoutPanel1.TabIndex = 392;
            // 
            // AddVisit_button
            // 
            this.AddVisit_button.BackColor = System.Drawing.Color.Transparent;
            this.AddVisit_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.add0;
            this.AddVisit_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddVisit_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.AddVisit_button.FlatAppearance.BorderSize = 0;
            this.AddVisit_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.AddVisit_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.AddVisit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddVisit_button.ForeColor = System.Drawing.Color.White;
            this.AddVisit_button.Location = new System.Drawing.Point(3, 2);
            this.AddVisit_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddVisit_button.Name = "AddVisit_button";
            this.AddVisit_button.Size = new System.Drawing.Size(104, 24);
            this.AddVisit_button.TabIndex = 53;
            this.AddVisit_button.UseVisualStyleBackColor = false;
            this.AddVisit_button.Click += new System.EventHandler(this.AddVisit_button_Click);
            // 
            // UpdateVisit_button
            // 
            this.UpdateVisit_button.BackColor = System.Drawing.Color.Transparent;
            this.UpdateVisit_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.update0;
            this.UpdateVisit_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UpdateVisit_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.UpdateVisit_button.FlatAppearance.BorderSize = 0;
            this.UpdateVisit_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.UpdateVisit_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.UpdateVisit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateVisit_button.ForeColor = System.Drawing.Color.White;
            this.UpdateVisit_button.Location = new System.Drawing.Point(115, 2);
            this.UpdateVisit_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UpdateVisit_button.Name = "UpdateVisit_button";
            this.UpdateVisit_button.Size = new System.Drawing.Size(104, 24);
            this.UpdateVisit_button.TabIndex = 54;
            this.UpdateVisit_button.UseVisualStyleBackColor = false;
            this.UpdateVisit_button.Click += new System.EventHandler(this.UpdateVisit_button_Click);
            // 
            // DeleteVisit_button
            // 
            this.DeleteVisit_button.BackColor = System.Drawing.Color.Transparent;
            this.DeleteVisit_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.delete0;
            this.DeleteVisit_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DeleteVisit_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.DeleteVisit_button.FlatAppearance.BorderSize = 0;
            this.DeleteVisit_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.DeleteVisit_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.DeleteVisit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteVisit_button.ForeColor = System.Drawing.Color.White;
            this.DeleteVisit_button.Location = new System.Drawing.Point(227, 2);
            this.DeleteVisit_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteVisit_button.Name = "DeleteVisit_button";
            this.DeleteVisit_button.Size = new System.Drawing.Size(104, 24);
            this.DeleteVisit_button.TabIndex = 55;
            this.DeleteVisit_button.UseVisualStyleBackColor = false;
            this.DeleteVisit_button.Click += new System.EventHandler(this.DeleteVisit_button_Click);
            // 
            // MainBack_button
            // 
            this.MainBack_button.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MainBack_button.AutoSize = true;
            this.MainBack_button.BackColor = System.Drawing.Color.Transparent;
            this.MainBack_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MainBack_button.BackgroundImage")));
            this.MainBack_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MainBack_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.MainBack_button.FlatAppearance.BorderSize = 0;
            this.MainBack_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.MainBack_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.MainBack_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MainBack_button.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainBack_button.ForeColor = System.Drawing.Color.White;
            this.MainBack_button.Location = new System.Drawing.Point(12, 6);
            this.MainBack_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MainBack_button.Name = "MainBack_button";
            this.MainBack_button.Size = new System.Drawing.Size(30, 30);
            this.MainBack_button.TabIndex = 397;
            this.MainBack_button.UseVisualStyleBackColor = false;
            this.MainBack_button.Click += new System.EventHandler(this.MainBack_button_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.Project_label);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.ForeColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1262, 71);
            this.panel3.TabIndex = 450;
            // 
            // Project_label
            // 
            this.Project_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Project_label.Font = new System.Drawing.Font("Proxima Nova Rg", 26F, System.Drawing.FontStyle.Bold);
            this.Project_label.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Project_label.Location = new System.Drawing.Point(144, 0);
            this.Project_label.Name = "Project_label";
            this.Project_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Project_label.Size = new System.Drawing.Size(998, 71);
            this.Project_label.TabIndex = 1;
            this.Project_label.Text = "VISITS";
            this.Project_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(144, 71);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // AllVisits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.Visits_DataGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Proxima Nova Rg", 10.8F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AllVisits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AllVisits_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Visits_DataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Visits_DataGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Search_TxtBox;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button AddVisit_button;
        private System.Windows.Forms.Button UpdateVisit_button;
        private System.Windows.Forms.Button DeleteVisit_button;
        private System.Windows.Forms.Button MainBack_button;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label Project_label;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton Financial_radioButton;
        private System.Windows.Forms.RadioButton Initial_radioButton;
        private System.Windows.Forms.RadioButton Story_radioButton;
        private System.Windows.Forms.ComboBox Users_comboBox;
        private System.Windows.Forms.TextBox Counter_textBox;
    }
}