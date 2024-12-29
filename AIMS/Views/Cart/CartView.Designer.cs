namespace AIMS.Views.Cart
{
    partial class CartView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.flpNavBar = new System.Windows.Forms.FlowLayoutPanel();
            this.placeOrderButton = new System.Windows.Forms.Button();
            this.updateCartButton = new System.Windows.Forms.Button();
            this.cartItemQuantityNumeric = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.viewName = new System.Windows.Forms.Label();
            this.dataGridViewCartItems = new System.Windows.Forms.DataGridView();
            this.isSelectedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mediaidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mediatitleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isPossibleToPlaceOrderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cartItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cartItemQuantityNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCartItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.flpNavBar);
            this.panel1.Controls.Add(this.placeOrderButton);
            this.panel1.Controls.Add(this.updateCartButton);
            this.panel1.Controls.Add(this.cartItemQuantityNumeric);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.viewName);
            this.panel1.Controls.Add(this.dataGridViewCartItems);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1510, 929);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 865);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 32);
            this.label1.TabIndex = 10;
            // 
            // flpNavBar
            // 
            this.flpNavBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(27)))), ((int)(((byte)(7)))));
            this.flpNavBar.Location = new System.Drawing.Point(0, 0);
            this.flpNavBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flpNavBar.Name = "flpNavBar";
            this.flpNavBar.Size = new System.Drawing.Size(1510, 77);
            this.flpNavBar.TabIndex = 9;
            // 
            // placeOrderButton
            // 
            this.placeOrderButton.BackColor = System.Drawing.Color.Firebrick;
            this.placeOrderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.placeOrderButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.placeOrderButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.placeOrderButton.Location = new System.Drawing.Point(1029, 865);
            this.placeOrderButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.placeOrderButton.Name = "placeOrderButton";
            this.placeOrderButton.Size = new System.Drawing.Size(231, 60);
            this.placeOrderButton.TabIndex = 8;
            this.placeOrderButton.Text = "ĐẶT HÀNG";
            this.placeOrderButton.UseVisualStyleBackColor = false;
            this.placeOrderButton.Click += new System.EventHandler(this.placeOrderButton_Click);
            // 
            // updateCartButton
            // 
            this.updateCartButton.BackColor = System.Drawing.Color.Chartreuse;
            this.updateCartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateCartButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateCartButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.updateCartButton.Location = new System.Drawing.Point(1269, 865);
            this.updateCartButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.updateCartButton.Name = "updateCartButton";
            this.updateCartButton.Size = new System.Drawing.Size(231, 60);
            this.updateCartButton.TabIndex = 7;
            this.updateCartButton.Text = "Cập nhật giỏ hàng";
            this.updateCartButton.UseVisualStyleBackColor = false;
            this.updateCartButton.Click += new System.EventHandler(this.updateCartButton_Click);
            // 
            // cartItemQuantityNumeric
            // 
            this.cartItemQuantityNumeric.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cartItemQuantityNumeric.Location = new System.Drawing.Point(1400, 805);
            this.cartItemQuantityNumeric.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cartItemQuantityNumeric.Name = "cartItemQuantityNumeric";
            this.cartItemQuantityNumeric.Size = new System.Drawing.Size(98, 39);
            this.cartItemQuantityNumeric.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1164, 795);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(226, 60);
            this.button2.TabIndex = 5;
            this.button2.Text = "Thay đổi số lượng";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1029, 795);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 60);
            this.button1.TabIndex = 4;
            this.button1.Text = "Xóa";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1029, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Bạn đang chọn: ";
            // 
            // viewName
            // 
            this.viewName.AutoSize = true;
            this.viewName.BackColor = System.Drawing.Color.Transparent;
            this.viewName.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewName.Location = new System.Drawing.Point(4, 83);
            this.viewName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.viewName.Name = "viewName";
            this.viewName.Size = new System.Drawing.Size(118, 32);
            this.viewName.TabIndex = 1;
            this.viewName.Text = "Giỏ hàng";
            // 
            // dataGridViewCartItems
            // 
            this.dataGridViewCartItems.AllowUserToAddRows = false;
            this.dataGridViewCartItems.AllowUserToDeleteRows = false;
            this.dataGridViewCartItems.AllowUserToResizeColumns = false;
            this.dataGridViewCartItems.AllowUserToResizeRows = false;
            this.dataGridViewCartItems.AutoGenerateColumns = false;
            this.dataGridViewCartItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCartItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isSelectedDataGridViewCheckBoxColumn,
            this.mediaidDataGridViewTextBoxColumn,
            this.mediatitleDataGridViewTextBoxColumn,
            this.quantityDataGridViewTextBoxColumn,
            this.total_money,
            this.isPossibleToPlaceOrderDataGridViewTextBoxColumn});
            this.dataGridViewCartItems.DataSource = this.cartItemBindingSource;
            this.dataGridViewCartItems.Location = new System.Drawing.Point(4, 125);
            this.dataGridViewCartItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewCartItems.Name = "dataGridViewCartItems";
            this.dataGridViewCartItems.RowHeadersVisible = false;
            this.dataGridViewCartItems.RowHeadersWidth = 62;
            this.dataGridViewCartItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCartItems.Size = new System.Drawing.Size(1016, 725);
            this.dataGridViewCartItems.TabIndex = 0;
            this.dataGridViewCartItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCartItems_CellClick);
            this.dataGridViewCartItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCartItems_CellContentClick_1);
            // 
            // isSelectedDataGridViewCheckBoxColumn
            // 
            this.isSelectedDataGridViewCheckBoxColumn.DataPropertyName = "isSelected";
            this.isSelectedDataGridViewCheckBoxColumn.HeaderText = "";
            this.isSelectedDataGridViewCheckBoxColumn.MinimumWidth = 8;
            this.isSelectedDataGridViewCheckBoxColumn.Name = "isSelectedDataGridViewCheckBoxColumn";
            this.isSelectedDataGridViewCheckBoxColumn.Width = 30;
            // 
            // mediaidDataGridViewTextBoxColumn
            // 
            this.mediaidDataGridViewTextBoxColumn.DataPropertyName = "media_id";
            this.mediaidDataGridViewTextBoxColumn.HeaderText = "Mã sản phẩm";
            this.mediaidDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.mediaidDataGridViewTextBoxColumn.Name = "mediaidDataGridViewTextBoxColumn";
            this.mediaidDataGridViewTextBoxColumn.ReadOnly = true;
            this.mediaidDataGridViewTextBoxColumn.Width = 150;
            // 
            // mediatitleDataGridViewTextBoxColumn
            // 
            this.mediatitleDataGridViewTextBoxColumn.DataPropertyName = "media_title";
            this.mediatitleDataGridViewTextBoxColumn.HeaderText = "Tên sản phẩm";
            this.mediatitleDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.mediatitleDataGridViewTextBoxColumn.Name = "mediatitleDataGridViewTextBoxColumn";
            this.mediatitleDataGridViewTextBoxColumn.ReadOnly = true;
            this.mediatitleDataGridViewTextBoxColumn.Width = 180;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            this.quantityDataGridViewTextBoxColumn.DataPropertyName = "quantity";
            this.quantityDataGridViewTextBoxColumn.HeaderText = "Số lượng";
            this.quantityDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            this.quantityDataGridViewTextBoxColumn.ReadOnly = true;
            this.quantityDataGridViewTextBoxColumn.Width = 150;
            // 
            // total_money
            // 
            this.total_money.DataPropertyName = "total_money";
            this.total_money.HeaderText = "Số tiền";
            this.total_money.MinimumWidth = 8;
            this.total_money.Name = "total_money";
            this.total_money.Width = 150;
            // 
            // isPossibleToPlaceOrderDataGridViewTextBoxColumn
            // 
            this.isPossibleToPlaceOrderDataGridViewTextBoxColumn.DataPropertyName = "isPossibleToPlaceOrder";
            this.isPossibleToPlaceOrderDataGridViewTextBoxColumn.HeaderText = "Trạng thái";
            this.isPossibleToPlaceOrderDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.isPossibleToPlaceOrderDataGridViewTextBoxColumn.Name = "isPossibleToPlaceOrderDataGridViewTextBoxColumn";
            this.isPossibleToPlaceOrderDataGridViewTextBoxColumn.ReadOnly = true;
            this.isPossibleToPlaceOrderDataGridViewTextBoxColumn.Width = 150;
            // 
            // cartItemBindingSource
            // 
            this.cartItemBindingSource.DataSource = typeof(AIMS.Models.Entities.CartItem);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CartView";
            this.Size = new System.Drawing.Size(1510, 929);
            this.Load += new System.EventHandler(this.CartView_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cartItemQuantityNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCartItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataGridView dataGridViewCartItems;
        public System.Windows.Forms.BindingSource cartItemBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label viewName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSelectedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mediaidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mediatitleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn isPossibleToPlaceOrderDataGridViewTextBoxColumn;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown cartItemQuantityNumeric;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button updateCartButton;
        private System.Windows.Forms.FlowLayoutPanel flpNavBar;
        private System.Windows.Forms.Button placeOrderButton;
        private System.Windows.Forms.Label label1;
    }
}
