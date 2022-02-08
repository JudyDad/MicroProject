namespace MyWorkApplication
{
    partial class Login_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Form));
            this.Connect_button = new System.Windows.Forms.Button();
            this.Remember_checkBox = new System.Windows.Forms.CheckBox();
            this.Close_button = new System.Windows.Forms.Button();
            this.Online_checkBox = new System.Windows.Forms.CheckBox();
            this.UserName_textBox = new System.Windows.Forms.TextBox();
            this.Password_textBox = new System.Windows.Forms.TextBox();
            this.ProfilePicture_pictureBox = new OvalPictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Version_label = new System.Windows.Forms.Label();
            this.ShowPassword_button = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicture_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Connect_button
            // 
            this.Connect_button.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Connect_button.BackColor = System.Drawing.Color.Transparent;
            this.Connect_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.login_D;
            this.Connect_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Connect_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Connect_button.FlatAppearance.BorderSize = 0;
            this.Connect_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.Connect_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Connect_button.Font = new System.Drawing.Font("Proxima Nova Lt", 8F);
            this.Connect_button.ForeColor = System.Drawing.Color.Black;
            this.Connect_button.Location = new System.Drawing.Point(130, 509);
            this.Connect_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Connect_button.Name = "Connect_button";
            this.Connect_button.Size = new System.Drawing.Size(174, 37);
            this.Connect_button.TabIndex = 3;
            this.Connect_button.UseVisualStyleBackColor = false;
            this.Connect_button.Click += new System.EventHandler(this.Connect_button_Click);
            this.Connect_button.MouseEnter += new System.EventHandler(this.Connect_button_MouseEnter);
            this.Connect_button.MouseLeave += new System.EventHandler(this.Connect_button_MouseLeave);
            // 
            // Remember_checkBox
            // 
            this.Remember_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Remember_checkBox.AutoSize = true;
            this.Remember_checkBox.BackColor = System.Drawing.Color.Transparent;
            this.Remember_checkBox.Font = new System.Drawing.Font("Proxima Nova Rg", 10F);
            this.Remember_checkBox.ForeColor = System.Drawing.Color.Black;
            this.Remember_checkBox.Location = new System.Drawing.Point(119, 556);
            this.Remember_checkBox.Name = "Remember_checkBox";
            this.Remember_checkBox.Size = new System.Drawing.Size(117, 18);
            this.Remember_checkBox.TabIndex = 4;
            this.Remember_checkBox.Text = "Remember Me";
            this.Remember_checkBox.UseVisualStyleBackColor = false;
            // 
            // Close_button
            // 
            this.Close_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_button.BackColor = System.Drawing.Color.Transparent;
            this.Close_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.Exit_D;
            this.Close_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Close_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Close_button.FlatAppearance.BorderSize = 0;
            this.Close_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.Close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close_button.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close_button.Location = new System.Drawing.Point(355, 9);
            this.Close_button.Margin = new System.Windows.Forms.Padding(0);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(15, 15);
            this.Close_button.TabIndex = 10;
            this.Close_button.UseVisualStyleBackColor = false;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            this.Close_button.MouseEnter += new System.EventHandler(this.Close_button_MouseEnter);
            this.Close_button.MouseLeave += new System.EventHandler(this.Close_button_MouseLeave);
            // 
            // Online_checkBox
            // 
            this.Online_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Online_checkBox.AutoSize = true;
            this.Online_checkBox.BackColor = System.Drawing.Color.Transparent;
            this.Online_checkBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.Online_checkBox.Font = new System.Drawing.Font("Proxima Nova Rg", 10F);
            this.Online_checkBox.ForeColor = System.Drawing.Color.Black;
            this.Online_checkBox.Location = new System.Drawing.Point(119, 583);
            this.Online_checkBox.Name = "Online_checkBox";
            this.Online_checkBox.Size = new System.Drawing.Size(67, 18);
            this.Online_checkBox.TabIndex = 5;
            this.Online_checkBox.Text = "Online";
            this.Online_checkBox.UseVisualStyleBackColor = false;
            // 
            // UserName_textBox
            // 
            this.UserName_textBox.BackColor = System.Drawing.Color.White;
            this.UserName_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserName_textBox.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserName_textBox.ForeColor = System.Drawing.Color.Black;
            this.UserName_textBox.Location = new System.Drawing.Point(118, 400);
            this.UserName_textBox.Name = "UserName_textBox";
            this.UserName_textBox.Size = new System.Drawing.Size(198, 30);
            this.UserName_textBox.TabIndex = 1;
            this.UserName_textBox.Text = "username";
            // 
            // Password_textBox
            // 
            this.Password_textBox.BackColor = System.Drawing.Color.White;
            this.Password_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Password_textBox.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 13.8F, System.Drawing.FontStyle.Bold);
            this.Password_textBox.ForeColor = System.Drawing.Color.Black;
            this.Password_textBox.Location = new System.Drawing.Point(118, 464);
            this.Password_textBox.Name = "Password_textBox";
            this.Password_textBox.PasswordChar = '*';
            this.Password_textBox.Size = new System.Drawing.Size(198, 30);
            this.Password_textBox.TabIndex = 2;
            this.Password_textBox.Text = "password";
            this.Password_textBox.WordWrap = false;
            // 
            // ProfilePicture_pictureBox
            // 
            this.ProfilePicture_pictureBox.BackColor = System.Drawing.Color.White;
            this.ProfilePicture_pictureBox.BackgroundImage = global::MyWorkApplication.Properties.Resources.Unknown_User;
            this.ProfilePicture_pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ProfilePicture_pictureBox.Location = new System.Drawing.Point(134, 209);
            this.ProfilePicture_pictureBox.MaximumSize = new System.Drawing.Size(158, 158);
            this.ProfilePicture_pictureBox.MinimumSize = new System.Drawing.Size(158, 158);
            this.ProfilePicture_pictureBox.Name = "ProfilePicture_pictureBox";
            this.ProfilePicture_pictureBox.Size = new System.Drawing.Size(158, 158);
            this.ProfilePicture_pictureBox.TabIndex = 425;
            this.ProfilePicture_pictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Proxima Nova Rg", 11F);
            this.label2.Location = new System.Drawing.Point(118, 442);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 436;
            this.label2.Text = "PASSWORD:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Proxima Nova Rg", 11F);
            this.label1.Location = new System.Drawing.Point(118, 378);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 435;
            this.label1.Text = "USERNAME:";
            // 
            // Version_label
            // 
            this.Version_label.BackColor = System.Drawing.Color.Transparent;
            this.Version_label.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Version_label.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version_label.Location = new System.Drawing.Point(0, 621);
            this.Version_label.Name = "Version_label";
            this.Version_label.Size = new System.Drawing.Size(379, 19);
            this.Version_label.TabIndex = 437;
            this.Version_label.Text = "Micro Projects 2018 - 2022  |  V";
            this.Version_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShowPassword_button
            // 
            this.ShowPassword_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ShowPassword_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.show_password_false;
            this.ShowPassword_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ShowPassword_button.FlatAppearance.BorderSize = 0;
            this.ShowPassword_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ShowPassword_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ShowPassword_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowPassword_button.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.ShowPassword_button.Location = new System.Drawing.Point(291, 469);
            this.ShowPassword_button.MaximumSize = new System.Drawing.Size(20, 20);
            this.ShowPassword_button.MinimumSize = new System.Drawing.Size(20, 20);
            this.ShowPassword_button.Name = "ShowPassword_button";
            this.ShowPassword_button.Size = new System.Drawing.Size(20, 20);
            this.ShowPassword_button.TabIndex = 438;
            this.ShowPassword_button.UseVisualStyleBackColor = true;
            this.ShowPassword_button.Click += new System.EventHandler(this.ShowPassword_button_Click);
            // 
            // Login_Form
            // 
            this.AcceptButton = this.Connect_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::MyWorkApplication.Properties.Resources.Login_13_04_2021;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(379, 640);
            this.Controls.Add(this.ShowPassword_button);
            this.Controls.Add(this.Version_label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProfilePicture_pictureBox);
            this.Controls.Add(this.Online_checkBox);
            this.Controls.Add(this.Remember_checkBox);
            this.Controls.Add(this.Connect_button);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Password_textBox);
            this.Controls.Add(this.UserName_textBox);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Avenir LT Std 35 Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Login_Form";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.Login_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicture_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Connect_button;
        private System.Windows.Forms.CheckBox Remember_checkBox;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.CheckBox Online_checkBox;
        private System.Windows.Forms.TextBox UserName_textBox;
        private OvalPictureBox ProfilePicture_pictureBox;
        private System.Windows.Forms.TextBox Password_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Version_label;
        private System.Windows.Forms.Button ShowPassword_button;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}