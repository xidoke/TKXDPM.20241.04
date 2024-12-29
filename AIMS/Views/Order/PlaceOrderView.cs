using AIMS.Controllers.Address;
using AIMS.Controllers.Order;
using AIMS.Controllers.Product;
using AIMS.Models.Entities;
using AIMS.Views.Payment;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AIMS.Views.Order
{
    public partial class PlaceOrderView : UserControl
    {
        private readonly PlaceOrderController placeOrderController;
        private readonly AddressController addressController;
        private readonly MediaController mediaController;
        private readonly NavBar navBar;
        public static PlaceOrderView Instance;

        public PlaceOrderView(List<CartItem> cartItems, PlaceOrderController placeOrderController, 
            AddressController addressController, NavBar navBar, MediaController mediaController)
        {
            InitializeComponent();
            Instance = this;
            this.placeOrderController = placeOrderController;
            this.addressController = addressController;
            this.navBar = navBar;
            this.mediaController = mediaController;
            placeOrderController.CartItems = cartItems;
        }

        private async void PlaceOrderView_Load(object sender, EventArgs e)
        {
            flpNavBar.Controls.Add(navBar);
            navBar.Show();
            checkBoxNormalOrder.Checked = true;
            cbxCity.Items.Clear();

            await addressController.GetProvincesAsync(cbxCity);
            await placeOrderController.LoadItemsOrder();
        }

        private async void cbxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxDistrict.Text = "";
            cbxWard.Text = "";
            cbxDistrict.Items.Clear();
            await addressController.GetDistrictsByProvinceAsync(cbxCity, cbxDistrict);
        }

        private async void cbxDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxWard.Text = "";
            cbxWard.Items.Clear();
            await addressController.GetWardsByDistrictAsync(cbxCity, cbxDistrict, cbxWard);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
            // Gửi orderdata lên database
            // Lấy ID của orderData vừa up lên database
            // set ID của orderData trong placeOrderControll 
            PaymentView paymentView = new PaymentView(placeOrderController.orderData);
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(paymentView);
            paymentView.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.tempOrderItemBindingSource.Count > 0)
            {
                if (checkBoxRushOrder.Checked)
                {
                    label14.Text = $"({placeOrderController.CountItemSupportedRushOrder()} sản phẩm vận chuyển hóa tốc)"; label14.Visible = true;
                }
                else
                    label14.Visible = false;
                label11.Text = placeOrderController.GetStringTotalMoneyFormat() + "đ";
                if (placeOrderController.CountItemIsNotEnough() > 0)
                {
                    this.btnPayment.Enabled = false;
                    this.lblNotEnoughNotification.Visible = true;
                    this.lblNotEnoughNotification.Text = $"{placeOrderController.CountItemIsNotEnough()} sản phẩm đã hết hàng, vui lòng kiểm tra giỏ hàng!";
                }
                else
                {
                    this.btnPayment.Enabled = true;
                    this.lblNotEnoughNotification.Visible = false;
                }
            }
        }

        private async void btnSaveDeliveryInfo_Click(object sender, EventArgs e)
        {
            string name = txtNameOfRecipient.Text?.Trim();
            string phoneNumber = txtPhoneNumberOfRecipient.Text?.Trim();
            string address = txtAddress.Text?.Trim();

            string selectedCity = cbxCity.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedCity))
            {
                MessageBox.Show("Vui lòng chọn Tỉnh/Thành phố!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedDistrict = cbxDistrict.SelectedItem?.ToString();
            string selectedWard = cbxWard.SelectedItem?.ToString();
            bool isRushOrder = checkBoxRushOrder.Checked;
            string deliveryTime = txtDeliveryTime.Text?.Trim();
            string description = txtDescription.Text?.Trim();

            int shippingFee = await placeOrderController.CalculateShippingFee(selectedCity, isRushOrder);

            var validator = new DeliveryInfoValidator();
            string validationMessage = validator.ValidateDeliveryInfo(name, phoneNumber, address,
                selectedCity, selectedDistrict, selectedWard);

            if (validationMessage.Contains("Delivery information is valid."))
            {
                MessageBox.Show(validationMessage, "Valid Address", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtShippingFee.Text = shippingFee.ToString() + "đ";
                await placeOrderController.SetOrderData(name, phoneNumber, address, selectedCity,
                    selectedDistrict, selectedWard, shippingFee, isRushOrder, deliveryTime, description);
            }
            else
            {
                MessageBox.Show(validationMessage, "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            HomeView homeView = new HomeView(mediaController, navBar);
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(homeView);
            homeView.Show();
        }

    }
}
