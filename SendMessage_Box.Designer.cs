namespace MyWorkApplication
{
    partial class SendMessage_Box
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendMessage_Box));
            this.Close_button = new System.Windows.Forms.Button();
            this.Project_label = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.date_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.Save_button = new System.Windows.Forms.Button();
            this.MicroProject_ID_textBox = new System.Windows.Forms.TextBox();
            this.mp_label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Body_richTextBox = new System.Windows.Forms.RichTextBox();
            this.P_Name_richTextBox = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Users_comboBox = new System.Windows.Forms.ComboBox();
            this.Date_dateTimePicker = new MyWorkApplication.Classes.BCDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel18.SuspendLayout();
            this.SuspendLayout();
            // 
            // Close_button
            // 
            this.Close_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_button.BackColor = System.Drawing.Color.Transparent;
            this.Close_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Close_button.BackgroundImage")));
            this.Close_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Close_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Close_button.FlatAppearance.BorderSize = 0;
            this.Close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close_button.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close_button.Location = new System.Drawing.Point(651, 12);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(15, 15);
            this.Close_button.TabIndex = 458;
            this.Close_button.UseVisualStyleBackColor = false;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // Project_label
            // 
            this.Project_label.BackColor = System.Drawing.Color.Transparent;
            this.Project_label.Dock = System.Windows.Forms.DockStyle.Top;
            this.Project_label.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Project_label.ForeColor = System.Drawing.Color.Black;
            this.Project_label.Location = new System.Drawing.Point(0, 0);
            this.Project_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Project_label.Name = "Project_label";
            this.Project_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Project_label.Size = new System.Drawing.Size(678, 54);
            this.Project_label.TabIndex = 457;
            this.Project_label.Text = "New Message";
            this.Project_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Project_label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseDown);
            this.Project_label.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseMove);
            this.Project_label.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseUp);
            // 
            // label84
            // 
            this.label84.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label84.Location = new System.Drawing.Point(152, 186);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(59, 23);
            this.label84.TabIndex = 540;
            this.label84.Text = "Body:";
            this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // date_label
            // 
            this.date_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.date_label.AutoSize = true;
            this.date_label.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.date_label.Location = new System.Drawing.Point(57, 247);
            this.date_label.Name = "date_label";
            this.date_label.Size = new System.Drawing.Size(154, 23);
            this.date_label.TabIndex = 541;
            this.date_label.Text = "Scheduled Date:";
            this.date_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel18.ColumnCount = 4;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.152875F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.48378F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.32743F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.96165F));
            this.tableLayoutPanel18.Controls.Add(this.Save_button, 2, 6);
            this.tableLayoutPanel18.Controls.Add(this.MicroProject_ID_textBox, 2, 5);
            this.tableLayoutPanel18.Controls.Add(this.mp_label, 1, 5);
            this.tableLayoutPanel18.Controls.Add(this.label3, 1, 2);
            this.tableLayoutPanel18.Controls.Add(this.label84, 1, 3);
            this.tableLayoutPanel18.Controls.Add(this.Body_richTextBox, 2, 3);
            this.tableLayoutPanel18.Controls.Add(this.P_Name_richTextBox, 2, 2);
            this.tableLayoutPanel18.Controls.Add(this.label5, 1, 1);
            this.tableLayoutPanel18.Controls.Add(this.Users_comboBox, 2, 1);
            this.tableLayoutPanel18.Controls.Add(this.date_label, 1, 4);
            this.tableLayoutPanel18.Controls.Add(this.Date_dateTimePicker, 2, 4);
            this.tableLayoutPanel18.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(0, 54);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 8;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.404076F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.18676F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.22241F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.22241F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.18676F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.18676F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.18676F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.404076F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(678, 394);
            this.tableLayoutPanel18.TabIndex = 542;
            this.tableLayoutPanel18.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseDown);
            this.tableLayoutPanel18.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseMove);
            this.tableLayoutPanel18.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Notification_Box_MouseUp);
            // 
            // Save_button
            // 
            this.Save_button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Save_button.BackColor = System.Drawing.Color.Red;
            this.Save_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Save_button.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.Save_button.FlatAppearance.BorderSize = 0;
            this.Save_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.Save_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.Save_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.Save_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_button.Font = new System.Drawing.Font("Avenir LT Std 35 Light", 14F);
            this.Save_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Save_button.Location = new System.Drawing.Point(217, 321);
            this.Save_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Save_button.Name = "Save_button";
            this.Save_button.Size = new System.Drawing.Size(342, 36);
            this.Save_button.TabIndex = 6;
            this.Save_button.Text = "Send Message";
            this.Save_button.UseVisualStyleBackColor = false;
            this.Save_button.Click += new System.EventHandler(this.Save_button_Click);
            // 
            // MicroProject_ID_textBox
            // 
            this.MicroProject_ID_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MicroProject_ID_textBox.BackColor = System.Drawing.Color.White;
            this.MicroProject_ID_textBox.Font = new System.Drawing.Font("Janna LT", 10F);
            this.MicroProject_ID_textBox.Location = new System.Drawing.Point(217, 282);
            this.MicroProject_ID_textBox.Name = "MicroProject_ID_textBox";
            this.MicroProject_ID_textBox.Size = new System.Drawing.Size(342, 38);
            this.MicroProject_ID_textBox.TabIndex = 5;
            this.MicroProject_ID_textBox.Visible = false;
            // 
            // mp_label
            // 
            this.mp_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.mp_label.AutoSize = true;
            this.mp_label.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mp_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.mp_label.Location = new System.Drawing.Point(113, 287);
            this.mp_label.Name = "mp_label";
            this.mp_label.Size = new System.Drawing.Size(98, 23);
            this.mp_label.TabIndex = 545;
            this.mp_label.Text = "Project ID:";
            this.mp_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mp_label.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label3.Location = new System.Drawing.Point(158, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 23);
            this.label3.TabIndex = 544;
            this.label3.Text = "Title:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Body_richTextBox
            // 
            this.Body_richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Body_richTextBox.Location = new System.Drawing.Point(217, 159);
            this.Body_richTextBox.Name = "Body_richTextBox";
            this.Body_richTextBox.Size = new System.Drawing.Size(342, 77);
            this.Body_richTextBox.TabIndex = 3;
            this.Body_richTextBox.Text = "";
            // 
            // P_Name_richTextBox
            // 
            this.P_Name_richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.P_Name_richTextBox.Location = new System.Drawing.Point(217, 76);
            this.P_Name_richTextBox.Name = "P_Name_richTextBox";
            this.P_Name_richTextBox.Size = new System.Drawing.Size(342, 77);
            this.P_Name_richTextBox.TabIndex = 2;
            this.P_Name_richTextBox.Text = "";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label5.Location = new System.Drawing.Point(176, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 23);
            this.label5.TabIndex = 112;
            this.label5.Text = "To:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Users_comboBox
            // 
            this.Users_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Users_comboBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Users_comboBox.FormattingEnabled = true;
            this.Users_comboBox.Location = new System.Drawing.Point(217, 37);
            this.Users_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Users_comboBox.Name = "Users_comboBox";
            this.Users_comboBox.Size = new System.Drawing.Size(342, 39);
            this.Users_comboBox.TabIndex = 1;
            // 
            // Date_dateTimePicker
            // 
            this.Date_dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Date_dateTimePicker.BackColor = System.Drawing.Color.Black;
            this.Date_dateTimePicker.CalendarForeColor = System.Drawing.Color.Black;
            this.Date_dateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.Date_dateTimePicker.Font = new System.Drawing.Font("Janna LT", 11F);
            this.Date_dateTimePicker.ForeColor = System.Drawing.Color.Black;
            this.Date_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_dateTimePicker.Location = new System.Drawing.Point(217, 242);
            this.Date_dateTimePicker.Name = "Date_dateTimePicker";
            this.Date_dateTimePicker.RightToLeftLayout = true;
            this.Date_dateTimePicker.Size = new System.Drawing.Size(342, 42);
            this.Date_dateTimePicker.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.tableLayoutPanel18.SetColumnSpan(this.label2, 4);
            this.label2.Font = new System.Drawing.Font("Avenir LT Std 35 Light", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(672, 20);
            this.label2.TabIndex = 546;
            this.label2.Text = "Note: if you leave message receiver box empty .. your message will be send to all" +
    " users";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SendMessage_Box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MyWorkApplication.Properties.Resources.Back_700;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(678, 448);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel18);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Project_label);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Janna LT", 11.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.Name = "SendMessage_Box";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SendMessage_Box_Load);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.Label Project_label;
        private Classes.BCDateTimePicker Date_dateTimePicker;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label date_label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label mp_label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox MicroProject_ID_textBox;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.ComboBox Users_comboBox;
        private System.Windows.Forms.RichTextBox Body_richTextBox;
        private System.Windows.Forms.RichTextBox P_Name_richTextBox;
        private System.Windows.Forms.Label label2;
    }
}