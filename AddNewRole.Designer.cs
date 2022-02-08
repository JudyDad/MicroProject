namespace MyWorkApplication
{
    partial class AddNewRole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewRole));
            this.Project_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.InsertRole_button = new System.Windows.Forms.Button();
            this.DeleteRole_button = new System.Windows.Forms.Button();
            this.UpdateRole_button = new System.Windows.Forms.Button();
            this.RoleDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RoleName_textBox = new System.Windows.Forms.TextBox();
            this.RoleDescrib_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RoleCode_textBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoleDataGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Project_label
            // 
            this.Project_label.BackColor = System.Drawing.Color.Black;
            this.Project_label.Dock = System.Windows.Forms.DockStyle.Top;
            this.Project_label.Font = new System.Drawing.Font("Proxima Nova Rg", 15F, System.Drawing.FontStyle.Bold);
            this.Project_label.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Project_label.Location = new System.Drawing.Point(0, 0);
            this.Project_label.Name = "Project_label";
            this.Project_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Project_label.Size = new System.Drawing.Size(354, 64);
            this.Project_label.TabIndex = 2;
            this.Project_label.Text = "MICRO PROJECTS";
            this.Project_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.InsertRole_button, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DeleteRole_button, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.UpdateRole_button, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(28, 6);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(292, 33);
            this.tableLayoutPanel1.TabIndex = 449;
            // 
            // InsertRole_button
            // 
            this.InsertRole_button.BackColor = System.Drawing.Color.Transparent;
            this.InsertRole_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.add0;
            this.InsertRole_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InsertRole_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.InsertRole_button.FlatAppearance.BorderSize = 0;
            this.InsertRole_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.InsertRole_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.InsertRole_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.InsertRole_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InsertRole_button.ForeColor = System.Drawing.Color.White;
            this.InsertRole_button.Location = new System.Drawing.Point(3, 2);
            this.InsertRole_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InsertRole_button.Name = "InsertRole_button";
            this.InsertRole_button.Size = new System.Drawing.Size(91, 25);
            this.InsertRole_button.TabIndex = 8;
            this.InsertRole_button.UseVisualStyleBackColor = false;
            this.InsertRole_button.Click += new System.EventHandler(this.InsertRole_button_Click);
            this.InsertRole_button.MouseEnter += new System.EventHandler(this.AddSave_button_MouseEnter);
            this.InsertRole_button.MouseLeave += new System.EventHandler(this.AddSave_button_MouseLeave);
            // 
            // DeleteRole_button
            // 
            this.DeleteRole_button.BackColor = System.Drawing.Color.Transparent;
            this.DeleteRole_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.delete0;
            this.DeleteRole_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DeleteRole_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.DeleteRole_button.FlatAppearance.BorderSize = 0;
            this.DeleteRole_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.DeleteRole_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.DeleteRole_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.DeleteRole_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteRole_button.ForeColor = System.Drawing.Color.White;
            this.DeleteRole_button.Location = new System.Drawing.Point(100, 2);
            this.DeleteRole_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteRole_button.Name = "DeleteRole_button";
            this.DeleteRole_button.Size = new System.Drawing.Size(91, 25);
            this.DeleteRole_button.TabIndex = 10;
            this.DeleteRole_button.Text = " ";
            this.DeleteRole_button.UseVisualStyleBackColor = false;
            this.DeleteRole_button.Click += new System.EventHandler(this.DeleteRole_button_Click);
            this.DeleteRole_button.MouseEnter += new System.EventHandler(this.delete_button_MouseEnter);
            this.DeleteRole_button.MouseLeave += new System.EventHandler(this.delete_button_MouseLeave);
            // 
            // UpdateRole_button
            // 
            this.UpdateRole_button.BackColor = System.Drawing.Color.Transparent;
            this.UpdateRole_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.update0;
            this.UpdateRole_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UpdateRole_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.UpdateRole_button.FlatAppearance.BorderSize = 0;
            this.UpdateRole_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.UpdateRole_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.UpdateRole_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.UpdateRole_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateRole_button.ForeColor = System.Drawing.Color.White;
            this.UpdateRole_button.Location = new System.Drawing.Point(197, 2);
            this.UpdateRole_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UpdateRole_button.Name = "UpdateRole_button";
            this.UpdateRole_button.Size = new System.Drawing.Size(92, 25);
            this.UpdateRole_button.TabIndex = 302;
            this.UpdateRole_button.Text = " ";
            this.UpdateRole_button.UseVisualStyleBackColor = false;
            this.UpdateRole_button.Click += new System.EventHandler(this.UpdateRole_button_Click);
            this.UpdateRole_button.MouseEnter += new System.EventHandler(this.UpdateSave_button_MouseEnter);
            this.UpdateRole_button.MouseLeave += new System.EventHandler(this.UpdateSave_button_MouseLeave);
            // 
            // RoleDataGridView
            // 
            this.RoleDataGridView.AllowUserToAddRows = false;
            this.RoleDataGridView.AllowUserToDeleteRows = false;
            this.RoleDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RoleDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RoleDataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RoleDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.RoleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RoleDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.RoleDataGridView.Location = new System.Drawing.Point(28, 252);
            this.RoleDataGridView.Name = "RoleDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RoleDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.RoleDataGridView.RowTemplate.Height = 26;
            this.RoleDataGridView.Size = new System.Drawing.Size(292, 197);
            this.RoleDataGridView.TabIndex = 451;
            this.RoleDataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RoleDataGridView_RowHeaderMouseClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.63441F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.36559F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.RoleName_textBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.RoleDescrib_textBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.RoleCode_textBox, 1, 4);
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(28, 72);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(292, 175);
            this.tableLayoutPanel2.TabIndex = 450;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Proxima Nova Lt", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label1.Location = new System.Drawing.Point(22, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 14);
            this.label1.TabIndex = 256;
            this.label1.Text = "Role Name :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Proxima Nova Lt", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label2.Location = new System.Drawing.Point(25, 45);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(81, 14);
            this.label2.TabIndex = 257;
            this.label2.Text = "Describion :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RoleName_textBox
            // 
            this.RoleName_textBox.BackColor = System.Drawing.Color.White;
            this.RoleName_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoleName_textBox.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleName_textBox.Location = new System.Drawing.Point(112, 3);
            this.RoleName_textBox.Name = "RoleName_textBox";
            this.RoleName_textBox.Size = new System.Drawing.Size(177, 23);
            this.RoleName_textBox.TabIndex = 248;
            // 
            // RoleDescrib_textBox
            // 
            this.RoleDescrib_textBox.BackColor = System.Drawing.Color.White;
            this.RoleDescrib_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoleDescrib_textBox.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleDescrib_textBox.Location = new System.Drawing.Point(112, 38);
            this.RoleDescrib_textBox.Multiline = true;
            this.RoleDescrib_textBox.Name = "RoleDescrib_textBox";
            this.tableLayoutPanel2.SetRowSpan(this.RoleDescrib_textBox, 3);
            this.RoleDescrib_textBox.Size = new System.Drawing.Size(177, 99);
            this.RoleDescrib_textBox.TabIndex = 249;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Proxima Nova Lt", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label3.Location = new System.Drawing.Point(58, 150);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(48, 14);
            this.label3.TabIndex = 258;
            this.label3.Text = "Code :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RoleCode_textBox
            // 
            this.RoleCode_textBox.BackColor = System.Drawing.Color.White;
            this.RoleCode_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoleCode_textBox.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleCode_textBox.Location = new System.Drawing.Point(112, 143);
            this.RoleCode_textBox.Name = "RoleCode_textBox";
            this.RoleCode_textBox.Size = new System.Drawing.Size(177, 23);
            this.RoleCode_textBox.TabIndex = 250;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 456);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 44);
            this.panel1.TabIndex = 452;
            // 
            // AddNewRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(354, 500);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RoleDataGridView);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.Project_label);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(370, 539);
            this.MinimumSize = new System.Drawing.Size(370, 539);
            this.Name = "AddNewRole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AddNewRole_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoleDataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Project_label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button InsertRole_button;
        private System.Windows.Forms.Button DeleteRole_button;
        private System.Windows.Forms.Button UpdateRole_button;
        private System.Windows.Forms.DataGridView RoleDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RoleName_textBox;
        private System.Windows.Forms.TextBox RoleDescrib_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox RoleCode_textBox;
        private System.Windows.Forms.Panel panel1;
    }
}