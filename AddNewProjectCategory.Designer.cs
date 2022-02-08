namespace MyWorkApplication
{
    partial class AddNewProjectCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewProjectCategory));
            this.Project_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.InsertCategory_button = new System.Windows.Forms.Button();
            this.DeleteCategory_button = new System.Windows.Forms.Button();
            this.UpdateCategory_button = new System.Windows.Forms.Button();
            this.Category_dataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.CategoryName_textBox = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Category_dataGridView)).BeginInit();
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
            this.Project_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Project_label.Name = "Project_label";
            this.Project_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Project_label.Size = new System.Drawing.Size(306, 55);
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
            this.tableLayoutPanel1.Controls.Add(this.InsertCategory_button, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DeleteCategory_button, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.UpdateCategory_button, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 6);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(278, 32);
            this.tableLayoutPanel1.TabIndex = 449;
            // 
            // InsertCategory_button
            // 
            this.InsertCategory_button.BackColor = System.Drawing.Color.Transparent;
            this.InsertCategory_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.add0;
            this.InsertCategory_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InsertCategory_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.InsertCategory_button.FlatAppearance.BorderSize = 0;
            this.InsertCategory_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.InsertCategory_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.InsertCategory_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.InsertCategory_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InsertCategory_button.ForeColor = System.Drawing.Color.White;
            this.InsertCategory_button.Location = new System.Drawing.Point(4, 3);
            this.InsertCategory_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.InsertCategory_button.Name = "InsertCategory_button";
            this.InsertCategory_button.Size = new System.Drawing.Size(84, 26);
            this.InsertCategory_button.TabIndex = 8;
            this.InsertCategory_button.UseVisualStyleBackColor = false;
            this.InsertCategory_button.Click += new System.EventHandler(this.InsertCategory_button_Click);
            this.InsertCategory_button.MouseEnter += new System.EventHandler(this.Add_button_MouseEnter);
            this.InsertCategory_button.MouseLeave += new System.EventHandler(this.Add_button_MouseLeave);
            // 
            // DeleteCategory_button
            // 
            this.DeleteCategory_button.BackColor = System.Drawing.Color.Transparent;
            this.DeleteCategory_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.delete0;
            this.DeleteCategory_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DeleteCategory_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.DeleteCategory_button.FlatAppearance.BorderSize = 0;
            this.DeleteCategory_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.DeleteCategory_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.DeleteCategory_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.DeleteCategory_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteCategory_button.ForeColor = System.Drawing.Color.White;
            this.DeleteCategory_button.Location = new System.Drawing.Point(96, 3);
            this.DeleteCategory_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DeleteCategory_button.Name = "DeleteCategory_button";
            this.DeleteCategory_button.Size = new System.Drawing.Size(84, 26);
            this.DeleteCategory_button.TabIndex = 10;
            this.DeleteCategory_button.Text = " ";
            this.DeleteCategory_button.UseVisualStyleBackColor = false;
            this.DeleteCategory_button.Click += new System.EventHandler(this.DeleteCategory_button_Click);
            this.DeleteCategory_button.MouseEnter += new System.EventHandler(this.delete_button_MouseEnter);
            this.DeleteCategory_button.MouseLeave += new System.EventHandler(this.delete_button_MouseLeave);
            // 
            // UpdateCategory_button
            // 
            this.UpdateCategory_button.BackColor = System.Drawing.Color.Transparent;
            this.UpdateCategory_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.update0;
            this.UpdateCategory_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UpdateCategory_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.UpdateCategory_button.FlatAppearance.BorderSize = 0;
            this.UpdateCategory_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.UpdateCategory_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.UpdateCategory_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.UpdateCategory_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateCategory_button.ForeColor = System.Drawing.Color.White;
            this.UpdateCategory_button.Location = new System.Drawing.Point(188, 3);
            this.UpdateCategory_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UpdateCategory_button.Name = "UpdateCategory_button";
            this.UpdateCategory_button.Size = new System.Drawing.Size(86, 26);
            this.UpdateCategory_button.TabIndex = 302;
            this.UpdateCategory_button.Text = " ";
            this.UpdateCategory_button.UseVisualStyleBackColor = false;
            this.UpdateCategory_button.Click += new System.EventHandler(this.UpdateCategory_button_Click);
            this.UpdateCategory_button.MouseEnter += new System.EventHandler(this.Update_button_MouseEnter);
            this.UpdateCategory_button.MouseLeave += new System.EventHandler(this.Update_button_MouseLeave);
            // 
            // Category_dataGridView
            // 
            this.Category_dataGridView.AllowUserToAddRows = false;
            this.Category_dataGridView.AllowUserToDeleteRows = false;
            this.Category_dataGridView.AllowUserToOrderColumns = true;
            this.Category_dataGridView.AllowUserToResizeRows = false;
            this.Category_dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Category_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Category_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.Category_dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.Category_dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Proxima Nova Rg", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Category_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Category_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Proxima Nova Rg", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Category_dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.Category_dataGridView.EnableHeadersVisualStyles = false;
            this.Category_dataGridView.Location = new System.Drawing.Point(15, 108);
            this.Category_dataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Category_dataGridView.MultiSelect = false;
            this.Category_dataGridView.Name = "Category_dataGridView";
            this.Category_dataGridView.ReadOnly = true;
            this.Category_dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Proxima Nova Rg", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Category_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Category_dataGridView.RowHeadersWidth = 25;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Category_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Category_dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Category_dataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Category_dataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Category_dataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.Category_dataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Category_dataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.Category_dataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Category_dataGridView.RowTemplate.ReadOnly = true;
            this.Category_dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Category_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Category_dataGridView.Size = new System.Drawing.Size(276, 207);
            this.Category_dataGridView.StandardTab = true;
            this.Category_dataGridView.TabIndex = 448;
            this.Category_dataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Category_dataGridView_RowHeaderMouseClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.1677F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.8323F));
            this.tableLayoutPanel2.Controls.Add(this.CategoryName_textBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label48, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(15, 65);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(278, 35);
            this.tableLayoutPanel2.TabIndex = 447;
            // 
            // CategoryName_textBox
            // 
            this.CategoryName_textBox.BackColor = System.Drawing.Color.White;
            this.CategoryName_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CategoryName_textBox.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.CategoryName_textBox.Location = new System.Drawing.Point(124, 7);
            this.CategoryName_textBox.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.CategoryName_textBox.Name = "CategoryName_textBox";
            this.CategoryName_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CategoryName_textBox.Size = new System.Drawing.Size(150, 24);
            this.CategoryName_textBox.TabIndex = 318;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Dock = System.Windows.Forms.DockStyle.Right;
            this.label48.Font = new System.Drawing.Font("Proxima Nova Lt", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label48.Location = new System.Drawing.Point(6, 0);
            this.label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label48.Name = "label48";
            this.label48.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label48.Size = new System.Drawing.Size(110, 35);
            this.label48.TabIndex = 253;
            this.label48.Text = "Category Name:";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 322);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 44);
            this.panel1.TabIndex = 451;
            // 
            // AddNewProjectCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(306, 366);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Category_dataGridView);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.Project_label);
            this.Font = new System.Drawing.Font("Proxima Nova Rg", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AddNewProjectCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AddNewProjectCategory_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Category_dataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Project_label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button InsertCategory_button;
        private System.Windows.Forms.Button DeleteCategory_button;
        private System.Windows.Forms.Button UpdateCategory_button;
        private System.Windows.Forms.DataGridView Category_dataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox CategoryName_textBox;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Panel panel1;
    }
}