namespace MyWorkApplication
{
    partial class AddNewPlace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewPlace));
            this.DeletePlace_button = new System.Windows.Forms.Button();
            this.UpdatePlace_button = new System.Windows.Forms.Button();
            this.Place_dataGridView = new System.Windows.Forms.DataGridView();
            this.InsertPlace_button = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.PlaceName_textBox = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Project_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Place_dataGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeletePlace_button
            // 
            this.DeletePlace_button.BackColor = System.Drawing.Color.Transparent;
            this.DeletePlace_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.delete0;
            this.DeletePlace_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DeletePlace_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.DeletePlace_button.FlatAppearance.BorderSize = 0;
            this.DeletePlace_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.DeletePlace_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.DeletePlace_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.DeletePlace_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeletePlace_button.ForeColor = System.Drawing.Color.White;
            this.DeletePlace_button.Location = new System.Drawing.Point(86, 2);
            this.DeletePlace_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeletePlace_button.Name = "DeletePlace_button";
            this.DeletePlace_button.Size = new System.Drawing.Size(77, 20);
            this.DeletePlace_button.TabIndex = 331;
            this.DeletePlace_button.Text = " ";
            this.DeletePlace_button.UseVisualStyleBackColor = false;
            this.DeletePlace_button.Enter += new System.EventHandler(this.DeletePlace_button_Click);
            this.DeletePlace_button.MouseEnter += new System.EventHandler(this.Delete_button_MouseEnter);
            this.DeletePlace_button.MouseLeave += new System.EventHandler(this.Delete_button_MouseLeave);
            // 
            // UpdatePlace_button
            // 
            this.UpdatePlace_button.BackColor = System.Drawing.Color.Transparent;
            this.UpdatePlace_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.update0;
            this.UpdatePlace_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UpdatePlace_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.UpdatePlace_button.FlatAppearance.BorderSize = 0;
            this.UpdatePlace_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.UpdatePlace_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.UpdatePlace_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.UpdatePlace_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdatePlace_button.ForeColor = System.Drawing.Color.White;
            this.UpdatePlace_button.Location = new System.Drawing.Point(169, 2);
            this.UpdatePlace_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UpdatePlace_button.Name = "UpdatePlace_button";
            this.UpdatePlace_button.Size = new System.Drawing.Size(78, 20);
            this.UpdatePlace_button.TabIndex = 332;
            this.UpdatePlace_button.Text = " ";
            this.UpdatePlace_button.UseVisualStyleBackColor = false;
            this.UpdatePlace_button.Enter += new System.EventHandler(this.UpdatePlace_button_Click);
            this.UpdatePlace_button.MouseEnter += new System.EventHandler(this.Update_button_MouseEnter);
            this.UpdatePlace_button.MouseLeave += new System.EventHandler(this.Update_button_MouseLeave);
            // 
            // Place_dataGridView
            // 
            this.Place_dataGridView.AllowUserToAddRows = false;
            this.Place_dataGridView.AllowUserToDeleteRows = false;
            this.Place_dataGridView.AllowUserToOrderColumns = true;
            this.Place_dataGridView.AllowUserToResizeRows = false;
            this.Place_dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Place_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Place_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.Place_dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.Place_dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Place_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Place_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Place_dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.Place_dataGridView.EnableHeadersVisualStyles = false;
            this.Place_dataGridView.Location = new System.Drawing.Point(27, 93);
            this.Place_dataGridView.MultiSelect = false;
            this.Place_dataGridView.Name = "Place_dataGridView";
            this.Place_dataGridView.ReadOnly = true;
            this.Place_dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Place_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Place_dataGridView.RowHeadersWidth = 25;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Place_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Place_dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Place_dataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Place_dataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Place_dataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.Place_dataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Place_dataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.Place_dataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Place_dataGridView.RowTemplate.ReadOnly = true;
            this.Place_dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Place_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Place_dataGridView.Size = new System.Drawing.Size(250, 220);
            this.Place_dataGridView.StandardTab = true;
            this.Place_dataGridView.TabIndex = 334;
            this.Place_dataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Place_dataGridView_RowHeaderMouseClick);
            // 
            // InsertPlace_button
            // 
            this.InsertPlace_button.BackColor = System.Drawing.Color.Transparent;
            this.InsertPlace_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.add0;
            this.InsertPlace_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InsertPlace_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.InsertPlace_button.FlatAppearance.BorderSize = 0;
            this.InsertPlace_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.InsertPlace_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.InsertPlace_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.InsertPlace_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InsertPlace_button.ForeColor = System.Drawing.Color.White;
            this.InsertPlace_button.Location = new System.Drawing.Point(3, 2);
            this.InsertPlace_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InsertPlace_button.Name = "InsertPlace_button";
            this.InsertPlace_button.Size = new System.Drawing.Size(77, 20);
            this.InsertPlace_button.TabIndex = 330;
            this.InsertPlace_button.UseVisualStyleBackColor = false;
            this.InsertPlace_button.Click += new System.EventHandler(this.InsertPlace_button_Click);
            this.InsertPlace_button.MouseEnter += new System.EventHandler(this.Add_button_MouseEnter);
            this.InsertPlace_button.MouseLeave += new System.EventHandler(this.Add_button_MouseLeave);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.77104F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.22896F));
            this.tableLayoutPanel2.Controls.Add(this.PlaceName_textBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label48, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(27, 54);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(253, 33);
            this.tableLayoutPanel2.TabIndex = 333;
            // 
            // PlaceName_textBox
            // 
            this.PlaceName_textBox.BackColor = System.Drawing.Color.White;
            this.PlaceName_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlaceName_textBox.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.PlaceName_textBox.Location = new System.Drawing.Point(113, 4);
            this.PlaceName_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PlaceName_textBox.Name = "PlaceName_textBox";
            this.PlaceName_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PlaceName_textBox.Size = new System.Drawing.Size(137, 24);
            this.PlaceName_textBox.TabIndex = 318;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Dock = System.Windows.Forms.DockStyle.Right;
            this.label48.Font = new System.Drawing.Font("Proxima Nova Lt", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label48.Location = new System.Drawing.Point(20, 0);
            this.label48.Name = "label48";
            this.label48.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label48.Size = new System.Drawing.Size(87, 33);
            this.label48.TabIndex = 253;
            this.label48.Text = "Place Name:";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.Project_label);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.ForeColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(302, 52);
            this.panel3.TabIndex = 449;
            // 
            // Project_label
            // 
            this.Project_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Project_label.Font = new System.Drawing.Font("Proxima Nova Rg", 15F, System.Drawing.FontStyle.Bold);
            this.Project_label.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Project_label.Location = new System.Drawing.Point(0, 0);
            this.Project_label.Name = "Project_label";
            this.Project_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Project_label.Size = new System.Drawing.Size(302, 52);
            this.Project_label.TabIndex = 1;
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
            this.tableLayoutPanel1.Controls.Add(this.InsertPlace_button, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DeletePlace_button, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.UpdatePlace_button, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(27, 9);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(250, 27);
            this.tableLayoutPanel1.TabIndex = 450;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 320);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 42);
            this.panel1.TabIndex = 451;
            // 
            // AddNewPlace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(302, 362);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.Place_dataGridView);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddNewPlace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AddNewPlace_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Place_dataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DeletePlace_button;
        private System.Windows.Forms.Button UpdatePlace_button;
        private System.Windows.Forms.DataGridView Place_dataGridView;
        private System.Windows.Forms.Button InsertPlace_button;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox PlaceName_textBox;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label Project_label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}