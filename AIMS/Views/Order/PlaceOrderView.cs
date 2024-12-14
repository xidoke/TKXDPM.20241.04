using AIMS.Controllers.Order;
using AIMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AIMS.Views.Order
{
    public partial class PlaceOrderView : UserControl
    {
        private PlaceOrderController placeOrderController;
        public static PlaceOrderView Instance;
        public PlaceOrderView(List<CartItem> cartItems)
        {
            InitializeComponent();
            Instance = this;
            placeOrderController = new PlaceOrderController();
            placeOrderController.CartItems = cartItems;
        }

        private async void PlaceOrderView_Load(object sender, EventArgs e)
        {
            NavBar navBar = new NavBar();
            flpNavBar.Controls.Add(navBar);
            navBar.Show();
            checkBoxNormalOrder.Checked = true;
            this.cbxCity.Items.Clear();
            foreach (var city in placeOrderController.city)
                this.cbxCity.Items.Add(city);
            await placeOrderController.LoadItemsOrder();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxDistrict.Items.Clear();
            if (placeOrderController.districts.ContainsKey(cbxCity.Text))
            {
                cbxDistrict.Items.AddRange(placeOrderController.districts[cbxCity.Text].ToArray());
            }
        }

        private void cbxDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxWard.Items.Clear();
            if (placeOrderController.wards.ContainsKey(cbxCity.Text))
            {
                cbxWard.Items.AddRange(placeOrderController.wards[cbxCity.Text][cbxDistrict.Text].ToArray());
            }
        }

        private void checkBoxNormalOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxNormalOrder.Checked)
            {
                this.label7.Visible = false;
                this.label8.Visible = false;
                this.txtDeliveryTime.Visible = false;
                this.txtDescription.Visible = false;
                this.checkBoxRushOrder.Checked = false;
            }
        }

        private void checkBoxRushOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxRushOrder.Checked)
            {
                this.label7.Visible = true;
                this.label8.Visible = true;
                this.txtDeliveryTime.Visible = true;
                this.txtDescription.Visible = true;
                this.checkBoxNormalOrder.Checked = false;
            }
            else
            {
                this.label7.Visible = false;
                this.label8.Visible = false;
                this.txtDeliveryTime.Visible = false;
                this.txtDescription.Visible = false;
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.tempOrderItemBindingSource.Count > 0)
            {
                if (checkBoxRushOrder.Checked)
                {
                    label14.Text = $"({placeOrderController.countItemSupportedRushOrder()} sản phẩm vận chuyển hóa tốc)"; label14.Visible = true;
                }
                else
                    label14.Visible = false;
                label11.Text = placeOrderController.GetStringTotalMoneyFormat() + "đ";
                if (placeOrderController.countItemIsNotEnough() > 0)
                {
                    this.btnPayment.Enabled = false;
                    this.lblNotEnoughNotification.Visible = true;
                    this.lblNotEnoughNotification.Text = $"{placeOrderController.countItemIsNotEnough()} sản phẩm đã hết hàng, vui lòng kiểm tra giỏ hàng!";
                }
                else
                {
                    this.btnPayment.Enabled = true;
                    this.lblNotEnoughNotification.Visible = false;
                }
            }
        }

        private void btnSaveDeliveryInfo_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính khả thi của đặt hàng nhanh cho từng mặt hàng 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }
    }
}
