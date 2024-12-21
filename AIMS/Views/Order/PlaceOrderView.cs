using AIMS.Controllers.Cart;
using AIMS.Controllers.Order;
using AIMS.Enum;
using AIMS.Models.Entities;
using AIMS.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AIMS.Views.Order
{
    public partial class PlaceOrderView : UserControl
    {
        private PlaceOrderController placeOrderController;
        public static PlaceOrderView Instance;
        private readonly ProvinceService _provinceService;
        private readonly DistrictService _districtService;
        private readonly WardService _wardService;

        public PlaceOrderView(List<CartItem> cartItems)
        {
            InitializeComponent();
            Instance = this;
            placeOrderController = new PlaceOrderController();
            placeOrderController.CartItems = cartItems;
            _provinceService = new ProvinceService();
            _districtService = new DistrictService();
            _wardService = new WardService();
        }

        private async void PlaceOrderView_Load(object sender, EventArgs e)
        {
            NavBar navBar = new NavBar();
            flpNavBar.Controls.Add(navBar);
            navBar.Show();

            checkBoxNormalOrder.Checked = true;

            this.cbxCity.Items.Clear();
    
            var provinces = await _provinceService.GetAllProvincesAsync();
            foreach (var province in provinces)
            {
                this.cbxCity.Items.Add(province.Name);
            }

            await placeOrderController.LoadItemsOrder();
        }

        private async void cbxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxDistrict.Text = "";
            cbxWard.Text = "";
            cbxDistrict.Items.Clear();
            if (!string.IsNullOrEmpty(cbxCity.SelectedItem?.ToString()))
            {
                var selectedProvince = await _provinceService.GetProvinceByNameAsync(cbxCity.SelectedItem.ToString());
                var districts = await _districtService.GetDistrictsByProvinceAsync(selectedProvince?.Id);
                foreach (var district in districts)
                {
                    cbxDistrict.Items.Add(district.Name);
                }
            }

           
        }

        private async void cbxDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxWard.Text = "";
            cbxWard.Items.Clear();
            if (!string.IsNullOrEmpty(cbxCity.SelectedItem?.ToString()) && !string.IsNullOrEmpty(cbxDistrict.SelectedItem?.ToString()))
            {
                var selectedDistrict = await _districtService.GetDistrictByNameAsync(cbxDistrict.SelectedItem.ToString());
                var wards = await _wardService.GetWardsByDistrictAsync(selectedDistrict?.Id);
                foreach (var ward in wards)
                {
                    cbxWard.Items.Add(ward.Name);
                }
            }
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

            int shippingFee = await placeOrderController.CalculateShippingFee(selectedCity);

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
            HomeView homeView = new HomeView();
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(homeView);
            homeView.Show();
        }
        
    }
}
