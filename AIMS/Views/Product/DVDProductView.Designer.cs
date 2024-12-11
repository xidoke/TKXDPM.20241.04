namespace AIMS.Views.Product
{
    partial class DVDProductView
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
            this.flpDVD_Product = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.sortByPrice = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.flpNavBar = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpDVD_Product
            // 
            this.flpDVD_Product.Location = new System.Drawing.Point(0, 118);
            this.flpDVD_Product.Name = "flpDVD_Product";
            this.flpDVD_Product.Size = new System.Drawing.Size(1007, 486);
            this.flpDVD_Product.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tìm kiếm";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(80, 76);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(171, 20);
            this.searchBox.TabIndex = 2;
            // 
            // sortByPrice
            // 
            this.sortByPrice.FormattingEnabled = true;
            this.sortByPrice.Items.AddRange(new object[] {
            "Thấp đến cao",
            "Cao đến thấp"});
            this.sortByPrice.Location = new System.Drawing.Point(363, 75);
            this.sortByPrice.Name = "sortByPrice";
            this.sortByPrice.Size = new System.Drawing.Size(121, 21);
            this.sortByPrice.TabIndex = 3;
            this.sortByPrice.SelectedIndexChanged += new System.EventHandler(this.sortByPrice_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(322, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Giá:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(257, 76);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(59, 23);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Tìm kiếm";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 25);
            this.label3.TabIndex = 18;
            this.label3.Text = "DANH MỤC DVD";
            // 
            // flpNavBar
            // 
            this.flpNavBar.BackColor = System.Drawing.Color.Transparent;
            this.flpNavBar.Location = new System.Drawing.Point(-9, -3);
            this.flpNavBar.Name = "flpNavBar";
            this.flpNavBar.Size = new System.Drawing.Size(1016, 50);
            this.flpNavBar.TabIndex = 19;
            // 
            // DVDProductView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flpNavBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sortByPrice);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flpDVD_Product);
            this.Name = "DVDProductView";
            this.Size = new System.Drawing.Size(1007, 604);
            this.Load += new System.EventHandler(this.DVDProductView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel flpDVD_Product;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ComboBox sortByPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flpNavBar;
    }
}
