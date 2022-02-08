namespace MyWorkApplication
{
    partial class TaskBoard
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
            this.MP_dataGridView = new ADGV.AdvancedDataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Refresh_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Search_TxtBox = new System.Windows.Forms.TextBox();
            this.ShowHide_button = new System.Windows.Forms.Button();
            this.ExactSearch_checkBox = new System.Windows.Forms.CheckBox();
            this.Counter_textBox = new System.Windows.Forms.TextBox();
            this.ExportToExcel_button = new System.Windows.Forms.Button();
            this.MicroProject_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BeneficiaryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Donor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnglishStory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.MP_dataGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MP_dataGridView
            // 
            this.MP_dataGridView.AutoGenerateContextFilters = true;
            this.MP_dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.MP_dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.MP_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MP_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MicroProject_ID,
            this.BeneficiaryName,
            this.ProjectName,
            this.State,
            this.Donor,
            this.Type,
            this.EnglishStory});
            this.MP_dataGridView.DateWithTime = false;
            this.MP_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MP_dataGridView.Location = new System.Drawing.Point(0, 58);
            this.MP_dataGridView.MultiSelect = false;
            this.MP_dataGridView.Name = "MP_dataGridView";
            this.MP_dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MP_dataGridView.RowTemplate.Height = 24;
            this.MP_dataGridView.Size = new System.Drawing.Size(1262, 615);
            this.MP_dataGridView.TabIndex = 2;
            this.MP_dataGridView.TimeFilter = false;
            this.MP_dataGridView.SortStringChanged += new System.EventHandler(this.MP_dataGridView_SortStringChanged);
            this.MP_dataGridView.FilterStringChanged += new System.EventHandler(this.MP_dataGridView_FilterStringChanged);
            this.MP_dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.MP_dataGridView_DataError);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.97912F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.00626F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.1524F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.862213F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel2.Controls.Add(this.Refresh_button, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Search_TxtBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ShowHide_button, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.ExactSearch_checkBox, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.Counter_textBox, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.ExportToExcel_button, 6, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1262, 58);
            this.tableLayoutPanel2.TabIndex = 322;
            // 
            // Refresh_button
            // 
            this.Refresh_button.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Refresh_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.Refresh2_D;
            this.Refresh_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Refresh_button.FlatAppearance.BorderSize = 0;
            this.Refresh_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Refresh_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Refresh_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Refresh_button.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.Refresh_button.Location = new System.Drawing.Point(154, 13);
            this.Refresh_button.MaximumSize = new System.Drawing.Size(32, 32);
            this.Refresh_button.MinimumSize = new System.Drawing.Size(32, 32);
            this.Refresh_button.Name = "Refresh_button";
            this.Refresh_button.Size = new System.Drawing.Size(32, 32);
            this.Refresh_button.TabIndex = 413;
            this.Refresh_button.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Janna LT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(886, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 37);
            this.label5.TabIndex = 0;
            this.label5.Text = "بحث ضمن:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Search_TxtBox
            // 
            this.Search_TxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Search_TxtBox.BackColor = System.Drawing.Color.White;
            this.Search_TxtBox.Font = new System.Drawing.Font("Janna LT", 10F);
            this.Search_TxtBox.ForeColor = System.Drawing.Color.Black;
            this.Search_TxtBox.Location = new System.Drawing.Point(476, 10);
            this.Search_TxtBox.Margin = new System.Windows.Forms.Padding(5);
            this.Search_TxtBox.Name = "Search_TxtBox";
            this.Search_TxtBox.Size = new System.Drawing.Size(400, 38);
            this.Search_TxtBox.TabIndex = 15;
            this.Search_TxtBox.TextChanged += new System.EventHandler(this.Search_TxtBox_TextChanged);
            // 
            // ShowHide_button
            // 
            this.ShowHide_button.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ShowHide_button.BackColor = System.Drawing.Color.Transparent;
            this.ShowHide_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.Down_D;
            this.ShowHide_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ShowHide_button.FlatAppearance.BorderSize = 0;
            this.ShowHide_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ShowHide_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ShowHide_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowHide_button.Font = new System.Drawing.Font("Janna LT", 10F);
            this.ShowHide_button.Location = new System.Drawing.Point(77, 14);
            this.ShowHide_button.MaximumSize = new System.Drawing.Size(30, 30);
            this.ShowHide_button.MinimumSize = new System.Drawing.Size(30, 30);
            this.ShowHide_button.Name = "ShowHide_button";
            this.ShowHide_button.Size = new System.Drawing.Size(30, 30);
            this.ShowHide_button.TabIndex = 445;
            this.ShowHide_button.UseVisualStyleBackColor = false;
            // 
            // ExactSearch_checkBox
            // 
            this.ExactSearch_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ExactSearch_checkBox.AutoSize = true;
            this.ExactSearch_checkBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExactSearch_checkBox.Location = new System.Drawing.Point(358, 11);
            this.ExactSearch_checkBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ExactSearch_checkBox.Name = "ExactSearch_checkBox";
            this.ExactSearch_checkBox.Size = new System.Drawing.Size(110, 35);
            this.ExactSearch_checkBox.TabIndex = 443;
            this.ExactSearch_checkBox.Text = "تطابق كلي";
            this.ExactSearch_checkBox.UseVisualStyleBackColor = true;
            this.ExactSearch_checkBox.CheckedChanged += new System.EventHandler(this.ExactSearch_checkBox_CheckedChanged);
            // 
            // Counter_textBox
            // 
            this.Counter_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Counter_textBox.BackColor = System.Drawing.Color.White;
            this.Counter_textBox.Font = new System.Drawing.Font("Janna LT", 10F);
            this.Counter_textBox.ForeColor = System.Drawing.Color.Black;
            this.Counter_textBox.HideSelection = false;
            this.Counter_textBox.Location = new System.Drawing.Point(192, 10);
            this.Counter_textBox.Name = "Counter_textBox";
            this.Counter_textBox.ReadOnly = true;
            this.Counter_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Counter_textBox.Size = new System.Drawing.Size(115, 38);
            this.Counter_textBox.TabIndex = 409;
            this.Counter_textBox.TabStop = false;
            // 
            // ExportToExcel_button
            // 
            this.ExportToExcel_button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExportToExcel_button.AutoSize = true;
            this.ExportToExcel_button.BackColor = System.Drawing.Color.Transparent;
            this.ExportToExcel_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.Excel_D;
            this.ExportToExcel_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ExportToExcel_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ExportToExcel_button.FlatAppearance.BorderSize = 0;
            this.ExportToExcel_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ExportToExcel_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ExportToExcel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportToExcel_button.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportToExcel_button.ForeColor = System.Drawing.Color.White;
            this.ExportToExcel_button.Location = new System.Drawing.Point(114, 13);
            this.ExportToExcel_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ExportToExcel_button.MaximumSize = new System.Drawing.Size(32, 32);
            this.ExportToExcel_button.MinimumSize = new System.Drawing.Size(32, 32);
            this.ExportToExcel_button.Name = "ExportToExcel_button";
            this.ExportToExcel_button.Size = new System.Drawing.Size(32, 32);
            this.ExportToExcel_button.TabIndex = 446;
            this.ExportToExcel_button.UseVisualStyleBackColor = false;
            // 
            // MicroProject_ID
            // 
            this.MicroProject_ID.HeaderText = "رقم المشروع";
            this.MicroProject_ID.MinimumWidth = 22;
            this.MicroProject_ID.Name = "MicroProject_ID";
            this.MicroProject_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // BeneficiaryName
            // 
            this.BeneficiaryName.HeaderText = "المستفيد";
            this.BeneficiaryName.MinimumWidth = 22;
            this.BeneficiaryName.Name = "BeneficiaryName";
            this.BeneficiaryName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ProjectName
            // 
            this.ProjectName.HeaderText = "اسم المشروع";
            this.ProjectName.MinimumWidth = 22;
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // State
            // 
            this.State.HeaderText = "حالة المشروع";
            this.State.MinimumWidth = 22;
            this.State.Name = "State";
            this.State.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Donor
            // 
            this.Donor.HeaderText = "الجهة الممولة";
            this.Donor.MinimumWidth = 22;
            this.Donor.Name = "Donor";
            this.Donor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Type
            // 
            this.Type.HeaderText = "نوع المشروع";
            this.Type.MinimumWidth = 22;
            this.Type.Name = "Type";
            this.Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // EnglishStory
            // 
            this.EnglishStory.HeaderText = "القصة انكليزي";
            this.EnglishStory.MinimumWidth = 22;
            this.EnglishStory.Name = "EnglishStory";
            this.EnglishStory.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EnglishStory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // TaskBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.MP_dataGridView);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Janna LT", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "TaskBoard";
            this.Text = "TaskBoard";
            this.Load += new System.EventHandler(this.TaskBoard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MP_dataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ADGV.AdvancedDataGridView MP_dataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox ExactSearch_checkBox;
        private System.Windows.Forms.TextBox Counter_textBox;
        private System.Windows.Forms.Button Refresh_button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Search_TxtBox;
        private System.Windows.Forms.Button ShowHide_button;
        private System.Windows.Forms.Button ExportToExcel_button;
        private System.Windows.Forms.DataGridViewTextBoxColumn MicroProject_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeneficiaryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Donor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnglishStory;
    }
}