namespace MyWorkApplication
{
    partial class AllApplications
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllApplications));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.MP_idTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Application_DataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AddApplication_button = new System.Windows.Forms.Button();
            this.UpdateApplication_button = new System.Windows.Forms.Button();
            this.DeleteApplication_button = new System.Windows.Forms.Button();
            this.Counter_textBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Application_DataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.69149F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.75288F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.86413F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.69149F));
            this.tableLayoutPanel2.Controls.Add(this.MP_idTxtBox, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(532, 50);
            this.tableLayoutPanel2.TabIndex = 411;
            // 
            // MP_idTxtBox
            // 
            this.MP_idTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MP_idTxtBox.BackColor = System.Drawing.Color.White;
            this.MP_idTxtBox.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.MP_idTxtBox.Location = new System.Drawing.Point(172, 11);
            this.MP_idTxtBox.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.MP_idTxtBox.Name = "MP_idTxtBox";
            this.MP_idTxtBox.Size = new System.Drawing.Size(281, 28);
            this.MP_idTxtBox.TabIndex = 15;
            this.MP_idTxtBox.TextChanged += new System.EventHandler(this.MP_idTxtBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Location = new System.Drawing.Point(95, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 50);
            this.label5.TabIndex = 0;
            this.label5.Text = "Search:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Application_DataGridView
            // 
            this.Application_DataGridView.AllowUserToAddRows = false;
            this.Application_DataGridView.AllowUserToDeleteRows = false;
            this.Application_DataGridView.AllowUserToOrderColumns = true;
            this.Application_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Application_DataGridView.BackgroundColor = System.Drawing.Color.White;
            this.Application_DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Application_DataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Proxima Nova Rg", 10.8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Application_DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Application_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Proxima Nova Rg", 10.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Application_DataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.Application_DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Application_DataGridView.EnableHeadersVisualStyles = false;
            this.Application_DataGridView.Location = new System.Drawing.Point(0, 50);
            this.Application_DataGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Application_DataGridView.Name = "Application_DataGridView";
            this.Application_DataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Application_DataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Proxima Nova Rg", 10.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Application_DataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 10.8F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Application_DataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Application_DataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Application_DataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.Application_DataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10.8F);
            this.Application_DataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.Application_DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSlateGray;
            this.Application_DataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.Application_DataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Application_DataGridView.RowTemplate.Height = 26;
            this.Application_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Application_DataGridView.Size = new System.Drawing.Size(532, 578);
            this.Application_DataGridView.TabIndex = 412;
            this.Application_DataGridView.VirtualMode = true;
            this.Application_DataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Application_DataGridView_RowHeaderMouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.Counter_textBox);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 628);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 45);
            this.panel1.TabIndex = 486;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.AddApplication_button, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.UpdateApplication_button, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.DeleteApplication_button, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(172, 30);
            this.tableLayoutPanel1.TabIndex = 393;
            // 
            // AddApplication_button
            // 
            this.AddApplication_button.BackColor = System.Drawing.Color.Transparent;
            this.AddApplication_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.add0;
            this.AddApplication_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddApplication_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.AddApplication_button.FlatAppearance.BorderSize = 0;
            this.AddApplication_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.AddApplication_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.AddApplication_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddApplication_button.ForeColor = System.Drawing.Color.White;
            this.AddApplication_button.Location = new System.Drawing.Point(3, 2);
            this.AddApplication_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddApplication_button.Name = "AddApplication_button";
            this.AddApplication_button.Size = new System.Drawing.Size(51, 24);
            this.AddApplication_button.TabIndex = 50;
            this.AddApplication_button.UseVisualStyleBackColor = false;
            this.AddApplication_button.Click += new System.EventHandler(this.AddApplication_button_Click);
            // 
            // UpdateApplication_button
            // 
            this.UpdateApplication_button.BackColor = System.Drawing.Color.Transparent;
            this.UpdateApplication_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.update0;
            this.UpdateApplication_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UpdateApplication_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.UpdateApplication_button.FlatAppearance.BorderSize = 0;
            this.UpdateApplication_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.UpdateApplication_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.UpdateApplication_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateApplication_button.ForeColor = System.Drawing.Color.White;
            this.UpdateApplication_button.Location = new System.Drawing.Point(60, 2);
            this.UpdateApplication_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UpdateApplication_button.Name = "UpdateApplication_button";
            this.UpdateApplication_button.Size = new System.Drawing.Size(51, 24);
            this.UpdateApplication_button.TabIndex = 51;
            this.UpdateApplication_button.UseVisualStyleBackColor = false;
            this.UpdateApplication_button.Click += new System.EventHandler(this.UpdateApplication_button_Click);
            // 
            // DeleteApplication_button
            // 
            this.DeleteApplication_button.BackColor = System.Drawing.Color.Transparent;
            this.DeleteApplication_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.delete0;
            this.DeleteApplication_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DeleteApplication_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.DeleteApplication_button.FlatAppearance.BorderSize = 0;
            this.DeleteApplication_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.DeleteApplication_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.DeleteApplication_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteApplication_button.ForeColor = System.Drawing.Color.White;
            this.DeleteApplication_button.Location = new System.Drawing.Point(117, 2);
            this.DeleteApplication_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteApplication_button.Name = "DeleteApplication_button";
            this.DeleteApplication_button.Size = new System.Drawing.Size(52, 24);
            this.DeleteApplication_button.TabIndex = 52;
            this.DeleteApplication_button.UseVisualStyleBackColor = false;
            this.DeleteApplication_button.Click += new System.EventHandler(this.DeleteApplication_button_Click);
            // 
            // Counter_textBox
            // 
            this.Counter_textBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Counter_textBox.BackColor = System.Drawing.Color.White;
            this.Counter_textBox.Font = new System.Drawing.Font("Proxima Nova Lt", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Counter_textBox.HideSelection = false;
            this.Counter_textBox.Location = new System.Drawing.Point(413, 10);
            this.Counter_textBox.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Counter_textBox.Name = "Counter_textBox";
            this.Counter_textBox.ReadOnly = true;
            this.Counter_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Counter_textBox.Size = new System.Drawing.Size(106, 24);
            this.Counter_textBox.TabIndex = 410;
            this.Counter_textBox.TabStop = false;
            // 
            // AllApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(532, 673);
            this.Controls.Add(this.Application_DataGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Proxima Nova Rg", 10.8F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AllApplications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AllApplications_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Application_DataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox MP_idTxtBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView Application_DataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button AddApplication_button;
        private System.Windows.Forms.Button UpdateApplication_button;
        private System.Windows.Forms.Button DeleteApplication_button;
        private System.Windows.Forms.TextBox Counter_textBox;
    }
}