﻿namespace MyWorkApplication
{
    partial class Partners_MessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Partners_MessageBox));
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.MP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Beneficiary_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.P_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Name_label = new System.Windows.Forms.Label();
            this.Back_button = new System.Windows.Forms.Button();
            this.New_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView
            // 
            this.DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Janna LT", 10.2F);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MP_ID,
            this.Beneficiary_Name,
            this.P_ID});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Janna LT", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(216)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView.EnableHeadersVisualStyles = false;
            this.DataGridView.GridColor = System.Drawing.Color.DarkGray;
            this.DataGridView.Location = new System.Drawing.Point(25, 71);
            this.DataGridView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Janna LT", 10.2F);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.RowTemplate.Height = 26;
            this.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.Size = new System.Drawing.Size(478, 195);
            this.DataGridView.TabIndex = 394;
            this.DataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView_DataError);
            // 
            // MP_ID
            // 
            this.MP_ID.FillWeight = 50F;
            this.MP_ID.HeaderText = "رقم المشروع";
            this.MP_ID.Name = "MP_ID";
            this.MP_ID.ReadOnly = true;
            // 
            // Beneficiary_Name
            // 
            this.Beneficiary_Name.HeaderText = "اسم المستفيد";
            this.Beneficiary_Name.Name = "Beneficiary_Name";
            this.Beneficiary_Name.ReadOnly = true;
            // 
            // P_ID
            // 
            this.P_ID.HeaderText = "Person_ID";
            this.P_ID.Name = "P_ID";
            this.P_ID.ReadOnly = true;
            this.P_ID.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::MyWorkApplication.Properties.Resources.info;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(468, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 38);
            this.pictureBox1.TabIndex = 451;
            this.pictureBox1.TabStop = false;
            // 
            // Name_label
            // 
            this.Name_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Name_label.AutoSize = true;
            this.Name_label.BackColor = System.Drawing.Color.Transparent;
            this.Name_label.Font = new System.Drawing.Font("Janna LT", 10F);
            this.Name_label.ForeColor = System.Drawing.Color.Black;
            this.Name_label.Location = new System.Drawing.Point(25, 24);
            this.Name_label.Margin = new System.Windows.Forms.Padding(0);
            this.Name_label.Name = "Name_label";
            this.Name_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Name_label.Size = new System.Drawing.Size(436, 31);
            this.Name_label.TabIndex = 452;
            this.Name_label.Text = "هذا المستفيد يملك شركاء . يمكنك اختيار شريك وإضافة زيارة له:";
            this.Name_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Back_button
            // 
            this.Back_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Back_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Back_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Back_button.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Back_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.Back_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Back_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Back_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back_button.ForeColor = System.Drawing.Color.Black;
            this.Back_button.Location = new System.Drawing.Point(92, 290);
            this.Back_button.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.Back_button.Name = "Back_button";
            this.Back_button.Size = new System.Drawing.Size(147, 37);
            this.Back_button.TabIndex = 461;
            this.Back_button.Text = "تراجع";
            this.Back_button.UseVisualStyleBackColor = false;
            this.Back_button.Click += new System.EventHandler(this.Back_button_Click);
            // 
            // New_button
            // 
            this.New_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.New_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.New_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.New_button.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.New_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.New_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.New_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.New_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.New_button.ForeColor = System.Drawing.Color.Black;
            this.New_button.Location = new System.Drawing.Point(258, 290);
            this.New_button.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.New_button.Name = "New_button";
            this.New_button.Size = new System.Drawing.Size(147, 37);
            this.New_button.TabIndex = 463;
            this.New_button.Text = "زيارة/تقييم جديد";
            this.New_button.UseVisualStyleBackColor = false;
            this.New_button.Click += new System.EventHandler(this.New_button_Click);
            // 
            // Partners_MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 351);
            this.Controls.Add(this.Back_button);
            this.Controls.Add(this.New_button);
            this.Controls.Add(this.Name_label);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DataGridView);
            this.Font = new System.Drawing.Font("Janna LT", 10.2F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "Partners_MessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partners Message Box";
            this.Load += new System.EventHandler(this.Partners_MessageBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Name_label;
        private System.Windows.Forms.Button Back_button;
        private System.Windows.Forms.Button New_button;
        private System.Windows.Forms.DataGridViewTextBoxColumn MP_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Beneficiary_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn P_ID;
    }
}