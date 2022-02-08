namespace MyWorkApplication
{
    partial class Payments_Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Sum_label = new System.Windows.Forms.Label();
            this.ExportToExcel_button = new System.Windows.Forms.Button();
            this.Count_label = new System.Windows.Forms.Label();
            this.Selected_label = new System.Windows.Forms.Label();
            this.Top_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.SearchBy_textBox = new System.Windows.Forms.TextBox();
            this.ExactSearch_checkBox = new System.Windows.Forms.CheckBox();
            this.Refresh_button = new System.Windows.Forms.Button();
            this.Group_comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Paid_button = new System.Windows.Forms.Button();
            this.Pending_button = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Year_checkBox = new System.Windows.Forms.CheckBox();
            this.Year_comboBox = new System.Windows.Forms.ComboBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.Top_tableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Janna LT", 11.25F);
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Janna LT", 11.25F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.Gray;
            this.dataGridView.Location = new System.Drawing.Point(105, 95);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Janna LT", 11.25F);
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView.RowTemplate.Height = 30;
            this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(985, 541);
            this.dataGridView.TabIndex = 504;
            this.dataGridView.VirtualMode = true;
            this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            this.dataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_RowHeaderMouseDoubleClick);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.02041F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel2.Controls.Add(this.Sum_label, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.ExportToExcel_button, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Count_label, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Selected_label, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Janna LT", 10.8F);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(105, 636);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(985, 37);
            this.tableLayoutPanel2.TabIndex = 505;
            // 
            // Sum_label
            // 
            this.Sum_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Sum_label.AutoSize = true;
            this.Sum_label.BackColor = System.Drawing.Color.Transparent;
            this.Sum_label.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.Sum_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Sum_label.Location = new System.Drawing.Point(91, 8);
            this.Sum_label.Margin = new System.Windows.Forms.Padding(0);
            this.Sum_label.Name = "Sum_label";
            this.Sum_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Sum_label.Size = new System.Drawing.Size(139, 21);
            this.Sum_label.TabIndex = 481;
            this.Sum_label.Text = "Sum";
            this.Sum_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ExportToExcel_button
            // 
            this.ExportToExcel_button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExportToExcel_button.AutoSize = true;
            this.ExportToExcel_button.BackColor = System.Drawing.Color.Transparent;
            this.ExportToExcel_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.Excel_L;
            this.ExportToExcel_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ExportToExcel_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ExportToExcel_button.FlatAppearance.BorderSize = 0;
            this.ExportToExcel_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ExportToExcel_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ExportToExcel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportToExcel_button.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportToExcel_button.ForeColor = System.Drawing.Color.White;
            this.ExportToExcel_button.Location = new System.Drawing.Point(949, 3);
            this.ExportToExcel_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ExportToExcel_button.MaximumSize = new System.Drawing.Size(30, 30);
            this.ExportToExcel_button.MinimumSize = new System.Drawing.Size(30, 30);
            this.ExportToExcel_button.Name = "ExportToExcel_button";
            this.ExportToExcel_button.Size = new System.Drawing.Size(30, 30);
            this.ExportToExcel_button.TabIndex = 411;
            this.ExportToExcel_button.UseVisualStyleBackColor = false;
            this.ExportToExcel_button.Click += new System.EventHandler(this.ExportToExcel_button_Click);
            // 
            // Count_label
            // 
            this.Count_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Count_label.AutoSize = true;
            this.Count_label.BackColor = System.Drawing.Color.Transparent;
            this.Count_label.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Count_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Count_label.Location = new System.Drawing.Point(806, 8);
            this.Count_label.Margin = new System.Windows.Forms.Padding(0);
            this.Count_label.Name = "Count_label";
            this.Count_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Count_label.Size = new System.Drawing.Size(139, 21);
            this.Count_label.TabIndex = 475;
            this.Count_label.Text = "Count";
            this.Count_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Selected_label
            // 
            this.Selected_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Selected_label.AutoSize = true;
            this.Selected_label.BackColor = System.Drawing.Color.Transparent;
            this.Selected_label.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Selected_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Selected_label.Location = new System.Drawing.Point(667, 8);
            this.Selected_label.Margin = new System.Windows.Forms.Padding(0);
            this.Selected_label.Name = "Selected_label";
            this.Selected_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Selected_label.Size = new System.Drawing.Size(139, 21);
            this.Selected_label.TabIndex = 476;
            this.Selected_label.Text = "Selected";
            this.Selected_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Top_tableLayoutPanel
            // 
            this.Top_tableLayoutPanel.ColumnCount = 9;
            this.Top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.675121F));
            this.Top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.94732F));
            this.Top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.13574F));
            this.Top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.Top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.Top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.67207F));
            this.Top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.94732F));
            this.Top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.94732F));
            this.Top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.675121F));
            this.Top_tableLayoutPanel.Controls.Add(this.label16, 1, 0);
            this.Top_tableLayoutPanel.Controls.Add(this.SearchBy_textBox, 2, 0);
            this.Top_tableLayoutPanel.Controls.Add(this.ExactSearch_checkBox, 5, 0);
            this.Top_tableLayoutPanel.Controls.Add(this.Refresh_button, 0, 0);
            this.Top_tableLayoutPanel.Controls.Add(this.Group_comboBox, 2, 1);
            this.Top_tableLayoutPanel.Controls.Add(this.label2, 1, 1);
            this.Top_tableLayoutPanel.Controls.Add(this.Paid_button, 4, 1);
            this.Top_tableLayoutPanel.Controls.Add(this.Pending_button, 3, 1);
            this.Top_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Top_tableLayoutPanel.Location = new System.Drawing.Point(105, 0);
            this.Top_tableLayoutPanel.Name = "Top_tableLayoutPanel";
            this.Top_tableLayoutPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Top_tableLayoutPanel.RowCount = 2;
            this.Top_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.52632F));
            this.Top_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.47368F));
            this.Top_tableLayoutPanel.Size = new System.Drawing.Size(985, 95);
            this.Top_tableLayoutPanel.TabIndex = 506;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label16.Location = new System.Drawing.Point(827, 8);
            this.label16.Margin = new System.Windows.Forms.Padding(7);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 31);
            this.label16.TabIndex = 470;
            this.label16.Text = "بحث:";
            // 
            // SearchBy_textBox
            // 
            this.SearchBy_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBy_textBox.BackColor = System.Drawing.Color.White;
            this.Top_tableLayoutPanel.SetColumnSpan(this.SearchBy_textBox, 3);
            this.SearchBy_textBox.Font = new System.Drawing.Font("Janna LT", 9.5F);
            this.SearchBy_textBox.Location = new System.Drawing.Point(446, 5);
            this.SearchBy_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchBy_textBox.Name = "SearchBy_textBox";
            this.SearchBy_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SearchBy_textBox.Size = new System.Drawing.Size(371, 37);
            this.SearchBy_textBox.TabIndex = 440;
            this.SearchBy_textBox.TextChanged += new System.EventHandler(this.SearchBy_textBox_TextChanged);
            // 
            // ExactSearch_checkBox
            // 
            this.ExactSearch_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ExactSearch_checkBox.AutoSize = true;
            this.ExactSearch_checkBox.Font = new System.Drawing.Font("Janna LT", 10F);
            this.ExactSearch_checkBox.Location = new System.Drawing.Point(330, 6);
            this.ExactSearch_checkBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ExactSearch_checkBox.Name = "ExactSearch_checkBox";
            this.ExactSearch_checkBox.Size = new System.Drawing.Size(110, 35);
            this.ExactSearch_checkBox.TabIndex = 442;
            this.ExactSearch_checkBox.Text = "تطابق كلي";
            this.ExactSearch_checkBox.UseVisualStyleBackColor = true;
            this.ExactSearch_checkBox.CheckedChanged += new System.EventHandler(this.ExactSearch_checkBox_CheckedChanged);
            // 
            // Refresh_button
            // 
            this.Refresh_button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Refresh_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.Refresh2_D;
            this.Refresh_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Refresh_button.FlatAppearance.BorderSize = 0;
            this.Refresh_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Refresh_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Refresh_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Refresh_button.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.Refresh_button.Location = new System.Drawing.Point(953, 9);
            this.Refresh_button.MaximumSize = new System.Drawing.Size(30, 30);
            this.Refresh_button.MinimumSize = new System.Drawing.Size(30, 30);
            this.Refresh_button.Name = "Refresh_button";
            this.Refresh_button.Size = new System.Drawing.Size(30, 30);
            this.Refresh_button.TabIndex = 469;
            this.Refresh_button.UseVisualStyleBackColor = true;
            this.Refresh_button.Click += new System.EventHandler(this.Refresh_button_Click);
            // 
            // Group_comboBox
            // 
            this.Group_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Group_comboBox.BackColor = System.Drawing.Color.White;
            this.Group_comboBox.Font = new System.Drawing.Font("Janna LT", 9F);
            this.Group_comboBox.FormattingEnabled = true;
            this.Group_comboBox.Location = new System.Drawing.Point(661, 53);
            this.Group_comboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Group_comboBox.Name = "Group_comboBox";
            this.Group_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Group_comboBox.Size = new System.Drawing.Size(156, 37);
            this.Group_comboBox.TabIndex = 471;
            this.Group_comboBox.SelectedIndexChanged += new System.EventHandler(this.Group_comboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label2.Location = new System.Drawing.Point(827, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 31);
            this.label2.TabIndex = 472;
            this.label2.Text = "المجموعة:";
            // 
            // Paid_button
            // 
            this.Paid_button.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Paid_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.Paid_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.Paid_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Paid_button.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.Paid_button.Location = new System.Drawing.Point(446, 52);
            this.Paid_button.MaximumSize = new System.Drawing.Size(100, 38);
            this.Paid_button.MinimumSize = new System.Drawing.Size(100, 38);
            this.Paid_button.Name = "Paid_button";
            this.Paid_button.Size = new System.Drawing.Size(100, 38);
            this.Paid_button.TabIndex = 473;
            this.Paid_button.Text = "Paid";
            this.Paid_button.UseVisualStyleBackColor = true;
            this.Paid_button.Click += new System.EventHandler(this.Paid_button_Click);
            // 
            // Pending_button
            // 
            this.Pending_button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Pending_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.Pending_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.Pending_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pending_button.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.Pending_button.Location = new System.Drawing.Point(551, 52);
            this.Pending_button.MaximumSize = new System.Drawing.Size(100, 38);
            this.Pending_button.MinimumSize = new System.Drawing.Size(100, 38);
            this.Pending_button.Name = "Pending_button";
            this.Pending_button.Size = new System.Drawing.Size(100, 38);
            this.Pending_button.TabIndex = 474;
            this.Pending_button.Text = "Pending";
            this.Pending_button.UseVisualStyleBackColor = true;
            this.Pending_button.Click += new System.EventHandler(this.Pending_button_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1090, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(12, 673);
            this.panel3.TabIndex = 509;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.button12);
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1102, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 673);
            this.panel1.TabIndex = 507;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Controls.Add(this.Year_checkBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Year_comboBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 526);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(160, 49);
            this.tableLayoutPanel1.TabIndex = 505;
            // 
            // Year_checkBox
            // 
            this.Year_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Year_checkBox.AutoSize = true;
            this.Year_checkBox.Checked = true;
            this.Year_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Year_checkBox.Font = new System.Drawing.Font("Janna LT", 10F);
            this.Year_checkBox.Location = new System.Drawing.Point(135, 15);
            this.Year_checkBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Year_checkBox.Name = "Year_checkBox";
            this.Year_checkBox.Padding = new System.Windows.Forms.Padding(1);
            this.Year_checkBox.Size = new System.Drawing.Size(20, 19);
            this.Year_checkBox.TabIndex = 443;
            this.Year_checkBox.UseVisualStyleBackColor = true;
            this.Year_checkBox.CheckedChanged += new System.EventHandler(this.Year_checkBox_CheckedChanged);
            // 
            // Year_comboBox
            // 
            this.Year_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Year_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Year_comboBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Year_comboBox.ForeColor = System.Drawing.Color.Black;
            this.Year_comboBox.FormatString = "####";
            this.Year_comboBox.FormattingEnabled = true;
            this.Year_comboBox.Location = new System.Drawing.Point(3, 4);
            this.Year_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Year_comboBox.Name = "Year_comboBox";
            this.Year_comboBox.Size = new System.Drawing.Size(126, 39);
            this.Year_comboBox.TabIndex = 15;
            this.Year_comboBox.SelectedIndexChanged += new System.EventHandler(this.Year_comboBox_SelectedIndexChanged);
            // 
            // button12
            // 
            this.button12.Dock = System.Windows.Forms.DockStyle.Top;
            this.button12.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button12.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button12.Location = new System.Drawing.Point(0, 486);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(160, 40);
            this.button12.TabIndex = 11;
            this.button12.Text = "12-December";
            this.button12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.Dock = System.Windows.Forms.DockStyle.Top;
            this.button11.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button11.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button11.Location = new System.Drawing.Point(0, 446);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(160, 40);
            this.button11.TabIndex = 10;
            this.button11.Text = "11-November";
            this.button11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button10
            // 
            this.button10.Dock = System.Windows.Forms.DockStyle.Top;
            this.button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button10.Location = new System.Drawing.Point(0, 406);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(160, 40);
            this.button10.TabIndex = 9;
            this.button10.Text = "10-October";
            this.button10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Dock = System.Windows.Forms.DockStyle.Top;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button9.Location = new System.Drawing.Point(0, 366);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(160, 40);
            this.button9.TabIndex = 8;
            this.button9.Text = "9-September";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Top;
            this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button8.Location = new System.Drawing.Point(0, 326);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(160, 40);
            this.button8.TabIndex = 7;
            this.button8.Text = "8-August";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Top;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button7.Location = new System.Drawing.Point(0, 286);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(160, 40);
            this.button7.TabIndex = 6;
            this.button7.Text = "7-July";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Dock = System.Windows.Forms.DockStyle.Top;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button6.Location = new System.Drawing.Point(0, 246);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(160, 40);
            this.button6.TabIndex = 5;
            this.button6.Text = "6-June";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Top;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button5.Location = new System.Drawing.Point(0, 206);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(160, 40);
            this.button5.TabIndex = 4;
            this.button5.Text = "5-May";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Top;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button4.Location = new System.Drawing.Point(0, 166);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(160, 40);
            this.button4.TabIndex = 3;
            this.button4.Text = "4-April";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(0, 126);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(160, 40);
            this.button3.TabIndex = 2;
            this.button3.Text = "3-March";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(0, 86);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "2-February";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(0, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "1-January";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(7);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(160, 46);
            this.label1.TabIndex = 471;
            this.label1.Text = "بحث حسب الأشهر:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(105, 673);
            this.panel2.TabIndex = 508;
            // 
            // Payments_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.Top_tableLayoutPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Janna LT", 11.25F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.Name = "Payments_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payments_Form";
            this.Load += new System.EventHandler(this.Payments_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.Top_tableLayoutPanel.ResumeLayout(false);
            this.Top_tableLayoutPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button ExportToExcel_button;
        private System.Windows.Forms.Label Count_label;
        private System.Windows.Forms.Label Selected_label;
        private System.Windows.Forms.TableLayoutPanel Top_tableLayoutPanel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox SearchBy_textBox;
        private System.Windows.Forms.CheckBox ExactSearch_checkBox;
        private System.Windows.Forms.Button Refresh_button;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox Year_comboBox;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox Group_comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Paid_button;
        private System.Windows.Forms.Button Pending_button;
        private System.Windows.Forms.Label Sum_label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox Year_checkBox;
    }
}