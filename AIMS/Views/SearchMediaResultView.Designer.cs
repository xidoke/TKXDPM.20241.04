namespace AIMS.Views
{
    partial class SearchMediaResultView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpSearchResults = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBoxFilterbyCategory = new System.Windows.Forms.ComboBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSortbyPrice = new System.Windows.Forms.ComboBox();
            this.txtPriceFrom = new System.Windows.Forms.TextBox();
            this.txtPriceTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonShowSearchResults = new System.Windows.Forms.Button();
            this.flpNavBar = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpSearchResults
            // 
            this.flpSearchResults.Location = new System.Drawing.Point(0, 139);
            this.flpSearchResults.Name = "flpSearchResults";
            this.flpSearchResults.Size = new System.Drawing.Size(1007, 465);
            this.flpSearchResults.TabIndex = 0;
            // 
            // comboBoxFilterbyCategory
            // 
            this.comboBoxFilterbyCategory.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFilterbyCategory.FormattingEnabled = true;
            this.comboBoxFilterbyCategory.Items.AddRange(new object[] {
            "DVD",
            "CD",
            "Book",
            "Tất cả"});
            this.comboBoxFilterbyCategory.Location = new System.Drawing.Point(6, 100);
            this.comboBoxFilterbyCategory.Name = "comboBoxFilterbyCategory";
            this.comboBoxFilterbyCategory.Size = new System.Drawing.Size(121, 33);
            this.comboBoxFilterbyCategory.TabIndex = 1;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.BackColor = System.Drawing.Color.Transparent;
            this.labelCategory.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCategory.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCategory.Location = new System.Drawing.Point(4, 85);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(66, 13);
            this.labelCategory.TabIndex = 2;
            this.labelCategory.Text = "DANH MỤC";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(619, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "SẮP XẾP THEO GIÁ";
            // 
            // comboBoxSortbyPrice
            // 
            this.comboBoxSortbyPrice.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSortbyPrice.FormattingEnabled = true;
            this.comboBoxSortbyPrice.Items.AddRange(new object[] {
            "Từ cao đến thấp",
            "Từ thấp đến cao"});
            this.comboBoxSortbyPrice.Location = new System.Drawing.Point(619, 100);
            this.comboBoxSortbyPrice.Name = "comboBoxSortbyPrice";
            this.comboBoxSortbyPrice.Size = new System.Drawing.Size(172, 33);
            this.comboBoxSortbyPrice.TabIndex = 3;
            // 
            // txtPriceFrom
            // 
            this.txtPriceFrom.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtPriceFrom.Location = new System.Drawing.Point(327, 100);
            this.txtPriceFrom.Name = "txtPriceFrom";
            this.txtPriceFrom.Size = new System.Drawing.Size(100, 33);
            this.txtPriceFrom.TabIndex = 5;
            // 
            // txtPriceTo
            // 
            this.txtPriceTo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtPriceTo.Location = new System.Drawing.Point(459, 100);
            this.txtPriceTo.Name = "txtPriceTo";
            this.txtPriceTo.Size = new System.Drawing.Size(100, 33);
            this.txtPriceTo.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(433, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(238, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "MỨC GIÁ";
            // 
            // buttonShowSearchResults
            // 
            this.buttonShowSearchResults.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowSearchResults.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonShowSearchResults.Location = new System.Drawing.Point(888, 100);
            this.buttonShowSearchResults.Name = "buttonShowSearchResults";
            this.buttonShowSearchResults.Size = new System.Drawing.Size(116, 33);
            this.buttonShowSearchResults.TabIndex = 9;
            this.buttonShowSearchResults.Text = "TÌM KIẾM";
            this.buttonShowSearchResults.UseVisualStyleBackColor = true;
            this.buttonShowSearchResults.Click += new System.EventHandler(this.buttonShowSearchResults_Click);
            // 
            // flpNavBar
            // 
            this.flpNavBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(27)))), ((int)(((byte)(7)))));
            this.flpNavBar.Location = new System.Drawing.Point(0, -2);
            this.flpNavBar.Name = "flpNavBar";
            this.flpNavBar.Size = new System.Drawing.Size(1008, 50);
            this.flpNavBar.TabIndex = 10;
            // 
            // SearchMediaResultView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.flpNavBar);
            this.Controls.Add(this.buttonShowSearchResults);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPriceTo);
            this.Controls.Add(this.txtPriceFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSortbyPrice);
            this.Controls.Add(this.labelCategory);
            this.Controls.Add(this.comboBoxFilterbyCategory);
            this.Controls.Add(this.flpSearchResults);
            this.Name = "SearchMediaResultView";
            this.Size = new System.Drawing.Size(1007, 604);
            this.Load += new System.EventHandler(this.SearchMediaResultView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpSearchResults;
        private System.Windows.Forms.ComboBox comboBoxFilterbyCategory;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxSortbyPrice;
        private System.Windows.Forms.TextBox txtPriceFrom;
        private System.Windows.Forms.TextBox txtPriceTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonShowSearchResults;
        private System.Windows.Forms.FlowLayoutPanel flpNavBar;
    }
}
