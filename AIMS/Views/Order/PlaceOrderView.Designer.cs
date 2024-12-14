namespace AIMS.Views.Order
{
    partial class PlaceOrderView
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNotEnoughNotification = new System.Windows.Forms.Label();
            this.flpNavBar = new System.Windows.Forms.FlowLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblPricewithVAT = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPayment = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveDeliveryInfo = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDeliveryTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.checkBoxRushOrder = new System.Windows.Forms.CheckBox();
            this.checkBoxNormalOrder = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxWard = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxDistrict = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxCity = new System.Windows.Forms.ComboBox();
            this.txtAddress = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhoneNumberOfRecipient = new System.Windows.Forms.MaskedTextBox();
            this.txtNameOfRecipient = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.mediaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.mediaIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tempOrderItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempOrderItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblNotEnoughNotification);
            this.panel1.Controls.Add(this.flpNavBar);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lblPricewithVAT);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnPayment);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 604);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblNotEnoughNotification
            // 
            this.lblNotEnoughNotification.AutoSize = true;
            this.lblNotEnoughNotification.BackColor = System.Drawing.Color.Transparent;
            this.lblNotEnoughNotification.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotEnoughNotification.ForeColor = System.Drawing.Color.Red;
            this.lblNotEnoughNotification.Location = new System.Drawing.Point(695, 533);
            this.lblNotEnoughNotification.Name = "lblNotEnoughNotification";
            this.lblNotEnoughNotification.Size = new System.Drawing.Size(295, 15);
            this.lblNotEnoughNotification.TabIndex = 29;
            this.lblNotEnoughNotification.Text = "1 Sản phẩm đã hết hàng, vui lòng kiểm tra lại giỏ hàng";
            this.lblNotEnoughNotification.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNotEnoughNotification.Visible = false;
            // 
            // flpNavBar
            // 
            this.flpNavBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(27)))), ((int)(((byte)(7)))));
            this.flpNavBar.Location = new System.Drawing.Point(-1, -3);
            this.flpNavBar.Name = "flpNavBar";
            this.flpNavBar.Size = new System.Drawing.Size(1007, 50);
            this.flpNavBar.TabIndex = 28;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(718, 489);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(182, 15);
            this.label14.TabIndex = 27;
            this.label14.Text = "(1 sản phẩm vận chuyển hóa tốc)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(914, 462);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 20);
            this.label13.TabIndex = 26;
            this.label13.Text = "100.000đ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(717, 462);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(118, 20);
            this.label12.TabIndex = 25;
            this.label12.Text = "Phí vận chuyển:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(914, 425);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 20);
            this.label11.TabIndex = 24;
            this.label11.Text = "100.000đ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPricewithVAT
            // 
            this.lblPricewithVAT.AutoSize = true;
            this.lblPricewithVAT.BackColor = System.Drawing.Color.Transparent;
            this.lblPricewithVAT.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPricewithVAT.Location = new System.Drawing.Point(717, 425);
            this.lblPricewithVAT.Name = "lblPricewithVAT";
            this.lblPricewithVAT.Size = new System.Drawing.Size(103, 20);
            this.lblPricewithVAT.TabIndex = 23;
            this.lblPricewithVAT.Text = "Giá (Đã VAT):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(717, 504);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(273, 20);
            this.label10.TabIndex = 22;
            this.label10.Text = "____________________________________________";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(717, 396);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(273, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "____________________________________________";
            // 
            // btnPayment
            // 
            this.btnPayment.BackColor = System.Drawing.Color.Red;
            this.btnPayment.FlatAppearance.BorderSize = 0;
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayment.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayment.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPayment.Location = new System.Drawing.Point(777, 564);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(213, 30);
            this.btnPayment.TabIndex = 20;
            this.btnPayment.Text = "THANH TOÁN";
            this.btnPayment.UseVisualStyleBackColor = false;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSaveDeliveryInfo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtDeliveryTime);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.checkBoxRushOrder);
            this.groupBox1.Controls.Add(this.checkBoxNormalOrder);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbxWard);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxDistrict);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbxCity);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPhoneNumberOfRecipient);
            this.groupBox1.Controls.Add(this.txtNameOfRecipient);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 521);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "THÔNG TIN GIAO HÀNG";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancel.Location = new System.Drawing.Point(12, 485);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(213, 30);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "HỦY";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveDeliveryInfo
            // 
            this.btnSaveDeliveryInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(27)))), ((int)(((byte)(7)))));
            this.btnSaveDeliveryInfo.FlatAppearance.BorderSize = 0;
            this.btnSaveDeliveryInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDeliveryInfo.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveDeliveryInfo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSaveDeliveryInfo.Location = new System.Drawing.Point(231, 485);
            this.btnSaveDeliveryInfo.Name = "btnSaveDeliveryInfo";
            this.btnSaveDeliveryInfo.Size = new System.Drawing.Size(213, 30);
            this.btnSaveDeliveryInfo.TabIndex = 18;
            this.btnSaveDeliveryInfo.Text = "Lưu thông tin vận chuyển";
            this.btnSaveDeliveryInfo.UseVisualStyleBackColor = false;
            this.btnSaveDeliveryInfo.Click += new System.EventHandler(this.btnSaveDeliveryInfo_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 353);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 22);
            this.label8.TabIndex = 17;
            this.label8.Text = "Thời gian vận chuyển";
            // 
            // txtDeliveryTime
            // 
            this.txtDeliveryTime.Location = new System.Drawing.Point(12, 378);
            this.txtDeliveryTime.Multiline = true;
            this.txtDeliveryTime.Name = "txtDeliveryTime";
            this.txtDeliveryTime.Size = new System.Drawing.Size(213, 101);
            this.txtDeliveryTime.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(231, 353);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(218, 22);
            this.label7.TabIndex = 15;
            this.label7.Text = "Chỉ dẫn cho người vận chuyển";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(231, 378);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(213, 101);
            this.txtDescription.TabIndex = 14;
            // 
            // checkBoxRushOrder
            // 
            this.checkBoxRushOrder.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRushOrder.Location = new System.Drawing.Point(12, 322);
            this.checkBoxRushOrder.Name = "checkBoxRushOrder";
            this.checkBoxRushOrder.Size = new System.Drawing.Size(327, 28);
            this.checkBoxRushOrder.TabIndex = 13;
            this.checkBoxRushOrder.Text = "Vận chuyển hỏa tốc";
            this.checkBoxRushOrder.UseVisualStyleBackColor = true;
            this.checkBoxRushOrder.CheckedChanged += new System.EventHandler(this.checkBoxRushOrder_CheckedChanged);
            // 
            // checkBoxNormalOrder
            // 
            this.checkBoxNormalOrder.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNormalOrder.Location = new System.Drawing.Point(12, 288);
            this.checkBoxNormalOrder.Name = "checkBoxNormalOrder";
            this.checkBoxNormalOrder.Size = new System.Drawing.Size(432, 28);
            this.checkBoxNormalOrder.TabIndex = 12;
            this.checkBoxNormalOrder.Text = "Vận chuyển thường (Nhận được sau 3 - 4 ngày)";
            this.checkBoxNormalOrder.UseVisualStyleBackColor = true;
            this.checkBoxNormalOrder.CheckedChanged += new System.EventHandler(this.checkBoxNormalOrder_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 22);
            this.label6.TabIndex = 11;
            this.label6.Text = "Phường/xã";
            // 
            // cbxWard
            // 
            this.cbxWard.FormattingEnabled = true;
            this.cbxWard.Location = new System.Drawing.Point(12, 249);
            this.cbxWard.Name = "cbxWard";
            this.cbxWard.Size = new System.Drawing.Size(213, 33);
            this.cbxWard.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(231, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 22);
            this.label5.TabIndex = 9;
            this.label5.Text = "Quận/huyện";
            // 
            // cbxDistrict
            // 
            this.cbxDistrict.FormattingEnabled = true;
            this.cbxDistrict.Location = new System.Drawing.Point(235, 188);
            this.cbxDistrict.Name = "cbxDistrict";
            this.cbxDistrict.Size = new System.Drawing.Size(213, 33);
            this.cbxDistrict.TabIndex = 8;
            this.cbxDistrict.SelectedIndexChanged += new System.EventHandler(this.cbxDistrict_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tỉnh/thành";
            // 
            // cbxCity
            // 
            this.cbxCity.FormattingEnabled = true;
            this.cbxCity.Location = new System.Drawing.Point(12, 188);
            this.cbxCity.Name = "cbxCity";
            this.cbxCity.Size = new System.Drawing.Size(213, 33);
            this.cbxCity.TabIndex = 6;
            this.cbxCity.SelectedIndexChanged += new System.EventHandler(this.cbxCity_SelectedIndexChanged);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(12, 123);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(437, 33);
            this.txtAddress.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Địa chỉ";
            // 
            // txtPhoneNumberOfRecipient
            // 
            this.txtPhoneNumberOfRecipient.Location = new System.Drawing.Point(231, 60);
            this.txtPhoneNumberOfRecipient.Name = "txtPhoneNumberOfRecipient";
            this.txtPhoneNumberOfRecipient.Size = new System.Drawing.Size(218, 33);
            this.txtPhoneNumberOfRecipient.TabIndex = 3;
            // 
            // txtNameOfRecipient
            // 
            this.txtNameOfRecipient.Location = new System.Drawing.Point(12, 60);
            this.txtNameOfRecipient.Name = "txtNameOfRecipient";
            this.txtNameOfRecipient.Size = new System.Drawing.Size(213, 33);
            this.txtNameOfRecipient.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(227, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Số điện thoại";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Họ và tên người nhận";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mediaIDDataGridViewTextBoxColumn,
            this.mediaName,
            this.quantityDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tempOrderItemBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(521, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(469, 334);
            this.dataGridView1.TabIndex = 0;
            // 
            // mediaName
            // 
            this.mediaName.DataPropertyName = "mediaName";
            this.mediaName.HeaderText = "Tên sản phẩm";
            this.mediaName.Name = "mediaName";
            this.mediaName.ReadOnly = true;
            this.mediaName.Width = 210;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mediaIDDataGridViewTextBoxColumn
            // 
            this.mediaIDDataGridViewTextBoxColumn.DataPropertyName = "mediaID";
            this.mediaIDDataGridViewTextBoxColumn.HeaderText = "mediaID";
            this.mediaIDDataGridViewTextBoxColumn.Name = "mediaIDDataGridViewTextBoxColumn";
            this.mediaIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.mediaIDDataGridViewTextBoxColumn.Visible = false;
            this.mediaIDDataGridViewTextBoxColumn.Width = 140;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            this.quantityDataGridViewTextBoxColumn.DataPropertyName = "quantity";
            this.quantityDataGridViewTextBoxColumn.HeaderText = "Số lượng";
            this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            this.quantityDataGridViewTextBoxColumn.ReadOnly = true;
            this.quantityDataGridViewTextBoxColumn.Width = 120;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "Giá";
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.Width = 120;
            // 
            // tempOrderItemBindingSource
            // 
            this.tempOrderItemBindingSource.DataSource = typeof(AIMS.Models.Entities.TempOrderItem);
            // 
            // PlaceOrderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PlaceOrderView";
            this.Size = new System.Drawing.Size(1007, 604);
            this.Load += new System.EventHandler(this.PlaceOrderView_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempOrderItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn mediaIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mediaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxWard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxDistrict;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxCity;
        private System.Windows.Forms.MaskedTextBox txtAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtPhoneNumberOfRecipient;
        private System.Windows.Forms.TextBox txtNameOfRecipient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox checkBoxRushOrder;
        private System.Windows.Forms.CheckBox checkBoxNormalOrder;
        private System.Windows.Forms.TextBox txtDeliveryTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveDeliveryInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblPricewithVAT;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.FlowLayoutPanel flpNavBar;
        public System.Windows.Forms.BindingSource tempOrderItemBindingSource;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblNotEnoughNotification;
    }
}
