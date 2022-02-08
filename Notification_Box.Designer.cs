namespace MyWorkApplication
{
    partial class Notification_Box
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notification_Box));
            this.Project_label = new System.Windows.Forms.Label();
            this.Notification_dataGridView = new System.Windows.Forms.DataGridView();
            this.Close_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Counter_textBox = new System.Windows.Forms.TextBox();
            this.SeenCounter_textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Notification_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Project_label
            // 
            this.Project_label.BackColor = System.Drawing.Color.Transparent;
            this.Project_label.Dock = System.Windows.Forms.DockStyle.Top;
            this.Project_label.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Project_label.ForeColor = System.Drawing.Color.Black;
            this.Project_label.Location = new System.Drawing.Point(0, 0);
            this.Project_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Project_label.Name = "Project_label";
            this.Project_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Project_label.Size = new System.Drawing.Size(714, 62);
            this.Project_label.TabIndex = 453;
            this.Project_label.Text = "Notification";
            this.Project_label.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Project_label.Click += new System.EventHandler(this.Project_label_Click);
            this.Project_label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseDown);
            this.Project_label.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseMove);
            this.Project_label.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseUp);
            // 
            // Notification_dataGridView
            // 
            this.Notification_dataGridView.AllowUserToAddRows = false;
            this.Notification_dataGridView.AllowUserToDeleteRows = false;
            this.Notification_dataGridView.AllowUserToOrderColumns = true;
            this.Notification_dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Notification_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Notification_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.Notification_dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.Notification_dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Notification_dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.Notification_dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Janna LT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Notification_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Notification_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Notification_dataGridView.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Janna LT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Notification_dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.Notification_dataGridView.EnableHeadersVisualStyles = false;
            this.Notification_dataGridView.GridColor = System.Drawing.Color.Black;
            this.Notification_dataGridView.Location = new System.Drawing.Point(30, 110);
            this.Notification_dataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Notification_dataGridView.Name = "Notification_dataGridView";
            this.Notification_dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Janna LT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Notification_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Notification_dataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Janna LT", 10F);
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Notification_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Notification_dataGridView.RowTemplate.Height = 30;
            this.Notification_dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Notification_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Notification_dataGridView.Size = new System.Drawing.Size(654, 274);
            this.Notification_dataGridView.TabIndex = 455;
            this.Notification_dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Notification_dataGridView_CellContentClick);
            this.Notification_dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Notification_dataGridView_DataError);
            // 
            // Close_button
            // 
            this.Close_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_button.BackColor = System.Drawing.Color.Transparent;
            this.Close_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.Exit_D;
            this.Close_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Close_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Close_button.FlatAppearance.BorderSize = 0;
            this.Close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close_button.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close_button.Location = new System.Drawing.Point(687, 12);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(15, 15);
            this.Close_button.TabIndex = 456;
            this.Close_button.UseVisualStyleBackColor = false;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            this.Close_button.MouseEnter += new System.EventHandler(this.Close_button_MouseEnter);
            this.Close_button.MouseLeave += new System.EventHandler(this.Close_button_MouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label2.Location = new System.Drawing.Point(98, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 23);
            this.label2.TabIndex = 459;
            this.label2.Text = "All:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label1.Location = new System.Drawing.Point(423, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 23);
            this.label1.TabIndex = 460;
            this.label1.Text = "Seen:";
            // 
            // Counter_textBox
            // 
            this.Counter_textBox.BackColor = System.Drawing.Color.White;
            this.Counter_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Counter_textBox.Enabled = false;
            this.Counter_textBox.Font = new System.Drawing.Font("Janna LT", 9F);
            this.Counter_textBox.ForeColor = System.Drawing.Color.Black;
            this.Counter_textBox.Location = new System.Drawing.Point(140, 70);
            this.Counter_textBox.Name = "Counter_textBox";
            this.Counter_textBox.Size = new System.Drawing.Size(100, 28);
            this.Counter_textBox.TabIndex = 461;
            this.Counter_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SeenCounter_textBox
            // 
            this.SeenCounter_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SeenCounter_textBox.BackColor = System.Drawing.Color.White;
            this.SeenCounter_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SeenCounter_textBox.Enabled = false;
            this.SeenCounter_textBox.Font = new System.Drawing.Font("Janna LT", 9F);
            this.SeenCounter_textBox.ForeColor = System.Drawing.Color.Black;
            this.SeenCounter_textBox.Location = new System.Drawing.Point(484, 70);
            this.SeenCounter_textBox.Name = "SeenCounter_textBox";
            this.SeenCounter_textBox.Size = new System.Drawing.Size(100, 28);
            this.SeenCounter_textBox.TabIndex = 462;
            this.SeenCounter_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Notification_Box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.BackgroundImage = global::MyWorkApplication.Properties.Resources.Back_600;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(714, 414);
            this.ControlBox = false;
            this.Controls.Add(this.SeenCounter_textBox);
            this.Controls.Add(this.Counter_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Notification_dataGridView);
            this.Controls.Add(this.Project_label);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Janna LT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Notification_Box";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Notification_Box_FormClosing);
            this.Load += new System.EventHandler(this.Notification_Box_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.Notification_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Project_label;
        private System.Windows.Forms.DataGridView Notification_dataGridView;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Counter_textBox;
        private System.Windows.Forms.TextBox SeenCounter_textBox;
    }
}