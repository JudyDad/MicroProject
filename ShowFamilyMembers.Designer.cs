namespace MyWorkApplication
{
    partial class ShowFamilyMembers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowFamilyMembers));
            this.PersonDataGridView = new System.Windows.Forms.DataGridView();
            this.Project_label = new System.Windows.Forms.Label();
            this.Close_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PersonDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // PersonDataGridView
            // 
            this.PersonDataGridView.AllowUserToAddRows = false;
            this.PersonDataGridView.AllowUserToDeleteRows = false;
            this.PersonDataGridView.AllowUserToOrderColumns = true;
            this.PersonDataGridView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PersonDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PersonDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.PersonDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PersonDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Janna LT", 10F);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PersonDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PersonDataGridView.ColumnHeadersHeight = 130;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Janna LT", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(216)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PersonDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.PersonDataGridView.EnableHeadersVisualStyles = false;
            this.PersonDataGridView.GridColor = System.Drawing.Color.DarkGray;
            this.PersonDataGridView.Location = new System.Drawing.Point(30, 91);
            this.PersonDataGridView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.PersonDataGridView.Name = "PersonDataGridView";
            this.PersonDataGridView.ReadOnly = true;
            this.PersonDataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Janna LT", 11.25F);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PersonDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.PersonDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PersonDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.PersonDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PersonDataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PersonDataGridView.RowTemplate.Height = 26;
            this.PersonDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PersonDataGridView.Size = new System.Drawing.Size(739, 278);
            this.PersonDataGridView.TabIndex = 393;
            // 
            // Project_label
            // 
            this.Project_label.BackColor = System.Drawing.Color.Transparent;
            this.Project_label.Dock = System.Windows.Forms.DockStyle.Top;
            this.Project_label.Font = new System.Drawing.Font("Janna LT", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Project_label.ForeColor = System.Drawing.Color.Black;
            this.Project_label.Location = new System.Drawing.Point(0, 0);
            this.Project_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Project_label.Name = "Project_label";
            this.Project_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Project_label.Size = new System.Drawing.Size(800, 76);
            this.Project_label.TabIndex = 454;
            this.Project_label.Text = "تفاصيل العائلة";
            this.Project_label.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
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
            this.Close_button.Location = new System.Drawing.Point(773, 12);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(15, 15);
            this.Close_button.TabIndex = 457;
            this.Close_button.UseVisualStyleBackColor = false;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            this.Close_button.MouseEnter += new System.EventHandler(this.Close_button_MouseEnter);
            this.Close_button.MouseLeave += new System.EventHandler(this.Close_button_MouseLeave);
            // 
            // ShowFamilyMembers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.BackgroundImage = global::MyWorkApplication.Properties.Resources.Back_700;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Project_label);
            this.Controls.Add(this.PersonDataGridView);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Janna LT", 11.25F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.Name = "ShowFamilyMembers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ShowFamilyMembers";
            this.Load += new System.EventHandler(this.ShowFamilyMembers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PersonDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PersonDataGridView;
        private System.Windows.Forms.Label Project_label;
        private System.Windows.Forms.Button Close_button;
    }
}