namespace AIMS.Views.Product
{
    partial class DVDDetailsView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.flpNavBar = new System.Windows.Forms.FlowLayoutPanel();
            this.quantityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.placeOrderButton = new System.Windows.Forms.Button();
            this.addToCartButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblStudio = new System.Windows.Forms.Label();
            this.lblRuntime = new System.Windows.Forms.Label();
            this.lblDics_type = new System.Windows.Forms.Label();
            this.lblRelease_date = new System.Windows.Forms.Label();
            this.lblDirector = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.productImage = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flpNavBar);
            this.panel1.Controls.Add(this.quantityNumericUpDown);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.placeOrderButton);
            this.panel1.Controls.Add(this.addToCartButton);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.lblPrice);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.productImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 604);
            this.panel1.TabIndex = 0;
            // 
            // flpNavBar
            // 
            this.flpNavBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(27)))), ((int)(((byte)(7)))));
            this.flpNavBar.Location = new System.Drawing.Point(-1, -1);
            this.flpNavBar.Name = "flpNavBar";
            this.flpNavBar.Size = new System.Drawing.Size(1008, 50);
            this.flpNavBar.TabIndex = 18;
            // 
            // quantityNumericUpDown
            // 
            this.quantityNumericUpDown.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantityNumericUpDown.Location = new System.Drawing.Point(605, 453);
            this.quantityNumericUpDown.Name = "quantityNumericUpDown";
            this.quantityNumericUpDown.Size = new System.Drawing.Size(120, 33);
            this.quantityNumericUpDown.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(479, 453);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 30);
            this.label5.TabIndex = 15;
            this.label5.Text = "SỐ LƯỢNG";
            // 
            // placeOrderButton
            // 
            this.placeOrderButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.placeOrderButton.FlatAppearance.BorderSize = 0;
            this.placeOrderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.placeOrderButton.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold);
            this.placeOrderButton.Location = new System.Drawing.Point(710, 511);
            this.placeOrderButton.Name = "placeOrderButton";
            this.placeOrderButton.Size = new System.Drawing.Size(202, 65);
            this.placeOrderButton.TabIndex = 16;
            this.placeOrderButton.Text = "ĐẶT HÀNG";
            this.placeOrderButton.UseVisualStyleBackColor = false;
            this.placeOrderButton.Click += new System.EventHandler(this.placeOrderButton_Click);
            // 
            // addToCartButton
            // 
            this.addToCartButton.BackColor = System.Drawing.Color.Tomato;
            this.addToCartButton.FlatAppearance.BorderSize = 0;
            this.addToCartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addToCartButton.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addToCartButton.Image = global::AIMS.Properties.Resources.add_cart;
            this.addToCartButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addToCartButton.Location = new System.Drawing.Point(475, 511);
            this.addToCartButton.Name = "addToCartButton";
            this.addToCartButton.Size = new System.Drawing.Size(216, 65);
            this.addToCartButton.TabIndex = 14;
            this.addToCartButton.Text = "THÊM VÀO GIỎ";
            this.addToCartButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addToCartButton.UseVisualStyleBackColor = false;
            this.addToCartButton.Click += new System.EventHandler(this.addToCartButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSubtitle);
            this.groupBox1.Controls.Add(this.lblStudio);
            this.groupBox1.Controls.Add(this.lblRuntime);
            this.groupBox1.Controls.Add(this.lblDics_type);
            this.groupBox1.Controls.Add(this.lblRelease_date);
            this.groupBox1.Controls.Add(this.lblDirector);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(475, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 310);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chi tiết";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Location = new System.Drawing.Point(15, 229);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(478, 30);
            this.lblSubtitle.TabIndex = 5;
            this.lblSubtitle.Text = "Phụ đề:";
            // 
            // lblStudio
            // 
            this.lblStudio.Location = new System.Drawing.Point(15, 69);
            this.lblStudio.Name = "lblStudio";
            this.lblStudio.Size = new System.Drawing.Size(478, 30);
            this.lblStudio.TabIndex = 4;
            this.lblStudio.Text = "Studio:";
            // 
            // lblRuntime
            // 
            this.lblRuntime.Location = new System.Drawing.Point(15, 189);
            this.lblRuntime.Name = "lblRuntime";
            this.lblRuntime.Size = new System.Drawing.Size(478, 30);
            this.lblRuntime.TabIndex = 3;
            this.lblRuntime.Text = "Thời lượng:";
            // 
            // lblDics_type
            // 
            this.lblDics_type.Location = new System.Drawing.Point(15, 149);
            this.lblDics_type.Name = "lblDics_type";
            this.lblDics_type.Size = new System.Drawing.Size(478, 30);
            this.lblDics_type.TabIndex = 2;
            this.lblDics_type.Text = "Loại đĩa:";
            // 
            // lblRelease_date
            // 
            this.lblRelease_date.Location = new System.Drawing.Point(15, 109);
            this.lblRelease_date.Name = "lblRelease_date";
            this.lblRelease_date.Size = new System.Drawing.Size(478, 30);
            this.lblRelease_date.TabIndex = 1;
            this.lblRelease_date.Text = "Ngày phát hành:";
            // 
            // lblDirector
            // 
            this.lblDirector.Location = new System.Drawing.Point(15, 29);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Size = new System.Drawing.Size(478, 30);
            this.lblDirector.TabIndex = 0;
            this.lblDirector.Text = "Đạo diễn:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.Red;
            this.lblPrice.Location = new System.Drawing.Point(479, 102);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(100, 25);
            this.lblPrice.TabIndex = 12;
            this.lblPrice.Text = "500.000 đ";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(479, 61);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(140, 30);
            this.lblTitle.TabIndex = 11;
            this.lblTitle.Text = "Product Title";
            // 
            // productImage
            // 
            this.productImage.Location = new System.Drawing.Point(34, 78);
            this.productImage.Name = "productImage";
            this.productImage.Size = new System.Drawing.Size(425, 498);
            this.productImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.productImage.TabIndex = 10;
            this.productImage.TabStop = false;
            // 
            // DVDDetailsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "DVDDetailsView";
            this.Size = new System.Drawing.Size(1007, 604);
            this.Load += new System.EventHandler(this.ProductDetailsView_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.productImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flpNavBar;
        private System.Windows.Forms.NumericUpDown quantityNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button placeOrderButton;
        private System.Windows.Forms.Button addToCartButton;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lblSubtitle;
        public System.Windows.Forms.Label lblStudio;
        public System.Windows.Forms.Label lblRuntime;
        public System.Windows.Forms.Label lblDics_type;
        public System.Windows.Forms.Label lblRelease_date;
        public System.Windows.Forms.Label lblDirector;
        public System.Windows.Forms.Label lblPrice;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.PictureBox productImage;
    }
}
