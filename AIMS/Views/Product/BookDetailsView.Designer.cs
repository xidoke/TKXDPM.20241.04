namespace AIMS.Views.Product
{
    partial class BookDetailsView
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
            this.lblPageNumber = new System.Windows.Forms.Label();
            this.lblPublish_date = new System.Windows.Forms.Label();
            this.lblCoverType = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblPublisher = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
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
            this.flpNavBar.Location = new System.Drawing.Point(-1, -3);
            this.flpNavBar.Name = "flpNavBar";
            this.flpNavBar.Size = new System.Drawing.Size(1008, 50);
            this.flpNavBar.TabIndex = 9;
            this.flpNavBar.Paint += new System.Windows.Forms.PaintEventHandler(this.flpNavBar_Paint);
            // 
            // quantityNumericUpDown
            // 
            this.quantityNumericUpDown.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantityNumericUpDown.Location = new System.Drawing.Point(605, 451);
            this.quantityNumericUpDown.Name = "quantityNumericUpDown";
            this.quantityNumericUpDown.Size = new System.Drawing.Size(120, 33);
            this.quantityNumericUpDown.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(479, 451);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 30);
            this.label5.TabIndex = 5;
            this.label5.Text = "SỐ LƯỢNG";
            // 
            // placeOrderButton
            // 
            this.placeOrderButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.placeOrderButton.FlatAppearance.BorderSize = 0;
            this.placeOrderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.placeOrderButton.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold);
            this.placeOrderButton.Location = new System.Drawing.Point(710, 509);
            this.placeOrderButton.Name = "placeOrderButton";
            this.placeOrderButton.Size = new System.Drawing.Size(202, 65);
            this.placeOrderButton.TabIndex = 5;
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
            this.addToCartButton.Location = new System.Drawing.Point(475, 509);
            this.addToCartButton.Name = "addToCartButton";
            this.addToCartButton.Size = new System.Drawing.Size(216, 65);
            this.addToCartButton.TabIndex = 4;
            this.addToCartButton.Text = "THÊM VÀO GIỎ";
            this.addToCartButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addToCartButton.UseVisualStyleBackColor = false;
            this.addToCartButton.Click += new System.EventHandler(this.addToCartButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPageNumber);
            this.groupBox1.Controls.Add(this.lblPublish_date);
            this.groupBox1.Controls.Add(this.lblCoverType);
            this.groupBox1.Controls.Add(this.lblLanguage);
            this.groupBox1.Controls.Add(this.lblPublisher);
            this.groupBox1.Controls.Add(this.lblAuthor);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(475, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 310);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chi tiết";
            // 
            // lblPageNumber
            // 
            this.lblPageNumber.Location = new System.Drawing.Point(15, 229);
            this.lblPageNumber.Name = "lblPageNumber";
            this.lblPageNumber.Size = new System.Drawing.Size(478, 30);
            this.lblPageNumber.TabIndex = 5;
            this.lblPageNumber.Text = "Số trang:";
            // 
            // lblPublish_date
            // 
            this.lblPublish_date.Location = new System.Drawing.Point(15, 69);
            this.lblPublish_date.Name = "lblPublish_date";
            this.lblPublish_date.Size = new System.Drawing.Size(478, 30);
            this.lblPublish_date.TabIndex = 4;
            this.lblPublish_date.Text = "Ngày xuất bản:";
            // 
            // lblCoverType
            // 
            this.lblCoverType.Location = new System.Drawing.Point(15, 189);
            this.lblCoverType.Name = "lblCoverType";
            this.lblCoverType.Size = new System.Drawing.Size(478, 30);
            this.lblCoverType.TabIndex = 3;
            this.lblCoverType.Text = "Loại bìa:";
            // 
            // lblLanguage
            // 
            this.lblLanguage.Location = new System.Drawing.Point(15, 149);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(478, 30);
            this.lblLanguage.TabIndex = 2;
            this.lblLanguage.Text = "Ngôn ngữ:";
            // 
            // lblPublisher
            // 
            this.lblPublisher.Location = new System.Drawing.Point(15, 109);
            this.lblPublisher.Name = "lblPublisher";
            this.lblPublisher.Size = new System.Drawing.Size(478, 30);
            this.lblPublisher.TabIndex = 1;
            this.lblPublisher.Text = "Nhà xuất bản:";
            // 
            // lblAuthor
            // 
            this.lblAuthor.Location = new System.Drawing.Point(15, 29);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(478, 30);
            this.lblAuthor.TabIndex = 0;
            this.lblAuthor.Text = "Tác giả:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.Red;
            this.lblPrice.Location = new System.Drawing.Point(479, 100);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(100, 25);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "500.000 đ";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(479, 59);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(140, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Product Title";
            // 
            // productImage
            // 
            this.productImage.Location = new System.Drawing.Point(34, 76);
            this.productImage.Name = "productImage";
            this.productImage.Size = new System.Drawing.Size(425, 498);
            this.productImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.productImage.TabIndex = 0;
            this.productImage.TabStop = false;
            // 
            // BookDetailsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "BookDetailsView";
            this.Size = new System.Drawing.Size(1007, 604);
            this.Load += new System.EventHandler(this.BookDetailsView_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.productImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lblPrice;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.PictureBox productImage;
        public System.Windows.Forms.Label lblPublish_date;
        public System.Windows.Forms.Label lblCoverType;
        public System.Windows.Forms.Label lblLanguage;
        public System.Windows.Forms.Label lblPublisher;
        public System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Button placeOrderButton;
        private System.Windows.Forms.Button addToCartButton;
        private System.Windows.Forms.NumericUpDown quantityNumericUpDown;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label lblPageNumber;
        private System.Windows.Forms.FlowLayoutPanel flpNavBar;
    }
}
