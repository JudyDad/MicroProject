namespace MyWorkApplication
{
    partial class Statistics_Form
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series17 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series18 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series19 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series20 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series21 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series22 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series23 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series24 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Statistics_Form));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDetailsInSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.AdvancedSearch_button = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.X_Axis_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.results_dataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Count_label = new System.Windows.Forms.Label();
            this.ExportToExcel_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Search_panel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.DonorGroup_label = new System.Windows.Forms.Label();
            this.DonorGroup_comboBox = new System.Windows.Forms.ComboBox();
            this.FundedFrom_textBox = new System.Windows.Forms.TextBox();
            this.FundedFrom_label = new System.Windows.Forms.Label();
            this.RequestedFrom_label = new System.Windows.Forms.Label();
            this.RequestedFrom_textBox = new System.Windows.Forms.TextBox();
            this.FundedTo_label = new System.Windows.Forms.Label();
            this.FundedTo_textBox = new System.Windows.Forms.TextBox();
            this.RequestedTo_textBox = new System.Windows.Forms.TextBox();
            this.RequestedTo_label = new System.Windows.Forms.Label();
            this.Funded_checkBox = new System.Windows.Forms.CheckBox();
            this.Requested_checkBox = new System.Windows.Forms.CheckBox();
            this.FundedDate_checkBox = new System.Windows.Forms.CheckBox();
            this.ApplyDate_checkBox = new System.Windows.Forms.CheckBox();
            this.FundDateTo_bcDateTimePicker = new MyWorkApplication.Classes.BCDateTimePicker();
            this.ApplyDateTo_bcDateTimePicker = new MyWorkApplication.Classes.BCDateTimePicker();
            this.FundedDateTo_label = new System.Windows.Forms.Label();
            this.ApplyDateTo_label = new System.Windows.Forms.Label();
            this.FundDateFrom_bcDateTimePicker = new MyWorkApplication.Classes.BCDateTimePicker();
            this.ApplyDateFrom_dateTimePicker = new MyWorkApplication.Classes.BCDateTimePicker();
            this.FundedDateFrom_label = new System.Windows.Forms.Label();
            this.ApplyDateFrom_label = new System.Windows.Forms.Label();
            this.ApplyDate_comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FundDate_comboBox = new System.Windows.Forms.ComboBox();
            this.P_Parish_label = new System.Windows.Forms.Label();
            this.P_Parish_comboBox = new System.Windows.Forms.ComboBox();
            this.Age_label = new System.Windows.Forms.Label();
            this.Age_comboBox = new System.Windows.Forms.ComboBox();
            this.Gender_label = new System.Windows.Forms.Label();
            this.Gender_comboBox = new System.Windows.Forms.ComboBox();
            this.MaritalStatus_label = new System.Windows.Forms.Label();
            this.MaritalStatus_comboBox = new System.Windows.Forms.ComboBox();
            this.MP_Status_label = new System.Windows.Forms.Label();
            this.MP_Status_comboBox = new System.Windows.Forms.ComboBox();
            this.Donor_label = new System.Windows.Forms.Label();
            this.Donor_comboBox = new System.Windows.Forms.ComboBox();
            this.Street_comboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MP_Category_label = new System.Windows.Forms.Label();
            this.MP_Category_comboBox = new System.Windows.Forms.ComboBox();
            this.SubCategory_label = new System.Windows.Forms.Label();
            this.SubCategory_comboBox = new System.Windows.Forms.ComboBox();
            this.Partnership_comboBox = new System.Windows.Forms.ComboBox();
            this.Partnership_label = new System.Windows.Forms.Label();
            this.Type_comboBox = new System.Windows.Forms.ComboBox();
            this.Type_label = new System.Windows.Forms.Label();
            this.FundType_label = new System.Windows.Forms.Label();
            this.FundType_comboBox = new System.Windows.Forms.ComboBox();
            this.SubType_label = new System.Windows.Forms.Label();
            this.SubType_comboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.results_dataGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.Search_panel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Graphics;
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.IsSoftShadows = false;
            legend2.Alignment = System.Drawing.StringAlignment.Far;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend2.IsDockedInsideChartArea = false;
            legend2.IsEquallySpacedItems = true;
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend0";
            legend2.TableStyle = System.Windows.Forms.DataVisualization.Charting.LegendTableStyle.Wide;
            legend2.TitleFont = new System.Drawing.Font("Avenir LT Std 45 Book", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 316);
            this.chart1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart1.Name = "chart1";
            series13.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series13.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series13.IsValueShownAsLabel = true;
            series13.IsXValueIndexed = true;
            series13.Legend = "Legend0";
            series13.MarkerBorderColor = System.Drawing.Color.White;
            series13.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series13.Name = "state";
            series13.ShadowOffset = 1;
            series13.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series13.SmartLabelStyle.CalloutLineAnchorCapStyle = System.Windows.Forms.DataVisualization.Charting.LineAnchorCapStyle.Round;
            series13.SmartLabelStyle.CalloutLineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series13.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.Box;
            series13.SmartLabelStyle.IsOverlappedHidden = false;
            series14.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series14.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series14.IsValueShownAsLabel = true;
            series14.IsXValueIndexed = true;
            series14.Legend = "Legend0";
            series14.MarkerBorderColor = System.Drawing.Color.White;
            series14.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series14.Name = "type";
            series14.ShadowOffset = 1;
            series15.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series15.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series15.IsValueShownAsLabel = true;
            series15.IsXValueIndexed = true;
            series15.Legend = "Legend0";
            series15.MarkerBorderColor = System.Drawing.Color.White;
            series15.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series15.Name = "category";
            series15.ShadowOffset = 1;
            series15.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series15.SmartLabelStyle.CalloutLineAnchorCapStyle = System.Windows.Forms.DataVisualization.Charting.LineAnchorCapStyle.Round;
            series15.SmartLabelStyle.CalloutLineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series15.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.Box;
            series15.SmartLabelStyle.IsOverlappedHidden = false;
            series16.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series16.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series16.IsValueShownAsLabel = true;
            series16.IsXValueIndexed = true;
            series16.Legend = "Legend0";
            series16.MarkerBorderColor = System.Drawing.Color.White;
            series16.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series16.Name = "parishes";
            series16.ShadowOffset = 1;
            series16.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series16.SmartLabelStyle.CalloutLineAnchorCapStyle = System.Windows.Forms.DataVisualization.Charting.LineAnchorCapStyle.Round;
            series16.SmartLabelStyle.CalloutLineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series16.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.Box;
            series16.SmartLabelStyle.IsOverlappedHidden = false;
            series17.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series17.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series17.IsValueShownAsLabel = true;
            series17.IsXValueIndexed = true;
            series17.Legend = "Legend0";
            series17.MarkerBorderColor = System.Drawing.Color.White;
            series17.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series17.Name = "donor";
            series17.ShadowOffset = 1;
            series17.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series17.SmartLabelStyle.CalloutLineAnchorCapStyle = System.Windows.Forms.DataVisualization.Charting.LineAnchorCapStyle.Round;
            series17.SmartLabelStyle.CalloutLineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series17.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.Box;
            series17.SmartLabelStyle.IsOverlappedHidden = false;
            series18.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series18.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series18.IsValueShownAsLabel = true;
            series18.IsXValueIndexed = true;
            series18.Legend = "Legend0";
            series18.MarkerBorderColor = System.Drawing.Color.White;
            series18.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series18.Name = "gender";
            series18.ShadowOffset = 1;
            series18.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series18.SmartLabelStyle.CalloutLineAnchorCapStyle = System.Windows.Forms.DataVisualization.Charting.LineAnchorCapStyle.Round;
            series18.SmartLabelStyle.CalloutLineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series18.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.Box;
            series18.SmartLabelStyle.IsOverlappedHidden = false;
            series19.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series19.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series19.IsValueShownAsLabel = true;
            series19.IsXValueIndexed = true;
            series19.Legend = "Legend0";
            series19.MarkerBorderColor = System.Drawing.Color.White;
            series19.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series19.Name = "maritalStatus";
            series19.ShadowOffset = 1;
            series19.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series19.SmartLabelStyle.CalloutLineAnchorCapStyle = System.Windows.Forms.DataVisualization.Charting.LineAnchorCapStyle.Round;
            series19.SmartLabelStyle.CalloutLineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series19.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.Box;
            series19.SmartLabelStyle.IsOverlappedHidden = false;
            series20.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series20.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series20.IsValueShownAsLabel = true;
            series20.IsXValueIndexed = true;
            series20.Legend = "Legend0";
            series20.MarkerBorderColor = System.Drawing.Color.White;
            series20.Name = "Age";
            series20.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series20.SmartLabelStyle.CalloutLineAnchorCapStyle = System.Windows.Forms.DataVisualization.Charting.LineAnchorCapStyle.Round;
            series20.SmartLabelStyle.CalloutLineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series20.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.Box;
            series20.SmartLabelStyle.IsOverlappedHidden = false;
            series21.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series21.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series21.IsValueShownAsLabel = true;
            series21.IsXValueIndexed = true;
            series21.Legend = "Legend0";
            series21.MarkerBorderColor = System.Drawing.Color.White;
            series21.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series21.Name = "purpuse";
            series21.ShadowOffset = 1;
            series22.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series22.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series22.IsValueShownAsLabel = true;
            series22.IsXValueIndexed = true;
            series22.Legend = "Legend0";
            series22.MarkerBorderColor = System.Drawing.Color.White;
            series22.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series22.Name = "experience";
            series22.ShadowOffset = 1;
            series23.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series23.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series23.IsValueShownAsLabel = true;
            series23.IsXValueIndexed = true;
            series23.Legend = "Legend0";
            series23.MarkerBorderColor = System.Drawing.Color.White;
            series23.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series23.Name = "profitRate";
            series23.ShadowOffset = 1;
            series24.CustomProperties = "DrawSideBySide=True, MinPixelPointWidth=30, MaxPixelPointWidth=60, EmptyPointValu" +
    "e=Zero";
            series24.Font = new System.Drawing.Font("AvenirNext LT Pro Bold", 9.75F, System.Drawing.FontStyle.Bold);
            series24.IsValueShownAsLabel = true;
            series24.IsXValueIndexed = true;
            series24.Legend = "Legend0";
            series24.Name = "partnership";
            series24.ShadowOffset = 1;
            this.chart1.Series.Add(series13);
            this.chart1.Series.Add(series14);
            this.chart1.Series.Add(series15);
            this.chart1.Series.Add(series16);
            this.chart1.Series.Add(series17);
            this.chart1.Series.Add(series18);
            this.chart1.Series.Add(series19);
            this.chart1.Series.Add(series20);
            this.chart1.Series.Add(series21);
            this.chart1.Series.Add(series22);
            this.chart1.Series.Add(series23);
            this.chart1.Series.Add(series24);
            this.chart1.Size = new System.Drawing.Size(1264, 317);
            this.chart1.SuppressExceptions = true;
            this.chart1.TabIndex = 412;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshPageToolStripMenuItem,
            this.showHideFiltersToolStripMenuItem,
            this.clearAllFiltersToolStripMenuItem,
            this.showDetailsInSearchToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(276, 100);
            // 
            // refreshPageToolStripMenuItem
            // 
            this.refreshPageToolStripMenuItem.Name = "refreshPageToolStripMenuItem";
            this.refreshPageToolStripMenuItem.Size = new System.Drawing.Size(275, 24);
            this.refreshPageToolStripMenuItem.Text = "Refresh Page";
            this.refreshPageToolStripMenuItem.Click += new System.EventHandler(this.refreshPageToolStripMenuItem_Click);
            // 
            // showHideFiltersToolStripMenuItem
            // 
            this.showHideFiltersToolStripMenuItem.Name = "showHideFiltersToolStripMenuItem";
            this.showHideFiltersToolStripMenuItem.Size = new System.Drawing.Size(275, 24);
            this.showHideFiltersToolStripMenuItem.Text = "Show/Hide Filters";
            this.showHideFiltersToolStripMenuItem.Click += new System.EventHandler(this.showHideFiltersToolStripMenuItem_Click);
            // 
            // clearAllFiltersToolStripMenuItem
            // 
            this.clearAllFiltersToolStripMenuItem.Name = "clearAllFiltersToolStripMenuItem";
            this.clearAllFiltersToolStripMenuItem.Size = new System.Drawing.Size(275, 24);
            this.clearAllFiltersToolStripMenuItem.Text = "Clear all filters";
            this.clearAllFiltersToolStripMenuItem.Click += new System.EventHandler(this.clearAllFiltersToolStripMenuItem_Click);
            // 
            // showDetailsInSearchToolStripMenuItem
            // 
            this.showDetailsInSearchToolStripMenuItem.Name = "showDetailsInSearchToolStripMenuItem";
            this.showDetailsInSearchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.showDetailsInSearchToolStripMenuItem.Size = new System.Drawing.Size(275, 24);
            this.showDetailsInSearchToolStripMenuItem.Text = "Show Details in search";
            this.showDetailsInSearchToolStripMenuItem.Click += new System.EventHandler(this.showDetailsInSearchToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.481013F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.92405F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.46519F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.88608F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.50633F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.177216F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.10443F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.746831F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ContextMenuStrip = this.contextMenuStrip1;
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.AdvancedSearch_button, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.label16, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.X_Axis_comboBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.results_dataGridView, 8, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1264, 50);
            this.tableLayoutPanel1.TabIndex = 413;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.Font = new System.Drawing.Font("Janna LT", 9F);
            this.comboBox1.ForeColor = System.Drawing.Color.Black;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(322, 6);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboBox1.Size = new System.Drawing.Size(185, 37);
            this.comboBox1.TabIndex = 462;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // AdvancedSearch_button
            // 
            this.AdvancedSearch_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.AdvancedSearch_button.BackColor = System.Drawing.Color.Transparent;
            this.AdvancedSearch_button.BackgroundImage = global::MyWorkApplication.Properties.Resources.Show2_L;
            this.AdvancedSearch_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AdvancedSearch_button.ContextMenuStrip = this.contextMenuStrip1;
            this.AdvancedSearch_button.FlatAppearance.BorderSize = 0;
            this.AdvancedSearch_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.AdvancedSearch_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.AdvancedSearch_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AdvancedSearch_button.Font = new System.Drawing.Font("Janna LT", 10F);
            this.AdvancedSearch_button.Location = new System.Drawing.Point(50, 8);
            this.AdvancedSearch_button.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.AdvancedSearch_button.Name = "AdvancedSearch_button";
            this.AdvancedSearch_button.Size = new System.Drawing.Size(153, 37);
            this.AdvancedSearch_button.TabIndex = 441;
            this.AdvancedSearch_button.UseVisualStyleBackColor = false;
            this.AdvancedSearch_button.Click += new System.EventHandler(this.AdvancedSearch_button_Click);
            this.AdvancedSearch_button.MouseEnter += new System.EventHandler(this.AdvancedSearch_button_MouseEnter);
            this.AdvancedSearch_button.MouseLeave += new System.EventHandler(this.AdvancedSearch_button_MouseLeave);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Janna LT", 10F);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label16.Location = new System.Drawing.Point(1069, 9);
            this.label16.Margin = new System.Windows.Forms.Padding(7);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 31);
            this.label16.TabIndex = 296;
            this.label16.Text = "محور X :";
            // 
            // X_Axis_comboBox
            // 
            this.X_Axis_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.X_Axis_comboBox.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.X_Axis_comboBox, 2);
            this.X_Axis_comboBox.ContextMenuStrip = this.contextMenuStrip1;
            this.X_Axis_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.X_Axis_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.X_Axis_comboBox.Font = new System.Drawing.Font("Janna LT", 9F);
            this.X_Axis_comboBox.ForeColor = System.Drawing.Color.Black;
            this.X_Axis_comboBox.FormattingEnabled = true;
            this.X_Axis_comboBox.Items.AddRange(new object[] {
            "Project Status",
            "Fund Type",
            "Categories",
            "Parishes",
            "Donors",
            "Gender",
            "Marital Status",
            "Age",
            "New Project VS Developing",
            "Experience VS Project",
            "Profit Rate",
            "Partnership"});
            this.X_Axis_comboBox.Location = new System.Drawing.Point(785, 6);
            this.X_Axis_comboBox.Margin = new System.Windows.Forms.Padding(6);
            this.X_Axis_comboBox.Name = "X_Axis_comboBox";
            this.X_Axis_comboBox.Size = new System.Drawing.Size(271, 37);
            this.X_Axis_comboBox.TabIndex = 439;
            this.X_Axis_comboBox.SelectedIndexChanged += new System.EventHandler(this.X_Axis_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Janna LT", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label1.Location = new System.Drawing.Point(522, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 31);
            this.label1.TabIndex = 463;
            this.label1.Text = "نوع المخطط:";
            // 
            // results_dataGridView
            // 
            this.results_dataGridView.AllowUserToAddRows = false;
            this.results_dataGridView.AllowUserToDeleteRows = false;
            this.results_dataGridView.AllowUserToOrderColumns = true;
            this.results_dataGridView.AllowUserToResizeRows = false;
            this.results_dataGridView.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.results_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.results_dataGridView.BackgroundColor = System.Drawing.Color.Gray;
            this.results_dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.results_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.results_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.results_dataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.results_dataGridView.Location = new System.Drawing.Point(4, 8);
            this.results_dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.results_dataGridView.Name = "results_dataGridView";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.results_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.results_dataGridView.RowHeadersVisible = false;
            this.results_dataGridView.RowTemplate.Height = 26;
            this.results_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.results_dataGridView.Size = new System.Drawing.Size(42, 33);
            this.results_dataGridView.TabIndex = 442;
            this.results_dataGridView.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.60981F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.94186F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.99581F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.49685F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.2956F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.65828F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 326F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.Count_label, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ExportToExcel_button, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button1, 6, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 633);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1264, 48);
            this.tableLayoutPanel2.TabIndex = 415;
            // 
            // Count_label
            // 
            this.Count_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Count_label.AutoSize = true;
            this.Count_label.BackColor = System.Drawing.Color.Transparent;
            this.Count_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Count_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Count_label.Location = new System.Drawing.Point(1091, 8);
            this.Count_label.Margin = new System.Windows.Forms.Padding(0);
            this.Count_label.Name = "Count_label";
            this.Count_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Count_label.Size = new System.Drawing.Size(130, 31);
            this.Count_label.TabIndex = 476;
            this.Count_label.Text = "Count";
            this.Count_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.ExportToExcel_button.Location = new System.Drawing.Point(1225, 9);
            this.ExportToExcel_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ExportToExcel_button.MaximumSize = new System.Drawing.Size(30, 30);
            this.ExportToExcel_button.MinimumSize = new System.Drawing.Size(30, 30);
            this.ExportToExcel_button.Name = "ExportToExcel_button";
            this.ExportToExcel_button.Size = new System.Drawing.Size(30, 30);
            this.ExportToExcel_button.TabIndex = 411;
            this.ExportToExcel_button.UseVisualStyleBackColor = false;
            this.ExportToExcel_button.Click += new System.EventHandler(this.ExportToExcel_button_Click);
            this.ExportToExcel_button.MouseEnter += new System.EventHandler(this.ExportToExcel_button_MouseEnter);
            this.ExportToExcel_button.MouseLeave += new System.EventHandler(this.ExportToExcel_button_MouseLeave);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackColor = System.Drawing.Color.Maroon;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 42);
            this.button1.TabIndex = 477;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Search_panel
            // 
            this.Search_panel.BackColor = System.Drawing.Color.Transparent;
            this.Search_panel.Controls.Add(this.tableLayoutPanel3);
            this.Search_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Search_panel.Location = new System.Drawing.Point(0, 50);
            this.Search_panel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Search_panel.Name = "Search_panel";
            this.Search_panel.Size = new System.Drawing.Size(1264, 266);
            this.Search_panel.TabIndex = 416;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 12;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.048565F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.40554F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.01434F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.29616F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.07161F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.92041F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.44933F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.54327F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.20222F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.048565F));
            this.tableLayoutPanel3.ContextMenuStrip = this.contextMenuStrip1;
            this.tableLayoutPanel3.Controls.Add(this.DonorGroup_label, 6, 1);
            this.tableLayoutPanel3.Controls.Add(this.DonorGroup_comboBox, 7, 1);
            this.tableLayoutPanel3.Controls.Add(this.FundedFrom_textBox, 7, 5);
            this.tableLayoutPanel3.Controls.Add(this.FundedFrom_label, 6, 5);
            this.tableLayoutPanel3.Controls.Add(this.RequestedFrom_label, 6, 4);
            this.tableLayoutPanel3.Controls.Add(this.RequestedFrom_textBox, 7, 4);
            this.tableLayoutPanel3.Controls.Add(this.FundedTo_label, 8, 5);
            this.tableLayoutPanel3.Controls.Add(this.FundedTo_textBox, 9, 5);
            this.tableLayoutPanel3.Controls.Add(this.RequestedTo_textBox, 9, 4);
            this.tableLayoutPanel3.Controls.Add(this.RequestedTo_label, 8, 4);
            this.tableLayoutPanel3.Controls.Add(this.Funded_checkBox, 10, 5);
            this.tableLayoutPanel3.Controls.Add(this.Requested_checkBox, 10, 4);
            this.tableLayoutPanel3.Controls.Add(this.FundedDate_checkBox, 5, 5);
            this.tableLayoutPanel3.Controls.Add(this.ApplyDate_checkBox, 5, 4);
            this.tableLayoutPanel3.Controls.Add(this.FundDateTo_bcDateTimePicker, 4, 5);
            this.tableLayoutPanel3.Controls.Add(this.ApplyDateTo_bcDateTimePicker, 4, 4);
            this.tableLayoutPanel3.Controls.Add(this.FundedDateTo_label, 3, 5);
            this.tableLayoutPanel3.Controls.Add(this.ApplyDateTo_label, 3, 4);
            this.tableLayoutPanel3.Controls.Add(this.FundDateFrom_bcDateTimePicker, 2, 5);
            this.tableLayoutPanel3.Controls.Add(this.ApplyDateFrom_dateTimePicker, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.FundedDateFrom_label, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.ApplyDateFrom_label, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.ApplyDate_comboBox, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.label2, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label3, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.FundDate_comboBox, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.P_Parish_label, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.P_Parish_comboBox, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.Age_label, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.Age_comboBox, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.Gender_label, 6, 2);
            this.tableLayoutPanel3.Controls.Add(this.Gender_comboBox, 7, 2);
            this.tableLayoutPanel3.Controls.Add(this.MaritalStatus_label, 8, 2);
            this.tableLayoutPanel3.Controls.Add(this.MaritalStatus_comboBox, 9, 2);
            this.tableLayoutPanel3.Controls.Add(this.MP_Status_label, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.MP_Status_comboBox, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.Donor_label, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.Donor_comboBox, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.Street_comboBox, 9, 1);
            this.tableLayoutPanel3.Controls.Add(this.label6, 8, 1);
            this.tableLayoutPanel3.Controls.Add(this.MP_Category_label, 6, 3);
            this.tableLayoutPanel3.Controls.Add(this.MP_Category_comboBox, 7, 3);
            this.tableLayoutPanel3.Controls.Add(this.SubCategory_label, 8, 3);
            this.tableLayoutPanel3.Controls.Add(this.SubCategory_comboBox, 9, 3);
            this.tableLayoutPanel3.Controls.Add(this.Partnership_comboBox, 9, 0);
            this.tableLayoutPanel3.Controls.Add(this.Partnership_label, 8, 0);
            this.tableLayoutPanel3.Controls.Add(this.Type_comboBox, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.Type_label, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.FundType_label, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.FundType_comboBox, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.SubType_label, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.SubType_comboBox, 7, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1264, 266);
            this.tableLayoutPanel3.TabIndex = 500;
            // 
            // DonorGroup_label
            // 
            this.DonorGroup_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DonorGroup_label.AutoSize = true;
            this.DonorGroup_label.BackColor = System.Drawing.Color.Transparent;
            this.DonorGroup_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DonorGroup_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.DonorGroup_label.Location = new System.Drawing.Point(455, 50);
            this.DonorGroup_label.Margin = new System.Windows.Forms.Padding(0);
            this.DonorGroup_label.Name = "DonorGroup_label";
            this.DonorGroup_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DonorGroup_label.Size = new System.Drawing.Size(80, 31);
            this.DonorGroup_label.TabIndex = 573;
            this.DonorGroup_label.Text = "المجموعة:";
            // 
            // DonorGroup_comboBox
            // 
            this.DonorGroup_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DonorGroup_comboBox.BackColor = System.Drawing.Color.White;
            this.DonorGroup_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DonorGroup_comboBox.ForeColor = System.Drawing.Color.Black;
            this.DonorGroup_comboBox.FormattingEnabled = true;
            this.DonorGroup_comboBox.Location = new System.Drawing.Point(319, 48);
            this.DonorGroup_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DonorGroup_comboBox.Name = "DonorGroup_comboBox";
            this.DonorGroup_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DonorGroup_comboBox.Size = new System.Drawing.Size(133, 37);
            this.DonorGroup_comboBox.TabIndex = 574;
            this.DonorGroup_comboBox.Enter += new System.EventHandler(this.DonorGroup_comboBox_Enter);
            this.DonorGroup_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // FundedFrom_textBox
            // 
            this.FundedFrom_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FundedFrom_textBox.BackColor = System.Drawing.Color.White;
            this.FundedFrom_textBox.Enabled = false;
            this.FundedFrom_textBox.Font = new System.Drawing.Font("Janna LT", 9.5F);
            this.FundedFrom_textBox.Location = new System.Drawing.Point(319, 224);
            this.FundedFrom_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FundedFrom_textBox.Name = "FundedFrom_textBox";
            this.FundedFrom_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundedFrom_textBox.Size = new System.Drawing.Size(133, 37);
            this.FundedFrom_textBox.TabIndex = 554;
            this.FundedFrom_textBox.TextChanged += new System.EventHandler(this.FundedFrom_textBox_TextChanged);
            // 
            // FundedFrom_label
            // 
            this.FundedFrom_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FundedFrom_label.AutoSize = true;
            this.FundedFrom_label.BackColor = System.Drawing.Color.Transparent;
            this.FundedFrom_label.Enabled = false;
            this.FundedFrom_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundedFrom_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FundedFrom_label.Location = new System.Drawing.Point(457, 227);
            this.FundedFrom_label.Margin = new System.Windows.Forms.Padding(2);
            this.FundedFrom_label.Name = "FundedFrom_label";
            this.FundedFrom_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundedFrom_label.Size = new System.Drawing.Size(145, 31);
            this.FundedFrom_label.TabIndex = 553;
            this.FundedFrom_label.Text = "المبلغ الممول (من):";
            // 
            // RequestedFrom_label
            // 
            this.RequestedFrom_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RequestedFrom_label.AutoSize = true;
            this.RequestedFrom_label.BackColor = System.Drawing.Color.Transparent;
            this.RequestedFrom_label.Enabled = false;
            this.RequestedFrom_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RequestedFrom_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.RequestedFrom_label.Location = new System.Drawing.Point(457, 182);
            this.RequestedFrom_label.Margin = new System.Windows.Forms.Padding(2);
            this.RequestedFrom_label.Name = "RequestedFrom_label";
            this.RequestedFrom_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RequestedFrom_label.Size = new System.Drawing.Size(159, 31);
            this.RequestedFrom_label.TabIndex = 551;
            this.RequestedFrom_label.Text = "المبلغ المطلوب (من):";
            // 
            // RequestedFrom_textBox
            // 
            this.RequestedFrom_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RequestedFrom_textBox.BackColor = System.Drawing.Color.White;
            this.RequestedFrom_textBox.Enabled = false;
            this.RequestedFrom_textBox.Font = new System.Drawing.Font("Janna LT", 9.5F);
            this.RequestedFrom_textBox.Location = new System.Drawing.Point(319, 180);
            this.RequestedFrom_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RequestedFrom_textBox.Name = "RequestedFrom_textBox";
            this.RequestedFrom_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RequestedFrom_textBox.Size = new System.Drawing.Size(133, 37);
            this.RequestedFrom_textBox.TabIndex = 549;
            this.RequestedFrom_textBox.TextChanged += new System.EventHandler(this.RequestedFrom_textBox_TextChanged);
            // 
            // FundedTo_label
            // 
            this.FundedTo_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FundedTo_label.AutoSize = true;
            this.FundedTo_label.BackColor = System.Drawing.Color.Transparent;
            this.FundedTo_label.Enabled = false;
            this.FundedTo_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundedTo_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FundedTo_label.Location = new System.Drawing.Point(190, 227);
            this.FundedTo_label.Margin = new System.Windows.Forms.Padding(2);
            this.FundedTo_label.Name = "FundedTo_label";
            this.FundedTo_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundedTo_label.Size = new System.Drawing.Size(54, 31);
            this.FundedTo_label.TabIndex = 556;
            this.FundedTo_label.Text = "(إلى):";
            // 
            // FundedTo_textBox
            // 
            this.FundedTo_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FundedTo_textBox.BackColor = System.Drawing.Color.White;
            this.FundedTo_textBox.Enabled = false;
            this.FundedTo_textBox.Font = new System.Drawing.Font("Janna LT", 9.5F);
            this.FundedTo_textBox.Location = new System.Drawing.Point(55, 224);
            this.FundedTo_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FundedTo_textBox.Name = "FundedTo_textBox";
            this.FundedTo_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundedTo_textBox.Size = new System.Drawing.Size(130, 37);
            this.FundedTo_textBox.TabIndex = 555;
            this.FundedTo_textBox.TextChanged += new System.EventHandler(this.FundedTo_textBox_TextChanged);
            // 
            // RequestedTo_textBox
            // 
            this.RequestedTo_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RequestedTo_textBox.BackColor = System.Drawing.Color.White;
            this.RequestedTo_textBox.Enabled = false;
            this.RequestedTo_textBox.Font = new System.Drawing.Font("Janna LT", 9.5F);
            this.RequestedTo_textBox.Location = new System.Drawing.Point(55, 180);
            this.RequestedTo_textBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RequestedTo_textBox.Name = "RequestedTo_textBox";
            this.RequestedTo_textBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RequestedTo_textBox.Size = new System.Drawing.Size(130, 37);
            this.RequestedTo_textBox.TabIndex = 550;
            this.RequestedTo_textBox.TextChanged += new System.EventHandler(this.RequestedTo_textBox_TextChanged);
            // 
            // RequestedTo_label
            // 
            this.RequestedTo_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RequestedTo_label.AutoSize = true;
            this.RequestedTo_label.BackColor = System.Drawing.Color.Transparent;
            this.RequestedTo_label.Enabled = false;
            this.RequestedTo_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RequestedTo_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.RequestedTo_label.Location = new System.Drawing.Point(190, 182);
            this.RequestedTo_label.Margin = new System.Windows.Forms.Padding(2);
            this.RequestedTo_label.Name = "RequestedTo_label";
            this.RequestedTo_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RequestedTo_label.Size = new System.Drawing.Size(54, 31);
            this.RequestedTo_label.TabIndex = 552;
            this.RequestedTo_label.Text = "(إلى):";
            // 
            // Funded_checkBox
            // 
            this.Funded_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Funded_checkBox.AutoSize = true;
            this.Funded_checkBox.Location = new System.Drawing.Point(32, 234);
            this.Funded_checkBox.Name = "Funded_checkBox";
            this.Funded_checkBox.Size = new System.Drawing.Size(17, 17);
            this.Funded_checkBox.TabIndex = 548;
            this.Funded_checkBox.UseVisualStyleBackColor = true;
            this.Funded_checkBox.CheckedChanged += new System.EventHandler(this.Funded_checkBox_CheckedChanged);
            // 
            // Requested_checkBox
            // 
            this.Requested_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Requested_checkBox.AutoSize = true;
            this.Requested_checkBox.Location = new System.Drawing.Point(32, 189);
            this.Requested_checkBox.Name = "Requested_checkBox";
            this.Requested_checkBox.Size = new System.Drawing.Size(17, 17);
            this.Requested_checkBox.TabIndex = 547;
            this.Requested_checkBox.UseVisualStyleBackColor = true;
            this.Requested_checkBox.CheckedChanged += new System.EventHandler(this.Requested_checkBox_CheckedChanged);
            // 
            // FundedDate_checkBox
            // 
            this.FundedDate_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FundedDate_checkBox.AutoSize = true;
            this.FundedDate_checkBox.Location = new System.Drawing.Point(627, 234);
            this.FundedDate_checkBox.Name = "FundedDate_checkBox";
            this.FundedDate_checkBox.Size = new System.Drawing.Size(18, 17);
            this.FundedDate_checkBox.TabIndex = 546;
            this.FundedDate_checkBox.UseVisualStyleBackColor = true;
            this.FundedDate_checkBox.CheckedChanged += new System.EventHandler(this.FundedDate_checkBox_CheckedChanged);
            // 
            // ApplyDate_checkBox
            // 
            this.ApplyDate_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ApplyDate_checkBox.AutoSize = true;
            this.ApplyDate_checkBox.Location = new System.Drawing.Point(627, 189);
            this.ApplyDate_checkBox.Name = "ApplyDate_checkBox";
            this.ApplyDate_checkBox.Size = new System.Drawing.Size(18, 17);
            this.ApplyDate_checkBox.TabIndex = 545;
            this.ApplyDate_checkBox.UseVisualStyleBackColor = true;
            this.ApplyDate_checkBox.CheckedChanged += new System.EventHandler(this.ApplyDate_checkBox_CheckedChanged);
            // 
            // FundDateTo_bcDateTimePicker
            // 
            this.FundDateTo_bcDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FundDateTo_bcDateTimePicker.BackColor = System.Drawing.Color.Black;
            this.FundDateTo_bcDateTimePicker.CalendarForeColor = System.Drawing.Color.Black;
            this.FundDateTo_bcDateTimePicker.Checked = false;
            this.FundDateTo_bcDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.FundDateTo_bcDateTimePicker.Enabled = false;
            this.FundDateTo_bcDateTimePicker.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundDateTo_bcDateTimePicker.ForeColor = System.Drawing.Color.Black;
            this.FundDateTo_bcDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FundDateTo_bcDateTimePicker.Location = new System.Drawing.Point(651, 229);
            this.FundDateTo_bcDateTimePicker.Name = "FundDateTo_bcDateTimePicker";
            this.FundDateTo_bcDateTimePicker.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundDateTo_bcDateTimePicker.RightToLeftLayout = true;
            this.FundDateTo_bcDateTimePicker.Size = new System.Drawing.Size(153, 28);
            this.FundDateTo_bcDateTimePicker.TabIndex = 544;
            this.FundDateTo_bcDateTimePicker.ValueChanged += new System.EventHandler(this.FundedDateTo_bcDateTimePicker_ValueChanged);
            // 
            // ApplyDateTo_bcDateTimePicker
            // 
            this.ApplyDateTo_bcDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyDateTo_bcDateTimePicker.BackColor = System.Drawing.Color.Black;
            this.ApplyDateTo_bcDateTimePicker.CalendarForeColor = System.Drawing.Color.Black;
            this.ApplyDateTo_bcDateTimePicker.Checked = false;
            this.ApplyDateTo_bcDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.ApplyDateTo_bcDateTimePicker.Enabled = false;
            this.ApplyDateTo_bcDateTimePicker.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyDateTo_bcDateTimePicker.ForeColor = System.Drawing.Color.Black;
            this.ApplyDateTo_bcDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ApplyDateTo_bcDateTimePicker.Location = new System.Drawing.Point(651, 184);
            this.ApplyDateTo_bcDateTimePicker.Name = "ApplyDateTo_bcDateTimePicker";
            this.ApplyDateTo_bcDateTimePicker.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ApplyDateTo_bcDateTimePicker.RightToLeftLayout = true;
            this.ApplyDateTo_bcDateTimePicker.Size = new System.Drawing.Size(153, 28);
            this.ApplyDateTo_bcDateTimePicker.TabIndex = 539;
            this.ApplyDateTo_bcDateTimePicker.ValueChanged += new System.EventHandler(this.ApplyDateFrom_dateTimePicker_ValueChanged);
            // 
            // FundedDateTo_label
            // 
            this.FundedDateTo_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FundedDateTo_label.AutoSize = true;
            this.FundedDateTo_label.Enabled = false;
            this.FundedDateTo_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundedDateTo_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FundedDateTo_label.Location = new System.Drawing.Point(809, 227);
            this.FundedDateTo_label.Margin = new System.Windows.Forms.Padding(2);
            this.FundedDateTo_label.Name = "FundedDateTo_label";
            this.FundedDateTo_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundedDateTo_label.Size = new System.Drawing.Size(44, 31);
            this.FundedDateTo_label.TabIndex = 541;
            this.FundedDateTo_label.Text = "إلى:";
            // 
            // ApplyDateTo_label
            // 
            this.ApplyDateTo_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ApplyDateTo_label.AutoSize = true;
            this.ApplyDateTo_label.Enabled = false;
            this.ApplyDateTo_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyDateTo_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ApplyDateTo_label.Location = new System.Drawing.Point(809, 182);
            this.ApplyDateTo_label.Margin = new System.Windows.Forms.Padding(2);
            this.ApplyDateTo_label.Name = "ApplyDateTo_label";
            this.ApplyDateTo_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ApplyDateTo_label.Size = new System.Drawing.Size(44, 31);
            this.ApplyDateTo_label.TabIndex = 540;
            this.ApplyDateTo_label.Text = "إلى:";
            // 
            // FundDateFrom_bcDateTimePicker
            // 
            this.FundDateFrom_bcDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FundDateFrom_bcDateTimePicker.BackColor = System.Drawing.Color.Black;
            this.FundDateFrom_bcDateTimePicker.CalendarForeColor = System.Drawing.Color.Black;
            this.FundDateFrom_bcDateTimePicker.Checked = false;
            this.FundDateFrom_bcDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.FundDateFrom_bcDateTimePicker.Enabled = false;
            this.FundDateFrom_bcDateTimePicker.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundDateFrom_bcDateTimePicker.ForeColor = System.Drawing.Color.Black;
            this.FundDateFrom_bcDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FundDateFrom_bcDateTimePicker.Location = new System.Drawing.Point(935, 229);
            this.FundDateFrom_bcDateTimePicker.Name = "FundDateFrom_bcDateTimePicker";
            this.FundDateFrom_bcDateTimePicker.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundDateFrom_bcDateTimePicker.RightToLeftLayout = true;
            this.FundDateFrom_bcDateTimePicker.Size = new System.Drawing.Size(152, 28);
            this.FundDateFrom_bcDateTimePicker.TabIndex = 543;
            this.FundDateFrom_bcDateTimePicker.Value = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.FundDateFrom_bcDateTimePicker.ValueChanged += new System.EventHandler(this.FundedDateTo_bcDateTimePicker_ValueChanged);
            // 
            // ApplyDateFrom_dateTimePicker
            // 
            this.ApplyDateFrom_dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyDateFrom_dateTimePicker.BackColor = System.Drawing.Color.Black;
            this.ApplyDateFrom_dateTimePicker.CalendarForeColor = System.Drawing.Color.Black;
            this.ApplyDateFrom_dateTimePicker.Checked = false;
            this.ApplyDateFrom_dateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.ApplyDateFrom_dateTimePicker.Enabled = false;
            this.ApplyDateFrom_dateTimePicker.Font = new System.Drawing.Font("Avenir LT Std 45 Book", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyDateFrom_dateTimePicker.ForeColor = System.Drawing.Color.Black;
            this.ApplyDateFrom_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ApplyDateFrom_dateTimePicker.Location = new System.Drawing.Point(935, 184);
            this.ApplyDateFrom_dateTimePicker.Name = "ApplyDateFrom_dateTimePicker";
            this.ApplyDateFrom_dateTimePicker.RightToLeftLayout = true;
            this.ApplyDateFrom_dateTimePicker.Size = new System.Drawing.Size(152, 28);
            this.ApplyDateFrom_dateTimePicker.TabIndex = 537;
            this.ApplyDateFrom_dateTimePicker.Value = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.ApplyDateFrom_dateTimePicker.ValueChanged += new System.EventHandler(this.ApplyDateFrom_dateTimePicker_ValueChanged);
            // 
            // FundedDateFrom_label
            // 
            this.FundedDateFrom_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FundedDateFrom_label.AutoSize = true;
            this.FundedDateFrom_label.Enabled = false;
            this.FundedDateFrom_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundedDateFrom_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FundedDateFrom_label.Location = new System.Drawing.Point(1092, 227);
            this.FundedDateFrom_label.Margin = new System.Windows.Forms.Padding(2);
            this.FundedDateFrom_label.Name = "FundedDateFrom_label";
            this.FundedDateFrom_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundedDateFrom_label.Size = new System.Drawing.Size(136, 31);
            this.FundedDateFrom_label.TabIndex = 542;
            this.FundedDateFrom_label.Text = "تاريخ التمويل: (من)";
            // 
            // ApplyDateFrom_label
            // 
            this.ApplyDateFrom_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ApplyDateFrom_label.AutoSize = true;
            this.ApplyDateFrom_label.Enabled = false;
            this.ApplyDateFrom_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyDateFrom_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ApplyDateFrom_label.Location = new System.Drawing.Point(1092, 182);
            this.ApplyDateFrom_label.Margin = new System.Windows.Forms.Padding(2);
            this.ApplyDateFrom_label.Name = "ApplyDateFrom_label";
            this.ApplyDateFrom_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ApplyDateFrom_label.Size = new System.Drawing.Size(137, 31);
            this.ApplyDateFrom_label.TabIndex = 538;
            this.ApplyDateFrom_label.Text = "تاريخ التقديم: (من)";
            // 
            // ApplyDate_comboBox
            // 
            this.ApplyDate_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyDate_comboBox.BackColor = System.Drawing.Color.White;
            this.ApplyDate_comboBox.ContextMenuStrip = this.contextMenuStrip1;
            this.ApplyDate_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyDate_comboBox.ForeColor = System.Drawing.Color.Black;
            this.ApplyDate_comboBox.FormattingEnabled = true;
            this.ApplyDate_comboBox.Location = new System.Drawing.Point(935, 135);
            this.ApplyDate_comboBox.Name = "ApplyDate_comboBox";
            this.ApplyDate_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ApplyDate_comboBox.Size = new System.Drawing.Size(152, 37);
            this.ApplyDate_comboBox.TabIndex = 559;
            this.ApplyDate_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label2.Location = new System.Drawing.Point(1092, 138);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(102, 31);
            this.label2.TabIndex = 557;
            this.label2.Text = "سنة التقديم:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label3.Location = new System.Drawing.Point(809, 138);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(101, 31);
            this.label3.TabIndex = 558;
            this.label3.Text = "سنة التمويل:";
            // 
            // FundDate_comboBox
            // 
            this.FundDate_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FundDate_comboBox.BackColor = System.Drawing.Color.White;
            this.FundDate_comboBox.ContextMenuStrip = this.contextMenuStrip1;
            this.FundDate_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundDate_comboBox.ForeColor = System.Drawing.Color.Black;
            this.FundDate_comboBox.FormattingEnabled = true;
            this.FundDate_comboBox.Location = new System.Drawing.Point(651, 135);
            this.FundDate_comboBox.Name = "FundDate_comboBox";
            this.FundDate_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundDate_comboBox.Size = new System.Drawing.Size(153, 37);
            this.FundDate_comboBox.TabIndex = 560;
            this.FundDate_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // P_Parish_label
            // 
            this.P_Parish_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.P_Parish_label.AutoSize = true;
            this.P_Parish_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P_Parish_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.P_Parish_label.Location = new System.Drawing.Point(1090, 94);
            this.P_Parish_label.Margin = new System.Windows.Forms.Padding(0);
            this.P_Parish_label.Name = "P_Parish_label";
            this.P_Parish_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.P_Parish_label.Size = new System.Drawing.Size(68, 31);
            this.P_Parish_label.TabIndex = 466;
            this.P_Parish_label.Text = "الطائفة:";
            // 
            // P_Parish_comboBox
            // 
            this.P_Parish_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.P_Parish_comboBox.BackColor = System.Drawing.Color.White;
            this.P_Parish_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P_Parish_comboBox.ForeColor = System.Drawing.Color.Black;
            this.P_Parish_comboBox.FormattingEnabled = true;
            this.P_Parish_comboBox.Items.AddRange(new object[] {
            "Armenian Catholic",
            "Armenian Orthodox",
            "Greek Catholic",
            "Greek Orthodox",
            "Syriac Catholic",
            "Syriac Orthodox",
            "Latin",
            "Chaldean",
            "Maronite",
            "Evangelic",
            "Muslim",
            "Christians"});
            this.P_Parish_comboBox.Location = new System.Drawing.Point(935, 91);
            this.P_Parish_comboBox.Name = "P_Parish_comboBox";
            this.P_Parish_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.P_Parish_comboBox.Size = new System.Drawing.Size(152, 37);
            this.P_Parish_comboBox.TabIndex = 439;
            this.P_Parish_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // Age_label
            // 
            this.Age_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Age_label.AutoSize = true;
            this.Age_label.BackColor = System.Drawing.Color.Transparent;
            this.Age_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Age_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Age_label.Location = new System.Drawing.Point(807, 94);
            this.Age_label.Margin = new System.Windows.Forms.Padding(0);
            this.Age_label.Name = "Age_label";
            this.Age_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Age_label.Size = new System.Drawing.Size(101, 31);
            this.Age_label.TabIndex = 496;
            this.Age_label.Text = "الفئة العمرية:";
            // 
            // Age_comboBox
            // 
            this.Age_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Age_comboBox.BackColor = System.Drawing.Color.White;
            this.Age_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Age_comboBox.ForeColor = System.Drawing.Color.Black;
            this.Age_comboBox.FormattingEnabled = true;
            this.Age_comboBox.Items.AddRange(new object[] {
            "<16",
            "16-26",
            "27-35",
            "36-45",
            "46-60",
            ">60"});
            this.Age_comboBox.Location = new System.Drawing.Point(651, 92);
            this.Age_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Age_comboBox.Name = "Age_comboBox";
            this.Age_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Age_comboBox.Size = new System.Drawing.Size(153, 37);
            this.Age_comboBox.TabIndex = 495;
            this.Age_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // Gender_label
            // 
            this.Gender_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Gender_label.AutoSize = true;
            this.Gender_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gender_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Gender_label.Location = new System.Drawing.Point(455, 94);
            this.Gender_label.Margin = new System.Windows.Forms.Padding(0);
            this.Gender_label.Name = "Gender_label";
            this.Gender_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Gender_label.Size = new System.Drawing.Size(63, 31);
            this.Gender_label.TabIndex = 467;
            this.Gender_label.Text = "الجنس:";
            // 
            // Gender_comboBox
            // 
            this.Gender_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Gender_comboBox.BackColor = System.Drawing.Color.White;
            this.Gender_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gender_comboBox.ForeColor = System.Drawing.Color.Black;
            this.Gender_comboBox.FormattingEnabled = true;
            this.Gender_comboBox.Items.AddRange(new object[] {
            "ذكر",
            "أنثى"});
            this.Gender_comboBox.Location = new System.Drawing.Point(319, 92);
            this.Gender_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Gender_comboBox.Name = "Gender_comboBox";
            this.Gender_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Gender_comboBox.Size = new System.Drawing.Size(133, 37);
            this.Gender_comboBox.TabIndex = 464;
            this.Gender_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // MaritalStatus_label
            // 
            this.MaritalStatus_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.MaritalStatus_label.AutoSize = true;
            this.MaritalStatus_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaritalStatus_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MaritalStatus_label.Location = new System.Drawing.Point(188, 94);
            this.MaritalStatus_label.Margin = new System.Windows.Forms.Padding(0);
            this.MaritalStatus_label.Name = "MaritalStatus_label";
            this.MaritalStatus_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MaritalStatus_label.Size = new System.Drawing.Size(120, 31);
            this.MaritalStatus_label.TabIndex = 470;
            this.MaritalStatus_label.Text = "الحالة الاجتماعية:";
            // 
            // MaritalStatus_comboBox
            // 
            this.MaritalStatus_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MaritalStatus_comboBox.BackColor = System.Drawing.Color.White;
            this.MaritalStatus_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaritalStatus_comboBox.ForeColor = System.Drawing.Color.Black;
            this.MaritalStatus_comboBox.FormattingEnabled = true;
            this.MaritalStatus_comboBox.Items.AddRange(new object[] {
            "متزوج/ة",
            "عازب/ة",
            "أرمل/ة",
            "مطلق/ة",
            "مخطوب/ة",
            "الكل"});
            this.MaritalStatus_comboBox.Location = new System.Drawing.Point(55, 92);
            this.MaritalStatus_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaritalStatus_comboBox.Name = "MaritalStatus_comboBox";
            this.MaritalStatus_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MaritalStatus_comboBox.Size = new System.Drawing.Size(130, 37);
            this.MaritalStatus_comboBox.TabIndex = 463;
            this.MaritalStatus_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // MP_Status_label
            // 
            this.MP_Status_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.MP_Status_label.AutoSize = true;
            this.MP_Status_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MP_Status_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MP_Status_label.Location = new System.Drawing.Point(1090, 50);
            this.MP_Status_label.Margin = new System.Windows.Forms.Padding(0);
            this.MP_Status_label.Name = "MP_Status_label";
            this.MP_Status_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MP_Status_label.Size = new System.Drawing.Size(104, 31);
            this.MP_Status_label.TabIndex = 441;
            this.MP_Status_label.Text = "حالة المشروع:";
            // 
            // MP_Status_comboBox
            // 
            this.MP_Status_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MP_Status_comboBox.BackColor = System.Drawing.Color.White;
            this.MP_Status_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MP_Status_comboBox.ForeColor = System.Drawing.Color.Black;
            this.MP_Status_comboBox.FormattingEnabled = true;
            this.MP_Status_comboBox.Items.AddRange(new object[] {
            "مقبول",
            "ممول",
            "مرفوض",
            "مؤجل",
            "بالانتظار",
            "منتهي",
            "منسحب",
            "ملغى",
            "ممول+منتهي",
            "ممول+منتهي+ملغى"});
            this.MP_Status_comboBox.Location = new System.Drawing.Point(935, 48);
            this.MP_Status_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MP_Status_comboBox.Name = "MP_Status_comboBox";
            this.MP_Status_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MP_Status_comboBox.Size = new System.Drawing.Size(152, 37);
            this.MP_Status_comboBox.TabIndex = 442;
            this.MP_Status_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // Donor_label
            // 
            this.Donor_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Donor_label.AutoSize = true;
            this.Donor_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Donor_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Donor_label.Location = new System.Drawing.Point(807, 50);
            this.Donor_label.Margin = new System.Windows.Forms.Padding(0);
            this.Donor_label.Name = "Donor_label";
            this.Donor_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Donor_label.Size = new System.Drawing.Size(107, 31);
            this.Donor_label.TabIndex = 468;
            this.Donor_label.Text = "الجهة الممولة:";
            // 
            // Donor_comboBox
            // 
            this.Donor_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Donor_comboBox.BackColor = System.Drawing.Color.White;
            this.Donor_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Donor_comboBox.ForeColor = System.Drawing.Color.Black;
            this.Donor_comboBox.FormattingEnabled = true;
            this.Donor_comboBox.Location = new System.Drawing.Point(651, 48);
            this.Donor_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Donor_comboBox.Name = "Donor_comboBox";
            this.Donor_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Donor_comboBox.Size = new System.Drawing.Size(153, 37);
            this.Donor_comboBox.TabIndex = 443;
            this.Donor_comboBox.SelectedValueChanged += new System.EventHandler(this.Donor_comboBox_SelectedValueChanged);
            this.Donor_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // Street_comboBox
            // 
            this.Street_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Street_comboBox.BackColor = System.Drawing.Color.White;
            this.Street_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Street_comboBox.ForeColor = System.Drawing.Color.Black;
            this.Street_comboBox.FormattingEnabled = true;
            this.Street_comboBox.Location = new System.Drawing.Point(55, 47);
            this.Street_comboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Street_comboBox.Name = "Street_comboBox";
            this.Street_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Street_comboBox.Size = new System.Drawing.Size(130, 37);
            this.Street_comboBox.TabIndex = 574;
            this.Street_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label6.Location = new System.Drawing.Point(188, 50);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(126, 31);
            this.label6.TabIndex = 573;
            this.label6.Text = "منطقة المشروع:";
            // 
            // MP_Category_label
            // 
            this.MP_Category_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.MP_Category_label.AutoSize = true;
            this.MP_Category_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MP_Category_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MP_Category_label.Location = new System.Drawing.Point(455, 138);
            this.MP_Category_label.Margin = new System.Windows.Forms.Padding(0);
            this.MP_Category_label.Name = "MP_Category_label";
            this.MP_Category_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MP_Category_label.Size = new System.Drawing.Size(115, 31);
            this.MP_Category_label.TabIndex = 465;
            this.MP_Category_label.Text = "صنف المشروع:";
            // 
            // MP_Category_comboBox
            // 
            this.MP_Category_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MP_Category_comboBox.BackColor = System.Drawing.Color.White;
            this.MP_Category_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MP_Category_comboBox.ForeColor = System.Drawing.Color.Black;
            this.MP_Category_comboBox.FormattingEnabled = true;
            this.MP_Category_comboBox.Location = new System.Drawing.Point(319, 136);
            this.MP_Category_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MP_Category_comboBox.Name = "MP_Category_comboBox";
            this.MP_Category_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MP_Category_comboBox.Size = new System.Drawing.Size(133, 37);
            this.MP_Category_comboBox.TabIndex = 461;
            this.MP_Category_comboBox.SelectedValueChanged += new System.EventHandler(this.MP_Category_comboBox_SelectedValueChanged);
            this.MP_Category_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // SubCategory_label
            // 
            this.SubCategory_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SubCategory_label.AutoSize = true;
            this.SubCategory_label.BackColor = System.Drawing.Color.Transparent;
            this.SubCategory_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubCategory_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.SubCategory_label.Location = new System.Drawing.Point(188, 138);
            this.SubCategory_label.Margin = new System.Windows.Forms.Padding(0);
            this.SubCategory_label.Name = "SubCategory_label";
            this.SubCategory_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SubCategory_label.Size = new System.Drawing.Size(61, 31);
            this.SubCategory_label.TabIndex = 569;
            this.SubCategory_label.Text = "المهنة:";
            // 
            // SubCategory_comboBox
            // 
            this.SubCategory_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SubCategory_comboBox.BackColor = System.Drawing.Color.White;
            this.SubCategory_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubCategory_comboBox.ForeColor = System.Drawing.Color.Black;
            this.SubCategory_comboBox.FormattingEnabled = true;
            this.SubCategory_comboBox.Location = new System.Drawing.Point(55, 136);
            this.SubCategory_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SubCategory_comboBox.Name = "SubCategory_comboBox";
            this.SubCategory_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SubCategory_comboBox.Size = new System.Drawing.Size(130, 37);
            this.SubCategory_comboBox.TabIndex = 570;
            this.SubCategory_comboBox.Enter += new System.EventHandler(this.SubCategory_comboBox_Enter);
            // 
            // Partnership_comboBox
            // 
            this.Partnership_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Partnership_comboBox.BackColor = System.Drawing.Color.White;
            this.Partnership_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Partnership_comboBox.ForeColor = System.Drawing.Color.Black;
            this.Partnership_comboBox.FormattingEnabled = true;
            this.Partnership_comboBox.Items.AddRange(new object[] {
            "فردي",
            "شراكة",
            "الكل"});
            this.Partnership_comboBox.Location = new System.Drawing.Point(55, 4);
            this.Partnership_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Partnership_comboBox.Name = "Partnership_comboBox";
            this.Partnership_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Partnership_comboBox.Size = new System.Drawing.Size(130, 37);
            this.Partnership_comboBox.TabIndex = 572;
            this.Partnership_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // Partnership_label
            // 
            this.Partnership_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Partnership_label.AutoSize = true;
            this.Partnership_label.BackColor = System.Drawing.Color.Transparent;
            this.Partnership_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Partnership_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Partnership_label.Location = new System.Drawing.Point(188, 6);
            this.Partnership_label.Margin = new System.Windows.Forms.Padding(0);
            this.Partnership_label.Name = "Partnership_label";
            this.Partnership_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Partnership_label.Size = new System.Drawing.Size(117, 31);
            this.Partnership_label.TabIndex = 571;
            this.Partnership_label.Text = "ملكية المشروع:";
            // 
            // Type_comboBox
            // 
            this.Type_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Type_comboBox.BackColor = System.Drawing.Color.White;
            this.Type_comboBox.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Type_comboBox.ForeColor = System.Drawing.Color.Black;
            this.Type_comboBox.FormattingEnabled = true;
            this.Type_comboBox.Items.AddRange(new object[] {
            "Loan",
            "Grant"});
            this.Type_comboBox.Location = new System.Drawing.Point(651, 4);
            this.Type_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Type_comboBox.Name = "Type_comboBox";
            this.Type_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Type_comboBox.Size = new System.Drawing.Size(153, 37);
            this.Type_comboBox.TabIndex = 493;
            this.Type_comboBox.SelectedValueChanged += new System.EventHandler(this.Type_comboBox_SelectedValueChanged);
            this.Type_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // Type_label
            // 
            this.Type_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Type_label.AutoSize = true;
            this.Type_label.BackColor = System.Drawing.Color.Transparent;
            this.Type_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Type_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Type_label.Location = new System.Drawing.Point(807, 6);
            this.Type_label.Margin = new System.Windows.Forms.Padding(0);
            this.Type_label.Name = "Type_label";
            this.Type_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Type_label.Size = new System.Drawing.Size(102, 31);
            this.Type_label.TabIndex = 494;
            this.Type_label.Text = "نوع المشروع:";
            // 
            // FundType_label
            // 
            this.FundType_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FundType_label.AutoSize = true;
            this.FundType_label.BackColor = System.Drawing.Color.Transparent;
            this.FundType_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundType_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FundType_label.Location = new System.Drawing.Point(1090, 6);
            this.FundType_label.Margin = new System.Windows.Forms.Padding(0);
            this.FundType_label.Name = "FundType_label";
            this.FundType_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundType_label.Size = new System.Drawing.Size(95, 31);
            this.FundType_label.TabIndex = 581;
            this.FundType_label.Text = "نوع التمويل:";
            // 
            // FundType_comboBox
            // 
            this.FundType_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FundType_comboBox.BackColor = System.Drawing.Color.White;
            this.FundType_comboBox.Font = new System.Drawing.Font("Janna LT", 10F);
            this.FundType_comboBox.ForeColor = System.Drawing.Color.Black;
            this.FundType_comboBox.FormattingEnabled = true;
            this.FundType_comboBox.Location = new System.Drawing.Point(935, 4);
            this.FundType_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FundType_comboBox.Name = "FundType_comboBox";
            this.FundType_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FundType_comboBox.Size = new System.Drawing.Size(152, 39);
            this.FundType_comboBox.TabIndex = 582;
            this.FundType_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // SubType_label
            // 
            this.SubType_label.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SubType_label.AutoSize = true;
            this.SubType_label.BackColor = System.Drawing.Color.Transparent;
            this.SubType_label.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubType_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.SubType_label.Location = new System.Drawing.Point(455, 6);
            this.SubType_label.Margin = new System.Windows.Forms.Padding(0);
            this.SubType_label.Name = "SubType_label";
            this.SubType_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SubType_label.Size = new System.Drawing.Size(152, 31);
            this.SubType_label.TabIndex = 583;
            this.SubType_label.Text = "تفصيل نوع المشروع:";
            // 
            // SubType_comboBox
            // 
            this.SubType_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SubType_comboBox.Font = new System.Drawing.Font("Janna LT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubType_comboBox.FormattingEnabled = true;
            this.SubType_comboBox.Location = new System.Drawing.Point(319, 4);
            this.SubType_comboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SubType_comboBox.Name = "SubType_comboBox";
            this.SubType_comboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SubType_comboBox.Size = new System.Drawing.Size(133, 39);
            this.SubType_comboBox.TabIndex = 580;
            this.SubType_comboBox.Leave += new System.EventHandler(this.MP_Category_comboBox_Leave);
            // 
            // Statistics_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.Search_panel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Statistics_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistics_Form";
            this.Load += new System.EventHandler(this.Statistics_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.results_dataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.Search_panel.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox X_Axis_comboBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button AdvancedSearch_button;
        private System.Windows.Forms.DataGridView results_dataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button ExportToExcel_button;
        private System.Windows.Forms.Panel Search_panel;
        private System.Windows.Forms.ComboBox Donor_comboBox;
        private System.Windows.Forms.ComboBox MP_Status_comboBox;
        private System.Windows.Forms.ComboBox MP_Category_comboBox;
        private System.Windows.Forms.ComboBox P_Parish_comboBox;
        private System.Windows.Forms.Label MP_Category_label;
        private System.Windows.Forms.Label P_Parish_label;
        private System.Windows.Forms.Label MP_Status_label;
        private System.Windows.Forms.Label MaritalStatus_label;
        private System.Windows.Forms.Label Donor_label;
        private System.Windows.Forms.Label Gender_label;
        private System.Windows.Forms.ComboBox Gender_comboBox;
        private System.Windows.Forms.ComboBox MaritalStatus_comboBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label Type_label;
        private System.Windows.Forms.ComboBox Type_comboBox;
        private System.Windows.Forms.ComboBox Age_comboBox;
        private System.Windows.Forms.Label Age_label;
        private System.Windows.Forms.Label Count_label;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllFiltersToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem showDetailsInSearchToolStripMenuItem;
        private Classes.BCDateTimePicker ApplyDateFrom_dateTimePicker;
        private System.Windows.Forms.Label ApplyDateFrom_label;
        private Classes.BCDateTimePicker ApplyDateTo_bcDateTimePicker;
        private System.Windows.Forms.Label ApplyDateTo_label;
        private System.Windows.Forms.Label FundedDateTo_label;
        private System.Windows.Forms.Label FundedDateFrom_label;
        private Classes.BCDateTimePicker FundDateTo_bcDateTimePicker;
        private Classes.BCDateTimePicker FundDateFrom_bcDateTimePicker;
        private System.Windows.Forms.CheckBox ApplyDate_checkBox;
        private System.Windows.Forms.CheckBox FundedDate_checkBox;
        private System.Windows.Forms.CheckBox Requested_checkBox;
        private System.Windows.Forms.CheckBox Funded_checkBox;
        private System.Windows.Forms.TextBox FundedTo_textBox;
        private System.Windows.Forms.Label RequestedTo_label;
        private System.Windows.Forms.TextBox FundedFrom_textBox;
        private System.Windows.Forms.Label FundedFrom_label;
        private System.Windows.Forms.Label RequestedFrom_label;
        private System.Windows.Forms.Label FundedTo_label;
        private System.Windows.Forms.TextBox RequestedFrom_textBox;
        private System.Windows.Forms.TextBox RequestedTo_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ApplyDate_comboBox;
        private System.Windows.Forms.ComboBox FundDate_comboBox;
        private System.Windows.Forms.Label SubCategory_label;
        private System.Windows.Forms.ComboBox Partnership_comboBox;
        private System.Windows.Forms.ComboBox SubCategory_comboBox;
        private System.Windows.Forms.Label Partnership_label;
        private System.Windows.Forms.ComboBox Street_comboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label DonorGroup_label;
        private System.Windows.Forms.ComboBox DonorGroup_comboBox;
        private System.Windows.Forms.Label FundType_label;
        private System.Windows.Forms.ComboBox FundType_comboBox;
        private System.Windows.Forms.Label SubType_label;
        private System.Windows.Forms.ComboBox SubType_comboBox;
    }
}